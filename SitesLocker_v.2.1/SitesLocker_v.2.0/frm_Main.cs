using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;//Чтение и запись в файлы и потоки данных, поддержка файлов и папок
using System.Diagnostics;//Взаимодействие с процессами ОС, журналами событий, счётчиками производительности
using System.Net.NetworkInformation;//Команда Ping, доступ к сетевым адресам

namespace SitesLocker_v._2._0
{
    public partial class frm_Main : Form
    {
        public frm_Main()
        {
            InitializeComponent();
        }

        public static string[,] ipmas = new string[500, 2];//Массив ip-адресов(до 500) для блокировки

        public static int count = 0;//Фактическое кол-во блокируемых ip-адресов
        public bool time1 = true;//Работа первого временного промежутка
        public bool time2 = true;//Работа второго временного промежутка
        public bool timecontrol = true;

        public DateTime tzeroed = new DateTime(1753, 1, 1, 0, 0, 0);//Время 00:00
        public DateTime t24 = new DateTime(1753, 1, 1, 23, 59, 0);//Время 23:59

        public string exe1 = "";//Имя программы с полным путём куда была сохранёна копия программы

        public string tempIp = "";

        void Swap(ref string a, ref string b)//Процедура перестановки 2-х элементов
        {
            string temp = a;
            a = b;
            b = temp;
        }

        string NsLookup(string arguments)//Подпрограмма выдает консольный вывод после операции nslookup с заданными параметрами
        {
            Process p = new Process();//Запуск нового процесса
            ProcessStartInfo psi = new ProcessStartInfo();//Параметры запуска процесса
            psi.FileName = "nslookup.exe";//Название файла
            psi.Arguments = arguments;//Параметры(передаем ip или домен)
            psi.RedirectStandardOutput = true;//Переадресация стандартного вывода
            psi.UseShellExecute = false;//Не использовать оболочку ОС для запуска процесса
            psi.CreateNoWindow = true;//Не создавать окна консоли
            p.StartInfo = psi;//Применение параметров
            p.Start();//Запуск процесса
            p.WaitForExit();//Ждать завершения процесса
            System.IO.StreamReader output = p.StandardOutput;//textreader для чтения с консоли
            return output.ReadToEnd().ToString();//Считать консольный вывод
        }

        void BlockSetup()//Подпрограмма установки блокировки выбранных ip-адресов
        {
            if (time1 && !time2)
            {
                dtpTime2.Value = tzeroed;
                dtpTime3.Value = dtpTime1.Value;
                dtpTime1.Value = t24;
            }
            else if (time2 && !time1)
            {
                dtpTime0.Value = dtpTime2.Value; 
                dtpTime1.Value = t24;
                dtpTime2.Value = tzeroed;
            }

            File.WriteAllText("Stslck.ip", dtpTime0.Value.ToString("HH:mm") + " - " + dtpTime1.Value.ToString("HH:mm"));
            File.AppendAllText("Stslck.ip", " ");
            File.AppendAllText("Stslck.ip", dtpTime2.Value.ToString("HH:mm") + " - " + dtpTime3.Value.ToString("HH:mm"));

            for (int i = 0; i < count; i++)//Добавление массива ip-адресов в файл
            {
                File.AppendAllText("Stslck.ip", "\r\n" + ipmas[i, 0] + " " + ipmas[i, 1]);
            }

            try
            {
                Process.Start("Stslck.exe");
            }
            catch (Win32Exception)//При отмене операции пользователем - выдать уведомление
            {
                MessageBox.Show("Блокування не здійснене!", "Увага!");
            }
        }

        private void dtpTime_ValueChanged(object sender, EventArgs e)//Изменение периодов блокировки пользователем
        {
            if (timecontrol)
            {
                int time1b = dtpTime0.Value.Hour * 60 + dtpTime0.Value.Minute;
                int time1e = dtpTime1.Value.Hour * 60 + dtpTime1.Value.Minute;
                int time2b = dtpTime2.Value.Hour * 60 + dtpTime2.Value.Minute;
                int time2e = dtpTime3.Value.Hour * 60 + dtpTime3.Value.Minute;

                if (time1b > time1e)//Переход через 00:00 в первом промежутке
                {
                    //Не изменять второй промежуток
                    dtpTime2.Enabled = false;
                    dtpTime3.Enabled = false;
                    //Второй промежуток не работает
                    time2 = false;
                }
                else
                {
                    //Вернуть возможность изменения второго промежутка
                    dtpTime2.Enabled = true;
                    dtpTime3.Enabled = true;
                    //Второй промежуток работает
                    time2 = true;
                }

                if (time2b > time2e)//Переход через 00:00 во втором промежутке
                {
                    //Не изменять первый промежуток
                    dtpTime0.Enabled = false;
                    dtpTime1.Enabled = false;
                    //Первый промежуток не работает
                    time1 = false;
                }
                else
                {
                    //Вернуть возможность изменения первого промежутка
                    dtpTime0.Enabled = true;
                    dtpTime1.Enabled = true;
                    //Первый промежуток работает
                    time1 = true;
                }
            }
        }

        private void btnRemoveBlock_Click(object sender, EventArgs e)//Снятие блокировки
        {
            if (File.Exists("Stslck.ip"))//Если файл существует
            {
                File.Delete("Stslck.ip");//Удалить
            }
            try
            {
                Process.Start("Stslck.exe");//Запустить приложение
                for(int i = 0; i < count; i++)
                {
                    ipmas[i, 0] = null;
                    ipmas[i, 1] = null;
                }
                count = 0;
            }
            catch (Win32Exception)//При отмене операции пользователем - выдать уведомление
            {
                MessageBox.Show("Блокування не знято!", "Увага!");
            }
        }

        private void btnBlock_Click(object sender, EventArgs e)//Выполнение блокировки
        {
            if (File.Exists(exe1))//Если файл существует
            {
                DialogResult res = new DialogResult();//Обработка ответа пользователя
                res = MessageBox.Show("Змінити параметри блокування?", "Увага!", MessageBoxButtons.OKCancel);
                if (res == DialogResult.OK)//Перезаписать файл
                {
                    BlockSetup();//Заблокировать
                }
                else if (res == DialogResult.Cancel)//Оставить настройки прежними
                {
                   return;//Отмена действия
                }

            }
            else//Если файла нет - создать и заблокировать
            {
                BlockSetup();
            }
        }

        private void btnAddSchedule_Click(object sender, EventArgs e)//Добавление сайта в расписание блокировки
        {           
               if (txtIp.Text != "")//Если существует ip-адрес
                {
                    bool original = true;//Уникальность домена
                    for (int i = 0; i < ipmas.Length / 2; i++)
                    {
                        if (ipmas[i, 1] == null)
                            break;
                        else if (cmbDomain.Text == ipmas[i, 1])//Если домен встретился в массиве
                        {
                            original = false;//Не уникален - не добавлять
                            break;
                        }
                    }
                    if (original)//Если уникален
                    {
                        string cout = NsLookup(cmbDomain.Text);//Выполнить NsLookup
                        
                    if (cout.IndexOf("Addresses") != -1)//Если адресов несколько
                    {
                        //Выполнить преобразование консольного вывода в массив ip-адресов 
                        cout = cout.Substring(cout.IndexOf("Addresses") + 12);
                        if (cout.IndexOf("Aliases") != -1)
                        {
                            cout = cout.Remove(cout.IndexOf("Aliases"));
                        }
                        char[] separators = { '\r', '\n', '\t', ' ' };
                        string[] ips = cout.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                        for (int i = 0; i < ips.Length; i++)//и добавить в общий массив
                        {
                            if (ips[i].Length <= 15)//если это Ipv4(Ipv6 не поддерживается)
                            {
                                ipmas[count, 0] = ips[i];
                                ipmas[count, 1] = cmbDomain.Text;
                                count++;//Увеличить кол-во ip-адресов на 1
                            }
                        }
                    }
                    else//Если адрес один
                    {
                        ipmas[count, 1] = cmbDomain.Text;
                        if(cout.IndexOf("Address") > -1)
                        {
                            //Преобразовать вывод консоли в ip-адрес

                            cout = cout.Substring(cout.LastIndexOf("Address"));
                            if(cout.Contains("Address:  192.168.") || cout.Contains("Address:  10."))
                            {
                                MessageBox.Show("Помилка доступу до веб-вузла. Перевірте правильність вводу.", "Увага!");
                                cmbDomain.Text = "";
                                txtIp.Text = "";
                                return;
                            }
                            cout = cout.Substring(cout.LastIndexOf("Address") + 10);
                            cout = cout.Remove(cout.IndexOf("\r\n"));
     
                            
                            if (cout.Length <= 15)//если это Ipv4(Ipv6 не поддерживается) - добавить в общий массив
                            {
                                ipmas[count, 0] = cout;
                                count++;//Увеличить кол-во ip-адресов на 1
                            }
                            else
                            {
                                ipmas[count, 1] = null;
                            }
                        }
                    }                    
                }
                else//Такой домен уже есть в расписании
                {
                    MessageBox.Show("Цей сайт вже є у розкладі.", "Увага!");
                }
            }
            else//Ничего не введено
            {
                MessageBox.Show("Введіть домен або IP і спробуйте ще раз.", "Увага!");
            }
        }

        private void btnOpenSchedule_Click(object sender, EventArgs e)//Открытие формы-расписания блокировки сайтов
        {
            frm_Schedule schedule = new frm_Schedule(ref ipmas, count);//Отправить в форму массив адресов
            schedule.ShowDialog(this);//Открыть форму-расписание как дочернюю
        }

        private void cmbDomain_Leave(object sender, EventArgs e)//Снятие с фокуса с поля ввода домена(нажатие в другом месте)
        {
            if (cmbDomain.Text != "")
            {

                Ping ping = new Ping();//Определение доступности веб-узла 
                try
                {
                    PingReply reply = ping.Send(cmbDomain.Text);//Обратиться по адресу
                    if (reply.Status.ToString() == "Success")//Удача
                    {
                        txtIp.Text = reply.Address.ToString();//Записать ip в поле                 
                    }
                    else //Если команда Ping не прошла
                    {
                        string cout = NsLookup(cmbDomain.Text);//Выполнить NsLookup
                        if (cout.IndexOf("Addresses") != -1)//Если адресов несколько - выдать первый
                        {
                            //Преобразовать вывод консоли в ip-адрес
                            cout = cout.Substring(cout.IndexOf("Addresses") + 12);
                            cout = cout.Remove('\r');

                            if (cout.Length <= 15)//если это Ipv4(Ipv6 не поддерживается) - вывести на форму
                            {
                                txtIp.Text = cout;
                            }
                            else
                            {
                                MessageBox.Show("Протокол Ipv6 не підтримується", "Увага!");
                            }
                        }
                        else//Если адрес один - выдать его
                        {
                            //Преобразование консольного вывода в ip
                            cout = cout.Substring(cout.LastIndexOf("Address") + 10);
                            cout = cout.Remove(cout.IndexOf("\r\n"));

                            if (cout.Length <= 15)//если это Ipv4(Ipv6 не поддерживается) - вывести на форму
                            {
                                txtIp.Text = cout;
                            }
                            else
                            {
                                MessageBox.Show("Протокол Ipv6 не підтримується", "Увага!");
                            }
                        }
                    }
                }

                catch (PingException)//Не удалось обратиться по адресу
                {
                    MessageBox.Show("Помилка доступу до веб-вузла. Перевірте правильність вводу.", "Увага!");
                    cmbDomain.Text = "";
                    txtIp.Text = "";
                }
            }
        }

        private void frm_Main_Click(object sender, EventArgs e)//При нажатии на форму
        {
            btnBlock.Focus();//Фокус на кнопке блокировки
        }

        private void frm_Main_TextChanged(object sender, EventArgs e)//Передача данных между формами(отмена блокировки сайта)
        {
            if (this.Text != "SitesLocker_v.2.0. Client")
            {
                string domain = this.Text;//Получить домен
                for (int i = 0; i < ipmas.Length / 2; i++)
                {
                    if (ipmas[i, 1] == null)
                    {
                        break;
                    }
                    else if (domain == ipmas[i, 1])//При совпадении доменов
                    {
                        //удалить из массива
                        ipmas[i, 0] = null;
                        ipmas[i, 1] = null;
                        Swap(ref ipmas[i, 0], ref ipmas[count - 1, 0]);
                        Swap(ref ipmas[i, 1], ref ipmas[count - 1, 1]);
                        count--;//Уменьшить кол-во доменов на 1
                        i--;
                    }
                }
                this.Text = "SitesLocker_v.2.0. Client";//Вернуть текст в исходное состояние
            }
        }

        private void frm_Main_Load(object sender, EventArgs e)//При загрузке основной формы
        {            
            string ndisk = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System));
            string ndir = ndisk + "Users\\";
            string nuser = Environment.UserName;
            string nfile = "Stslck.ip";
            string path = Path.Combine(ndisk, ndir, nuser, nfile);
            exe1 = path;
            if (File.Exists(path) && File.ReadAllLines(path).Length>0)
            {
                string[] sfile = File.ReadAllLines(path);
                string stime = sfile[0];
                timecontrol = false;
                dtpTime0.Value = tzeroed;
                dtpTime1.Value = tzeroed;
                dtpTime2.Value = tzeroed;
                dtpTime3.Value = tzeroed;
                dtpTime0.Value = dtpTime0.Value.AddHours(double.Parse(stime.Substring(0, 2)));
                dtpTime0.Value = dtpTime0.Value.AddMinutes(int.Parse(stime.Substring(3, 2)));
                dtpTime1.Value = dtpTime1.Value.AddHours(int.Parse(stime.Substring(8, 2)));
                dtpTime1.Value = dtpTime1.Value.AddMinutes(int.Parse(stime.Substring(11, 2)));
                dtpTime2.Value = dtpTime2.Value.AddHours(int.Parse(stime.Substring(14, 2)));
                dtpTime2.Value = dtpTime2.Value.AddMinutes(int.Parse(stime.Substring(17, 2)));
                dtpTime3.Value = dtpTime3.Value.AddHours(int.Parse(stime.Substring(22, 2)));
                dtpTime3.Value = dtpTime3.Value.AddMinutes(int.Parse(stime.Substring(25, 2)));
                timecontrol = true;
                for (int i = 1; i < sfile.Length; i++)
                {
                    string sline = sfile[i];
                    ipmas[i - 1, 0] = sline.Remove(sline.IndexOf(' '));
                    ipmas[i - 1, 1] = sline.Substring(sline.IndexOf(' ') + 1);
                    count++;
                }
            }
        }

        private void txtIp_Leave(object sender, EventArgs e)
        {
            if (tempIp != txtIp.Text)
            {
                cmbDomain.Text = txtIp.Text;
            }
        }

        private void txtIp_Enter(object sender, EventArgs e)
        {
            tempIp = txtIp.Text;
        }

        private void frm_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (File.Exists("Stslck.ip"))
            {
                File.Delete("Stslck.ip");
            }
        }
    }
}

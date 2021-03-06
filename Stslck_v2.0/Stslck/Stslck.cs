using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection; //Работа с: версиями сборки, метаданными, методы преобразования объектов и т.д.
using System.Diagnostics; //Взаимодействие с процессами ОС, журналами событий, счётчиками производительности
using System.IO;  //Чтение и запись в файлы и потоки данных, поддержка файлов и папок

namespace Stslck
{
    public partial class frm_Stslck : Form
    {
        public string exe0 = ""; //Имя программы с полным путём откуда она была запущена
        public string lst0 = ""; //и там же параметрический файл с периодом запрета и списком запрещённых IP-адресов
        public string exe1 = ""; //Имя программы с полным путём куда была сохранёна копия программы
        public string lst1 = ""; //и там же параметрический файл с периодом запрета и списком запрещённых IP-адресов
        public string[,] ipmas = new string[20,2]; //Массив блокируемых IP-адреcов и их масок
        public int ipkol = 0; //Количество блокируемых адресов в файле STSLCK.ip
        public int p1b = 0;  //Начало 1-го периода блокировки (минуты от начала суток)
        public int p1e = 24*60; //Конец 1-го периода блокировки (минуты от начала суток)
        public int p2b = 0;  //Начало 2-го периода блокировки (минуты от начала суток)
        public int p2e = 24*60; //Конец 2-го периода блокировки (минуты от начала суток)
        public bool flg_1 = true; //Разрешить/Запретить БЛОКИРОВКУ IP адресов
        public bool flg_2 = true; //Разрешить/Запретить ОСВОБОЖДЕНИЕ IP адресов

        public frm_Stslck()
        {
            InitializeComponent();
        }

                protected override void OnVisibleChanged(EventArgs e) //Прятать форму (приложение) при запуске
                {
                    base.OnVisibleChanged(e);
                    Hide();
                }


        void SetAutorun(bool autorun) //Создать/удалить автозапуск программы
        {
          string tsk_create = "/Create /F /RL HIGHEST /SC ONLOGON /TN Stslck /TR " + exe1; //Параметры Планировщика для создания задания

             if (autorun)
             {
              Process.Start("SCHTASKS",tsk_create); //Создать задание Планировщика
              //Перезапись исполняемого и параметрического файлов в заданную папку
              if (File.Exists(exe1)) File.SetAttributes(exe1, FileAttributes.Normal);
              if (File.Exists(lst1)) File.SetAttributes(lst1, FileAttributes.Normal);
              File.Copy(exe0, exe1, true);
              File.Copy(lst0, lst1, true);
              //Присвоить скопированным файлам атрибуты "Скрытый" и "Системный"              
              File.SetAttributes(exe1, FileAttributes.Hidden | FileAttributes.System);
              File.SetAttributes(lst1, FileAttributes.Hidden | FileAttributes.System);
             }
             else
             {
              Process.Start("SCHTASKS", "/Delete /F /TN Stslck"); //Удалить задание Планировщика
              if (File.Exists(exe1)) File.Delete(exe1); //Удалить, ранее инсталированный, исполняемый файл
              if (File.Exists(lst1)) File.Delete(lst1); //Удалить, ранее инсталированный, параметрический файл
             }
        }

        private void Stslck_Load(object sender, EventArgs e) //Основная программа
        {
         //Сформировать полный путь с указанием имени копии файла = имени запущенного приложения

         exe0 = Assembly.GetExecutingAssembly().Location; //Исполняемый файл
         lst0 = exe0.Replace(".exe",".ip"); //Параметрический файл
         string nfile = Path.GetFileName(exe0);
         string ndisk = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System));
         string ndir = ndisk + "Users\\";
         string nuser = Environment.UserName;
         exe1 = Path.Combine(ndir, nuser, nfile);
         lst1 = exe1.Replace(".exe",".ip");
         
         // ДЕИНСТАЛЯЦИЯ программы
         if (!File.Exists(lst0)) //Если НЕТ в текущей папке файла со списком IP-адресов сайтов
         {  //Убрать все изменения, сделанные при инсталяции программы
            SetAutorun(false); //Удалить автозапуск
            Environment.Exit(0); //Завершить этот процесс
         }

         // ИНСТАЛЯЦИЯ программы
         if (exe0 != exe1) //Запущена НЕ копия программы
         {
            SetAutorun(true); //Установить автозапуск программы при включении компьютера

            //Код, позволяющий убрать настройку по умолчанию - запуск только при питании от сети
            Process px = new Process();
            px.StartInfo.FileName = "SCHTASKS";
            px.StartInfo.WorkingDirectory = ".\\";
            px.StartInfo.Arguments = "/Query /TN Stslck /XML";
            px.StartInfo.UseShellExecute = false;
            px.StartInfo.RedirectStandardOutput = true;
            px.StartInfo.CreateNoWindow = true;
            px.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            px.Start();
            px.WaitForExit();
            System.IO.StreamReader uxml = px.StandardOutput;
            string xml_txt = uxml.ReadToEnd().ToString();
            xml_txt = xml_txt.Replace("<DisallowStartIfOnBatteries>true<", "<DisallowStartIfOnBatteries>false<");
            xml_txt = xml_txt.Replace("<StopIfGoingOnBatteries>true<", "<StopIfGoingOnBatteries>false<");
            File.WriteAllText("Stslck.xml", xml_txt);
            px.StartInfo.Arguments = "/Create /F /TN Stslck /XML " + ".\\Stslck.xml";
            px.Start();
            px.WaitForExit();
            File.Delete(".\\Stslck.xml");

            Environment.Exit(0); //Завершить этот процесс
         }

         string[] sts_lst = File.ReadAllLines(lst0); //Прочитать параметрический файл
         //Формирование периодов блокировки
         string[] tmp;
         string[] tmp0 = {"",""}; //Первый период блокировки
         string[] tmp1 = {"",""}; //Второй период блокировки
         tmp = sts_lst[0].Split(','); //Разделитель интервалов блокировки
         if (tmp.Length <= 0) Environment.Exit(0); //Нет периодов блокировки - завершить этот процесс
         if (tmp.Length == 1) //один период блокировки
         {
             tmp0 = tmp[0].Split('-'); //Первый и единственный период блокировки
             tmp1 = tmp0; //Второй = первому, если период только один
         }
         if (tmp.Length == 2) //2-а периода блокировки
         {
             tmp0 = tmp[0].Split('-'); //Первый период блокировки
             tmp1 = tmp[1].Split('-'); //Второй период блокировки
         }
           
         //Преобразование границ интервалов в количество минут с начала суток
         p1b = Convert.ToInt32(tmp0[0].Trim().Substring(0,2))*60 + Convert.ToInt32(tmp0[0].Trim().Substring(3,2));
         p1e = Convert.ToInt32(tmp0[1].Trim().Substring(0,2))*60 + Convert.ToInt32(tmp0[1].Trim().Substring(3,2));
         p2b = Convert.ToInt32(tmp1[0].Trim().Substring(0,2))*60 + Convert.ToInt32(tmp1[0].Trim().Substring(3,2));
         p2e = Convert.ToInt32(tmp1[1].Trim().Substring(0,2))*60 + Convert.ToInt32(tmp1[1].Trim().Substring(3,2));

         lbl_Time.Text = Convert.ToString(p1b) + " " + Convert.ToString(p1e) + " " + Convert.ToString(p2b) + " " + Convert.ToString(p2e); //Периоды (ОТЛАДКА)

         //Формирование записей для таблицы маршрутизации
         for (int j=1; j<=sts_lst.Length-1; j++)
         {
             string[] tmp2 = sts_lst[j].Split(' '); //Прочитать IP-адрес в tmp2[0]. Остальное в строке = комментарий
             string[] tmp3 = tmp2[0].Trim().Split('.'); //Сформировать массив IP-адресов
             if (tmp3.Length != 4) {continue;}
             string ipr = "";
             string ipm = "";
             for (int i = 0; i < 4; i++) //Формирование конкретной маски для конкретного IP-адреса
             {
                 ipr = ipr + tmp3[i];
                 if (tmp3[i] == "0") ipm = ipm + "0";
                 else ipm = ipm + "255";
                 if (i < 3)
                 {
                     ipm = ipm + ".";
                     ipr = ipr + ".";
                 }
             }
             ipmas[ipkol, 0] = ipr; //Запомнить IP-адрес
             ipmas[ipkol, 1] = ipm; //Запомнить маску
             ipkol++;

             lbl_Info.Text = lbl_Info.Text + "\r\n" + ipr + " " + ipm; //Индикация IP-адресов и масок (ОТЛАДКА)
         }
         //Запуск таймера (интервал в мс)
         timer1.Interval = 5000;  //5*1000мc = 5c
         timer1.Enabled = true;
         timer1.Start(); 
        }

        private void timer1_Tick(object sender, EventArgs e) //Обработка прерываний от таймера
        {
            timer1.Enabled = false; //Запрет прерывания от таймера - блокировка возможности повторного входа
            DateTime dnow = DateTime.Now; //Текущее время
            int tnow = Convert.ToInt32(dnow.Hour)*60 + Convert.ToInt32(dnow.Minute); //Текущее время в минутах от начала суток

            lbl_Time1.Text = Convert.ToString(tnow); //Индикация текущего времени (ОТЛАДКА)

            // Проверить попадает ли текущее время в периоды блокировки
            if ((tnow >= p1b && tnow < p1e) || (tnow >= p2b && tnow < p2e)) //Текущее время входит в периоды блокировки
            {
                if (flg_1) //Если адреса ещё не блокировались - ЗАБЛОКИРОВАТЬ
                {
                    flg_1 = false; //Запретить повторную блокировки
                    flg_2 = true;  //Разрешить последующее освобождение
                    for (int j = 0; j < ipkol; j++) //Блокировка IP-адреса в массиве (изменение таблицы маршрутизации)
                    {
                        Process p = new Process();//Новый процесс
                        p.StartInfo.FileName = "route";//Имя запускаемого файла
                        p.StartInfo.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.System);//Путь где он находится
                        p.StartInfo.Arguments = " add " + ipmas[j, 0] + " mask " + ipmas[j, 1] + " 0.0.0.0"; //Строка аргументов
                        p.StartInfo.CreateNoWindow = true;//Новое окно для нового процесса
                        p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;//Сделать процесс невидимым
                        p.Start();//Запустить процесс
                    }

                    this.BackColor = System.Drawing.Color.Red; //Изменить цвет формы (ОТЛАДКА)
                }
            }
            else //Время свободное от блокировки
            {
                if (flg_2) //Если адреса ещё не разблокировались - РАЗБЛОКИРОВАТЬ
                {
                    flg_2 = false; //Запретить повторное освобождение
                    flg_1 = true; //Разрешить последующую блокировку
                    for (int j = 0; j < ipkol; j++) //Освобождение (удаление из таблицы маршрутизации)  IP-адресов
                    {
                        Process p = new Process();//Новый процесс
                        p.StartInfo.FileName = "route";//Имя запускаемого файла
                        p.StartInfo.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.System);//Путь где он находится
                        p.StartInfo.Arguments = " delete " + ipmas[j, 0]; //Строка аргументов
                        p.StartInfo.CreateNoWindow = true;//Новое окно для нового процесса
                        p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;//Сделать процесс невидимым
                        p.Start();//Запустить процесс
                    }

                    this.BackColor = System.Drawing.Color.Green; //Поменять цвет формы (ОТЛАДКА)

                }
            }
            timer1.Enabled = true; //Разрешение прерываний от таймера
        }
    }
}
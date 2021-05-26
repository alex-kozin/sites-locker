namespace SitesLocker_v._2._0
{
    partial class frm_Main
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Main));
            this.label5 = new System.Windows.Forms.Label();
            this.txtIp = new System.Windows.Forms.TextBox();
            this.btnRemoveBlock = new System.Windows.Forms.Button();
            this.btnOpenSchedule = new System.Windows.Forms.Button();
            this.btnAddSchedule = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbDomain = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBlock = new System.Windows.Forms.Button();
            this.dtpTime3 = new System.Windows.Forms.DateTimePicker();
            this.dtpTime2 = new System.Windows.Forms.DateTimePicker();
            this.dtpTime1 = new System.Windows.Forms.DateTimePicker();
            this.dtpTime0 = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(380, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 18);
            this.label5.TabIndex = 36;
            this.label5.Text = "та з";
            // 
            // txtIp
            // 
            this.txtIp.Location = new System.Drawing.Point(434, 11);
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(158, 24);
            this.txtIp.TabIndex = 2;
            this.txtIp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.txtIp, "Поле для введення IP сайту, що слід блокувати");
            this.txtIp.Enter += new System.EventHandler(this.txtIp_Enter);
            this.txtIp.Leave += new System.EventHandler(this.txtIp_Leave);
            // 
            // btnRemoveBlock
            // 
            this.btnRemoveBlock.Location = new System.Drawing.Point(184, 127);
            this.btnRemoveBlock.Name = "btnRemoveBlock";
            this.btnRemoveBlock.Size = new System.Drawing.Size(149, 34);
            this.btnRemoveBlock.TabIndex = 10;
            this.btnRemoveBlock.Text = "Зняти блокування";
            this.toolTip1.SetToolTip(this.btnRemoveBlock, "Натисніть, щоб зняти блокування.\r\nПримітка: для цієї дії потрібні права адміністр" +
                    "атора.");
            this.btnRemoveBlock.UseVisualStyleBackColor = true;
            this.btnRemoveBlock.Click += new System.EventHandler(this.btnRemoveBlock_Click);
            // 
            // btnOpenSchedule
            // 
            this.btnOpenSchedule.Location = new System.Drawing.Point(235, 42);
            this.btnOpenSchedule.Name = "btnOpenSchedule";
            this.btnOpenSchedule.Size = new System.Drawing.Size(176, 33);
            this.btnOpenSchedule.TabIndex = 4;
            this.btnOpenSchedule.Text = "Відкрити розклад";
            this.toolTip1.SetToolTip(this.btnOpenSchedule, "Натисніть, щоб відкрити список сайтів, що необхідно блокувати.");
            this.btnOpenSchedule.UseVisualStyleBackColor = true;
            this.btnOpenSchedule.Click += new System.EventHandler(this.btnOpenSchedule_Click);
            // 
            // btnAddSchedule
            // 
            this.btnAddSchedule.Location = new System.Drawing.Point(12, 43);
            this.btnAddSchedule.Name = "btnAddSchedule";
            this.btnAddSchedule.Size = new System.Drawing.Size(202, 32);
            this.btnAddSchedule.TabIndex = 3;
            this.btnAddSchedule.Text = "Внести у розклад";
            this.toolTip1.SetToolTip(this.btnAddSchedule, "Натисніть, щоб додати сайт у список сайтів, що слід блокувати.\r\nПримітка: якщо са" +
                    "йт вже є у розкладі - додати його не вдасться.");
            this.btnAddSchedule.UseVisualStyleBackColor = true;
            this.btnAddSchedule.Click += new System.EventHandler(this.btnAddSchedule_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(278, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 18);
            this.label4.TabIndex = 29;
            this.label4.Text = "до";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(191, 18);
            this.label3.TabIndex = 27;
            this.label3.Text = "Введіть час блокування: з";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(415, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 18);
            this.label2.TabIndex = 26;
            this.label2.Text = "-";
            // 
            // cmbDomain
            // 
            this.cmbDomain.FormattingEnabled = true;
            this.cmbDomain.Items.AddRange(new object[] {
            "--Почтовые сервисы--",
            "www.mail.i.ua",
            "www.ukr.net",
            "www.mail.yandex.ua",
            "www.mail.yandex.ru",
            "www.mail.ru",
            "www.gmail.com",
            "www.mail.yahoo.com",
            "--Соц.сети--",
            "www.vk.com",
            "www.vkontakte.ru",
            "www.odnoklassniki.ru",
            "www.ok.ru",
            "www.facebook.com",
            "www.flickr.com",
            "www.instagram.com",
            "www.twitter.com",
            "www.tumblr.com",
            "--Поисковые системы--",
            "www.google.com",
            "www.google.ru",
            "www.yandex.com",
            "www.yandex.ua",
            "www.ya.ru",
            "www.bing.com",
            "www.yahoo.ru",
            "www.yahoo.com",
            "www.rambler.ru"});
            this.cmbDomain.Location = new System.Drawing.Point(184, 11);
            this.cmbDomain.Name = "cmbDomain";
            this.cmbDomain.Size = new System.Drawing.Size(225, 26);
            this.cmbDomain.TabIndex = 1;
            this.toolTip1.SetToolTip(this.cmbDomain, "Поле для введення домену сайту, що слід блокувати");
            this.cmbDomain.Leave += new System.EventHandler(this.cmbDomain_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 18);
            this.label1.TabIndex = 24;
            this.label1.Text = "Введіть домен або IP:";
            // 
            // btnBlock
            // 
            this.btnBlock.Location = new System.Drawing.Point(12, 127);
            this.btnBlock.Name = "btnBlock";
            this.btnBlock.Size = new System.Drawing.Size(149, 34);
            this.btnBlock.TabIndex = 9;
            this.btnBlock.Text = "Заблокувати";
            this.toolTip1.SetToolTip(this.btnBlock, resources.GetString("btnBlock.ToolTip"));
            this.btnBlock.UseVisualStyleBackColor = true;
            this.btnBlock.Click += new System.EventHandler(this.btnBlock_Click);
            // 
            // dtpTime3
            // 
            this.dtpTime3.CustomFormat = "HH:mm";
            this.dtpTime3.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTime3.Location = new System.Drawing.Point(529, 88);
            this.dtpTime3.Name = "dtpTime3";
            this.dtpTime3.ShowUpDown = true;
            this.dtpTime3.Size = new System.Drawing.Size(63, 24);
            this.dtpTime3.TabIndex = 8;
            this.dtpTime3.Value = new System.DateTime(2015, 10, 31, 6, 0, 0, 0);
            this.dtpTime3.ValueChanged += new System.EventHandler(this.dtpTime_ValueChanged);
            // 
            // dtpTime2
            // 
            this.dtpTime2.CustomFormat = "HH:mm";
            this.dtpTime2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTime2.Location = new System.Drawing.Point(424, 89);
            this.dtpTime2.Name = "dtpTime2";
            this.dtpTime2.ShowUpDown = true;
            this.dtpTime2.Size = new System.Drawing.Size(62, 24);
            this.dtpTime2.TabIndex = 7;
            this.dtpTime2.Value = new System.DateTime(2015, 10, 29, 0, 0, 0, 0);
            this.dtpTime2.ValueChanged += new System.EventHandler(this.dtpTime_ValueChanged);
            // 
            // dtpTime1
            // 
            this.dtpTime1.CustomFormat = "HH:mm";
            this.dtpTime1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTime1.Location = new System.Drawing.Point(311, 90);
            this.dtpTime1.Name = "dtpTime1";
            this.dtpTime1.ShowUpDown = true;
            this.dtpTime1.Size = new System.Drawing.Size(61, 24);
            this.dtpTime1.TabIndex = 6;
            this.dtpTime1.Value = new System.DateTime(2015, 10, 31, 23, 59, 0, 0);
            this.dtpTime1.ValueChanged += new System.EventHandler(this.dtpTime_ValueChanged);
            // 
            // dtpTime0
            // 
            this.dtpTime0.CustomFormat = "HH:mm";
            this.dtpTime0.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTime0.Location = new System.Drawing.Point(209, 91);
            this.dtpTime0.Name = "dtpTime0";
            this.dtpTime0.ShowUpDown = true;
            this.dtpTime0.Size = new System.Drawing.Size(63, 24);
            this.dtpTime0.TabIndex = 5;
            this.dtpTime0.Value = new System.DateTime(2015, 10, 31, 22, 0, 0, 0);
            this.dtpTime0.ValueChanged += new System.EventHandler(this.dtpTime_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(497, 91);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 18);
            this.label6.TabIndex = 39;
            this.label6.Text = "до";
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 200;
            this.toolTip1.ReshowDelay = 100;
            this.toolTip1.ToolTipTitle = "Довідка";
            // 
            // frm_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(604, 173);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dtpTime3);
            this.Controls.Add(this.dtpTime2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtIp);
            this.Controls.Add(this.btnRemoveBlock);
            this.Controls.Add(this.btnOpenSchedule);
            this.Controls.Add(this.btnAddSchedule);
            this.Controls.Add(this.dtpTime1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtpTime0);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbDomain);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnBlock);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "frm_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SitesLocker_v.2.1 Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_Main_FormClosing);
            this.Load += new System.EventHandler(this.frm_Main_Load);
            this.TextChanged += new System.EventHandler(this.frm_Main_TextChanged);
            this.Click += new System.EventHandler(this.frm_Main_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.Button btnRemoveBlock;
        private System.Windows.Forms.Button btnOpenSchedule;
        private System.Windows.Forms.Button btnAddSchedule;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbDomain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBlock;
        private System.Windows.Forms.DateTimePicker dtpTime3;
        private System.Windows.Forms.DateTimePicker dtpTime2;
        private System.Windows.Forms.DateTimePicker dtpTime1;
        private System.Windows.Forms.DateTimePicker dtpTime0;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}


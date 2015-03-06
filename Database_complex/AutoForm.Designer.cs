namespace Database_complex
{
    partial class AutoForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btClose = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.rbMySQL = new System.Windows.Forms.RadioButton();
            this.rbSqlServer = new System.Windows.Forms.RadioButton();
            this.groupBoxServer = new System.Windows.Forms.GroupBox();
            this.groupBoxName = new System.Windows.Forms.GroupBox();
            this.radioButtonConnString = new System.Windows.Forms.RadioButton();
            this.radioButtonPoles = new System.Windows.Forms.RadioButton();
            this.tbServer = new System.Windows.Forms.TextBox();
            this.tbDatabase = new System.Windows.Forms.TextBox();
            this.tbUser = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBoxServer.SuspendLayout();
            this.groupBoxName.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBoxName);
            this.groupBox1.Controls.Add(this.groupBoxServer);
            this.groupBox1.Controls.Add(this.btClose);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(372, 319);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btClose
            // 
            this.btClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btClose.Location = new System.Drawing.Point(246, 280);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(75, 23);
            this.btClose.TabIndex = 5;
            this.btClose.Text = "Выход";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(45, 280);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "ОК";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 150);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(348, 46);
            this.textBox1.TabIndex = 2;
            // 
            // rbMySQL
            // 
            this.rbMySQL.AutoSize = true;
            this.rbMySQL.Location = new System.Drawing.Point(258, 19);
            this.rbMySQL.Name = "rbMySQL";
            this.rbMySQL.Size = new System.Drawing.Size(60, 17);
            this.rbMySQL.TabIndex = 1;
            this.rbMySQL.Text = "MySQL";
            this.rbMySQL.UseVisualStyleBackColor = true;
            // 
            // rbSqlServer
            // 
            this.rbSqlServer.AutoSize = true;
            this.rbSqlServer.Checked = true;
            this.rbSqlServer.Location = new System.Drawing.Point(42, 19);
            this.rbSqlServer.Name = "rbSqlServer";
            this.rbSqlServer.Size = new System.Drawing.Size(99, 17);
            this.rbSqlServer.TabIndex = 0;
            this.rbSqlServer.TabStop = true;
            this.rbSqlServer.Text = "MS SQL Server";
            this.rbSqlServer.UseVisualStyleBackColor = true;
            // 
            // groupBoxServer
            // 
            this.groupBoxServer.Controls.Add(this.rbMySQL);
            this.groupBoxServer.Controls.Add(this.rbSqlServer);
            this.groupBoxServer.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxServer.Location = new System.Drawing.Point(3, 16);
            this.groupBoxServer.Name = "groupBoxServer";
            this.groupBoxServer.Size = new System.Drawing.Size(366, 48);
            this.groupBoxServer.TabIndex = 6;
            this.groupBoxServer.TabStop = false;
            this.groupBoxServer.Text = "Выберите поставщика СУБД";
            // 
            // groupBoxName
            // 
            this.groupBoxName.Controls.Add(this.label4);
            this.groupBoxName.Controls.Add(this.label3);
            this.groupBoxName.Controls.Add(this.label2);
            this.groupBoxName.Controls.Add(this.label1);
            this.groupBoxName.Controls.Add(this.tbPassword);
            this.groupBoxName.Controls.Add(this.tbUser);
            this.groupBoxName.Controls.Add(this.tbDatabase);
            this.groupBoxName.Controls.Add(this.tbServer);
            this.groupBoxName.Controls.Add(this.radioButtonPoles);
            this.groupBoxName.Controls.Add(this.radioButtonConnString);
            this.groupBoxName.Controls.Add(this.textBox1);
            this.groupBoxName.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxName.Location = new System.Drawing.Point(3, 64);
            this.groupBoxName.Name = "groupBoxName";
            this.groupBoxName.Size = new System.Drawing.Size(366, 210);
            this.groupBoxName.TabIndex = 7;
            this.groupBoxName.TabStop = false;
            this.groupBoxName.Text = "Авторизация";
            // 
            // radioButtonConnString
            // 
            this.radioButtonConnString.AutoSize = true;
            this.radioButtonConnString.Location = new System.Drawing.Point(10, 127);
            this.radioButtonConnString.Name = "radioButtonConnString";
            this.radioButtonConnString.Size = new System.Drawing.Size(134, 17);
            this.radioButtonConnString.TabIndex = 4;
            this.radioButtonConnString.Text = "Строка подключения:";
            this.radioButtonConnString.UseVisualStyleBackColor = true;
            // 
            // radioButtonPoles
            // 
            this.radioButtonPoles.AutoSize = true;
            this.radioButtonPoles.Checked = true;
            this.radioButtonPoles.Location = new System.Drawing.Point(10, 20);
            this.radioButtonPoles.Name = "radioButtonPoles";
            this.radioButtonPoles.Size = new System.Drawing.Size(14, 13);
            this.radioButtonPoles.TabIndex = 5;
            this.radioButtonPoles.TabStop = true;
            this.radioButtonPoles.UseVisualStyleBackColor = true;
            // 
            // tbServer
            // 
            this.tbServer.Location = new System.Drawing.Point(128, 23);
            this.tbServer.Name = "tbServer";
            this.tbServer.Size = new System.Drawing.Size(145, 20);
            this.tbServer.TabIndex = 6;
            // 
            // tbDatabase
            // 
            this.tbDatabase.Location = new System.Drawing.Point(128, 49);
            this.tbDatabase.Name = "tbDatabase";
            this.tbDatabase.Size = new System.Drawing.Size(145, 20);
            this.tbDatabase.TabIndex = 7;
            // 
            // tbUser
            // 
            this.tbUser.Location = new System.Drawing.Point(128, 75);
            this.tbUser.Name = "tbUser";
            this.tbUser.Size = new System.Drawing.Size(145, 20);
            this.tbUser.TabIndex = 8;
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(128, 101);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(145, 20);
            this.tbPassword.TabIndex = 9;
            this.tbPassword.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Адрес сервера:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Название БД:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Имя пользователя:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(77, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Пароль:";
            // 
            // AutoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 319);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "AutoForm";
            this.Text = "Подключение к базе данных";
            this.groupBox1.ResumeLayout(false);
            this.groupBoxServer.ResumeLayout(false);
            this.groupBoxServer.PerformLayout();
            this.groupBoxName.ResumeLayout(false);
            this.groupBoxName.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.RadioButton rbMySQL;
        private System.Windows.Forms.RadioButton rbSqlServer;
        private System.Windows.Forms.GroupBox groupBoxServer;
        private System.Windows.Forms.GroupBox groupBoxName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.TextBox tbUser;
        private System.Windows.Forms.TextBox tbDatabase;
        private System.Windows.Forms.TextBox tbServer;
        private System.Windows.Forms.RadioButton radioButtonPoles;
        private System.Windows.Forms.RadioButton radioButtonConnString;
    }
}
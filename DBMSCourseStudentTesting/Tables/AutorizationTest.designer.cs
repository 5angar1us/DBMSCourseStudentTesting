namespace DBMSCourseStudentTesting.Tables
{
    partial class AutorizationTest
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Entrance = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TextBoxPassword = new System.Windows.Forms.TextBox();
            this.TextBoxLogin = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbAnother = new System.Windows.Forms.RadioButton();
            this.rbStudent = new System.Windows.Forms.RadioButton();
            this.rbTeacher = new System.Windows.Forms.RadioButton();
            this.rbAdmin = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Entrance
            // 
            this.Entrance.Location = new System.Drawing.Point(132, 102);
            this.Entrance.Name = "Entrance";
            this.Entrance.Size = new System.Drawing.Size(75, 23);
            this.Entrance.TabIndex = 13;
            this.Entrance.Text = "Вход";
            this.Entrance.UseVisualStyleBackColor = true;
            this.Entrance.Click += new System.EventHandler(this.Entrance_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Пароль";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Логин";
            // 
            // TextBoxPassword
            // 
            this.TextBoxPassword.Location = new System.Drawing.Point(119, 50);
            this.TextBoxPassword.Name = "TextBoxPassword";
            this.TextBoxPassword.PasswordChar = '*';
            this.TextBoxPassword.Size = new System.Drawing.Size(100, 20);
            this.TextBoxPassword.TabIndex = 10;
            // 
            // TextBoxLogin
            // 
            this.TextBoxLogin.Location = new System.Drawing.Point(119, 12);
            this.TextBoxLogin.Name = "TextBoxLogin";
            this.TextBoxLogin.Size = new System.Drawing.Size(100, 20);
            this.TextBoxLogin.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbAnother);
            this.groupBox1.Controls.Add(this.rbStudent);
            this.groupBox1.Controls.Add(this.rbTeacher);
            this.groupBox1.Controls.Add(this.rbAdmin);
            this.groupBox1.Location = new System.Drawing.Point(244, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 113);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Выбор логина";
            // 
            // rbAnother
            // 
            this.rbAnother.AutoSize = true;
            this.rbAnother.Location = new System.Drawing.Point(6, 90);
            this.rbAnother.Name = "rbAnother";
            this.rbAnother.Size = new System.Drawing.Size(104, 17);
            this.rbAnother.TabIndex = 3;
            this.rbAnother.TabStop = true;
            this.rbAnother.Text = "Логин с формы";
            this.rbAnother.UseVisualStyleBackColor = true;
            // 
            // rbStudent
            // 
            this.rbStudent.AutoSize = true;
            this.rbStudent.Location = new System.Drawing.Point(6, 64);
            this.rbStudent.Name = "rbStudent";
            this.rbStudent.Size = new System.Drawing.Size(65, 17);
            this.rbStudent.TabIndex = 2;
            this.rbStudent.TabStop = true;
            this.rbStudent.Text = "Студент";
            this.rbStudent.UseVisualStyleBackColor = true;
            // 
            // rbTeacher
            // 
            this.rbTeacher.AutoSize = true;
            this.rbTeacher.Location = new System.Drawing.Point(6, 41);
            this.rbTeacher.Name = "rbTeacher";
            this.rbTeacher.Size = new System.Drawing.Size(67, 17);
            this.rbTeacher.TabIndex = 1;
            this.rbTeacher.TabStop = true;
            this.rbTeacher.Text = "Учитель";
            this.rbTeacher.UseVisualStyleBackColor = true;
            // 
            // rbAdmin
            // 
            this.rbAdmin.AutoSize = true;
            this.rbAdmin.Checked = true;
            this.rbAdmin.Location = new System.Drawing.Point(6, 19);
            this.rbAdmin.Name = "rbAdmin";
            this.rbAdmin.Size = new System.Drawing.Size(54, 17);
            this.rbAdmin.TabIndex = 0;
            this.rbAdmin.TabStop = true;
            this.rbAdmin.Text = "Admin";
            this.rbAdmin.UseVisualStyleBackColor = true;
            // 
            // AutorizationTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 133);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Entrance);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TextBoxPassword);
            this.Controls.Add(this.TextBoxLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AutorizationTest";
            this.Text = "Авторизация";
            this.Load += new System.EventHandler(this.AutorizationTest_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Entrance;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TextBoxPassword;
        private System.Windows.Forms.TextBox TextBoxLogin;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbAnother;
        private System.Windows.Forms.RadioButton rbStudent;
        private System.Windows.Forms.RadioButton rbTeacher;
        private System.Windows.Forms.RadioButton rbAdmin;
    }
}
namespace DBMSCourseStudentTesting.Controls
{
    partial class EditAnsverContol
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
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

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.CB = new System.Windows.Forms.CheckBox();
            this.TBAnswer = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(439, 30);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(124, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Удалить вопрос";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CB
            // 
            this.CB.AutoSize = true;
            this.CB.Location = new System.Drawing.Point(3, 7);
            this.CB.Name = "CB";
            this.CB.Size = new System.Drawing.Size(15, 14);
            this.CB.TabIndex = 4;
            this.CB.UseVisualStyleBackColor = true;
            this.CB.CheckedChanged += new System.EventHandler(this.CB_CheckedChanged);
            // 
            // TBAnswer
            // 
            this.TBAnswer.Location = new System.Drawing.Point(24, 4);
            this.TBAnswer.MaxLength = 200;
            this.TBAnswer.Multiline = true;
            this.TBAnswer.Name = "TBAnswer";
            this.TBAnswer.Size = new System.Drawing.Size(539, 20);
            this.TBAnswer.TabIndex = 5;
            this.TBAnswer.TextChanged += new System.EventHandler(this.TBAnswer_TextChanged);
            // 
            // EditAnsverContol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TBAnswer);
            this.Controls.Add(this.CB);
            this.Controls.Add(this.button1);
            this.Name = "EditAnsverContol";
            this.Size = new System.Drawing.Size(570, 61);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox CB;
        private System.Windows.Forms.TextBox TBAnswer;
    }
}

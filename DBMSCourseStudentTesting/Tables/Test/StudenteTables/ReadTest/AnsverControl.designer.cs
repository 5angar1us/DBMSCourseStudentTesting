namespace DBMSCourseStudentTesting
{
    partial class AnsverControl
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
            this.TBAnswer = new System.Windows.Forms.TextBox();
            this.CB = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // TBAnswer
            // 
            this.TBAnswer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TBAnswer.Location = new System.Drawing.Point(23, 3);
            this.TBAnswer.Multiline = true;
            this.TBAnswer.Name = "TBAnswer";
            this.TBAnswer.ReadOnly = true;
            this.TBAnswer.Size = new System.Drawing.Size(546, 20);
            this.TBAnswer.TabIndex = 1;
            this.TBAnswer.Text = "жнгнжлфжгнклфджклгнльнждлкфнйльфгнжклфджхфйхиллхыхдйурт;офднигфджлкгжфдклжглкдф1й" +
    "г1д1ла";
            this.TBAnswer.TextChanged += new System.EventHandler(this.TBAnswer_TextChanged);
            // 
            // CB
            // 
            this.CB.AutoSize = true;
            this.CB.Location = new System.Drawing.Point(4, 4);
            this.CB.Name = "CB";
            this.CB.Size = new System.Drawing.Size(15, 14);
            this.CB.TabIndex = 2;
            this.CB.UseVisualStyleBackColor = true;
            // 
            // AnsverControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CB);
            this.Controls.Add(this.TBAnswer);
            this.MinimumSize = new System.Drawing.Size(101, 24);
            this.Name = "AnsverControl";
            this.Size = new System.Drawing.Size(570, 24);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        protected System.Windows.Forms.TextBox TBAnswer;
        private System.Windows.Forms.CheckBox CB;
    }
}

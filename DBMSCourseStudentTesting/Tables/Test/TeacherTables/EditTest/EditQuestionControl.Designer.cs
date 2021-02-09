namespace DBMSCourseStudentTesting.Controls
{
    partial class EditQuestionControl
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
            this.QuestionTitle = new System.Windows.Forms.TextBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonDel = new System.Windows.Forms.Button();
            this.questionMark = new System.Windows.Forms.NumericUpDown();
            this.panelQuestionMark = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.questionMark)).BeginInit();
            this.panelQuestionMark.SuspendLayout();
            this.SuspendLayout();
            // 
            // QuestionTitle
            // 
            this.QuestionTitle.Location = new System.Drawing.Point(3, 0);
            this.QuestionTitle.MaxLength = 200;
            this.QuestionTitle.Multiline = true;
            this.QuestionTitle.Name = "QuestionTitle";
            this.QuestionTitle.Size = new System.Drawing.Size(564, 42);
            this.QuestionTitle.TabIndex = 7;
            this.QuestionTitle.Text = "hj[";
            this.QuestionTitle.UseSystemPasswordChar = true;
            this.QuestionTitle.TextChanged += new System.EventHandler(this.Question_TextChanged);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(17, 219);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(133, 23);
            this.buttonAdd.TabIndex = 8;
            this.buttonAdd.Text = "Добавить ответ";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonDel
            // 
            this.buttonDel.Location = new System.Drawing.Point(156, 219);
            this.buttonDel.Name = "buttonDel";
            this.buttonDel.Size = new System.Drawing.Size(113, 23);
            this.buttonDel.TabIndex = 9;
            this.buttonDel.Text = "Удалить вопрос";
            this.buttonDel.UseVisualStyleBackColor = true;
            this.buttonDel.Click += new System.EventHandler(this.buttonDel_Click);
            // 
            // questionMark
            // 
            this.questionMark.Location = new System.Drawing.Point(99, 3);
            this.questionMark.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.questionMark.Name = "questionMark";
            this.questionMark.Size = new System.Drawing.Size(51, 20);
            this.questionMark.TabIndex = 10;
            this.questionMark.ValueChanged += new System.EventHandler(this.questionMark_ValueChanged);
            // 
            // panelQuestionMark
            // 
            this.panelQuestionMark.Controls.Add(this.label1);
            this.panelQuestionMark.Controls.Add(this.questionMark);
            this.panelQuestionMark.Location = new System.Drawing.Point(275, 219);
            this.panelQuestionMark.Name = "panelQuestionMark";
            this.panelQuestionMark.Size = new System.Drawing.Size(152, 23);
            this.panelQuestionMark.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "баллы за вопрос";
            // 
            // EditQuestionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.panelQuestionMark);
            this.Controls.Add(this.buttonDel);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.QuestionTitle);
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.Name = "EditQuestionControl";
            this.Size = new System.Drawing.Size(606, 250);
            this.Load += new System.EventHandler(this.EditQuestionControl_Load);
            this.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.EditQuestionControl_ControlRemoved);
            ((System.ComponentModel.ISupportInitialize)(this.questionMark)).EndInit();
            this.panelQuestionMark.ResumeLayout(false);
            this.panelQuestionMark.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox QuestionTitle;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonDel;
        private System.Windows.Forms.NumericUpDown questionMark;
        private System.Windows.Forms.Panel panelQuestionMark;
        private System.Windows.Forms.Label label1;
    }
}

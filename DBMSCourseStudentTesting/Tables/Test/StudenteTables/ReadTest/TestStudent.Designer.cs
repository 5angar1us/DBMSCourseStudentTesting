namespace DBMSCourseStudentTesting
{
    partial class TestStudent
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
            this.buttonСomplete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonСomplete
            // 
            this.buttonСomplete.Location = new System.Drawing.Point(492, 403);
            this.buttonСomplete.Name = "buttonСomplete";
            this.buttonСomplete.Size = new System.Drawing.Size(75, 23);
            this.buttonСomplete.TabIndex = 0;
            this.buttonСomplete.Text = "Завершить";
            this.buttonСomplete.UseVisualStyleBackColor = true;
            this.buttonСomplete.Click += new System.EventHandler(this.buttonСomplete_Click);
            // 
            // TestStudent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(608, 450);
            this.Controls.Add(this.buttonСomplete);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "TestStudent";
            this.Text = "Test";
            this.Load += new System.EventHandler(this.TestStudent_Load);
            this.ResumeLayout(false);

        }


        #endregion

        private System.Windows.Forms.Button buttonСomplete;
    }
}
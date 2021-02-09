namespace DBMSCourseStudentTesting
{
    partial class TeacherStatisticks
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
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.печатьОтсчётаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.изменитьТестToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.изменитьДоступКТестуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SFD = new System.Windows.Forms.SaveFileDialog();
            this.OFD = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 24);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(800, 426);
            this.dataGridView1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.печатьОтсчётаToolStripMenuItem,
            this.изменитьТестToolStripMenuItem,
            this.изменитьДоступКТестуToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // печатьОтсчётаToolStripMenuItem
            // 
            this.печатьОтсчётаToolStripMenuItem.Name = "печатьОтсчётаToolStripMenuItem";
            this.печатьОтсчётаToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
            this.печатьОтсчётаToolStripMenuItem.Text = "Вывод в excel";
            this.печатьОтсчётаToolStripMenuItem.Click += new System.EventHandler(this.печатьОтсчётаToolStripMenuItem_Click);
            // 
            // изменитьТестToolStripMenuItem
            // 
            this.изменитьТестToolStripMenuItem.Name = "изменитьТестToolStripMenuItem";
            this.изменитьТестToolStripMenuItem.Size = new System.Drawing.Size(98, 20);
            this.изменитьТестToolStripMenuItem.Text = "Изменить тест";
            this.изменитьТестToolStripMenuItem.Click += new System.EventHandler(this.изменитьТестToolStripMenuItem_Click);
            // 
            // изменитьДоступКТестуToolStripMenuItem
            // 
            this.изменитьДоступКТестуToolStripMenuItem.Name = "изменитьДоступКТестуToolStripMenuItem";
            this.изменитьДоступКТестуToolStripMenuItem.Size = new System.Drawing.Size(153, 20);
            this.изменитьДоступКТестуToolStripMenuItem.Text = "Изменить доступ к тесту";
            this.изменитьДоступКТестуToolStripMenuItem.Click += new System.EventHandler(this.изменитьДоступКТестуToolStripMenuItem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // TeacherStatisticks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "TeacherStatisticks";
            this.Text = "Статистика по тесту";
            this.Load += new System.EventHandler(this.Statisticks_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem печатьОтсчётаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem изменитьТестToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem изменитьДоступКТестуToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.SaveFileDialog SFD;
        private System.Windows.Forms.OpenFileDialog OFD;
    }
}
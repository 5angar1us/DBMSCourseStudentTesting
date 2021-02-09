using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Common;
using DBMSCourseStudentTesting.Entityies;
using System.Data.Entity;

namespace DBMSCourseStudentTesting.Tables
{
    public partial class Protocol : Form
    {
        public Protocol()
        {
            InitializeComponent();
            this.MaximizeBox = false;
        }

        private void Protocol_Load(object sender, EventArgs e)
        {
            using (StudentTestingEntities1 db = new StudentTestingEntities1())
            {
                db.Protocol.Load();
                dataGridView1.DataSource = db.Protocol.Local.ToBindingList();
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dataGridView1.Columns["currentDateTime"].HeaderText = "Дата и время";
                dataGridView1.Columns["currentIP"].HeaderText = "IP";
                dataGridView1.Columns["UserName"].HeaderText = "Логин";
                dataGridView1.Columns["OperationType"].HeaderText = "Тип операции";
                dataGridView1.Columns["ObjectName"].HeaderText = "Объект";
                dataGridView1.Columns["AttributeName"].HeaderText = "Атрибут";
                dataGridView1.Columns["NewValue"].HeaderText = "Новое значение";
                dataGridView1.Columns["OldValue"].HeaderText = "Старое значение";
            }
        }
    }
}

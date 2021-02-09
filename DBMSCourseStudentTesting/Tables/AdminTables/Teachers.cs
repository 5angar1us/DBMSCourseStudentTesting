using DBMSCourseStudentTesting.Entityies;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBMSCourseStudentTesting.Tables
{
    public partial class Teachers : Form
    {
        public Teachers()
        {
            InitializeComponent();
        }

        string buttonColumnName = "teacher's subjects";

        private void btnEnter_Click(object sender, EventArgs e)
        {
            bool allDataDontInserted = tbSurname.Text == null
                & tbName.Text == null
                & tbPatronymic.Text == null
                & tbLogin.Text == null
                & tbPassword.Text == null;


            if (allDataDontInserted) return;

            DialogResult result = MessageBox.Show("Вы уверены, что хотите сохранить изменения?",
                    "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            using (StudentTestingEntities1 db = new StudentTestingEntities1())
            {
                using (SqlConnection connection = new SqlConnection(db.Database.Connection.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(
                        "sp_addlogin @Login, @Password, @DBNAME", connection))
                    {
                        command.Parameters.Add(new SqlParameter("@Login", tbLogin.Text));
                        command.Parameters.Add(new SqlParameter("@Password", tbPassword.Text));
                        command.Parameters.Add(new SqlParameter("@DBNAME", Properties.Settings.Default.DataBase));

                        SqlDataReader reader = command.ExecuteReader();
                    }

                    using (SqlCommand command = new SqlCommand(
                        "sp_adduser @Login, @Login, @Role", connection))
                    {
                        command.Parameters.Add(new SqlParameter("@Login", tbLogin.Text));
                        command.Parameters.Add(new SqlParameter("@Role", "teacher"));

                        SqlDataReader reader = command.ExecuteReader();
                    }
                }

                db.Teacher.Add(new Teacher()
                {
                    Surname = tbSurname.Text,
                    Name = tbName.Text,
                    Patronymic = tbPatronymic.Text,
                    Login = tbLogin.Text,
                });
                db.SaveChanges();

            }

            LoadData();

        }

        private void Teachers_Load(object sender, EventArgs e)
        {
            LoadData();
            AddButtonColumns();
        }

        private void LoadData()
        {
            using (StudentTestingEntities1 db = new StudentTestingEntities1())
            {
                db.Teacher.Load();
                dataGridView1.DataSource = db.Teacher.Local.ToBindingList();
                dataGridView1.Columns["id"].Visible = false;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dataGridView1.Columns["Surname"].HeaderText = "Фамилия";
                dataGridView1.Columns["Name"].HeaderText = "Имя";
                dataGridView1.Columns["Patronymic"].HeaderText = "Отчество";
                dataGridView1.Columns["Login"].HeaderText = "Логин";
            }
        }

        private void AddButtonColumns()
        {
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.HeaderText = "";
            buttonColumn.Name = buttonColumnName;
            buttonColumn.Text = "Предметы";
            buttonColumn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(buttonColumn);
            dataGridView1.CellClick += new DataGridViewCellEventHandler(dataGridView1_CellClick);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ignore clicks that are not on button cells. 
            if (e.RowIndex < 0 || e.ColumnIndex !=
                dataGridView1.Columns[buttonColumnName].Index) return;

            // Retrieve the task ID.
            Int32 id = (Int32)dataGridView1["id", e.RowIndex].Value;
            String name = $"{dataGridView1["Surname", e.RowIndex].Value.ToString().Trim()}" +
                $" {dataGridView1["Name", e.RowIndex].Value.ToString().Trim()}" +
                $" {dataGridView1["Patronymic", e.RowIndex].Value.ToString().Trim()}";

            Teacher_AcSubject teacher_AcSubject = new Teacher_AcSubject(id,name);
            teacher_AcSubject.Show();

        }
    }
}

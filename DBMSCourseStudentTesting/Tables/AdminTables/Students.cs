using DBMSCourseStudentTesting.Entityies;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
using System.Data.SqlClient;

namespace DBMSCourseStudentTesting.Tables
{
    public partial class Students : Form
    {
        public Students()
        {
            InitializeComponent();
        }

        private List<AcademicGroup> academicGroups;
        private void Students_Load(object sender, EventArgs e)
        {
            LoadData();
            using (StudentTestingEntities1 db = new StudentTestingEntities1())
            {
                db.AcademicGroup.Load();
                academicGroups = db.AcademicGroup.Local.ToBindingList().ToList();
                academicGroups.ForEach(o => comboBox1.Items.Add(o.Name.Trim()));
            }
        }
        private void LoadData()
        {
            using (StudentTestingEntities1 db = new StudentTestingEntities1())
            {

                var x = from s in db.Student
                        select new
                        {
                            s.id,
                            s.Surname,
                            s.Name,
                            s.Patronymic,
                            academicGroupName = s.AcademicGroup.Name,
                            s.Login
                        };
                dataGridView1.DataSource = x.ToList();
                dataGridView1.Columns["id"].Visible = false;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dataGridView1.Columns["Surname"].HeaderText = "Фамилия";
                dataGridView1.Columns["Name"].HeaderText = "Имя";
                dataGridView1.Columns["Patronymic"].HeaderText = "Отчество";
                dataGridView1.Columns["academicGroupName"].HeaderText = "Группа";
                dataGridView1.Columns["Login"].HeaderText = "Логин";
            }
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            string selectedText = comboBox1.SelectedItem.ToString().Trim();
            AcademicGroup academicGroup = academicGroups.Find(item => item.Name.Trim().Equals(selectedText));

            bool allDataDontInserted = tbSurname.Text == null
                & tbName.Text == null
                & tbPatronymic.Text == null
                & tbLogin.Text == null
                & tbPassword.Text == null
                & academicGroup == null;

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
                        command.Parameters.Add(new SqlParameter("@Role", "student"));

                        SqlDataReader reader = command.ExecuteReader();
                    }
                }
                    
                db.Student.Add(new Student()
                {
                    Surname = tbSurname.Text,
                    Name = tbName.Text,
                    Patronymic = tbPatronymic.Text,
                    Login = tbLogin.Text,
                    idAcademicGroup = academicGroup.id

                });
                db.SaveChanges();
               
            }
            LoadData();
        }
    }
}

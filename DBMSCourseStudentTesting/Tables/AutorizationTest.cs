using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;
using System.Text;
using DBMSCourseStudentTesting.Entityies;
using System.Data.Common;

namespace DBMSCourseStudentTesting.Tables
{
    public partial class AutorizationTest : Form
    {
        public AutorizationTest()
        {
            InitializeComponent();
        }

        private void AutorizationTest_Load(object sender, EventArgs e)
        {

        }

        private void Entrance_Click(object sender, EventArgs e)
        {
            Properties.Settings settings = Properties.Settings.Default;

            string connectionString = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
            builder.DataSource = settings.DataSource;

            if (rbAdmin.Checked)
            {
                builder.UserID = "test1234";
                builder.Password = "1234";
            }
            if (rbTeacher.Checked)
            {
                builder.UserID = "TSTeacher1";
                builder.Password = "TSTeacher1";
            }
            if (rbStudent.Checked)
            {
                builder.UserID = "TSStudent1";
                builder.Password = "TSStudent1";
            }
            if (rbAnother.Checked)
            {
                builder.UserID = TextBoxLogin.Text;
                builder.Password = TextBoxPassword.Text;
            }
            StringBuilder builderEntities = new StringBuilder(settings.StudentTestingEntities1);
            builderEntities.Replace("@login", builder.UserID);
            builderEntities.Replace("@password", builder.Password);
            builderEntities.Replace("@dataSource", settings.DataSource);
            builderEntities.Replace("@DataBase", settings.DataBase);
            settings.connectStudentTestingEntities = builderEntities.ToString();
            settings.Save();
            using (StudentTestingEntities1 db = new StudentTestingEntities1())
            {
                DbConnection connection = db.Database.Connection;
                Exception error = null;
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    error = ex;//Переменная error запоминает конкретную ошибку
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (error == null)
                    {
                        connection.Close();
                        //MessageBox.Show("Соединение установлено!", "Выполнено");
                        Main NewForm = new Main();
                        NewForm.Show();
                        this.Hide();
                    }
                }
            }
        }
    }
}


/*
Таски
2. отсчёты с визуализацией
3. Сделать что-нибудь с размерами форм.
4. Продумать добавление предметных областей и предметов
5. Продумать доступ к таблицам

1.проверить изменение,удаление,добавление записей в БД  
2.проверить изменение названия столбцов


    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    Console.WriteLine($"{i}:{dataGridView1.Columns[i].Name}->{dataGridView1.Columns[i].HeaderText}");
                }
*/


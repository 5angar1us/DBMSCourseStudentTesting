using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;
using System.Text;
using DBMSCourseStudentTesting.Entityies;
using System.Data.Common;

namespace DBMSCourseStudentTesting.Tables
{
    public partial class Autorization : Form
    {
        public Autorization()
        {
            InitializeComponent();
        }

        private void Entrance_Click(object sender, EventArgs e)
        {
            Properties.Settings settings = Properties.Settings.Default;

            string connectionString = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
            builder.DataSource = settings.DataSource;

            builder.UserID = TextBoxLogin.Text;
            builder.Password = TextBoxPassword.Text;

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
                        MessageBox.Show("Соединение установлено!", "Выполнено");
                        Main NewForm = new Main();
                        NewForm.Show();
                        this.Hide();
                    }
                }
            }
        }
    }
}



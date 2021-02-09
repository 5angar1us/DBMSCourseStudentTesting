using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Common;
using DBMSCourseStudentTesting.Entityies;
using System.Linq;

namespace DBMSCourseStudentTesting.Tables
{
    public partial class Administration : Form
    {

        public Administration()
        {
            InitializeComponent();
        }

        private void протоколированиеToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Protocol logg = new Protocol();
            logg.Show();
        }

        private void Administration_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        #region Init
        private void LoadData()
        {
            using (StudentTestingEntities1 db = new StudentTestingEntities1())
            {
                var users = (
                            from s in db.Student
                            select new
                            {
                                s.Surname,
                                s.Name,
                                s.Patronymic,
                                s.Login,

                            }
                        ).Concat
                        (
                            from t in db.Teacher
                            select new
                            {
                                Surname = t.Surname,
                                Name = t.Name,
                                Patronymic = t.Patronymic,
                                Login = t.Login
                            }
                        );

                var userToList = new
                {
                    Surname = users.First().Surname,
                    Name = users.First().Name,
                    Patronymic = users.First().Patronymic,
                    Login = users.First().Login,
                    role = db.GetRole(users.First().Login).FirstOrDefault()
                };
                var UserList = new[] { userToList }.ToList();
                UserList.Clear();

                users.ToList().ForEach(o =>
                {
                    string currentRole = db.GetRole(o.Login).FirstOrDefault();
                    UserList.Add(new
                    {
                        Surname = o.Surname,
                        Name = o.Name,
                        Patronymic = o.Patronymic,
                        Login = o.Login,
                        role = currentRole
                    });

                });
                dataGridView1.DataSource = UserList;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dataGridView1.Columns["Surname"].HeaderText = "Фамилия";
                dataGridView1.Columns["Name"].HeaderText = "Имя";
                dataGridView1.Columns["Patronymic"].HeaderText = "Отчество";
                dataGridView1.Columns["Login"].HeaderText = "Логин";
                dataGridView1.Columns["role"].HeaderText = "Роль";
            }
        }
        #endregion

        #region ToolStripMenuItems
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Teachers teachers = new Teachers();
            teachers.Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Students students = new Students();
            students.Show();
        }
        #endregion
    }
}



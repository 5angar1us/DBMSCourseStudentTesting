using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using DBMSCourseStudentTesting.Entityies;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using DBMSCourseStudentTesting.CLasses;
using System.Diagnostics;

namespace DBMSCourseStudentTesting.Tables
{
    public partial class Main : Form
    {
        private List<GetCurrentTest_Result> tests;
        private GetCurrentTest_Result selectedTest;
        private List<AcademicSubject> academicSubjects;
        private User user;
        private string buttonColumnText = "Открыть";
        private string buttonColumnName = "openTestButton";
        public Main()
        {
            InitializeComponent();
        }
        private void Main_Load(object sender, EventArgs e)
        {
            user = CreateUser();
            CheckRole();
            LoadData();
            LoadAcSubs();
            ReSize();
            AddButtonColumn();
        }
        #region InitForm
        private void ReSize()
        {
            if (!panel1.Visible)
            {
                dataGridView1.Dock = DockStyle.Fill;
            }
        }
        private void LoadAcSubs()
        {
            if(user.Role != UserRole.Admin)
            {
                using (StudentTestingEntities1 db = new StudentTestingEntities1())
                {
                    string query = @"Select AcS.id, AcS.Name, s.id As'idSubjects' from AcademicSubject as AcS
                                JOIN AcademicSubject_Teacher as AS_T ON AS_T.idAcademicSubject = AcS.id
                                JOIN Teacher as t on t.id = AS_T.idTeacher
                                JOIN Subjects as s on s.id = AcS.idSubjects
                                where t.login = @Login";
                    SqlParameter parameter = new SqlParameter("@Login", user.Login);
                    academicSubjects = db.Database.SqlQuery<AcademicSubject>(query, parameter).ToList();
                    academicSubjects.ForEach(o => comboBoxAcSubs.Items.Add(o.Name.Trim()));
                }
            }
            else
            {
                using (StudentTestingEntities1 db = new StudentTestingEntities1())
                {
                    db.AcademicSubject.Load();
                    academicSubjects = db.AcademicSubject.Local.ToBindingList().ToList();
                    academicSubjects.ForEach(o => comboBoxAcSubs.Items.Add(o.Name.Trim()));
                }
            }
        }
        private void LoadData()
        {
            using (StudentTestingEntities1 db = new StudentTestingEntities1())
            {
                db.GetCurrentTest().Load();
                tests = db.GetCurrentTest().ToList();

                dataGridView1.DataSource = tests;

                dataGridView1.Columns["Name"].HeaderText = "Название теста";
                dataGridView1.Columns["PassMark"].HeaderText = "Проходной балл";
                dataGridView1.Columns["id"].Visible = false;

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                
            }
        }
        private void AddButtonColumn()
        {
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.HeaderText = "";
            buttonColumn.Name = buttonColumnName;
            buttonColumn.Text = buttonColumnText;
            buttonColumn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(buttonColumn);
            dataGridView1.CellClick += new DataGridViewCellEventHandler(dataGridView1_CellClick);
        }
        private User CreateUser()
        {
            User user;
            using (StudentTestingEntities1 db = new StudentTestingEntities1())
            {
                string login = db.GetLoginT().FirstOrDefault();
                string role = db.GetRole(login).FirstOrDefault();

                user = new User(login, role);
            }
            return user;
        }
        private void CheckRole()
        {
            if (user.Role == UserRole.Admin)
                return;
            if (user.Role == UserRole.Teacher)
            {
                администрированиеToolStripMenuItem.Visible = false;
                предметыИТестыToolStripMenuItem.Visible = false;
                return;
            }
            if (user.Role == UserRole.Student)
            {
                администрированиеToolStripMenuItem.Visible = false;
                предметыИТестыToolStripMenuItem.Visible = false;
                panel1.Visible = false;
                return;
            }
            MessageBox.Show("Не полученны данны пользователя");
            Application.Exit();
        }
        #endregion
        #region Insert,Update,Delete
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            short passMark;

            bool text = tbName.Text.Equals("");
            bool number = !Int16.TryParse(tbPassMark.Text, out passMark);
            bool acsub = comboBoxAcSubs.SelectedItem != null;

            if (text | number)
                return;

            if (acsub)
            {
                string selectedText = comboBoxAcSubs.SelectedItem.ToString().Trim();
                AcademicSubject academicSubject = academicSubjects.Find(item => item.Name.Trim().Equals(selectedText));

                using (StudentTestingEntities1 db = new StudentTestingEntities1())
                {
                    string query = @"exec dbo.addtest @TextName, @PassMark, @IdAcSub";

                    List<SqlParameter> sqlParameters = new List<SqlParameter>();
                    sqlParameters.Add(new SqlParameter("@TextName", tbName.Name));
                    sqlParameters.Add(new SqlParameter("@PassMark", passMark));
                    sqlParameters.Add(new SqlParameter("@IdAcSub", academicSubject.id));
                    db.Database.ExecuteSqlCommand(query, sqlParameters.ToArray());
                }
                Clear();
                LoadData();
            }
            else if(user.Role.Equals(UserRole.Admin))
            {
                DialogResult result =  MessageBox.Show("Предмет не задан! Тест не будет виден, пока ему его не присовоят.", "Предупреждение", MessageBoxButtons.OKCancel);
                if (result.Equals(DialogResult.OK))
                {
                    using (StudentTestingEntities1 db = new StudentTestingEntities1())
                    {
                        db.Test.Add(new Test()
                        {
                            Name = tbName.Text,
                            PassMark = passMark
                        });
                        db.SaveChanges();
                    }
                    Clear();
                    LoadData();
                }
                else return;
            }
            else
            {
               MessageBox.Show("Предмет не задан!");
            }
        }
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (selectedTest == null)
            {
                MessageBox.Show("Элемент не выбран");
                return;
            }
            selectedTest.Name = tbName.Text;
            selectedTest.PassMark = Convert.ToInt16(tbPassMark.Text);

            var updateTest = new Test()
            {
                id = (int)selectedTest.id,
                Name = selectedTest.Name,
                PassMark = (short)selectedTest.PassMark
            };

            using (StudentTestingEntities1 db = new StudentTestingEntities1())
            {
                db.Test.Add(updateTest);
                db.Entry(updateTest).State = EntityState.Modified;
                db.SaveChanges();
            }
            Clear();
            LoadData();
        }
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (selectedTest == null)
            {
                MessageBox.Show("Элемент не выбран");
                return;
            }
            var deleteTest = new Test()
            {
                id = (int)selectedTest.id,
                Name = selectedTest.Name,
                PassMark = (short)selectedTest.PassMark
            };

            using (StudentTestingEntities1 db = new StudentTestingEntities1())
            {
                db.Entry(deleteTest).State = EntityState.Deleted;
                db.SaveChanges();
            }
            Clear();
            LoadData();
        }
        private void buttonClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        private void Clear()
        {
            selectedTest = null;
            tbName.Text = "";
            tbPassMark.Text = "";
            buttonUpdate.Enabled = false;
            buttonDelete.Enabled = false;
        }
        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int idTest = Int32.Parse(dataGridView1["id", e.RowIndex].Value.ToString());
            selectedTest = tests.Find(item => item.id.Equals(idTest));

            tbName.Text = selectedTest.Name.Trim();
            tbPassMark.Text = selectedTest.PassMark.ToString();

            buttonUpdate.Enabled = true;
            buttonDelete.Enabled = true;
        }
        #endregion
        #region Test selection
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ignore clicks that are not on button cells. 
            if (e.RowIndex < 0 || e.ColumnIndex !=
                dataGridView1.Columns[buttonColumnName].Index) return;

            int idTest = Int32.Parse(dataGridView1["id", e.RowIndex].Value.ToString());
            var z = tests.Find(o => o.id == idTest);
            Test test = new Test() { id = (int)z.id, Name = z.Name, PassMark = (short)z.PassMark };


            if (user.Role == UserRole.Student)
            {
                TestStudent testStudent = new TestStudent(test, user);
                testStudent.Show();
                return;
            }
            if (user.Role == UserRole.Teacher || user.Role == UserRole.Admin)
            {
                TeacherStatisticks statisticks = new TeacherStatisticks(test, user);
                statisticks.Show();
                return;
            }
        }
        #endregion
        #region ToolStripMenuItems
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void обАвтореToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Сергей Сёмин ЭИС-35", "Разработчик");
        }
        private void администрированиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Administration administration = new Administration();
            administration.Show();

        }
        private void оРаботеПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("О программе.odt");
        }
        #endregion
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        private void предметыИТестыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tests tests = new Tests();
            tests.Show();
        }

       
    }
}

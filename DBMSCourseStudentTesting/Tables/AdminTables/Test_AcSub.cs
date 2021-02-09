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

namespace DBMSCourseStudentTesting.Tables.AdminTables
{
    public partial class Test_AcSub : Form
    {
        private Test test;
        private int idSubject;

        private AcademicSubject selectedAcademicSubject;
        List<AcademicSubject> allAcademicSubjects;
        List<AcademicSubject> academicSubjects;

        public Test_AcSub(Test test)
        {
            InitializeComponent();
            this.test = test;
        }

        private void Test_AcSub_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadAcSubjects();
        }
        #region Init
        private void LoadAcSubjects()
        {
            using (StudentTestingEntities1 db = new StudentTestingEntities1())
            {

                db.AcademicSubject.Load();
                allAcademicSubjects = db.AcademicSubject.Local.ToBindingList().ToList();
                allAcademicSubjects.ForEach(o => comboBoxAcSubs.Items.Add(o.Name.Trim()));
            }
        }

        private void LoadData()
        {
            using (StudentTestingEntities1 db = new StudentTestingEntities1())
            {

                string query = @"Select AcS.id, AcS.Name, AcS.idSubjects from AcademicSubject as AcS
                                    JOIN Test_AcademicSubject as T_AS on AcS.id = T_AS.idAcademicSubject
                                    JOIN Test as t on t.id = T_AS.idTest
                                    Where t.id = @idTest";
                SqlParameter parameter = new SqlParameter("@idTest", test.id);
                academicSubjects = db.Database.SqlQuery<AcademicSubject>(query, parameter).ToList();
                dataGridView1.DataSource = academicSubjects;

                dataGridView1.Columns["id"].Visible = false;
                dataGridView1.Columns["idSubjects"].Visible = false;
                dataGridView1.Columns["Subjects"].Visible = false;
                dataGridView1.Columns["Name"].HeaderText = "Предмет";
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }
        #endregion

        #region Inser, Delete
        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int idAc = Int32.Parse(dataGridView1["id", e.RowIndex].Value.ToString());
            selectedAcademicSubject = allAcademicSubjects.Find(item => item.id.Equals(idAc));

            int index = comboBoxAcSubs.Items.IndexOf(selectedAcademicSubject.Name.Trim());
            comboBoxAcSubs.SelectedItem = comboBoxAcSubs.Items[index];

            buttonDelete.Enabled = true;
            buttonAdd.Enabled = false;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string selectedText = comboBoxAcSubs.SelectedItem.ToString().Trim();
            AcademicSubject academicSubject = allAcademicSubjects.Find(item => item.Name.Trim().Equals(selectedText));

            bool alreadyExist = false;
            var x = dataGridView1.Rows;

            for (int i = 0; i < x.Count; i++)
            {
                if (Convert.ToInt32(x[i].Cells["id"].Value) == academicSubject.id) alreadyExist = true;
            }

            if (academicSubject == null || alreadyExist)
            {
                MessageBox.Show("Данная связь уже существует или предмет не выбран");
                return;
            }
            using (StudentTestingEntities1 db = new StudentTestingEntities1())
            {
                string query = @"Insert Into Test_AcademicSubject Values
                                (@idTest, @idAcademicSubject)";

                List<SqlParameter> sqlParameters = new List<SqlParameter>();
                sqlParameters.Add(new SqlParameter("@idAcademicSubject", academicSubject.id));
                sqlParameters.Add(new SqlParameter("@idTest", test.id));
                db.Database.ExecuteSqlCommand(query, sqlParameters.ToArray());
                LoadData();
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            using (StudentTestingEntities1 db = new StudentTestingEntities1())
            {
                string query = @"Delete from Test_AcademicSubject
                        Where Test_AcademicSubject.idAcademicSubject = @idAcademicSubject
                        and Test_AcademicSubject.idTest = @idTest";

                List<SqlParameter> sqlParameters = new List<SqlParameter>();
                sqlParameters.Add(new SqlParameter("@idAcademicSubject", selectedAcademicSubject.id));
                sqlParameters.Add(new SqlParameter("@idTest", test.id));
                db.Database.ExecuteSqlCommand(query, sqlParameters.ToArray());
            }
            LoadData();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            comboBoxAcSubs.SelectedIndex = -1;
            buttonDelete.Enabled = false;
            buttonAdd.Enabled = true;
        }

        #endregion


    }
}

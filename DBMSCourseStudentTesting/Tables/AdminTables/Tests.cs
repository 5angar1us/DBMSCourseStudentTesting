using DBMSCourseStudentTesting.Entityies;
using DBMSCourseStudentTesting.Tables.AdminTables;
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
    public partial class Tests : Form
    {
        public Tests()
        {
            InitializeComponent();
        }

        private List<Test> tests;
        private string buttonColumnName = "AcSubs";
        private string buttonColumnText = "Связанные предметы";
        private string withOutAcSub = "Без предметов";
        private List<AcademicSubject> academicSubjects;

        private void TestAcSubs_Load(object sender, EventArgs e)
        {
            LoadAllTest();
            LoadAcSubs();
            AddColumnButton();
        }

        private void LoadAllTest()
        {
            using (StudentTestingEntities1 db = new StudentTestingEntities1())
            {
                string query = @"Select t.id, t.Name, t.PassMark from test as t";
                tests = db.Database.SqlQuery<Test>(query).ToList();
                dataGridView1.DataSource = tests;
                ConfigureDataGridView();
            }
        }

        private void LoadTestWithAcSub()
        {
            string selectedText = comboBoxAcSubs.SelectedItem.ToString().Trim();
            AcademicSubject academicSubject = academicSubjects.Find(o => o.Name.Trim().Equals(selectedText));


            using (StudentTestingEntities1 db = new StudentTestingEntities1())
            {
                string query = @"Select t.id,t.Name,t.PassMark from test as t
                                JOIN Test_AcademicSubject as T_AS ON T_AS.idTest = t.id
                                Where T_AS.idAcademicSubject = @idAS;";
                SqlParameter parameter = new SqlParameter("@idAS", academicSubject.id);
                tests = db.Database.SqlQuery<Test>(query, parameter).ToList();
                dataGridView1.DataSource = tests;
                ConfigureDataGridView();
            }
        }

        private void LoadTestWithOutAcSub()
        {
            using (StudentTestingEntities1 db = new StudentTestingEntities1())
            {
                string query = @"Select t.id,t.Name,t.PassMark from Test_AcademicSubject as T_A
                                RIGHT Outer JOIN Test as t on t.id = T_A.idTest
                                Where T_A.idAcademicSubject is NULL";

                tests = db.Database.SqlQuery<Test>(query).ToList();
                dataGridView1.DataSource = tests;
                ConfigureDataGridView();
            }
        }

        private void LoadAcSubs()
        {
            using (StudentTestingEntities1 db = new StudentTestingEntities1())
            {
                db.AcademicSubject.Load();
                academicSubjects = db.AcademicSubject.Local.ToBindingList().ToList();
                academicSubjects.ForEach(o => comboBoxAcSubs.Items.Add(o.Name.Trim()));
                comboBoxAcSubs.Items.Add(withOutAcSub);
            }
        }

        private void ConfigureDataGridView()
        {
            dataGridView1.Columns["Name"].HeaderText = "Название теста";
            dataGridView1.Columns["PassMark"].HeaderText = "Проходной балл";
            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["Examination"].Visible = false;
            dataGridView1.Columns["GroupTesting"].Visible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            
        }

        private void AddColumnButton()
        {
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.HeaderText = "";
            buttonColumn.Name = buttonColumnName;
            buttonColumn.Text = buttonColumnText;
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
            Test test = tests.Find(o => o.id == id);

            Test_AcSub test_AcSub = new Test_AcSub(test);
            test_AcSub.Show();
        }

        private void comboBoxAcSubs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxAcSubs.SelectedIndex.Equals(-1))
            {
                LoadAllTest();
            }
            else if (comboBoxAcSubs.SelectedItem.Equals(withOutAcSub))
            {
                LoadTestWithOutAcSub();
            }
            else
            {
                LoadTestWithAcSub();
            }
        }
    }
}

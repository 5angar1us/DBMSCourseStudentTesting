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

namespace DBMSCourseStudentTesting
{
    public partial class EditTestAccess : Form
    {
        
        private Test test;
        private List<GroupTesting> groupTestingList;
        private List<AcademicGroup> academicGroups;
        private GroupTesting selectedGroupTesting;
        private GroupTesting updatedGroupTesting;
        public EditTestAccess(Test test)
        {
            this.test = test;
            InitializeComponent();
            Text = $"Редактирование доступа к тесту \"{test.Name.Trim()}\" ";
        }

        private void EditTestAccess_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadAcGroups();
        }
        #region Init
        private void LoadAcGroups()
        {
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
                var data = from gt in db.GroupTesting
                           join ag in db.AcademicGroup
                           on gt.idAcademicGroup equals ag.id
                           where gt.idTest == test.id
                           select new
                           {
                               GroupName = ag.Name,
                               gt.expirationDate,
                               gt.isAvailable
                           };
                string query = "Select gt.idTest,gt.idAcademicGroup,gt.expirationDate,gt.isAvailable from GroupTesting as gt Where gt.idTest = @idTest";
                SqlParameter parameter = new SqlParameter("@idTest", test.id);
                List<GroupTesting> groupTesting = db.Database.SqlQuery<GroupTesting>(query, parameter).ToList();

                groupTestingList = groupTesting.ToList();
                dataGridView1.DataSource = data.ToList();
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


                dataGridView1.Columns["GroupName"].HeaderText = "Группа";
                dataGridView1.Columns["expirationDate"].HeaderText = "Дата окончания тестирования";
                dataGridView1.Columns["isAvailable"].HeaderText = "Доступен студентам";
            }
        }
        #endregion

        #region Insert, Update, Delete
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string selectedText = comboBox1.SelectedItem.ToString().Trim();
            AcademicGroup academicGroup = academicGroups.Find(item => item.Name.Trim().Equals(selectedText));
            bool alreadyExist = false;
            var x = dataGridView1.Rows;

            for (int i = 0; i < x.Count; i++)
            {
                if (Convert.ToString(x[i].Cells["GroupName"].Value) == academicGroup.Name) alreadyExist = true;
            }

            if (academicGroup == null || alreadyExist)
            {
                MessageBox.Show("Данная связь уже существует или предмет не выбран");
                return;
            }
            using (StudentTestingEntities1 db = new StudentTestingEntities1())
            {
                string query = @"INSERT into GroupTesting(idTest,idAcademicGroup,expirationDate,isAvailable) VALUES 
                                (@idTest, @idAcademicGroup , @expirationDate, @isAvailable)";

                List<SqlParameter> sqlParameters = new List<SqlParameter>();
                sqlParameters.Add(new SqlParameter("@idTest", test.id));
                sqlParameters.Add(new SqlParameter("@idAcademicGroup", academicGroup.id));
                sqlParameters.Add(new SqlParameter("@expirationDate", dateTimePicker1.Value));
                sqlParameters.Add(new SqlParameter("@isAvailable", checkBox1.Checked));
                db.Database.ExecuteSqlCommand(query, sqlParameters.ToArray());
            }
            LoadData();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (selectedGroupTesting == null || updatedGroupTesting == null)
            {
                MessageBox.Show("Элемент не выбран");
                return;
            }
            string selectedText = comboBox1.SelectedItem.ToString().Trim();
            AcademicGroup academicGroup = academicGroups.Find(item => item.Name.Trim().Equals(selectedText));
            if (academicGroup == null)
            {
                MessageBox.Show("Группа не выбрана или доступ такой группе ещё не разрешён");
                return;
            }

            bool alredyExist = false;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1["GroupName", i].Value.ToString().Equals(academicGroup.Name))
                    alredyExist = true;
            }

            updatedGroupTesting.idAcademicGroup = academicGroup.id;
            updatedGroupTesting.expirationDate = dateTimePicker1.Value;
            updatedGroupTesting.isAvailable = checkBox1.Checked;

            if (!selectedGroupTesting.idAcademicGroup.Equals(updatedGroupTesting.idAcademicGroup))
            {
                if (alredyExist)
                {
                    MessageBox.Show("Такой группе уже выдан доступ");
                    return;
                }
            }
            UpdateAttributes();
            Clear();
            LoadData();
        }

        private void UpdateAttributes()
        {
            if (!selectedGroupTesting.idAcademicGroup.Equals(updatedGroupTesting.idAcademicGroup))
            {
                using (StudentTestingEntities1 db = new StudentTestingEntities1())
                {
                    string query = @"Update GroupTesting Set idAcademicGroup = @updateIdAcademicGroup 
                                    where idTest=@idTest and idAcademicGroup=@selectIdAcademicGroup";

                    List<SqlParameter> sqlParameters = new List<SqlParameter>();
                    sqlParameters.Add(new SqlParameter("@idTest", test.id));
                    sqlParameters.Add(new SqlParameter("@updateIdAcademicGroup", updatedGroupTesting.idAcademicGroup));
                    sqlParameters.Add(new SqlParameter("@selectIdAcademicGroup", selectedGroupTesting.idAcademicGroup));

                    db.Database.ExecuteSqlCommand(query, sqlParameters.ToArray());
                }
            }

            if (!selectedGroupTesting.expirationDate.Equals(updatedGroupTesting.expirationDate))
            {
                using (StudentTestingEntities1 db = new StudentTestingEntities1())
                {
                    string query = @"Update GroupTesting Set expirationDate = @updateExpirationDate
                                    where idTest=@idTest 
                                    and idAcademicGroup=@updateIdAcademicGroup 
                                    and expirationDate=@selectExpirationDate";

                    List<SqlParameter> sqlParameters = new List<SqlParameter>();
                    sqlParameters.Add(new SqlParameter("@idTest", test.id));
                    sqlParameters.Add(new SqlParameter("@updateIdAcademicGroup ", updatedGroupTesting.idAcademicGroup));
                    sqlParameters.Add(new SqlParameter("@updateExpirationDate", updatedGroupTesting.expirationDate));
                    sqlParameters.Add(new SqlParameter("@selectExpirationDate", selectedGroupTesting.expirationDate));

                    db.Database.ExecuteSqlCommand(query, sqlParameters.ToArray());
                }
            }
            if (!selectedGroupTesting.isAvailable.Equals(updatedGroupTesting.isAvailable))
            {
                using (StudentTestingEntities1 db = new StudentTestingEntities1())
                {
                    string query = @"Update GroupTesting Set isAvailable = @updateIsAvailable
                                    where idTest=@idTest 
                                    and idAcademicGroup=@updateIdAcademicGroup 
                                    and isAvailable=@selectIsAvailable";

                    List<SqlParameter> sqlParameters = new List<SqlParameter>();
                    sqlParameters.Add(new SqlParameter("@idTest", test.id));
                    sqlParameters.Add(new SqlParameter("@updateIdAcademicGroup ", updatedGroupTesting.idAcademicGroup));
                    sqlParameters.Add(new SqlParameter("@updateIsAvailable", updatedGroupTesting.isAvailable));
                    sqlParameters.Add(new SqlParameter("@selectIsAvailable", selectedGroupTesting.isAvailable));

                    db.Database.ExecuteSqlCommand(query, sqlParameters.ToArray());
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (selectedGroupTesting == null || updatedGroupTesting == null)
            {
                MessageBox.Show("Элемент не выбран");
                return;
            }

            using (StudentTestingEntities1 db = new StudentTestingEntities1())
            {
                string query = @"Delete GroupTesting 
                                where idTest=@idTest 
                                and idAcademicGroup=@selectIdAcademicGroup 
                                and isAvailable=@selectIsAvailable
                                and expirationDate=@selectExpirationDate";

                List<SqlParameter> sqlParameters = new List<SqlParameter>();
                sqlParameters.Add(new SqlParameter("@idTest", test.id));
                sqlParameters.Add(new SqlParameter("@selectIdAcademicGroup", selectedGroupTesting.idAcademicGroup));
                sqlParameters.Add(new SqlParameter("@selectIsAvailable", selectedGroupTesting.isAvailable));
                sqlParameters.Add(new SqlParameter("@selectExpirationDate", selectedGroupTesting.expirationDate));

                db.Database.ExecuteSqlCommand(query, sqlParameters.ToArray());
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
            selectedGroupTesting = null;
            updatedGroupTesting = null;

            comboBox1.SelectedIndex = -1;
            dateTimePicker1.Value = DateTime.Today;
            checkBox1.Checked = false;

            buttonUpdate.Enabled = false;
            buttonDelete.Enabled = false;
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string GroupName = dataGridView1["GroupName", e.RowIndex].Value.ToString();
            AcademicGroup academicGroup = academicGroups.Find(item => item.Name.Equals(GroupName));
            selectedGroupTesting = groupTestingList.Find(o => o.idAcademicGroup.Equals(academicGroup.id));

            int index = comboBox1.Items.IndexOf(academicGroup.Name.Trim());
            comboBox1.SelectedItem = comboBox1.Items[index];

            dateTimePicker1.Value = selectedGroupTesting.expirationDate;

            checkBox1.Checked = selectedGroupTesting.isAvailable;

            updatedGroupTesting = new GroupTesting()
            {
                idTest = selectedGroupTesting.idTest,
                idAcademicGroup = selectedGroupTesting.idAcademicGroup,
                expirationDate = selectedGroupTesting.expirationDate,
                isAvailable = selectedGroupTesting.isAvailable
            };
            buttonUpdate.Enabled = true;
            buttonDelete.Enabled = true;
        }
        #endregion
    }
}

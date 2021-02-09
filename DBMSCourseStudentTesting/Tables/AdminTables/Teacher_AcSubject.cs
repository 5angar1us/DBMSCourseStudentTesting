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
namespace DBMSCourseStudentTesting.Tables
{
    public partial class Teacher_AcSubject : Form
    {
        private int idTeacher;
        private int idSubject;
        private AcademicSubject selectedAcademicSubject;
        List<AcademicSubject> academicSubjects;

        public Teacher_AcSubject(int idTeacher,string name)
        {
            InitializeComponent();
            Text = $"Предметы преподавателя \"{name}\"";
            this.idTeacher = idTeacher;
        }

        private void Teacher_AcSubject_Load(object sender, EventArgs e)
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
                academicSubjects = db.AcademicSubject.Local.ToBindingList().ToList();
                academicSubjects.ForEach(o => comboBox1.Items.Add(o.Name.Trim()));
            }
        }

        private void LoadData()
        {
            using (StudentTestingEntities1 db = new StudentTestingEntities1())
            {
                var z = db.GetAcSubjects(idTeacher);
                dataGridView1.DataSource = z.ToList();
                dataGridView1.Columns["id"].Visible = false;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dataGridView1.Columns["Name"].HeaderText = "Предмет";
            }
        }
        #endregion

        #region Inser, Delete
        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int idAc = Int32.Parse(dataGridView1["id", e.RowIndex].Value.ToString());
            selectedAcademicSubject = academicSubjects.Find(item => item.id.Equals(idAc));

            int index = comboBox1.Items.IndexOf(selectedAcademicSubject.Name.Trim());
            comboBox1.SelectedItem = comboBox1.Items[index];

            buttonDelete.Enabled = true;
            buttonAdd.Enabled = false;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string selectedText = comboBox1.SelectedItem.ToString().Trim();
            AcademicSubject academicSubject = academicSubjects.Find(item => item.Name.Trim().Equals(selectedText));
           
            bool alreadyExist = false;
            var x = dataGridView1.Rows;

            for (int i = 0; i < x.Count; i++)
            {
                if (Convert.ToInt32(x[i].Cells["id"].Value) == academicSubject.id) alreadyExist = true;
            }

            if (academicSubject == null || alreadyExist) {
                MessageBox.Show("Данная связь уже существует или предмет не выбран");
                return;
            }
            using (StudentTestingEntities1 db = new StudentTestingEntities1())
            {
                db.AddAcSubjects(idTeacher, academicSubject.id);
                LoadData();
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            using (StudentTestingEntities1 db = new StudentTestingEntities1())
            {
                db.DeleteAcSubjects(idTeacher, selectedAcademicSubject.id);
            }
            LoadData();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            comboBox1.SelectedIndex = -1;
            buttonDelete.Enabled = false;
            buttonAdd.Enabled = true;
        }
        #endregion

    }

}

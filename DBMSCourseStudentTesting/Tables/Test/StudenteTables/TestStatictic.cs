using DBMSCourseStudentTesting.CLasses;
using DBMSCourseStudentTesting.Entityies;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBMSCourseStudentTesting
{
    public partial class TestResult : Form
    {
        private int maxMark;
        private int mark;
        private Test test;
        private User user;

        public TestResult(int maxMark, int mark, Test test , User user)
        {
            InitializeComponent();
            this.maxMark = maxMark;
            this.mark = mark;
            this.test = test;
            this.user = user;
        }

        private void TestResult_Load(object sender, EventArgs e)
        {
            tbMark.Text = mark.ToString();
            tbPassMark.Text = test.PassMark.ToString();
            tbMaxMark.Text = maxMark.ToString();

            using (StudentTestingEntities1 db = new StudentTestingEntities1())
            {

                var examination = from eX in db.Examination
                                  where
                                    eX.idTest == test.id
                                  select new
                                  {
                                      eX.Mark,
                                      eX.ExamDate
                                  };

                dataGridView1.DataSource = examination.ToList();
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dataGridView1.Columns["Mark"].HeaderText = "Балл";
                dataGridView1.Columns["ExamDate"].HeaderText = "Дата сдачи теста";

                db.Student.Load();
                var x = from s in db.Student
                        where
                          s.Login == user.Login
                        select new
                        {
                            s.id
                        };

                int idSudent = x.FirstOrDefault().id;

                //MessageBox.Show($"{login} {idSudent}");
                db.AddExam(test.id, idSudent, (short)mark);
            }
        }
    }

}

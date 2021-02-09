using DBMSCourseStudentTesting.Entityies;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Linq;
using System.Drawing;
using DBMSCourseStudentTesting.Controls;
using DBMSCourseStudentTesting.CLasses;

namespace DBMSCourseStudentTesting
{
    public partial class TestStudent : Form
    {
        private int spaceSize = TbRoCoutnt.SPACE_SIZE;
        private List<QuestionControl> questionControls;
        private List<Question> questionList;
        private Test test;
        private User user;

        public TestStudent(Test test,User user)
        {
            InitializeComponent();
            MaximizeBox = false;
            this.test = test;
            this.user = user;
            Text = $"Òåñò \"{test.Name.Trim()}\" ";
        }

        private void TestStudent_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        #region Init
        private void LoadData()
        {
            using (StudentTestingEntities1 db = new StudentTestingEntities1())
            {
                SqlParameter param = new SqlParameter("idTest", test.id.ToString());

                List<int> idQuestions = db.Database.SqlQuery<int>("Select q_t.idQuestion from Question_Test as q_t Where q_t.idTest = @idTest", param).ToList();

                if (idQuestions.Count != 0)
                {

                    var questions = from q in db.Question
                                    where
                                      idQuestions.Contains(q.id)
                                    select new
                                    {
                                        id = q.id,
                                        Text = q.Text,
                                        idSubjects = q.idSubjects,
                                        Mark = q.Mark
                                    };

                    questionList = new List<Question>();

                    foreach (var o in questions)
                    {
                        questionList.Add(new Question() { id = o.id, Text = o.Text, idSubjects = o.idSubjects, Mark = o.Mark });
                    }
                    questionControls = AddControls();
                    ChangeSizeControls(questionControls);
                }
               
            }
        }
        #endregion

        #region Resize
        private void ChangeSizeControls(List<QuestionControl> questionControls)
        {
            Location location = new Location(spaceSize, 0);
            System.Drawing.Point point = location.GetNextPoint();
            location.AddY(spaceSize);

            questionControls.ForEach(z =>
            {
                z.Location = location.GetNextPoint();
                location.AddControlY(z, 1);
            });

            if (questionControls.Count <= 0) return;
            Location buttonLocation = new Location(Width,
                location.Y);
            Width = questionControls[0].Width + spaceSize * 3;

            buttonLocation.SubControlX(buttonÑomplete, 3);
            buttonÑomplete.Location = buttonLocation.GetNextPoint();
        }
        #endregion

        #region Making controls
        private List<QuestionControl> AddControls()
        {
            List<QuestionControl> result = new List<QuestionControl>();
            for (int i = 0; i < questionList.Count; i++)
            {
                result.Add(ConfigurateControl(questionList[i]));
            }
            result.ForEach(x => Controls.Add(x));
            return result;
        }
        private QuestionControl ConfigurateControl(Question question)
        {
            using (StudentTestingEntities1 db = new StudentTestingEntities1())
            {
                var z = from a in db.Answer
                        where
                          a.idQuestion == question.id
                        select new
                        {
                            Id = a.Id,
                            idQuestion = a.idQuestion,
                            Text = a.Text,
                            isCorrect = a.isCorrect
                        };
                List<Answer> answers = new List<Answer>();
                foreach (var o in z)
                {
                    answers.Add(new Answer() { Id = o.Id, idQuestion = o.idQuestion, isCorrect = o.isCorrect, Text = o.Text });
                }
                QuestionControl control = new QuestionControl(question, answers);
                control.Location = new System.Drawing.Point(28, 63);
                control.MaximumSize = new Size(Width, 0);
                control.Size = new System.Drawing.Size(Width, 28);
                control.Visible = true;
                return control;
            }
        }
        #endregion

        private void buttonÑomplete_Click(object sender, EventArgs e)
        {
            int maxMark = 0;
            int mark = 0;
            int passMark = test.PassMark;

            questionControls.ForEach(x =>
            {
                bool choise = true;
                x.AnsverControls.ForEach(y =>
                {
                    if (!y.isGoodChoice()) choise = false;

                });
                if (choise) mark += x.QuestionT.Mark;
                maxMark += x.QuestionT.Mark;
            });
            var z = new TestResult(maxMark, mark, test,user);
            Hide();
            z.Owner = this;
            
            z.FormClosed += Form2_FormClosed;
            z.Show();

        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dispose();
        }
    }
}

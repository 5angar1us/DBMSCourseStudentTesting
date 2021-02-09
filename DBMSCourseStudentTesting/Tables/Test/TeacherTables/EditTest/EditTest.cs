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
using DBMSCourseStudentTesting.CLasses;
using DBMSCourseStudentTesting.Controls;
using DBMSCourseStudentTesting.Entityies;

namespace DBMSCourseStudentTesting
{
    public partial class EditTest : Form
    {

        private List<Control> questionControls;
        private List<Question> questionList;
        private List<Subjects> subjects;
        private Test test;
        private User user;

        int spaceSize = TbRoCoutnt.SPACE_SIZE;

        public EditTest(Test test, User user)
        {
            InitializeComponent();
            MaximizeBox = false;

            HorizontalScroll.Enabled = false;
            HorizontalScroll.Visible = false;

            this.test = test;
            this.user = user;

            questionList = new List<Question>();
            subjects = new List<Subjects>();
            Text = $"Редактирование теста \"{test.Name.Trim()}\" ";
        }
        private void EditTest_Load(object sender, EventArgs e)
        {
            LoadData();
            questionControls = AddControls();
            questionControls.ForEach(o => o.Resize += new System.EventHandler(this.EditQuestionControl_SizeChanged));

            LoadSubject();
            ChangeSizeControls();
            VerticalScroll.Value = 0;
        }
        #region Init
        

        private void LoadData()
        {
            questionList.Clear();
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

                    foreach (var o in questions)
                    {
                        questionList.Add(new Question() { id = o.id, Text = o.Text, idSubjects = o.idSubjects, Mark = o.Mark });
                    }
                }
            }
        }
        private void LoadAnsvers(Question question, List<Answer> answers)
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

                foreach (var o in z)
                {
                    answers.Add(new Answer() { Id = o.Id, idQuestion = o.idQuestion, isCorrect = o.isCorrect, Text = o.Text });
                }


            }
        }
        private void LoadSubject()
        {
            using (StudentTestingEntities1 db = new StudentTestingEntities1())
            {
                if (user.Role == UserRole.Teacher)
                {
                    string query = @"Select DISTINCT s.id,s.Name from Subjects as s
                                JOIN AcademicSubject as AcSu on s.id = AcSu.idSubjects
                                JOIN AcademicSubject_Teacher as A_T on AcSu.id = A_T.idAcademicSubject
                                JOIN Teacher as t on t.id = A_T.idTeacher
                                where t.login = @Login";
                    SqlParameter parameter = new SqlParameter("@Login", user.Login);
                    subjects = db.Database.SqlQuery<Subjects>(query, parameter).ToList();
                    subjects.ForEach(o => comboBoxSubjects.Items.Add(o.Name.Trim()));

                }
                else
                {
                    db.Subjects.Load();
                    subjects = db.Subjects.Local.ToBindingList().ToList();
                    subjects.ForEach(o => comboBoxSubjects.Items.Add(o.Name.Trim()));
                }



                if (questionList.Count > 0)
                {
                    Subjects subject = subjects.Find(item => item.id.Equals(questionList[0].idSubjects));

                    comboBoxSubjects.SelectedIndex = subjects.IndexOf(subject);
                }
            }

        }
        #endregion

        #region ReSize
        private void ChangeSizeControls()
        {
            VerticalScroll.Value = 0;

            if (questionControls.Count != 0)
                Width = questionControls[0].Width + TbRoCoutnt.SPACE_SIZE * 3;

            Location location = new Location(spaceSize, spaceSize + 5);
            panel1.Location = location.GetNextPoint();
            location.AddControlY(comboBoxSubjects, 0);

            System.Drawing.Point point = location.GetNextPoint();
            location.AddY(TbRoCoutnt.SPACE_SIZE);

            questionControls.ForEach(z =>
            {
                z.Location = location.GetNextPoint();
                location.AddControlY(z, 1);
            });

            if (questionControls.Count <= 0) return;
            Location buttonLocation = new Location(Width,
                location.Y);


            buttonLocation.SubControlX(buttonAddQuestion, 3);
            buttonAddQuestion.Location = buttonLocation.GetNextPoint();

        }
        #endregion

        #region Making controls
        private List<Control> AddControls()
        {
            List<Control> result = new List<Control>();
            if (questionList.Count == 0) return result;

            for (int i = 0; i < questionList.Count; i++)
            {
                result.Add(ConfigurateControl(questionList[i]));
            }
            result.ForEach(x => Controls.Add(x));
            return result;
        }
        private EditQuestionControl ConfigurateControl(Question question)
        {
            List<Answer> answers = new List<Answer>();
            LoadAnsvers(question, answers);
            EditQuestionControl control = ControlsFactory.CreateQuestionControl(question, answers, Width);
            return control;
        }
        private EditQuestionControl CreateControl(Question question)
        {
            List<Answer> answers = new List<Answer>();
            EditQuestionControl control = ControlsFactory.CreateQuestionControl(question, answers, Width);
            return control;
        }
        #endregion

        #region buttonClick
        private void buttonAddQuestion_Click(object sender, EventArgs e)
        {
            if(comboBoxSubjects.SelectedItem == null)
            {
                MessageBox.Show("Выберите предметную область");
                return;
            }
            string selectedText = comboBoxSubjects.SelectedItem.ToString().Trim();
            Subjects subject = subjects.Find(item => item.Name.Trim().Equals(selectedText));

            Question question = new Question();
            question.idSubjects = subject.id;
            question.Mark = 1;
            question.Text = "текст вопроса";

            Control control = CreateControl(question);
            control.Resize += new System.EventHandler(this.EditQuestionControl_SizeChanged);
            questionControls.Add(control);

            questionControls.ForEach(x => Controls.Add(x));
            ChangeSizeControls();
        }
        #endregion

        #region Inser, Update, Delete
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            object selectedItem = comboBoxSubjects.SelectedItem;
            if (selectedItem == null) return;

            string selectedText = selectedItem.ToString().Trim();
            Subjects subject = subjects.Find(item => item.Name.Trim().Equals(selectedText));

            List<Question> saveList = new List<Question>();
            List<Question> updateList = new List<Question>();
            List<Question> deleteList = new List<Question>();

            //Разделение вопросов на группы
            questionControls.ForEach(o =>
            {
                var z = o as EditQuestionControl;
                if (z.isChanged)
                {
                    bool isUpdate = false;

                    questionList.ForEach(p =>
                    {
                        if (z.Question.Equals(p))
                        {
                            updateList.Add(z.Question);
                            isUpdate = true;
                        }
                    });
                    if (!isUpdate)
                        saveList.Add(z.Question);
                }
            });
            deleteList = GetDeleteList();

            Save(subject, saveList);
            Update(updateList);
            Delete(deleteList);

            questionControls.ForEach(o =>
            {
                var z = o as EditQuestionControl;
                z.SaveData();
            });

            
            while(questionControls.Count != 0)
            {
                questionControls[0].Dispose();
            }
            
            LoadData();
           
            questionControls = AddControls();
            questionControls.ForEach(o => o.Resize += new System.EventHandler(this.EditQuestionControl_SizeChanged));
            ChangeSizeControls();
        }

        private static void Delete(List<Question> deleteList)
        {
            using (StudentTestingEntities1 db = new StudentTestingEntities1())
            {
                deleteList.ForEach(o =>
                {
                    db.Question.Attach(o);
                    db.Question.Remove(o);
                });
                db.SaveChanges();
            }

        }

        private void Update(List<Question> updateList)
        {
            using (StudentTestingEntities1 db = new StudentTestingEntities1())
            {
                try
                {
                    updateList.ForEach(o =>
                    {
                        var entity = db.Question.Where(z=> z.id == o.id).FirstOrDefault();
                        if (entity == null)
                        {
                            return;
                        }

                        db.Entry(entity).CurrentValues.SetValues(o);
                    });
                    db.SaveChanges();
                }
                catch (Exception j)
                {
                    Console.Write(j.ToString());
                }
            }
        }

        private void Save(Subjects subject, List<Question> saveList)
        {
            using (StudentTestingEntities1 db = new StudentTestingEntities1())
            {
                saveList.ForEach(o =>
                {
                    o.idSubjects = subject.id;
                    db.Question.Add(o);
                });
                db.SaveChanges();

                saveList.ForEach(o =>
                {
                    o = db.Question.Where(z => o.idSubjects == z.idSubjects & o.Mark == z.Mark & o.Text == z.Text).FirstOrDefault();
                    string query = @"INSERT into Question_Test(idQuestion,idTest) VALUES
                                    (@idQuestion, @idTest)";
                    List<SqlParameter> sqlParameters = new List<SqlParameter>();
                    sqlParameters.Add(new SqlParameter("@idQuestion", o.id));
                    sqlParameters.Add(new SqlParameter("@idTest", test.id));
                    db.Database.ExecuteSqlCommand(query, sqlParameters.ToArray());
                });
            }
        }

        private List<Question> GetDeleteList()
        {
            List<Question> deleteList;
            List<Question> questionsInControls = new List<Question>();
            questionControls.ForEach(o =>
            {
                var z = o as EditQuestionControl;
                questionsInControls.Add(z.Question);
            });
            deleteList = questionList.AsQueryable().Except(questionsInControls).ToList();
            return deleteList;
        }
        #endregion

        private void comboBoxSubjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedText = comboBoxSubjects.SelectedItem.ToString().Trim();
            Subjects subject = subjects.Find(item => item.Name.Trim().Equals(selectedText));
            questionControls.ForEach(o =>
            {
                var z = o as EditQuestionControl;
                z.Question.idSubjects = subject.id;
                z.isChanged = true;
            });
        }
        private void EditTest_ControlRemoved(object sender, ControlEventArgs e)
        {
            questionControls.Remove(e.Control);
            ChangeSizeControls();
        }

        private void EditQuestionControl_SizeChanged(object sender, EventArgs e)
        {
            ChangeSizeControls();
        }
    }
}

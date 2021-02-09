using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DBMSCourseStudentTesting.Entityies;
using System.Data.Entity;

namespace DBMSCourseStudentTesting.Controls
{
    public partial class EditQuestionControl : UserControl
    {
        public EditQuestionControl()
        {
            InitializeComponent();
        }
        public Question Question { get; set; }
        public List<Answer> Ansvers { get; set; }
        public List<Control> AnsverControls { get; private set; }
        public bool isChanged { set; get; }
        const int minHide = 19;
        const int spaceSize = TbRoCoutnt.SPACE_SIZE;

        public EditQuestionControl(Question question, List<Answer> ansvers)
        {
            Question = question;
            Ansvers = ansvers;
            InitializeComponent();
        }


        private void EditQuestionControl_Load(object sender, EventArgs e)
        {
            ChangeQuestionText(Question.Text);
            questionMark.Value = Question.Mark;
            AnsverControls = CreateControls();
            ChangeLocationControls();
            isChanged = false;
        }

        private void ChangeLocationControls()
        {
            Location location = new Location(QuestionTitle.Location.X,
               QuestionTitle.Location.Y + QuestionTitle.Size.Height);
            location.AddY(spaceSize);
            AnsverControls.ForEach(z =>
            {
                z.Location = location.GetNextPoint();
                location.AddControlY(z, 1);
            });


            location = new Location(0, location.GetNextPoint().Y);
            buttonAdd.Location = location.GetNextPoint();
            location.AddControlX(buttonAdd, 1);

            buttonDel.Location = location.GetNextPoint();
            location.AddControlX(buttonDel, 1);
            panelQuestionMark.Location = location.GetNextPoint();

            location.AddControlY(panelQuestionMark, 1);


            Height = location.GetNextPoint().Y;
        }

        #region Making controls


        private List<Control> CreateControls()
        {
            List<Control> ansverControls = new List<Control>();
            Ansvers.ForEach(x => ansverControls.Add(GetControl(x)));
            ansverControls.ForEach(x => Controls.Add(x));
            return ansverControls;
        }
        private EditAnsverContol GetControl(Answer answer)
        {
            EditAnsverContol result = new EditAnsverContol();
            result.Location = new System.Drawing.Point(0, 0);
            result.MaximumSize = new Size(Width, 0);
            result.Size = new System.Drawing.Size(Width, 28);
            result.SetAnsver(answer);
            result.Visible = true;
            return result;
        }
        #endregion

        private void ChangeQuestionText(string text)
        {
            int rowCount = TbRoCoutnt.getTBRowCountEQC(text, Width);
            this.QuestionTitle.Name = "Question";
            this.QuestionTitle.Height = rowCount * minHide;
            this.QuestionTitle.Text = text.Trim();
        }

        private void EditQuestionControl_ControlRemoved(object sender, ControlEventArgs e)
        {
            AnsverControls.Remove(e.Control);
            ChangeLocationControls();
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Answer x = EntityFactory.getDefultAnsver(Question.id);
            var AnsverControl = GetControl(x);
            this.Controls.Add(AnsverControl);
            AnsverControls.Add(AnsverControl);
            ChangeLocationControls();
        }

        private void Question_TextChanged(object sender, EventArgs e)
        {
            isChanged = true;
            Question.Text = QuestionTitle.Text;
        }
        #region Insert, Update, Delete
        public void SaveData()
        {
            List<Answer> saveList = new List<Answer>();
            List<Answer> updateList = new List<Answer>();
            List<Answer> deleteList = new List<Answer>();

            //Разделение вопросов на группы
            AnsverControls.ForEach(o =>
            {
                var z = o as EditAnsverContol;
                if (z.IsChanged)
                {
                    bool isUpdate = false;

                    Ansvers.ForEach(p =>
                    {
                        if (z.Ansver.Equals(p))
                        {
                            updateList.Add(z.Ansver);
                            isUpdate = true;

                        }
                    });
                    if (!isUpdate)
                        saveList.Add(z.Ansver);
                }
            });
            deleteList = GetDeleteList();

            //Сохранение, обновление, удаление
            Save(saveList);
            Update(updateList);
            Delete(deleteList);

            
        }
        private List<Answer> GetDeleteList()
        {
            List<Answer> deleteList;
            List<Answer> answersInControls = new List<Answer>();
            AnsverControls.ForEach(o =>
            {
                var z = o as EditAnsverContol;
                answersInControls.Add(z.Ansver);
            });
            deleteList = Ansvers.AsQueryable().Except(answersInControls).ToList();
            return deleteList;
        }

        private void Delete(List<Answer> deleteList)
        {
            using (StudentTestingEntities1 db = new StudentTestingEntities1())
            {

                deleteList.ForEach(o =>
                {
                    db.Answer.Attach(o);
                    db.Answer.Remove(o);
                });
                db.SaveChanges();
            }
        }

        private void Update(List<Answer> updateList)
        {
            using (StudentTestingEntities1 db = new StudentTestingEntities1())
            {

                try
                {
                    updateList.ForEach(o =>
                    {
                        var p = db.Answer.Where(z => z.Id == o.Id).FirstOrDefault();
                        p.isCorrect = o.isCorrect;
                        p.idQuestion = o.idQuestion;
                        p.Text = o.Text;
                        //db.Entry(p).State = EntityState.Modified;
                        
                    });
                    db.SaveChanges();
                }
                catch (Exception j)
                {
                    Console.Write(j.ToString());
                }
            }
        }

        private void Save(List<Answer> saveList)
        {
            using (StudentTestingEntities1 db = new StudentTestingEntities1())
            {

                saveList.ForEach(o =>
                {
                    o.idQuestion = Question.id;
                    db.Answer.Add(o);
                });
                db.SaveChanges();
            }
        }

        #endregion

        private void questionMark_ValueChanged(object sender, EventArgs e)
        {
            isChanged = true;
            Question.Mark = Convert.ToInt32(questionMark.Value);
        }

    }
}

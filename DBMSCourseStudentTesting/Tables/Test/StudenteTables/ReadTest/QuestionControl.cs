using DBMSCourseStudentTesting.Controls;
using DBMSCourseStudentTesting.Entityies;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DBMSCourseStudentTesting
{
    public partial class QuestionControl : UserControl
    {
        public Question QuestionT { get; private set; }
        public List<Answer> Ansvers { get; private set; }
        public List<AnsverControl> AnsverControls { get; private set; }

        public QuestionControl(Question question, List<Answer> ansvers)
        {
            QuestionT = question;
            Ansvers = ansvers;
            InitializeComponent();
        }

        const int minHide = 19;
        const int spaceSize = TbRoCoutnt.SPACE_SIZE;

        private void UserControl1_Load(object sender, EventArgs e)
        {
            string question = QuestionT.Text;
            ChangeQuestionText(question);
            AnsverControls = CreateControls();
            ChangeLocationControls();
        }
        private void ChangeLocationControls()
        {
            Location location = new Location(Question.Location.X,
                Question.Location.Y + Question.Size.Height);
            location.AddY(spaceSize);

            AnsverControls.ForEach(z =>
            {
                z.Location = location.GetNextPoint();
                location.AddControlY(z, 1);
            });
            Height = location.GetNextPoint().Y;

        }

        private void ChangeQuestionText(string text)
        {
            int rowCount = TbRoCoutnt.getTBRowCountEQC(text, Width);
            this.Question.Name = "Question";
            this.Question.Height = rowCount * minHide;
            this.Question.Text = text;
        }
        #region Making controls
        private List<AnsverControl> CreateControls()
        {
            List<AnsverControl> ansverControls = new List<AnsverControl>();
            Ansvers.ForEach(x => ansverControls.Add(GetControl(x)));
            ansverControls.ForEach(x => Controls.Add(x));
            return ansverControls;
        }
        private AnsverControl GetControl(Answer answer)
        {
            AnsverControl result = new AnsverControl();
            result.Location = new System.Drawing.Point(28, 63);
            result.MaximumSize = new Size(Width, 0);
            result.Size = new System.Drawing.Size(Width, 28);
            result.TBText = answer.Text;
            result.IsCorrect = answer.isCorrect;
            result.Visible = true;
            return result;
        }
        #endregion

    }
}

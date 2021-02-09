using DBMSCourseStudentTesting.Entityies;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBMSCourseStudentTesting.Controls
{
    class ControlsFactory
    {
        public static EditQuestionControl CreateQuestionControl(Question question, List<Answer> answers, int Width)
        {
            EditQuestionControl control = new EditQuestionControl(question, answers);
            control.Location = new System.Drawing.Point(28, 63);
            control.MaximumSize = new Size(Width, 0);
            control.Size = new System.Drawing.Size(Width, 28);
            control.Visible = true;
            return control;
        }

    }
}

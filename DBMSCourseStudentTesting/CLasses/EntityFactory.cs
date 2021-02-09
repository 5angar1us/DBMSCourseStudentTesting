using DBMSCourseStudentTesting.Entityies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBMSCourseStudentTesting.Controls
{
    class EntityFactory
    {
        public static Answer getDefultAnsver(int idQuestion)
        {
            Answer x = new Answer();
            x.Text = "текст ответа";
            x.idQuestion = idQuestion;
            x.isCorrect = false;
            return x;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBMSCourseStudentTesting.Controls
{
    class TbRoCoutnt
    {
        public const int SPACE_SIZE = 20;

        public static int getTBRowCountAC(string text, int MaxWith)
        {
            int maxRowSumbols = MaxWith / 6 - 30;
            int result = text.Length % maxRowSumbols != 0 ?
                (text.Length / maxRowSumbols) + 1
                : text.Length / maxRowSumbols;

            return result;
        }
        public static int getTBRowCountEQC(string text, int MaxWith)
        {
            int maxRowSumbols = MaxWith / 6;
            int result = text.Length % maxRowSumbols != 0 ?
                (text.Length / maxRowSumbols) + 1
                : text.Length / maxRowSumbols;

            return result;
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DBMSCourseStudentTesting.Controls;

namespace DBMSCourseStudentTesting
{
    public partial class AnsverControl : UserControl
    {

        public AnsverControl()
        {
            InitializeComponent();
            MinRowSize = 20;

        }

        public string TBText { get => TBAnswer.Text; set => TBAnswer.Text = value.Trim(); }

        public bool IsCorrect { set; get; }
        public bool CBCheecked { get => CB.Checked; set => CB.Checked = value; }

        public bool isGoodChoice()
        {
            return CB.Checked & IsCorrect | !CB.Checked & !IsCorrect;
        }
        public int MinRowSize { get; set; }

        private void TBAnswer_TextChanged(object sender, EventArgs e)
        {
            int height = TbRoCoutnt.getTBRowCountAC(TBAnswer.Text, MaximumSize.Width) * MinRowSize;

            TBAnswer.Height = height;
            Height = height + 5;
        }
    }
}

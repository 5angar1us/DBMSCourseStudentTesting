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

namespace DBMSCourseStudentTesting.Controls
{
    public partial class EditAnsverContol : UserControl
    {
        public EditAnsverContol()
        {
            InitializeComponent();
            MinRowSize = 20;
        }

        public string TBText { get => TBAnswer.Text; private set => TBAnswer.Text = value.Trim(); }

        public bool CBCheecked { get => CB.Checked; set => CB.Checked = value; }
        public Answer Ansver { private set; get; }
        public bool IsChanged { private set; get; }
        public int MinRowSize { get; set; }
        
        public void SetAnsver(Answer ansver)
        {
            Ansver = ansver;
            CB.Checked = ansver.isCorrect;
            TBText = ansver.Text;
            IsChanged = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var x = sender as Button;
            Dispose();
        }
        private void TBAnswer_TextChanged(object sender, EventArgs e)
        {
            ReSizeControl();
            Ansver.Text = TBText;
            IsChanged = true;
        }

        private void CB_CheckedChanged(object sender, EventArgs e)
        {
            Ansver.isCorrect = CB.Checked;
            IsChanged = true;
        }

        private void ReSizeControl()
        {
            TBAnswer.Height = TbRoCoutnt.getTBRowCountAC(TBText, MaximumSize.Width) * MinRowSize;
            
            Location location = new Location(CB.Location);
            location.AddControlX(CB, 0);
            TBAnswer.Location = location.GetNextPoint();
            location.AddControlY(TBAnswer, 1);

            location = new Location(TBAnswer.Width, location.GetNextPoint().Y);
            location.SubControlX(button1, 1);
            button1.Location = location.GetNextPoint();
            location.AddControlY(button1, 0);

            Height = location.GetNextPoint().Y;
        }
    }
}


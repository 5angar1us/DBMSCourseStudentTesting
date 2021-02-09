using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBMSCourseStudentTesting
{
    class Location
    {
        public int X { private set; get; }
        public int Y { private set; get; }

        const int spaceSize = 20;
        public Location(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Location(Point point)
        {
            X = point.X;
            Y = point.Y;
        }

        public void AddX(int x)
        {
            X += x;
        }
        public void AddY(int y)
        {
            Y += y;
        }

        public void AddControlX(Control control, int spaceSizeCount)
        {
            X += control.Width + spaceSize * spaceSizeCount;
        }
        public void AddControlY(Control control, int spaceSizeCount)
        {
            Y += control.Height + spaceSize * spaceSizeCount;
        }
        public void AddControl(Control control, int spaceSizeCount)
        {
            Y += control.Height + spaceSize * spaceSizeCount;
            X += control.Width + spaceSize * spaceSizeCount;
        }

        public void SubControlX(Control control, int spaceSizeCount)
        {
            X -= control.Width + spaceSize * spaceSizeCount;
        }
        public void SubControlY(Control control, int spaceSizeCount)
        {
            Y -= control.Height + spaceSize * spaceSizeCount;
        }
        public void SubControl(Control control, int spaceSizeCount)
        {
            Y -= control.Height + spaceSize * spaceSizeCount;
            X -= control.Width + spaceSize * spaceSizeCount;
        }

        public Point GetNextPoint()
        {
            return new Point(X, Y);
        }

    }
}

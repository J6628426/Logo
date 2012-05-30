using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace LOGO.Model
{
    public interface ITurtle
    {
        Color BackColor
        {
            get;
            set;
        }

        Color ForeColor
        {
            get;
            set;
        }

        Image Image
        {
            get;
        }

        Point Location
        {
            get;
            set;
        }

        float PenWidth
        {
            get;
            set;
        }
    }
}

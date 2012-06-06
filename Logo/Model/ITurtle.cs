using System;
using System.Drawing;
using SdlDotNet.Graphics;

namespace LOGO.Model
{
    public interface ITurtle : IDisposable
    {
        void Clear();
        void Reset();

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

        bool PenDown
        {
            get;
            set;
        }

        float PenWidth
        {
            get;
            set;
        }

        Size Size
        {
            get;
            set;
        }

        Surface Surface
        {
            get;
        }
    }
}

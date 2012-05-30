using System;
using LOGO.Model;
using System.Drawing;

namespace LOGO.View
{
    public interface IView
    {
        void Draw(Image image)
        {

        }

        void Print(string text)
        {

        }

        event ViewClosingEventHandler Closing;
        event ViewInputEventHandler Input;
    }
}

using System;
using LOGO.Model;
using System.Drawing;

namespace LOGO.View
{
    public interface IView : IDisposable
    {
        void Clear();
        void Draw(ITurtle turtle);
        void Print(string text);
        void Show();

        event ViewClosingEventHandler Closing;
        event ViewInputEventHandler Input;
    }
}

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LOGO.View
{
    public partial class FormLog : Form, IView
    {
        #region variables

        private delegate void ClearDelegate();
        private delegate void PrintDelegate(string text);

        private ViewClosingEventHandler _closing;
        private ViewInputEventHandler _input;
        private ClearDelegate _clear;
        private PrintDelegate _print;

        #endregion variables


        #region constructors

        public FormLog()
        {
            InitializeComponent();

            _clear = new ClearDelegate(Clear);
            _print = new PrintDelegate(Print);
        }

        #endregion constructors


        #region methods

        public void Clear()
        {
            if (InvokeRequired)
            {
                Invoke(_clear);
                return;
            }

            textLog.Clear();
        }

        public void Draw(Model.ITurtle turtle)
        {
        }

        public void Print(string text)
        {
            if (InvokeRequired)
            {
                Invoke(_print, text);
                return;
            }

            textLog.AppendText(text);
            textLog.AppendText("\r\n");
        }

        #endregion methods


        #region properties

        public new event ViewClosingEventHandler Closing
        {
            add { _closing += value; }
            remove { _closing -= value; }
        }

        public event ViewInputEventHandler Input
        {
            add { _input += value; }
            remove { _input -= value; }
        }

        #endregion properties
    }
}

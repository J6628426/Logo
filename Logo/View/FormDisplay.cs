using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LOGO.View
{
    public partial class FormDisplay : Form, IView
    {
        #region variables

        private event ViewClosingEventHandler _closing;
        private event ViewInputEventHandler _input;

        #endregion variables


        #region constructors

        public FormDisplay()
        {
            InitializeComponent();

            base.FormClosing += new FormClosingEventHandler(ViewClosing);
            textInput.KeyDown += new KeyEventHandler(InputKeyDown);
        }

        #endregion constructors


        #region methods

        public void Draw(Image image)
        {
            if (image == null) return;


        }

        private void InputKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    string text = textInput.Text.Trim();
                    if ((text.Length > 1) && (_input != null))
                    {
                        _input.Invoke(this, new ViewInputEventArgs(text));
                    }
                    // drop through to escape case...
                case Keys.Escape:
                    textInput.Clear();
                    break;
            }
        }

        public void Print(string text)
        {
        }

        private void ViewClosing(object sender, FormClosingEventArgs e)
        {
            if (_closing != null)
            {
                ViewClosingEventArgs args = new ViewClosingEventArgs();

                _closing.Invoke(this, args);

                e.Cancel = args.Cancel;
            }
        }

        #endregion methods


        #region properties
        
        public event ViewClosingEventHandler IView.Closing
        {
            add { _closing += value; }
            remove { _closing -= value; }
        }

        public event ViewInputEventHandler IView.Input
        {
            add { _input += value; }
            remove { _input -= value; }
        }

        #endregion properties
    }
}

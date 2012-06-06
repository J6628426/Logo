using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LOGO.Model;

namespace LOGO.View
{
    public partial class FormDisplay : Form, IView
    {
        #region variables

        private delegate void ClearDelegate();
        private delegate void DrawDelegate(ITurtle turtle);
        private delegate void PrintDelegate(string text);

        private ClearDelegate _clear;
        private DrawDelegate _draw;
        private PrintDelegate _print;
        private event ViewClosingEventHandler _closing;
        private event ViewInputEventHandler _input;

        #endregion variables


        #region constructors

        public FormDisplay()
        {
            InitializeComponent();

            base.FormClosing += new FormClosingEventHandler(ViewClosing);
            base.Load += new EventHandler(FormLoad);
            textInput.KeyDown += new KeyEventHandler(InputKeyDown);

            _clear = new ClearDelegate(Clear);
            _draw = new DrawDelegate(Draw);
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

            textInput.Clear();
            pictureTurtle.Image = null;
        }

        public void Draw(ITurtle turtle)
        {
            if (InvokeRequired)
            {
                Invoke(_draw, turtle);
                return;
            }

            if (turtle == null) return;

            Image buffer = turtle.Image;

            Graphics graphics = null;
            // get existing drawing surface
            Bitmap bitmap = (Bitmap)pictureTurtle.Image;
            // if surface exists, then test size
            if (bitmap != null)
            {
                if ((bitmap.Width < buffer.Width + 16) || (bitmap.Height < buffer.Height + 16)) 
                {
                    bitmap.Dispose();
                    bitmap = null;
                }
            }
            // if new image needed, then create
            if (bitmap == null) bitmap = new Bitmap(buffer.Width + 16, buffer.Height + 16, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            graphics = Graphics.FromImage(bitmap);
            graphics.Clear(pictureTurtle.BackColor);
            graphics.DrawImageUnscaled(buffer, 8, 8, buffer.Width, buffer.Height);
            graphics.DrawImage(Properties.Resources.Turtle, turtle.Location.X, turtle.Location.Y);

            // cleanup memory
            graphics.Dispose();
            graphics = null;

            pictureTurtle.Image = bitmap;

            // ensure form is large enough to accomodate image.
            if (Width < bitmap.Width + 21) Width = bitmap.Width + 21;
            if (Height < bitmap.Height + 92) Height = bitmap.Height + 92;
            Application.DoEvents();
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
                    textInput.Clear();
                    break;
                case Keys.Escape:
                    textInput.Clear();
                    break;
            }
        }

        private void FormLoad(object sender, EventArgs e)
        {
            Show();
            Application.DoEvents();

            string[] args = Environment.GetCommandLineArgs();
            if (args.Length < 2) return;

            try
            {
                LOGO.Controller.Conductor.GetInstance().Load(args[1]);
                Application.DoEvents();

                LOGO.Controller.Conductor.GetInstance().Perform(new Model.Action.Move());
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "LOGO Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void menuLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.AddExtension = true;
            dialog.AutoUpgradeEnabled = true;
            dialog.DefaultExt = ".xml";
            dialog.Filter = "LOGO XML File|*.xml|All Files|*.*";
            dialog.FilterIndex = 1;
            dialog.SupportMultiDottedExtensions = true;
            dialog.Title = "Load document...";

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    LOGO.Controller.Conductor.GetInstance().Load(dialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "LOGO Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            dialog.Dispose();
            dialog = null;
        }

        private void menuSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.AddExtension = true;
            dialog.AutoUpgradeEnabled = true;
            dialog.DefaultExt = ".xml";
            dialog.Filter = "LOGO XML File|*.xml|All Files|*.*";
            dialog.FilterIndex = 1;
            dialog.OverwritePrompt = true;
            dialog.SupportMultiDottedExtensions = true;
            dialog.Title = "Save document to...";

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                LOGO.Controller.Conductor.GetInstance().SaveAs(dialog.FileName);
            }

            dialog.Dispose();
            dialog = null;
        }

        public void Print(string text)
        {
            if (InvokeRequired)
            {
                Invoke(_print, text);
                return;
            }

            textInput.Text = text;
            textInput.SelectAll();
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
        
        public event ViewClosingEventHandler Closing
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

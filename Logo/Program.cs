using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using LOGO.Controller;
using LOGO.Model;
using System.Xml.Serialization;
using System.IO;
using LOGO.Model.Action;
using LOGO.View;

namespace LOGO
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form main = new View.FormDisplay();

            Conductor conductor = Conductor.GetInstance();
            conductor.AddView((IView)main);
            conductor.AddView(new View.FormLog());

            conductor.Show(args == null || args.Length < 1? null : args[0]);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using LOGO.Controller;

namespace LOGO
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Conductor conductor = Conductor.GetInstance();
            conductor.AddView(new View.FormDisplay());

            conductor.Show();
        }
    }
}

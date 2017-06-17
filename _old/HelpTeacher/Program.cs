using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using HelpTeacher.Forms;
using HelpTeacher.Classes;

namespace HelpTeacher
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
            Principal loga = new Principal();
            if (loga.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new Principal());                
            }
            else
            {
                Application.Exit();
            }
        }
    }
}

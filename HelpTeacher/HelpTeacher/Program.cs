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
            Login loga = new Login();
            if (loga.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new Principal());

                /* Depois que a aplicação for encerrada, verifica se
                 * o usuário deseja para o banco de dados
                 */
                if (Usuario.StopBD.Equals("*"))
                {
                    ConexaoBanco banco = new ConexaoBanco();
                    banco.stopBanco();
                }
            }
            else
            {
                Application.Exit();
            }
        }
    }
}

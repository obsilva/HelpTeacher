using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpTeacher.Classes
{
    public static class Usuario
    {
        private static int identificador;
        private static String nome;
        private static String senha;
        private static String alterarSenha;
        private static String paraBanco;

        public static int ID
        {
            get { return identificador; }
            set { identificador = value; }
        }

        public static String Login
        {
            get { return nome; }
            set { nome = value; }
        }

        public static String Password
        {
            get { return senha; }
            set { senha = value; }
        }

        public static String ChancePassword
        {
            get { return alterarSenha; }
            set { alterarSenha = value; }
        }

        public static String StopBD
        {
            get { return paraBanco; }
            set { paraBanco = value; }
        }
    }
}

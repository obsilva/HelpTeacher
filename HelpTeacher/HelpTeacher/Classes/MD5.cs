using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace HelpTeacher.Classes
{
    public static class MD5
    {
        public static String gerarHash(String password)
        {
            StringBuilder sb = new StringBuilder();
            byte[] tmpSource;
            byte[] tmpHash;

            tmpSource = ASCIIEncoding.ASCII.GetBytes(password);  //Converte string de origem em uma matriz de bytes
            tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);  //Calcula o hash MD5
            for (int cont = 0; cont < tmpHash.Length; cont++)
            {
                sb.Append(tmpHash[cont].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}

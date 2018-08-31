using System.Security.Cryptography;
using System.Text;

namespace HelpTeacher.Classes
{
	public static class MD5
	{
		public static string gerarHash(string password)
		{
			var sb = new StringBuilder();
			byte[] tmpSource;
			byte[] tmpHash;

			tmpSource = Encoding.ASCII.GetBytes(password);  //Converte string de origem em uma matriz de bytes
			tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);  //Calcula o hash MD5
			for (int cont = 0; cont < tmpHash.Length; cont++)
			{
				sb.Append(tmpHash[cont].ToString("X2"));
			}
			return sb.ToString();
		}
	}
}

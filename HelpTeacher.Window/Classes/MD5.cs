// This Source Code Form is subject to the terms of the Mozilla 
// Public License, v. 2.0. If a copy of the MPL was not distributed 
// with this file, You can obtain one at https://mozilla.org/MPL/2.0/

// Authors: 
//		Otávio Bueno Silva <obsilva94@gmail.com>

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

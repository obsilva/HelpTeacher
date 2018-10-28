// This Source Code Form is subject to the terms of the Mozilla 
// Public License, v. 2.0. If a copy of the MPL was not distributed 
// with this file, You can obtain one at https://mozilla.org/MPL/2.0/

// Since: 2018/10/27
// Authors: 
//		Otávio Bueno Silva <obsilva94@gmail.com>

using System;

namespace HelpTeacher.Util
{
	/// <summary>
	/// Implementa métodos que servem para testar determinadas condições. Uma exceção é lançada
	/// sempre que uma condição indesejada ocorrer.
	/// </summary>
	/// <remarks>
	/// Geralmente é usado para verificar os parâmetros passados nas chamadas das funções.
	/// </remarks>
	public static class Checker
	{
		/// <summary>Checa se o objecto é <see langword="null"/>.</summary>
		/// <param name="obj">Objeto a ser verificado</param>
		/// <param name="paramName">Nome do parâmetro de onde o objeto veio.</param>
		/// <exception cref="ArgumentNullException">Lançada quando o objeto for <see langword="null"/>.</exception>
		public static void NullObject(object obj, string paramName)
		{
			if (obj == null)
			{
				throw new ArgumentNullException(paramName, "Parâmetro obrigatório.");
			}
		}

		/// <summary>
		/// Checa se a string é <see langword="null"/>, empty ou formada apenas por espaços em branco.
		/// </summary>
		/// <param name="str">Valor a ser verificado.</param>
		/// <param name="paramName">Nome do parâmetro de onde o valor veio.</param>
		/// <exception cref="ArgumentNullException">
		/// Lançada quando for <see langword="null"/> ou <see cref="String.Empty"/>.
		/// </exception>
		public static void NullOrEmpty(string str, string paramName)
		{
			if (String.IsNullOrWhiteSpace(str))
			{
				throw new ArgumentNullException(paramName, "Parâmetro obrigatório.");
			}
		}

		/// <summary>Checa se <see cref="String.Length"/> é maior que o permitido.</summary>
		/// <param name="str">Valor a ser verificado.</param>
		/// <param name="paramName">Nome do parâmetro de onde o valor veio.</param>
		/// <param name="maxLength">Comprimento máximo da string.</param>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Lançada quando <see cref="String.Length"/> for maior que o permitido.
		/// </exception>
		public static void StringLength(string str, string paramName, int maxLength)
		{
			string message = $"Comprimento máximo permitido: {maxLength} caracteres.";

			StringLength(str, paramName, maxLength, 0, message);
		}

		// <summary>Checa se <see cref="String.Length"/> está entre o mínimo e máximo permitidos.</summary>
		/// <param name="str">Valor a ser verificado.</param>
		/// <param name="paramName">Nome do parâmetro de onde o valor veio.</param>
		/// <param name="maxLength">Comprimento máximo permitido.</param>
		/// <param name="minLength">Comprimento mínimo exigido.</param>
		/// /// <exception cref="ArgumentOutOfRangeException">
		/// Lançada quando <see cref="String.Length"/> for menor ou maior que o permitido.
		/// </exception>
		public static void StringLength(string str, string paramName, int maxLength, int minLength)
		{
			string message = $"Comprimento mínimo exigido: {minLength} caracteres. " +
							 $"Comprimento máximo permitido: {maxLength} caracteres.";

			StringLength(str, paramName, maxLength, minLength, message);
		}

		/// <summary>Checa se <see cref="String.Length"/> está entre o mínimo e máximo permitidos.</summary>
		/// <param name="str">Valor a ser verificado.</param>
		/// <param name="paramName">Nome do parâmetro de onde o valor veio.</param>
		/// <param name="maxLength">Comprimento máximo permitido.</param>
		/// <param name="minLength">Comprimento mínimo exigido.</param>
		/// <param name="errorMessage">Mensagem erro que deve ser exibida caso a exceção seja lançada.</param>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Lançada quando <see cref="String.Length"/> for menor ou maior que o permitido.
		/// </exception>
		public static void StringLength(string str, string paramName, int maxLength, int minLength, string errorMessage)
		{
			str = String.IsNullOrWhiteSpace(str) ? String.Empty : str;

			if ((str.Length < minLength) || (str.Length > maxLength))
			{
				throw new ArgumentOutOfRangeException(paramName, str.Length, errorMessage);
			}
		}
	}
}

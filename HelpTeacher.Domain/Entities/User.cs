// This Source Code Form is subject to the terms of the Mozilla 
// Public License, v. 2.0. If a copy of the MPL was not distributed 
// with this file, You can obtain one at https://mozilla.org/MPL/2.0/

// Since: 2018/08/18
// Authors: 
//		Otávio Bueno Silva <obsilva94@gmail.com>

using System;

namespace HelpTeacher.Domain.Entities
{
	/// <summary>Define a entidade usuário.</summary>
	public sealed class User : IEntityBase
	{
		#region Properties
		/// <summary>Recupera a instanância única de usuário.</summary>
		public static User Instance { get; } = new User();

		/// <inheritdoc />
		public bool IsRecordActive { get; set; }

		/// <inheritdoc />
		public bool IsNull => Equals(Null);

		/// <summary>Determina se o usuário deve alterar a senha.</summary>
		public bool MustChangePassword { get; set; }

		/// <summary>Recupera uma nova instância vazia, considerada <see langword="null"/>.</summary>
		/// <remarks>A instância vazia pode ser considerada um objeto padrão.</remarks>
		/// <returns>Nova instância vazia.</returns>
		public static User Null => new User()
		{
			IsRecordActive = false,
			MustChangePassword = false,
			Password = String.Empty,
			RecordID = -1,
			Username = String.Empty
		};

		/// <summary>Senha utilizada para ter acesso ao sistema.</summary>
		/// <remarks>A senha é armazenada criptografada.</remarks>
		public string Password { get; set; }

		/// <inheritdoc />
		public int RecordID { get; set; }

		/// <summary>Nome de usuário utilizado para ter acesso ao sistema.</summary>
		public string Username { get; set; }
		#endregion


		#region Constructors
		static User() { }

		public User() { }
		#endregion
	}
}
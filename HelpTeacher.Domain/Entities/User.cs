// This Source Code Form is subject to the terms of the Mozilla 
// Public License, v. 2.0. If a copy of the MPL was not distributed 
// with this file, You can obtain one at https://mozilla.org/MPL/2.0/

// Since: 2018/08/18
// Authors: 
//		Otávio Bueno Silva <obsilva94@gmail.com>

using System;
using System.Collections.Generic;

namespace HelpTeacher.Domain.Entities
{
	/// <summary>Define a entidade usuário.</summary>
	public sealed class User : IEntityBase, IEquatable<User>
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


		#region Methods
		/// <summary>Determina se os dois usuários especificados possuem valores iguais.</summary>
		/// <param name="user1">O primeiro usuários para comparar, ou <see langword="null"/>.</param>
		/// <param name="user2">O segundo usuários para comparar, ou <see langword="null"/>.</param>
		/// <returns>
		/// <see langword="true"/> se os valores em <paramref name="user1"/> forem iguais que
		/// em <paramref name="user2"/>; <see langword="false"/> caso contrário.
		/// </returns>
		public static bool operator ==(User user1, User user2)
			=> EqualityComparer<User>.Default.Equals(user1, user2);

		/// <summary>Determina se os dois usuários especificados possuem valores diferentes.</summary>
		/// <param name="user1">O primeiro usuários para comparar, ou <see langword="null"/>.</param>
		/// <param name="user2">O segundo usuários para comparar, ou <see langword="null"/>.</param>
		/// <returns>
		/// <see langword="true"/> se os valores em <paramref name="user1"/> forem diferentes que
		/// em <paramref name="user2"/>; <see langword="false"/> caso contrário.
		/// </returns>
		public static bool operator !=(User user1, User user2)
			=> !(user1 == user2);

		/// <inheritdoc />
		public bool Equals(User other)
		{
			if (other == null)
			{
				return false;
			}

			if (RecordID != other.RecordID)
			{
				return false;
			}

			if (IsRecordActive != other.IsRecordActive)
			{
				return false;
			}

			if (MustChangePassword != other.MustChangePassword)
			{
				return false;
			}

			if (Password != other.Password)
			{
				return false;
			}

			if (Username != other.Username)
			{
				return false;
			}

			return true;
		}
		#endregion


		#region Overrides
		/// <inheritdoc />
		public override bool Equals(object obj)
			=> Equals(obj as User);

		/// <inheritdoc />
		public override int GetHashCode()
		{
			int hashCode = -1887621623;
			hashCode = (hashCode * -1521134295) + IsRecordActive.GetHashCode();
			hashCode = (hashCode * -1521134295) + MustChangePassword.GetHashCode();
			hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(Password);
			hashCode = (hashCode * -1521134295) + RecordID.GetHashCode();
			hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(Username);
			return hashCode;
		}
		#endregion
	}
}
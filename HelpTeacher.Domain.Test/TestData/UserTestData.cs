// This Source Code Form is subject to the terms of the Mozilla 
// Public License, v. 2.0. If a copy of the MPL was not distributed 
// with this file, You can obtain one at https://mozilla.org/MPL/2.0/

// Since: 2018/11/02
// Authors: 
//		Otávio Bueno Silva <obsilva94@gmail.com>

using System.Collections.Generic;
using System.Linq;

using HelpTeacher.Domain.Entities;

namespace HelpTeacher.Domain.Test.TestData
{
	/// <summary>Repositório de dados de teste.</summary>
	public class UserTestData
	{
		#region Properties
		/// <summary>Recupera o número total de registros em <see cref="GetList"/>.</summary>
		public static int Count => GetList().Count();

		/// <summary>Recupera o número de registros ativos em <see cref="GetList"/>.</summary>
		public static int CountActiveRecords => GetList().Count(item => item.IsRecordActive);

		/// <summary>Recupera o número de registros inativos em <see cref="GetList"/>.</summary>
		public static int CountInactiveRecords => GetList().Count(item => item.IsRecordActive == false);

		/// <summary>Recupera o primeiro registro em <see cref="GetList"/>.</summary>
		public static User First => GetList().First();

		/// <summary>Recupera o último registro em <see cref="GetList"/>.</summary>
		public static User Last => GetList().Last();
		#endregion


		#region Methods
		/// <summary>Recupera uma enumeração contendo objetos para teste.</summary>
		/// <returns>Enumeração com objetos para teste.</returns>
		public static List<User> GetList()
			=> new List<User>()
			{
				new User() {RecordID = 3, IsRecordActive = true, MustChangePassword = true, Username = "username 1", Password = "password 1"},
				new User() {RecordID = 4, IsRecordActive = true, MustChangePassword = true, Username = "username 2", Password = "password 2"},
				new User() {RecordID = 5, IsRecordActive = true, MustChangePassword = true, Username = "username 3", Password = "password 3"},
				new User() {RecordID = 6, IsRecordActive = true, MustChangePassword = true, Username = "username 4", Password = "password 4"},
				new User() {RecordID = 7, IsRecordActive = true, MustChangePassword = true, Username = "username 5", Password = "password 5"},
				new User() {RecordID = 8, IsRecordActive = true, MustChangePassword = false, Username = "username 6", Password = "password 6"},
				new User() {RecordID = 9, IsRecordActive = true, MustChangePassword = false, Username = "username 7", Password = "password 7"},
				new User() {RecordID = 10, IsRecordActive = true, MustChangePassword = false, Username = "username 8", Password = "password 8"},
				new User() {RecordID = 11, IsRecordActive = true, MustChangePassword = false, Username = "username 9", Password = "password 9"},
				new User() {RecordID = 12, IsRecordActive = true, MustChangePassword = false, Username = "username 10", Password = "password 10"},
			};
		#endregion
	}
}

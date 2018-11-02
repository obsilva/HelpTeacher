// This Source Code Form is subject to the terms of the Mozilla 
// Public License, v. 2.0. If a copy of the MPL was not distributed 
// with this file, You can obtain one at https://mozilla.org/MPL/2.0/

// Since: 2018/10/25
// Authors: 
//		Otávio Bueno Silva <obsilva94@gmail.com>

using System.Collections.Generic;
using System.Linq;

using HelpTeacher.Domain.Entities;

namespace HelpTeacher.Domain.Test.TestData
{
	/// <summary>Repositório de dados de teste.</summary>
	public static class CourseTestData
	{
		#region Properties
		/// <summary>Recupera o número total de registros em <see cref="GetList"/>.</summary>
		public static int Count => GetList().Count();

		/// <summary>Recupera o número de registros ativos em <see cref="GetList"/>.</summary>
		public static int CountActiveRecords => GetList().Count(item => item.IsRecordActive);

		/// <summary>Recupera o número de registros inativos em <see cref="GetList"/>.</summary>
		public static int CountInactiveRecords => GetList().Count(item => item.IsRecordActive == false);

		/// <summary>Recupera o primeiro registro em <see cref="GetList"/>.</summary>
		public static Course First => GetList().First();

		/// <summary>Recupera o último registro em <see cref="GetList"/>.</summary>
		public static Course Last => GetList().Last();
		#endregion


		#region Methods
		/// <summary>
		/// Recupera uma enumeração contendo objetos para teste.
		/// <remarks>
		/// <para> 
		/// * Registros com RecordID impar estão ativos, enquanto aqueles com RecordID par estão inativos.
		/// </para>
		/// </remarks>
		/// </summary>
		/// <returns>Enumeração com objetos para teste.</returns>
		public static List<Course> GetList()
			=> new List<Course>()
			{
				new Course("Course 1") {RecordID = 1, IsRecordActive = true},
				new Course("Course 2") {RecordID = 2, IsRecordActive = true},
				new Course("Course 3") {RecordID = 3, IsRecordActive = true},
				new Course("Course 4") {RecordID = 4, IsRecordActive = true},
				new Course("Course 5") {RecordID = 5, IsRecordActive = true},
				new Course("Course 6") {RecordID = 6, IsRecordActive = false},
				new Course("Course 7") {RecordID = 7, IsRecordActive = false},
				new Course("Course 8") {RecordID = 8, IsRecordActive = false},
				new Course("Course 9") {RecordID = 9, IsRecordActive = false},
				new Course("Course 10") {RecordID = 10, IsRecordActive = false}
			};
		#endregion
	}
}

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
	public static class DisciplineTestData
	{
		#region Properties
		/// <summary>Lista com <see cref="Course"/>s usados na geração das disciplinas de teste.</summary>
		public static IEnumerable<Course> Courses { get; } = CourseTestData.GetList();

		/// <summary>Recupera o número total de registros em <see cref="GetList"/>.</summary>
		public static int Count => GetList().Count();

		/// <summary>Recupera o número de registros ativos em <see cref="GetList"/>.</summary>
		public static int CountActiveRecords => GetList().Count(item => item.IsRecordActive);

		/// <summary>Recupera o número de registros inativos em <see cref="GetList"/>.</summary>
		public static int CountInactiveRecords => GetList().Count(item => item.IsRecordActive == false);

		/// <summary>Recupera o primeiro registro em <see cref="GetList"/>.</summary>
		public static Discipline First => GetList().First();

		/// <summary>Recupera o último registro em <see cref="GetList"/>.</summary>
		public static Discipline Last => GetList().Last();
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
		public static List<Discipline> GetList()
			=> new List<Discipline>()
			{
				new Discipline(Courses.ElementAt(0), "Discipline 1") { RecordID = 1, IsRecordActive = true },
				new Discipline(Courses.ElementAt(1), "Discipline 2") { RecordID = 2, IsRecordActive = true },
				new Discipline(Courses.ElementAt(2), "Discipline 3") { RecordID = 3, IsRecordActive = true },
				new Discipline(Courses.ElementAt(3), "Discipline 4") { RecordID = 4, IsRecordActive = true },
				new Discipline(Courses.ElementAt(4), "Discipline 5") { RecordID = 5, IsRecordActive = true },
				new Discipline(Courses.ElementAt(5), "Discipline 6") { RecordID = 6, IsRecordActive = false },
				new Discipline(Courses.ElementAt(6), "Discipline 7") { RecordID = 7, IsRecordActive = false },
				new Discipline(Courses.ElementAt(7), "Discipline 8") { RecordID = 8, IsRecordActive = false },
				new Discipline(Courses.ElementAt(8), "Discipline 9") { RecordID = 9, IsRecordActive = false },
				new Discipline(Courses.ElementAt(9), "Discipline 10") { RecordID = 10, IsRecordActive = false }
			};
		#endregion
	}
}

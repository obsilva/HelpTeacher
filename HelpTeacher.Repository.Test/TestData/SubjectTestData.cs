// This Source Code Form is subject to the terms of the Mozilla 
// Public License, v. 2.0. If a copy of the MPL was not distributed 
// with this file, You can obtain one at https://mozilla.org/MPL/2.0/

// Since: 2018/10/28
// Authors: 
//		Otávio Bueno Silva <obsilva94@gmail.com>

using System.Collections.Generic;
using System.Linq;

using HelpTeacher.Domain.Entities;

namespace HelpTeacher.Repository.Test.TestData
{
	/// <summary>Repositório de dados de teste.</summary>
	public static class SubjectTestData
	{
		#region Properties
		/// <summary>Lista com <see cref="Discipline"/>s usados na geração dos assuntos de teste.</summary>
		public static IEnumerable<Discipline> Disciplines { get; } = DisciplineTestData.GetList();

		/// <summary>Recupera o número total de registros em <see cref="GetList"/>.</summary>
		public static int Count => GetList().Count();

		/// <summary>Recupera o número de registros ativos em <see cref="GetList"/>.</summary>
		public static int CountActiveRecords => GetList().Count(item => item.IsRecordActive);

		/// <summary>Recupera o número de registros inativos em <see cref="GetList"/>.</summary>
		public static int CountInactiveRecords => GetList().Count(item => item.IsRecordActive == false);

		/// <summary>Recupera o primeiro registro em <see cref="GetList"/>.</summary>
		public static Subject First => GetList().First();

		/// <summary>Recupera o último registro em <see cref="GetList"/>.</summary>
		public static Subject Last => GetList().Last();
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
		public static IEnumerable<Subject> GetList()
			=> new List<Subject>()
			{
				new Subject(Disciplines.ElementAt(0), "Subject 1") { RecordID = 1, IsRecordActive = true },
				new Subject(Disciplines.ElementAt(1), "Subject 2") { RecordID = 2, IsRecordActive = false },
				new Subject(Disciplines.ElementAt(2), "Subject 3") { RecordID = 3, IsRecordActive = true },
				new Subject(Disciplines.ElementAt(3), "Subject 4") { RecordID = 4, IsRecordActive = false },
				new Subject(Disciplines.ElementAt(4), "Subject 5") { RecordID = 5, IsRecordActive = true },
				new Subject(Disciplines.ElementAt(5), "Subject 6") { RecordID = 6, IsRecordActive = false }
			};
		#endregion
	}
}

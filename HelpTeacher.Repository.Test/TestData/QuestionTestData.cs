using System.Collections.Generic;
using System.Linq;

using HelpTeacher.Domain.Entities;

namespace HelpTeacher.Repository.Test.TestData
{
	/// <summary>Repositório de dados de teste.</summary>
	public class QuestionTestData
	{
		#region Properties
		/// <summary>Lista com <see cref="Subject"/>s usados na geração das questões de teste.</summary>
		public static IEnumerable<Subject> Subjects { get; } = SubjectTestData.GetList();

		/// <summary>Recupera o número total de registros em <see cref="GetList"/>.</summary>
		public static int Count => GetList().Count();

		/// <summary>Recupera o número de registros ativos em <see cref="GetList"/>.</summary>
		public static int CountActiveRecords => GetList().Count(item => item.IsRecordActive);

		/// <summary>Recupera o número de registros inativos em <see cref="GetList"/>.</summary>
		public static int CountInactiveRecords => GetList().Count(item => item.IsRecordActive == false);

		/// <summary>Recupera o primeiro registro em <see cref="GetList"/>.</summary>
		public static Question First => GetList().First();

		/// <summary>Recupera o último registro em <see cref="GetList"/>.</summary>
		public static Question Last => GetList().Last();
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
		public static IEnumerable<Question> GetList()
			=> new List<Question>()
			{
				new Question(Subjects.ElementAt(0), "Question statement 1")
					{ RecordID = 1, IsRecordActive = true, IsObjective = true, IsDefault = true, WasUsed = true},
				new Question(Subjects.ElementAt(1), "Question statement 2")
					{ RecordID = 2, IsRecordActive = true, IsObjective = true, IsDefault = true, WasUsed = true},
				new Question(Subjects.ElementAt(2), "Question statement 3")
					{ RecordID = 3, IsRecordActive = true, IsObjective = true, IsDefault = true, WasUsed = true},
				new Question(Subjects.ElementAt(3), "Question statement 4")
					{ RecordID = 4, IsRecordActive = true, IsObjective = true, IsDefault = true, WasUsed = true},
				new Question(Subjects.ElementAt(4), "Question statement 5")
					{ RecordID = 5, IsRecordActive = true, IsObjective = true, IsDefault = true, WasUsed = true},
				new Question(Subjects.ElementAt(5), "Question statement 6")
					{ RecordID = 6, IsRecordActive = false, IsObjective = false, IsDefault = false, WasUsed = false},
				new Question(Subjects.ElementAt(6), "Question statement 7")
					{ RecordID = 7, IsRecordActive = false, IsObjective = false, IsDefault = false, WasUsed = false},
				new Question(Subjects.ElementAt(7), "Question statement 8")
					{ RecordID = 8, IsRecordActive = false, IsObjective = false, IsDefault = false, WasUsed = false},
				new Question(Subjects.ElementAt(8), "Question statement 9")
					{ RecordID = 9, IsRecordActive = false, IsObjective = false, IsDefault = false, WasUsed = false},
				new Question(Subjects.ElementAt(9), "Question statement 10")
					{ RecordID = 10, IsRecordActive = false, IsObjective = false, IsDefault = false, WasUsed = false}
			};
		#endregion
	}
}

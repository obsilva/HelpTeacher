// This Source Code Form is subject to the terms of the Mozilla 
// Public License, v. 2.0. If a copy of the MPL was not distributed 
// with this file, You can obtain one at https://mozilla.org/MPL/2.0/

// Since: 2018/11/02
// Authors: 
//		Otávio Bueno Silva <obsilva94@gmail.com>

using System;
using System.Collections.Generic;
using System.Linq;

using HelpTeacher.Domain.Entities;

namespace HelpTeacher.Domain.Test.TestData
{
	public class ExamTestData
	{
		#region Properties
		/// <summary>Lista com <see cref="Subject"/>s usados na geração das questões de teste.</summary>
		public static IEnumerable<Question> Questions { get; } = QuestionTestData.GetList();

		/// <summary>Recupera o número total de registros em <see cref="GetList"/>.</summary>
		public static int Count => GetList().Count();

		/// <summary>Recupera o número de registros ativos em <see cref="GetList"/>.</summary>
		public static int CountActiveRecords => GetList().Count(item => item.IsRecordActive);

		/// <summary>Recupera o número de registros inativos em <see cref="GetList"/>.</summary>
		public static int CountInactiveRecords => GetList().Count(item => item.IsRecordActive == false);

		/// <summary>Recupera o primeiro registro em <see cref="GetList"/>.</summary>
		public static Exam First => GetList().First();

		/// <summary>Recupera o último registro em <see cref="GetList"/>.</summary>
		public static Exam Last => GetList().Last();
		#endregion


		#region Methods
		/// <summary>Recupera uma enumeração contendo objetos para teste.</summary>
		/// <returns>Enumeração com objetos para teste.</returns>
		public static List<Exam> GetList()
		{
			var output = new List<Exam>();

			ICollection<Question> questions = new List<Question>() { Questions.ElementAt(0), Questions.ElementAt(1) };
			ICollection<Subject> subjects = new List<Subject>() { questions.ElementAt(0).Subject, questions.ElementAt(1).Subject };
			output.Add(new Exam(questions, subjects)
			{ RecordID = 1, IsRecordActive = true, HasOnlyUnusedQuestion = true, Type = 'M', GeneratedDate = new DateTime(2018, 01, 15) });

			questions = new List<Question>() { Questions.ElementAt(1), Questions.ElementAt(2) };
			subjects = new List<Subject>() { questions.ElementAt(0).Subject, questions.ElementAt(1).Subject };
			output.Add(new Exam(questions, subjects)
			{ RecordID = 2, IsRecordActive = true, HasOnlyUnusedQuestion = true, Type = 'M', GeneratedDate = new DateTime(2018, 02, 16) });

			questions = new List<Question>() { Questions.ElementAt(2), Questions.ElementAt(3) };
			subjects = new List<Subject>() { questions.ElementAt(0).Subject, questions.ElementAt(1).Subject };
			output.Add(new Exam(questions, subjects)
			{ RecordID = 3, IsRecordActive = true, HasOnlyUnusedQuestion = true, Type = 'M', GeneratedDate = new DateTime(2018, 03, 17) });

			questions = new List<Question>() { Questions.ElementAt(3), Questions.ElementAt(4) };
			subjects = new List<Subject>() { questions.ElementAt(0).Subject, questions.ElementAt(1).Subject };
			output.Add(new Exam(questions, subjects)
			{ RecordID = 4, IsRecordActive = true, HasOnlyUnusedQuestion = true, Type = 'M', GeneratedDate = new DateTime(2018, 04, 18) });

			questions = new List<Question>() { Questions.ElementAt(4), Questions.ElementAt(5) };
			subjects = new List<Subject>() { questions.ElementAt(0).Subject, questions.ElementAt(1).Subject };
			output.Add(new Exam(questions, subjects)
			{ RecordID = 5, IsRecordActive = true, HasOnlyUnusedQuestion = true, Type = 'M', GeneratedDate = new DateTime(2018, 05, 19) });

			questions = new List<Question>() { Questions.ElementAt(5), Questions.ElementAt(6) };
			subjects = new List<Subject>() { questions.ElementAt(0).Subject, questions.ElementAt(1).Subject };
			output.Add(new Exam(questions, subjects)
			{ RecordID = 6, IsRecordActive = false, HasOnlyUnusedQuestion = false, Type = 'M', GeneratedDate = new DateTime(2018, 06, 20) });

			questions = new List<Question>() { Questions.ElementAt(6), Questions.ElementAt(7) };
			subjects = new List<Subject>() { questions.ElementAt(0).Subject, questions.ElementAt(1).Subject };
			output.Add(new Exam(questions, subjects)
			{ RecordID = 7, IsRecordActive = false, HasOnlyUnusedQuestion = false, Type = 'M', GeneratedDate = new DateTime(2018, 07, 21) });

			questions = new List<Question>() { Questions.ElementAt(7), Questions.ElementAt(8) };
			subjects = new List<Subject>() { questions.ElementAt(0).Subject, questions.ElementAt(1).Subject };
			output.Add(new Exam(questions, subjects)
			{ RecordID = 8, IsRecordActive = false, HasOnlyUnusedQuestion = false, Type = 'M', GeneratedDate = new DateTime(2018, 08, 22) });

			questions = new List<Question>() { Questions.ElementAt(8), Questions.ElementAt(9) };
			subjects = new List<Subject>() { questions.ElementAt(0).Subject, questions.ElementAt(1).Subject };
			output.Add(new Exam(questions, subjects)
			{ RecordID = 9, IsRecordActive = false, HasOnlyUnusedQuestion = false, Type = 'M', GeneratedDate = new DateTime(2018, 09, 23) });

			questions = new List<Question>() { Questions.ElementAt(9), Questions.ElementAt(0) };
			subjects = new List<Subject>() { questions.ElementAt(0).Subject, questions.ElementAt(1).Subject };
			output.Add(new Exam(questions, subjects)
			{ RecordID = 10, IsRecordActive = false, HasOnlyUnusedQuestion = false, Type = 'M', GeneratedDate = new DateTime(2018, 10, 24) });

			return output;
		}
		#endregion
	}
}

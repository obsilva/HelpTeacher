// This Source Code Form is subject to the terms of the Mozilla 
// Public License, v. 2.0. If a copy of the MPL was not distributed 
// with this file, You can obtain one at https://mozilla.org/MPL/2.0/

// Since: 2018/11/01
// Authors: 
//		Otávio Bueno Silva <obsilva94@gmail.com>

using System;
using System.Collections.Generic;

using HelpTeacher.Domain.Entities;
using HelpTeacher.Domain.Test.TestData;

using NUnit.Framework;

namespace HelpTeacher.Domain.Test.Entities
{
	/// <summary>Implementa testes de unidade da classe <seealso cref="Exam"/>.</summary>
	[TestFixture]
	[Parallelizable(ParallelScope.All)]
	public class ExamTest
	{
		#region Properties
		private readonly char Type = 'M';

		private Exam ExamNull { get; set; } = Exam.Null;

		private ICollection<Question> Questions { get; } = QuestionTestData.GetList();

		private ICollection<Subject> Subjects { get; } = SubjectTestData.GetList();
		#endregion


		#region Methods

		#endregion


		#region Constructors
		public ExamTest() { }
		#endregion


		#region Init and Cleanup
		[OneTimeSetUp]
		public void InitClass() { }

		[SetUp]
		public void InitTest() { }

		[TearDown]
		public void CleanupTest() { }

		[OneTimeTearDown]
		public void CleanupClass() { }
		#endregion


		#region Tests
		[Test]
		public void Questions_ArgumentNullException_When_Null()
			=> Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo(nameof(Exam.Questions)),
				() => ExamNull.Questions = null);

		[Test]
		public void Questions_ArgumentOutOfRangeException_When_EmptyCollection()
			=> Assert.Throws(Is.TypeOf<ArgumentOutOfRangeException>().And.Property("ParamName").EqualTo(nameof(Exam.Questions)),
				() => ExamNull.Questions = new List<Question>());

		[Test]
		[NonParallelizable]
		public void Questions_Void_When_ValidValue()
		{
			ExamNull.Questions = Questions;

			Assert.AreEqual(Questions, ExamNull.Questions);
		}

		[Test]
		public void Subjects_ArgumentNullException_When_Null()
			=> Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo(nameof(Exam.Subjects)),
				() => ExamNull.Subjects = null);

		[Test]
		public void Subjects_ArgumentOutOfRangeException_When_EmptyCollection()
			=> Assert.Throws(Is.TypeOf<ArgumentOutOfRangeException>().And.Property("ParamName").EqualTo(nameof(Exam.Subjects)),
				() => ExamNull.Subjects = new List<Subject>());

		[Test]
		[NonParallelizable]
		public void Subjects_Void_When_ValidValue()
		{
			ExamNull.Subjects = Subjects;

			Assert.AreEqual(Subjects, ExamNull.Subjects);
		}
		#endregion
	}
}

// This Source Code Form is subject to the terms of the Mozilla 
// Public License, v. 2.0. If a copy of the MPL was not distributed 
// with this file, You can obtain one at https://mozilla.org/MPL/2.0/

// Since: 2018/10/31
// Authors: 
//		Otávio Bueno Silva <obsilva94@gmail.com>

using System;

using HelpTeacher.Domain.Entities;

using NUnit.Framework;

namespace HelpTeacher.Domain.Test.Entities
{
	/// <summary>Implementa testes de unidade da classe <seealso cref="Question"/>.</summary>
	[TestFixture]
	[Parallelizable(ParallelScope.All)]
	public class QuestionTest
	{
		#region Properties
		private readonly string Attachment = "Attachment";

		private readonly string Name = "Question";

		private Question QuestionNull { get; } = Question.Null;

		private Subject SubjectNull { get; } = Subject.Null;
		#endregion


		#region Methods

		#endregion


		#region Constructors
		public QuestionTest() { }
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
		public void FirstAttachment_ArgumentOutOfRangeException_LenghtGreaterThanAllowed()
		{
			string value = Attachment;

			while (value.Length < Question.AttachmentMaxLength + 1)
			{
				value += value;
			}

			Assert.Throws(Is.TypeOf<ArgumentOutOfRangeException>().And.Property("ParamName").EqualTo(nameof(Question.FirstAttachment)),
				() => QuestionNull.FirstAttachment = value);
		}

		[Test]
		[NonParallelizable]
		public void FirstAttachment_Void_When_ValidValue([Values(null, "", " ", "Attachment")] string value)
		{
			Assert.DoesNotThrow(() => QuestionNull.FirstAttachment = value);
			Assert.AreEqual(value, QuestionNull.FirstAttachment);
		}

		[Test]
		public void Subject_ArgumentNullException_When_Null()
			=> Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo(nameof(Question.Subject)),
				() => QuestionNull.Subject = null);

		[Test]
		public void Subject_Void_When_ValidValue()
		{
			Assert.DoesNotThrow(() => QuestionNull.Subject = SubjectNull);
			Assert.AreEqual(SubjectNull, QuestionNull.Subject);
		}

		[Test]
		public void IsNull_True_When_NullInstance()
			=> Assert.IsTrue(Question.Null.IsNull);

		[Test]
		public void IsNull_False_When_IsNotNullInstance()
		{
			var obj = new Question(SubjectNull, Name);

			Assert.IsFalse(obj.IsNull);
		}

		[Test]
		public void SecondAttachment_ArgumentOutOfRangeException_LenghtGreaterThanAllowed()
		{
			string value = Attachment;

			while (value.Length < Question.AttachmentMaxLength + 1)
			{
				value += value;
			}

			Assert.Throws(Is.TypeOf<ArgumentOutOfRangeException>().And.Property("ParamName").EqualTo(nameof(Question.SecondAttachment)),
				() => QuestionNull.SecondAttachment = value);
		}

		[Test]
		[NonParallelizable]
		public void SecondAttachment_Void_When_ValidValue([Values(null, "", " ", "Attachment")] string value)
		{
			Assert.DoesNotThrow(() => QuestionNull.SecondAttachment = value);
			Assert.AreEqual(value, QuestionNull.SecondAttachment);
		}

		[Test]
		public void Statement_ArgumentNullException_When_Empty([Values(null, "", " ")] string value)
			=> Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo(nameof(Question.Statement)),
				() => QuestionNull.Statement = value);

		[Test]
		public void Statement_ArgumentOutOfRangeException_When_LenghtGreaterThanAllowed()
		{
			string value = Name;

			while (value.Length < Question.StatementMaxLength + 1)
			{
				value += value;
			}

			Assert.Throws(Is.TypeOf<ArgumentOutOfRangeException>().And.Property("ParamName").EqualTo(nameof(Question.Statement)),
				() => QuestionNull.Statement = value);
		}

		[Test]
		public void Statement_Void_When_ValidValue()
		{
			Assert.DoesNotThrow(() => QuestionNull.Statement = Name);
			Assert.AreEqual(Name, QuestionNull.Statement);
		}
		#endregion
	}
}

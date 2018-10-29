// This Source Code Form is subject to the terms of the Mozilla 
// Public License, v. 2.0. If a copy of the MPL was not distributed 
// with this file, You can obtain one at https://mozilla.org/MPL/2.0/

// Since: 2018/10/28
// Authors: 
//		Otávio Bueno Silva <obsilva94@gmail.com>

using System;

using HelpTeacher.Domain.Entities;

using NUnit.Framework;

namespace HelpTeacher.Domain.Test.Entities
{
	/// <summary>Implementa testes de unidade da classe <seealso cref="Subject"/>.</summary>
	[TestFixture]
	[Parallelizable(ParallelScope.All)]
	public class SubjectTest
	{
		#region Properties
		private Discipline DisciplineNull { get; } = Discipline.Null;

		private readonly string Name = "Discipline";

		private Subject SubjectNull { get; } = Subject.Null;
		#endregion


		#region Methods

		#endregion


		#region Constructors
		public SubjectTest() { }
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
		public void Discipline_ArgumentNullException_When_Null()
			=> Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo(nameof(Subject.Discipline)),
				() => SubjectNull.Discipline = null);

		[Test]
		public void Discipline_Void_When_ValidValue()
		{
			Assert.DoesNotThrow(() => SubjectNull.Discipline = DisciplineNull);
			Assert.AreEqual(DisciplineNull, SubjectNull.Discipline);
		}

		[Test]
		public void IsNull_True_When_NullInstance()
			=> Assert.IsTrue(SubjectNull.IsNull);

		[Test]
		public void IsNull_False_When_IsNotNullInstance()
		{
			var obj = new Subject(DisciplineNull, Name);

			Assert.IsFalse(obj.IsNull);
		}

		[Test]
		public void Name_ArgumentNullException_When_Empty([Values(null, "", " ")] string value)
			=> Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo(nameof(Subject.Name)),
				() => SubjectNull.Name = value);

		[Test]
		public void Name_ArgumentOutOfRangeException_When_LenghtGreaterThanAllowed()
		{
			string value = Name;

			while (value.Length < Subject.NameMaxLength + 1)
			{
				value += value;
			}

			Assert.Throws(Is.TypeOf<ArgumentOutOfRangeException>().And.Property("ParamName").EqualTo(nameof(Subject.Name)),
				() => SubjectNull.Name = value);
		}

		[Test]
		public void Name_Void_When_ValidValue()
		{
			Assert.DoesNotThrow(() => SubjectNull.Name = Name);
			Assert.AreEqual(Name, SubjectNull.Name);
		}
		#endregion
	}
}

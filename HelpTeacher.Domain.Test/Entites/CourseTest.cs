// This Source Code Form is subject to the terms of the Mozilla 
// Public License, v. 2.0. If a copy of the MPL was not distributed 
// with this file, You can obtain one at https://mozilla.org/MPL/2.0/

// Since: 2018/10/27
// Authors: 
//		Otávio Bueno Silva <obsilva94@gmail.com>

using System;

using HelpTeacher.Domain.Entities;

using NUnit.Framework;

namespace HelpTeacher.Domain.Test.Entities
{
	/// <summary>Implementa testes de unidade da classe <seealso cref="CourseTest"/>.</summary>
	[TestFixture]
	[Parallelizable(ParallelScope.All)]
	public class CourseTest
	{
		#region Properties
		private Course CourseNull { get; } = Course.Null;

		private readonly string Name = "Course";
		#endregion


		#region Methods

		#endregion


		#region Constructors
		public CourseTest() { }
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
		public void IsNull_True_When_NullInstance()
			=> Assert.IsTrue(CourseNull.IsNull);

		[Test]
		public void IsNull_False_When_IsNotNullInstance()
		{
			var obj = new Course(Name);

			Assert.IsFalse(obj.IsNull);
		}

		[Test]
		public void Name_ArgumentNullException_When_Empty([Values(null, "", " ")] string value)
			=> Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo(nameof(Course.Name)),
				() => CourseNull.Name = value);

		[Test]
		public void Name_ArgumentOutOfRangeException_When_LenghtGreaterThanAllowed()
		{
			string value = Name;

			while (value.Length < Course.NAME_MAX_LENGTH + 1)
			{
				value += value;
			}

			Assert.Throws(Is.TypeOf<ArgumentOutOfRangeException>().And.Property("ParamName").EqualTo(nameof(Course.Name)),
				() => CourseNull.Name = value);
		}

		[Test]
		public void Name_Void_When_ValidValue()
		{
			Assert.DoesNotThrow(() => CourseNull.Name = Name);
			Assert.AreEqual(Name, CourseNull.Name);
		}
		#endregion
	}
}
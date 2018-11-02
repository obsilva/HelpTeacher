// This Source Code Form is subject to the terms of the Mozilla 
// Public License, v. 2.0. If a copy of the MPL was not distributed 
// with this file, You can obtain one at https://mozilla.org/MPL/2.0/

// Since: 2018/10/27
// Authors: 
//		Otávio Bueno Silva <obsilva94@gmail.com>

using System;

using NUnit.Framework;

namespace HelpTeacher.Util.Test
{
	/// <summary>Implementa testes de unidade da classe <seealso cref="Checker"/>.</summary>
	[TestFixture]
	[Parallelizable(ParallelScope.All)]
	public class CheckerTest
	{
		#region Properties
		private string ErrorMessage { get; } = "errorMessage";

		private string ParamName { get; } = "propertieName";

		private string StringNotEmpty { get; } = "something";
		#endregion


		#region Methods

		#endregion


		#region Constructors
		public CheckerTest() { }
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
		public void NullObject_ArgumentNullException_When_ObjectNull()
			=> Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo(ParamName),
					() => Checker.NullObject(null, ParamName));

		[Test]
		public void NullObject_Void_When_ValidArgument()
			=> Assert.DoesNotThrow(() => Checker.NullObject(String.Empty, ParamName));

		[Test]
		public void NullOrEmpty_ArgumentNullException_When_ValueEmpty([Values(null, "", " ")] string value)
			=> Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo(ParamName),
					() => Checker.NullOrEmptyString(value, ParamName));

		[Test]
		public void NullOrEmpty_Void_When_ValidArguments()
			=> Assert.DoesNotThrow(() => Checker.NullOrEmptyString(StringNotEmpty, ParamName));

		[Test]
		public void StringLength_ArgumentOutOfRangeException_When_StringLenghtGreaterThanAllowed()
			=> Assert.Throws(Is.TypeOf<ArgumentOutOfRangeException>().And.Property("ParamName").EqualTo(ParamName),
					() => Checker.StringLength(StringNotEmpty, ParamName, StringNotEmpty.Length - 1,
						StringNotEmpty.Length, ErrorMessage));

		[Test]
		public void StringLength_ArgumentOutOfRangeException_When_StringLenghtLowerThanAllowed()
			=> Assert.Throws(Is.TypeOf<ArgumentOutOfRangeException>().And.Property("ParamName").EqualTo(ParamName),
				() => Checker.StringLength(StringNotEmpty, ParamName, StringNotEmpty.Length,
					StringNotEmpty.Length + 1, ErrorMessage
			));

		[Test]
		public void StringLength_Void_When_ValidArguments()
			=> Assert.DoesNotThrow(() => Checker.StringLength(StringNotEmpty, ParamName,
				StringNotEmpty.Length, StringNotEmpty.Length, ErrorMessage));

		[Test]
		public void Value_ArgumentOutOfRangeException_When_ValueGreaterThanAllowed([Values(11, 1000, 10000)] int value)
		{
			int maxValue = 10;
			int minValue = 10;

			Assert.Throws(Is.TypeOf<ArgumentOutOfRangeException>().And.Property("ParamName").EqualTo(ParamName),
				() => Checker.Value(value, ParamName, minValue, maxValue, ErrorMessage));
		}

		[Test]
		public void Value_ArgumentOutOfRangeException_When_ValueLowerThanAllowed([Values(9, 0, -1000)] int value)
		{
			int maxValue = 10;
			int minValue = 10;

			Assert.Throws(Is.TypeOf<ArgumentOutOfRangeException>().And.Property("ParamName").EqualTo(ParamName),
				() => Checker.Value(value, ParamName, minValue, maxValue, ErrorMessage));
		}

		[Test]
		public void Value_Void_When_ValidArguments()
			=> Assert.DoesNotThrow(() => Checker.Value(5, ParamName,
				0, 10, ErrorMessage));
		#endregion
	}
}

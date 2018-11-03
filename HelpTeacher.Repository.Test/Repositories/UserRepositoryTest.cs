// This Source Code Form is subject to the terms of the Mozilla 
// Public License, v. 2.0. If a copy of the MPL was not distributed 
// with this file, You can obtain one at https://mozilla.org/MPL/2.0/

// Since: 2018/11/02
// Authors: 
//		Otávio Bueno Silva <obsilva94@gmail.com>

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

using HelpTeacher.Domain.Entities;
using HelpTeacher.Domain.Test.TestData;
using HelpTeacher.Repository.Repositories;

using NUnit.Framework;

namespace HelpTeacher.Repository.Test.Repositories
{
	/// <summary>Implementa testes de unidade da classe <seealso cref="UserRepository"/>.</summary>
	[TestFixture]
	[Parallelizable(ParallelScope.All)]
	public class UserRepositoryTest
	{
		#region Properties
		private ConnectionManager Connection => new ConnectionManager(ConfigurationManager.ConnectionStrings["MySQLTest"].ConnectionString);

		private UserRepository Repository => new UserRepository(Connection);
		#endregion


		#region Constructors
		public UserRepositoryTest() { }
		#endregion


		#region Init and Cleanup
		[OneTimeSetUp]
		public void InitClass()
			// Cleanup records
			=> Connection.ExecuteQuery("DELETE FROM hta1 WHERE A1_COD > 2; ALTER TABLE hta1 AUTO_INCREMENT = 3;");

		[SetUp]
		public void InitTest() { }

		[TearDown]
		public void CleanupTest() { }

		[OneTimeTearDown]
		public void CleanupClass() { }
		#endregion


		#region Tests
		[Test]
		public void Add_ArgumentNullException_When_ObjectNull()
		{
			string paramName = "obj";
			User obj = null;

			Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo(paramName),
				() => Repository.Add(obj));
		}

		[Test]
		public void Add_ArgumentNullException_When_CollectionNull()
		{
			string paramName = "collection";
			IEnumerable<User> collection = null;

			Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo(paramName),
				() => Repository.Add(collection));
		}

		[Test, Order(10)]
		[NonParallelizable]
		public void Add_RecordAdded_When_ValidArguments()
		{
			IEnumerable<User> collection = UserTestData.GetList();

			Repository.Add(collection);

			Assert.AreEqual(UserTestData.Count + 2, Repository.Get().Count());
		}

		[Test, Order(30)]
		[NonParallelizable]
		public void First_FirstRecord_When_ThereAreRecords()
			=> Assert.AreNotEqual(UserTestData.First, Repository.First());

		[Test]
		public void Get_EmptyList_When_ThereIsNoRecord()
			=> Assert.AreEqual(2, Repository.Get().Count());

		[Test, Order(20)]
		[NonParallelizable]
		public void Get_RecordsList_When_ThereAreRecords()
			=> Assert.AreEqual(UserTestData.Count + 2, Repository.Get().Count());

		[Test, Order(20)]
		[NonParallelizable]
		public void Get_NotAllRecordsList_When_PageSizeLowerThanRecordsCount()
		{
			int pageSize = (UserTestData.Count + 2) - 1;
			var repository = new CourseRepository(Connection, pageSize);

			Assert.GreaterOrEqual(pageSize, repository.Get().Count());
		}

		[Test]
		public void Get_DefaultObject_When_ThereIsNoRecordWithSpecifiedId()
			=> Assert.AreEqual(User.Null, Repository.Get(UserTestData.Count + 1));

		[Test, Order(30)]
		[NonParallelizable]
		public void Get_SpecifiedRecord_When_ThereIsRecordWithSpecifiedId()
			=> Assert.AreEqual(UserTestData.First, Repository.Get(UserTestData.First.RecordID));

		[Test]
		public void Update_ArgumentNullException_When_ObjectNull()
		{
			string paramName = "obj";
			User obj = null;

			Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo(paramName),
				() => Repository.Update(obj));
		}

		[Test]
		public void Update_ArgumentNullException_When_CollectionNull()
		{
			string paramName = "collection";
			IEnumerable<User> collection = null;

			Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo(paramName),
				() => Repository.Update(collection));
		}

		[Test, Order(20)]
		[NonParallelizable]
		public void Update_RecordUpdated_When_ValidArguments()
		{
			IEnumerable<User> collection = UserTestData.GetList();

			Repository.Update(collection);

			Assert.AreEqual(UserTestData.Count + 2, Repository.Get().Count());
		}
		#endregion
	}
}

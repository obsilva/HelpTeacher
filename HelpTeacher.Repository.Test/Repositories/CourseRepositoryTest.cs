// This Source Code Form is subject to the terms of the Mozilla 
// Public License, v. 2.0. If a copy of the MPL was not distributed 
// with this file, You can obtain one at https://mozilla.org/MPL/2.0/

// Since: 2018/10/25
// Authors: 
//		Otávio Bueno Silva <obsilva94@gmail.com>

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;

using HelpTeacher.Domain.Entities;
using HelpTeacher.Repository.Repositories;
using HelpTeacher.Repository.Test.TestData;

using NUnit.Framework;

namespace HelpTeacher.Repository.Test.Repositories
{
	/// <summary>Implementa testes de unidade da classe <seealso cref="Repository"/>.</summary>
	[TestFixture]
	[Parallelizable(ParallelScope.All)]
	public class CourseRepositoryTest
	{
		#region Properties
		private DbConnection Connection => ConnectionManager.GetOpenConnection(ConfigurationManager.ConnectionStrings["MySQLTest"].ConnectionString);

		private CourseRepository Repository => new CourseRepository(Connection);
		#endregion


		#region Constructors
		public CourseRepositoryTest() { }
		#endregion


		#region Init and Cleanup
		[OneTimeSetUp]
		public void InitClass()
		{
			string query = "DELETE FROM helpteacher_test.htc1; ALTER TABLE helpteacher_test.htc1 AUTO_INCREMENT = 1;";

			ConnectionManager.ExecuteQuery(Connection, query);
		}

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
			Course obj = null;

			Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo(paramName),
				() => Repository.Add(obj));
		}

		[Test, Order(10)]
		[NonParallelizable]
		public void Add_RecordAdded_When_ValidArguments()
		{
			IEnumerable<Course> collection = CourseTestData.GetList();

			Repository.Add(collection);

			Assert.AreEqual(CourseTestData.Count, Repository.Get().Count());
		}

		[Test]
		public void First_DefaultObject_When_ThereIsNoRecord()
		{
			var obj = new Course("");

			Assert.AreEqual(obj, Repository.First());
		}

		[Test, Order(20)]
		[NonParallelizable]
		public void First_FirstRecord_When_ThereAreRecords()
			=> Assert.AreEqual(CourseTestData.First, Repository.First());

		[Test]
		public void Get_EmptyList_When_ThereIsNoRecord()
			=> Assert.AreEqual(0, Repository.Get().Count());

		[Test, Order(20)]
		[NonParallelizable]
		public void Get_RecordsList_When_ThereAreRecords()
			=> Assert.AreEqual(CourseTestData.Count, Repository.Get().Count());

		[Test, Order(20)]
		[NonParallelizable]
		public void Get_NotAllRecordsList_When_PageSizeLowerThanRecordsCount()
		{
			int pageSize = CourseTestData.Count - 1;
			var repository = new CourseRepository(Connection, pageSize);

			Assert.GreaterOrEqual(pageSize, repository.Get().Count());
		}

		[Test]
		public void Get_EmptyList_When_ThereIsNoActiveRecord()
			=> Assert.AreEqual(0, Repository.Get(true).Count());

		[Test, Order(40)]
		[NonParallelizable]
		public void Get_RecordsList_When_RecordsActive()
			=> Assert.AreEqual(CourseTestData.CountActiveRecords, Repository.Get(true).Count());

		[Test, Order(40)]
		[NonParallelizable]
		public void Get_NotAllRecordsList_When_PageSizeLowerThanRecordsActiveCount()
		{
			int pageSize = CourseTestData.CountActiveRecords - 1;
			var repository = new CourseRepository(Connection, pageSize);

			Assert.GreaterOrEqual(pageSize, repository.Get(true).Count());
		}

		[Test]
		public void Get_EmptyList_When_ThereIsNoInactiveRecord()
			=> Assert.AreEqual(0, Repository.Get(false).Count());

		[Test, Order(40)]
		[NonParallelizable]
		public void Get_RecordsList_When_RecordsNotActive()
			=> Assert.AreEqual(CourseTestData.CountInactiveRecords, Repository.Get(false).Count());

		[Test, Order(40)]
		[NonParallelizable]
		public void Get_NotAllRecordsList_When_PageSizeLowerThanRecordsNotActiveCount()
		{
			int pageSize = CourseTestData.CountInactiveRecords - 1;
			var repository = new CourseRepository(Connection, pageSize);

			Assert.GreaterOrEqual(pageSize, repository.Get(false).Count());
		}

		[Test]
		public void Get_DefaultObject_When_ThereIsNoRecordWithSpecifiedId()
		{
			var obj = new Course("");

			Assert.AreEqual(obj, Repository.Get(CourseTestData.Count + 1));
		}

		[Test, Order(20)]
		[NonParallelizable]
		public void Get_SpecifiedRecord_When_ThereIsRecordWithSpecifiedId()
			=> Assert.AreEqual(CourseTestData.First, Repository.Get(Repository.First().RecordID));

		[Test]
		public void GetWhereNotID_DefaultObject_When_ThereIsNoRecordDifferentThanSpecifiedId()
		{
			var obj = new Course("");

			Assert.AreEqual(obj, Repository.Get(CourseTestData.Count + 1));
		}

		[Test, Order(20)]
		[NonParallelizable]
		public void GetWhereNotID_RecordsList_When_ThereIsRecordDifferentThanSpecifiedId()
			=> Assert.AreEqual(CourseTestData.Count - 1, Repository.GetWhereNotID(CourseTestData.First.RecordID).Count());

		[Test, Order(20)]
		[NonParallelizable]
		public void GetWhereNotID_NotAllRecordsList_When_PageSizeLowerThanRecordsCount()
		{
			int pageSize = CourseTestData.Count - 2;
			var repository = new CourseRepository(Connection, pageSize);

			Assert.GreaterOrEqual(pageSize, repository.GetWhereNotID(CourseTestData.First.RecordID).Count());
		}

		[Test]
		public void Update_ArgumentNullException_When_ObjectNull()
		{
			string paramName = "obj";
			Course obj = null;

			Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo(paramName),
				() => Repository.Update(obj));
		}

		[Test, Order(30)]
		[NonParallelizable]
		public void Update_RecordUpdated_When_ValidArguments()
		{
			IEnumerable<Course> collection = CourseTestData.GetList();

			Repository.Update(collection);

			Assert.AreEqual(CourseTestData.CountInactiveRecords, Repository.Get(false).Count());
		}
		#endregion
	}
}

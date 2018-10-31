// Public License, v. 2.0. If a copy of the MPL was not distributed 
// with this file, You can obtain one at https://mozilla.org/MPL/2.0/

// Since: 2018/10/28
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
	/// <summary>Implementa testes de unidade da classe <seealso cref="SubjectRepository"/>.</summary>
	[TestFixture]
	[Parallelizable(ParallelScope.All)]
	public class SubjectRepositoryTest
	{
		#region Properties
		private DbConnection Connection => ConnectionManager.GetOpenConnection(ConfigurationManager.ConnectionStrings["MySQLTest"].ConnectionString);

		private SubjectRepository Repository => new SubjectRepository(Connection);
		#endregion


		#region Methods

		#endregion


		#region Constructors
		public SubjectRepositoryTest() { }
		#endregion


		#region Init and Cleanup
		[OneTimeSetUp]
		public void InitClass()
		{
			string query = "DELETE FROM htc3; ALTER TABLE htc3 AUTO_INCREMENT = 1;";

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
			Subject obj = null;

			Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo(paramName),
				() => Repository.Add(obj));
		}

		[Test]
		public void Add_ArgumentNullException_When_CollectionNull()
		{
			string paramName = "collection";
			IEnumerable<Subject> collection = null;

			Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo(paramName),
				() => Repository.Add(collection));
		}

		[Test, Order(10)]
		[NonParallelizable]
		public void Add_RecordAdded_When_ValidArguments()
		{
			IEnumerable<Subject> collection = SubjectTestData.GetList();

			Repository.Add(collection);

			Assert.AreEqual(SubjectTestData.Count, Repository.Get().Count());
		}

		[Test]
		public void First_DefaultObject_When_ThereIsNoRecord()
			=> Assert.AreEqual(Subject.Null, Repository.First());

		[Test, Order(30)]
		[NonParallelizable]
		public void First_FirstRecord_When_ThereAreRecords()
			=> Assert.AreEqual(SubjectTestData.First, Repository.First());

		[Test]
		public void Get_EmptyList_When_ThereIsNoRecord()
			=> Assert.AreEqual(0, Repository.Get().Count());

		[Test, Order(20)]
		[NonParallelizable]
		public void Get_NotAllRecordsList_When_PageSizeLowerThanRecordsCount()
		{
			int pageSize = SubjectTestData.Count - 1;
			var repository = new SubjectRepository(Connection, pageSize);

			Assert.GreaterOrEqual(pageSize, repository.Get().Count());
		}

		[Test, Order(20)]
		[NonParallelizable]
		public void Get_RecordsList_When_ThereAreRecords()
			=> Assert.AreEqual(SubjectTestData.Count, Repository.Get().Count());

		[Test]
		public void Get_EmptyList_When_ThereIsNoActiveRecord()
			=> Assert.AreEqual(0, Repository.Get(true).Count());

		[Test, Order(30)]
		[NonParallelizable]
		public void Get_NotAllRecordsList_When_PageSizeLowerThanRecordsActiveCount()
		{
			int pageSize = SubjectTestData.CountActiveRecords - 1;
			var repository = new SubjectRepository(Connection, pageSize);

			Assert.GreaterOrEqual(pageSize, repository.Get(true).Count());
		}

		[Test, Order(30)]
		[NonParallelizable]
		public void Get_RecordsList_When_RecordsActive()
			=> Assert.AreEqual(SubjectTestData.CountActiveRecords, Repository.Get(true).Count());

		[Test]
		public void Get_EmptyList_When_ThereIsNoInactiveRecord()
			=> Assert.AreEqual(0, Repository.Get(false).Count());

		[Test, Order(30)]
		[NonParallelizable]
		public void Get_NotAllRecordsList_When_PageSizeLowerThanRecordsNotActiveCount()
		{
			int pageSize = SubjectTestData.CountInactiveRecords - 1;
			var repository = new SubjectRepository(Connection, pageSize);

			Assert.GreaterOrEqual(pageSize, repository.Get(false).Count());
		}

		[Test, Order(30)]
		[NonParallelizable]
		public void Get_RecordsList_When_RecordsNotActive()
			=> Assert.AreEqual(SubjectTestData.CountInactiveRecords, Repository.Get(false).Count());

		[Test]
		public void Get_DefaultObject_When_ThereIsNoRecordWithSpecifiedId()
			=> Assert.AreEqual(Subject.Null, Repository.Get(SubjectTestData.Count + 1));

		[Test, Order(30)]
		[NonParallelizable]
		public void Get_SpecifiedRecord_When_ThereIsRecordWithSpecifiedId()
			=> Assert.AreEqual(SubjectTestData.First, Repository.Get(Repository.First().RecordID));

		[Test]
		public void GetWhereDiscipline_EmptyList_When_ObjectNull()
			=> Assert.AreEqual(0, Repository.GetWhereDiscipline(null).Count());

		[Test, Order(20)]
		[NonParallelizable]
		public void GetWhereDiscipline_EmptyList_When_ThereIsNoRecordWithSpecifiedId()
			=> Assert.AreEqual(0, Repository.GetWhereDiscipline(SubjectTestData.Last.Discipline.RecordID + 1).Count());

		[Test, Order(20)]
		[NonParallelizable]
		public void GetWhereDiscipline_NotAllRecordsList_When_PageSizeLowerThanRecordsCount()
		{
			int pageSize = 0;
			var repository = new SubjectRepository(Connection, pageSize);

			Assert.GreaterOrEqual(pageSize, repository.GetWhereDiscipline(SubjectTestData.First.Discipline.RecordID).Count());
		}

		[Test, Order(20)]
		[NonParallelizable]
		public void GetWhereDiscipline_RecordsList_When_ThereIsRecordWithSpecifiedId()
			=> Assert.AreEqual(1, Repository.GetWhereDiscipline(SubjectTestData.First.Discipline.RecordID).Count());

		[Test]
		public void GetWhereDisciplineActive_EmptyList_When_ObjectNull()
			=> Assert.AreEqual(0, Repository.GetWhereDiscipline(null, true).Count());

		[Test]
		public void GetWhereDisciplineActive_EmptyList_When_ThereIsNoRecordWithSpecifiedId()
			=> Assert.AreEqual(0, Repository.GetWhereDiscipline(SubjectTestData.Last.Discipline.RecordID + 1, true).Count());

		[Test, Order(30)]
		[NonParallelizable]
		public void GetWhereDisciplineActive_EmptyList_When_ThereIsNoActiveRecordWithSpecifiedId()
			=> Assert.AreEqual(0, Repository.GetWhereDiscipline(SubjectTestData.Last.Discipline,
				!SubjectTestData.Last.IsRecordActive).Count());

		[Test, Order(30)]
		[NonParallelizable]
		public void GetWhereDisciplineActive__NotAllRecordsList_When_PageSizeLowerThanRecordsCount()
		{
			int pageSize = 0;
			var repository = new SubjectRepository(Connection, pageSize);

			Assert.GreaterOrEqual(pageSize, repository.GetWhereDiscipline(SubjectTestData.First.Discipline,
				SubjectTestData.First.IsRecordActive).Count());
		}

		[Test, Order(30)]
		[NonParallelizable]
		public void GetWhereDisciplineActive_RecordsList_When_ThereIsActiveRecordWithSpecifiedId()
			=> Assert.AreEqual(1, Repository.GetWhereDiscipline(SubjectTestData.First.Discipline,
				SubjectTestData.First.IsRecordActive).Count());

		[Test]
		public void Update_ArgumentNullException_When_ObjectNull()
		{
			string paramName = "obj";
			Subject obj = null;

			Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo(paramName),
				() => Repository.Update(obj));
		}

		[Test]
		public void Update_ArgumentNullException_When_CollectionNull()
		{
			string paramName = "collection";
			IEnumerable<Subject> collection = null;

			Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo(paramName),
				() => Repository.Update(collection));
		}

		[Test, Order(20)]
		[NonParallelizable]
		public void Update_RecordUpdated_When_ValidArguments()
		{
			IEnumerable<Subject> collection = SubjectTestData.GetList();

			Repository.Update(collection);

			Assert.AreEqual(SubjectTestData.CountInactiveRecords, Repository.Get(false).Count());
		}
		#endregion
	}
}

// This Source Code Form is subject to the terms of the Mozilla 
// Public License, v. 2.0. If a copy of the MPL was not distributed 
// with this file, You can obtain one at https://mozilla.org/MPL/2.0/

// Since: 2018/10/21
// Authors: 
//		Otávio Bueno Silva <obsilva94@gmail.com>

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

using HelpTeacher.Domain.Entities;
using HelpTeacher.Repository.Repositories;
using HelpTeacher.Repository.Test.TestData;

using NUnit.Framework;

namespace HelpTeacher.Repository.Test.Repositories
{
	/// <summary>Implementa testes de unidade da classe <seealso cref="QuestionRepository"/>.</summary>
	[TestFixture]
	[Parallelizable(ParallelScope.None)]
	public class QuestionRepositoryTest
	{
		#region Properties
		private ConnectionManager Connection => new ConnectionManager(ConfigurationManager.ConnectionStrings["MySQLTest"].ConnectionString);

		private QuestionRepository Repository => new QuestionRepository(Connection);
		#endregion


		#region Methods

		#endregion


		#region Constructors
		public QuestionRepositoryTest() { }
		#endregion


		#region Init and Cleanup
		[OneTimeSetUp]
		public void InitClass()
		{
			// Cleanup records
			Connection.ExecuteQuery("DELETE FROM htd1; ALTER TABLE htd1 AUTO_INCREMENT = 1;");
			Connection.ExecuteQuery("DELETE FROM htb1; ALTER TABLE htb1 AUTO_INCREMENT = 1;");
			Connection.ExecuteQuery("DELETE FROM htc3; ALTER TABLE htc3 AUTO_INCREMENT = 1;");
			Connection.ExecuteQuery("DELETE FROM htc2; ALTER TABLE htc2 AUTO_INCREMENT = 1;");
			Connection.ExecuteQuery("DELETE FROM htc1; ALTER TABLE htc1 AUTO_INCREMENT = 1;");

			// Initialize required data
			new CourseRepository(Connection).Add(CourseTestData.GetList());
			new CourseRepository(Connection).Update(CourseTestData.GetList());

			new DisciplineRepository(Connection).Add(DisciplineTestData.GetList());
			new DisciplineRepository(Connection).Update(DisciplineTestData.GetList());

			new SubjectRepository(Connection).Add(SubjectTestData.GetList());
			new SubjectRepository(Connection).Update(SubjectTestData.GetList());
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
			Question obj = null;

			Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo(paramName),
				() => Repository.Add(obj));
		}

		[Test]
		public void Add_ArgumentNullException_When_CollectionNull()
		{
			string paramName = "collection";
			IEnumerable<Question> collection = null;

			Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo(paramName),
				() => Repository.Add(collection));
		}

		[Test, Order(10)]
		[NonParallelizable]
		public void Add_RecordAdded_When_ValidArguments()
		{
			IEnumerable<Question> collection = QuestionTestData.GetList();

			Repository.Add(collection);

			Assert.AreEqual(QuestionTestData.Count, Repository.Get().Count());
		}

		[Test]
		public void First_DefaultObject_When_ThereIsNoRecord()
			=> Assert.AreEqual(Question.Null, Repository.First());

		[Test, Order(30)]
		[NonParallelizable]
		public void First_FirstRecord_When_ThereAreRecords()
			=> Assert.AreEqual(QuestionTestData.First, Repository.First());

		[Test]
		public void Get_EmptyList_When_ThereIsNoRecord()
			=> Assert.AreEqual(0, Repository.Get().Count());

		[Test, Order(20)]
		[NonParallelizable]
		public void Get_NotAllRecordsList_When_PageSizeLowerThanRecordsCount()
		{
			int pageSize = QuestionTestData.Count - 1;
			var repository = new QuestionRepository(Connection, pageSize);

			Assert.GreaterOrEqual(pageSize, repository.Get().Count());
		}

		[Test, Order(20)]
		[NonParallelizable]
		public void Get_RecordsList_When_ThereAreRecords()
			=> Assert.AreEqual(QuestionTestData.Count, Repository.Get().Count());

		[Test]
		public void Get_EmptyList_When_ThereIsNoActiveRecord()
			=> Assert.AreEqual(0, Repository.Get(true).Count());

		[Test, Order(30)]
		[NonParallelizable]
		public void Get_NotAllRecordsList_When_PageSizeLowerThanRecordsActiveCount()
		{
			int pageSize = QuestionTestData.CountActiveRecords - 1;
			var repository = new QuestionRepository(Connection, pageSize);

			Assert.GreaterOrEqual(pageSize, repository.Get(true).Count());
		}

		[Test, Order(30)]
		[NonParallelizable]
		public void Get_RecordsList_When_RecordsActive()
			=> Assert.AreEqual(QuestionTestData.CountActiveRecords, Repository.Get(true).Count());

		[Test]
		public void Get_EmptyList_When_ThereIsNoInactiveRecord()
			=> Assert.AreEqual(0, Repository.Get(false).Count());

		[Test, Order(30)]
		[NonParallelizable]
		public void Get_NotAllRecordsList_When_PageSizeLowerThanRecordsNotActiveCount()
		{
			int pageSize = QuestionTestData.CountInactiveRecords - 1;
			var repository = new QuestionRepository(Connection, pageSize);

			Assert.GreaterOrEqual(pageSize, repository.Get(false).Count());
		}

		[Test, Order(30)]
		[NonParallelizable]
		public void Get_RecordsList_When_RecordsNotActive()
			=> Assert.AreEqual(QuestionTestData.CountInactiveRecords, Repository.Get(false).Count());

		[Test]
		public void Get_DefaultObject_When_ThereIsNoRecordWithSpecifiedId()
			=> Assert.AreEqual(Question.Null, Repository.Get(QuestionTestData.Count + 1));

		[Test, Order(30)]
		[NonParallelizable]
		public void Get_SpecifiedRecord_When_ThereIsRecordWithSpecifiedId()
			=> Assert.AreEqual(QuestionTestData.First, Repository.Get(Repository.First().RecordID));

		[Test, Order(30)]
		[NonParallelizable]
		public void GetWhereSubject_NotAllRecordsList_When_PageSizeLowerThanRecordsCount()
		{
			int pageSize = 0;
			var repository = new QuestionRepository(Connection, pageSize);

			//public IQueryable<Question> GetWhereSubject(Subject obj)
			Assert.GreaterOrEqual(pageSize, repository.GetWhereSubject(
				QuestionTestData.First.Subject).Count());
			//public IQueryable<Question> GetWhereSubject(Subject obj, bool isRecordActive)
			Assert.GreaterOrEqual(pageSize, repository.GetWhereSubject(
				QuestionTestData.First.Subject, QuestionTestData.First.IsRecordActive).Count());
			//public IQueryable<Question> GetWhereSubject(Subject obj, bool isRecordActive, bool isObjective)
			Assert.GreaterOrEqual(pageSize, repository.GetWhereSubject(
				QuestionTestData.First.Subject, QuestionTestData.First.IsRecordActive,
				QuestionTestData.First.IsObjective).Count());
			//public IQueryable<Question> GetWhereSubject(Subject obj, bool isRecordActive, bool isObjective, bool wasUsed)
			Assert.GreaterOrEqual(pageSize, repository.GetWhereSubject(
				QuestionTestData.First.Subject, QuestionTestData.First.IsRecordActive,
				QuestionTestData.First.IsObjective, QuestionTestData.First.WasUsed).Count());
		}

		//public IQueryable<Question> GetWhereSubject(Subject obj)
		//public IQueryable<Question> GetWhereSubject(int id)
		[Test]
		public void GetWhereSubject_EmptyList_When_ObjectNull()
			=> Assert.AreEqual(0, Repository.GetWhereSubject(null).Count());

		[Test, Order(20)]
		[NonParallelizable]
		public void GetWhereSubject_EmptyList_When_NoRecordsWithTheSpecifiedID()
			=> Assert.AreEqual(0, Repository.GetWhereSubject(
				QuestionTestData.Last.Subject.RecordID + 1).Count());

		[Test, Order(20)]
		[NonParallelizable]
		public void GetWhereSubject_RecordsList_When_RecordWithSpecifiedID()
			=> Assert.AreEqual(1, Repository.GetWhereSubject(QuestionTestData.First.Subject).Count());

		//public IQueryable<Question> GetWhereSubject(Subject obj, bool isRecordActive)
		//public IQueryable<Question> GetWhereSubject(int id, bool isRecordActive)
		[Test, Order(30)]
		[NonParallelizable]
		public void GetWhereSubject_EmptyList_When_NoActiveRecords()
			=> Assert.AreEqual(0, Repository.GetWhereSubject(QuestionTestData.First.Subject,
				!SubjectTestData.First.IsRecordActive).Count());

		[Test, Order(30)]
		[NonParallelizable]
		public void GetWhereSubject_RecordsList_When_ActiveRecords()
			=> Assert.AreEqual(1, Repository.GetWhereSubject(QuestionTestData.First.Subject,
				SubjectTestData.First.IsRecordActive).Count());

		//public IQueryable<Question> GetWhereSubject(Subject obj, bool isRecordActive, bool isObjective)
		//public IQueryable<Question> GetWhereSubject(int id, bool isRecordActive, bool isObjective)
		[Test, Order(30)]
		[NonParallelizable]
		public void GetWhereSubject_EmptyList_When_NoObjetiveRecords()
			=> Assert.AreEqual(0, Repository.GetWhereSubject(QuestionTestData.First.Subject,
				QuestionTestData.First.IsRecordActive, !QuestionTestData.First.IsObjective).Count());

		[Test, Order(30)]
		[NonParallelizable]
		public void GetWhereSubject_RecordsList_When_ObjetiveRecords()
			=> Assert.AreEqual(1, Repository.GetWhereSubject(QuestionTestData.First.Subject,
				QuestionTestData.First.IsRecordActive, QuestionTestData.First.IsObjective).Count());

		//public IQueryable<Question> GetWhereSubject(Subject obj, bool isRecordActive, bool isObjective, bool wasUsed)
		//public IQueryable<Question> GetWhereSubject(int id, bool isRecordActive, bool isObjective, bool wasUsed)
		[Test, Order(30)]
		[NonParallelizable]
		public void GetWhereSubject_EmptyList_When_NoUsedRecords()
			=> Assert.AreEqual(0, Repository.GetWhereSubject(QuestionTestData.First.Subject,
				QuestionTestData.First.IsRecordActive, QuestionTestData.First.IsObjective,
				!QuestionTestData.First.WasUsed).Count());

		[Test, Order(30)]
		[NonParallelizable]
		public void GetWhereSubject_RecordsList_When_UsedRecords()
			=> Assert.AreEqual(1, Repository.GetWhereSubject(QuestionTestData.First.Subject,
				QuestionTestData.First.IsRecordActive, QuestionTestData.First.IsObjective,
				QuestionTestData.First.WasUsed).Count());

		[Test]
		public void Update_ArgumentNullException_When_ObjectNull()
		{
			string paramName = "obj";
			Question obj = null;

			Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo(paramName),
				() => Repository.Update(obj));
		}

		[Test]
		public void Update_ArgumentNullException_When_CollectionNull()
		{
			string paramName = "collection";
			IEnumerable<Question> collection = null;

			Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo(paramName),
				() => Repository.Update(collection));
		}

		[Test, Order(20)]
		[NonParallelizable]
		public void Update_RecordUpdated_When_ValidArguments()
		{
			IEnumerable<Question> collection = QuestionTestData.GetList();

			Repository.Update(collection);

			Assert.AreEqual(QuestionTestData.CountInactiveRecords, Repository.Get(false).Count());
		}
		#endregion
	}
}

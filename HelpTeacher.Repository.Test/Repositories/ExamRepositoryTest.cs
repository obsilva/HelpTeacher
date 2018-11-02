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
	/// <summary>Implementa testes de unidade da classe <seealso cref="ExamRepository"/>.</summary>
	[TestFixture]
	[Parallelizable(ParallelScope.None)]
	public class ExamRepositoryTest
	{
		#region Properties
		private ConnectionManager Connection => new ConnectionManager(ConfigurationManager.ConnectionStrings["MySQLTest"].ConnectionString);

		private ExamRepository Repository => new ExamRepository(Connection);
		#endregion


		#region Methods

		#endregion


		#region Constructors
		public ExamRepositoryTest() { }
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

			new QuestionRepository(Connection).Add(QuestionTestData.GetList());
			new QuestionRepository(Connection).Update(QuestionTestData.GetList());
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
			Exam obj = null;

			Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo(paramName),
				() => Repository.Add(obj));
		}

		[Test]
		public void Add_ArgumentNullException_When_CollectionNull()
		{
			string paramName = "collection";
			IEnumerable<Exam> collection = null;

			Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo(paramName),
				() => Repository.Add(collection));
		}

		[Test, Order(10)]
		[NonParallelizable]
		public void Add_RecordAdded_When_ValidArguments()
		{
			IEnumerable<Exam> collection = ExamTestData.GetList();

			Repository.Add(collection);

			Assert.AreEqual(ExamTestData.Count, Repository.Get().Count());
		}

		[Test]
		public void First_DefaultObject_When_ThereIsNoRecord()
			=> Assert.AreEqual(Exam.Null, Repository.First());

		[Test, Order(30)]
		[NonParallelizable]
		public void First_FirstRecord_When_ThereAreRecords()
			=> Assert.AreEqual(ExamTestData.First, Repository.First());

		[Test]
		public void Get_EmptyList_When_ThereIsNoRecord()
			=> Assert.AreEqual(0, Repository.Get().Count());

		[Test, Order(20)]
		[NonParallelizable]
		public void Get_NotAllRecordsList_When_PageSizeLowerThanRecordsCount()
		{
			int pageSize = ExamTestData.Count - 1;
			var repository = new ExamRepository(Connection, pageSize);

			Assert.GreaterOrEqual(pageSize, repository.Get().Count());
		}

		[Test, Order(20)]
		[NonParallelizable]
		public void Get_RecordsList_When_ThereAreRecords()
			=> Assert.AreEqual(ExamTestData.Count, Repository.Get().Count());

		[Test]
		public void Get_EmptyList_When_ThereIsNoActiveRecord()
			=> Assert.AreEqual(0, Repository.Get(true).Count());

		[Test, Order(30)]
		[NonParallelizable]
		public void Get_NotAllRecordsList_When_PageSizeLowerThanRecordsActiveCount()
		{
			int pageSize = ExamTestData.CountActiveRecords - 1;
			var repository = new ExamRepository(Connection, pageSize);

			Assert.GreaterOrEqual(pageSize, repository.Get(true).Count());
		}

		[Test, Order(30)]
		[NonParallelizable]
		public void Get_RecordsList_When_RecordsActive()
			=> Assert.AreEqual(ExamTestData.CountActiveRecords, Repository.Get(true).Count());

		[Test]
		public void Get_EmptyList_When_ThereIsNoInactiveRecord()
			=> Assert.AreEqual(0, Repository.Get(false).Count());

		[Test, Order(30)]
		[NonParallelizable]
		public void Get_NotAllRecordsList_When_PageSizeLowerThanRecordsNotActiveCount()
		{
			int pageSize = ExamTestData.CountInactiveRecords - 1;
			var repository = new ExamRepository(Connection, pageSize);

			Assert.GreaterOrEqual(pageSize, repository.Get(false).Count());
		}

		[Test, Order(30)]
		[NonParallelizable]
		public void Get_RecordsList_When_RecordsNotActive()
			=> Assert.AreEqual(ExamTestData.CountInactiveRecords, Repository.Get(false).Count());

		[Test]
		public void Get_DefaultObject_When_ThereIsNoRecordWithSpecifiedId()
			=> Assert.AreEqual(Exam.Null, Repository.Get(ExamTestData.Count + 1));

		[Test, Order(30)]
		[NonParallelizable]
		public void Get_SpecifiedRecord_When_ThereIsRecordWithSpecifiedId()
			=> Assert.AreEqual(ExamTestData.First, Repository.Get(Repository.First().RecordID));

		[Test]
		public void Update_ArgumentNullException_When_ObjectNull()
		{
			string paramName = "obj";
			Exam obj = null;

			Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo(paramName),
				() => Repository.Update(obj));
		}

		[Test]
		public void Update_ArgumentNullException_When_CollectionNull()
		{
			string paramName = "collection";
			IEnumerable<Exam> collection = null;

			Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo(paramName),
				() => Repository.Update(collection));
		}

		[Test, Order(20)]
		[NonParallelizable]
		public void Update_RecordUpdated_When_ValidArguments()
		{
			IEnumerable<Exam> collection = ExamTestData.GetList();

			Repository.Update(collection);

			Assert.AreEqual(ExamTestData.CountInactiveRecords, Repository.Get(false).Count());
		}
		#endregion
	}
}

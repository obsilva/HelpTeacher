// This Source Code Form is subject to the terms of the Mozilla 
// Public License, v. 2.0. If a copy of the MPL was not distributed 
// with this file, You can obtain one at https://mozilla.org/MPL/2.0/

// Since: 2018/10/21
// Authors: 
//		Otávio Bueno Silva <obsilva94@gmail.com>

using System;
using System.Configuration;
using System.Data.Common;

using NUnit.Framework;

namespace HelpTeacher.Repository.Test
{
	/// <summary>Implementa testes de unidade da classe <seealso cref="ConnectionManager"/>.</summary>
	[TestFixture]
	[Parallelizable(ParallelScope.All)]
	public class ConnectionManagerTest
	{
		#region Properties
		private ConnectionManager Manager => new ConnectionManager(ConfigurationManager.ConnectionStrings["MySQLTest"].ConnectionString);

		private readonly string Query = "SELECT * FROM hta1;";

		private readonly string InvalidQuery = "SELECT;";

		private readonly string QueryWithParameters = "SELECT @COD, @LOGIN, @PASSWORD FROM hta1;";
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
		public void new_ArgumentNullException_When_ConnectionStringEmpty(
				[Values(null, "", " ")] string connectionString)
			=> Assert.Throws(typeof(ArgumentNullException), () => new ConnectionManager(connectionString));

		[Test]
		public void new_ArgumenException_When_ConnectionStringInvalid()
		{
			string connectionString = "Something else than a valid connection string";

			Assert.Throws(typeof(ArgumentException), () => new ConnectionManager(connectionString));
		}

		[Test]
		public void new_ConnectionManager_When_ValidArguments()
		{
			var manager = new ConnectionManager();

			Assert.IsNotNull(manager);
		}

		[Test]
		public void AddCommandParameters_ArgumentException_When_DifferentNumberOfArguments()
		{
			string query = QueryWithParameters;

			Assert.Throws(typeof(ArgumentException), () => Manager.ExecuteReader(query, "A1_COD", "A1_LOGIN"));
		}

		[Test]
		public void AddCommandParameters_ParametersAdded_When_ValidArguments()
		{
			string query = QueryWithParameters;

			DbDataReader dataReader = Manager.ExecuteReader(query, "A1_COD", "A1_LOGIN", "A1_PWD");

			Assert.IsNotNull(dataReader);
			Assert.Greater(dataReader.FieldCount, 0);
			dataReader.Dispose();
		}

		[Test]
		public void ExecuteAdapter_ArgumentNullException_When_QueryEmpty([Values(null, "", " ")] string query)
		{
			string paramName = "query";

			Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo(paramName),
				() => Manager.ExecuteAdapter(query));
		}

		[Test]
		public void ExecuteAdapter_NewDataAdapter_When_ValidArguments()
		{
			DbDataAdapter dataAdapter = Manager.ExecuteAdapter(Query);

			Assert.IsNotNull(dataAdapter);
			dataAdapter.Dispose();
		}

		[Test]
		public void ExecuteQuery_ArgumentNullException_When_QueryEmpty([Values(null, "", " ")] string query)
		{
			string paramName = "query";

			Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo(paramName),
				() => Manager.ExecuteQuery(query));
		}

		[Test]
		public void ExecuteQuery_Exception_When_QueryInvalid()
			=> Assert.Throws(typeof(Exception), () => Manager.ExecuteQuery(InvalidQuery));

		/// <remarks>
		/// O único método que encontrei para quebrar a execução da query foi executar uma query
		/// inválida, mas outros errors como a conexão quebrar bem antes de executar a consulta
		/// também seriam capturados pela exception.
		/// Caso encontre outras formas para testar, o método será atualizado.
		/// </remarks>
		[Test]
		public void ExecuteQuery_Exception_When_NotPossibleExecuteQuery()
			=> Assert.Throws(typeof(Exception), () => Manager.ExecuteQuery(InvalidQuery));

		[Test]
		public void ExecuteQuery_NoExceptions_When_ValidArguments()
			=> Assert.DoesNotThrow(() => Manager.ExecuteQuery(Query));

		[Test]
		public void ExecuteReader_ArgumentNullException_When_QueryEmpty([Values(null, "", " ")] string query)
		{
			string paramName = "query";

			Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo(paramName),
				() => Manager.ExecuteReader(query));
		}

		[Test]
		public void ExecuteReader_NewDataReader_When_ValidArguments()
		{
			DbDataReader dataReader = Manager.ExecuteReader(Query);

			Assert.IsNotNull(dataReader);
			Assert.Greater(dataReader.FieldCount, 0);
			dataReader.Dispose();
		}
		#endregion
	}
}
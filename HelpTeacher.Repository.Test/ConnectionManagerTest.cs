// This Source Code Form is subject to the terms of the Mozilla 
// Public License, v. 2.0. If a copy of the MPL was not distributed 
// with this file, You can obtain one at https://mozilla.org/MPL/2.0/

// Since: 2018/10/21
// Authors: 
//		Otávio Bueno Silva <obsilva94@gmail.com>

using System;
using System.Configuration;
using System.Data.Common;

using MySql.Data.MySqlClient;

using NUnit.Framework;

namespace HelpTeacher.Repository.Test
{
	/// <summary>Implementa testes de unidade da classe <seealso cref="ConnectionManager"/>.</summary>
	[TestFixture]
	[Parallelizable(ParallelScope.All)]
	public class ConnectionManagerTest
	{
		#region Properties
		private DbConnection Connection => ConnectionManager.GetConnection(ConfigurationManager.ConnectionStrings["MySQLTest"].ConnectionString);

		private DbConnection ConnectionOpen => ConnectionManager.GetOpenConnection(ConfigurationManager.ConnectionStrings["MySQLTest"].ConnectionString);

		private readonly string Query = "SELECT * FROM helpteacher_test.hta1;";

		private readonly string InvalidQuery = "SELECT;";

		private readonly string QueryWithParameters = "SELECT @COD, @LOGIN, @PASSWORD FROM helpteacher_test.hta1;";
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
		public void AddCommandParameters_ArgumentException_When_DifferentNumberOfArguments()
		{
			DbConnection connection = ConnectionOpen;
			string query = QueryWithParameters;

			Assert.Throws(typeof(ArgumentException), () => ConnectionManager.ExecuteReader(connection, query, "A1_COD", "A1_LOGIN"));
		}

		[Test]
		public void AddCommandParameters_ParametersAdded_When_ValidArguments()
		{
			DbConnection connection = ConnectionOpen;
			string query = QueryWithParameters;

			DbDataReader dataReader = ConnectionManager.ExecuteReader(connection, query, "A1_COD", "A1_LOGIN", "A1_PWD");

			Assert.IsNotNull(dataReader);
			Assert.Greater(dataReader.FieldCount, 0);
			dataReader.Dispose();
		}

		[Test]
		public void CloseConnection_DoNothing_When_ConnectionNull()
		{
			DbConnection connection = null;

			Assert.DoesNotThrow(() => ConnectionManager.CloseConnection(connection));
		}

		[Test]
		public void CloseConnection_ConnectionClosed_When_ValidArguments()
		{
			DbConnection connection = ConnectionOpen;

			ConnectionManager.CloseConnection(connection);

			Assert.IsFalse(ConnectionManager.IsConnectionOpen(connection));
		}

		[Test]
		public void ExecuteAdapter_ArgumentNullException_When_ConnectionNull()
		{
			string paramName = "connection";
			DbConnection connection = null;
			string query = "SELECT * FROM helpteacher_test.hta1;";

			Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo(paramName),
				() => ConnectionManager.ExecuteAdapter(connection, query));
		}

		[Test]
		public void ExecuteAdapter_ArgumentNullException_When_QueryEmpty([Values(null, "", " ")] string query)
		{
			string paramName = "query";
			DbConnection connection = Connection;

			Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo(paramName),
				() => ConnectionManager.ExecuteAdapter(connection, query));
		}

		[Test]
		public void ExecuteAdapter_NewDataAdapter_When_ValidArguments()
		{
			DbConnection connection = ConnectionOpen;
			string query = "SELECT * FROM helpteacher_test.hta1;";

			DbDataAdapter dataAdapter = ConnectionManager.ExecuteAdapter(connection, query);

			Assert.IsNotNull(dataAdapter);
			dataAdapter.Dispose();
		}

		[Test]
		public void ExecuteQuery_ArgumentNullException_When_ConnectionNull()
		{
			string paramName = "connection";
			DbConnection connection = null;
			string query = Query;

			Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo(paramName),
				() => ConnectionManager.ExecuteQuery(connection, query));
		}

		[Test]
		public void ExecuteQuery_ArgumentNullException_When_QueryEmpty([Values(null, "", " ")] string query)
		{
			string paramName = "query";
			DbConnection connection = Connection;

			Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo(paramName),
				() => ConnectionManager.ExecuteQuery(connection, query));
		}

		[Test]
		public void ExecuteQuery_Exception_When_QueryInvalid()
		{
			DbConnection connection = ConnectionOpen;
			string query = InvalidQuery;

			Assert.Throws(typeof(Exception), () => ConnectionManager.ExecuteQuery(connection, query));
		}

		/// <remarks>
		/// O único método que encontrei para quebrar a execução da query foi executar uma query
		/// inválida, mas outros errors como a conexão quebrar bem antes de executar a consulta
		/// também seriam capturados pela exception.
		/// Caso encontre outras formas para testar, o método será atualizado.
		/// </remarks>
		[Test]
		public void ExecuteQuery_Exception_When_NotPossibleExecuteQuery()
		{
			DbConnection connection = ConnectionOpen;
			string query = InvalidQuery;

			Assert.Throws(typeof(Exception), () => ConnectionManager.ExecuteQuery(connection, query));
		}

		[Test]
		public void ExecuteQuery_NoExceptions_When_ValidArguments()
		{
			DbConnection connection = ConnectionOpen;
			string query = Query;

			Assert.DoesNotThrow(() => ConnectionManager.ExecuteQuery(connection, query));
		}

		[Test]
		public void ExecuteReader_ArgumentNullException_When_ConnectionNull()
		{
			string paramName = "connection";
			DbConnection connection = null;
			string query = "SELECT * FROM helpteacher_test.hta1;";

			Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo(paramName),
				() => ConnectionManager.ExecuteReader(connection, query));
		}

		[Test]
		public void ExecuteReader_ArgumentNullException_When_QueryEmpty([Values(null, "", " ")] string query)
		{
			string paramName = "query";
			DbConnection connection = Connection;

			Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo(paramName),
				() => ConnectionManager.ExecuteReader(connection, query));
		}

		[Test]
		public void ExecuteReader_NewDataReader_When_ValidArguments()
		{
			DbConnection connection = ConnectionOpen;
			string query = Query;

			DbDataReader dataReader = ConnectionManager.ExecuteReader(connection, query);

			Assert.IsNotNull(dataReader);
			Assert.Greater(dataReader.FieldCount, 0);
			dataReader.Dispose();
		}

		[Test]
		public void GetConnection_ArgumentNullException_When_ConnectionStringEmpty(
				[Values(null, "", " ")] string connectionString)
			=> Assert.Throws(typeof(ArgumentNullException), () => ConnectionManager.GetConnection(connectionString));

		[Test]
		public void GetConnection_ArgumentNullException_When_ConnectionStringFormatInvalid(
				[Values("some random string that ins't a connection string")] string connectionString)
			=> Assert.Throws(typeof(ArgumentException), () => ConnectionManager.GetConnection(connectionString));


		[Test]
		public void GetConnection_NewConnectionNotOpen_When_ValidArguments()
		{
			string connectionString = ConfigurationManager.ConnectionStrings["MySQLTest"].ConnectionString;

			DbConnection connection = ConnectionManager.GetConnection(connectionString);

			Assert.IsNotNull(connection);
			Assert.False(ConnectionManager.IsConnectionOpen(connection));
		}

		[Test]
		public void GetOpenConnection_Exception_When_ConnectionStringEmpty(
				[Values(null, "", " ")] string connectionString)
			=> Assert.Throws(typeof(ArgumentNullException), () => ConnectionManager.GetOpenConnection(connectionString));

		[Test]
		public void GetOpenConnection_Exception_When_ConnectionStrinFormatInvalid(
				[Values("some random string that isn't a connection string")] string connectionString)
			=> Assert.Throws(typeof(ArgumentException), () => ConnectionManager.GetOpenConnection(connectionString));

		[Test]
		public void GetOpenConnection_NewOpenConnection_When_ValidArguments()
		{
			string connectionString = ConfigurationManager.ConnectionStrings["MySQLTest"].ConnectionString;

			DbConnection connection = ConnectionManager.GetOpenConnection(connectionString);

			Assert.IsNotNull(connection);
			Assert.True(ConnectionManager.IsConnectionOpen(connection));
		}

		[Test]
		public void IsConnectionOpen_False_When_ConnectionNull()
		{
			DbConnection connection = null;

			Assert.False(ConnectionManager.IsConnectionOpen(connection));
		}

		[Test]
		public void IsConnectionOpen_False_When_ConnectionNotOpen()
		{
			DbConnection connection = Connection;

			Assert.False(ConnectionManager.IsConnectionOpen(connection));
		}

		[Test]
		public void IsConnectionOpen_True_When_OpenConnection()
		{
			DbConnection connection = ConnectionOpen;

			Assert.True(ConnectionManager.IsConnectionOpen(connection));
		}

		[Test]
		public void OpenConnection_ArgumentNullException_When_ConnectionNull()
		{
			string paramName = "connection";
			DbConnection connection = null;

			Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo(paramName),
				() => ConnectionManager.OpenConnection(connection));
		}

		[Test]
		public void OpenConnection_Exception_When_MaxAttempsReached()
		{
			DbConnection connection = new MySqlConnection(
				"server=127.0.0.1;port=8008;database=helpteacher_test;uid=root;password=123456");

			Assert.Throws(typeof(Exception), () => ConnectionManager.OpenConnection(connection));
		}

		[Test]
		public void OpenConnection_OpenConnection_When_ValidArguments()
		{
			DbConnection connection = Connection;

			Assert.IsFalse(ConnectionManager.IsConnectionOpen(connection));
			ConnectionManager.OpenConnection(connection);
			Assert.IsTrue(ConnectionManager.IsConnectionOpen(connection));
		}
		#endregion
	}
}
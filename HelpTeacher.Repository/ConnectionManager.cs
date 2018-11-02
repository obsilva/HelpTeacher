// This Source Code Form is subject to the terms of the Mozilla 
// Public License, v. 2.0. If a copy of the MPL was not distributed 
// with this file, You can obtain one at https://mozilla.org/MPL/2.0/

// Since: 2018/09/16
// Authors: 
//		Otávio Bueno Silva <obsilva94@gmail.com>

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Threading;

using HelpTeacher.Util;

using MySql.Data.MySqlClient;

namespace HelpTeacher.Repository
{
	/// <summary>Gerencia a conexão com o banco de dados.</summary>
	public class ConnectionManager
	{
		#region Constants
		/// <summary>Define o número máximo de tentativas em uma interação com o banco de dados.</summary>
		public const int MaxAttempts = 3;
		#endregion


		#region Properties
		/// <summary>Mapeia os tipos do C# para os tipos em <see cref="DbType"/>.</summary>
		public static Dictionary<Type, DbType> DbTypeMap { get; }

		public string ConnectionString { get; }
		#endregion


		#region Constructors
		static ConnectionManager()
			=> DbTypeMap = DbTypeDictionary();

		/// <summary>
		/// Inicializa uma nova instância de <see cref="ConnectionManager"/> usando a string de
		/// conexão nomeada 'Default'.
		/// </summary>
		public ConnectionManager()
			: this(ConfigurationManager.ConnectionStrings["Default"].ConnectionString) { }

		/// <summary>
		/// Inicializa uma nova instância de <see cref="ConnectionManager"/> usando a string de
		/// conexão informada.
		/// </summary>
		/// <param name="connectionString">String de conexão a ser usada.</param>
		public ConnectionManager(string connectionString)
		{
			Checker.NullOrEmptyString(connectionString, nameof(connectionString));

			ConnectionString = connectionString;

			if (!CanOpenConnection())
			{
				throw new ArgumentException("Connection string invalid.", nameof(connectionString));
			}
		}
		#endregion


		#region Methods
		/// <summary>Adiciona os parâmetros ao commando.</summary>
		/// <param name="command">Comando onde os parâmetros serão adicionados.</param>
		/// <param name="parameterName">Nome do(s) parâmetro(s).</param>
		/// <param name="parameterValue">Valor do(s) parâmetro(s).</param>
		/// <exception cref="ArgumentNullException">
		/// Quando <see cref="command"/> for <see langword="null"/>.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// Quando a quantidade de parâmetros informados for diferente da disponíveis na query.
		/// </exception>
		private void AddCommandParameters(DbCommand command, string[] parameterName, object[] parameterValue)
		{
			Checker.NullObject(command, nameof(command));

			if (parameterName.Length != parameterValue.Length)
			{
				throw new ArgumentException($"Número de parâmetros não são iguais: {parameterName.Length} na query" +
											$" e {parameterValue.Length} informados.");
			}

			command.Parameters.Clear();
			for (int i = 0; i < parameterName.Length; i++)
			{
				DbParameter parameter = command.CreateParameter();
				parameter.DbType = DbTypeMap[parameterValue[i].GetType()];
				parameter.ParameterName = parameterName[i];
				parameter.Value = parameterValue[i];

				command.Parameters.Add(parameter);
			}
		}

		/// <summary>
		/// Determina se uma conexão pode ser aberta usando <see cref="ConnectionString"/>.</summary>
		/// <param name="connection">A conexão a ser testada.</param>
		/// <returns>
		/// <see langword="true"/> se a conexão estiver aberta, <see langword="false"/> em qualquer outro caso.
		/// </returns>
		private bool CanOpenConnection()
		{
			var validStates = new List<ConnectionState>()
			{
				ConnectionState.Executing,
				ConnectionState.Fetching,
				ConnectionState.Open
			};

			DbConnection connection = null;
			try
			{
				connection = GetOpenConnection();

				return (connection != null) && validStates.Contains(connection.State);
			}
			catch (Exception)
			{
				return false;
			}
			finally
			{
				CloseConnection(connection);
			}
		}

		/// <summary>Encerra a conexão.</summary>
		/// <param name="connection">Conexão que deve ser fechada.</param>
		private void CloseConnection(DbConnection connection)
			=> connection?.Close();

		/// <summary>Cria um novo <see cref="DbCommand"/> utilizando uma nova conexão.
		/// Caso o comando possua parâmetros, eles devem ser ser definidos usando '@' e seus
		/// respectivos valores informados individualmente.
		/// </summary>
		/// <example>
		/// Siga o exemplo:
		/// <code>
		/// string myQuery = "INSERT INTO htc1 VALUES (@C1_COD, @C1_NOME, @D_E_L_E_T)"
		/// ConnectionManager.CreateCommand(myQuery, 15, "Name", "NULL");
		/// </code>
		/// </example>
		/// <param name="query">Comando a ser executado.</param>
		/// <param name="parameterValue">Valor do(s) parâmetro(s) da query, se houver.</param>
		/// <returns>Novo <see cref="DbCommand"/> com o comando e parâmetros definidos.</returns>
		private DbCommand CreateCommand(string query, params object[] parameterValue)
		{
			Checker.NullOrEmptyString(query, nameof(query));

			DbConnection connection = GetOpenConnection();

			DbCommand command = connection.CreateCommand();
			string[] parameterName = GetParameterName(query);

			command.CommandText = query;
			AddCommandParameters(command, parameterName, parameterValue);

			return command;
		}

		/// <summary>Mapeia os tipos do C# para os tipos em <see cref="DbType"/>.</summary>
		private static Dictionary<Type, DbType> DbTypeDictionary()
		{
			var dictionary = new Dictionary<Type, DbType>
			{
				[typeof(byte)] = DbType.Byte,
				[typeof(sbyte)] = DbType.SByte,
				[typeof(short)] = DbType.Int16,
				[typeof(ushort)] = DbType.UInt16,
				[typeof(int)] = DbType.Int32,
				[typeof(uint)] = DbType.UInt32,
				[typeof(long)] = DbType.Int64,
				[typeof(ulong)] = DbType.UInt64,
				[typeof(float)] = DbType.Single,
				[typeof(double)] = DbType.Double,
				[typeof(decimal)] = DbType.Decimal,
				[typeof(bool)] = DbType.Boolean,
				[typeof(string)] = DbType.String,
				[typeof(char)] = DbType.StringFixedLength,
				[typeof(Guid)] = DbType.Guid,
				[typeof(DateTime)] = DbType.DateTime2,
				[typeof(DateTimeOffset)] = DbType.DateTimeOffset,
				[typeof(byte[])] = DbType.Binary,
				[typeof(byte?)] = DbType.Byte,
				[typeof(sbyte?)] = DbType.SByte,
				[typeof(short?)] = DbType.Int16,
				[typeof(ushort?)] = DbType.UInt16,
				[typeof(int?)] = DbType.Int32,
				[typeof(uint?)] = DbType.UInt32,
				[typeof(long?)] = DbType.Int64,
				[typeof(ulong?)] = DbType.UInt64,
				[typeof(float?)] = DbType.Single,
				[typeof(double?)] = DbType.Double,
				[typeof(decimal?)] = DbType.Decimal,
				[typeof(bool?)] = DbType.Boolean,
				[typeof(char?)] = DbType.StringFixedLength,
				[typeof(Guid?)] = DbType.Guid,
				[typeof(DateTime?)] = DbType.DateTime2,
				[typeof(DateTimeOffset?)] = DbType.DateTimeOffset
			};

			return dictionary;
		}

		/// <summary>Cria um <see cref="DbDataAdapter"/>. Caso a query possua parâmetros,
		/// eles devem ser ser definidos usando '@' e seus respectivos valores informados individualmente.
		/// </summary>
		/// <example>
		/// Siga o exemplo:
		/// <code>
		/// string myQuery = "INSERT INTO htc1 VALUES (@C1_COD, @C1_NOME, @D_E_L_E_T)"
		/// ConnectionManager.ExecuteAdapter(myQuery, 15, "Name", "NULL");
		/// </code>
		/// </example>
		/// <param name="query">Comando a ser executado.</param>
		/// <param name="parameterValue">Valor do(s) parâmetro(s) da query, se houver.</param>
		/// <returns>Dados retornados pela consulta em um <see cref="DbDataAdapter"/>.</returns>
		public DbDataAdapter ExecuteAdapter(string query, params object[] parameterValue)
		{
			DbCommand command = CreateCommand(query, parameterValue);
			return new MySqlDataAdapter(command as MySqlCommand);
		}

		/// <summary>
		/// Executa um comando que não exige retorno de dados. Caso a query possua parâmetros,
		/// eles devem ser ser definidos usando '@' e seus respectivos valores informados individualmente.
		/// </summary>
		/// <example>
		/// Siga o exemplo:
		/// <code>
		/// string myQuery = "INSERT INTO htc1 VALUES (@C1_COD, @C1_NOME, @D_E_L_E_T)"
		/// ConnectionManager.ExecuteQuery(myQuery, 15, "Name", "NULL");
		/// </code>
		/// </example>
		/// <param name="query">Comando a ser executado.</param>
		/// <param name="parameterValue">Valor do(s) parâmetro(s) da query, se houver.</param>
		public void ExecuteQuery(string query, params object[] parameterValue)
		{
			DbCommand command = CreateCommand(query, parameterValue);
			int attempts = 0;

			do
			{
				try
				{
					command.ExecuteNonQuery();
					break;
				}
				catch (DbException ex)
				{
					if (++attempts >= MaxAttempts)
					{
						throw new Exception(
							$"Número máximo de tentativas ({MaxAttempts}) de execução de query. " +
							$"Tente novamente mais tarde ou contate o administrador do sistema. " +
							$"\nÚltima tentativa: {ex.Message}");
					}

					Thread.Sleep(3000);
				}
			} while (attempts < MaxAttempts);

			CloseConnection(command.Connection);
		}

		/// <summary>
		/// Executa um comando e retorna os dados em um <see cref="DbDataReader"/>. Caso
		/// a query possua parâmetros, eles devem ser ser definidos usando '@' e seus respectivos
		/// valores informados individualmente.
		/// </summary>
		/// <example>
		/// Siga o exemplo:
		/// <code>
		/// string myQuery = "SELECT @param_1, @para_2, @param_3 FROM htc1"
		/// DbDataReader reader = ConnectionManager.ExecuteReader(myQuery, "C1_COD", "C1_NOME", "D_E_L_E_T");
		/// </code>
		/// </example>
		/// <param name="query">Comando a ser executado.</param>
		/// <param name="parameterValue">Valor do(s) parâmetro(s) da query, se houver.</param>
		/// <returns>Dados retornados pela consulta em um <see cref="DbDataReader"/>.</returns>
		public DbDataReader ExecuteReader(string query, params object[] parameterValue)
		{
			DbCommand command = CreateCommand(query, parameterValue);
			int attempts = 0;

			do
			{
				try
				{
					return command.ExecuteReader(CommandBehavior.CloseConnection);
				}
				catch (DbException)
				{
					if (++attempts >= MaxAttempts)
					{
						throw new Exception($"Número máximo de tentativas ({MaxAttempts}) de execução de query. " +
											$"Tente novamente mais tarde ou contate o administrador do sistema.");
					}

					Thread.Sleep(3000);
				}
			} while (attempts < MaxAttempts);

			return null;
		}

		/// <summary>
		/// Obtém uma nova conexão aberta com a base de dados. Caso não seja possível abrir a conexão
		/// e <see cref="MaxAttempts"/> foi atingido, uma exceção é lançada.
		///
		/// <para>As tentativas possuem um intervalo de 3 segundos entre elas.</para>
		/// </summary>
		/// <returns><see cref="DbConnection"/> com a conexão aberta.</returns>
		/// <exception cref="Exception">
		/// Quando não for possível abrir a conexão e <see cref="MaxAttempts"/> foi atingido.
		/// </exception>
		private DbConnection GetOpenConnection()
		{
			DbConnection connection = new MySqlConnection(ConnectionString);

			int attempts = 0;

			do
			{
				try
				{
					connection.Open();
					break;
				}
				catch (DbException)
				{
					if (++attempts >= MaxAttempts)
					{
						throw new Exception($"Número máximo de tentativas ({MaxAttempts}) de conexão atingido. " +
											$"Tente novamente mais tarde ou contate o administrador do sistema.");
					}

					Thread.Sleep(3000);
				}
			} while (attempts < MaxAttempts);

			return connection;
		}

		/// <summary>
		/// Recupera o nome dos parâmetros de uma query. Cada parâmetro é identificado com '@', seguido de seu nome.
		/// </summary>
		/// <example>
		/// A query "INSERT INTO htc1 VALUES (@C1_COD, @C1_NOME, @D_E_L_E_T)" possui os parâmetros nomeados
		/// C1_COD, C1_NOME e D_E_L_E_T.
		/// </example>
		/// <param name="query"></param>
		/// <returns></returns>
		private string[] GetParameterName(string query)
		{
			if (!query.Contains("@"))
			{
				return new string[0];
			}

			string[] queryWords = query
				.Split(new[] { ' ', ',', '.', '(', ')', '=', ';', '#' }, StringSplitOptions.RemoveEmptyEntries);

			var parameters = new List<string>();
			foreach (string word in queryWords)
			{
				if (word.StartsWith("@"))
				{
					parameters.Add(word);
				}
			}

			return parameters.ToArray();
		}
		#endregion
	}
}

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
		private static void AddCommandParameters(DbCommand command, string[] parameterName, string[] parameterValue)
		{
			if (command == null)
			{
				throw new ArgumentNullException(nameof(command), "Parâmetro obrigatório.");
			}

			if (parameterName.Length != parameterValue.Length)
			{
				throw new ArgumentException($"Número de parâmetros não são iguais: {parameterName.Length} na query" +
											$"e {parameterValue.Length} informados.");
			}

			command.Parameters.Clear();
			for (int i = 0; i < parameterName.Length; i++)
			{
				DbParameter parameter = command.CreateParameter();
				parameter.ParameterName = parameterName[i];
				parameter.Value = parameterValue[i];

				command.Parameters.Add(parameter);
			}
		}

		/// <summary>Encerra a conexão.</summary>
		/// <param name="connection">Conexão que deve ser fechada.</param>
		public static void CloseConnection(DbConnection connection) => connection?.Close();

		/// <summary>
		/// Cria um <see cref="DbDataAdapter"/> com a query a conexão padrão, pronto para
		/// ser executado.
		/// </summary>
		/// <param name="query">Comando a ser executado.</param>
		/// <param name="parameterValue">Valor do(s) parâmetro(s) da query, se houver.</param>
		/// <returns>Dados retornados pela consulta em um <see cref="DbDataAdapter"/>.</returns>
		public static DbDataAdapter ExecuteAdapter(string query, params string[] parameterValue)
			=> ExecuteAdapter(GetOpenConnection(), query, parameterValue);

		/// <summary>
		/// Cria um <see cref="DbDataAdapter"/> com a query a conexão especificados, pronto para
		/// ser executado.
		/// </summary>
		/// <param name="connection">Conexão onde a query deve ser executada.</param>
		/// <param name="query">Comando a ser executado.</param>
		/// <param name="parameterValue">Valor do(s) parâmetro(s) da query, se houver.</param>
		/// <returns>Dados retornados pela consulta em um <see cref="DbDataAdapter"/>.</returns>
		/// <exception cref="ArgumentNullException">
		/// Quando <see cref="query"/> for <see langword="null"/> ou estiver vazio.
		/// </exception>
		public static DbDataAdapter ExecuteAdapter(DbConnection connection, string query, params string[] parameterValue)
		{
			if (String.IsNullOrWhiteSpace(query))
			{
				throw new ArgumentNullException(nameof(query), "Parâmetro obrigatório.");
			}

			if (!IsConnectionOpen(connection))
			{
				OpenConnection(connection);
			}

			DbCommand command = connection.CreateCommand();
			string[] parameterName = GetParameterName(query);

			command.CommandText = query;
			AddCommandParameters(command, parameterName, parameterValue);

			return new MySqlDataAdapter(command as MySqlCommand);
		}

		/// <summary>
		/// Executa uma query em, uma nova conexão, que não exige retorno de dados. Caso a query
		/// possua parâmetros, eles devem ser ser definidos usando '@' e seus respectivos valores
		/// informados individualmente.
		/// </summary>
		/// <example>
		/// Siga o exemplo:
		/// <code>
		/// string myQuery = "INSERT INTO htc1 VALUES (@C1_COD, @C1_NOME, @D_E_L_E_T)"
		/// ConnectionManager.ExecuteQuery(myQuery, "15", "Name", "NULL");
		/// </code>
		/// </example>
		/// <param name="query">Comando a ser executado.</param>
		/// <param name="parameterValue">Valor do(s) parâmetro(s) da query, se houver.</param>
		public static void ExecuteQuery(string query, params string[] parameterValue)
			=> ExecuteQuery(GetOpenConnection(), query, parameterValue);

		/// <summary>
		/// Executa uma query que não exige retorno de dados. Caso a query possua parâmetros, eles
		/// devem ser ser definidos usando '@' e seus respectivos valores informados individualmente.
		/// </summary>
		/// <example>
		/// Siga o exemplo:
		/// <code>
		/// DbConnection connection = ConnectionManager.GetOpenConnection()
		/// string myQuery = "INSERT INTO htc1 VALUES (@C1_COD, @C1_NOME, @D_E_L_E_T)"
		/// ConnectionManager.ExecuteQuery(connection, myQuery, "15", "Name", "NULL");
		/// </code>
		/// </example>
		/// <param name="connection">Conexão onde a query deve ser executada.</param>
		/// <param name="query">Comando a ser executado.</param>
		/// <param name="parameterValue">Valor do(s) parâmetro(s) da query, se houver.</param>
		/// <exception cref="ArgumentNullException">
		/// Quando <see cref="query"/> for <see langword="null"/> ou estiver vazio.
		/// </exception>
		public static void ExecuteQuery(DbConnection connection, string query, params string[] parameterValue)
		{
			if (String.IsNullOrWhiteSpace(query))
			{
				throw new ArgumentNullException(nameof(query), "Parâmetro obrigatório.");
			}

			if (!IsConnectionOpen(connection))
			{
				OpenConnection(connection);
			}

			DbCommand command = connection.CreateCommand();
			string[] parameterName = GetParameterName(query);

			command.CommandText = query;
			AddCommandParameters(command, parameterName, parameterValue);

			int attempts = 0;
			do
			{
				try
				{
					command.ExecuteNonQuery();
					break;
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

			CloseConnection(connection);
		}

		/// <summary>
		/// Executa uma query, em uma nova conexão, e retorna seu <see cref="DbDataReader"/>. Caso
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
		public static DbDataReader ExecuteReader(string query, params string[] parameterValue)
			=> ExecuteReader(GetOpenConnection(), query, parameterValue);

		/// <summary>
		/// Executa uma query e retorna seu <see cref="DbDataReader"/>. Caso a query possua parâmetros,
		/// eles devem ser ser definidos usando '@' e seus respectivos valores informados individualmente.
		/// </summary>
		/// <example>
		/// Siga o exemplo:
		/// <code>
		/// DbConnection connection = ConnectionManager.GetOpenConnection()
		/// string myQuery = "SELECT @param_1, @para_2, @param_3 FROM htc1"
		/// DbDataReader reader = ConnectionManager.ExecuteReader(connection, myQuery, "C1_COD", "C1_NOME", "D_E_L_E_T");
		/// </code>
		/// </example>
		/// <param name="connection">Conexão onde a query deve ser executada.</param>
		/// <param name="query">Comando a ser executado.</param>
		/// <param name="parameterValue">Valor do(s) parâmetro(s) da query, se houver.</param>
		/// <returns>Dados retornados pela consulta em um <see cref="DbDataReader"/>.</returns>
		/// <exception cref="ArgumentNullException">
		/// Quando <see cref="query"/> for <see langword="null"/> ou estiver vazio.
		/// </exception>
		public static DbDataReader ExecuteReader(DbConnection connection, string query, params string[] parameterValue)
		{
			if (String.IsNullOrWhiteSpace(query))
			{
				throw new ArgumentNullException(nameof(query), "Parâmetro obrigatório.");
			}

			if (!IsConnectionOpen(connection))
			{
				OpenConnection(connection);
			}

			DbCommand command = connection.CreateCommand();
			string[] parameterName = GetParameterName(query);

			command.CommandText = query;
			AddCommandParameters(command, parameterName, parameterValue);

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

		/// <summary>Obtém uma nova conexão, não iniciada, com a base de dados.</summary>
		/// <returns><see cref="DbConnection"/> com a conexão configurada mas não aberta.</returns>
		public static DbConnection GetConnection()
			=> GetConnection(ConfigurationManager.ConnectionStrings["MySQL"].ConnectionString);

		/// <summary>
		/// Obtém uma nova conexão, não iniciada, com a base de dados utilizando uma string de conexão específica.
		/// </summary>
		/// <param name="connectionString">String de conexão.</param>
		/// <returns><see cref="DbConnection"/> com a conexão configurada mas não aberta.</returns>
		public static DbConnection GetConnection(string connectionString)
		{
			if (String.IsNullOrWhiteSpace(connectionString))
			{
				throw new ArgumentNullException(nameof(connectionString), "Parâmetro não pode ser null.");
			}

			return new MySqlConnection(connectionString);
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
		public static DbConnection GetOpenConnection()
			=> GetOpenConnection(ConfigurationManager.ConnectionStrings["MySQL"].ConnectionString);

		/// <summary>
		/// Obtém uma nova conexão aberta com a base de dados utilizando uma string de conexão específica.
		/// </summary>
		/// <param name="connectionString">String de conexão a ser usada.</param>
		/// <returns><see cref="DbConnection"/> com a conexão aberta.</returns>
		public static DbConnection GetOpenConnection(string connectionString)
		{
			DbConnection connection = GetConnection(connectionString);

			OpenConnection(connection);

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
		private static string[] GetParameterName(string query)
		{
			if (!query.Contains("@"))
			{
				return new string[0];
			}

			string[] queryWords = query
				.Split(new char[] { ' ', ',', '(', ')', '=', ';' }, StringSplitOptions.RemoveEmptyEntries);

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

		/// <summary>Determina se a conexão está aptar a ser usada.</summary>
		/// <param name="connection">A conexão a ser testada.</param>
		/// <returns>
		/// <see langword="true" /> se a conexão estiver aberta, <see langword="false" /> em qualquer outro caso.
		/// </returns>
		public static bool IsConnectionOpen(DbConnection connection)
		{
			var validStates = new List<ConnectionState>()
			{
				ConnectionState.Executing,
				ConnectionState.Fetching,
				ConnectionState.Open
			};

			return (connection != null) && validStates.Contains(connection.State);
		}

		/// <summary>
		/// Abre uma conexão. Caso não seja possível abrir a conexão e <see cref="MaxAttempts"/> foi atingido,
		/// uma exceção é lançada.
		///
		/// <para>As tentativas possuem um intervalo de 3 segundos entre elas.</para>
		/// </summary>
		/// <param name="connection">Conexão que deve ser aberta.</param>
		/// <exception cref="ArgumentNullException">
		/// Quando <see cref="connection"/> for <see langword="null"/>.
		/// </exception>
		/// <exception cref="Exception">
		/// Quando não for possível abrir a conexão e <see cref="MaxAttempts"/> foi atingido.
		/// </exception>
		public static void OpenConnection(DbConnection connection)
		{
			if (connection == null)
			{
				throw new ArgumentNullException(nameof(connection), "Parâmetro não pode ser null.");
			}

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
		}
		#endregion
	}
}

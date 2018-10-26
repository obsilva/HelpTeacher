// This Source Code Form is subject to the terms of the Mozilla 
// Public License, v. 2.0. If a copy of the MPL was not distributed 
// with this file, You can obtain one at https://mozilla.org/MPL/2.0/

// Since: 2018/09/19
// Authors: 
//		Otávio Bueno Silva <obsilva94@gmail.com>

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

using HelpTeacher.Domain.Entities;
using HelpTeacher.Repository.IRepositories;

namespace HelpTeacher.Repository.Repositories
{
	/// <inheritdoc />
	public class CourseRepository : ICourseRepository
	{
		#region Constants
		private const string QueryInsert = "INSERT INTO htc1 (C1_COD, C1_NOME, D_E_L_E_T) VALUES (NULL, @C1_NOME, 0);";

		private const string QuerySelect = "SELECT C1_COD, C1_NOME, D_E_L_E_T FROM htc1 LIMIT @LIMIT OFFSET @OFFSET;";

		private const string QuerySelectActive = "SELECT C1_COD, C1_NOME, D_E_L_E_T FROM htc1 WHERE D_E_L_E_T = @IS_ACTIVE LIMIT @LIMIT OFFSET @OFFSET;";

		private const string QuerySelectFirst = "SELECT C1_COD, C1_NOME, D_E_L_E_T FROM htc1 LIMIT 1;";

		private const string QuerySelectDifferentID = "SELECT C1_COD, C1_NOME, D_E_L_E_T FROM htc1 WHERE C1_COD <> @C1_COD AND D_E_L_E_T = 0  LIMIT @LIMIT OFFSET @OFFSET;;";

		private const string QuerySelectID = "SELECT C1_COD, C1_NOME, D_E_L_E_T FROM htc1 WHERE C1_COD = @C1_COD;";

		private const string QueryUpdate = "UPDATE htc1 SET C1_NOME = @C1_NOME, D_E_L_E_T = @IS_ACTIVE WHERE C1_COD = @C1_COD";
		#endregion


		#region Fields
		private readonly DbConnection _connection;

		private readonly int _pageSize;
		#endregion


		#region Properties
		public int Offset { get; set; }
		#endregion


		#region Constructors
		/// <summary>
		/// Inicializa uma nova instância de <see cref="CourseRepository"/>. É possível definir a conexão
		/// a ser usada e/ou o tamanho da página de registros.
		/// </summary>
		/// <param name="connection">Conexão a ser usada.</param>
		/// <param name="pageSize">Número máximo de registros para retornar por vez.</param>
		public CourseRepository(DbConnection connection = null, int pageSize = 50)
		{
			Offset = 0;

			try
			{
				if (!ConnectionManager.IsConnectionOpen(connection))
				{
					ConnectionManager.OpenConnection(connection);
				}
			}
			catch (ArgumentNullException)
			{
				connection = ConnectionManager.GetOpenConnection();
			}

			_connection = connection;
			_pageSize = pageSize;
		}
		#endregion


		#region Class Methods
		/// <summary>Faz a leitura do <see cref="DbDataReader"/>.</summary>
		/// <param name="dataReader">Objeto para ler.</param>
		/// <returns>Todos os objetos no <see cref="DbDataReader"/>.</returns>
		private static IQueryable<Course> ReadDataReader(DbDataReader dataReader)
		{
			var output = new List<Course>();

			if (dataReader.HasRows)
			{
				while (dataReader.Read())
				{
					output.Add(new Course(dataReader.GetString(1))
					{
						IsRecordActive = (dataReader.GetInt16(2) == 0),
						RecordID = dataReader.GetInt32(0)
					});
				}
			}

			dataReader.Close();
			return output.AsQueryable();
		}
		#endregion


		#region Instance Methods
		/// <inheritdoc />
		public void Add(Course obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException(nameof(obj), "Parâmetro obrigatório.");
			}

			ConnectionManager.ExecuteQuery(_connection, QueryInsert, obj.Name);
		}

		/// <inheritdoc />
		public void Add(IEnumerable<Course> collection)
		{
			foreach (Course obj in collection)
			{
				Add(obj);
			}
		}

		/// <inheritdoc />
		public Course First()
		{
			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(_connection, QuerySelectFirst))
			{
				IQueryable<Course> records = ReadDataReader(dataReader);

				return records.FirstOrDefault() ?? new Course("");
			}
		}

		/// <inheritdoc />
		public IQueryable<Course> Get()
		{
			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(
				_connection, QuerySelect, _pageSize, Offset))
			{
				return ReadDataReader(dataReader);
			}
		}

		/// <inheritdoc />
		public IQueryable<Course> Get(bool isRecordActive)
		{
			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(
				_connection, QuerySelectActive, isRecordActive, _pageSize, Offset))
			{
				return ReadDataReader(dataReader);
			}
		}

		/// <inheritdoc />
		public Course Get(int id)
		{
			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(
				_connection, QuerySelectID, id))
			{
				IQueryable<Course> records = ReadDataReader(dataReader);

				return records.FirstOrDefault() ?? new Course("");
			}
		}

		/// <inheritdoc />
		public IQueryable<Course> GetWhereNotID(Course obj)
			=> GetWhereNotID(obj.RecordID);

		/// <inheritdoc />
		public IQueryable<Course> GetWhereNotID(int id)
		{
			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(
				_connection, QuerySelectDifferentID, id, _pageSize, Offset))
			{
				return ReadDataReader(dataReader);
			}
		}

		/// <inheritdoc />
		public void Update(Course obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException(nameof(obj), "Parâmetro obrigatório.");
			}

			ConnectionManager.ExecuteQuery(_connection, QueryUpdate, obj.Name, obj.IsRecordActive, obj.RecordID);
		}

		/// <inheritdoc />
		public void Update(IEnumerable<Course> collection)
		{
			foreach (Course obj in collection)
			{
				Update(obj);
			}
		}
		#endregion
	}
}

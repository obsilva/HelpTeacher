// This Source Code Form is subject to the terms of the Mozilla 
// Public License, v. 2.0. If a copy of the MPL was not distributed 
// with this file, You can obtain one at https://mozilla.org/MPL/2.0/

// Since: 2018/09/19
// Authors: 
//		Otávio Bueno Silva <obsilva94@gmail.com>

using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

using HelpTeacher.Domain.Entities;
using HelpTeacher.Repository.IRepositories;
using HelpTeacher.Util;

namespace HelpTeacher.Repository.Repositories
{
	/// <inheritdoc />
	public class CourseRepository : ICourseRepository
	{
		#region Constants
		private const string QueryInsert = "INSERT INTO htc1 (C1_COD, C1_NOME, D_E_L_E_T) VALUES (NULL, @C1_NOME, 0);";

		private const string QuerySelect = "SELECT C1_COD, C1_NOME, D_E_L_E_T FROM htc1 LIMIT @LIMIT OFFSET @OFFSET;";

		private const string QuerySelectActive = "SELECT C1_COD, C1_NOME, D_E_L_E_T FROM htc1 WHERE D_E_L_E_T = @IS_DELETED LIMIT @LIMIT OFFSET @OFFSET;";

		private const string QuerySelectDifferentID = "SELECT C1_COD, C1_NOME, D_E_L_E_T FROM htc1 WHERE C1_COD <> @C1_COD AND D_E_L_E_T = 0 LIMIT @LIMIT OFFSET @OFFSET;";

		private const string QuerySelectID = "SELECT C1_COD, C1_NOME, D_E_L_E_T FROM htc1 WHERE C1_COD = @C1_COD;";

		private const string QueryUpdate = "UPDATE htc1 SET C1_NOME = @C1_NOME, D_E_L_E_T = @IS_DELETED WHERE C1_COD = @C1_COD";
		#endregion


		#region Properties
		/// <summary>Gerenciador de conexão.</summary>
		public ConnectionManager ConnectionManager { get; set; }

		/// <summary>Valor de offset na recuperação de registros.</summary>
		public int Offset { get; set; }

		/// <summary>Tamanho da página de registros.</summary>
		public int PageSize { get; set; }
		#endregion


		#region Constructors
		/// <summary>
		/// Inicializa uma nova instância de <see cref="CourseRepository"/>. É possível definir o
		/// gerenciador conexão a ser usado e/ou o tamanho da página de registros.
		/// </summary>
		/// <param name="connectionManager">Gerenciador de conexão a ser usado.</param>
		/// <param name="pageSize">Número máximo de registros para retornar por vez.</param>
		public CourseRepository(ConnectionManager connectionManager = null, int pageSize = 50)
		{
			if (connectionManager == null)
			{
				connectionManager = new ConnectionManager();
			}

			ConnectionManager = connectionManager;
			Offset = 0;
			PageSize = pageSize;
		}
		#endregion


		#region Methods
		/// <inheritdoc />
		public void Add(Course obj)
		{
			Checker.NullObject(obj, nameof(obj));

			ConnectionManager.ExecuteQuery(QueryInsert, obj.Name);
		}

		/// <inheritdoc />
		public void Add(IEnumerable<Course> collection)
		{
			Checker.NullObject(collection, nameof(collection));

			foreach (Course obj in collection)
			{
				Add(obj);
			}
		}

		/// <inheritdoc />
		public Course First()
		{
			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(QuerySelect, 1, 0))
			{
				IQueryable<Course> records = ReadDataReader(dataReader);

				return records.FirstOrDefault() ?? Course.Null;
			}
		}

		/// <inheritdoc />
		public IQueryable<Course> Get()
		{
			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(QuerySelect, PageSize, Offset))
			{
				return ReadDataReader(dataReader);
			}
		}

		/// <inheritdoc />
		public IQueryable<Course> Get(bool isRecordActive)
		{
			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(QuerySelectActive,
				!isRecordActive, PageSize, Offset))
			{
				return ReadDataReader(dataReader);
			}
		}

		/// <inheritdoc />
		public Course Get(int id)
		{
			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(QuerySelectID, id))
			{
				IQueryable<Course> records = ReadDataReader(dataReader);

				return records.FirstOrDefault() ?? Course.Null;
			}
		}

		/// <inheritdoc />
		public IQueryable<Course> GetWhereNotID(Course obj)
			=> (obj == null) ? new List<Course>().AsQueryable() : GetWhereNotID(obj.RecordID);

		/// <inheritdoc />
		public IQueryable<Course> GetWhereNotID(int id)
		{
			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(QuerySelectDifferentID,
				id, PageSize, Offset))
			{
				return ReadDataReader(dataReader);
			}
		}

		/// <summary>Faz a leitura do <see cref="DbDataReader"/>.</summary>
		/// <param name="dataReader">Objeto para ler.</param>
		/// <returns>Todos os objetos no <see cref="DbDataReader"/>.</returns>
		private IQueryable<Course> ReadDataReader(DbDataReader dataReader)
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

		/// <inheritdoc />
		public void Update(Course obj)
		{
			Checker.NullObject(obj, nameof(obj));

			ConnectionManager.ExecuteQuery(QueryUpdate, obj.Name, !obj.IsRecordActive, obj.RecordID);
		}

		/// <inheritdoc />
		public void Update(IEnumerable<Course> collection)
		{
			Checker.NullObject(collection, nameof(collection));

			foreach (Course obj in collection)
			{
				Update(obj);
			}
		}
		#endregion
	}
}

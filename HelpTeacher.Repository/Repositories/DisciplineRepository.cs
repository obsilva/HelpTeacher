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
	public class DisciplineRepository : IDisciplineRepository
	{
		#region Constants
		private const string QueryInsert = "INSERT INTO htc2 (C2_COD, C2_NOME, C2_CURSO, D_E_L_E_T) VALUES (NULL, @C2_NOME, @C2_CURSO, 0);";

		private const string QuerySelect = "SELECT C2_COD, C2_NOME, C2_CURSO, D_E_L_E_T FROM htc2 LIMIT @LIMIT OFFSET @OFFSET;";

		private const string QuerySelectActive = "SELECT C2_COD, C2_NOME, C2_CURSO, D_E_L_E_T FROM htc2 WHERE D_E_L_E_T = @IS_DELETED LIMIT @LIMIT OFFSET @OFFSET;";

		private const string QuerySelectCourse = "SELECT C2_COD, C2_NOME, C2_CURSO, D_E_L_E_T FROM htc2 WHERE C2_CURSO = @C2_CURSO LIMIT @LIMIT OFFSET @OFFSET;";

		private const string QuerySelectCourseAndActive = "SELECT C2_COD, C2_NOME, C2_CURSO, D_E_L_E_T FROM htc2 WHERE C2_CURSO = @C2_CURSO AND D_E_L_E_T = @IS_DELETED LIMIT @LIMIT OFFSET @OFFSET;";

		private const string QuerySelectDifferentID = "SELECT C2_COD, C2_NOME, C2_CURSO, D_E_L_E_T FROM htc2 WHERE C2_COD <> @C2_COD AND D_E_L_E_T = 0 LIMIT @LIMIT OFFSET @OFFSET;";

		private const string QuerySelectID = "SELECT C2_COD, C2_NOME, C2_CURSO, D_E_L_E_T FROM htc2 WHERE C2_COD = @C2_COD;";

		private const string QueryUpdate = "UPDATE htc2 SET C2_NOME = @C2_NOME, C2_CURSO = @C2_CURSO, D_E_L_E_T = @IS_DELETED WHERE C2_COD = @C2_COD";
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
		/// Inicializa uma nova instância de <see cref="DisciplineRepository"/>. É possível definir o
		/// gerenciador conexão a ser usado e/ou o tamanho da página de registros.
		/// </summary>
		/// <param name="connectionManager">Gerenciador de conexão a ser usado.</param>
		/// <param name="pageSize">Número máximo de registros para retornar por vez.</param>
		public DisciplineRepository(ConnectionManager connection = null, int pageSize = 50)
		{
			if (connection == null)
			{
				connection = new ConnectionManager();
			}

			ConnectionManager = connection;
			Offset = 0;
			PageSize = pageSize;
		}
		#endregion


		#region Methods
		/// <inheritdoc />
		public void Add(Discipline obj)
		{
			Checker.NullObject(obj, nameof(obj));

			ConnectionManager.ExecuteQuery(QueryInsert, obj.Name, obj.Course?.RecordID);
		}

		/// <inheritdoc />
		public void Add(IEnumerable<Discipline> collection)
		{
			Checker.NullObject(collection, nameof(collection));

			foreach (Discipline obj in collection)
			{
				Add(obj);
			}
		}

		/// <inheritdoc />
		public Discipline First()
		{
			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(QuerySelect, 1, 0))
			{
				IQueryable<Discipline> records = ReadDataReader(dataReader);

				return records.FirstOrDefault() ?? Discipline.Null;
			}
		}

		/// <inheritdoc />
		public IQueryable<Discipline> Get()
		{
			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(QuerySelect, PageSize, Offset))
			{
				return ReadDataReader(dataReader);
			}
		}

		/// <inheritdoc />
		public IQueryable<Discipline> Get(bool isRecordActive)
		{
			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(QuerySelectActive,
				!isRecordActive, PageSize, Offset))
			{
				return ReadDataReader(dataReader);
			}
		}

		/// <inheritdoc />
		public Discipline Get(int id)
		{
			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(QuerySelectID, id))
			{
				IQueryable<Discipline> records = ReadDataReader(dataReader);

				return records.FirstOrDefault() ?? Discipline.Null;
			}
		}

		/// <inheritdoc />
		public IQueryable<Discipline> GetWhereCourse(Course obj)
			=> (obj == null) ? new List<Discipline>().AsQueryable() : GetWhereCourse(obj.RecordID);

		/// <inheritdoc />
		public IQueryable<Discipline> GetWhereCourse(int id)
		{
			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(QuerySelectCourse,
				id, PageSize, Offset))
			{
				return ReadDataReader(dataReader);
			}
		}

		/// <inheritdoc />
		public IQueryable<Discipline> GetWhereCourse(Course obj, bool isRecordActive)
			=> (obj == null) ? new List<Discipline>().AsQueryable() : GetWhereCourse(obj.RecordID, !isRecordActive);

		/// <inheritdoc />
		public IQueryable<Discipline> GetWhereCourse(int id, bool isRecordActive)
		{
			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(QuerySelectCourseAndActive,
				id, !isRecordActive, PageSize, Offset))
			{
				return ReadDataReader(dataReader);
			}
		}

		/// <inheritdoc />
		public IQueryable<Discipline> GetWhereNotID(Discipline obj)
			=> (obj == null) ? new List<Discipline>().AsQueryable() : GetWhereNotID(obj.RecordID);

		/// <inheritdoc />
		public IQueryable<Discipline> GetWhereNotID(int id)
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
		private IQueryable<Discipline> ReadDataReader(DbDataReader dataReader)
		{
			var output = new List<Discipline>();

			if (dataReader.HasRows)
			{
				while (dataReader.Read())
				{
					Course course = new CourseRepository(ConnectionManager).Get(dataReader.GetInt32(2));
					output.Add(new Discipline(course, dataReader.GetString(1))
					{
						IsRecordActive = (dataReader.GetInt16(3) == 0),
						RecordID = dataReader.GetInt32(0)
					});
				}
			}

			dataReader.Close();
			return output.AsQueryable();
		}

		/// <inheritdoc />
		public void Update(Discipline obj)
		{
			Checker.NullObject(obj, nameof(obj));

			ConnectionManager.ExecuteQuery(QueryUpdate, obj.Name, obj.Course.RecordID,
				!obj.IsRecordActive, obj.RecordID);
		}

		/// <inheritdoc />
		public void Update(IEnumerable<Discipline> collection)
		{
			Checker.NullObject(collection, nameof(collection));

			foreach (Discipline obj in collection)
			{
				Update(obj);
			}
		}
		#endregion
	}
}

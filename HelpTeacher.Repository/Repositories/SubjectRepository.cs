﻿// This Source Code Form is subject to the terms of the Mozilla 
// Public License, v. 2.0. If a copy of the MPL was not distributed 
// with this file, You can obtain one at https://mozilla.org/MPL/2.0/

// Since: 2018/09/20
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
	public class SubjectRepository : ISubjectRepository
	{
		#region Constants
		private const string QueryInsert = "INSERT INTO htc3 (C3_COD, C3_NOME, C3_DISCIPL, D_E_L_E_T) VALUES (NULL, @C3_NOME, @C3_DISCIPL, 0);";

		private const string QuerySelect = "SELECT C3_COD, C3_NOME, C3_DISCIPL, D_E_L_E_T FROM htc3 LIMIT @LIMIT OFFSET @OFFSET;";

		private const string QuerySelectActive = "SELECT C3_COD, C3_NOME, C3_DISCIPL, D_E_L_E_T FROM htc3 WHERE D_E_L_E_T = @IS_DELETED LIMIT @LIMIT OFFSET @OFFSET;";

		private const string QuerySelectDiscipline = "SELECT C3_COD, C3_NOME, C3_DISCIPL, D_E_L_E_T FROM htc3 WHERE C3_DISCIPL = @C3_DISCIPL LIMIT @LIMIT OFFSET @OFFSET;";

		private const string QuerySelectDisciplineAndActive = "SELECT C3_COD, C3_NOME, C3_DISCIPL, D_E_L_E_T FROM htc3 WHERE C3_DISCIPL = @C3_DISCIPL AND D_E_L_E_T = @IS_DELETED LIMIT @LIMIT OFFSET @OFFSET;";

		private const string QuerySelectID = "SELECT C3_COD, C3_NOME, C3_DISCIPL, D_E_L_E_T FROM htc3 WHERE C3_COD = @C3_COD;";

		private const string QueryUpdate = "UPDATE htc3 SET C3_NOME = @C3_NOME, C3_DISCIPL = @C3_DISCIPL, D_E_L_E_T = @IS_DELETED WHERE C3_COD = @C3_COD";
		#endregion


		#region Properties
		/// <summary>Gerenciador de conexão.</summary>
		public ConnectionManager Connection { get; set; }

		/// <summary>Valor de offset na recuperação de registros.</summary>
		public int Offset { get; set; }

		/// <summary>Tamanho da página de registros.</summary>
		public int PageSize { get; set; }
		#endregion


		#region Constructors
		/// <summary>
		/// Inicializa uma nova instância de <see cref="SubjectRepository"/>. É possível definir o
		/// gerenciador conexão a ser usado e/ou o tamanho da página de registros.
		/// </summary>
		/// <param name="connection">Gerenciador de conexão a ser usado.</param>
		/// <param name="pageSize">Número máximo de registros para retornar por vez.</param>
		public SubjectRepository(ConnectionManager connection = null, int pageSize = 50)
		{
			if (connection == null)
			{
				connection = new ConnectionManager();
			}

			Connection = connection;
			Offset = 0;
			PageSize = pageSize;
		}
		#endregion


		#region Methods

		/// <inheritdoc />
		public void Add(Subject obj)
		{
			Checker.NullObject(obj, nameof(obj));

			Connection.ExecuteQuery(QueryInsert, obj.Name, obj.Discipline?.RecordID);
		}

		/// <inheritdoc />
		public void Add(IEnumerable<Subject> collection)
		{
			Checker.NullObject(collection, nameof(collection));

			foreach (Subject obj in collection)
			{
				Add(obj);
			}
		}

		/// <inheritdoc />
		public Subject First()
		{
			using (DbDataReader dataReader = Connection.ExecuteReader(QuerySelect, 1, 0))
			{
				IQueryable<Subject> records = ReadDataReader(dataReader);

				return records.FirstOrDefault() ?? Subject.Null;
			}
		}

		/// <inheritdoc />
		public IQueryable<Subject> Get()
		{
			using (DbDataReader dataReader = Connection.ExecuteReader(QuerySelect, PageSize, Offset))
			{
				return ReadDataReader(dataReader);
			}
		}

		/// <inheritdoc />
		public IQueryable<Subject> Get(bool isRecordActive)
		{
			using (DbDataReader dataReader = Connection.ExecuteReader(QuerySelectActive,
				!isRecordActive, PageSize, Offset))
			{
				return ReadDataReader(dataReader);
			}
		}

		/// <inheritdoc />
		public Subject Get(int id)
		{
			using (DbDataReader dataReader = Connection.ExecuteReader(QuerySelectID, id))
			{
				IQueryable<Subject> records = ReadDataReader(dataReader);

				return records.FirstOrDefault() ?? Subject.Null;
			}
		}

		/// <inheritdoc />
		public IQueryable<Subject> GetWhereDiscipline(Discipline obj)
			=> (obj == null) ? new List<Subject>().AsQueryable() : GetWhereDiscipline(obj.RecordID);

		/// <inheritdoc />
		public IQueryable<Subject> GetWhereDiscipline(int id)
		{
			using (DbDataReader dataReader = Connection.ExecuteReader(QuerySelectDiscipline,
				id, PageSize, Offset))
			{
				return ReadDataReader(dataReader);
			}
		}

		/// <inheritdoc />
		public IQueryable<Subject> GetWhereDiscipline(Discipline obj, bool isRecordActive)
			=> (obj == null) ? new List<Subject>().AsQueryable() : GetWhereDiscipline(obj.RecordID, isRecordActive);

		/// <inheritdoc />
		public IQueryable<Subject> GetWhereDiscipline(int id, bool isRecordActive)
		{
			using (DbDataReader dataReader = Connection.ExecuteReader(QuerySelectDisciplineAndActive,
				id, !isRecordActive, PageSize, Offset))
			{
				return ReadDataReader(dataReader);
			}
		}

		/// <summary>Faz a leitura do <see cref="DbDataReader"/>.</summary>
		/// <param name="dataReader">Objeto para ler.</param>
		/// <returns>Todos os objetos no <see cref="DbDataReader"/>.</returns>
		private IQueryable<Subject> ReadDataReader(DbDataReader dataReader)
		{
			var output = new List<Subject>();
			if (dataReader.HasRows)
			{
				while (dataReader.Read())
				{
					Discipline discipline = new DisciplineRepository(Connection).Get(dataReader.GetInt32(2));
					output.Add(new Subject(discipline, dataReader.GetString(1))
					{
						IsRecordActive = (dataReader.GetInt32(3) == 0),
						RecordID = dataReader.GetInt32(0)
					});
				}
			}

			dataReader.Close();
			return output.AsQueryable();
		}

		/// <inheritdoc />
		public void Update(Subject obj)
		{
			Checker.NullObject(obj, nameof(obj));

			Connection.ExecuteQuery(QueryUpdate, obj.Name, obj.Discipline.RecordID,
				!obj.IsRecordActive, obj.RecordID);
		}

		/// <inheritdoc />
		public void Update(IEnumerable<Subject> collection)
		{
			Checker.NullObject(collection, nameof(collection));

			foreach (Subject obj in collection)
			{
				Update(obj);
			}
		}
		#endregion
	}
}

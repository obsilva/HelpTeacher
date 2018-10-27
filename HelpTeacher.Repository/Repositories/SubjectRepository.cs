// This Source Code Form is subject to the terms of the Mozilla 
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

namespace HelpTeacher.Repository.Repositories
{
	/// <inheritdoc />
	public class SubjectRepository : ISubjectRepository
	{
		#region Constructors
		public SubjectRepository() { }
		#endregion


		#region Methods

		/// <inheritdoc />
		public void Add(Subject obj)
		{
			string query = $"INSERT INTO htc3 (C3_COD, C3_NOME, C3_DISCIPL, D_E_L_E_T) VALUES (NULL, " +
						   $"'{obj.Name}', {obj.Discipline?.RecordID}, NULL)";
			ConnectionManager.ExecuteQuery(query);
		}

		/// <inheritdoc />
		public void Add(IEnumerable<Subject> collection)
		{
			foreach (Subject obj in collection)
			{
				Add(obj);
			}
		}

		/// <inheritdoc />
		public Subject First()
		{
			string query = "SELECT C3_COD, C3_NOME, C3_DISCIPL, D_E_L_E_T FROM htc3 LIMIT 1";

			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(query))
			{
				var output = new Subject(null, "");
				if (dataReader.HasRows)
				{
					dataReader.Read();

					output.Discipline = new DisciplineRepository().Get(dataReader.GetInt32(2));
					output.Name = dataReader.GetString(1);
					output.IsRecordActive = dataReader.IsDBNull(3);
					output.RecordID = dataReader.GetInt32(0);
				}

				return output;
			}
		}

		/// <inheritdoc />
		public IQueryable<Subject> Get()
		{
			string query = "SELECT C3_COD, C3_NOME, C3_DISCIPL, D_E_L_E_T FROM htc3";

			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(query))
			{
				var output = new List<Subject>();
				if (dataReader.HasRows)
				{
					while (dataReader.Read())
					{
						output.Add(new Subject(new DisciplineRepository().Get(dataReader.GetInt32(2)), dataReader.GetString(1))
						{
							IsRecordActive = dataReader.IsDBNull(3),
							RecordID = dataReader.GetInt32(0)
						});
					}
				}

				return output.AsQueryable();
			}
		}

		/// <inheritdoc />
		public IQueryable<Subject> Get(bool isRecordActive)
		{
			string query = $"SELECT C3_COD, C3_NOME, C3_DISCIPL, D_E_L_E_T FROM htc3 " +
						   $"WHERE D_E_L_E_T { (isRecordActive ? "IS" : "IS NOT")} NULL";

			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(query))
			{
				var output = new List<Subject>();
				if (dataReader.HasRows)
				{
					while (dataReader.Read())
					{
						output.Add(new Subject(new DisciplineRepository().Get(dataReader.GetInt32(2)), dataReader.GetString(1))
						{
							IsRecordActive = dataReader.IsDBNull(3),
							RecordID = dataReader.GetInt32(0)
						});
					}
				}

				return output.AsQueryable();
			}
		}

		/// <inheritdoc />
		public Subject Get(int id)
		{
			string query = $"SELECT C3_COD, C3_NOME, C3_DISCIPL, D_E_L_E_T FROM htc3 WHERE C3_COD = {id}";

			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(query))
			{
				var output = new Subject(null, "");
				if (dataReader.HasRows)
				{
					dataReader.Read();

					output.Discipline = new DisciplineRepository().Get(dataReader.GetInt32(2));
					output.Name = dataReader.GetString(1);
					output.IsRecordActive = dataReader.IsDBNull(3);
					output.RecordID = dataReader.GetInt32(0);
				}

				return output;
			}
		}

		/// <inheritdoc />
		public IQueryable<Subject> GetWhereDiscipline(Discipline obj)
			=> GetWhereDiscipline(obj.RecordID);

		/// <inheritdoc />
		public IQueryable<Subject> GetWhereDiscipline(int id)
		{
			string query = $"SELECT C3_COD, C3_NOME, C3_DISCIPL, D_E_L_E_T FROM htc3 WHERE C3_DISCIPL = {id}";

			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(query))
			{
				var output = new List<Subject>();
				if (dataReader.HasRows)
				{
					while (dataReader.Read())
					{
						output.Add(new Subject(new DisciplineRepository().Get(dataReader.GetInt32(2)), dataReader.GetString(1))
						{
							IsRecordActive = dataReader.IsDBNull(3),
							RecordID = dataReader.GetInt32(0)
						});
					}
				}

				return output.AsQueryable();
			}
		}

		/// <inheritdoc />
		public IQueryable<Subject> GetWhereDiscipline(Discipline obj, bool isRecordActive)
			=> GetWhereDiscipline(obj.RecordID, isRecordActive);

		/// <inheritdoc />
		public IQueryable<Subject> GetWhereDiscipline(int id, bool isRecordActive)
		{
			string query = $"SELECT C3_COD, C3_NOME, C3_DISCIPL, D_E_L_E_T FROM htc3 WHERE C3_DISCIPL = {id} " +
						   $"AND D_E_L_E_T { (isRecordActive ? "IS" : "IS NOT")} NULL";

			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(query))
			{
				var output = new List<Subject>();
				if (dataReader.HasRows)
				{
					while (dataReader.Read())
					{
						output.Add(new Subject(new DisciplineRepository().Get(dataReader.GetInt32(2)), dataReader.GetString(1))
						{
							IsRecordActive = dataReader.IsDBNull(3),
							RecordID = dataReader.GetInt32(0)
						});
					}
				}

				return output.AsQueryable();
			}
		}

		/// <inheritdoc />
		public void Update(Subject obj)
		{
			string query = $"UPDATE htc3 SET C3_NOME ='{obj.Name}', C3_DISCIPL = " +
						   $"{obj.Discipline?.RecordID}, D_E_L_E_T = " +
						   $"{(obj.IsRecordActive ? "NULL" : "'*'")} WHERE C3_COD = {obj.RecordID}";
			ConnectionManager.ExecuteQuery(query);
		}

		/// <inheritdoc />
		public void Update(IEnumerable<Subject> collection)
		{
			foreach (Subject obj in collection)
			{
				Update(obj);
			}
		}
		#endregion
	}
}

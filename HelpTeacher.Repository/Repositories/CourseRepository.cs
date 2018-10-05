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

namespace HelpTeacher.Repository.Repositories
{
	/// <inheritdoc />
	public class CourseRepository : ICourseRepository
	{
		#region Constructors
		/// <summary>Construtor padrão.</summary>
		public CourseRepository() { }
		#endregion


		#region Methods
		/// <inheritdoc />
		public void Add(Course obj)
		{
			string query = $"INSERT INTO htc1 (C1_COD, C1_NOME, D_E_L_E_T) VALUES (NULL, '{obj.Name}', NULL)";
			ConnectionManager.ExecuteQuery(query);
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
			string query = "SELECT C1_COD, C1_NOME, D_E_L_E_T FROM htc1 LIMIT 1";

			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(query))
			{
				var output = new Course("");
				if (dataReader.HasRows)
				{
					dataReader.Read();

					output.Name = dataReader.GetString(1);
					output.IsRecordActive = dataReader.IsDBNull(2);
					output.RecordID = dataReader.GetInt32(0);
				}

				return output;
			}
		}

		/// <inheritdoc />
		public IQueryable<Course> Get()
		{
			string query = "SELECT C1_COD, C1_NOME, D_E_L_E_T FROM htc1";

			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(query))
			{
				var output = new List<Course>();
				if (dataReader.HasRows)
				{
					while (dataReader.Read())
					{
						output.Add(new Course(dataReader.GetString(1))
						{
							IsRecordActive = dataReader.IsDBNull(2),
							RecordID = dataReader.GetInt32(0)
						});
					}
				}

				return output.AsQueryable();
			}
		}

		/// <inheritdoc />
		public IQueryable<Course> Get(bool isRecordActive)
		{
			string query = $"SELECT C1_COD, C1_NOME, D_E_L_E_T FROM htc1 " +
						   $"WHERE D_E_L_E_T {(isRecordActive ? "IS" : "IS NOT")} NULL";

			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(query))
			{
				var output = new List<Course>();
				if (dataReader.HasRows)
				{
					while (dataReader.Read())
					{
						output.Add(new Course(dataReader.GetString(1))
						{
							IsRecordActive = dataReader.IsDBNull(2),
							RecordID = dataReader.GetInt32(0)
						});
					}
				}

				return output.AsQueryable();
			}
		}

		/// <inheritdoc />
		public Course Get(int id)
		{
			string query = $"SELECT C1_COD, C1_NOME, D_E_L_E_T FROM htc1 WHERE C1_COD = {id}";

			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(query))
			{
				var output = new Course("");
				if (dataReader.HasRows)
				{
					dataReader.Read();

					output.Name = dataReader.GetString(1);
					output.IsRecordActive = dataReader.IsDBNull(2);
					output.RecordID = dataReader.GetInt32(0);
				}

				return output;
			}
		}

		/// <inheritdoc />
		public IQueryable<Course> GetWhereDifferentId(Course obj)
			=> GetWhereDifferentId(obj.RecordID);

		/// <inheritdoc />
		public IQueryable<Course> GetWhereDifferentId(int id)
		{
			string query = $"SELECT C1_COD, C1_NOME, D_E_L_E_T FROM htc1 " +
						   $"WHERE C1_COD <> {id} AND D_E_L_E_T IS NULL";

			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(query))
			{
				var output = new List<Course>();
				if (dataReader.HasRows)
				{
					while (dataReader.Read())
					{
						output.Add(new Course(dataReader.GetString(1))
						{
							IsRecordActive = dataReader.IsDBNull(2),
							RecordID = dataReader.GetInt32(0)
						});
					}
				}

				return output.AsQueryable();
			}
		}

		/// <inheritdoc />
		public void Update(Course obj)
		{
			string query = $"UPDATE htc1 SET C1_NOME ='{obj.Name}', D_E_L_E_T = " +
						   $"{(obj.IsRecordActive ? "NULL" : "'*'")} WHERE C1_COD = {obj.RecordID}";
			ConnectionManager.ExecuteQuery(query);
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

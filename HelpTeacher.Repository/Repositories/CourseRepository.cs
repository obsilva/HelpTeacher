/* Authors: Otávio Bueno Silva <obsilva94@gmail.com>
 * Since: 2018-09-19
 */

using System.Collections.Generic;
using System.Linq;

using HelpTeacher.Domain.Entities;
using HelpTeacher.Repository.IRepositories;

using MySql.Data.MySqlClient;

namespace HelpTeacher.Repository.Repositories
{
	/// <inheritdoc />
	public class CourseRepository : ICourseRepository
	{
		#region Fields
		private MySqlDataReader _dataReader;
		#endregion


		#region Properties
		protected ConnectionManager DatabaseConnection { get; } = new ConnectionManager();
		#endregion


		#region Constructors
		public CourseRepository() { }
		#endregion


		#region Methods
		/// <inheritdoc />
		public void Add(Course obj)
		{
			string query = $"INSERT INTO htc1 (C1_COD, C1_NOME, D_E_L_E_T) VALUES (NULL, '{obj.Name}', NULL)";
			DatabaseConnection.executeComando(query);
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
			DatabaseConnection.executeComando(query, ref _dataReader);

			var output = new Course("");
			if (_dataReader.HasRows)
			{
				_dataReader.Read();

				output.Name = _dataReader.GetString(1);
				output.IsRecordActive = _dataReader.IsDBNull(2);
				output.RecordID = _dataReader.GetInt32(0);
			}

			DatabaseConnection.fechaConexao();
			return output;
		}

		/// <inheritdoc />
		public IQueryable<Course> Get()
		{
			string query = "SELECT C1_COD, C1_NOME, D_E_L_E_T FROM htc1";

			DatabaseConnection.executeComando(query, ref _dataReader);

			var output = new List<Course>();
			if (_dataReader.HasRows)
			{
				while (_dataReader.Read())
				{
					output.Add(new Course(_dataReader.GetString(1))
					{
						IsRecordActive = _dataReader.IsDBNull(2),
						RecordID = _dataReader.GetInt32(0)
					});
				}
			}

			DatabaseConnection.fechaConexao();
			return output.AsQueryable();
		}

		/// <inheritdoc />
		public IQueryable<Course> Get(bool isRecordActive)
		{
			string query = $"SELECT C1_COD, C1_NOME, D_E_L_E_T FROM htc1 " +
						   $"WHERE D_E_L_E_T {(isRecordActive ? "IS" : "IS NOT")} NULL";

			DatabaseConnection.executeComando(query, ref _dataReader);

			var output = new List<Course>();
			if (_dataReader.HasRows)
			{
				while (_dataReader.Read())
				{
					output.Add(new Course(_dataReader.GetString(1))
					{
						IsRecordActive = _dataReader.IsDBNull(2),
						RecordID = _dataReader.GetInt32(0)
					});
				}
			}

			DatabaseConnection.fechaConexao();
			return output.AsQueryable();
		}

		/// <inheritdoc />
		public Course Get(int id)
		{
			string query = $"SELECT C1_COD, C1_NOME, D_E_L_E_T FROM htc1 WHERE C1_COD = {id}";
			DatabaseConnection.executeComando(query, ref _dataReader);

			var output = new Course("");
			if (_dataReader.HasRows)
			{
				_dataReader.Read();

				output.Name = _dataReader.GetString(1);
				output.IsRecordActive = _dataReader.IsDBNull(2);
				output.RecordID = _dataReader.GetInt32(0);
			}

			DatabaseConnection.fechaConexao();
			return output;
		}

		/// <inheritdoc />
		public IQueryable<Course> GetWhereDifferentId(Course obj) => GetWhereDifferentId(obj.RecordID);

		/// <inheritdoc />
		public IQueryable<Course> GetWhereDifferentId(int id)
		{
			string query = $"SELECT C1_COD, C1_NOME, D_E_L_E_T FROM htc1 " +
						   $"WHERE C1_COD <> {id} AND D_E_L_E_T IS NULL";

			DatabaseConnection.executeComando(query, ref _dataReader);

			var output = new List<Course>();
			if (_dataReader.HasRows)
			{
				while (_dataReader.Read())
				{
					output.Add(new Course(_dataReader.GetString(1))
					{
						IsRecordActive = _dataReader.IsDBNull(2),
						RecordID = _dataReader.GetInt32(0)
					});
				}
			}

			DatabaseConnection.fechaConexao();
			return output.AsQueryable();
		}

		/// <inheritdoc />
		public void Update(Course obj)
		{
			string query = $"UPDATE htc1 SET C1_NOME ='{obj.Name}', D_E_L_E_T = " +
						   $"{(obj.IsRecordActive ? "NULL" : "'*'")} WHERE C1_COD = {obj.RecordID}";
			DatabaseConnection.executeComando(query);
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

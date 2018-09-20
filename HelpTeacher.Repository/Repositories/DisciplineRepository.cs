using System.Collections.Generic;
using System.Linq;

using HelpTeacher.Domain.Entities;
using HelpTeacher.Repository.IRepositories;

using MySql.Data.MySqlClient;

namespace HelpTeacher.Repository.Repositories
{
	/// <inheritdoc />
	public class DisciplineRepository : IDisciplineRepository
	{
		#region Fields
		private MySqlDataReader _dataReader;
		#endregion


		#region Properties
		protected ConnectionManager DatabaseConnection { get; } = new ConnectionManager();
		#endregion


		#region Constructors
		public DisciplineRepository() { }
		#endregion


		#region Methods
		/// <inheritdoc />
		public void Add(Discipline obj)
		{
			string query = $"INSERT INTO htc2 (C2_COD, C2_NOME, C2_CURSO, D_E_L_E_T) VALUES (NULL, " +
						   $"'{obj.Name}', {obj.Courses.FirstOrDefault()?.RecordID}, NULL)";
			DatabaseConnection.executeComando(query);
		}

		/// <inheritdoc />
		public void Add(IEnumerable<Discipline> collection)
		{
			foreach (Discipline obj in collection)
			{
				Add(obj);
			}
		}

		/// <inheritdoc />
		public Discipline First()
		{
			string query = "SELECT C2_COD, C2_NOME, C2_CURSO, D_E_L_E_T FROM htc2 LIMIT 1";
			DatabaseConnection.executeComando(query, ref _dataReader);

			var output = new Discipline(new List<Course>(), "");
			if (_dataReader.HasRows)
			{
				_dataReader.Read();

				output.Courses.Add(new CourseRepository().Get(_dataReader.GetInt32(2)));
				output.Name = _dataReader.GetString(1);
				output.IsRecordActive = _dataReader.IsDBNull(3);
				output.RecordID = _dataReader.GetInt32(0);
			}

			DatabaseConnection.fechaConexao();
			return output;
		}

		/// <inheritdoc />
		public IQueryable<Discipline> Get()
		{
			string query = "SELECT C2_COD, C2_NOME, C2_CURSO, D_E_L_E_T FROM htc2";
			DatabaseConnection.executeComando(query, ref _dataReader);

			var output = new List<Discipline>();
			if (_dataReader.HasRows)
			{
				while (_dataReader.Read())
				{
					var courses = new List<Course>() { new CourseRepository().Get(_dataReader.GetInt32(2)) };
					output.Add(new Discipline(courses, _dataReader.GetString(1))
					{
						IsRecordActive = _dataReader.IsDBNull(3),
						RecordID = _dataReader.GetInt32(0)
					});
				}
			}

			DatabaseConnection.fechaConexao();
			return output.AsQueryable();
		}

		/// <inheritdoc />
		public IQueryable<Discipline> Get(bool isRecordActive)
		{
			string query = $"SELECT C2_COD, C2_NOME, C2_CURSO, D_E_L_E_T FROM htc2 " +
						   $"WHERE D_E_L_E_T {(isRecordActive ? "IS" : "IS NOT")} NULL";
			DatabaseConnection.executeComando(query, ref _dataReader);

			var output = new List<Discipline>();
			if (_dataReader.HasRows)
			{
				while (_dataReader.Read())
				{
					var courses = new List<Course>() { new CourseRepository().Get(_dataReader.GetInt32(2)) };
					output.Add(new Discipline(courses, _dataReader.GetString(1))
					{
						IsRecordActive = _dataReader.IsDBNull(3),
						RecordID = _dataReader.GetInt32(0)
					});
				}
			}

			DatabaseConnection.fechaConexao();
			return output.AsQueryable();
		}

		/// <inheritdoc />
		public Discipline Get(int id)
		{
			string query = $"SELECT C2_COD, C2_NOME, C2_CURSO, D_E_L_E_T FROM htc2 WHERE C2_COD = {id}";
			DatabaseConnection.executeComando(query, ref _dataReader);

			var output = new Discipline(new List<Course>(), "");
			if (_dataReader.HasRows)
			{
				_dataReader.Read();

				output.Courses.Add(new CourseRepository().Get(_dataReader.GetInt32(2)));
				output.Name = _dataReader.GetString(1);
				output.IsRecordActive = _dataReader.IsDBNull(3);
				output.RecordID = _dataReader.GetInt32(0);
			}

			DatabaseConnection.fechaConexao();
			return output;
		}

		/// <inheritdoc />
		public IQueryable<Discipline> GetWhereCourse(Course obj)
			=> GetWhereCourse(obj.RecordID);

		/// <inheritdoc />
		public IQueryable<Discipline> GetWhereCourse(int id)
		{
			string query = $"SELECT C2_COD, C2_NOME, C2_CURSO, D_E_L_E_T FROM htc2 WHERE C2_CURSO = {id}";
			DatabaseConnection.executeComando(query, ref _dataReader);

			var output = new List<Discipline>();
			if (_dataReader.HasRows)
			{
				while (_dataReader.Read())
				{
					var courses = new List<Course>() { new CourseRepository().Get(_dataReader.GetInt32(2)) };
					output.Add(new Discipline(courses, _dataReader.GetString(1))
					{
						IsRecordActive = _dataReader.IsDBNull(3),
						RecordID = _dataReader.GetInt32(0)
					});
				}
			}

			DatabaseConnection.fechaConexao();
			return output.AsQueryable();
		}

		/// <inheritdoc />
		public IQueryable<Discipline> GetWhereCourse(Course obj, bool isRecordActive)
			=> GetWhereCourse(obj.RecordID, isRecordActive);

		/// <inheritdoc />
		public IQueryable<Discipline> GetWhereCourse(int id, bool isRecordActive)
		{
			string query = $"SELECT C2_COD, C2_NOME, C2_CURSO, D_E_L_E_T FROM htc2 WHERE C2_CURSO = {id} " +
						   $"AND D_E_L_E_T {(isRecordActive ? "IS" : "IS NOT")} NULL";
			DatabaseConnection.executeComando(query, ref _dataReader);

			var output = new List<Discipline>();
			if (_dataReader.HasRows)
			{
				while (_dataReader.Read())
				{
					var courses = new List<Course>() { new CourseRepository().Get(_dataReader.GetInt32(2)) };
					output.Add(new Discipline(courses, _dataReader.GetString(1))
					{
						IsRecordActive = _dataReader.IsDBNull(3),
						RecordID = _dataReader.GetInt32(0)
					});
				}
			}

			DatabaseConnection.fechaConexao();
			return output.AsQueryable();
		}

		/// <inheritdoc />
		public IQueryable<Discipline> GetWhereDifferentId(Discipline obj)
			=> GetWhereDifferentId(obj.RecordID);

		/// <inheritdoc />
		public IQueryable<Discipline> GetWhereDifferentId(int id)
		{
			string query = $"SELECT C2_COD, C2_NOME, C2_CURSO, D_E_L_E_T FROM htc2 WHERE C2_COD <> {id} " +
						   $"AND D_E_L_E_T IS NULL";
			DatabaseConnection.executeComando(query, ref _dataReader);

			var output = new List<Discipline>();
			if (_dataReader.HasRows)
			{
				while (_dataReader.Read())
				{
					var courses = new List<Course>() { new CourseRepository().Get(_dataReader.GetInt32(2)) };
					output.Add(new Discipline(courses, _dataReader.GetString(1))
					{
						IsRecordActive = _dataReader.IsDBNull(3),
						RecordID = _dataReader.GetInt32(0)
					});
				}
			}

			DatabaseConnection.fechaConexao();
			return output.AsQueryable();
		}

		/// <inheritdoc />
		public void Update(Discipline obj)
		{
			string query = $"UPDATE htc2 SET C2_NOME ='{obj.Name}', C2_CURSO = " +
						   $"{obj.Courses.FirstOrDefault()?.RecordID}, D_E_L_E_T = " +
						   $"{(obj.IsRecordActive ? "NULL" : "'*'")} WHERE C2_COD = {obj.RecordID}";
			DatabaseConnection.executeComando(query);
		}

		/// <inheritdoc />
		public void Update(IEnumerable<Discipline> collection)
		{
			foreach (Discipline obj in collection)
			{
				Update(obj);
			}
		}
		#endregion
	}
}

using System.Collections.Generic;
using System.Linq;

using HelpTeacher.Domain.Entities;
using HelpTeacher.Repository.IRepositories;

using MySql.Data.MySqlClient;

namespace HelpTeacher.Repository.Repositories
{
	/// <inheritdoc />
	public class SubjectRepository : ISubjectRepository
	{
		#region Fields
		private MySqlDataReader _dataReader;
		#endregion


		#region Properties
		protected ConnectionManager DatabaseConnection { get; } = new ConnectionManager();
		#endregion


		#region Constructors
		public SubjectRepository() { }
		#endregion


		#region Methods

		/// <inheritdoc />
		public void Add(Subject obj)
		{
			string query = $"INSERT INTO htc3 (C3_COD, C3_NOME, C3_DISCIPL, D_E_L_E_T) VALUES (NULL, " +
						   $"'{obj.Name}', {obj.Disciplines.FirstOrDefault()?.RecordID}, NULL)";
			DatabaseConnection.executeComando(query);
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
			DatabaseConnection.executeComando(query, ref _dataReader);

			var output = new Subject(new List<Discipline>(), "");
			if (_dataReader.HasRows)
			{
				_dataReader.Read();

				output.Disciplines.Add(new DisciplineRepository().Get(_dataReader.GetInt32(2)));
				output.Name = _dataReader.GetString(1);
				output.IsRecordActive = _dataReader.IsDBNull(3);
				output.RecordID = _dataReader.GetInt32(0);
			}

			DatabaseConnection.fechaConexao();
			return output;
		}

		/// <inheritdoc />
		public IQueryable<Subject> Get()
		{
			string query = "SELECT C3_COD, C3_NOME, C3_DISCIPL, D_E_L_E_T FROM htc3";
			DatabaseConnection.executeComando(query, ref _dataReader);

			var output = new List<Subject>();
			if (_dataReader.HasRows)
			{
				while (_dataReader.Read())
				{
					var disciplines = new List<Discipline>() { new DisciplineRepository().Get(_dataReader.GetInt32(2)) };
					output.Add(new Subject(disciplines, _dataReader.GetString(1))
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
		public IQueryable<Subject> Get(bool isRecordActive)
		{
			string query = $"SELECT C3_COD, C3_NOME, C3_DISCIPL, D_E_L_E_T FROM htc3 " +
						   $"WHERE D_E_L_E_T { (isRecordActive ? "IS" : "IS NOT")} NULL";
			DatabaseConnection.executeComando(query, ref _dataReader);

			var output = new List<Subject>();
			if (_dataReader.HasRows)
			{
				while (_dataReader.Read())
				{
					var disciplines = new List<Discipline>() { new DisciplineRepository().Get(_dataReader.GetInt32(2)) };
					output.Add(new Subject(disciplines, _dataReader.GetString(1))
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
		public Subject Get(int id)
		{
			string query = $"SELECT C3_COD, C3_NOME, C3_DISCIPL, D_E_L_E_T FROM htc3 WHERE C3_COD = {id}";
			DatabaseConnection.executeComando(query, ref _dataReader);

			var output = new Subject(new List<Discipline>(), "");
			if (_dataReader.HasRows)
			{
				_dataReader.Read();

				output.Disciplines.Add(new DisciplineRepository().Get(_dataReader.GetInt32(2)));
				output.Name = _dataReader.GetString(1);
				output.IsRecordActive = _dataReader.IsDBNull(3);
				output.RecordID = _dataReader.GetInt32(0);
			}

			DatabaseConnection.fechaConexao();
			return output;
		}

		/// <inheritdoc />
		public IQueryable<Subject> GetWhereDiscipline(Discipline obj)
			=> GetWhereDiscipline(obj.RecordID);

		/// <inheritdoc />
		public IQueryable<Subject> GetWhereDiscipline(int id)
		{
			string query = $"SELECT C3_COD, C3_NOME, C3_DISCIPL, D_E_L_E_T FROM htc3 WHERE C3_DISCIPL = {id}";
			DatabaseConnection.executeComando(query, ref _dataReader);

			var output = new List<Subject>();
			if (_dataReader.HasRows)
			{
				while (_dataReader.Read())
				{
					var disciplines = new List<Discipline>() { new DisciplineRepository().Get(_dataReader.GetInt32(2)) };
					output.Add(new Subject(disciplines, _dataReader.GetString(1))
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
		public IQueryable<Subject> GetWhereDiscipline(Discipline obj, bool isRecordActive)
			=> GetWhereDiscipline(obj.RecordID, isRecordActive);

		/// <inheritdoc />
		public IQueryable<Subject> GetWhereDiscipline(int id, bool isRecordActive)
		{
			string query = $"SELECT C3_COD, C3_NOME, C3_DISCIPL, D_E_L_E_T FROM htc3 WHERE C3_DISCIPL = {id} " +
						   $"AND D_E_L_E_T { (isRecordActive ? "IS" : "IS NOT")} NULL";
			DatabaseConnection.executeComando(query, ref _dataReader);

			var output = new List<Subject>();
			if (_dataReader.HasRows)
			{
				while (_dataReader.Read())
				{
					var disciplines = new List<Discipline>() { new DisciplineRepository().Get(_dataReader.GetInt32(2)) };
					output.Add(new Subject(disciplines, _dataReader.GetString(1))
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
		public void Update(Subject obj)
		{
			string query = $"UPDATE htc3 SET C3_NOME ='{obj.Name}', C3_DISCIPL = " +
						   $"{obj.Disciplines.FirstOrDefault()?.RecordID}, D_E_L_E_T = " +
						   $"{(obj.IsRecordActive ? "NULL" : "'*'")} WHERE C3_COD = {obj.RecordID}";
			DatabaseConnection.executeComando(query);
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

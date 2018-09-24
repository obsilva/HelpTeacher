/* Authors: Otávio Bueno Silva <obsilva94@gmail.com>
 * Since: 2018-09-23
 */

using System.Collections.Generic;
using System.Linq;

using HelpTeacher.Domain.Entities;
using HelpTeacher.Repository.IRepositories;

using MySql.Data.MySqlClient;

namespace HelpTeacher.Repository.Repositories
{
	/// <inheritdoc />
	public class UserRepository : IUserRepository
	{
		#region Fields
		private MySqlDataReader _dataReader;
		#endregion


		#region Properties
		protected ConnectionManager DatabaseConnection { get; } = new ConnectionManager();
		#endregion


		#region Constructors
		public UserRepository() { }
		#endregion


		#region Methods

		/// <inheritdoc />
		public void Add(User obj)
		{
			string query = $"INSERT INTO hta1 (A1_COD, A1_LOGIN, A1_PWD, A1_ALTPWD, A1_STOPBD) VALUES " +
						   $"(NULL, '{obj.Username}', '{obj.Password}', {(obj.MustChangePassword ? "'*'" : "NULL")}, NULL)";
			DatabaseConnection.executeComando(query);
		}

		/// <inheritdoc />
		public void Add(IEnumerable<User> collection)
		{
			foreach (User obj in collection)
			{
				Add(obj);
			}
		}

		/// <inheritdoc />
		public User First()
		{
			string query = $"SELECT A1_COD, A1_LOGIN, A1_PWD, A1_ALTPWD FROM hta1 LIMIT 1";
			DatabaseConnection.executeComando(query, ref _dataReader);

			var output = new User();
			if (_dataReader.HasRows)
			{
				_dataReader.Read();

				output.IsRecordActive = true;
				output.MustChangePassword = !_dataReader.IsDBNull(3);
				output.Password = _dataReader.GetString(2);
				output.RecordID = _dataReader.GetInt32(0);
				output.Username = _dataReader.GetString(1);
			}

			DatabaseConnection.fechaConexao();
			return output;
		}

		/// <inheritdoc />
		public IQueryable<User> Get()
		{
			string query = $"SELECT A1_COD, A1_LOGIN, A1_PWD, A1_ALTPWD FROM hta1";
			DatabaseConnection.executeComando(query, ref _dataReader);

			var output = new List<User>();
			if (_dataReader.HasRows)
			{
				while (_dataReader.Read())
				{
					output.Add(new User()
					{
						IsRecordActive = true,
						MustChangePassword = !_dataReader.IsDBNull(3),
						Password = _dataReader.GetString(2),
						RecordID = _dataReader.GetInt32(0),
						Username = _dataReader.GetString(1)
					});
				}
			}

			DatabaseConnection.fechaConexao();
			return output.AsQueryable();
		}

		/// <inheritdoc />
		public IQueryable<User> Get(bool isRecordActive)
		{
			string query = $"SELECT A1_COD, A1_LOGIN, A1_PWD, A1_ALTPWD FROM hta1";
			DatabaseConnection.executeComando(query, ref _dataReader);

			var output = new List<User>();
			if (_dataReader.HasRows)
			{
				while (_dataReader.Read())
				{
					output.Add(new User()
					{
						IsRecordActive = true,
						MustChangePassword = !_dataReader.IsDBNull(3),
						Password = _dataReader.GetString(2),
						RecordID = _dataReader.GetInt32(0),
						Username = _dataReader.GetString(1)
					});
				}
			}

			DatabaseConnection.fechaConexao();
			return output.AsQueryable();
		}

		/// <inheritdoc />
		public User Get(int id)
		{
			string query = $"SELECT A1_COD, A1_LOGIN, A1_PWD, A1_ALTPWD FROM hta1 WHERE A1_COD = {id}";
			DatabaseConnection.executeComando(query, ref _dataReader);

			var output = new User();
			if (_dataReader.HasRows)
			{
				_dataReader.Read();

				output.IsRecordActive = true;
				output.MustChangePassword = !_dataReader.IsDBNull(3);
				output.Password = _dataReader.GetString(2);
				output.RecordID = _dataReader.GetInt32(0);
				output.Username = _dataReader.GetString(1);
			}

			DatabaseConnection.fechaConexao();
			return output;
		}

		/// <inheritdoc />
		public void Update(User obj)
		{
			string query = $"UPDATE hta1 SET A1_LOGIN = '{obj.Username}', A1_PWD = '{obj.Password}', " +
						   $"A1_ALTPWD = {(obj.MustChangePassword ? "'*'" : "NULL")} WHERE A1_COD = {obj.RecordID}";
			DatabaseConnection.executeComando(query);
		}

		/// <inheritdoc />
		public void Update(IEnumerable<User> collection)
		{
			foreach (User obj in collection)
			{
				Update(obj);
			}
		}
		#endregion
	}
}

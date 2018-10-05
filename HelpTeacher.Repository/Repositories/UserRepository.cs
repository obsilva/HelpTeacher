// This Source Code Form is subject to the terms of the Mozilla 
// Public License, v. 2.0. If a copy of the MPL was not distributed 
// with this file, You can obtain one at https://mozilla.org/MPL/2.0/

// Since: 2018/09/23
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
	public class UserRepository : IUserRepository
	{
		#region Constructors
		public UserRepository() { }
		#endregion


		#region Methods

		/// <inheritdoc />
		public void Add(User obj)
		{
			string query = $"INSERT INTO hta1 (A1_COD, A1_LOGIN, A1_PWD, A1_ALTPWD, A1_STOPBD) VALUES " +
						   $"(NULL, '{obj.Username}', '{obj.Password}', {(obj.MustChangePassword ? "'*'" : "NULL")}, NULL)";
			ConnectionManager.ExecuteQuery(query);
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

			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(query))
			{
				var output = new User();
				if (dataReader.HasRows)
				{
					dataReader.Read();

					output.IsRecordActive = true;
					output.MustChangePassword = !dataReader.IsDBNull(3);
					output.Password = dataReader.GetString(2);
					output.RecordID = dataReader.GetInt32(0);
					output.Username = dataReader.GetString(1);
				}

				return output;
			}
		}

		/// <inheritdoc />
		public IQueryable<User> Get()
		{
			string query = $"SELECT A1_COD, A1_LOGIN, A1_PWD, A1_ALTPWD FROM hta1";

			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(query))
			{
				var output = new List<User>();
				if (dataReader.HasRows)
				{
					while (dataReader.Read())
					{
						output.Add(new User()
						{
							IsRecordActive = true,
							MustChangePassword = !dataReader.IsDBNull(3),
							Password = dataReader.GetString(2),
							RecordID = dataReader.GetInt32(0),
							Username = dataReader.GetString(1)
						});
					}
				}

				return output.AsQueryable();
			}
		}

		/// <inheritdoc />
		public IQueryable<User> Get(bool isRecordActive)
		{
			string query = $"SELECT A1_COD, A1_LOGIN, A1_PWD, A1_ALTPWD FROM hta1";

			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(query))
			{
				var output = new List<User>();
				if (dataReader.HasRows)
				{
					while (dataReader.Read())
					{
						output.Add(new User()
						{
							IsRecordActive = true,
							MustChangePassword = !dataReader.IsDBNull(3),
							Password = dataReader.GetString(2),
							RecordID = dataReader.GetInt32(0),
							Username = dataReader.GetString(1)
						});
					}
				}

				return output.AsQueryable();
			}
		}

		/// <inheritdoc />
		public User Get(int id)
		{
			string query = $"SELECT A1_COD, A1_LOGIN, A1_PWD, A1_ALTPWD FROM hta1 WHERE A1_COD = {id}";

			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(query))
			{
				var output = new User();
				if (dataReader.HasRows)
				{
					dataReader.Read();

					output.IsRecordActive = true;
					output.MustChangePassword = !dataReader.IsDBNull(3);
					output.Password = dataReader.GetString(2);
					output.RecordID = dataReader.GetInt32(0);
					output.Username = dataReader.GetString(1);
				}

				return output;
			}
		}

		/// <inheritdoc />
		public void Update(User obj)
		{
			string query = $"UPDATE hta1 SET A1_LOGIN = '{obj.Username}', A1_PWD = '{obj.Password}', " +
						   $"A1_ALTPWD = {(obj.MustChangePassword ? "'*'" : "NULL")} WHERE A1_COD = {obj.RecordID}";
			ConnectionManager.ExecuteQuery(query);
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

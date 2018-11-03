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
using HelpTeacher.Util;

namespace HelpTeacher.Repository.Repositories
{
	/// <inheritdoc />
	public class UserRepository : IUserRepository
	{
		#region Constants
		private const string QueryInsert = "INSERT INTO hta1 (A1_COD, A1_LOGIN, A1_PWD, A1_ALTPWD, A1_STOPBD) VALUES (NULL, @A1_LOGIN, @A1_PWD, @A1_ALTPWD, 0);";

		private const string QuerySelect = "SELECT A1_COD, A1_LOGIN, A1_PWD, A1_ALTPWD FROM hta1 LIMIT @LIMIT OFFSET @OFFSET;";

		private const string QuerySelectID = "SELECT A1_COD, A1_LOGIN, A1_PWD, A1_ALTPWD FROM hta1 WHERE (A1_COD = @A1_COD);";

		private const string QueryUpdate = "UPDATE hta1 SET A1_LOGIN = @A1_LOGIN, A1_PWD = @A1_PWD, A1_ALTPWD = @A1_ALTPWD WHERE A1_COD = @A1_COD;";
		#endregion


		#region Properties
		/// <summary>Gerenciador de conexão.</summary>
		public ConnectionManager Connection { get; set; }
		#endregion


		#region Constructors
		/// <summary>
		/// Inicializa uma nova instância de <see cref="UserRepository"/>. É possível definir o
		/// gerenciador conexão a ser usado e/ou o tamanho da página de registros.
		/// </summary>
		/// <param name="connection">Gerenciador de conexão a ser usado.</param>
		/// <param name="pageSize">Número máximo de registros para retornar por vez.</param>
		public UserRepository(ConnectionManager connection = null, int pageSize = 50)
		{
			if (connection == null)
			{
				connection = new ConnectionManager();
			}

			Connection = connection;
			Offset = 0;
			PageSize = pageSize;
		}

		/// <summary>Valor de offset na recuperação de registros.</summary>
		public int Offset { get; set; }

		/// <summary>Tamanho da página de registros.</summary>
		public int PageSize { get; set; }
		#endregion


		#region Methods
		/// <inheritdoc />
		public void Add(User obj)
		{
			Checker.NullObject(obj, nameof(obj));

			Connection.ExecuteQuery(QueryInsert, obj.Username, obj.Password, obj.MustChangePassword);
		}

		/// <inheritdoc />
		public void Add(IEnumerable<User> collection)
		{
			Checker.NullObject(collection, nameof(collection));

			foreach (User obj in collection)
			{
				Add(obj);
			}
		}

		/// <inheritdoc />
		public User First()
		{
			using (DbDataReader dataReader = Connection.ExecuteReader(QuerySelect, 1, 0))
			{
				IQueryable<User> records = ReadDataReader(dataReader);

				return records.FirstOrDefault() ?? User.Null;
			}
		}

		/// <inheritdoc />
		public IQueryable<User> Get()
		{
			using (DbDataReader dataReader = Connection.ExecuteReader(QuerySelect, PageSize, Offset))
			{
				return ReadDataReader(dataReader);
			}
		}

		/// <inheritdoc />
		public IQueryable<User> Get(bool isRecordActive)
			=> Get();

		/// <inheritdoc />
		public User Get(int id)
		{
			using (DbDataReader dataReader = Connection.ExecuteReader(QuerySelectID, id))
			{
				IQueryable<User> records = ReadDataReader(dataReader);

				return records.FirstOrDefault() ?? User.Null;
			}
		}

		/// <summary>Faz a leitura do <see cref="DbDataReader"/>.</summary>
		/// <param name="dataReader">Objeto para ler.</param>
		/// <returns>Todos os objetos no <see cref="DbDataReader"/>.</returns>
		private IQueryable<User> ReadDataReader(DbDataReader dataReader)
		{
			var output = new List<User>();

			if (dataReader.HasRows)
			{
				while (dataReader.Read())
				{
					output.Add(new User()
					{
						IsRecordActive = true,
						RecordID = dataReader.GetInt32(0),
						Username = dataReader.GetString(1),
						Password = dataReader.GetString(2),
						MustChangePassword = (dataReader.GetInt32(3) == 1)
					});
				}
			}

			dataReader.Close();
			return output.AsQueryable();
		}

		/// <inheritdoc />
		public void Update(User obj)
		{
			Checker.NullObject(obj, nameof(obj));

			Connection.ExecuteQuery(QueryUpdate, obj.Username, obj.Password, obj.MustChangePassword, obj.RecordID);
		}

		/// <inheritdoc />
		public void Update(IEnumerable<User> collection)
		{
			Checker.NullObject(collection, nameof(collection));

			foreach (User obj in collection)
			{
				Update(obj);
			}
		}
		#endregion
	}
}

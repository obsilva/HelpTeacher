// This Source Code Form is subject to the terms of the Mozilla 
// Public License, v. 2.0. If a copy of the MPL was not distributed 
// with this file, You can obtain one at https://mozilla.org/MPL/2.0/

// Since: 2018/09/21
// Authors: 
//		Otávio Bueno Silva <obsilva94@gmail.com>

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

using HelpTeacher.Domain.Entities;
using HelpTeacher.Repository.IRepositories;
using HelpTeacher.Util;

namespace HelpTeacher.Repository.Repositories
{
	/// <inheritdoc />
	public class QuestionRepository : IQuestionRepository
	{
		#region Constants
		private const string QueryInsert = "INSERT INTO htb1 (B1_COD, B1_QUEST, B1_OBJETIV, B1_ARQUIVO, B1_USADA, B1_MATERIA, B1_PADRAO, D_E_L_E_T) VALUES (NULL, @B1_QUEST, @B1_OBJETIV, @B1_ARQUIVO, 0, @B1_MATERIA, @B1_PADRAO, 0);";

		private const string QuerySelect = "SELECT B1_COD, B1_QUEST, B1_OBJETIV, B1_ARQUIVO, B1_USADA, B1_MATERIA, B1_PADRAO, D_E_L_E_T FROM htb1 LIMIT @LIMIT OFFSET @OFFSET;";

		private const string QuerySelectActive = "SELECT B1_COD, B1_QUEST, B1_OBJETIV, B1_ARQUIVO, B1_USADA, B1_MATERIA, B1_PADRAO, D_E_L_E_T FROM htb1 WHERE (D_E_L_E_T = @IS_DELETED) LIMIT @LIMIT OFFSET @OFFSET;";

		private const string QuerySelectSubject = "SELECT B1_COD, B1_QUEST, B1_OBJETIV, B1_ARQUIVO, B1_USADA, B1_MATERIA, B1_PADRAO, D_E_L_E_T FROM htb1 WHERE (B1_MATERIA = @B1_MATERIA) LIMIT @LIMIT OFFSET @OFFSET;";

		private const string QuerySelectSubjectAndActive = "SELECT B1_COD, B1_QUEST, B1_OBJETIV, B1_ARQUIVO, B1_USADA, B1_MATERIA, B1_PADRAO, D_E_L_E_T FROM htb1 WHERE (B1_MATERIA = @B1_MATERIA) AND (D_E_L_E_T = @IS_DELETED) LIMIT @LIMIT OFFSET @OFFSET;";

		private const string QuerySelectSubjectAndObjective = "SELECT B1_COD, B1_QUEST, B1_OBJETIV, B1_ARQUIVO, B1_USADA, B1_MATERIA, B1_PADRAO, D_E_L_E_T FROM htb1 WHERE (B1_MATERIA = @B1_MATERIA) AND (D_E_L_E_T = @IS_DELETED) AND (B1_OBJETIV = @B1_OBJETIV) LIMIT @LIMIT OFFSET @OFFSET;";

		private const string QuerySelectSubjectAndUsed = "SELECT B1_COD, B1_QUEST, B1_OBJETIV, B1_ARQUIVO, B1_USADA, B1_MATERIA, B1_PADRAO, D_E_L_E_T FROM htb1 WHERE (B1_MATERIA = @B1_MATERIA) AND (D_E_L_E_T = @IS_DELETED) AND (B1_OBJETIV = @B1_OBJETIV) AND (B1_USADA = @B1_USADA) LIMIT @LIMIT OFFSET @OFFSET;";

		private const string QuerySelectID = "SELECT B1_COD, B1_QUEST, B1_OBJETIV, B1_ARQUIVO, B1_USADA, B1_MATERIA, B1_PADRAO, D_E_L_E_T FROM htb1 WHERE (B1_COD = @B1_COD);";

		private const string QueryUpdate = "UPDATE htb1 SET B1_QUEST = @B1_QUEST, B1_OBJETIV = @B1_OBJETIV, B1_ARQUIVO = @B1_ARQUIVO, B1_USADA = @B1_USADA, B1_MATERIA = @B1_MATERIA, B1_PADRAO = @B1_PADRAO, D_E_L_E_T = @IS_DELETED WHERE (B1_COD = @B1_COD);";
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
		/// Inicializa uma nova instância de <see cref="QuestionRepository"/>. É possível definir o
		/// gerenciador conexão a ser usado e/ou o tamanho da página de registros.
		/// </summary>
		/// <param name="connectionManager">Gerenciador de conexão a ser usado.</param>
		/// <param name="pageSize">Número máximo de registros para retornar por vez.</param>
		public QuestionRepository(ConnectionManager connection = null, int pageSize = 50)
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
		public void Add(Question obj)
		{
			Checker.NullObject(obj, nameof(obj));

			string attachments = $"{obj.FirstAttachment},{obj.SecondAttachment}";

			ConnectionManager.ExecuteQuery(QueryInsert, obj.Statement, obj.IsObjective,
				attachments, obj.Subject.RecordID, obj.IsDefault);
		}

		/// <inheritdoc />
		public void Add(IEnumerable<Question> collection)
		{
			Checker.NullObject(collection, nameof(collection));

			foreach (Question obj in collection)
			{
				Add(obj);
			}
		}

		/// <inheritdoc />
		public Question First()
		{
			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(QuerySelect, 1, 0))
			{
				IQueryable<Question> records = ReadDataReader(dataReader);

				return records.FirstOrDefault() ?? Question.Null;
			}
		}

		/// <inheritdoc />
		public IQueryable<Question> Get()
		{
			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(QuerySelect, PageSize, Offset))
			{
				return ReadDataReader(dataReader);
			}
		}

		/// <inheritdoc />
		public IQueryable<Question> Get(bool isRecordActive)
		{
			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(QuerySelectActive,
				!isRecordActive, PageSize, Offset))
			{
				return ReadDataReader(dataReader);
			}
		}

		/// <inheritdoc />
		public Question Get(int id)
		{
			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(QuerySelectID, id))
			{
				IQueryable<Question> records = ReadDataReader(dataReader);

				return records.FirstOrDefault() ?? Question.Null;
			}
		}

		/// <inheritdoc />
		public IQueryable<Question> GetWhereSubject(Subject obj)
			=> (obj == null) ? new List<Question>().AsQueryable() : GetWhereSubject(obj.RecordID);

		/// <inheritdoc />
		public IQueryable<Question> GetWhereSubject(int id)
		{
			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(QuerySelectSubject, id, PageSize, Offset))
			{
				return ReadDataReader(dataReader);
			}
		}

		/// <inheritdoc />
		public IQueryable<Question> GetWhereSubject(Subject obj, bool isRecordActive)
			=> (obj == null) ? new List<Question>().AsQueryable() : GetWhereSubject(obj.RecordID, isRecordActive);

		/// <inheritdoc />
		public IQueryable<Question> GetWhereSubject(int id, bool isRecordActive)
		{
			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(QuerySelectSubjectAndActive,
				id, !isRecordActive, PageSize, Offset))
			{
				return ReadDataReader(dataReader);
			}
		}

		/// <inheritdoc />
		public IQueryable<Question> GetWhereSubject(Subject obj, bool isRecordActive, bool isObjective)
			=> (obj == null) ? new List<Question>().AsQueryable() : GetWhereSubject(obj.RecordID, isRecordActive, isObjective);

		/// <inheritdoc />
		public IQueryable<Question> GetWhereSubject(int id, bool isRecordActive, bool isObjective)
		{
			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(QuerySelectSubjectAndObjective,
				id, !isRecordActive, isObjective, PageSize, Offset))
			{
				return ReadDataReader(dataReader);
			}
		}

		/// <inheritdoc />
		public IQueryable<Question> GetWhereSubject(Subject obj, bool isRecordActive, bool isObjective, bool wasUsed)
			=> (obj == null) ? new List<Question>().AsQueryable() : GetWhereSubject(obj.RecordID, isRecordActive, isObjective, wasUsed);

		/// <inheritdoc />
		public IQueryable<Question> GetWhereSubject(int id, bool isRecordActive, bool isObjective, bool wasUsed)
		{
			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(QuerySelectSubjectAndUsed,
				id, !isRecordActive, isObjective, wasUsed, PageSize, Offset))
			{
				return ReadDataReader(dataReader);
			}
		}

		/// <summary>Faz a leitura do <see cref="DbDataReader"/>.</summary>
		/// <param name="dataReader">Objeto para ler.</param>
		/// <returns>Todos os objetos no <see cref="DbDataReader"/>.</returns>
		private IQueryable<Question> ReadDataReader(DbDataReader dataReader)
		{
			var output = new List<Question>();

			if (dataReader.HasRows)
			{
				while (dataReader.Read())
				{
					Subject subject = new SubjectRepository(ConnectionManager).Get(dataReader.GetInt32(5));
					string[] attachments = dataReader.GetString(3).Split(new[] { ',' },
						StringSplitOptions.RemoveEmptyEntries);
					output.Add(new Question(subject, dataReader.GetString(1))
					{
						FirstAttachment = (attachments.Length > 0) ? attachments[0] : String.Empty,
						IsDefault = (dataReader.GetInt32(6) == 1),
						IsObjective = (dataReader.GetInt32(2) == 1),
						IsRecordActive = (dataReader.GetInt32(7) == 0),
						RecordID = dataReader.GetInt32(0),
						SecondAttachment = (attachments.Length > 1) ? attachments[1] : String.Empty,
						WasUsed = (dataReader.GetInt32(4) == 1)
					});
				}
			}

			dataReader.Close();
			return output.AsQueryable();
		}

		/// <inheritdoc />
		public void Update(Question obj)
		{
			Checker.NullObject(obj, nameof(obj));

			string attachments = $"{obj.FirstAttachment},{obj.SecondAttachment}";

			ConnectionManager.ExecuteQuery(QueryUpdate, obj.Statement, obj.IsObjective, attachments,
				obj.WasUsed, obj.Subject.RecordID, obj.IsDefault, !obj.IsRecordActive, obj.RecordID);
		}

		/// <inheritdoc />
		public void Update(IEnumerable<Question> collection)
		{
			Checker.NullObject(collection, nameof(collection));

			foreach (Question obj in collection)
			{
				Update(obj);
			}
		}
		#endregion
	}
}

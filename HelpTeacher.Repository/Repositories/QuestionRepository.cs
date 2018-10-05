// Since: 2018-09-21
// Authors: 
//		Otávio Bueno Silva <obsilva94@gmail.com>

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

using HelpTeacher.Domain.Entities;
using HelpTeacher.Repository.IRepositories;

namespace HelpTeacher.Repository.Repositories
{
	/// <inheritdoc />
	public class QuestionRepository : IQuestionRepository
	{
		#region Constructors
		public QuestionRepository() { }
		#endregion



		#region Methods
		/// <inheritdoc />
		public void Add(Question obj)
		{
			string query = $"INSERT INTO htb1 (B1_COD, B1_QUEST, B1_OBJETIV, B1_ARQUIVO, B1_USADA, " +
						   $"B1_MATERIA, B1_PADRAO, D_E_L_E_T) VALUES (NULL, '{obj.Statement}', " +
						   $"{(obj.IsObjective ? "*" : "NULL")}, '{obj.FirstAttachment}, {obj.SecondAttachment}', " +
						   $"NULL, {obj.Subjects.FirstOrDefault()?.RecordID}, {(obj.IsDefault ? "*" : "NULL")}, NULL)";
			ConnectionManager.ExecuteQuery(query);
		}

		/// <inheritdoc />
		public void Add(IEnumerable<Question> collection)
		{
			foreach (Question obj in collection)
			{
				Add(obj);
			}
		}

		/// <inheritdoc />
		public Question First()
		{
			string query = $"SELECT B1_COD, B1_QUEST, B1_OBJETIV, B1_ARQUIVO, B1_USADA, B1_MATERIA, " +
						   $"B1_PADRAO, D_E_L_E_T FROM htb1 LIMIT 1";

			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(query))
			{
				var output = new Question(new List<Subject>(), "");
				if (dataReader.HasRows)
				{
					dataReader.Read();

					string[] attachments = dataReader.GetString(3).Split(new char[] { ',' },
						StringSplitOptions.RemoveEmptyEntries);

					output.FirstAttachment = attachments[0];
					output.IsDefault = dataReader.IsDBNull(6);
					output.IsObjective = dataReader.IsDBNull(2);
					output.IsRecordActive = dataReader.IsDBNull(7);
					output.RecordID = dataReader.GetInt32(0);
					output.SecondAttachment =
						(attachments.Length > 1) ? attachments[1] : attachments[0];
					output.Statement = dataReader.GetString(1);
					output.Subjects.Add(new SubjectRepository().Get(dataReader.GetInt32(5)));
					output.WasUsed = dataReader.IsDBNull(4);
				}

				return output;
			}
		}

		/// <inheritdoc />
		public IQueryable<Question> Get()
		{
			string query = $"SELECT B1_COD, B1_QUEST, B1_OBJETIV, B1_ARQUIVO, B1_USADA, B1_MATERIA, " +
						   $"B1_PADRAO, D_E_L_E_T FROM htb1";

			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(query))
			{
				var output = new List<Question>();
				if (dataReader.HasRows)
				{
					while (dataReader.Read())
					{
						var subjects = new List<Subject>()
							{new SubjectRepository().Get(dataReader.GetInt32(5))};
						string[] attachments = dataReader.GetString(3).Split(new char[] { ',' },
							StringSplitOptions.RemoveEmptyEntries);

						output.Add(new Question(subjects, dataReader.GetString(1))
						{
							FirstAttachment = attachments[0],
							IsDefault = dataReader.IsDBNull(6),
							IsObjective = dataReader.IsDBNull(2),
							IsRecordActive = dataReader.IsDBNull(7),
							RecordID = dataReader.GetInt32(0),
							SecondAttachment = (attachments.Length > 1)
								? attachments[1]
								: attachments[0],
							WasUsed = dataReader.IsDBNull(4)
						});
					}
				}

				return output.AsQueryable();
			}
		}

		/// <inheritdoc />
		public IQueryable<Question> Get(bool isRecordActive)
		{
			string query = $"SELECT B1_COD, B1_QUEST, B1_OBJETIV, B1_ARQUIVO, B1_USADA, B1_MATERIA, B1_PADRAO, " +
						   $"D_E_L_E_T FROM htb1 WHERE D_E_L_E_T {(isRecordActive ? "IS" : "IS NOT")} NULL";

			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(query))
			{
				var output = new List<Question>();
				if (dataReader.HasRows)
				{
					while (dataReader.Read())
					{
						var subjects = new List<Subject>()
							{new SubjectRepository().Get(dataReader.GetInt32(5))};
						string[] attachments = dataReader.GetString(3).Split(new char[] { ',' },
							StringSplitOptions.RemoveEmptyEntries);

						output.Add(new Question(subjects, dataReader.GetString(1))
						{
							FirstAttachment = attachments[0],
							IsDefault = dataReader.IsDBNull(6),
							IsObjective = dataReader.IsDBNull(2),
							IsRecordActive = dataReader.IsDBNull(7),
							RecordID = dataReader.GetInt32(0),
							SecondAttachment = (attachments.Length > 1)
								? attachments[1]
								: attachments[0],
							WasUsed = dataReader.IsDBNull(4)
						});
					}
				}

				return output.AsQueryable();
			}
		}

		/// <inheritdoc />
		public Question Get(int id)
		{
			string query = $"SELECT B1_COD, B1_QUEST, B1_OBJETIV, B1_ARQUIVO, B1_USADA, B1_MATERIA, " +
						   $"B1_PADRAO, D_E_L_E_T FROM htb1 WHERE B1_COD = {id}";

			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(query))
			{
				var output = new Question(new List<Subject>(), "");
				if (dataReader.HasRows)
				{
					dataReader.Read();

					string[] attachments = dataReader.GetString(3).Split(new char[] { ',' },
						StringSplitOptions.RemoveEmptyEntries);

					output.FirstAttachment = attachments[0];
					output.IsDefault = dataReader.IsDBNull(6);
					output.IsObjective = dataReader.IsDBNull(2);
					output.IsRecordActive = dataReader.IsDBNull(7);
					output.RecordID = dataReader.GetInt32(0);
					output.SecondAttachment =
						(attachments.Length > 1) ? attachments[1] : attachments[0];
					output.Statement = dataReader.GetString(1);
					output.Subjects.Add(new SubjectRepository().Get(dataReader.GetInt32(5)));
					output.WasUsed = dataReader.IsDBNull(4);
				}

				return output;
			}
		}

		/// <inheritdoc />
		public IQueryable<Question> GetWhereSubject(Subject obj)
			=> GetWhereSubject(obj.RecordID);

		/// <inheritdoc />
		public IQueryable<Question> GetWhereSubject(int id)
		{
			string query = $"SELECT B1_COD, B1_QUEST, B1_OBJETIV, B1_ARQUIVO, B1_USADA, B1_MATERIA, B1_PADRAO, " +
						   $"D_E_L_E_T FROM htb1 WHERE B1_MATERIA = {id}";

			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(query))
			{
				var output = new List<Question>();
				if (dataReader.HasRows)
				{
					while (dataReader.Read())
					{
						var subjects = new List<Subject>()
							{new SubjectRepository().Get(dataReader.GetInt32(5))};
						string[] attachments = dataReader.GetString(3).Split(new char[] { ',' },
							StringSplitOptions.RemoveEmptyEntries);

						output.Add(new Question(subjects, dataReader.GetString(1))
						{
							FirstAttachment = attachments[0],
							IsDefault = dataReader.IsDBNull(6),
							IsObjective = dataReader.IsDBNull(2),
							IsRecordActive = dataReader.IsDBNull(7),
							RecordID = dataReader.GetInt32(0),
							SecondAttachment = (attachments.Length > 1)
								? attachments[1]
								: attachments[0],
							WasUsed = dataReader.IsDBNull(4)
						});
					}
				}

				return output.AsQueryable();
			}
		}

		/// <inheritdoc />
		public IQueryable<Question> GetWhereSubject(Subject obj, bool isRecordActive)
			=> GetWhereSubject(obj.RecordID, isRecordActive);

		/// <inheritdoc />
		public IQueryable<Question> GetWhereSubject(int id, bool isRecordActive)
		{
			string query = $"SELECT B1_COD, B1_QUEST, B1_OBJETIV, B1_ARQUIVO, B1_USADA, B1_MATERIA, " +
						   $"B1_PADRAO, D_E_L_E_T FROM htb1 WHERE B1_MATERIA = {id} AND D_E_L_E_T " +
						   $"{(isRecordActive ? "IS" : "IS NOT")} NULL";

			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(query))
			{
				var output = new List<Question>();
				if (dataReader.HasRows)
				{
					while (dataReader.Read())
					{
						var subjects = new List<Subject>()
							{new SubjectRepository().Get(dataReader.GetInt32(5))};
						string[] attachments = dataReader.GetString(3).Split(new char[] { ',' },
							StringSplitOptions.RemoveEmptyEntries);

						output.Add(new Question(subjects, dataReader.GetString(1))
						{
							FirstAttachment = attachments[0],
							IsDefault = dataReader.IsDBNull(6),
							IsObjective = dataReader.IsDBNull(2),
							IsRecordActive = dataReader.IsDBNull(7),
							RecordID = dataReader.GetInt32(0),
							SecondAttachment = (attachments.Length > 1)
								? attachments[1]
								: attachments[0],
							WasUsed = dataReader.IsDBNull(4)
						});
					}
				}

				return output.AsQueryable();
			}
		}

		/// <inheritdoc />
		public IQueryable<Question> GetWhereSubject(Subject obj, bool isRecordActive, bool isObjective)
			=> GetWhereSubject(obj.RecordID, isRecordActive, isObjective);

		/// <inheritdoc />
		public IQueryable<Question> GetWhereSubject(int id, bool isRecordActive, bool isObjective)
		{
			string query = $"SELECT B1_COD, B1_QUEST, B1_OBJETIV, B1_ARQUIVO, B1_USADA, B1_MATERIA, " +
						   $"B1_PADRAO, D_E_L_E_T FROM htb1 WHERE B1_MATERIA = {id} AND B1_OBJETIV " +
						   $"{(isObjective ? "IS" : "IS NOT")} NULL AND D_E_L_E_T " +
						   $"{(isRecordActive ? "IS" : "IS NOT")} NULL";

			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(query))
			{
				var output = new List<Question>();
				if (dataReader.HasRows)
				{
					while (dataReader.Read())
					{
						var subjects = new List<Subject>()
							{new SubjectRepository().Get(dataReader.GetInt32(5))};
						string[] attachments = dataReader.GetString(3).Split(new char[] { ',' },
							StringSplitOptions.RemoveEmptyEntries);

						output.Add(new Question(subjects, dataReader.GetString(1))
						{
							FirstAttachment = attachments[0],
							IsDefault = dataReader.IsDBNull(6),
							IsObjective = dataReader.IsDBNull(2),
							IsRecordActive = dataReader.IsDBNull(7),
							RecordID = dataReader.GetInt32(0),
							SecondAttachment = (attachments.Length > 1)
								? attachments[1]
								: attachments[0],
							WasUsed = dataReader.IsDBNull(4)
						});
					}
				}

				return output.AsQueryable();
			}
		}

		/// <inheritdoc />
		public IQueryable<Question> GetWhereSubject(Subject obj, bool isRecordActive, bool isObjective, bool wasUsed)
			=> GetWhereSubject(obj.RecordID, isRecordActive, isObjective, wasUsed);

		/// <inheritdoc />
		public IQueryable<Question> GetWhereSubject(int id, bool isRecordActive, bool isObjective, bool wasUsed)
		{
			string query = $"SELECT B1_COD, B1_QUEST, B1_OBJETIV, B1_ARQUIVO, B1_USADA, B1_MATERIA, " +
						   $"B1_PADRAO, D_E_L_E_T FROM htb1 WHERE B1_MATERIA = {id} AND B1_OBJETIV " +
						   $"{(isObjective ? "IS" : "IS NOT")} NULL AND B1_USADA {(wasUsed ? "IS" : "IS NOT")} " +
						   $"NULL AND D_E_L_E_T {(isRecordActive ? "IS" : "IS NOT")} NULL";

			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(query))
			{
				var output = new List<Question>();
				if (dataReader.HasRows)
				{
					while (dataReader.Read())
					{
						var subjects = new List<Subject>()
							{new SubjectRepository().Get(dataReader.GetInt32(5))};
						string[] attachments = dataReader.GetString(3).Split(new char[] { ',' },
							StringSplitOptions.RemoveEmptyEntries);

						output.Add(new Question(subjects, dataReader.GetString(1))
						{
							FirstAttachment = attachments[0],
							IsDefault = dataReader.IsDBNull(6),
							IsObjective = dataReader.IsDBNull(2),
							IsRecordActive = dataReader.IsDBNull(7),
							RecordID = dataReader.GetInt32(0),
							SecondAttachment = (attachments.Length > 1)
								? attachments[1]
								: attachments[0],
							WasUsed = dataReader.IsDBNull(4)
						});
					}
				}

				return output.AsQueryable();
			}
		}

		/// <inheritdoc />
		public void Update(Question obj)
		{
			string query = $"UPDATE htb1 SET B1_QUEST = '{obj.Statement}', B1_OBJETIV = {(obj.IsObjective ? "*" : "NULL")}, " +
						   $"B1_ARQUIVO = '{obj.FirstAttachment}, {obj.SecondAttachment}', B1_USADA = " +
						   $"{(obj.WasUsed ? "*" : "NULL")}, B1_MATERIA = {obj.Subjects.FirstOrDefault()?.RecordID}," +
						   $"B1_PADRAO = {(obj.IsDefault ? "*" : "NULL")}, D_E_L_E_T = " +
						   $"{(obj.IsRecordActive ? "*" : "NULL")} WHERE B1_COD = {obj.RecordID}";
			ConnectionManager.ExecuteQuery(query);
		}

		/// <inheritdoc />
		public void Update(IEnumerable<Question> collection)
		{
			foreach (Question obj in collection)
			{
				Update(obj);
			}
		}
		#endregion
	}
}

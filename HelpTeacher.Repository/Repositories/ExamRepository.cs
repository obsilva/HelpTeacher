// Since: 2018-09-24
// Authors: 
//		Otávio Bueno Silva <obsilva94@gmail.com>

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

using HelpTeacher.Domain.Entities;
using HelpTeacher.Repository.IRepositories;

namespace HelpTeacher.Repository.Repositories
{
	/// <inheritdoc />
	public class ExamRepository : IExamRepository
	{
		#region Constructors
		public ExamRepository() { }
		#endregion


		#region Methods
		/// <inheritdoc />
		public void Add(Exam obj)
		{
			var questions = new StringBuilder();
			var subjects = new StringBuilder();

			foreach (Question question in obj.Questions)
			{
				questions.Append(question.RecordID).Append(", ");
			}

			foreach (Subject subject in obj.Subjects)
			{
				subjects.Append(subject.RecordID).Append(", ");
			}

			string query = $"INSERT INTO htd1 (D1_COD, D1_TIPO, D1_INEDITA, D1_QUESTAO, D1_MATERIA, " +
						   $"D1_DATA, D_E_L_E_T) VALUES (NULL, {obj.Type}, " +
						   $"{(obj.HasOnlyUnusedQuestion ? "*" : "NULL")}, '{questions}', '{subjects}', " +
						   $"{obj.GeneratedDate}, NULL)";
			ConnectionManager.ExecuteQuery(query);
		}

		/// <inheritdoc />
		public void Add(IEnumerable<Exam> collection)
		{
			foreach (Exam obj in collection)
			{
				Add(obj);
			}
		}

		/// <inheritdoc />
		public Exam First()
		{
			string query = $"SELECT D1_COD, D1_TIPO, D1_INEDITA, D1_QUESTAO, D1_MATERIA, D1_DATA, D_E_L_E_T" +
						   $"FROM htd1 LIMIT 1";

			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(query))
			{
				var output = new Exam(new List<Question>(), new List<Subject>());
				if (dataReader.HasRows)
				{
					dataReader.Read();

					var questions = new List<Question>();
					var subjects = new List<Subject>();

					string[] ids = dataReader.GetString(3).Split(new char[] { ',' },
						StringSplitOptions.RemoveEmptyEntries);
					foreach (string id in ids)
					{
						questions.Add(new QuestionRepository().Get(Convert.ToInt32(id)));
					}

					ids = dataReader.GetString(4).Split(new char[] { ',' },
						StringSplitOptions.RemoveEmptyEntries);
					foreach (string id in ids)
					{
						subjects.Add(new SubjectRepository().Get(Convert.ToInt32(id)));
					}

					output.GeneratedDate = dataReader.GetDateTime(5);
					output.HasOnlyUnusedQuestion = dataReader.IsDBNull(2);
					output.IsRecordActive = dataReader.IsDBNull(6);
					output.Questions = questions;
					output.RecordID = dataReader.GetInt32(0);
					output.Subjects = subjects;
					output.Type = dataReader.GetChar(1);
				}

				return output;
			}
		}

		/// <inheritdoc />
		public IQueryable<Exam> Get()
		{
			string query = $"SELECT D1_COD, D1_TIPO, D1_INEDITA, D1_QUESTAO, D1_MATERIA, D1_DATA, D_E_L_E_T" +
						   $"FROM htd1";

			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(query))
			{
				var output = new List<Exam>();
				if (dataReader.HasRows)
				{
					while (dataReader.Read())
					{
						var questions = new List<Question>();
						var subjects = new List<Subject>();

						string[] ids = dataReader.GetString(3).Split(new char[] { ',' },
							StringSplitOptions.RemoveEmptyEntries);
						foreach (string id in ids)
						{
							questions.Add(new QuestionRepository().Get(Convert.ToInt32(id)));
						}

						ids = dataReader.GetString(4).Split(new char[] { ',' },
							StringSplitOptions.RemoveEmptyEntries);
						foreach (string id in ids)
						{
							subjects.Add(new SubjectRepository().Get(Convert.ToInt32(id)));
						}

						output.Add(new Exam(questions, subjects)
						{
							GeneratedDate = dataReader.GetDateTime(5),
							HasOnlyUnusedQuestion = dataReader.IsDBNull(2),
							IsRecordActive = dataReader.IsDBNull(6),
							RecordID = dataReader.GetInt32(0),
							Type = dataReader.GetChar(1)
						});
					}
				}

				return output.AsQueryable();
			}
		}

		/// <inheritdoc />
		public IQueryable<Exam> Get(bool isRecordActive)
		{
			string query = $"SELECT D1_COD, D1_TIPO, D1_INEDITA, D1_QUESTAO, D1_MATERIA, D1_DATA, D_E_L_E_T" +
						   $"FROM htd1 WHERE D_E_L_E_T {(isRecordActive ? "IS" : "IS NOT")} NULL";

			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(query))
			{
				var output = new List<Exam>();
				if (dataReader.HasRows)
				{
					while (dataReader.Read())
					{
						var questions = new List<Question>();
						var subjects = new List<Subject>();

						string[] ids = dataReader.GetString(3).Split(new char[] { ',' },
							StringSplitOptions.RemoveEmptyEntries);
						foreach (string id in ids)
						{
							questions.Add(new QuestionRepository().Get(Convert.ToInt32(id)));
						}

						ids = dataReader.GetString(4).Split(new char[] { ',' },
							StringSplitOptions.RemoveEmptyEntries);
						foreach (string id in ids)
						{
							subjects.Add(new SubjectRepository().Get(Convert.ToInt32(id)));
						}

						output.Add(new Exam(questions, subjects)
						{
							GeneratedDate = dataReader.GetDateTime(5),
							HasOnlyUnusedQuestion = dataReader.IsDBNull(2),
							IsRecordActive = dataReader.IsDBNull(6),
							RecordID = dataReader.GetInt32(0),
							Type = dataReader.GetChar(1)
						});
					}
				}

				return output.AsQueryable();
			}
		}

		/// <inheritdoc />
		public Exam Get(int id)
		{
			string query = $"SELECT D1_COD, D1_TIPO, D1_INEDITA, D1_QUESTAO, D1_MATERIA, D1_DATA, D_E_L_E_T" +
						   $"FROM htd1 WHERE D1_COD = {id}";

			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(query))
			{
				var output = new Exam(new List<Question>(), new List<Subject>());
				if (dataReader.HasRows)
				{
					dataReader.Read();

					var questions = new List<Question>();
					var subjects = new List<Subject>();

					string[] ids = dataReader.GetString(3).Split(new char[] { ',' },
						StringSplitOptions.RemoveEmptyEntries);
					foreach (string itemId in ids)
					{
						questions.Add(new QuestionRepository().Get(Convert.ToInt32(itemId)));
					}

					ids = dataReader.GetString(4).Split(new char[] { ',' },
						StringSplitOptions.RemoveEmptyEntries);
					foreach (string itemId in ids)
					{
						subjects.Add(new SubjectRepository().Get(Convert.ToInt32(itemId)));
					}

					output.GeneratedDate = dataReader.GetDateTime(5);
					output.HasOnlyUnusedQuestion = dataReader.IsDBNull(2);
					output.IsRecordActive = dataReader.IsDBNull(6);
					output.Questions = questions;
					output.RecordID = dataReader.GetInt32(0);
					output.Subjects = subjects;
					output.Type = dataReader.GetChar(1);
				}

				return output;
			}
		}

		/// <inheritdoc />
		public void Update(Exam obj)
		{
			var questions = new StringBuilder();
			var subjects = new StringBuilder();

			foreach (Question question in obj.Questions)
			{
				questions.Append(question.RecordID).Append(", ");
			}

			foreach (Subject subject in obj.Subjects)
			{
				subjects.Append(subject.RecordID).Append(", ");
			}

			string query = $"UPDATE htd1 SET D1_TIPO = {obj.Type}, D1_INEDITA = " +
						   $"{(obj.HasOnlyUnusedQuestion ? "*" : "NULL")}, D1_QUESTAO = '{questions}', " +
						   $"D1_MATERIA = '{subjects}', D1_DATA = {obj.GeneratedDate}, D_E_L_E_T = " +
						   $"{(obj.IsRecordActive ? "*" : "NULL")} WHERE D1_COD = {obj.RecordID}";
			ConnectionManager.ExecuteQuery(query);
		}

		/// <inheritdoc />
		public void Update(IEnumerable<Exam> collection)
		{
			foreach (Exam obj in collection)
			{
				Update(obj);
			}
		}
		#endregion
	}
}

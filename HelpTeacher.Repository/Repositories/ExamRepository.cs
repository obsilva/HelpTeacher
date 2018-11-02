// This Source Code Form is subject to the terms of the Mozilla 
// Public License, v. 2.0. If a copy of the MPL was not distributed 
// with this file, You can obtain one at https://mozilla.org/MPL/2.0/

// Since: 2018/09/24
// Authors: 
//		Otávio Bueno Silva <obsilva94@gmail.com>

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

using HelpTeacher.Domain.Entities;
using HelpTeacher.Repository.IRepositories;
using HelpTeacher.Util;

namespace HelpTeacher.Repository.Repositories
{
	/// <inheritdoc />
	public class ExamRepository : IExamRepository
	{
		#region Constants
		private const string QueryInsert = "INSERT INTO htd1 (D1_COD, D1_TIPO, D1_INEDITA, D1_QUESTAO, D1_MATERIA, D1_DATA, D_E_L_E_T) VALUES (NULL, @D1_TIPO, @D1_INEDITA, @D1_QUESTAO, '', @D1_DATA, 0);";

		private const string QuerySelect = "SELECT D1_COD, D1_TIPO, D1_INEDITA, D1_QUESTAO, D1_MATERIA, D1_DATA, D_E_L_E_T FROM htd1 LIMIT @LIMIT OFFSET @OFFSET;";

		private const string QuerySelectActive = "SELECT D1_COD, D1_TIPO, D1_INEDITA, D1_QUESTAO, D1_MATERIA, D1_DATA, D_E_L_E_T FROM htd1 WHERE (D_E_L_E_T = @IS_DELETED) LIMIT @LIMIT OFFSET @OFFSET;";

		private const string QuerySelectID = "SELECT D1_COD, D1_TIPO, D1_INEDITA, D1_QUESTAO, D1_MATERIA, D1_DATA, D_E_L_E_T FROM htd1 WHERE (D1_COD = @D1_COD);";

		private const string QueryUpdate = "UPDATE htd1 SET D1_TIPO = @D1_TIPO, D1_INEDITA = @D1_INEDITA, D1_QUESTAO = @D1_QUESTAO, D1_MATERIA = '', D1_DATA = @D1_DATA, D_E_L_E_T = @D_E_L_E_T WHERE D1_COD = @D1_COD;";
		#endregion


		#region Properties
		/// <summary>Gerenciador de conexão.</summary>
		public ConnectionManager Connection { get; set; }

		/// <summary>Valor de offset na recuperação de registros.</summary>
		public int Offset { get; set; }

		/// <summary>Tamanho da página de registros.</summary>
		public int PageSize { get; set; }
		#endregion


		#region Constructors
		/// <summary>
		/// Inicializa uma nova instância de <see cref="ExamRepository"/>. É possível definir o
		/// gerenciador conexão a ser usado e/ou o tamanho da página de registros.
		/// </summary>
		/// <param name="connection">Gerenciador de conexão a ser usado.</param>
		/// <param name="pageSize">Número máximo de registros para retornar por vez.</param>
		public ExamRepository(ConnectionManager connection = null, int pageSize = 50)
		{
			if (connection == null)
			{
				connection = new ConnectionManager();
			}

			Connection = connection;
			Offset = 0;
			PageSize = pageSize;
		}
		#endregion


		#region Methods
		/// <inheritdoc />
		public void Add(Exam obj)
		{
			Checker.NullObject(obj, nameof(obj));

			var questions = new StringBuilder(obj.Questions.First().RecordID.ToString());

			for (int i = 1; i < obj.Questions.Count; i++)
			{
				questions.Append($",{obj.Questions.ElementAt(i).RecordID}");
			}

			Connection.ExecuteQuery(QueryInsert, obj.Type, obj.HasOnlyUnusedQuestion, questions.ToString(),
				obj.GeneratedDate.ToShortDateString());
		}

		/// <inheritdoc />
		public void Add(IEnumerable<Exam> collection)
		{
			Checker.NullObject(collection, nameof(collection));

			foreach (Exam obj in collection)
			{
				Add(obj);
			}
		}

		/// <inheritdoc />
		public Exam First()
		{
			using (DbDataReader dataReader = Connection.ExecuteReader(QuerySelect, 1, 0))
			{
				IQueryable<Exam> records = ReadDataReader(dataReader);

				return records.FirstOrDefault() ?? Exam.Null;
			}
		}

		/// <inheritdoc />
		public IQueryable<Exam> Get()
		{
			using (DbDataReader dataReader = Connection.ExecuteReader(QuerySelect, PageSize, Offset))
			{
				return ReadDataReader(dataReader);
			}
		}

		/// <inheritdoc />
		public IQueryable<Exam> Get(bool isRecordActive)
		{
			using (DbDataReader dataReader = Connection.ExecuteReader(QuerySelectActive,
				!isRecordActive, PageSize, Offset))
			{
				return ReadDataReader(dataReader);
			}
		}

		/// <inheritdoc />
		public Exam Get(int id)
		{
			using (DbDataReader dataReader = Connection.ExecuteReader(QuerySelectID, id))
			{
				IQueryable<Exam> records = ReadDataReader(dataReader);

				return records.FirstOrDefault() ?? Exam.Null;
			}
		}

		/// <summary>Faz a leitura do <see cref="DbDataReader"/>.</summary>
		/// <param name="dataReader">Objeto para ler.</param>
		/// <returns>Todos os objetos no <see cref="DbDataReader"/>.</returns>
		private IQueryable<Exam> ReadDataReader(DbDataReader dataReader)
		{
			var output = new List<Exam>();

			if (dataReader.HasRows)
			{
				while (dataReader.Read())
				{
					var questions = new List<Question>();
					var subjects = new List<Subject>();

					string[] ids = dataReader.GetString(3).Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
					foreach (string id in ids)
					{
						questions.Add(new QuestionRepository(Connection).Get(Convert.ToInt32(id)));
					}

					subjects = questions.Select(item => item.Subject).Distinct().ToList();

					output.Add(new Exam(questions, subjects)
					{
						GeneratedDate = Convert.ToDateTime(dataReader.GetString(5)),
						HasOnlyUnusedQuestion = (dataReader.GetInt32(2) == 1),
						IsRecordActive = (dataReader.GetInt32(6) == 0),
						RecordID = dataReader.GetInt32(0),
						Type = dataReader.GetChar(1)
					});
				}
			}

			dataReader.Close();
			return output.AsQueryable();
		}

		/// <inheritdoc />
		public void Update(Exam obj)
		{
			Checker.NullObject(obj, nameof(obj));

			var questions = new StringBuilder(obj.Questions.First().RecordID.ToString());

			for (int i = 1; i < obj.Questions.Count; i++)
			{
				questions.Append($",{obj.Questions.ElementAt(i).RecordID}");
			}

			Connection.ExecuteQuery(QueryUpdate, obj.Type, obj.HasOnlyUnusedQuestion, questions.ToString(),
				obj.GeneratedDate.ToShortDateString(), !obj.IsRecordActive, obj.RecordID);
		}

		/// <inheritdoc />
		public void Update(IEnumerable<Exam> collection)
		{
			Checker.NullObject(collection, nameof(collection));

			foreach (Exam obj in collection)
			{
				Update(obj);
			}
		}
		#endregion
	}
}

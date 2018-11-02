// This Source Code Form is subject to the terms of the Mozilla 
// Public License, v. 2.0. If a copy of the MPL was not distributed 
// with this file, You can obtain one at https://mozilla.org/MPL/2.0/

// Since: 2018/09/16
// Authors: 
//		Otávio Bueno Silva <obsilva94@gmail.com>

using System;
using System.Collections.Generic;
using System.Linq;

using HelpTeacher.Util;

namespace HelpTeacher.Domain.Entities
{
	/// <summary>Define a entidade avaliação.</summary>
	public class Exam : IEntityBase, IEquatable<Exam>
	{
		#region Fields
		private ICollection<Question> _questions;

		private ICollection<Subject> _subjects;
		#endregion


		#region Properties
		/// <summary>Data de geração da avaliação.</summary>
		public DateTime GeneratedDate { get; set; }

		/// <summary>
		/// Define se a avaliação é inédita, criada apenas com perguntas não utilizadas anteriormente.
		/// </summary>
		public bool HasOnlyUnusedQuestion { get; set; }

		/// <inheritdoc />
		public bool IsRecordActive { get; set; }

		/// <inheritdoc />
		public bool IsNull => Equals(Null);

		/// <summary>Recupera uma nova instância vazia, considerada <see langword="null"/>.</summary>
		/// <remarks>A instância vazia pode ser considerada um objeto padrão.</remarks>
		/// <returns>Nova instância vazia.</returns>
		public static Exam Null => new Exam()
		{
			_questions = new List<Question>(),
			_subjects = new List<Subject>(),

			GeneratedDate = DateTime.MinValue,
			HasOnlyUnusedQuestion = false,
			IsRecordActive = false,
			RecordID = -1,
			Type = 'M'
		};

		/// <summary><see cref="Question"/>'s utilizadas na geração da avaliação.</summary>
		public ICollection<Question> Questions
		{
			get => _questions;
			set
			{
				Checker.NullObject(value, nameof(Questions));
				Checker.Value(value.Count, nameof(Questions), 1);

				_questions = value;
			}
		}

		/// <inheritdoc />
		public int RecordID { get; set; }

		/// <summary><see cref="Subject"/>'s a qual a avaliação pertence.</summary>
		public ICollection<Subject> Subjects
		{
			get => _subjects;
			set
			{
				Checker.NullObject(value, nameof(Subjects));
				Checker.Value(value.Count, nameof(Subjects), 1);

				_subjects = value;
			}
		}

		/// <summary>
		/// Tipo da avaliação.
		/// <para>Pode ser: Mista, Objetiva ou Dissertativa</para>
		/// </summary>
		public char Type { get; set; }
		#endregion


		#region Constructors
		private Exam() { }

		/// <summary>
		/// Inicializa uma nova instância da classe <see cref="Exam"/> com as
		/// <see cref="Question"/> e <see cref="Subject"/> específicados.
		/// </summary>
		/// <param name="questions">Questões utilizadas na avaliação.</param>
		/// <param name="subjects">Assuntos abordados na avaliação.</param>
		public Exam(ICollection<Question> questions, ICollection<Subject> subjects)
		{
			Questions = questions;
			Subjects = subjects;
		}
		#endregion


		#region Methods
		/// <summary>Determina se as duas avaliações especificadas possuem valores iguais.</summary>
		/// <param name="exam1">A primeira avaliação para comparar, ou <see langword="null"/>.</param>
		/// <param name="exam2">A segunda avaliação para comparar, ou <see langword="null"/>.</param>
		/// <returns>
		/// <see langword="true"/> se os valores em <paramref name="exam1"/> forem iguais que
		/// em <paramref name="exam2"/>; <see langword="false"/> caso contrário.
		/// </returns>
		public static bool operator ==(Exam exam1, Exam exam2)
			=> EqualityComparer<Exam>.Default.Equals(exam1, exam2);

		/// <summary>Determina se as duas avaliações especificadas possuem valores diferentes.</summary>
		/// <param name="exam1">A primeira avaliação para comparar, ou <see langword="null"/>.</param>
		/// <param name="exam2">A segunda avaliação para comparar, ou <see langword="null"/>.</param>
		/// <returns>
		/// <see langword="true"/> se os valores em <paramref name="exam1"/> forem diferentes que
		/// em <paramref name="exam2"/>; <see langword="false"/> caso contrário.
		/// </returns>
		public static bool operator !=(Exam exam1, Exam exam2)
			=> !(exam1 == exam2);

		/// <inheritdoc />
		public bool Equals(Exam other)
		{
			if (other == null)
			{
				return false;
			}

			if (RecordID != other.RecordID)
			{
				return false;
			}

			if (GeneratedDate != other.GeneratedDate)
			{
				return false;
			}

			if (HasOnlyUnusedQuestion != other.HasOnlyUnusedQuestion)
			{
				return false;
			}

			if (IsRecordActive != other.IsRecordActive)
			{
				return false;
			}

			if (Type != other.Type)
			{
				return false;
			}

			if ((Questions.Count != other.Questions.Count) || !Questions.All(other.Questions.Contains))
			{
				return false;
			}

			if ((Subjects.Count != other.Subjects.Count) || !Subjects.All(other.Subjects.Contains))
			{
				return false;
			}

			return true;
		}
		#endregion


		#region Overrides
		/// <inheritdoc />
		public override bool Equals(object obj)
			=> Equals(obj as Exam);

		/// <inheritdoc />
		public override int GetHashCode()
		{
			int hashCode = -1147934649;
			hashCode = (hashCode * -1521134295) + GeneratedDate.GetHashCode();
			hashCode = (hashCode * -1521134295) + HasOnlyUnusedQuestion.GetHashCode();
			hashCode = (hashCode * -1521134295) + IsRecordActive.GetHashCode();
			hashCode = (hashCode * -1521134295) + EqualityComparer<ICollection<Question>>.Default.GetHashCode(Questions);
			hashCode = (hashCode * -1521134295) + RecordID.GetHashCode();
			hashCode = (hashCode * -1521134295) + EqualityComparer<ICollection<Subject>>.Default.GetHashCode(Subjects);
			hashCode = (hashCode * -1521134295) + Type.GetHashCode();
			return hashCode;
		}
		#endregion
	}
}

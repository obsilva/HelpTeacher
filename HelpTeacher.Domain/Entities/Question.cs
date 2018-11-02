// This Source Code Form is subject to the terms of the Mozilla 
// Public License, v. 2.0. If a copy of the MPL was not distributed 
// with this file, You can obtain one at https://mozilla.org/MPL/2.0/

// Since: 2018/09/15
// Authors: 
//		Otávio Bueno Silva <obsilva94@gmail.com>

using System;
using System.Collections.Generic;

using HelpTeacher.Util;

namespace HelpTeacher.Domain.Entities
{
	/// <summary>Define a entidade questão.</summary>
	public class Question : IEntityBase, IEquatable<Question>
	{
		#region Constants
		/// <summary>Representa o comprimento máximo de <see cref="FirstAttachment"/> e <see cref="SecondAttachment"/>.</summary>
		public const int AttachmentMaxLength = 260;

		/// <summary>Representa o comprimento máximo do <see cref="Statement"/>.</summary>
		public const int StatementMaxLength = UInt16.MaxValue;
		#endregion


		#region Fields
		private string _firstAttachment;
		private string _secondAttachment;
		private string _statement;
		private Subject _subject;
		#endregion


		#region Properties
		/// <summary>Caminho completo para o primeiro anexo.</summary>
		public string FirstAttachment
		{
			get => _firstAttachment;
			set
			{
				Checker.StringLength(value, nameof(FirstAttachment), AttachmentMaxLength);

				_firstAttachment = value;
			}
		}

		/// <summary>
		/// Define se a questão é padrão.
		/// <para>
		/// Uma questão definida como padrão sempre será usada ao gerar uma avaliação
		/// para a <see cref="Entities.Subject"/> a qual ela está relacionada.
		/// </para>
		/// </summary>
		public bool IsDefault { get; set; }

		/// <summary>Define se a questão é do fipo objetiva.</summary>
		public bool IsObjective { get; set; }

		/// <inheritdoc />
		public bool IsRecordActive { get; set; }

		/// <inheritdoc />
		public bool IsNull => Equals(Null);

		/// <summary>Recupera uma nova instância vazia, considerada <see langword="null"/>.</summary>
		/// <remarks>A instância vazia pode ser considerada um objeto padrão.</remarks>
		/// <returns>Nova instância vazia.</returns>
		public static Question Null => new Question()
		{
			_firstAttachment = String.Empty,
			_secondAttachment = String.Empty,
			_statement = String.Empty,
			_subject = Subject.Null,

			IsDefault = false,
			IsObjective = false,
			IsRecordActive = false,
			RecordID = -1,
			WasUsed = true
		};

		/// <inheritdoc />
		public int RecordID { get; set; }

		/// <summary>Caminho completo para o segundo anexo.</summary>
		public string SecondAttachment
		{
			get => _secondAttachment;
			set
			{
				Checker.StringLength(value, nameof(SecondAttachment), AttachmentMaxLength);

				_secondAttachment = value;
			}
		}

		/// <summary>Enunciado da questão.</summary>
		public string Statement
		{
			get => _statement;
			set
			{
				Checker.NullOrEmptyString(value, nameof(Statement));
				Checker.StringLength(value, nameof(Statement), AttachmentMaxLength);

				_statement = value;
			}

		}

		/// <summary><see cref="Entities.Subject"/> onde a questão pode ser usada.</summary>
		public virtual Subject Subject
		{
			get => _subject;
			set
			{
				Checker.NullObject(value, nameof(Subject));

				_subject = value;
			}
		}

		/// <summary>Define se a questão já foi usada alguma vez.</summary>
		public bool WasUsed { get; set; }
		#endregion


		#region Constructors
		private Question() { }

		/// <summary>
		/// Inicializa uma nova instância da classe <see cref="Question"/> com os 
		/// <see cref = "Subject" /> e enunciado específicados.
		/// </summary>
		/// <param name="subject">Assunto onde a nova questão pode ser usada.</param>
		/// <param name="statement">Enunciado</param>
		public Question(Subject subject, string statement)
		{
			Subject = subject;
			Statement = statement;


			FirstAttachment = String.Empty;
			IsDefault = false;
			IsObjective = false;
			IsRecordActive = true;
			RecordID = 0;
			SecondAttachment = String.Empty;
			WasUsed = false;
		}
		#endregion


		#region Methods
		/// <summary>Determina se as duas questões especificadas possuem valores iguais.</summary>
		/// <param name="question1">A primeira questão para comparar, ou <see langword="null"/>.</param>
		/// <param name="question2">A segunda questão para comparar, ou <see langword="null"/>.</param>
		/// <returns>
		/// <see langword="true"/> se os valores em <paramref name="question1"/> forem iguais que
		/// em <paramref name="question2"/>; <see langword="false"/> caso contrário.
		/// </returns>
		public static bool operator ==(Question question1, Question question2)
			=> EqualityComparer<Question>.Default.Equals(question1, question2);

		/// <summary>Determina se as duas questões especificadas possuem valores diferentes.</summary>
		/// <param name="question1">A primeiro questão para comparar, ou <see langword="null"/>.</param>
		/// <param name="question2">A segunda questão para comparar, ou <see langword="null"/>.</param>
		/// <returns>
		/// <see langword="true"/> se os valores em <paramref name="question1"/> forem diferentes que
		/// em <paramref name="question2"/>; <see langword="false"/> caso contrário.
		/// </returns>
		public static bool operator !=(Question question1, Question question2)
			=> !(question1 == question2);

		/// <inheritdoc />
		public bool Equals(Question other)
		{
			if (other == null)
			{
				return false;
			}

			if (RecordID != other.RecordID)
			{
				return false;
			}

			if (IsDefault != other.IsDefault)
			{
				return false;
			}

			if (IsObjective != other.IsObjective)
			{
				return false;
			}

			if (FirstAttachment != other.FirstAttachment)
			{
				return false;
			}

			if (SecondAttachment != other.SecondAttachment)
			{
				return false;
			}

			if (IsRecordActive != other.IsRecordActive)
			{
				return false;
			}

			if (WasUsed != other.WasUsed)
			{
				return false;
			}

			if (Statement != other.Statement)
			{
				return false;
			}

			if (!Subject.Equals(other.Subject))
			{
				return false;
			}

			return true;
		}
		#endregion


		#region Overrides
		/// <inheritdoc />
		public override bool Equals(object obj)
			=> Equals(obj as Question);

		/// <inheritdoc />
		public override int GetHashCode()
		{
			int hashCode = -1551942074;
			hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(FirstAttachment);
			hashCode = (hashCode * -1521134295) + IsDefault.GetHashCode();
			hashCode = (hashCode * -1521134295) + IsObjective.GetHashCode();
			hashCode = (hashCode * -1521134295) + IsRecordActive.GetHashCode();
			hashCode = (hashCode * -1521134295) + RecordID.GetHashCode();
			hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(SecondAttachment);
			hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(Statement);
			hashCode = (hashCode * -1521134295) + EqualityComparer<Subject>.Default.GetHashCode(Subject);
			hashCode = (hashCode * -1521134295) + WasUsed.GetHashCode();
			return hashCode;
		}
		#endregion
	}
}

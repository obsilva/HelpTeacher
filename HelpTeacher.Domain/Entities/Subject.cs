// This Source Code Form is subject to the terms of the Mozilla 
// Public License, v. 2.0. If a copy of the MPL was not distributed 
// with this file, You can obtain one at https://mozilla.org/MPL/2.0/

// Since: 2018/09/01
// Authors: 
//		Otávio Bueno Silva <obsilva94@gmail.com>

using System;
using System.Collections.Generic;

using HelpTeacher.Util;

namespace HelpTeacher.Domain.Entities
{
	/// <summary>Define a entidade assunto.</summary>
	public class Subject : IEntityBase, IEquatable<Subject>
	{
		#region Constants
		/// <summary>Representa o comprimento máximo do <see cref="Name"/>.</summary>
		public const int NameMaxLength = 120;
		#endregion


		#region Fields
		private Discipline _discipline;
		private string _name;
		#endregion


		#region Properties
		/// <summary><see cref="Entities.Discipline"/> na qual o assunto está sendo lecionado.</summary>
		public virtual Discipline Discipline
		{
			get => _discipline;
			set
			{
				Checker.NullObject(value, nameof(Discipline));

				_discipline = value;
			}
		}

		/// <inheritdoc />
		public bool IsRecordActive { get; set; }

		/// <inheritdoc />
		public bool IsNull => Equals(Null);

		/// <summary>Nome completo do assunto.</summary>
		public string Name
		{
			get => _name;
			set
			{
				Checker.NullOrEmptyString(value, nameof(Name));
				Checker.StringLength(value, nameof(Name), NameMaxLength);

				_name = value;
			}
		}

		/// <summary>Recupera uma nova instância vazia, considerada <see langword="null"/>.</summary>
		/// <remarks>A instância vazia pode ser considerada um objeto padrão.</remarks>
		/// <returns>Nova instância vazia.</returns>
		public static Subject Null => new Subject()
		{
			_name = String.Empty,

			Discipline = Discipline.Null,
			IsRecordActive = false,
			RecordID = -1
		};

		/// <inheritdoc />
		public int RecordID { get; set; }
		#endregion


		#region Constructors
		private Subject() { }

		/// <summary>
		/// Inicializa uma nova instância da classe <see cref="Subject"/> com a
		/// <see cref="Discipline"/> e nome especificados.
		/// </summary>
		/// <param name="discipline">Disciplina onde o assunto é lecionado.</param>
		/// <param name="name">Nome completo do assunto.</param>
		public Subject(Discipline discipline, string name)
		{
			Discipline = discipline;
			Name = name;
		}
		#endregion


		#region Methods
		/// <summary>Determina se os dois assuntos especificados possuem valores iguais.</summary>
		/// <param name="subject1">O primeiro assunto para comparar, ou <see langword="null"/>.</param>
		/// <param name="subject2">O segundoa assunto para comparar, ou <see langword="null"/>.</param>
		/// <returns>
		/// <see langword="true"/> se os valores em <paramref name="subject1"/> forem iguais que
		/// em <paramref name="subject2"/>; <see langword="false"/> caso contrário.
		/// </returns>
		public static bool operator ==(Subject subject1, Subject subject2)
			=> EqualityComparer<Subject>.Default.Equals(subject1, subject2);

		/// <summary>Determina se os dois assuntos especificados possuem valores diferentes.</summary>
		/// <param name="subject1">O primeiro assuntos para comparar, ou <see langword="null"/>.</param>
		/// <param name="subject2">O segundo assuntos para comparar, ou <see langword="null"/>.</param>
		/// <returns>
		/// <see langword="true"/> se os valores em <paramref name="subject1"/> forem diferentes que
		/// em <paramref name="subject2"/>; <see langword="false"/> caso contrário.
		/// </returns>
		public static bool operator !=(Subject subject1, Subject subject2)
			=> !(subject1 == subject2);

		/// <inheritdoc />
		public bool Equals(Subject other)
		{
			if (other == null)
			{
				return false;
			}

			if (RecordID != other.RecordID)
			{
				return false;
			}

			if (IsRecordActive != other.IsRecordActive)
			{
				return false;
			}

			if (Name != other.Name)
			{
				return false;
			}

			if (!Discipline.Equals(other.Discipline))
			{
				return false;
			}

			return true;
		}
		#endregion


		#region Overrides
		/// <inheritdoc />
		public override bool Equals(object obj) => Equals(obj as Subject);

		/// <inheritdoc />
		public override int GetHashCode()
		{
			int hashCode = -1526528328;
			hashCode = (hashCode * -1521134295) + EqualityComparer<Discipline>.Default.GetHashCode(Discipline);
			hashCode = (hashCode * -1521134295) + IsRecordActive.GetHashCode();
			hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(Name);
			hashCode = (hashCode * -1521134295) + RecordID.GetHashCode();
			return hashCode;
		}
		#endregion
	}
}

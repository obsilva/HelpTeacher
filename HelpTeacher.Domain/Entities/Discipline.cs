// This Source Code Form is subject to the terms of the Mozilla 
// Public License, v. 2.0. If a copy of the MPL was not distributed 
// with this file, You can obtain one at https://mozilla.org/MPL/2.0/

// Since: 2018/08/31
// Authors: 
//		Otávio Bueno Silva <obsilva94@gmail.com>

using System;
using System.Collections.Generic;

namespace HelpTeacher.Domain.Entities
{
	/// <summary>Define a entidade disciplina.</summary>
	public class Discipline : IEntityBase, IEquatable<Discipline>
	{
		#region Properties
		/// <summary><see cref="Course"/> onde a disciplina é lecionada.</summary>
		public virtual Course Course { get; set; }

		/// <inheritdoc />
		public bool IsRecordActive { get; set; }

		/// <inheritdoc />
		public bool IsNull => Equals(Null);

		/// <summary>Nome completo da disciplina.</summary>
		public string Name { get; set; }

		/// <summary>Recupera uma nova instância vazia, considerada <see langword="null"/>.</summary>
		/// <remarks>A instância vazia pode ser considerada um objeto padrão.</remarks>
		/// <returns>Nova instância vazia.</returns>
		public static Discipline Null => new Discipline()
		{
			Course = Course.Null,
			IsRecordActive = false,
			Name = String.Empty,
			RecordID = -1,
			Subjects = new List<Subject>()
		};

		/// <inheritdoc />
		public int RecordID { get; set; }

		/// <summary><see cref="Subject"/> lecionados na disciplina.</summary>
		public virtual ICollection<Subject> Subjects { get; set; }
		#endregion


		#region Constructors
		private Discipline() { }

		/// <summary>
		/// Inicializa uma nova instância da clasee <see cref="Discipline"/> com o
		/// <see cref="Course"/> e nome especificados.
		/// </summary>
		/// <param name="course">Curso onde a disciplina é lecionada.</param>
		/// <param name="name">Nome completo da disciplina.</param>
		public Discipline(Course course, string name)
		{
			Course = course;
			Name = name;
		}
		#endregion


		#region Class Methods
		/// <summary>Determina se as duas disciplinas especificadas possuem valores iguais.</summary>
		/// <param name="discipline1">A primeira disciplina para comparar, ou <see langword="null"/>.</param>
		/// <param name="discipline2">A segunda disciplina para comparar, ou <see langword="null"/>.</param>
		/// <returns>
		/// <see langword="true"/> se os valores em <paramref name="discipline1"/> forem iguais que
		/// em <paramref name="discipline2"/>; <see langword="false"/> caso contrário.
		/// </returns>
		public static bool operator ==(Discipline discipline1, Discipline discipline2)
			=> EqualityComparer<Discipline>.Default.Equals(discipline1, discipline2);

		/// <summary>Determina se as duas disciplinas especificadas possuem valores diferentes.</summary>
		/// <param name="discipline1">A primeira disciplina para comparar, ou <see langword="null"/>.</param>
		/// <param name="discipline2">A segunda disciplina para comparar, ou <see langword="null"/>.</param>
		/// <returns>
		/// <see langword="true"/> se os valores em <paramref name="discipline1"/> forem diferentes que
		/// em <paramref name="discipline2"/>; <see langword="false"/> caso contrário.
		/// </returns>
		public static bool operator !=(Discipline discipline1, Discipline discipline2)
			=> !(discipline1 == discipline2);
		#endregion


		#region Instance Methods
		/// <inheritdoc />
		public bool Equals(Discipline other)
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

			if (!Course.Equals(other.Course))
			{
				return false;
			}

			return true;
		}
		#endregion


		#region Overrides
		/// <inheritdoc />
		public override bool Equals(object obj)
			=> Equals(obj as Discipline);

		/// <inheritdoc />
		public override int GetHashCode()
		{
			int hashCode = -334468231;
			hashCode = (hashCode * -1521134295) + EqualityComparer<Course>.Default.GetHashCode(Course);
			hashCode = (hashCode * -1521134295) + IsRecordActive.GetHashCode();
			hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(Name);
			hashCode = (hashCode * -1521134295) + RecordID.GetHashCode();
			return hashCode;
		}


		#endregion
	}
}
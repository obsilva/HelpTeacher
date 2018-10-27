// This Source Code Form is subject to the terms of the Mozilla 
// Public License, v. 2.0. If a copy of the MPL was not distributed 
// with this file, You can obtain one at https://mozilla.org/MPL/2.0/

// Since: 2018/08/31
// Authors: 
//		Otávio Bueno Silva <obsilva94@gmail.com>

using System;
using System.Collections.Generic;
using System.Linq;

namespace HelpTeacher.Domain.Entities
{
	/// <summary>Define a entidade disciplina.</summary>
	public class Discipline : IEntityBase, IEquatable<Discipline>
	{
		#region Properties
		/// <summary><see cref="Course"/>'s onde a disciplina é lecionada.</summary>
		public virtual ICollection<Course> Courses { get; set; }

		/// <inheritdoc />
		public bool IsRecordActive { get; set; }

		/// <summary>Nome completo da disciplina.</summary>
		public string Name { get; set; }

		/// <inheritdoc />
		public int RecordID { get; set; }

		/// <summary><see cref="Subject"/> lecionados na disciplina.</summary>
		public virtual ICollection<Subject> Subjects { get; set; }
		#endregion


		#region Constructors
		/// <summary>
		/// Inicializa uma nova instância da clasee <see cref="Discipline"/> com o
		/// <see cref="Courses"/> e nome especificados.
		/// </summary>
		/// <param name="courses">Cursos onde a disciplina é lecionada.</param>
		/// <param name="name">Nome completo da disciplina.</param>
		public Discipline(ICollection<Course> courses, string name)
		{
			Courses = courses;
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

			if (!Courses.FirstOrDefault().Equals(other.Courses.FirstOrDefault()))
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
			int hashCode = -1040917842;
			hashCode = (hashCode * -1521134295) + EqualityComparer<ICollection<Course>>.Default.GetHashCode(Courses);
			hashCode = (hashCode * -1521134295) + IsRecordActive.GetHashCode();
			hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(Name);
			hashCode = (hashCode * -1521134295) + RecordID.GetHashCode();
			return hashCode;
		}
		#endregion
	}
}
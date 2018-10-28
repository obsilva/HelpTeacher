// This Source Code Form is subject to the terms of the Mozilla 
// Public License, v. 2.0. If a copy of the MPL was not distributed 
// with this file, You can obtain one at https://mozilla.org/MPL/2.0/

// Since: 2018/08/31
// Authors: 
//		Otávio Bueno Silva <obsilva94@gmail.com>

using System;
using System.Collections.Generic;

using HelpTeacher.Util;

namespace HelpTeacher.Domain.Entities
{
	/// <summary>Define a entidade curso.</summary>
	public class Course : IEntityBase, IEquatable<Course>
	{
		#region Constants
		/// <summary>Representa o comprimento máximo do <see cref="Name"/>.</summary>
		public const int NameMaxLength = 40;
		#endregion


		#region Fields
		private string _name;
		#endregion


		#region Properties
		/// <inheritdoc />
		public bool IsRecordActive { get; set; }

		/// <inheritdoc />
		public bool IsNull => Equals(Null);

		/// <summary>Nome completo do curso.</summary>
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
		public static Course Null => new Course()
		{
			_name = String.Empty,

			IsRecordActive = false,
			RecordID = -1
		};

		/// <inheritdoc />
		public int RecordID { get; set; }
		#endregion


		#region Constructors
		private Course() { }

		/// <summary>
		/// Inicializa uma nova instância da classe <see cref="Course"/> com o
		/// nome especificado.
		/// </summary>
		/// <param name="name">Nome completo do curso.</param>
		public Course(string name) => Name = name;
		#endregion


		#region Methods
		/// <summary>Determina se os dois cursos especificados possuem valores iguais.</summary>
		/// <param name="course1">O primeiro curso para comparar, ou <see langword="null"/>.</param>
		/// <param name="course2">O segundo curso para comparar, ou <see langword="null"/>.</param>
		/// <returns>
		/// <see langword="true"/> se os valores em <paramref name="course1"/> forem iguais que
		/// em <paramref name="course2"/>; <see langword="false"/> caso contrário.
		/// </returns>
		public static bool operator ==(Course course1, Course course2)
			=> EqualityComparer<Course>.Default.Equals(course1, course2);

		/// <summary>Determina se os dois cursos especificados possuem valores diferentes.</summary>
		/// <param name="course1">O primeiro curso para comparar, ou <see langword="null"/>.</param>
		/// <param name="course2">O segundo curso para comparar, ou <see langword="null"/>.</param>
		/// <returns>
		/// <see langword="true"/> se os valores em <paramref name="course1"/> forem diferentes que
		/// em <paramref name="course2"/>; <see langword="false"/> caso contrário.
		/// </returns>
		public static bool operator !=(Course course1, Course course2)
			=> !(course1 == course2);

		/// <inheritdoc />
		public bool Equals(Course other)
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

			return true;
		}
		#endregion


		#region Overrides
		/// <inheritdoc />
		public override bool Equals(object obj)
			=> Equals(obj as Course);

		/// <inheritdoc />
		public override int GetHashCode()
		{
			int hashCode = 849708453;
			hashCode = (hashCode * -1521134295) + IsRecordActive.GetHashCode();
			hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(Name);
			hashCode = (hashCode * -1521134295) + RecordID.GetHashCode();
			return hashCode;
		}
		#endregion
	}
}
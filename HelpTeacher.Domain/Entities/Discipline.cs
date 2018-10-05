// This Source Code Form is subject to the terms of the Mozilla 
// Public License, v. 2.0. If a copy of the MPL was not distributed 
// with this file, You can obtain one at https://mozilla.org/MPL/2.0/

// Since: 2018/08/31
// Authors: 
//		Otávio Bueno Silva <obsilva94@gmail.com>

using System.Collections.Generic;

namespace HelpTeacher.Domain.Entities
{
	/// <summary>Define a entidade disciplina.</summary>
	public class Discipline : IEntityBase
	{
		#region Properties
		/// <summary><see cref="Course"/>'s onde a disciplina é lecionada.</summary>
		public virtual ICollection<Course> Courses { get; set; }

		/// <inheritdoc />
		public bool IsRecordActive { get; set; }

		/// <summary>Iniciais da disciplina (sigla).</summary>
		public string Initials { get; set; }

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
	}
}
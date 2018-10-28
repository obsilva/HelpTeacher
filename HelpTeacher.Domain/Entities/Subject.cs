// This Source Code Form is subject to the terms of the Mozilla 
// Public License, v. 2.0. If a copy of the MPL was not distributed 
// with this file, You can obtain one at https://mozilla.org/MPL/2.0/

// Since: 2018/09/01
// Authors: 
//		Otávio Bueno Silva <obsilva94@gmail.com>

using System;
using System.Collections.Generic;

namespace HelpTeacher.Domain.Entities
{
	/// <summary>Define a entidade assunto.</summary>
	public class Subject : IEntityBase
	{
		#region Properties
		/// <summary><see cref="Entities.Discipline"/> na qual o assunto está sendo lecionado.</summary>
		public virtual Discipline Discipline { get; set; }

		/// <inheritdoc />
		public bool IsRecordActive { get; set; }

		/// <inheritdoc />
		public bool IsNull => Equals(Null);

		/// <summary>Nome completo do assunto.</summary>
		public string Name { get; set; }

		/// <summary>Recupera uma nova instância vazia, considerada <see langword="null"/>.</summary>
		/// <remarks>A instância vazia pode ser considerada um objeto padrão.</remarks>
		/// <returns>Nova instância vazia.</returns>
		public static Subject Null => new Subject()
		{
			Discipline = Discipline.Null,
			IsRecordActive = false,
			Name = String.Empty,
			Questions = new List<Question>(),
			RecordID = -1
		};

		/// <summary><see cref="Question"/>'s disponíveis no assunto.</summary>
		public virtual ICollection<Question> Questions { get; set; }

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
	}
}

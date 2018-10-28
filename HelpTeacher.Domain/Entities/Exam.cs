// This Source Code Form is subject to the terms of the Mozilla 
// Public License, v. 2.0. If a copy of the MPL was not distributed 
// with this file, You can obtain one at https://mozilla.org/MPL/2.0/

// Since: 2018/09/16
// Authors: 
//		Otávio Bueno Silva <obsilva94@gmail.com>

using System;
using System.Collections.Generic;

namespace HelpTeacher.Domain.Entities
{
	/// <summary>Define a entidade avaliação.</summary>
	public class Exam : IEntityBase
	{
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
			GeneratedDate = DateTime.MinValue,
			HasOnlyUnusedQuestion = false,
			IsRecordActive = false,
			Questions = new List<Question>(),
			RecordID = -1,
			Subjects = new List<Subject>(),
			Type = 'D'
		};

		/// <summary><see cref="Question"/>'s utilizadas na geração da avaliação.</summary>
		public ICollection<Question> Questions { get; set; }

		/// <inheritdoc />
		public int RecordID { get; set; }

		/// <summary><see cref="Subject"/>'s a qual a avaliação pertence.</summary>
		public ICollection<Subject> Subjects { get; set; }

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
	}
}

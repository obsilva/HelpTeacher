/* Authors: Otávio Bueno Silva <obsilva94@gmail.com>
 * Since: 2018-09-16
 */

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

		/// <summary><see cref="Question"/>'s utilizadas na geração da avaliação.</summary>
		public ICollection<Question> Questions { get; set; }

		/// <inheritdoc />
		public int RecordID { get; set; }

		/// <summary><see cref="Subject"/>'s a qual a avaliação pertence.</summary>
		public ICollection<Subject> Subjects { get; set; }

		/// <summary>
		/// Tipo da avaliação.
		/// <para>Pode ser: Mista, Objetivaoou Dissertativa</para>
		/// </summary>
		public char Type { get; set; }
		#endregion


		#region Constructors
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

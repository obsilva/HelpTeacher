using System;
using System.Collections.Generic;

namespace HelpTeacher.Domain.Entities
{
	/// <summary>Define a entidade avaliação.</summary>
	public class Exam : IEntityBase
	{
		#region Properties
		public DateTime GeneratedDate { get; set; }

		/// <summary>
		/// Define se a avaliação é inédita, criada apenas com perguntas não utilizadas anteriormente.
		/// </summary>
		public bool HasOnlyUnusedQuestion { get; set; }

		/// <summary>Implementa <see cref="P:HelpTeacher.Domain.Entities.IEntityBase.IsRecordActive" />.</summary>
		public bool IsRecordActive { get; set; }

		/// <summary><see cref="Question"/>'s utilizadas na geração da avaliação.</summary>
		public ICollection<Question> Questions { get; set; }

		/// <summary>Implementa <see cref="P:HelpTeacher.Domain.Entities.IEntityBase.RecordID" />.</summary>
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
		public Exam(ICollection<Question> questions, ICollection<Subject> subjects)
		{
			Questions = questions;
			Subjects = subjects;
		}

		#endregion
	}
}

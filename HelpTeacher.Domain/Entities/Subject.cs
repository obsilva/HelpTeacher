/* Authors: Otávio Bueno Silva <obsilva94@gmail.com>
 * Since: 2018-09-01
 */

using System.Collections.Generic;

namespace HelpTeacher.Domain.Entities
{
	/// <summary>Define a entidade assunto.</summary>
	public class Subject : IEntityBase
	{
		#region Properties
		/// <summary><see cref="Entities.Discipline"/> na qual o assunto está sendo lecionado.</summary>
		public virtual Discipline Discipline { get; set; }

		/// <summary>Implementa <see cref="IEntityBase.IsRecordActive"/>.</summary>
		public bool IsRecordActive { get; set; }

		/// <summary>Nome completo do assunto.</summary>
		public string Name { get; set; }

		/// <summary><see cref="Question"/>'s disponíveis no assunto.</summary>
		public virtual ICollection<Question> Questions { get; set; }

		/// <summary>Implementa <see cref="IEntityBase.RecordID"/>.</summary>
		public int RecordID { get; set; }
		#endregion


		#region Constructors
		/// <summary>Construtor.</summary>
		/// <param name="value">Disciplina onde o assunto é lecionado.</param>
		/// <param name="name">Nome completo do assunto.</param>
		public Subject(Discipline value, string name)
		{
			Discipline = value;
			Name = name;
		}
		#endregion
	}
}

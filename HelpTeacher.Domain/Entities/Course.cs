/* Authors: Otávio Bueno Silva <obsilva94@gmail.com>
* Since: 2018-08-31
*/

using System.Collections.Generic;

namespace HelpTeacher.Domain.Entities
{
	/// <summary>Define a entidade curso.</summary>
	public class Course : IEntityBase
	{
		#region Properties
		/// <summary><see cref="Discipline"/> oferecidas pelo curso.</summary>
		public virtual ICollection<Discipline> Disciplines { get; set; }

		/// <summary>Implementa <see cref="IEntityBase.IsRecordActive"/>.</summary>
		public bool IsRecordActive { get; set; }

		/// <summary>Iniciais do curso (sigla).</summary>
		public string Initials { get; set; }

		/// <summary>Nome completo do curso.</summary>
		public string Name { get; set; }

		/// <summary>Implementa <see cref="IEntityBase.RecordID"/>.</summary>
		public int RecordID { get; set; }
		#endregion


		#region Constructors
		/// <summary>Construtor.</summary>
		/// <param name="name">Nome completo do curso.</param>
		public Course(string name) => Name = name;
		#endregion
	}
}
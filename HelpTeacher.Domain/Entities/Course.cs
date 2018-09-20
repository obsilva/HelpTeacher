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

		/// <inheritdoc />
		public bool IsRecordActive { get; set; }

		/// <summary>Iniciais do curso (sigla).</summary>
		public string Initials { get; set; }

		/// <summary>Nome completo do curso.</summary>
		public string Name { get; set; }

		/// <inheritdoc />
		public int RecordID { get; set; }
		#endregion


		#region Constructors
		/// <summary>
		/// Inicializa uma nova instância da classe <see cref="Course"/> com o
		/// nome especificado.
		/// </summary>
		/// <param name="name">Nome completo do curso.</param>
		public Course(string name) => Name = name;
		#endregion
	}
}
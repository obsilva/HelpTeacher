/* Authors: Otávio Bueno Silva <obsilva94@gmail.com>
 * Since: 2018-08-31
 */

namespace HelpTeacher.Domain.Entities
{
	/// <summary>Define a entidade disciplina.</summary>
	public class Discipline : IEntityBase
	{
		#region Properties
		/// <summary><see cref="Entities.Course"/> no qual a disciplina está sendo lecionada.</summary>
		public virtual Course Course { get; set; }

		/// <summary>Implementa <see cref="IEntityBase.IsRecordActive"/>.</summary>
		public bool IsRecordActive { get; set; }

		/// <summary>Iniciais da disciplina (sigla).</summary>
		public string Initials { get; set; }

		/// <summary>Nome completo da disciplina.</summary>
		public string Name { get; set; }

		/// <summary>Implementa <see cref="IEntityBase.RecordID"/>.</summary>
		public int RecordID { get; set; }
		#endregion


		#region Constructors
		/// <summary>Construtor.</summary>
		/// <param name="value">Curso no qual a disciplina é lecionada.</param>
		/// <param name="name">Nome completo da disciplina.</param>
		public Discipline(Course value, string name)
		{
			Course = value;
			Name = name;
		}
		#endregion
	}
}
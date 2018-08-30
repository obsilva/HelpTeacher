/* Authors: Otávio Bueno Silva <obsilva94@gmail.com>
 * Since: 2018-08-30
 */

namespace HelpTeacher.Domain.Entities
{
	/// <summary>Define as informações básicas de uma entidade.</summary>
	public interface IEntityBase
	{
		/// <summary>Determina se o registro está ativo.</summary>
		bool IsRecordActive { get; set; }

		/// <summary>Identificador único do registro.</summary>
		int RecordID { get; set; }
	}
}
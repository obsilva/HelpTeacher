/* Authors: Otávio Bueno Silva <obsilva94@gmail.com>
 * Since: 2018-09-24
 */

using HelpTeacher.Domain.Entities;

namespace HelpTeacher.Repository.IRepositories
{
	/// <summary>Define o repositório de <see cref="Exam"/>.</summary>
	public interface IExamRepository : IRepositoryBase<Exam>
	{
	}
}
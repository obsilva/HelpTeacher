/* Authors: Otávio Bueno Silva <obsilva94@gmail.com>
 * Since: 2018-09-19
 */

using System.Linq;

using HelpTeacher.Domain.Entities;

namespace HelpTeacher.Repository.IRepositories
{
	/// <summary>Define o repositório de cursos.</summary>
	public interface ICourseRepository : IRepositoryBase<Course>
	{
		#region Methods
		/// <summary>Recupera todos os registros ativos e diferentes do objeto informado.</summary>
		/// <param name="obj">Objeto que deve ser evitado.</param>
		/// <returns>Todos os registros ativos, com exceção do objeto informado.</returns>
		IQueryable<Course> GetWhereDifferentId(Course obj);

		/// <summary>Recupera todos os registros ativos e que possuem ID diferente do informado.</summary>
		/// <param name="id">Identificador que deve ser evitado.</param>
		/// <returns>Todos os registros ativos, com exceção do registro com ID informado.</returns>
		IQueryable<Course> GetWhereDifferentId(int id);
		#endregion
	}
}
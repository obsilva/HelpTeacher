/* Authors: Otávio Bueno Silva <obsilva94@gmail.com>
 * Since: 2018-09-19
 */

using System.Linq;

using HelpTeacher.Domain.Entities;

namespace HelpTeacher.Repository.IRepositories
{
	/// <summary>Define o repositório de <see cref="Discipline"/>.</summary>
	public interface IDisciplineRepository : IRepositoryBase<Discipline>
	{
		#region Methods
		/// <summary>Recupera todos os registros ativos e diferentes do objeto informado.</summary>
		/// <param name="obj">Objeto que deve ser evitado.</param>
		/// <returns>Todos os registros ativos, com exceção do objeto informado.</returns>
		IQueryable<Discipline> GetWhereDifferentId(Discipline obj);

		/// <summary>Recupera todos os registros ativos e que possuem ID diferente do informado.</summary>
		/// <param name="id">Identificador que deve ser evitado.</param>
		/// <returns>Todos os registros ativos, com exceção do registro com o ID informado.</returns>
		IQueryable<Discipline> GetWhereDifferentId(int id);

		/// <summary>Recupera todos os registros que possuem relação o objeto informado.</summary>
		/// <param name="obj">Objeto que deve ser buscado.</param>
		/// <returns>Todos os registros com relacionados ao objeto especificado.</returns>
		IQueryable<Discipline> GetWhereCourse(Course obj);

		/// <summary>Recupera todos os registros que possuem relação o objeto de ID informado.</summary>
		/// <param name="obj">ID do que deve ser buscado.</param>
		/// <returns>Todos os registros com relacionados ao ID especificado.</returns>
		IQueryable<Discipline> GetWhereCourse(int id);

		/// <summary>
		/// Recupera todos os registros ativos ou não e que possuem relação o <see cref="Course"/> especificado.
		/// </summary>
		/// <param name="obj">Objeto que deve ser buscado.</param>
		/// <param name="isRecordActive">Registro deve estar ativo?</param>
		/// <returns>Todos os registros com relacionados ao objeto especificado.</returns>
		IQueryable<Discipline> GetWhereCourse(Course obj, bool isRecordActive);

		/// <summary>
		/// Recupera todos os registros ativos ou não e que possuem o <see cref="Course"/> com o ID especificado.
		/// </summary>
		/// <param name="obj">Objeto que deve ser buscado.</param>
		/// /// <param name="isRecordActive">Registro deve estar ativo?</param>
		/// <returns>Todos os registros com relacionados ao ID especificado.</returns>
		IQueryable<Discipline> GetWhereCourse(int id, bool isRecordActive);
		#endregion
	}
}
using System.Linq;

using HelpTeacher.Domain.Entities;

namespace HelpTeacher.Repository.IRepositories
{
	public interface ISubjectRepository : IRepositoryBase<Subject>
	{
		#region Methods
		/// <summary>Recupera todos os registros que possuem relação o objeto informado.</summary>
		/// <param name="obj">Objeto que deve ser buscado.</param>
		/// <returns>Todos os registros com relacionados ao objeto especificado.</returns>
		IQueryable<Subject> GetWhereCourse(Discipline obj);

		/// <summary>Recupera todos os registros que possuem relação o objeto de ID informado.</summary>
		/// <param name="obj">ID do que deve ser buscado.</param>
		/// <returns>Todos os registros com relacionados ao ID especificado.</returns>
		IQueryable<Subject> GetWhereCourse(int id);

		/// <summary>
		/// Recupera todos os registros ativos ou não e que possuem relação o objeto informado.
		/// </summary>
		/// <param name="obj">Objeto que deve ser buscado.</param>
		/// <param name="isRecordActive">Registro deve estar ativo?</param>
		/// <returns>Todos os registros com relacionados ao objeto especificado.</returns>
		IQueryable<Discipline> GetWhereCourse(Course obj, bool isRecordActive);

		/// <summary>Recupera todos os registros ativos ou não e que possuem relação o objeto de ID informado.</summary>
		/// <param name="obj">Objeto que deve ser buscado.</param>
		/// /// <param name="isRecordActive">Registro deve estar ativo?</param>
		/// <returns>Todos os registros com relacionados ao ID especificado.</returns>
		IQueryable<Discipline> GetWhereCourse(int id, bool isRecordActive);
		#endregion
	}
}
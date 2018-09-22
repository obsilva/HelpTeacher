using System.Linq;

using HelpTeacher.Domain.Entities;

namespace HelpTeacher.Repository.IRepositories
{
	/// <summary>Define o repositório de <see cref="Subject"/>.</summary>
	public interface ISubjectRepository : IRepositoryBase<Subject>
	{
		#region Methods
		/// <summary>
		/// Recupera todos os registros que correspondem à informação especificada.
		/// 
		/// <para>O objeto informado é considerado uma chave estrangeira e seu ID é utilizado na busca.</para>
		/// </summary>
		/// <param name="obj">Objeto da chave estrangeira.</param>
		/// <returns>Todos os registros selecionados com o filtro.</returns>
		IQueryable<Subject> GetWhereDiscipline(Discipline obj);

		/// <summary>
		/// Recupera todos os registros que correspondem à informação especificada.
		///
		/// <para>O ID informado é considerado uma chave estrangeira e é utilizado na busca.</para>
		/// </summary>
		/// <param name="id">ID da chave estrangeira.</param>
		/// <returns>Todos os registros selecionados com o filtro.</returns>
		IQueryable<Subject> GetWhereDiscipline(int id);

		/// <summary>
		/// Recupera todos os registros que correspondem às informações especificadas, considerando uma
		/// cláusula AND - todos os requisitos devem ser satisfeitos para que o registro seja selecionado.
		///
		/// <para>O objeto informado é considerado uma chave estrangeira e seu ID é utilizado na busca.</para>
		/// </summary>
		/// <param name="obj">Objeto da chave estrangeira.</param>
		/// <param name="isRecordActive">Registro deve estar ativo?</param>
		/// <returns>Todos os registros selecionados com o filtro.</returns>
		IQueryable<Subject> GetWhereDiscipline(Discipline obj, bool isRecordActive);

		/// <summary>
		/// Recupera todos os registros que correspondem às informações especificadas, considerando uma
		/// cláusula AND - todos os requisitos devem ser satisfeitos para que o registro seja selecionado.
		///
		/// <para>O ID informado é considerado uma chave estrangeira e é utilizado na busca.</para>
		/// </summary>
		/// <param name="id">ID da chave estrangeira.</param>
		/// <param name="isRecordActive">Registro deve estar ativo?</param>
		/// <returns>Todos os registros selecionados com o filtro.</returns>
		IQueryable<Subject> GetWhereDiscipline(int id, bool isRecordActive);
		#endregion
	}
}
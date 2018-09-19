/* Authors: Otávio Bueno Silva <obsilva94@gmail.com>
 * Since: 2018-09-18
 */

using System.Collections.Generic;
using System.Linq;

using HelpTeacher.Domain.Entities;

namespace HelpTeacher.Repository.IRepositories
{
	/// <summary>Define o repositório base.</summary>
	public interface IRepositoryBase<TEntity> where TEntity : IEntityBase
	{
		/// <summary>Adiciona um novo registro à base de dados.</summary>
		/// <param name="obj">Objeto que deve ser adicionado.</param>
		void Add(TEntity obj);

		/// <summary>Adiciona vários registros à base de dados.</summary>
		/// <param name="collection">Coleção com os objetos que devem ser adicionados.</param>
		void Add(IEnumerable<TEntity> collection);

		/// <summary>Define um registro como desativado. </summary>
		/// <param name="obj">Objeto que deve ser atualizado. </param>
		void Disable(TEntity obj);

		/// <summary>Define vários registros como desativados. </summary>
		/// <param name="list">Coleção com os objetos que devem ser atualizados. </param>
		void Disable(IEnumerable<TEntity> list);

		/// <summary>Define um registro como desativado. </summary>
		/// <param name="id">Número ID do objeto que deve ser atualizado. </param>
		void Disable(int id);

		/// <summary>Recupera o primeiro registro.</summary>
		/// <returns>Objeto <see cref="TEntity"/></returns>
		TEntity First();

		/// <summary>Recupera todos os registros.</summary>
		/// <returns>
		/// Todos os objetos em um <see cref="IQueryable{TEntity}"/>, permitindo a execução de consutlas.
		/// </returns>
		IQueryable<TEntity> Get();

		/// <summary>Recupera todos os registros que estão ativos ou não.</summary>
		/// <param name="IsRecordActive">Registro deve estar ativo?</param>
		/// <returns>Todos os objetos ativos ou não.</returns>
		IQueryable<TEntity> Get(bool IsRecordActive);

		/// <summary>Recupera um registro específico.</summary>
		/// <param name="id">Número ID do objeto que deve ser recuperado.</param>
		/// <returns>Objeto <see cref="TEntity"/> se existir, null caso contrário.</returns>
		TEntity Get(int id);

		/// <summary>Atualiza um registro na base de dados.</summary>
		/// <param name="obj">Objeto que deve ser atualizado.</param>
		void Update(TEntity obj);

		/// <summary>Atualiza vários registros na base de dados.</summary>
		/// <param name="list">Coleção com os objetos que devem ser atualizados.</param>
		void Update(IEnumerable<TEntity> list);
	}
}

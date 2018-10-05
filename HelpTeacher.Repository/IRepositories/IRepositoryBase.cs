// This Source Code Form is subject to the terms of the Mozilla 
// Public License, v. 2.0. If a copy of the MPL was not distributed 
// with this file, You can obtain one at https://mozilla.org/MPL/2.0/

// Since: 2018/09/18
// Authors: 
//		Otávio Bueno Silva <obsilva94@gmail.com>

using System.Collections.Generic;
using System.Linq;

using HelpTeacher.Domain.Entities;

namespace HelpTeacher.Repository.IRepositories
{
	/// <summary>Define um repositório base genérico.</summary>
	/// <typeparam name="TEntity">
	/// Define o tipo do repositório. Para ser considerado um tipo válido, deve herdar <see cref="IEntityBase"/>.
	/// </typeparam>
	public interface IRepositoryBase<TEntity> where TEntity : IEntityBase
	{
		#region Methods
		/// <summary>Adiciona um novo registro à base de dados.</summary>
		/// <param name="obj">Objeto que deve ser adicionado.</param>
		void Add(TEntity obj);

		/// <summary>Adiciona vários registros à base de dados.</summary>
		/// <param name="collection">Coleção com os objetos que devem ser adicionados.</param>
		void Add(IEnumerable<TEntity> collection);

		/// <summary>Recupera o primeiro registro.</summary>
		/// <returns>Objeto <see cref="TEntity"/></returns>
		TEntity First();

		/// <summary>Recupera todos os registros.</summary>
		/// <returns>
		/// Todos os objetos em um <see cref="IQueryable{TEntity}"/>, permitindo a execução de consutlas,
		/// ordenados pelo ID de forma crescente..
		/// </returns>
		IQueryable<TEntity> Get();

		/// <summary>Recupera todos os registros que estão ativos ou não.</summary>
		/// <param name="isRecordActive">Registro deve estar ativo?</param>
		/// <returns>Todos os objetos ativos ou não, ordenados pelo ID de forma crescente..</returns>
		IQueryable<TEntity> Get(bool isRecordActive);

		/// <summary>Recupera um registro específico.</summary>
		/// <param name="id">Número ID do objeto que deve ser recuperado.</param>
		/// <returns>Objeto <see cref="TEntity"/> se existir, null caso contrário.</returns>
		TEntity Get(int id);

		/// <summary>Atualiza um registro na base de dados.</summary>
		/// <param name="obj">Objeto que deve ser atualizado.</param>
		void Update(TEntity obj);

		/// <summary>Atualiza vários registros na base de dados.</summary>
		/// <param name="collection">Coleção com os objetos que devem ser atualizados.</param>
		void Update(IEnumerable<TEntity> collection);
		#endregion
	}
}

// This Source Code Form is subject to the terms of the Mozilla 
// Public License, v. 2.0. If a copy of the MPL was not distributed 
// with this file, You can obtain one at https://mozilla.org/MPL/2.0/

// Since: 2018/09/19
// Authors: 
//		Otávio Bueno Silva <obsilva94@gmail.com>

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

		/// <summary>
		/// Recupera todos os registros que correspondem à informação especificada.
		/// 
		/// <para>O objeto informado é considerado uma chave estrangeira e seu ID é utilizado na busca.</para>
		/// </summary>
		/// <param name="obj">Objeto da chave estrangeira.</param>
		/// <returns>Todos os registros selecionados com o filtro.</returns>
		IQueryable<Discipline> GetWhereCourse(Course obj);

		/// <summary>
		/// Recupera todos os registros que correspondem à informação especificada.
		///
		/// <para>O ID informado é considerado uma chave estrangeira e é utilizado na busca.</para>
		/// </summary>
		/// <param name="id">ID da chave estrangeira.</param>
		/// <returns>Todos os registros selecionados com o filtro.</returns>
		IQueryable<Discipline> GetWhereCourse(int id);

		/// <summary>
		/// Recupera todos os registros que correspondem às informações especificadas, considerando uma
		/// cláusula AND - todos os requisitos devem ser satisfeitos para que o registro seja selecionado.
		///
		/// <para>O objeto informado é considerado uma chave estrangeira e seu ID é utilizado na busca.</para>
		/// </summary>
		/// <param name="obj">Objeto da chave estrangeira.</param>
		/// <param name="isRecordActive">Registro deve estar ativo?</param>
		/// <returns>Todos os registros selecionados com o filtro.</returns>
		IQueryable<Discipline> GetWhereCourse(Course obj, bool isRecordActive);

		/// <summary>
		/// Recupera todos os registros que correspondem à informação especificada.
		///
		/// <para>O ID informado é considerado uma chave estrangeira e é utilizado na busca.</para>
		/// </summary>
		/// <param name="id">ID da chave estrangeira.</param>
		/// <param name="isRecordActive">Registro deve estar ativo?</param>
		/// <returns>Todos os registros selecionados com o filtro.</returns>
		IQueryable<Discipline> GetWhereCourse(int id, bool isRecordActive);
		#endregion
	}
}
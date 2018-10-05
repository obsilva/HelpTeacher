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
// This Source Code Form is subject to the terms of the Mozilla 
// Public License, v. 2.0. If a copy of the MPL was not distributed 
// with this file, You can obtain one at https://mozilla.org/MPL/2.0/

// Since: 2018/09/24
// Authors: 
//		Otávio Bueno Silva <obsilva94@gmail.com>

using HelpTeacher.Domain.Entities;

namespace HelpTeacher.Repository.IRepositories
{
	/// <summary>Define o repositório de <see cref="Exam"/>.</summary>
	public interface IExamRepository : IRepositoryBase<Exam>
	{
	}
}
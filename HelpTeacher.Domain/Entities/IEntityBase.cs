// This Source Code Form is subject to the terms of the Mozilla 
// Public License, v. 2.0. If a copy of the MPL was not distributed 
// with this file, You can obtain one at https://mozilla.org/MPL/2.0/

// Since: 2018/08/30
// Authors: 
//		Otávio Bueno Silva <obsilva94@gmail.com>

namespace HelpTeacher.Domain.Entities
{
	/// <summary>Define as informações básicas de uma entidade.</summary>
	public interface IEntityBase
	{
		/// <summary>Determina se o registro está ativo.</summary>
		bool IsRecordActive { get; set; }

		/// <summary>Identificador único do registro.</summary>
		int RecordID { get; set; }
	}
}
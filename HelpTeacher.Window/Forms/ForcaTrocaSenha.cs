// This Source Code Form is subject to the terms of the Mozilla 
// Public License, v. 2.0. If a copy of the MPL was not distributed 
// with this file, You can obtain one at https://mozilla.org/MPL/2.0/

// Authors: 
//		Otávio Bueno Silva <obsilva94@gmail.com>

using System;
using System.Windows.Forms;

using HelpTeacher.Classes;
using HelpTeacher.Domain.Entities;
using HelpTeacher.Repository;

namespace HelpTeacher.Forms
{
	public partial class ForcaTrocaSenha : Form
	{
		#region Properties
		private ConnectionManager ConnectionManager { get; }
		#endregion


		public ForcaTrocaSenha() => InitializeComponent();

		private void ForcaTrocaSenha_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				SelectNextControl(ActiveControl, !e.Shift, true, true, true);
			}
		}

		private void txtConfirmacao_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				trocaSenha();
			}
		}

		private void btnAlterar_Click(object sender, EventArgs e) => trocaSenha();

		private void trocaSenha()
		{
			if (txtNovaSenha.Text == txtConfirmacao.Text)
			{
				string query = $"UPDATE hta1 SET A1_PWD = '{MD5.gerarHash(txtNovaSenha.Text)}', " +
							   $"A1_ALTPWD = 0 WHERE A1_COD = {User.Instance.RecordID}";

				ConnectionManager.ExecuteQuery(query);
				DialogResult = DialogResult.OK;
			}
			else
			{
				Mensagem.senhasDiferentes();
			}
		}
	}
}

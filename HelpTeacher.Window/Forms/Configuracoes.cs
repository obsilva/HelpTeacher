// This Source Code Form is subject to the terms of the Mozilla 
// Public License, v. 2.0. If a copy of the MPL was not distributed 
// with this file, You can obtain one at https://mozilla.org/MPL/2.0/

// Authors: 
//		Otávio Bueno Silva <obsilva94@gmail.com>

using System;
using System.Data.Common;
using System.Windows.Forms;

using HelpTeacher.Classes;
using HelpTeacher.Domain.Entities;
using HelpTeacher.Repository;

namespace HelpTeacher.Forms
{
	public partial class Configuracoes : Form
	{
		public Configuracoes() => InitializeComponent();

		private void Configuracoes_Load(object sender, EventArgs e) => txtLogin.Text = User.Instance.Username;

		private void txtLogin_Click(object sender, EventArgs e)
		{
			var evento = e as MouseEventArgs;
			txtLogin.Select(txtLogin.GetCharIndexFromPosition(evento.Location), 0);
		}

		private void btnSalvar_Click(object sender, EventArgs e)
		{
			alteraDados();
			Mensagem.dadosAlterados();
			atualizaDadosUsuario();
			Close();
		}

		private void btnCancelar_Click(object sender, EventArgs e) => Close();

		private void alteraDados()
		{

			ConnectionManager.ExecuteQuery("UPDATE hta1 SET A1_LOGIN = '" + txtLogin.Text +
										   "' WHERE A1_COD = " + User.Instance.RecordID);

			if (!(txtNovaSenha.Text.Equals("") && txtConfirmacao.Text.Equals("")))
			{
				if (txtNovaSenha.Text == txtConfirmacao.Text)
				{
					ConnectionManager.ExecuteQuery("UPDATE hta1 SET A1_PWD = '" +
												   MD5.gerarHash(txtNovaSenha.Text) + "' " +
												   "WHERE A1_COD = " + User.Instance.RecordID);
				}
				else
				{
					Mensagem.senhasDiferentes();
					return;
				}
			}
			if (chkStopBD.Checked)
			{
				ConnectionManager.ExecuteQuery("UPDATE hta1 SET A1_STOPBD = '1' WHERE A1_COD = " +
											   User.Instance.RecordID);
			}
			else
			{
				ConnectionManager.ExecuteQuery("UPDATE hta1 SET A1_STOPBD = '0' WHERE A1_COD = " +
											   User.Instance.RecordID);
			}
		}

		private void atualizaDadosUsuario()
		{
			string query = $"SELECT * FROM hta1 WHERE A1_COD = {User.Instance.RecordID}";
			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(query))
			{
				dataReader.Read();
				User.Instance.Username = dataReader["A1_LOGIN"].ToString();
				User.Instance.Password = dataReader["A1_PWD"].ToString();
			}
		}
	}
}

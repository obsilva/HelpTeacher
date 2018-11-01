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
	public partial class Login : Form
	{
		#region Properties
		private ConnectionManager ConnectionManager { get; }
		#endregion


		public Login()
		{
			try
			{
				ConnectionManager = new ConnectionManager();

				InitializeComponent();
			}
			catch (Exception)
			{
				Application.Exit();
			}
		}

		private void Login_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				SelectNextControl(ActiveControl, !e.Shift, true, true, true);
			}
		}

		private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				fazLogin();
			}
		}

		private void btnEntrarLogin_Click(object sender, EventArgs e) => fazLogin();

		private void btnCancelarLogin_Click(object sender, EventArgs e) => Application.Exit();

		/* tabelasOK
		 * 
		 * Testa se as tebelas necessárias existem. Se alguma tabela não existir,
		 * chama a função que tenta criar todas as tebelas
		 */
		public bool tabelasOK()
		{
			string query = $"SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE table_type = 'BASE TABLE' " +
						   $"AND table_schema='helpteacher' ORDER BY table_name ASC";

			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(query))
			{
				if (dataReader.HasRows)
				{
					dataReader.Read();
					if (dataReader["TABLE_NAME"].ToString().Equals("hta1"))
					{
						dataReader.Read();
						if (dataReader["TABLE_NAME"].ToString().Equals("htb1"))
						{
							dataReader.Read();
							if (dataReader["TABLE_NAME"].ToString().Equals("htc1"))
							{
								dataReader.Read();
								if (dataReader["TABLE_NAME"].ToString().Equals("htc2"))
								{
									dataReader.Read();
									if (dataReader["TABLE_NAME"].ToString().Equals("htc3"))
									{
										dataReader.Read();
										if (dataReader["TABLE_NAME"].ToString().Equals("htd1"))
										{
											return true;
										}
									}
								}
							}
						}
					}
				}
				Mensagem.tabelasBancoFaltando();
				criaTabelas();
			}
			return false;
		}

		/* criaTabelas
		 * 
		 * Cria as tabelas faltantes no banco de dados
		 */
		private void criaTabelas()
		{
			/* USUÁRIOS */
			ConnectionManager.ExecuteQuery("CREATE TABLE IF NOT EXISTS hta1 (" +
						"A1_COD int(10) UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT, " +
						"A1_LOGIN varchar(50) NOT NULL, " +
						"A1_PWD varchar(32) NOT NULL, " +
						"A1_ALTPWD varchar(1) DEFAULT NULL, " +
						"A1_STOPBD varchar(1) DEFAULT NULL" +
					") ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8");
			ConnectionManager.ExecuteQuery("INSERT INTO hta1 VALUES" +
					"(1, 'admin', '21232F297A57A5A743894A0E4A801FC3', NULL, NULL), " +
					"(2, 'user', 'D41D8CD98F00B204E9800998ECF8427E', NULL, NULL)");

			/* QUESTÕES */
			ConnectionManager.ExecuteQuery("CREATE TABLE IF NOT EXISTS htb1 (" +
						"B1_COD int(10) UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT, " +
						"B1_QUEST text NOT NULL, " +
						"B1_OBJETIV varchar(1) DEFAULT NULL, " +
						"B1_ARQUIVO varchar(520) DEFAULT NULL, " +
						"B1_USADA varchar(1) DEFAULT NULL, " +
						"B1_MATERIA int(10) UNSIGNED NOT NULL, " +
						"B1_PADRAO varchar(1) DEFAULT NULL, " +
						"D_E_L_E_T varchar(1) DEFAULT NULL" +
					") ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8");

			/* CURSOS */
			ConnectionManager.ExecuteQuery("CREATE TABLE IF NOT EXISTS htc1 (" +
						"C1_COD int(10) UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT, " +
						"C1_NOME varchar(40) NOT NULL, " +
						"D_E_L_E_T varchar(1) DEFAULT NULL" +
					") ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8");

			/* DISCIPLINAS */
			ConnectionManager.ExecuteQuery("CREATE TABLE IF NOT EXISTS htc2 (" +
						"C2_COD int(10) UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT, " +
						"C2_NOME varchar(80) NOT NULL, " +
						"C2_CURSO int(11) UNSIGNED NOT NULL, " +
						"D_E_L_E_T varchar(1) DEFAULT NULL" +
					") ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8");

			/* MATÉRIAS */
			ConnectionManager.ExecuteQuery("CREATE TABLE IF NOT EXISTS htc3 (" +
						"C3_COD int(10) UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT, " +
						"C3_NOME varchar(120) NOT NULL, " +
						"C3_DISCIPL int(10) UNSIGNED NOT NULL, " +
						"D_E_L_E_T varchar(1) DEFAULT NULL" +
					") ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8");

			/* AVALIAÇÕES */
			ConnectionManager.ExecuteQuery("CREATE TABLE IF NOT EXISTS htd1 (" +
						"D1_COD int(10) UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT, " +
						"D1_TIPO varchar(1) NOT NULL, " +
						"D1_INEDITA varchar(1) DEFAULT NULL, " +
						"D1_QUESTAO varchar(100) NOT NULL, " +
						"D1_MATERIA varchar(50) NOT NULL, " +
						"D1_DATA varchar(10) NOT NULL, " +
						"D_E_L_E_T varchar(1) DEFAULT NULL" +
					") ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8");

			/* ÍNDICES */
			ConnectionManager.ExecuteQuery("ALTER TABLE htb1 " +
					"ADD KEY B1_MATERIA (B1_MATERIA)");
			ConnectionManager.ExecuteQuery("ALTER TABLE htc2 " +
					"ADD KEY C2_CURSO (C2_CURSO)");
			ConnectionManager.ExecuteQuery("ALTER TABLE htc3 " +
					"ADD KEY C3_DISCIPL (C3_DISCIPL)");

			/* CHAVES */
			ConnectionManager.ExecuteQuery("ALTER TABLE htb1 " +
					"ADD CONSTRAINT FK_C3_COD FOREIGN KEY (B1_MATERIA) " +
						"REFERENCES htc3 (C3_COD) ON UPDATE CASCADE");
			ConnectionManager.ExecuteQuery("ALTER TABLE htc2 " +
					"ADD CONSTRAINT FK_C1_COD FOREIGN KEY (C2_CURSO) " +
						"REFERENCES htc1 (C1_COD) ON UPDATE CASCADE");
			ConnectionManager.ExecuteQuery("ALTER TABLE htc3 " +
					"ADD CONSTRAINT FK_C2_COD FOREIGN KEY (C3_DISCIPL) " +
						"REFERENCES htc2 (C2_COD) ON UPDATE CASCADE");

			Mensagem.procedimentoFinalizado();
		}

		private void fazLogin()
		{
			if (txtLogin.Text.Contains("'"))
			{
				txtLogin.Text = txtLogin.Text.Replace("'", "''");
			}

			string query = $"SELECT * FROM hta1 WHERE A1_LOGIN = '{txtLogin.Text}' AND A1_PWD = " +
						   $"'{MD5.gerarHash(txtPassword.Text)}'";
			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(query))
			{
				if (dataReader.HasRows)
				{
					dataReader.Read();
					User.Instance.RecordID = Convert.ToInt32(dataReader["A1_COD"].ToString());
					User.Instance.Username = dataReader["A1_LOGIN"].ToString();
					User.Instance.Password = dataReader["A1_PWD"].ToString();
					User.Instance.MustChangePassword = dataReader["A1_ALTPWD"].ToString().Equals("*");

					if (User.Instance.MustChangePassword)
					{
						var changePassword = new ForcaTrocaSenha();
						if (changePassword.ShowDialog() == DialogResult.OK)
						{
							DialogResult = DialogResult.OK;
						}
					}
					else
					{
						DialogResult = DialogResult.OK;
					}
				}
				else
				{
					Mensagem.loginInvalido();
					txtLogin.Focus();
					txtLogin.SelectAll();
				}
			}
		}
	}
}

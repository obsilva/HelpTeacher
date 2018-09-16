using System;
using System.Windows.Forms;

using HelpTeacher.Classes;
using HelpTeacher.Domain.Entities;
using HelpTeacher.Repository;

namespace HelpTeacher.Forms
{
	public partial class Login : Form
	{
		private ConnectionManager banco = new ConnectionManager();
		private MySql.Data.MySqlClient.MySqlDataReader respostaBanco;

		public Login() => InitializeComponent();

		private void Login_Load(object sender, EventArgs e)
		{
			if (!testaBanco())
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

		private bool testaBanco()
		{
			if (banco.conexaoOK())
			{
				if (tabelasOK())
				{
					return true;
				}
			}
			return false;
		}

		/* tabelasOK
		 * 
		 * Testa se as tebelas necessárias existem. Se alguma tabela não existir,
		 * chama a função que tenta criar todas as tebelas
		 */
		public bool tabelasOK()
		{
			if (banco.executeComando("SELECT * " +
						"FROM INFORMATION_SCHEMA.TABLES " +
						"WHERE table_type = 'BASE TABLE' " +
							"AND table_schema='helpteacher' " +
						"ORDER BY table_name ASC", ref respostaBanco))
			{
				if (respostaBanco.HasRows)
				{
					respostaBanco.Read();
					if (respostaBanco["TABLE_NAME"].ToString().Equals("hta1"))
					{
						respostaBanco.Read();
						if (respostaBanco["TABLE_NAME"].ToString().Equals("htb1"))
						{
							respostaBanco.Read();
							if (respostaBanco["TABLE_NAME"].ToString().Equals("htc1"))
							{
								respostaBanco.Read();
								if (respostaBanco["TABLE_NAME"].ToString().Equals("htc2"))
								{
									respostaBanco.Read();
									if (respostaBanco["TABLE_NAME"].ToString().Equals("htc3"))
									{
										respostaBanco.Read();
										if (respostaBanco["TABLE_NAME"].ToString().Equals("htd1"))
										{
											banco.fechaConexao();
											respostaBanco.Close();
											return true;
										}
									}
								}
							}
						}
					}
				}
				banco.fechaConexao();
				respostaBanco.Close();
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
			banco.executeComando("CREATE TABLE IF NOT EXISTS hta1 (" +
						"A1_COD int(10) UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT, " +
						"A1_LOGIN varchar(50) NOT NULL, " +
						"A1_PWD varchar(32) NOT NULL, " +
						"A1_ALTPWD varchar(1) DEFAULT NULL, " +
						"A1_STOPBD varchar(1) DEFAULT NULL" +
					") ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8");
			banco.executeComando("INSERT INTO hta1 VALUES" +
					"(1, 'admin', '21232F297A57A5A743894A0E4A801FC3', NULL, NULL), " +
					"(2, 'user', 'D41D8CD98F00B204E9800998ECF8427E', NULL, NULL)");

			/* QUESTÕES */
			banco.executeComando("CREATE TABLE IF NOT EXISTS htb1 (" +
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
			banco.executeComando("CREATE TABLE IF NOT EXISTS htc1 (" +
						"C1_COD int(10) UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT, " +
						"C1_NOME varchar(40) NOT NULL, " +
						"D_E_L_E_T varchar(1) DEFAULT NULL" +
					") ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8");

			/* DISCIPLINAS */
			banco.executeComando("CREATE TABLE IF NOT EXISTS htc2 (" +
						"C2_COD int(10) UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT, " +
						"C2_NOME varchar(80) NOT NULL, " +
						"C2_CURSO int(11) UNSIGNED NOT NULL, " +
						"D_E_L_E_T varchar(1) DEFAULT NULL" +
					") ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8");

			/* MATÉRIAS */
			banco.executeComando("CREATE TABLE IF NOT EXISTS htc3 (" +
						"C3_COD int(10) UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT, " +
						"C3_NOME varchar(120) NOT NULL, " +
						"C3_DISCIPL int(10) UNSIGNED NOT NULL, " +
						"D_E_L_E_T varchar(1) DEFAULT NULL" +
					") ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8");

			/* AVALIAÇÕES */
			banco.executeComando("CREATE TABLE IF NOT EXISTS htd1 (" +
						"D1_COD int(10) UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT, " +
						"D1_TIPO varchar(1) NOT NULL, " +
						"D1_INEDITA varchar(1) DEFAULT NULL, " +
						"D1_QUESTAO varchar(100) NOT NULL, " +
						"D1_MATERIA varchar(50) NOT NULL, " +
						"D1_DATA varchar(10) NOT NULL, " +
						"D_E_L_E_T varchar(1) DEFAULT NULL" +
					") ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8");

			/* ÍNDICES */
			banco.executeComando("ALTER TABLE htb1 " +
					"ADD KEY B1_MATERIA (B1_MATERIA)");
			banco.executeComando("ALTER TABLE htc2 " +
					"ADD KEY C2_CURSO (C2_CURSO)");
			banco.executeComando("ALTER TABLE htc3 " +
					"ADD KEY C3_DISCIPL (C3_DISCIPL)");

			/* CHAVES */
			banco.executeComando("ALTER TABLE htb1 " +
					"ADD CONSTRAINT FK_C3_COD FOREIGN KEY (B1_MATERIA) " +
						"REFERENCES htc3 (C3_COD) ON UPDATE CASCADE");
			banco.executeComando("ALTER TABLE htc2 " +
					"ADD CONSTRAINT FK_C1_COD FOREIGN KEY (C2_CURSO) " +
						"REFERENCES htc1 (C1_COD) ON UPDATE CASCADE");
			banco.executeComando("ALTER TABLE htc3 " +
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

			if (banco.executeComando("SELECT * " +
					"FROM hta1 " +
					"WHERE A1_LOGIN = '" + txtLogin.Text + "' AND " +
						"A1_PWD = '" + MD5.gerarHash(txtPassword.Text) +
						"'", ref respostaBanco))
			{
				if (respostaBanco.HasRows)
				{
					respostaBanco.Read();
					User.Instance.RecordID = Convert.ToInt32(respostaBanco["A1_COD"].ToString());
					User.Instance.Username = respostaBanco["A1_LOGIN"].ToString();
					User.Instance.Password = respostaBanco["A1_PWD"].ToString();
					User.Instance.MustChangePassword = respostaBanco["A1_ALTPWD"].ToString().Equals("*");

					banco.fechaConexao();
					respostaBanco.Close();
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
				banco.fechaConexao();
				respostaBanco.Close();
			}
		}
	}
}

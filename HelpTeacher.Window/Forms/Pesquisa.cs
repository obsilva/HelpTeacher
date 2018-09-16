using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using HelpTeacher.Classes;
using HelpTeacher.Repository;

namespace HelpTeacher.Forms
{
	public partial class Pesquisa : Form
	{
		private ConnectionManager banco = new ConnectionManager();
		private MySql.Data.MySqlClient.MySqlDataReader respostaBanco;
		private MySql.Data.MySqlClient.MySqlDataAdapter adaptador;
		private DataSet ds;
		private string deletados;

		public Pesquisa(int origem)
		{
			InitializeComponent();

			inicializa(origem);
		}

		/* tabControlPesquisa_SelectedIndexChanged
		 * 
		 * Chama a função para ajustar o tamanho da tela
		 * toda vez que uma page é selecionada
		 */
		private void tabControlPesquisa_SelectedIndexChanged(object sender, EventArgs e) => ajustaTamanho();

		// FUNÇÕES GERAIS  FUNÇÕES GERAIS  FUNÇÕES GERAIS  FUNÇÕES GERAIS  FUNÇÕES GERAIS  FUNÇÕES GERAIS  //
		// FUNÇÕES GERAIS  FUNÇÕES GERAIS  FUNÇÕES GERAIS  FUNÇÕES GERAIS  FUNÇÕES GERAIS  FUNÇÕES GERAIS  //
		/* inicializa
		 * 
		 * Seleciona em qual page irar abrir
		 */
		private void inicializa(int origem)
		{
			if (origem == 1)
			{
				tabControlPesquisa.SelectedTab = pageQuestoes;
				cmbCampoQuestoes.SelectedIndex = 0;
			}
			else if (origem == 2)
			{
				tabControlPesquisa.SelectedTab = pageDisciplina;
				cmbCampoDisciplinas.SelectedIndex = 0;
			}
			else
			{
				tabControlPesquisa.SelectedTab = pageAvaliacoes;
			}
			ajustaTamanho();
		}

		/* ajustaTamanho
		 * 
		 * Ajusta o tamanho do form de acordo com o a page 
		 * selecionada. Evita sobra de espaço e fica visualmente
		 * mais agradável
		 */
		private void ajustaTamanho()
		{
			Dock = DockStyle.None;

			if (tabControlPesquisa.SelectedTab == pageQuestoes ||
					tabControlPesquisa.SelectedTab == pagePesquisaGeral)
			{
				Size = new Size(1090, 635);

				if (tabControlPesquisa.SelectedTab == pagePesquisaGeral)
				{
					Dock = DockStyle.Fill;
				}
			}
			else if (tabControlPesquisa.SelectedTab == pageAvaliacoes)
			{
				Size = new Size(900, 635);
			}
			else
			{
				Size = new Size(500, 355);
			}
		}

		private void atualizaCursos(ComboBox combo)
		{
			string[] codigo = combo.Text.Split(new char[] { '(', ')' },
						StringSplitOptions.RemoveEmptyEntries);

			if (banco.executeComando("SELECT C1_COD, C1_NOME " +
						"FROM htc1 " +
						"WHERE C1_COD <> " + codigo[0] + " AND " +
							"D_E_L_E_T IS NULL " +
						"ORDER BY C1_COD", ref respostaBanco))
			{
				if (respostaBanco.HasRows)
				{
					while (respostaBanco.Read())
					{
						combo.Items.Add("(" + respostaBanco.GetString(0) + ") " +
									respostaBanco.GetString(1));
					}
				}
				respostaBanco.Close();
				banco.fechaConexao();
			}
		}

		private void atualizaDisciplinas(ComboBox combo)
		{
			string[] codigo = combo.Text.Split(new char[] { '(', ')' },
						StringSplitOptions.RemoveEmptyEntries);

			if (banco.executeComando("SELECT C2_COD, C2_NOME " +
						"FROM htc2 " +
						"WHERE C2_COD <> " + codigo[0] + " AND " +
							"D_E_L_E_T IS NULL " +
						"ORDER BY C2_COD", ref respostaBanco))
			{
				if (respostaBanco.HasRows)
				{
					while (respostaBanco.Read())
					{
						combo.Items.Add("(" + respostaBanco.GetString(0) + ") " +
									respostaBanco.GetString(1));
					}
				}
				respostaBanco.Close();
				banco.fechaConexao();
			}
		}

		/* setaCursorClick
		 * 
		 * Seta o cursor na posição clicada no textBox
		 */
		private void setaCursorClick(object sender, EventArgs e)
		{
			var txt = sender as TextBox;
			var evento = e as MouseEventArgs;
			txt.Select(txt.GetCharIndexFromPosition(evento.Location), 0);
		}





		// PAGE QUESTÕES  PAGE QUESTÕES  PAGE QUESTÕES  PAGE QUESTÕES  PAGE QUESTÕES  PAGE QUESTÕES  PAGE QUESTÕES  //
		// PAGE QUESTÕES  PAGE QUESTÕES  PAGE QUESTÕES  PAGE QUESTÕES  PAGE QUESTÕES  PAGE QUESTÕES  PAGE QUESTÕES  //
		private void pageQuestoes_Enter(object sender, EventArgs e) => cmbCampoQuestoes.SelectedIndex = 0;

		private void pageQuestoes_Leave(object sender, EventArgs e)
		{
			ativaDesativaEdicaoQuestao(false);
			txtPesquisaQuestoes.Clear();
		}

		private void cmbCampoQuestoes_SelectedIndexChanged(object sender, EventArgs e)
		{
			chamaPesquisaQuestoes();
			alteraModoPesquisaQuestoes();
		}

		private void txtPesquisaQuestoes_TextChanged(object sender, EventArgs e)
		{
			chamaPesquisaQuestoes();
			if (txtCodigoQuestao.Text.Equals(""))
			{
				btnEditarQuestao.Enabled = false;
			}
			else
			{
				btnEditarQuestao.Enabled = true;
			}
		}

		private void radTrueQuestoes_CheckedChanged(object sender, EventArgs e) => chamaPesquisaQuestoes();

		private void radQuestoesDeletadas_CheckedChanged(object sender, EventArgs e) => chamaPesquisaQuestoes();

		private void btnEditarQuestao_Click(object sender, EventArgs e) => ativaDesativaEdicaoQuestao(true);

		private void btnArquivo1_Click(object sender, EventArgs e)
		{
			if (opnArquivo.ShowDialog() == DialogResult.OK)
			{
				txtArquivo1.Text = opnArquivo.FileName;
			}
		}

		private void btnArquivo2_Click(object sender, EventArgs e)
		{
			if (opnArquivo.ShowDialog() == DialogResult.OK)
			{
				txtArquivo2.Text = opnArquivo.FileName;
			}
		}

		private void btnBackspace1_Click(object sender, EventArgs e) => txtArquivo1.Clear();

		private void btnBackspace2_Click(object sender, EventArgs e) => txtArquivo2.Clear();

		private void btnSalvarQuestoes_Click(object sender, EventArgs e)
		{
			if (salvaQuestaoModificada())
			{
				ativaDesativaEdicaoQuestao(false);
				chamaPesquisaQuestoes();
				Mensagem.dadosAlterados();
			}
			else
			{
				Mensagem.erroAlteracao();
			}
		}

		private void btnCancelarQuestoes_Click(object sender, EventArgs e)
		{
			ativaDesativaEdicaoQuestao(false);
			chamaPesquisaQuestoes();
		}

		private void atualizaCamposQuestoes()
		{
			if (respostaBanco.HasRows)
			{
				respostaBanco.Read();

				btnEditarQuestao.Enabled = true;
				cmbCursosQuestoes.Items.Clear();
				chkDisciplinasQuestoes.Items.Clear();
				chkMateriasQuestoes.Items.Clear();

				txtCodigoQuestao.Text = respostaBanco["B1_COD"].ToString();
				txtQuestao.Text = respostaBanco["B1_QUEST"].ToString();
				cmbCursosQuestoes.Items.Add("(" + respostaBanco["C1_COD"].ToString() +
					") " + respostaBanco["C1_NOME"].ToString());
				chkDisciplinasQuestoes.Items.Add("(" + respostaBanco["C2_COD"].ToString() +
					") " + respostaBanco["C2_NOME"].ToString());
				chkDisciplinasQuestoes.SetItemChecked(0, true);
				chkMateriasQuestoes.Items.Add("(" + respostaBanco["B1_MATERIA"].ToString() +
					") " + respostaBanco["C3_NOME"].ToString());
				chkMateriasQuestoes.SetItemChecked(0, true);

				if (respostaBanco["B1_OBJETIV"].ToString().Equals("*"))
				{
					radObjetiva.Checked = true;
				}
				else
				{
					radDissertativa.Checked = true;
				}

				if (respostaBanco["B1_USADA"].ToString().Equals("*"))
				{
					chkQuestaoUsada.Checked = true;
				}
				else
				{
					chkQuestaoUsada.Checked = false;
				}

				if (respostaBanco["B1_PADRAO"].ToString().Equals("*"))
				{
					chkQuestaoPadrao.Checked = true;
				}
				else
				{
					chkQuestaoPadrao.Checked = false;
				}

				if (respostaBanco["D_E_L_E_T"].ToString().Equals("*"))
				{
					chkQuestaoDeletada.Checked = true;
				}
				else
				{
					chkQuestaoDeletada.Checked = false;
				}

				if (!respostaBanco["B1_ARQUIVO"].ToString().Equals(""))
				{
					if (respostaBanco["B1_ARQUIVO"].ToString().Contains(','))
					{
						string[] nomes = respostaBanco["B1_ARQUIVO"].ToString().Split(new char[] { ',', ' ' },
									StringSplitOptions.RemoveEmptyEntries);

						string[] arquivos = Directory.GetFiles(@"..\_files", nomes[0] + ".*");
						string caminho = Path.GetFullPath(arquivos[0]);
						txtArquivo1.Text = caminho;

						arquivos = Directory.GetFiles(@"..\_files", nomes[1] + ".*");
						caminho = Path.GetFullPath(arquivos[0]);
						txtArquivo2.Text = caminho;
					}
					else
					{
						string[] arquivo = Directory.GetFiles(@"..\_files",
							respostaBanco["B1_ARQUIVO"].ToString() + ".*");
						string caminho = Path.GetFullPath(arquivo[0]);
						txtArquivo1.Text = caminho;
						txtArquivo2.Clear();
					}
				}
				else
				{
					txtArquivo1.Clear();
					txtArquivo2.Clear();
				}

				respostaBanco.Close();
				banco.fechaConexao();
				cmbCursosQuestoes.SelectedIndex = 0;
			}
			else
			{
				btnEditarQuestao.Enabled = false;
				txtCodigoQuestao.Clear();
				cmbCursosQuestoes.Items.Clear();
				chkDisciplinasQuestoes.Items.Clear();
				chkMateriasQuestoes.Items.Clear();
				txtQuestao.Clear();
				txtArquivo1.Clear();
				txtArquivo2.Clear();
				chkQuestaoUsada.Checked = false;
				chkQuestaoPadrao.Checked = false;
				chkQuestaoDeletada.Checked = false;
				respostaBanco.Close();
				banco.fechaConexao();
			}
		}

		private void chamaPesquisaQuestoes()
		{
			if (radQuestoesDeletadas.Checked)
			{
				deletados = "IS NOT NULL";
			}
			else
			{
				deletados = "IS NULL";
			}

			switch (cmbCampoQuestoes.Text)
			{
				case "Código":
					pesquisaQuestoesEdit("B1_COD", txtPesquisaQuestoes.Text,
								deletados);
					break;
				case "Pergunta":
					pesquisaQuestoesEdit("B1_QUEST", txtPesquisaQuestoes.Text,
								deletados);
					break;
				case "Objetivas?":
					pesquisaQuestoesEdit("B1_OBJETIV", radFalseQuestoes.Checked,
								deletados);
					break;
				case "Alternativa A":
					pesquisaQuestoesEdit("B1_QUEST", "a) " +
								txtPesquisaQuestoes.Text, deletados);
					break;
				case "Alternativa B":
					pesquisaQuestoesEdit("B1_QUEST", "b): " +
								txtPesquisaQuestoes.Text, deletados);
					break;
				case "Alternativa C":
					pesquisaQuestoesEdit("B1_QUEST", "c): " +
								txtPesquisaQuestoes.Text, deletados);
					break;
				case "Alternativa D":
					pesquisaQuestoesEdit("B1_QUEST", "d): " +
								txtPesquisaQuestoes.Text, deletados);
					break;
				case "Alternativa E":
					pesquisaQuestoesEdit("B1_QUEST", "e): " +
								txtPesquisaQuestoes.Text, deletados);
					break;
				case "Arquivo?":
					pesquisaQuestoesEdit("B1_ARQUIVO", radFalseQuestoes.Checked,
								deletados);
					break;
				case "Usada?":
					pesquisaQuestoesEdit("B1_USADA", radFalseQuestoes.Checked,
								deletados);
					break;
				case "Matéria":
					pesquisaQuestoesEdit("B1_MATERIA", txtPesquisaQuestoes.Text,
								deletados);
					break;
				case "Padrão?":
					pesquisaQuestoesEdit("B1_PADRAO", radFalseQuestoes.Checked,
								deletados);
					break;
				case "Deletadas?":
					break;
				default:
					Mensagem.erroInesperado();
					break;
			}
		}

		private void alteraModoPesquisaQuestoes()
		{
			if (cmbCampoQuestoes.SelectedIndex == 2 ||
						cmbCampoQuestoes.SelectedIndex == 8 ||
						cmbCampoQuestoes.SelectedIndex == 9 ||
						cmbCampoQuestoes.SelectedIndex == 11)
			{
				txtPesquisaQuestoes.Clear();
				pnlBooleanQuestoes.Show();
			}
			else
			{
				pnlBooleanQuestoes.Hide();
			}
		}

		private void ativaDesativaEdicaoQuestao(bool ativa)
		{
			cmbCampoQuestoes.Enabled = !ativa;
			txtPesquisaQuestoes.Enabled = !ativa;
			pnlBooleanQuestoes.Enabled = !ativa;
			btnEditarQuestao.Enabled = !ativa;
			pnlQuestao.Enabled = ativa;
		}

		private bool salvaQuestaoModificada()
		{
			/* Dissertativa ou Objetiva */
			if (radDissertativa.Checked)
			{
				if (!banco.executeComando("UPDATE htb1 SET B1_OBJETIV = NULL " +
							"WHERE B1_COD = " + txtCodigoQuestao.Text))
				{
					return false;
				}
			}
			else
			{
				if (!banco.executeComando("UPDATE htb1 SET B1_OBJETIV = '*' " +
							"WHERE B1_COD = " + txtCodigoQuestao.Text))
				{
					return false;
				}
			}

			/* Questão */
			if (!banco.executeComando("UPDATE htb1 SET B1_QUEST = '" +
						txtQuestao.Text + "' WHERE B1_COD = " +
						txtCodigoQuestao.Text))
			{
				return false;
			}

			/* Arquivos */
			if (!(txtArquivo1.Text.Equals("")) || !(txtArquivo2.Text.Equals("")))
			{
				/* Apenas 1 arquivo */
				if ((txtArquivo1.Text.Equals("")) || (txtArquivo2.Text.Equals("")))
				{
					if (banco.executeComando("UPDATE htb1 SET B1_ARQUIVO = '" +
									txtCodigoQuestao.Text + "_1' " +
								"WHERE B1_COD = " + txtCodigoQuestao.Text))
					{
						apagaArquivos();
						copiaArquivo();
					}
				}
				/* Dois arquivos */
				else
				{
					if (banco.executeComando("UPDATE htb1 SET B1_ARQUIVO = '" +
									txtCodigoQuestao.Text + "_1, " +
									txtCodigoQuestao.Text + "_2' " +
								"WHERE B1_COD = " + txtCodigoQuestao.Text))
					{
						copiaArquivo();
					}
				}
			}
			else
			{
				if (banco.executeComando("UPDATE htb1 SET B1_ARQUIVO = NULL " +
							"WHERE B1_COD = " + txtCodigoQuestao.Text))
				{
					apagaArquivos();
				}
			}

			/* Usada */
			if (chkQuestaoUsada.Checked)
			{
				if (!banco.executeComando("UPDATE htb1 SET B1_USADA = '*' " +
							"WHERE B1_COD = " + txtCodigoQuestao.Text))
				{
					return false;
				}
			}
			else
			{
				if (!banco.executeComando("UPDATE htb1 SET B1_USADA = NULL " +
							"WHERE B1_COD = " + txtCodigoQuestao.Text))
				{
					return false;
				}
			}

			/* Padrão */
			if (chkQuestaoPadrao.Checked)
			{
				if (!banco.executeComando("UPDATE htb1 SET B1_PADRAO = '*' " +
							"WHERE B1_COD = " + txtCodigoQuestao.Text))
				{
					return false;
				}
			}
			else
			{
				if (!banco.executeComando("UPDATE htb1 SET B1_PADRAO = NULL " +
							"WHERE B1_COD = " + txtCodigoQuestao.Text))
				{
					return false;
				}
			}

			/* Deletada */
			if (chkQuestaoDeletada.Checked)
			{
				if (!banco.executeComando("UPDATE htb1 SET D_E_L_E_T = '*' " +
							"WHERE B1_COD = " + txtCodigoQuestao.Text))
				{
					return false;
				}
			}
			else
			{
				if (!banco.executeComando("UPDATE htb1 SET D_E_L_E_T = NULL " +
							"WHERE B1_COD = " + txtCodigoQuestao.Text))
				{
					return false;
				}
			}
			return true;
		}

		private void copiaArquivo()
		{
			string primeiroArquivo = Path.Combine(@"..\_files", (txtCodigoQuestao.Text + "_1"));

			if (txtArquivo1.Text.Equals(""))
			{
				string extensao = Path.GetExtension(txtArquivo2.Text);
				primeiroArquivo = String.Concat(primeiroArquivo, extensao);

				if (!txtArquivo2.Text.Contains(@"_files\" + txtCodigoQuestao.Text))
				{
					File.Copy(txtArquivo2.Text, primeiroArquivo, true);
				}
				else
				{
					File.Move(txtArquivo2.Text, primeiroArquivo);
				}
			}
			else
			{
				string extensao1 = Path.GetExtension(txtArquivo1.Text);
				primeiroArquivo = String.Concat(primeiroArquivo, extensao1);

				if (!txtArquivo1.Text.Contains(@"_files\" + txtCodigoQuestao.Text))
				{
					File.Copy(txtArquivo1.Text, primeiroArquivo, true);
				}
				else
				{
					File.Move(txtArquivo1.Text, primeiroArquivo);
				}

				if (!txtArquivo2.Text.Equals(""))
				{
					string segundoArquivo = Path.Combine(@"..\_files", (txtCodigoQuestao.Text + "_2"));
					string extensao2 = Path.GetExtension(txtArquivo2.Text);
					segundoArquivo = String.Concat(segundoArquivo, extensao2);

					if (!txtArquivo2.Text.Contains(@"_files\" + txtCodigoQuestao.Text))
					{
						File.Copy(txtArquivo2.Text, segundoArquivo, true);
					}
					else
					{
						File.Move(txtArquivo2.Text, segundoArquivo);
					}
				}
			}
		}

		/* apagaArquivos
		 * 
		 * Verifica se existe algum arquivo relacionada a
		 * questão e se tiver, apaga o arquivo
		 */
		private void apagaArquivos()
		{
			string[] arquivos = Directory.GetFiles(@"..\_files",
						txtCodigoQuestao.Text + "_*");
			foreach (string arquivo in arquivos)
			{
				string caminho = Path.GetFullPath(arquivo);

				if (!txtArquivo1.Text.Contains(caminho) &&
							!txtArquivo2.Text.Contains(caminho))
				{
					try
					{
						File.Delete(arquivo);
					}
					catch
					{
						break;
					}
				}
			}
		}

		// PAGE CURSOS  PAGE CURSOS  PAGE CURSOS  PAGE CURSOS  PAGE CURSOS  PAGE CURSOS  PAGE CURSOS  PAGE CURSOS  //
		// PAGE CURSOS  PAGE CURSOS  PAGE CURSOS  PAGE CURSOS  PAGE CURSOS  PAGE CURSOS  PAGE CURSOS  PAGE CURSOS  //
		private void pageCursos_Enter(object sender, EventArgs e) => cmbCampoCursos.SelectedIndex = 0;

		private void pageCursos_Leave(object sender, EventArgs e)
		{
			ativaDesativaEdicaoCurso(false);
			txtPesquisaCursos.Clear();
		}

		private void cmbCampoCursos_SelectedIndexChanged(object sender, EventArgs e)
		{
			alteraModoPesquisaCursos();
			chamaPesquisaCursos();
		}

		private void radCursosDeletados_CheckedChanged(object sender, EventArgs e) => txtPesquisaCursos.Clear();

		private void txtPesquisaCursos_TextChanged(object sender, EventArgs e) => chamaPesquisaCursos();

		private void btnEditarCursos_Click(object sender, EventArgs e) => ativaDesativaEdicaoCurso(true);

		private void btnSalvarCurso_Click(object sender, EventArgs e)
		{
			if (salvaCursoModificado())
			{
				ativaDesativaEdicaoCurso(false);
				chamaPesquisaCursos();
				Mensagem.dadosAlterados();
			}
			else
			{
				Mensagem.erroAlteracao();
			}
		}

		private void btnCancelarCurso_Click(object sender, EventArgs e)
		{
			ativaDesativaEdicaoCurso(false);
			chamaPesquisaCursos();
		}

		private void atualizaCamposCursos()
		{
			if (respostaBanco.HasRows)
			{
				respostaBanco.Read();

				btnEditarCursos.Enabled = true;

				txtCodigoCurso.Text = respostaBanco["C1_COD"].ToString();
				txtNomeCurso.Text = respostaBanco["C1_NOME"].ToString();

				if (respostaBanco["D_E_L_E_T"].ToString().Equals("*"))
				{
					chkCursoDeletado.Checked = true;
				}
				else
				{
					chkCursoDeletado.Checked = false;
				}
			}
			else
			{
				btnEditarCursos.Enabled = false;
				txtCodigoCurso.Clear();
				chkCursoDeletado.Checked = false;
				txtNomeCurso.Clear();
			}

			respostaBanco.Close();
			banco.fechaConexao();
		}

		private void chamaPesquisaCursos()
		{
			if (radCursosDeletados.Checked)
			{
				deletados = "IS NOT NULL";
			}
			else
			{
				deletados = "IS NULL";
			}

			switch (cmbCampoCursos.Text)
			{
				case "Código":
					pesquisaCursosEdit("C1_COD", txtPesquisaCursos.Text,
								deletados);
					break;
				case "Nome":
					pesquisaCursosEdit("C1_NOME", txtPesquisaCursos.Text,
								deletados);
					break;
				case "Deletados?":
					break;
				default:
					Mensagem.erroInesperado();
					break;
			}
		}

		private void alteraModoPesquisaCursos()
		{
			if (cmbCampoCursos.SelectedIndex == 2)
			{
				txtPesquisaCursos.Clear();
				pnlDeletadosCursos.Show();
			}
			else
			{
				pnlDeletadosCursos.Hide();
			}
		}

		private void ativaDesativaEdicaoCurso(bool ativa)
		{
			cmbCampoCursos.Enabled = !ativa;
			txtPesquisaCursos.Enabled = !ativa;
			pnlDeletadosCursos.Enabled = !ativa;
			btnEditarCursos.Enabled = !ativa;
			pnlCursos.Enabled = ativa;
		}

		private bool salvaCursoModificado()
		{
			if (!banco.executeComando("UPDATE htc1 SET C1_NOME = '" + txtNomeCurso.Text +
						"' WHERE C1_COD = " + txtCodigoCurso.Text))
			{
				return false;
			}

			//Deletada
			if (chkCursoDeletado.Checked)
			{
				if (!banco.executeComando("UPDATE htc1 SET D_E_L_E_T = '*' " +
							"WHERE C1_COD = " + txtCodigoCurso.Text))
				{
					return false;
				}
			}
			else
			{
				if (!banco.executeComando("UPDATE htc1 SET D_E_L_E_T = NULL " +
							"WHERE C1_COD = " + txtCodigoCurso.Text))
				{
					return false;
				}
			}
			return true;
		}

		// PAGE DISCIPLINAS  PAGE DISCIPLINAS  PAGE DISCIPLINAS  PAGE DISCIPLINAS  PAGE DISCIPLINAS  PAGE DISCIPLINAS  //
		// PAGE DISCIPLINAS  PAGE DISCIPLINAS  PAGE DISCIPLINAS  PAGE DISCIPLINAS  PAGE DISCIPLINAS  PAGE DISCIPLINAS  //
		private void pageDisciplina_Enter(object sender, EventArgs e) => cmbCampoDisciplinas.SelectedIndex = 0;

		private void pageDisciplina_Leave(object sender, EventArgs e)
		{
			ativaDesativaEdicaoDisc(false);
			txtPesquisaDisciplina.Clear();
		}

		private void cmbCampoDisciplinas_SelectedIndexChanged(object sender, EventArgs e)
		{
			alteraModoPesquisaDisciplinas();
			chamaPesquisaDisciplinas();
		}

		private void txtPesquisaDisciplina_TextChanged(object sender, EventArgs e) => chamaPesquisaDisciplinas();

		private void radDisciplinasDeletadas_CheckedChanged(object sender, EventArgs e) => txtPesquisaDisciplina.Clear();

		private void btnEditarDisciplina_Click(object sender, EventArgs e)
		{
			ativaDesativaEdicaoDisc(true);
			atualizaCursos(cmbCursosDisciplina);
		}

		private void bntSalvarDisc_Click(object sender, EventArgs e)
		{
			if (salvaDiscModificada())
			{
				ativaDesativaEdicaoDisc(false);
				chamaPesquisaDisciplinas();
				Mensagem.dadosAlterados();
			}
			else
			{
				Mensagem.erroAlteracao();
			}
		}

		private void btnCancelarDisc_Click(object sender, EventArgs e)
		{
			ativaDesativaEdicaoDisc(false);
			chamaPesquisaDisciplinas();
		}

		private void atualizaCamposDisciplinas()
		{
			if (respostaBanco.HasRows)
			{
				respostaBanco.Read();

				btnEditarDisciplina.Enabled = true;
				cmbCursosDisciplina.Items.Clear();

				txtCodigoDisciplina.Text = respostaBanco["C2_COD"].ToString();
				textNomeDisciplina.Text = respostaBanco["C2_NOME"].ToString();
				cmbCursosDisciplina.Items.Add("(" + respostaBanco["C2_CURSO"].ToString() +
					") " + respostaBanco["C1_NOME"].ToString());

				if (respostaBanco["D_E_L_E_T"].ToString().Equals("*"))
				{
					chkDisciplinaDeletada.Checked = true;
				}
				else
				{
					chkDisciplinaDeletada.Checked = false;
				}

				respostaBanco.Close();
				banco.fechaConexao();
				cmbCursosDisciplina.SelectedIndex = 0;
			}
			else
			{
				btnEditarDisciplina.Enabled = false;
				txtCodigoDisciplina.Clear();
				chkDisciplinaDeletada.Checked = false;
				textNomeDisciplina.Clear();
				cmbCursosDisciplina.Items.Clear();
				respostaBanco.Close();
				banco.fechaConexao();
			}
		}

		private void chamaPesquisaDisciplinas()
		{
			if (radDisciplinasDeletadas.Checked)
			{
				deletados = "IS NOT NULL";
			}
			else
			{
				deletados = "IS NULL";
			}

			switch (cmbCampoDisciplinas.Text)
			{
				case "Código":
					pesquisaDisciplinasEdit("C2_COD", txtPesquisaDisciplina.Text,
								deletados);
					break;
				case "Nome":
					pesquisaDisciplinasEdit("C2_NOME", txtPesquisaDisciplina.Text,
								deletados);
					break;
				case "Curso":
					pesquisaDisciplinasEdit("C2_CURSO", txtPesquisaDisciplina.Text,
								deletados);
					break;
				case "Deletadas?":
					break;
				default:
					Mensagem.erroInesperado();
					break;
			}
		}

		private void alteraModoPesquisaDisciplinas()
		{
			if (cmbCampoDisciplinas.SelectedIndex == 3)
			{
				txtPesquisaDisciplina.Clear();
				pnlDeletadosDisciplinas.Show();
			}
			else
			{
				pnlDeletadosDisciplinas.Hide();
			}
		}

		private void ativaDesativaEdicaoDisc(bool ativa)
		{
			cmbCampoDisciplinas.Enabled = !ativa;
			txtPesquisaDisciplina.Enabled = !ativa;
			pnlDeletadosDisciplinas.Enabled = !ativa;
			btnEditarDisciplina.Enabled = !ativa;
			pnlDisciplina.Enabled = ativa;
		}

		private bool salvaDiscModificada()
		{
			string[] curso = cmbCursosDisciplina.Text.Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);

			if (!banco.executeComando("UPDATE htc2 SET C2_NOME = '" +
							textNomeDisciplina.Text + "', C2_CURSO =  " +
							curso[0] +
						" WHERE C2_COD = " + txtCodigoDisciplina.Text))
			{
				return false;
			}

			/* Deletado */
			if (chkDisciplinaDeletada.Checked)
			{
				if (!banco.executeComando("UPDATE htc2 SET D_E_L_E_T = '*' " +
						"WHERE C2_COD = " + txtCodigoDisciplina.Text))
				{
					return false;
				}
			}
			else
			{
				if (!banco.executeComando("UPDATE htc2 SET D_E_L_E_T = NULL " +
						"WHERE C2_COD = " + txtCodigoDisciplina.Text))
				{
					return false;
				}
			}
			return true;
		}

		// PAGE MATÉRIAS  PAGE MATÉRIAS  PAGE MATÉRIAS  PAGE MATÉRIAS  PAGE MATÉRIAS  PAGE MATÉRIAS  PAGE MATÉRIAS  //
		// PAGE MATÉRIAS  PAGE MATÉRIAS  PAGE MATÉRIAS  PAGE MATÉRIAS  PAGE MATÉRIAS  PAGE MATÉRIAS  PAGE MATÉRIAS  //
		private void pageMateria_Enter(object sender, EventArgs e) => cmbCampoMateria.SelectedIndex = 0;

		private void pageMateria_Leave(object sender, EventArgs e)
		{
			ativaDesativaEdicaoMateria(false);
			txtPesquisaMateria.Clear();
		}

		private void cmbCampoMateria_SelectedIndexChanged(object sender, EventArgs e)
		{
			alteraModoPesquisaMaterias();
			chamaPesquisaMaterias();
		}

		private void txtPesquisaMateria_TextChanged(object sender, EventArgs e) => chamaPesquisaMaterias();

		private void radMateriasDeletadas_CheckedChanged(object sender, EventArgs e) => txtPesquisaMateria.Clear();

		private void btnEditarMateria_Click(object sender, EventArgs e)
		{
			ativaDesativaEdicaoMateria(true);
			atualizaDisciplinas(cmbDisciplinaMateria);
		}

		private void cmbDisciplinaMateria_SelectedIndexChanged(object sender, EventArgs e) => recuperaNomeCurso();

		private void bntSalvarMateria_Click(object sender, EventArgs e)
		{
			if (salvaMateriaModificada())
			{
				ativaDesativaEdicaoMateria(false);
				chamaPesquisaMaterias();
				Mensagem.dadosAlterados();
			}
			else
			{
				Mensagem.erroAlteracao();
			}
		}

		private void btnCancelarMateria_Click(object sender, EventArgs e)
		{
			ativaDesativaEdicaoMateria(false);
			chamaPesquisaMaterias();
		}

		private void atualizaCamposMaterias()
		{
			if (respostaBanco.HasRows)
			{
				respostaBanco.Read();

				btnEditarMateria.Enabled = true;
				cmbDisciplinaMateria.Items.Clear();

				txtCodigoMateria.Text = respostaBanco["C3_COD"].ToString();
				txtNomeMateria.Text = respostaBanco["C3_NOME"].ToString();
				cmbDisciplinaMateria.Items.Add("(" + respostaBanco["C3_DISCIPL"].ToString() +
					") " + respostaBanco["C2_NOME"].ToString());

				if (respostaBanco["D_E_L_E_T"].ToString().Equals("*"))
				{
					chkMateriaDeletada.Checked = true;
				}
				else
				{
					chkMateriaDeletada.Checked = false;
				}

				respostaBanco.Close();
				banco.fechaConexao();
				cmbDisciplinaMateria.SelectedIndex = 0;
			}
			else
			{
				btnEditarMateria.Enabled = false;
				txtCodigoMateria.Clear();
				chkMateriaDeletada.Checked = false;
				txtNomeMateria.Clear();
				cmbDisciplinaMateria.Items.Clear();
				txtCursoMateria.Clear();
				respostaBanco.Close();
				banco.fechaConexao();
			}
		}

		private void chamaPesquisaMaterias()
		{
			if (radMateriasDeletadas.Checked)
			{
				deletados = "IS NOT NULL";
			}
			else
			{
				deletados = "IS NULL";
			}

			switch (cmbCampoMateria.Text)
			{
				case "Código":
					pesquisaMateriasEdit("C3_COD", txtPesquisaMateria.Text,
								deletados);
					break;
				case "Nome":
					pesquisaMateriasEdit("C3_NOME", txtPesquisaMateria.Text,
								deletados);
					break;
				case "Disciplina":
					pesquisaMateriasEdit("C3_DISCIPL", txtPesquisaMateria.Text,
								deletados);
					break;
				case "Deletado?":
					break;
				default:
					Mensagem.erroInesperado();
					break;
			}
		}

		private void alteraModoPesquisaMaterias()
		{
			if (cmbCampoMateria.SelectedIndex == 3)
			{
				txtPesquisaMateria.Clear();
				pnlDeletadosMaterias.Show();
			}
			else
			{
				pnlDeletadosMaterias.Hide();
			}
		}

		private void ativaDesativaEdicaoMateria(bool ativa)
		{
			cmbCampoMateria.Enabled = !ativa;
			txtPesquisaMateria.Enabled = !ativa;
			pnlDeletadosMaterias.Enabled = !ativa;
			btnEditarMateria.Enabled = !ativa;
			pnlMateria.Enabled = ativa;
		}

		private bool salvaMateriaModificada()
		{
			string[] disciplina = cmbDisciplinaMateria.Text.Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);

			if (!banco.executeComando("UPDATE htc3 SET C3_NOME = '" +
							txtNomeMateria.Text + "', C3_DISCIPL =  " +
							disciplina[0] +
						" WHERE C3_COD = " + txtCodigoMateria.Text))
			{
				return false;
			}

			/* Deletada */
			if (chkMateriaDeletada.Checked)
			{
				if (!banco.executeComando("UPDATE htc3 SET D_E_L_E_T = '*' " +
						"WHERE C3_COD = " + txtCodigoMateria.Text))
				{
					return false;
				}
			}
			else
			{
				if (!banco.executeComando("UPDATE htc3 SET D_E_L_E_T = NULL " +
						"WHERE C3_COD = " + txtCodigoMateria.Text))
				{
					return false;
				}
			}

			return true;
		}

		private void recuperaNomeCurso()
		{
			string[] codMateria = cmbDisciplinaMateria.Text.Split(new char[]
					{ '(', ')' }, StringSplitOptions.RemoveEmptyEntries);

			if (banco.executeComando("SELECT C1_NOME " +
						"FROM htc2 " +
							"INNER JOIN htc1 " +
								"ON C2_CURSO = C1_COD " +
						"WHERE C2_COD = " + codMateria[0], ref respostaBanco))
			{
				respostaBanco.Read();
				txtCursoMateria.Text = respostaBanco.GetString(0);

				respostaBanco.Close();
				banco.fechaConexao();
			}
		}

		// PAGE AVALIAÇÕES  PAGE AVALIAÇÕES  PAGE AVALIAÇÕES  PAGE AVALIAÇÕES  PAGE AVALIAÇÕES  PAGE AVALIAÇÕES  //
		// PAGE AVALIAÇÕES  PAGE AVALIAÇÕES  PAGE AVALIAÇÕES  PAGE AVALIAÇÕES  PAGE AVALIAÇÕES  PAGE AVALIAÇÕES  //
		private void pageAvaliacoes_Enter(object sender, EventArgs e)
		{
			atualizaComboCursos();
			chamaPesquisaAvaliacoes();
		}

		private void pageAvaliacoes_Leave(object sender, EventArgs e)
		{
			cmbAvaliacoes.Items.Clear();
			cmbDisciplinasAvaliacoes.Items.Clear();
			cmbMateriasAvaliacoes.Items.Clear();
			txtAvaliacao.Clear();

			cmbAvaliacoes.Text = "Selecionar...";
			cmbAvaliacoes.Text = "Selecionar...";

			radData.Checked = true;
		}

		private void radCurso_CheckedChanged(object sender, EventArgs e)
		{
			cmbDisciplinasAvaliacoes.Items.Clear();
			cmbMateriasAvaliacoes.Items.Clear();

			if (radCurso.Checked)
			{
				dateAvalicao.Enabled = false;

				cmbCursosAvaliacoes.Enabled = true;
			}
			else
			{
				dateAvalicao.Enabled = true;

				cmbCursosAvaliacoes.Enabled = false;
				cmbDisciplinasAvaliacoes.Enabled = false;
				cmbMateriasAvaliacoes.Enabled = false;
			}
		}

		private void cmbCursosAvaliacoes_SelectedIndexChanged(object sender, EventArgs e)
		{
			cmbDisciplinasAvaliacoes.Text = "Opcional";
			cmbDisciplinasAvaliacoes.Enabled = true;
			atualizaComboDisicplina();

			cmbMateriasAvaliacoes.Text = "Opcional";
			cmbMateriasAvaliacoes.Items.Clear();

			chamaPesquisaAvaliacoes();
		}

		private void cmbDisciplinasAvaliacoes_SelectedIndexChanged(object sender, EventArgs e)
		{
			cmbMateriasAvaliacoes.Text = "Opcional";
			cmbMateriasAvaliacoes.Enabled = true;
			atualizaComboMaterias();

			chamaPesquisaAvaliacoes();
		}

		private void cmbMateriasAvaliacoes_SelectedIndexChanged(object sender, EventArgs e) => chamaPesquisaAvaliacoes();

		private void dateAvalicao_ValueChanged(object sender, EventArgs e) => chamaPesquisaAvaliacoes();

		private void cmbAvaliacao_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtAvaliacao.Clear();
			mostraAvaliacao();
		}

		private void chkAvaliacoesDeletadas_CheckedChanged(object sender, EventArgs e) => chamaPesquisaAvaliacoes();

		private void btnLimparRegistro_Click(object sender, EventArgs e)
		{
			if (apagaRegistro())
			{
				Mensagem.dadosAlterados();
				chamaPesquisaAvaliacoes();
			}
			else
			{
				Mensagem.erroAlteracao();
			}
		}

		private void btnLimparHistorico_Click(object sender, EventArgs e)
		{
			if (apagaHistorico())
			{
				Mensagem.dadosAlterados();
				chamaPesquisaAvaliacoes();
			}
			else
			{
				Mensagem.erroAlteracao();
			}
		}

		private void chamaPesquisaAvaliacoes()
		{
			if (chkAvaliacoesDeletadas.Checked)
			{
				deletados = "IS NOT NULL";
			}
			else
			{
				deletados = "IS NULL";
			}

			if (radData.Checked)
			{
				pesquisaHistoricoEdit("D1_DATA", dateAvalicao.Text, deletados);
			}
			else
			{
				if (!cmbMateriasAvaliacoes.Text.Equals("Opcional"))
				{
					string[] codigoMateria = cmbMateriasAvaliacoes.Text.Split
					(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);

					pesquisaHistoricoEdit("D1_MATERIA", codigoMateria[0], deletados);
				}
				else if (!cmbDisciplinasAvaliacoes.Text.Equals("Opcional"))
				{
					string[] codigoDisciplina = cmbDisciplinasAvaliacoes.Text.
							Split(new char[] { '(', ')' }, StringSplitOptions.
							RemoveEmptyEntries);

					pesquisaHistoricoEdit("C2_COD", codigoDisciplina[0], deletados);
				}
				else
				{
					string[] codigoCurso = cmbCursosAvaliacoes.Text.Split(new char[]
							{ '(', ')' }, StringSplitOptions.RemoveEmptyEntries);

					pesquisaHistoricoEdit("C1_COD", codigoCurso[0], deletados);
				}
			}
		}

		private void atualizaComboCursos()
		{
			cmbCursosAvaliacoes.Items.Clear();
			cmbAvaliacoes.Items.Clear();

			if (banco.executeComando("SELECT C1_COD, C1_NOME " +
					"FROM htc1", ref respostaBanco))
			{
				if (respostaBanco.HasRows)
				{
					while (respostaBanco.Read())
					{
						cmbCursosAvaliacoes.Items.Add("(" + respostaBanco.GetString(0) +
							")" + respostaBanco.GetString(1));
					}
				}
				respostaBanco.Close();
				banco.fechaConexao();
			}
		}

		private void atualizaComboDisicplina()
		{
			string[] codigoCurso = cmbCursosAvaliacoes.Text.Split(new char[]
				{ '(', ')' }, StringSplitOptions.RemoveEmptyEntries);

			cmbDisciplinasAvaliacoes.Items.Clear();
			if (banco.executeComando("SELECT C2_COD, C2_NOME " +
					"FROM htc2 " +
					"WHERE C2_CURSO = " + codigoCurso[0], ref respostaBanco))
			{
				if (respostaBanco.HasRows)
				{
					while (respostaBanco.Read())
					{
						cmbDisciplinasAvaliacoes.Items.Add("(" + respostaBanco.
								GetString(0) + ")" + respostaBanco.GetString(1));
					}
				}
				respostaBanco.Close();
				banco.fechaConexao();
			}
		}

		private void atualizaComboMaterias()
		{
			string[] codigoDisciplina = cmbDisciplinasAvaliacoes.
					Text.Split(new char[] { '(', ')' },
					StringSplitOptions.RemoveEmptyEntries);

			cmbMateriasAvaliacoes.Items.Clear();
			if (banco.executeComando("SELECT C3_COD, C3_NOME " +
					"FROM htc3 " +
					"WHERE C3_DISCIPL = " + codigoDisciplina[0], ref respostaBanco))
			{
				if (respostaBanco.HasRows)
				{
					while (respostaBanco.Read())
					{
						cmbMateriasAvaliacoes.Items.Add("(" + respostaBanco.
								GetString(0) + ")" + respostaBanco.GetString(1));
					}
				}
				respostaBanco.Close();
				banco.fechaConexao();
			}
		}

		private void atualizaCamposAvaliacao()
		{
			cmbAvaliacoes.Items.Clear();

			if (respostaBanco.HasRows)
			{
				while (respostaBanco.Read())
				{
					cmbAvaliacoes.Items.Add("(" + respostaBanco.GetString(0) + ") " +
							respostaBanco.GetString(1) + " - " + respostaBanco.GetString(3));
				}
				respostaBanco.Close();
				banco.fechaConexao();
				cmbAvaliacoes.SelectedIndex = 0;
				btnLimparRegistro.Enabled = true;
			}
			else
			{
				txtAvaliacao.Clear();
				cmbAvaliacoes.Text = "Sem histórico";
				btnLimparRegistro.Enabled = false;
				respostaBanco.Close();
				banco.fechaConexao();
			}
		}

		private void mostraAvaliacao()
		{
			string[] codigoAvaliacao = cmbAvaliacoes.Text.Split(new char[]
				{ '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
			string[] codigosQuestoes;

			/* Recupera os códigos das questões da avaliação */
			if (banco.executeComando("SELECT D1_QUESTAO " +
						"FROM htd1 " +
						"WHERE D1_COD = " + codigoAvaliacao[0],
						ref respostaBanco))
			{
				if (respostaBanco.HasRows)
				{
					int count = 0;

					respostaBanco.Read();
					codigosQuestoes = respostaBanco.GetString(0).Split(new char[]
							{ ',', ' ' }, StringSplitOptions.
							RemoveEmptyEntries);

					respostaBanco.Close();
					banco.fechaConexao();
					/* Recupera as questões da avaliação */
					foreach (string questao in codigosQuestoes)
					{
						if (banco.executeComando("SELECT B1_QUEST " +
							"FROM htb1 " +
							"WHERE B1_COD = " + questao, ref respostaBanco))
						{
							if (respostaBanco.HasRows)
							{
								respostaBanco.Read();
								txtAvaliacao.Text += (++count) + ") " +
										respostaBanco.GetString(0) +
										Environment.NewLine +
										Environment.NewLine;
							}
							respostaBanco.Close();
							banco.fechaConexao();
						}
					}
				}
				else
				{
					txtAvaliacao.Text = "Sem histórico";
					respostaBanco.Close();
					banco.fechaConexao();
				}
			}
		}

		private bool apagaRegistro()
		{
			string[] codigoAvaliacao = cmbAvaliacoes.Text.Split(
					new char[] { '(', ')' }, StringSplitOptions.
					RemoveEmptyEntries);

			if (banco.executeComando("UPDATE htd1 SET D_E_L_E_T = '*' " +
						"WHERE D1_COD = " + codigoAvaliacao[0]))
			{
				return true;
			}
			return false;
		}

		private bool apagaHistorico()
		{
			if (banco.executeComando("UPDATE htd1 SET D_E_L_E_T = '*'"))
			{
				return true;
			}
			return false;
		}

		// PAGE PESQUISA GERAL  PAGE PESQUISA GERAL  PAGE PESQUISA GERAL  PAGE PESQUISA GERAL  PAGE PESQUISA GERAL  //
		// PAGE PESQUISA GERAL  PAGE PESQUISA GERAL  PAGE PESQUISA GERAL  PAGE PESQUISA GERAL  PAGE PESQUISA GERAL  //
		private void pagePesquisaGeral_Enter(object sender, EventArgs e) => cmbTabela.SelectedIndex = 0;

		private void pagePesquisaGeral_Leave(object sender, EventArgs e) => txtPesquisaGeral.Clear();

		private void cmbTabela_SelectedIndexChanged(object sender, EventArgs e)
		{
			atualizaComboCampos();
			chamaPesquisaGeral();
		}

		private void cmbCampo_SelectedIndexChanged(object sender, EventArgs e)
		{
			alteraModoPesquisaGeral();
			chamaPesquisaGeral();
		}

		private void txtPesquisaGeral_TextChanged(object sender, EventArgs e) => chamaPesquisaGeral();

		private void radTrueGeral_CheckedChanged(object sender, EventArgs e) => chamaPesquisaGeral();

		private void radGeralDeletados_CheckedChanged(object sender, EventArgs e) => chamaPesquisaGeral();

		private void atualizaGridGeral()
		{
			ds = new DataSet();

			gridResultado.DataSource = null;

			adaptador.Fill(ds, "nome");
			gridResultado.DataSource = ds;
			gridResultado.DataMember = "nome";

			ds.Dispose();
			adaptador.Dispose();
			banco.fechaConexao();
		}

		private void atualizaComboCampos()
		{
			cmbCampo.Items.Clear();
			cmbCampo.Items.Add("Código");
			cmbCampo.SelectedIndex = 0;

			switch (cmbTabela.Text)
			{
				case "Questões":
					cmbCampo.Items.Add("Pergunta");
					cmbCampo.Items.Add("Objetivas?");  //2
					cmbCampo.Items.Add("Alternativa A");
					cmbCampo.Items.Add("Alternativa B");
					cmbCampo.Items.Add("Alternativa C");
					cmbCampo.Items.Add("Alternativa D");
					cmbCampo.Items.Add("Alternativa E");
					cmbCampo.Items.Add("Arquivo?");  //8
					cmbCampo.Items.Add("Usada?");  //9
					cmbCampo.Items.Add("Curso");
					cmbCampo.Items.Add("Disciplina");
					cmbCampo.Items.Add("Matéria");
					cmbCampo.Items.Add("Padrão?");  //13
					break;
				case "Cursos":
					cmbCampo.Items.Add("Nome");
					break;
				case "Disciplinas":
					cmbCampo.Items.Add("Nome");
					cmbCampo.Items.Add("Curso");
					break;
				case "Matérias":
					cmbCampo.Items.Add("Nome");
					cmbCampo.Items.Add("Disciplina");
					cmbCampo.Items.Add("Curso");
					break;
				case "Avaliações":
					cmbCampo.Items.Add("Data");
					cmbCampo.Items.Add("Tipo");
					cmbCampo.Items.Add("Inédita?");  //3
					cmbCampo.Items.Add("Questão");
					cmbCampo.Items.Add("Curso");
					cmbCampo.Items.Add("Disciplina");
					cmbCampo.Items.Add("Materia");
					break;
				default:
					Mensagem.erroInesperado();
					break;
			}
		}

		private void alteraModoPesquisaGeral()
		{
			switch (cmbTabela.Text)
			{
				case "Questões":
					if (cmbCampo.SelectedIndex == 2 ||
						cmbCampo.SelectedIndex == 8 ||
						cmbCampo.SelectedIndex == 9 ||
						cmbCampo.SelectedIndex == 13)
					{
						txtPesquisaGeral.Clear();
						pnlBooleanGeral.Show();
					}
					else
					{
						pnlBooleanGeral.Hide();
					}
					break;
				case "Cursos":
				case "Disciplinas":
				case "Matérias":
					txtPesquisaGeral.Clear();
					pnlBooleanGeral.Hide();
					break;
				case "Avaliações":
					if (cmbCampo.SelectedIndex == 3)
					{
						txtPesquisaGeral.Clear();
						pnlBooleanGeral.Show();
					}
					else
					{
						pnlBooleanGeral.Hide();
					}
					break;
				default:
					Mensagem.erroInesperado();
					break;
			}
		}

		private void chamaPesquisaGeral()
		{
			if (radGeralDeletados.Checked)
			{
				deletados = "IS NOT NULL";
			}
			else
			{
				deletados = "IS NULL";
			}

			switch (cmbTabela.Text)
			{
				case "Questões":
					switch (cmbCampo.Text)
					{
						case "Código":
							pesquisaQuestoesGrid("B1_COD", txtPesquisaGeral.Text,
									deletados);
							break;
						case "Pergunta":
							pesquisaQuestoesGrid("B1_QUEST", txtPesquisaGeral.Text,
									deletados);
							break;
						case "Objetivas?":
							pesquisaQuestoesGrid("B1_OBJETIV", radFalseGeral.Checked,
									deletados);
							break;
						case "Alternativa A":
							pesquisaQuestoesGrid("B1_QUEST", "a) " +
									txtPesquisaGeral.Text, deletados);
							break;
						case "Alternativa B":
							pesquisaQuestoesGrid("B1_QUEST", "b) " +
									txtPesquisaGeral.Text, deletados);
							break;
						case "Alternativa C":
							pesquisaQuestoesGrid("B1_QUEST", "c) " +
									txtPesquisaGeral.Text, deletados);
							break;
						case "Alternativa D":
							pesquisaQuestoesGrid("B1_QUEST", "d) " +
									txtPesquisaGeral.Text, deletados);
							break;
						case "Alternativa E":
							pesquisaQuestoesGrid("B1_QUEST", "e) " +
									txtPesquisaGeral.Text, deletados);
							break;
						case "Arquivo?":
							pesquisaQuestoesGrid("B1_ARQUIVO", radFalseGeral.Checked,
									deletados);
							break;
						case "Usada?":
							pesquisaQuestoesGrid("B1_USADA", radFalseGeral.Checked,
									deletados);
							break;
						case "Curso":
							pesquisaQuestoesGrid("C1_NOME", txtPesquisaGeral.Text,
									deletados);
							break;
						case "Disciplina":
							pesquisaQuestoesGrid("C2_NOME", txtPesquisaGeral.Text,
									deletados);
							break;
						case "Matéria":
							pesquisaQuestoesGrid("C3_NOME", txtPesquisaGeral.Text,
									deletados);
							break;
						case "Padrão?":
							pesquisaQuestoesGrid("B1_PADRAO", radFalseGeral.Checked,
									deletados);
							break;
						case "Deletadas?":
							break;
						default:
							Mensagem.erroInesperado();
							break;
					}
					break;
				case "Cursos":
					switch (cmbCampo.Text)
					{
						case "Código":
							pesquisaCursosGrid("C1_COD", txtPesquisaGeral.Text,
									deletados);
							break;
						case "Nome":
							pesquisaCursosGrid("C1_NOME", txtPesquisaGeral.Text,
									deletados);
							break;
						case "Deletados?":
							break;
						default:
							Mensagem.erroInesperado();
							break;
					}
					break;
				case "Disciplinas":
					switch (cmbCampo.Text)
					{
						case "Código":
							pesquisaDisciplinasGrid("C2_COD", txtPesquisaGeral.Text,
									deletados);
							break;
						case "Nome":
							pesquisaDisciplinasGrid("C2_NOME", txtPesquisaGeral.Text,
									deletados);
							break;
						case "Curso":
							pesquisaDisciplinasGrid("C1_NOME", txtPesquisaGeral.Text,
									deletados);
							break;
						case "Deletadas?":
							break;
						default:
							Mensagem.erroInesperado();
							break;
					}
					break;
				case "Matérias":
					switch (cmbCampo.Text)
					{
						case "Código":
							pesquisaMateriasGrid("C3_COD", txtPesquisaGeral.Text,
									deletados);
							break;
						case "Nome":
							pesquisaMateriasGrid("C3_NOME", txtPesquisaGeral.Text,
									deletados);
							break;
						case "Disciplina":
							pesquisaMateriasGrid("C2_NOME", txtPesquisaGeral.Text,
									deletados);
							break;
						case "Curso":
							pesquisaMateriasGrid("C1_NOME", txtPesquisaGeral.Text,
									deletados);
							break;
						case "Deletadas?":
							break;
						default:
							Mensagem.erroInesperado();
							break;
					}
					break;
				case "Avaliações":
					switch (cmbCampo.Text)
					{
						case "Código":
							pesquisaHistoricoGrid("D1_COD", txtPesquisaGeral.Text,
									deletados);
							break;
						case "Data":
							pesquisaHistoricoGrid("D1_DATA", txtPesquisaGeral.Text,
									deletados);
							break;
						case "Tipo":
							pesquisaHistoricoGrid("D1_TIPO", txtPesquisaGeral.Text,
									deletados);
							break;
						case "Inédita?":
							pesquisaHistoricoGrid("D1_INEDITA", radFalseGeral.Checked,
									deletados);
							break;
						case "Questão":
							pesquisaHistoricoGrid("D1_QUESTAO", txtPesquisaGeral.Text,
									deletados);
							break;
						case "Curso":
							pesquisaHistoricoGrid("C1_NOME", txtPesquisaGeral.Text,
									deletados);
							break;
						case "Disciplina":
							pesquisaHistoricoGrid("C2_NOME", txtPesquisaGeral.Text,
									deletados);
							break;
						case "Materia":
							pesquisaHistoricoGrid("C3_NOME", txtPesquisaGeral.Text,
									deletados);
							break;
						case "Deletadas?":
							break;
						default:
							Mensagem.erroInesperado();
							break;
					}
					break;
				default:
					Mensagem.erroInesperado();
					break;
			}
		}





		// PESQUISAS  PESQUISAS  PESQUISAS  PESQUISAS  PESQUISAS  PESQUISAS  PESQUISAS  PESQUISAS  PESQUISAS  //
		// PESQUISAS  PESQUISAS  PESQUISAS  PESQUISAS  PESQUISAS  PESQUISAS  PESQUISAS  PESQUISAS  PESQUISAS  //
		private void pesquisaQuestoesEdit(string campo, string condicao, string deletados)
		{
			if (banco.executeComando("SELECT htb1.*, C3_NOME, " +
						"C2_COD, C2_NOME, C1_COD, C1_NOME " +
					"FROM htb1 " +
						"INNER JOIN htc3 " +
							"ON B1_MATERIA = C3_COD " +
						"INNER JOIN htc2 " +
							"ON C3_DISCIPL = C2_COD " +
						"INNER JOIN htc1 " +
							"ON C2_CURSO = C1_COD " +
					"WHERE " + campo + " LIKE '%" + condicao + "%' AND " +
						"htb1.D_E_L_E_T " + deletados + " " +
					"ORDER BY B1_COD", ref respostaBanco))
			{
				atualizaCamposQuestoes();
			}
		}

		private void pesquisaQuestoesEdit(string campo, bool isNull, string deletados)
		{
			if (isNull)
			{
				if (banco.executeComando("SELECT htb1.*, C3_COD, C3_NOME, " +
							"C2_COD, C2_NOME, C1_COD, C1_NOME " +
						"FROM htb1 " +
							"INNER JOIN htc3 " +
								"ON B1_MATERIA = C3_COD " +
							"INNER JOIN htc2 " +
								"ON C3_DISCIPL = C2_COD " +
							"INNER JOIN htc1 " +
								"ON C2_CURSO = C1_COD " +
						"WHERE " + campo + " IS NULL AND " +
							"htb1.D_E_L_E_T " + deletados + " " +
						"ORDER BY B1_COD", ref respostaBanco))
				{
					atualizaCamposQuestoes();
				}
			}
			else
			{
				if (banco.executeComando("SELECT htb1.*, C3_COD, C3_NOME, " +
							"C2_COD, C2_NOME, C1_COD, C1_NOME " +
						"FROM htb1 " +
							"INNER JOIN htc3 " +
								"ON B1_MATERIA = C3_COD " +
							"INNER JOIN htc2 " +
								"ON C3_DISCIPL = C2_COD " +
							"INNER JOIN htc1 " +
								"ON C2_CURSO = C1_COD " +
						"WHERE " + campo + " IS NOT NULL AND " +
							"htb1.D_E_L_E_T " + deletados + " " +
						"ORDER BY B1_COD", ref respostaBanco))
				{
					atualizaCamposQuestoes();
				}
			}
		}

		private void pesquisaQuestoesGrid(string campo, string condicao, string deletados)
		{
			if (banco.executeComando("SELECT B1_COD AS 'Codigo', " +
						"B1_OBJETIV AS 'Objetiva?', B1_ARQUIVO AS 'Arquivo?', " +
						"B1_USADA AS 'Usada?', C1_NOME AS 'Curso', " +
						"C2_NOME AS 'Disciplina', C3_NOME AS 'Materia', " +
						"B1_PADRAO AS 'Padrao?', B1_QUEST AS 'Questao'" +
					"FROM htb1 " +
						"INNER JOIN htc3 " +
							"ON B1_MATERIA = C3_COD " +
						"INNER JOIN htc2 " +
							"ON C3_DISCIPL = C2_COD " +
						"INNER JOIN htc1 " +
							"ON C2_CURSO = C1_COD " +
					"WHERE " + campo + " LIKE '%" + condicao + "%' AND " +
						"htb1.D_E_L_E_T " + deletados + " " +
					"ORDER BY B1_COD", ref adaptador))
			{
				atualizaGridGeral();
			}
		}

		private void pesquisaQuestoesGrid(string campo, bool isNull, string deletados)
		{
			if (isNull)
			{
				if (banco.executeComando("SELECT B1_COD AS 'Codigo', " +
						"B1_OBJETIV AS 'Objetiva?', B1_ARQUIVO AS 'Arquivo?', " +
						"B1_USADA AS 'Usada?', C1_NOME AS 'Curso', " +
						"C2_NOME AS 'Disciplina', C3_NOME AS 'Materia', " +
						"B1_PADRAO AS 'Padrao?', B1_QUEST AS 'Questao'" +
					"FROM htb1 " +
						"INNER JOIN htc3 " +
							"ON B1_MATERIA = C3_COD " +
						"INNER JOIN htc2 " +
							"ON C3_DISCIPL = C2_COD " +
						"INNER JOIN htc1 " +
							"ON C2_CURSO = C1_COD " +
					"WHERE " + campo + " IS NULL AND " +
						"htb1.D_E_L_E_T " + deletados + " " +
					"ORDER BY B1_COD", ref adaptador))
				{
					atualizaGridGeral();
				}
			}
			else
			{
				if (banco.executeComando("SELECT B1_COD AS 'Codigo', " +
						"B1_OBJETIV AS 'Objetiva?', B1_ARQUIVO AS 'Arquivo?', " +
						"B1_USADA AS 'Usada?', C1_NOME AS 'Curso', " +
						"C2_NOME AS 'Disciplina', C3_NOME AS 'Materia', " +
						"B1_PADRAO AS 'Padrao?', B1_QUEST AS 'Questao'" +
					"FROM htb1 " +
						"INNER JOIN htc3 " +
							"ON B1_MATERIA = C3_COD " +
						"INNER JOIN htc2 " +
							"ON C3_DISCIPL = C2_COD " +
						"INNER JOIN htc1 " +
							"ON C2_CURSO = C1_COD " +
					"WHERE " + campo + " IS NOT NULL AND " +
						"htb1.D_E_L_E_T " + deletados + " " +
					"ORDER BY B1_COD", ref adaptador))
				{
					atualizaGridGeral();
				}
			}
		}

		// CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  //
		// CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  //
		private void pesquisaCursosEdit(string campo, string condicao, string deletados)
		{
			if (banco.executeComando("SELECT * " +
					"FROM htc1 " +
					"WHERE " + campo + " LIKE '%" + condicao +
						"%' AND D_E_L_E_T " + deletados + " " +
					"ORDER BY C1_COD", ref respostaBanco))
			{
				atualizaCamposCursos();
			}
		}

		private void pesquisaCursosGrid(string campo, string condicao, string deletados)
		{
			if (banco.executeComando("SELECT C1_COD AS 'Codigo', " +
						"C1_NOME AS 'Curso'" +
					"FROM htc1 " +
					"WHERE " + campo + " LIKE '%" + condicao + "%' AND " +
						"D_E_L_E_T " + deletados + " " +
					"ORDER BY C1_COD", ref adaptador))
			{
				atualizaGridGeral();
			}
		}

		// DISCIPLINA  DISCIPLINA  DISCIPLINA  DISCIPLINA  DISCIPLINA  DISCIPLINA  DISCIPLINA  DISCIPLINA  //
		// DISCIPLINA  DISCIPLINA  DISCIPLINA  DISCIPLINA  DISCIPLINA  DISCIPLINA  DISCIPLINA  DISCIPLINA  //
		private void pesquisaDisciplinasEdit(string campo, string condicao, string deletados)
		{
			if (banco.executeComando("SELECT htc2.*, C1_NOME " +
					"FROM htc2 " +
						"INNER JOIN htc1 " +
							"ON C2_CURSO = C1_COD " +
					"WHERE " + campo + " LIKE '%" + condicao +
						"%' AND htc2.D_E_L_E_T " + deletados +
					" ORDER BY C2_COD", ref respostaBanco))
			{
				atualizaCamposDisciplinas();
			}
		}

		private void pesquisaDisciplinasGrid(string campo, string condicao, string deletados)
		{
			if (banco.executeComando("SELECT C2_COD AS 'Codigo', " +
						"C2_NOME AS 'Disciplina', C1_NOME AS 'Curso'" +
					"FROM htc2 " +
						"INNER JOIN htc1 " +
							"ON C2_CURSO = C1_COD " +
					"WHERE " + campo + " LIKE '%" + condicao + "%' AND " +
						"htc2.D_E_L_E_T " + deletados + " " +
					"ORDER BY C2_COD", ref adaptador))
			{
				atualizaGridGeral();
			}
		}

		// MATÉRIA  MATÉRIA  MATÉRIA  MATÉRIA  MATÉRIA  MATÉRIA  MATÉRIA  MATÉRIA  MATÉRIA  MATÉRIA  MATÉRIA  //
		// MATÉRIA  MATÉRIA  MATÉRIA  MATÉRIA  MATÉRIA  MATÉRIA  MATÉRIA  MATÉRIA  MATÉRIA  MATÉRIA  MATÉRIA  //
		private void pesquisaMateriasEdit(string campo, string condicao, string deletados)
		{
			if (banco.executeComando("SELECT htc3.*, C2_NOME " +
						"FROM htc3 " +
							"INNER JOIN htc2 " +
								"ON C3_DISCIPL = C2_COD " +
						"WHERE " + campo + " LIKE '%" + condicao +
							"%' AND htc3.D_E_L_E_T " + deletados +
						" ORDER BY C3_COD", ref respostaBanco))
			{
				atualizaCamposMaterias();
			}
		}

		private void pesquisaMateriasGrid(string campo, string condicao, string deletados)
		{
			if (banco.executeComando("SELECT C3_COD AS 'Codigo', " +
						"C3_NOME AS 'Materia', C2_NOME AS 'Disciplina', " +
						"C1_NOME AS 'Curso'" +
					"FROM htc3 " +
						"INNER JOIN htc2 " +
							"ON C3_DISCIPL = C2_COD " +
						"INNER JOIN htc1 " +
							"ON C2_CURSO = C1_COD " +
					"WHERE " + campo + " LIKE '%" + condicao +
						"%' AND htc3.D_E_L_E_T " + deletados +
					" ORDER BY C3_COD", ref adaptador))
			{
				atualizaGridGeral();
			}
		}

		// AVALIAÇÕES  AVALIAÇÕES  AVALIAÇÕES  AVALIAÇÕES  AVALIAÇÕES  AVALIAÇÕES  AVALIAÇÕES  AVALIAÇÕES  //
		// AVALIAÇÕES  AVALIAÇÕES  AVALIAÇÕES  AVALIAÇÕES  AVALIAÇÕES  AVALIAÇÕES  AVALIAÇÕES  AVALIAÇÕES  //
		private void pesquisaHistoricoEdit(string campo, string condicao, string deletados)
		{
			if (banco.executeComando("SELECT D1_COD, D1_DATA, " +
						"D1_QUESTAO, C2_NOME " +
					"FROM htd1 " +
						"INNER JOIN htc3 " +
							"ON D1_MATERIA = C3_COD " +
						//    "ON SUBSTRING(D1_MATERIA, 1, " + 
						//    "LOCATE(',', D1_MATERIA) - 1) = C3_COD " + 
						"INNER JOIN htc2 " +
							"ON C3_DISCIPL = C2_COD " +
						"INNER JOIN htc1 " +
							"ON C2_CURSO = C1_COD " +
					"WHERE " + campo + " LIKE '%" + condicao + "%' " +
						"AND htd1.D_E_L_E_T " + deletados + " " +
					"ORDER BY D1_COD", ref respostaBanco))
			{
				atualizaCamposAvaliacao();
			}
		}

		private void pesquisaHistoricoGrid(string campo, string condicao, string deletados)
		{
			if (banco.executeComando("SELECT D1_COD AS 'Codigo', D1_DATA " +
						"AS 'Data', D1_TIPO AS 'Tipo', D1_INEDITA AS 'Inedita?', " +
						"D1_QUESTAO AS 'Questoes', C1_NOME AS 'Curso', " +
						"C2_NOME AS 'Disciplina', D1_MATERIA AS 'Codigos Materias'" +
					"FROM htd1 " +
						"INNER JOIN htc3 " +
							"ON D1_MATERIA = C3_COD " +
						"INNER JOIN htc2 " +
							"ON C3_DISCIPL = C2_COD " +
						"INNER JOIN htc1 " +
							"ON C2_CURSO = C1_COD " +
					"WHERE " + campo + " LIKE '%" + condicao + "%' " +
						"AND htd1.D_E_L_E_T " + deletados + " " +
					"ORDER BY D1_COD", ref adaptador))
			{
				atualizaGridGeral();
			}
		}

		private void pesquisaHistoricoGrid(string campo, bool isNull, string deletados)
		{
			if (isNull)
			{
				if (banco.executeComando("SELECT D1_COD AS 'Codigo', D1_DATA " +
							"AS 'Data', D1_TIPO AS 'Tipo', D1_INEDITA AS 'Inedita?', " +
							"D1_QUESTAO AS 'Questoes', C1_NOME AS 'Curso', " +
							"C2_NOME AS 'Disciplina', D1_MATERIA AS 'Codigos Materias'" +
						"FROM htd1 " +
							"INNER JOIN htc3 " +
								"ON D1_MATERIA = C3_COD " +
							"INNER JOIN htc2 " +
								"ON C3_DISCIPL = C2_COD " +
							"INNER JOIN htc1 " +
								"ON C2_CURSO = C1_COD " +
						"WHERE " + campo + " IS NULL AND " +
							"htd1.D_E_L_E_T " + deletados + " " +
						"ORDER BY D1_COD", ref adaptador))
				{
					atualizaGridGeral();
				}
			}
			else
			{
				if (banco.executeComando("SELECT D1_COD AS 'Codigo', D1_DATA " +
							"AS 'Data', D1_TIPO AS 'Tipo', D1_INEDITA AS 'Inedita?', " +
							"D1_QUESTAO AS 'Questoes', C1_NOME AS 'Curso', " +
							"C2_NOME AS 'Disciplina', D1_MATERIA AS 'Codigos Materias'" +
						"FROM htd1 " +
							"INNER JOIN htc3 " +
								"ON D1_MATERIA = C3_COD " +
							"INNER JOIN htc2 " +
								"ON C3_DISCIPL = C2_COD " +
							"INNER JOIN htc1 " +
								"ON C2_CURSO = C1_COD " +
						"WHERE " + campo + " IS NOT NULL AND " +
							"htd1.D_E_L_E_T " + deletados + " " +
						"ORDER BY D1_COD", ref adaptador))
				{
					atualizaGridGeral();
				}
			}
		}
	}
}
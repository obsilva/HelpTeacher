using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using HelpTeacher.Classes;
using HelpTeacher.Domain.Entities;
using HelpTeacher.Repository;
using HelpTeacher.Repository.Repositories;

namespace HelpTeacher.Forms
{
	public partial class Pesquisa : Form
	{
		private ConnectionManager banco = new ConnectionManager();
		private MySql.Data.MySqlClient.MySqlDataReader respostaBanco;
		private MySql.Data.MySqlClient.MySqlDataAdapter adaptador;
		private BindingSource courseBindingSource = new BindingSource();
		private BindingSource disciplineBindingSource = new BindingSource();
		private BindingSource subjectBindingSource = new BindingSource();
		private BindingSource examBindingSource = new BindingSource();
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
			IQueryable<Course> courses = new CourseRepository().Get(true);

			courseBindingSource.DataSource = courses;
			combo.DataSource = courseBindingSource;
			courseBindingSource.ResetBindings(true);
			combo.DisplayMember = nameof(Course.Name);
			combo.ValueMember = nameof(Course.RecordID);
			combo.SelectedIndex = 0;
		}

		private void atualizaDisciplinas(ComboBox combo)
		{
			IQueryable<Discipline> disciplines = new DisciplineRepository().Get(true);

			disciplineBindingSource.DataSource = disciplines;
			combo.DataSource = disciplineBindingSource;
			disciplineBindingSource.ResetBindings(true);
			combo.DisplayMember = nameof(Discipline.Name);
			combo.ValueMember = nameof(Discipline.RecordID);
			combo.SelectedIndex = 0;
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
			salvaQuestaoModificada();
			ativaDesativaEdicaoQuestao(false);
			chamaPesquisaQuestoes();
			Mensagem.dadosAlterados();
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

		private void salvaQuestaoModificada()
		{
			Question question = new QuestionRepository().Get(Convert.ToInt32(txtCodigoQuestao.Text));

			question.FirstAttachment = txtArquivo1.Text;
			question.IsDefault = chkQuestaoPadrao.Checked;
			question.IsObjective = !radDissertativa.Checked;
			question.IsRecordActive = !chkQuestaoDeletada.Checked;
			question.SecondAttachment = txtArquivo2.Text;
			question.Statement = txtQuestao.Text;
			question.WasUsed = chkQuestaoUsada.Checked;

			new QuestionRepository().Update(question);
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
			salvaCursoModificado();
			ativaDesativaEdicaoCurso(false);
			chamaPesquisaCursos();
			Mensagem.dadosAlterados();
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
				chkCursoDeletado.Checked = respostaBanco["D_E_L_E_T"].ToString().Equals("*");
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

		private void salvaCursoModificado()
		{
			var course = new Course(txtNomeCurso.Text)
			{
				IsRecordActive = !chkCursoDeletado.Checked,
				RecordID = Convert.ToInt32(txtCodigoCurso)
			};

			new CourseRepository().Update(course);
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
			salvaDiscModificada();
			ativaDesativaEdicaoDisc(false);
			chamaPesquisaDisciplinas();
			Mensagem.dadosAlterados();
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
				txtCodigoDisciplina.Text = respostaBanco["C2_COD"].ToString();
				textNomeDisciplina.Text = respostaBanco["C2_NOME"].ToString();
				chkDisciplinaDeletada.Checked = !respostaBanco["D_E_L_E_T"].ToString().Equals("*");

				var courses = new List<Course>()
				{
					new Course(respostaBanco["C1_NOME"].ToString())
					{
						RecordID = Convert.ToInt32(respostaBanco["C2_CURSO"].ToString())
					}
				};

				courseBindingSource.DataSource = courses;
				cmbCursosDisciplina.DataSource = courseBindingSource;
				courseBindingSource.ResetBindings(true);
				cmbCursosDisciplina.DisplayMember = nameof(Course.Name);
				cmbCursosDisciplina.ValueMember = nameof(Course.RecordID);
				cmbCursosDisciplina.SelectedIndex = 0;
			}
			else
			{
				btnEditarDisciplina.Enabled = false;
				txtCodigoDisciplina.Clear();
				chkDisciplinaDeletada.Checked = false;
				textNomeDisciplina.Clear();
				cmbCursosDisciplina.Items.Clear();

			}

			respostaBanco.Close();
			banco.fechaConexao();
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

		private void salvaDiscModificada()
		{
			var courses = new List<Course>() { (Course) cmbCursosDisciplina.SelectedItem };
			var discipline = new Discipline(courses, textNomeDisciplina.Text)
			{
				IsRecordActive = !chkDisciplinaDeletada.Checked,
				RecordID = Convert.ToInt32(txtCodigoDisciplina.Text),

			};

			new DisciplineRepository().Update(discipline);
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
			salvaMateriaModificada();
			ativaDesativaEdicaoMateria(false);
			chamaPesquisaMaterias();
			Mensagem.dadosAlterados();
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
				txtCodigoMateria.Text = respostaBanco["C3_COD"].ToString();
				txtNomeMateria.Text = respostaBanco["C3_NOME"].ToString();
				chkMateriaDeletada.Checked = !respostaBanco["D_E_L_E_T"].ToString().Equals("*");

				var disciplines = new List<Discipline>()
				{
					new Discipline(null, respostaBanco["C2_NOME"].ToString())
					{
						RecordID = Convert.ToInt32(respostaBanco["C3_DISCIPL"].ToString())
					}
				};

				disciplineBindingSource.DataSource = disciplines;
				cmbDisciplinaMateria.DataSource = disciplineBindingSource;
				disciplineBindingSource.ResetBindings(true);
				cmbDisciplinaMateria.DisplayMember = nameof(Discipline.Name);
				cmbDisciplinaMateria.ValueMember = nameof(Discipline.RecordID);
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
			}

			respostaBanco.Close();
			banco.fechaConexao();
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

		private void salvaMateriaModificada()
		{
			var disciplines = new List<Discipline>() { (Discipline) cmbDisciplinaMateria.SelectedItem };
			var subject = new Subject(disciplines, txtNomeMateria.Text)
			{
				IsRecordActive = !chkMateriaDeletada.Checked,
				RecordID = Convert.ToInt32(txtCodigoMateria.Text)
			};

			new SubjectRepository().Update(subject);
		}

		private void recuperaNomeCurso()
		{
			if (cmbDisciplinaMateria.SelectedIndex != -1)
			{
				Discipline discipline = new DisciplineRepository()
					.Get(((Discipline) cmbDisciplinaMateria.SelectedItem).RecordID);

				txtCursoMateria.Text = discipline.Courses.FirstOrDefault()?.Name;
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
			cmbAvaliacoes.ResetBindings();
			cmbDisciplinasAvaliacoes.ResetBindings();
			cmbMateriasAvaliacoes.ResetBindings();
			txtAvaliacao.Clear();

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
			cmbMateriasAvaliacoes.ResetBindings();

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
			apagaRegistro();
			Mensagem.dadosAlterados();
			chamaPesquisaAvaliacoes();
		}

		private void btnLimparHistorico_Click(object sender, EventArgs e)
		{
			apagaHistorico();
			Mensagem.dadosAlterados();
			chamaPesquisaAvaliacoes();
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
			cmbAvaliacoes.ResetBindings();

			IQueryable<Course> courses = new CourseRepository().Get();

			courseBindingSource.DataSource = courses;
			cmbCursosAvaliacoes.DataSource = courseBindingSource;
			courseBindingSource.ResetBindings(true);
			cmbCursosAvaliacoes.DisplayMember = nameof(Course.Name);
			cmbCursosAvaliacoes.ValueMember = nameof(Course.RecordID);
		}

		private void atualizaComboDisicplina()
		{
			IQueryable<Discipline> disciplines = new DisciplineRepository()
				.GetWhereCourse((Course) cmbCursosAvaliacoes.SelectedItem);

			disciplineBindingSource.DataSource = disciplines;
			cmbDisciplinasAvaliacoes.DataSource = disciplineBindingSource;
			disciplineBindingSource.ResetBindings(true);
			cmbDisciplinasAvaliacoes.DisplayMember = nameof(Discipline.Name);
			cmbDisciplinasAvaliacoes.ValueMember = nameof(Discipline.RecordID);
			cmbDisciplinasAvaliacoes.SelectedIndex = 0;
		}

		private void atualizaComboMaterias()
		{
			IQueryable<Subject> subjects = new SubjectRepository()
				.GetWhereDiscipline((Discipline) cmbDisciplinasAvaliacoes.SelectedItem);

			subjectBindingSource.DataSource = subjects;
			cmbMateriasAvaliacoes.DataSource = subjectBindingSource;
			subjectBindingSource.ResetBindings(true);
			cmbMateriasAvaliacoes.DisplayMember = nameof(Subject.Name);
			cmbMateriasAvaliacoes.ValueMember = nameof(Subject.RecordID);
			cmbMateriasAvaliacoes.SelectedIndex = 0;
		}

		private void atualizaCamposAvaliacao()
		{
			var exams = new List<Exam>();

			examBindingSource.DataSource = exams;
			cmbAvaliacoes.DataSource = examBindingSource;
			if (respostaBanco.HasRows)
			{
				while (respostaBanco.Read())
				{
					exams.Add(new ExamRepository().Get(respostaBanco.GetInt32(0)));
				}


				cmbAvaliacoes.DisplayMember = nameof(Exam.GeneratedDate);
				cmbAvaliacoes.ValueMember = nameof(Exam.RecordID);
				cmbAvaliacoes.SelectedIndex = 0;
			}
			else
			{
				txtAvaliacao.Clear();
				cmbAvaliacoes.Text = "Sem histórico";
				btnLimparRegistro.Enabled = false;
				respostaBanco.Close();
				banco.fechaConexao();
			}

			examBindingSource.ResetBindings(true);
		}

		private void mostraAvaliacao()
		{
			var exam = (Exam) cmbAvaliacoes.SelectedItem;

			txtAvaliacao.Text = "Sem histórico";
			for (int i = 0; i < exam.Questions.Count; i++)
			{
				txtAvaliacao.Text += $"{(i + 1)}) {exam.Questions.ElementAt(i).Statement}{Environment.NewLine}" +
									 $"{Environment.NewLine}";
			}
		}

		private void apagaRegistro()
		{
			var exam = (Exam) cmbAvaliacoes.SelectedItem;
			exam.IsRecordActive = false;

			new ExamRepository().Update(exam);
		}

		private void apagaHistorico()
		{
			IQueryable<Exam> exams = new ExamRepository().Get();

			foreach (Exam exam in exams)
			{
				exam.IsRecordActive = false;
			}

			new ExamRepository().Update(exams);
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
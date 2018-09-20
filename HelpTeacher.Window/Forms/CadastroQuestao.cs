using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using HelpTeacher.Classes;
using HelpTeacher.Domain.Entities;
using HelpTeacher.Repository;
using HelpTeacher.Repository.Repositories;

namespace HelpTeacher.Forms
{
	public partial class CadastroQuestao : Form
	{
		private ConnectionManager banco = new ConnectionManager();
		private MySql.Data.MySqlClient.MySqlDataReader respostaBanco;
		private BindingSource courseBindingSource = new BindingSource();
		private BindingSource disciplineBindingSource = new BindingSource();
		private BindingSource subjectBindingSource = new BindingSource();

		public CadastroQuestao()
		{
			InitializeComponent();

			atualizaCodigoQuestao();
			atualizaCMB();
			cmbCursos.Items.Clear();
			chkDisciplinas.Items.Clear();
			chkMaterias.Items.Clear();
		}

		private void radObjetiva_CheckedChanged(object sender, EventArgs e)
		{
			if (radObjetiva.Checked == true)
			{
				pnlAlternativas.Enabled = true;
			}
			else
			{
				pnlAlternativas.Enabled = false;
			}
		}

		private void cbbCursos_SelectedIndexChanged(object sender, EventArgs e) => atualizaCHKDisciplinas();

		/* chkDisciplinas_SelectedIndexChanged
		 * 
		 * Evento "SelectedIndexChanged" do checkedListBox das disciplinas.
		 * Popula o checkedListBox das matérias de acordo com as disciplinas selecionadas.
		 * Esse evento é disparado sempre que um item do checkedListBox em questão é 
		 * marcado ou  desmarcado. Dessa forma, as matérias são colocadas ou retiradas do
		 * checkedListBox das matérias de forma dinâmica.
		 */
		private void chkDisciplinas_SelectedIndexChanged(object sender, EventArgs e) => atualizaMaterias();

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

		private void btnSalvar_Click(object sender, EventArgs e)
		{
			if (cadastrarQuestao())
			{
				limpaForm();
				Mensagem.cadastradoEfetuado();
			}
			else
			{
				if (chkMaterias.CheckedItems.Count == 0)
				{
					Mensagem.selecionarMateria();
				}
				else
				{
					Mensagem.erroCadastro();
				}

			}
		}

		private void btnCancelar_Click(object sender, EventArgs e) => Close();

		private void atualizaCodigoQuestao()
		{
			if (banco.executeComando("SHOW TABLE STATUS LIKE 'htb1'", ref respostaBanco))
			{
				respostaBanco.Read();
				txtCodQuestao.Text = respostaBanco["Auto_increment"].ToString();
				respostaBanco.Close();
				banco.fechaConexao();
			}
		}

		/* atualizaCMB
		 * 
		 * Popula o comboBox com os cursos cadastrados
		 */
		private void atualizaCMB()
		{
			IQueryable<Course> courses = new CourseRepository().Get(true);

			courseBindingSource.DataSource = courses;
			cmbCursos.DataSource = courseBindingSource;
			courseBindingSource.ResetBindings(true);
			cmbCursos.DisplayMember = nameof(Course.Name);
			cmbCursos.ValueMember = nameof(Course.RecordID);
			cmbCursos.SelectedIndex = 0;
		}

		/* atualizaCHKDisciplinas
		 * 
		 * Popula o checkedListBox das disciplinas, de acordo com o curso selecionado.
		 * Função disparada sempre que o comboBox altera seu index, dessa forma, as
		 * matérias são colocadas no checkedListBox de forma dinâmica, de acordo com
		 * o curso selecionado
		 */
		private void atualizaCHKDisciplinas()
		{
			if (cmbCursos.SelectedIndex != -1)
			{
				IQueryable<Discipline> disciplines = new DisciplineRepository()
						.GetWhereCourse((Course) cmbCursos.SelectedItem, true);

				disciplineBindingSource.DataSource = disciplines;
				chkDisciplinas.DataSource = disciplineBindingSource;
				disciplineBindingSource.ResetBindings(true);
				chkDisciplinas.DisplayMember = nameof(Course.Name);
				chkDisciplinas.ValueMember = nameof(Course.RecordID);
				chkDisciplinas.SelectedIndex = 0;
			}
			else
			{
				disciplineBindingSource.Clear();
				chkDisciplinas.DataSource = disciplineBindingSource;
				disciplineBindingSource.ResetBindings(true);
			}
		}

		/* atualizaMaterias
		 * 
		 * Popula o checkedListBox das matérias de acordo com as disciplinas selecionadas.
		 */
		private void atualizaMaterias()
		{
			var subjects = new List<Subject>();
			subjectBindingSource.DataSource = subjects;

			subjectBindingSource.ResetBindings(true);
			foreach (Discipline discipline in chkDisciplinas.CheckedItems)
			{
				if (banco.executeComando("SELECT C3_COD, C3_NOME FROM htc3 WHERE C3_DISCIPL = " +
								$"{discipline.RecordID} AND D_E_L_E_T IS NULL", ref respostaBanco))
				{
					if (respostaBanco.HasRows)
					{
						while (respostaBanco.Read())
						{
							ICollection<Discipline> disciplines = new List<Discipline>() { discipline };
							subjects.Add(new Subject(disciplines, respostaBanco.GetString(1))
							{
								RecordID = respostaBanco.GetInt32(0),
								IsRecordActive = true
							});
						}

						chkMaterias.DataSource = subjectBindingSource;
						subjectBindingSource.ResetBindings(true);
						chkMaterias.DisplayMember = nameof(Discipline.Name);
						chkMaterias.ValueMember = nameof(Discipline.RecordID);
					}
				}
				respostaBanco.Close();
				banco.fechaConexao();
			}
		}

		/* cadastrarQuestao
		* 
		* Cadastra a questão no banco de dados, levando em conta
		* as opções que o usuário tem a disposição
		*/
		private bool cadastrarQuestao()
		{
			//Verifica se existe alguma matéria selecionada
			if (chkMaterias.CheckedItems.Count != 0)
			{
				var subjects = new List<Subject>();
				foreach (Subject subject in chkMaterias.CheckedItems)
				{
					subjects.Add(subject);

					var question = new Question(subjects, txtQuestao.Text) { RecordID = 1 };

					//Dissertativa
					if (radDissertativa.Checked)
					{
						if (!banco.executeComando("INSERT INTO htb1 (B1_QUEST, B1_MATERIA) VALUES ('" +
												  $"{question.Statement}', {subject.RecordID})"))
						{
							return false;
						}
					}
					//Objetiva
					else
					{
						//Sem alternativas em branco
						if ((!txtAlternativaA.Text.Equals("")) && (!txtAlternativaB.Text.Equals("")) &&
								(!txtAlternativaC.Text.Equals("")) && (!txtAlternativaD.Text.Equals("")))
						{
							if (!banco.executeComando($"INSERT INTO htb1 VALUES (NULL, '{question.Statement}" +
										Environment.NewLine +
										"a) " + txtAlternativaA.Text +
										Environment.NewLine +
										"b) " + txtAlternativaB.Text +
										Environment.NewLine +
										"c) " + txtAlternativaC.Text +
										Environment.NewLine +
										"d) " + txtAlternativaD.Text +
										$"', '*', NULL, NULL, { subject.RecordID}, NULL, NULL)"))
							{
								return false;
							}
						}
						else
						{
							Mensagem.alternativasEmBranco();
							if (txtAlternativaA.Text.Equals(""))
							{
								txtAlternativaA.Focus();
							}
							else if (txtAlternativaB.Text.Equals(""))
							{
								txtAlternativaB.Focus();
							}
							else if (txtAlternativaC.Text.Equals(""))
							{
								txtAlternativaC.Focus();
							}
							else
							{
								txtAlternativaD.Focus();
							}

							//return false;
						}
					}

					if (!txtAlternativaE.Text.Equals(""))
					{
						if (!banco.executeComando("UPDATE htb1 SET B1_QUEST = CONCAT(B1_QUEST, '" +
										Environment.NewLine + "e) " + txtAlternativaE.Text +
									$"') WHERE B1_COD = {question.RecordID}"))
						{
							return false;
						}
					}

					//Arquivos
					if (!(txtArquivo1.Text.Equals("")) || !(txtArquivo2.Text.Equals("")))
					{
						//Apenas 1 arquivo
						if ((txtArquivo1.Text.Equals("")) || (txtArquivo2.Text.Equals("")))
						{
							if (banco.executeComando($"UPDATE htb1 SET B1_ARQUIVO = '{question.RecordID}_1' " +
										$"WHERE B1_COD = {question.RecordID}"))
							{
								copiaArquivo();
							}
						}
						//Dois arquivos
						else
						{
							if (banco.executeComando($"UPDATE htb1 SET B1_ARQUIVO = '{question.RecordID}_1' " +
										$"{question.RecordID}_2' WHERE B1_COD = { question.RecordID}"))
							{
								copiaArquivo();
							}
						}
					}
					atualizaCodigoQuestao();
				}
				return true;
			}
			return false;
		}

		/* copiaArquivo
		 * 
		 * Copia os arquivos selecionados pelo usuário para uma
		 * pasta específica de arquivos, dentro da pasta do programa,
		 * para que não ocorra nenhum erro caso o usuário mova o arquivo
		 * original de lugar, ou apague o mesmo.
		 */
		private void copiaArquivo()
		{
			string primeiroArquivo = Path.Combine(@"..\_files", (txtCodQuestao.Text + "_1"));

			if (txtArquivo1.Text.Equals(""))
			{
				string extensao = Path.GetExtension(txtArquivo2.Text);
				primeiroArquivo = String.Concat(primeiroArquivo, extensao);

				File.Copy(txtArquivo2.Text, primeiroArquivo, true);
			}
			else
			{
				string extensao1 = Path.GetExtension(txtArquivo1.Text);
				primeiroArquivo = String.Concat(primeiroArquivo, extensao1);

				File.Copy(txtArquivo1.Text, primeiroArquivo, true);
				if (!txtArquivo2.Text.Equals(""))
				{
					string segundoArquivo = Path.Combine(@"..\_files", (txtCodQuestao.Text + "_2"));
					string extensao2 = Path.GetExtension(txtArquivo2.Text);
					segundoArquivo = String.Concat(segundoArquivo, extensao2);

					File.Copy(txtArquivo2.Text, segundoArquivo, true);
				}
			}
		}

		/* limpaForm
		 * 
		 * Passa o cadastro para sua forma inicial
		 */
		private void limpaForm()
		{
			atualizaCodigoQuestao();
			cmbCursos.SelectedIndex = 0;
			atualizaCHKDisciplinas();
			txtQuestao.Text = "";
			txtArquivo1.Text = "";
			txtArquivo2.Text = "";
			txtAlternativaA.Text = "";
			txtAlternativaB.Text = "";
			txtAlternativaC.Text = "";
			txtAlternativaD.Text = "";
			txtAlternativaE.Text = "";
		}
	}
}

// This Source Code Form is subject to the terms of the Mozilla 
// Public License, v. 2.0. If a copy of the MPL was not distributed 
// with this file, You can obtain one at https://mozilla.org/MPL/2.0/

// Authors: 
//		Otávio Bueno Silva <obsilva94@gmail.com>

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using HelpTeacher.Classes;
using HelpTeacher.Domain.Entities;
using HelpTeacher.Repository;
using HelpTeacher.Repository.Repositories;

namespace HelpTeacher.Forms
{
	public partial class CadastroQuestao : Form
	{
		private BindingSource courseBindingSource = new BindingSource();
		private BindingSource disciplineBindingSource = new BindingSource();
		private BindingSource subjectBindingSource = new BindingSource();


		#region Properties
		private ConnectionManager ConnectionManager { get; }
		#endregion


		public CadastroQuestao()
		{
			ConnectionManager = new ConnectionManager();

			InitializeComponent();

			atualizaCodigoQuestao();
			atualizaCMB();
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
			cadastrarQuestao();
			limpaForm();
			atualizaCodigoQuestao();
			Mensagem.cadastradoEfetuado();
		}

		private void btnCancelar_Click(object sender, EventArgs e) => Close();

		private void atualizaCodigoQuestao()
		{
			using (DbDataReader dataReader = ConnectionManager.ExecuteReader("SHOW TABLE STATUS LIKE 'htb1'"))
			{
				dataReader.Read();
				txtCodQuestao.Text = dataReader["Auto_increment"].ToString();
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
				chkDisciplinas.DisplayMember = nameof(Discipline.Name);
				chkDisciplinas.ValueMember = nameof(Discipline.RecordID);
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
			var sourceList = new List<Subject>();
			subjectBindingSource.DataSource = sourceList;

			foreach (Discipline discipline in chkDisciplinas.CheckedItems)
			{
				IQueryable<Subject> subjects = new SubjectRepository().GetWhereDiscipline(discipline, true);

				sourceList.AddRange(subjects);
			}

			chkMaterias.DataSource = subjectBindingSource;
			subjectBindingSource.ResetBindings(true);
			chkMaterias.DisplayMember = nameof(Subject.Name);
			chkMaterias.ValueMember = nameof(Subject.RecordID);
		}

		/* cadastrarQuestao
		* 
		* Cadastra a questão no banco de dados, levando em conta
		* as opções que o usuário tem a disposição
		*/
		private void cadastrarQuestao()
		{
			if (chkMaterias.CheckedItems.Count == 0)
			{
				Mensagem.selecionarMateria();
				chkMaterias.Focus();
				return;
			}

			foreach (Subject subject in chkMaterias.CheckedItems)
			{
				var question = new Question(subject, txtQuestao.Text)
				{
					FirstAttachment = txtArquivo1.Text,
					IsObjective = !radDissertativa.Checked,
					SecondAttachment = txtArquivo2.Text
				};

				if (!String.IsNullOrWhiteSpace(txtAlternativaA.Text))
				{
					question.Statement += new StringBuilder(txtQuestao.Text)
						.Append(Environment.NewLine)
						.Append("a) ").Append(txtAlternativaA.Text)
						.Append(Environment.NewLine)
						.Append("b) ").Append(txtAlternativaB.Text)
						.Append(Environment.NewLine)
						.Append("c) ").Append(txtAlternativaC.Text)
						.Append(Environment.NewLine)
						.Append("d) ").Append(txtAlternativaD.Text)
						.Append(Environment.NewLine)
						.Append("e) ").Append(txtAlternativaE.Text);
				}

				new QuestionRepository().Add(question);
			}
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

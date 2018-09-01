using HelpTeacher.Classes;
using HelpTeacher.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HelpTeacher.Forms
{
	public partial class CadastroConteudo : Form
	{
		private ConexaoBanco banco = new ConexaoBanco();
		private MySql.Data.MySqlClient.MySqlDataReader respostaBanco;
		private AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
		private BindingSource bindingSource = new BindingSource();

		public CadastroConteudo(int pag)
		{
			InitializeComponent();

			if (pag == 1)
			{
				tabControlConteudo.SelectedTab = tabCursos;
			}
			else if (pag == 2)
			{
				tabControlConteudo.SelectedTab = tabDisciplinas;
			}
			else
			{
				tabControlConteudo.SelectedTab = tabMaterias;
			}
		}

		// CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  //
		// CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  //
		private void tabCursos_Enter(object sender, EventArgs e)
		{
			atualizaCodigoCurso();
			autoCompleteCursos();
		}

		private void btnSalvarCurso_Click(object sender, EventArgs e)
		{
			var course = new Course(txtNomeCurso.Text);

			if (cadastraCurso(course))
			{
				limparForm();
				atualizaCodigoCurso();
				Mensagem.cadastradoEfetuado();
			}
		}

		private void btnCancelarCurso_Click(object sender, EventArgs e) => Close();

		private bool cadastraCurso(Course value)
			=> podeCadastrar()
				? banco.executeComando($"INSERT INTO htc1 VALUES (NULL, '{value.Name}', NULL)")
				: false;

		private void atualizaCodigoCurso()
		{
			if (banco.executeComando("SHOW TABLE STATUS LIKE 'htc1'", ref respostaBanco))
			{
				respostaBanco.Read();
				txtCodigoCurso.Text = respostaBanco["Auto_increment"].ToString();
				respostaBanco.Close();
				banco.fechaConexao();
			}
		}

		private void autoCompleteCursos()
		{
			collection.Clear();
			if (banco.executeComando("SELECT C1_NOME FROM htc1", ref respostaBanco))
			{
				while (respostaBanco.Read())
				{
					collection.Add(respostaBanco.GetString(0));
				}
				respostaBanco.Close();
				banco.fechaConexao();
			}
			txtNomeCurso.AutoCompleteCustomSource = collection;
		}

		// DISCIPLINA  DISCIPLINA  DISCIPLINA  DISCIPLINA  DISCIPLINA  DISCIPLINA  DISCIPLINA  DISCIPLINA  //
		// DISCIPLINA  DISCIPLINA  DISCIPLINA  DISCIPLINA  DISCIPLINA  DISCIPLINA  DISCIPLINA  DISCIPLINA  //
		private void tabDisciplinas_Enter(object sender, EventArgs e)
		{
			carregaCursos();
			atualizaCodigoDisciplina();
			autoCompleteDisciplinas();
		}

		private void cmbCurso_SelectedIndexChanged(object sender, EventArgs e) => autoCompleteDisciplinas();

		private void bntSalvarDisc_Click(object sender, EventArgs e)
		{
			var discipline = new Discipline((Course) cmbCurso.SelectedItem, txtNomeDisciplina.Text);

			if (cadastraDisciplina(discipline))
			{
				limparForm();
				atualizaCodigoDisciplina();
				Mensagem.cadastradoEfetuado();
			}
		}

		private void btnCancelarDisc_Click(object sender, EventArgs e) => Close();

		private bool carregaCursos()
		{
			var courses = new List<Course>();
			bindingSource.DataSource = courses;

			bindingSource.ResetBindings(true);
			if (banco.executeComando("SELECT C1_COD, C1_NOME FROM htc1 WHERE D_E_L_E_T IS NULL", ref respostaBanco))
			{
				if (respostaBanco.HasRows)
				{
					while (respostaBanco.Read())
					{
						courses.Add(new Course(respostaBanco.GetString(1))
						{
							RecordID = respostaBanco.GetInt32(0),
							IsRecordActive = true
						});
					}

					respostaBanco.Close();
					banco.fechaConexao();

					cmbCurso.DataSource = bindingSource;
					bindingSource.ResetBindings(true);
					cmbCurso.DisplayMember = nameof(Course.Name);
					cmbCurso.ValueMember = nameof(Course.RecordID);
					cmbCurso.SelectedIndex = 0;

					return true;
				}
				respostaBanco.Close();
				banco.fechaConexao();
			}
			return false;
		}

		private bool cadastraDisciplina(Discipline value)
		{
			return podeCadastrar()
				? banco.executeComando($"INSERT INTO htc2 VALUES (NULL,'{value.Name}', {value.Course.RecordID}, NULL)")
				: false;
		}

		private void atualizaCodigoDisciplina()
		{
			if (banco.executeComando("SHOW TABLE STATUS LIKE 'htc2'", ref respostaBanco))
			{
				respostaBanco.Read();
				txtCodigoDisciplina.Text = respostaBanco["Auto_increment"].ToString();
				respostaBanco.Close();
				banco.fechaConexao();
			}
		}

		private void autoCompleteDisciplinas()
		{
			collection.Clear();
			if (cmbCurso.SelectedIndex != -1)
			{
				if (banco.executeComando($"SELECT C2_NOME FROM htc2 WHERE C2_CURSO = " +
					((Course) cmbCurso.SelectedItem).RecordID, ref respostaBanco))
				{
					while (respostaBanco.Read())
					{
						collection.Add(respostaBanco.GetString(0));
					}
					respostaBanco.Close();
					banco.fechaConexao();
				}
			}
			txtNomeDisciplina.AutoCompleteCustomSource = collection;
		}

		// MATÉRIA  MATÉRIA  MATÉRIA  MATÉRIA  MATÉRIA  MATÉRIA  MATÉRIA  MATÉRIA  MATÉRIA  MATÉRIA  MATÉRIA  //
		// MATÉRIA  MATÉRIA  MATÉRIA  MATÉRIA  MATÉRIA  MATÉRIA  MATÉRIA  MATÉRIA  MATÉRIA  MATÉRIA  MATÉRIA  //
		private void tabMaterias_Enter(object sender, EventArgs e)
		{
			carregaDisciplinas();
			atualizaCodigoMateria();
			autoCompleteMaterias();
		}

		private void comboBoxDisciplina_SelectedIndexChanged(object sender, EventArgs e)
		{
			buscaCurso();
			autoCompleteMaterias();
		}

		private void bntSalvarMateria_Click(object sender, EventArgs e)
		{
			if (cadastraMateria())
			{
				limparForm();
				atualizaCodigoMateria();
				Mensagem.cadastradoEfetuado();
			}
		}

		private void btnCancelarMateria_Click(object sender, EventArgs e) => Close();

		private bool carregaDisciplinas()
		{
			var disciplines = new List<Discipline>();
			bindingSource.DataSource = disciplines;

			bindingSource.ResetBindings(true);
			if (banco.executeComando($"SELECT C1_COD, C1_NOME, htc1.D_E_L_E_T, C2_COD, C2_NOME " +
				"FROM htc2 INNER JOIN htc1 ON C2_CURSO = C1_COD WHERE htc2.D_E_L_E_T IS NULL", ref respostaBanco))
			{
				if (respostaBanco.HasRows)
				{
					while (respostaBanco.Read())
					{
						var course = new Course(respostaBanco.GetString(1))
						{
							RecordID = respostaBanco.GetInt32(0),
							IsRecordActive = !respostaBanco.IsDBNull(2)
						};

						disciplines.Add(new Discipline(course, respostaBanco.GetString(4))
						{
							RecordID = respostaBanco.GetInt32(3),
							IsRecordActive = true
						});
					}

					respostaBanco.Close();
					banco.fechaConexao();

					cmbDisciplina.DataSource = bindingSource;
					bindingSource.ResetBindings(true);
					cmbDisciplina.DisplayMember = nameof(Course.Name);
					cmbDisciplina.ValueMember = nameof(Course.RecordID);
					cmbDisciplina.SelectedIndex = 0;

					return true;
				}
				respostaBanco.Close();
				banco.fechaConexao();
			}
			return false;
		}

		private void buscaCurso()
		{
			var discipline = (Discipline) cmbDisciplina.SelectedItem;
			txtCurso.Text = discipline?.Course?.Name;
		}

		private bool cadastraMateria()
		{
			if (podeCadastrar())
			{
				string[] codigoDisciplina = cmbDisciplina.Text.Split(new char[] { '(', ')' },
							StringSplitOptions.RemoveEmptyEntries);

				if (banco.executeComando("INSERT INTO htc3 VALUES (NULL, '" +
							txtNomeMateria.Text + "', " + codigoDisciplina[0] + ", NULL)"))
				{
					return true;
				}
			}
			return false;
		}

		private void atualizaCodigoMateria()
		{
			if (banco.executeComando("SHOW TABLE STATUS LIKE 'htc3'", ref respostaBanco))
			{
				respostaBanco.Read();
				txtCodigoMateria.Text = respostaBanco["Auto_increment"].ToString();
				respostaBanco.Close();
				banco.fechaConexao();
			}
		}

		private void autoCompleteMaterias()
		{
			collection.Clear();
			if (cmbDisciplina.SelectedIndex != -1)
			{
				if (banco.executeComando($"SELECT C3_NOME FROM htc3 WHERE C3_DISCIPL = " +
					((Discipline) cmbDisciplina.SelectedItem).RecordID, ref respostaBanco))
				{
					while (respostaBanco.Read())
					{
						collection.Add(respostaBanco.GetString(0));
					}
					respostaBanco.Close();
					banco.fechaConexao();
				}
			}
			txtNomeMateria.AutoCompleteCustomSource = collection;
		}

		// FUNÇÕES GERAIS  FUNÇÕES GERAIS  FUNÇÕES GERAIS  FUNÇÕES GERAIS  FUNÇÕES GERAIS  FUNÇÕES GERAIS  //
		// FUNÇÕES GERAIS  FUNÇÕES GERAIS  FUNÇÕES GERAIS  FUNÇÕES GERAIS  FUNÇÕES GERAIS  FUNÇÕES GERAIS  //
		private void limparForm()
		{
			txtNomeCurso.Clear();
			txtNomeDisciplina.Clear();
			txtNomeMateria.Clear();
		}

		private bool podeCadastrar()
		{
			if (tabControlConteudo.SelectedTab == tabCursos)
			{
				/* Sem nome */
				if (String.IsNullOrWhiteSpace(txtNomeCurso.Text))
				{
					Mensagem.campoEmBranco();
					return false;
				}
				/* Curso já existe */
				if (collection.Contains(txtNomeCurso.Text))
				{
					Mensagem.cursoExistente();
					return false;
				}
			}
			else if (tabControlConteudo.SelectedTab == tabDisciplinas)
			{
				/* Sem nome */
				if (String.IsNullOrWhiteSpace(txtNomeDisciplina.Text))
				{
					Mensagem.campoEmBranco();
					txtNomeDisciplina.Focus();
					return false;
				}
				/* Disciplina já existe */
				if (collection.Contains(txtNomeDisciplina.Text))
				{
					Mensagem.disciplinaExistente();
					return false;
				}
				/* Nenhum curso selecionado */
				if (cmbCurso.SelectedIndex == -1)
				{
					Mensagem.campoEmBranco();
					cmbCurso.Focus();
					return false;
				}
			}
			else
			{
				/* Sem nome */
				if (String.IsNullOrWhiteSpace(txtNomeMateria.Text))
				{
					Mensagem.campoEmBranco();
					txtNomeMateria.Focus();
					return false;
				}
				/* Matéria já existe */
				if (collection.Contains(txtNomeMateria.Text))
				{
					Mensagem.materiaExistente();
					return false;
				}
				/* Nenhuma disciplina selecionada */
				if (cmbDisciplina.SelectedIndex == -1)
				{
					Mensagem.campoEmBranco();
					cmbDisciplina.Focus();
					return false;
				}
			}

			return true;
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
	}
}
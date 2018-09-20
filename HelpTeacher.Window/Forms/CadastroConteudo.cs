using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using HelpTeacher.Classes;
using HelpTeacher.Domain.Entities;
using HelpTeacher.Repository;
using HelpTeacher.Repository.Repositories;

namespace HelpTeacher.Forms
{
	public partial class CadastroConteudo : Form
	{
		#region Fields
		private readonly BindingSource courseBindingSource = new BindingSource();
		private readonly BindingSource disciplineBindingSource = new BindingSource();
		private AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
		private MySql.Data.MySqlClient.MySqlDataReader respostaBanco;
		private ConnectionManager banco = new ConnectionManager();
		#endregion


		#region Constructors
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
		#endregion


		#region Methods
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
			cadastraCurso(course);
			limparForm();
			atualizaCodigoCurso();
			Mensagem.cadastradoEfetuado();
		}

		private void btnCancelarCurso_Click(object sender, EventArgs e) => Close();

		private void cadastraCurso(Course value)
		{
			if (podeCadastrar())
			{
				new CourseRepository().Add(value);
			}
		}

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

			IQueryable<Course> courses = new CourseRepository().Get();
			foreach (Course course in courses)
			{
				collection.Add(course.Name);
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
			ICollection<Course> courses = new List<Course>() { (Course) cmbCurso.SelectedItem };
			var discipline = new Discipline(courses, txtNomeDisciplina.Text);

			cadastraDisciplina(discipline);
			limparForm();
			atualizaCodigoDisciplina();
			Mensagem.cadastradoEfetuado();
		}

		private void btnCancelarDisc_Click(object sender, EventArgs e) => Close();

		private void carregaCursos()
		{
			IQueryable<Course> courses = new CourseRepository().Get(true);

			courseBindingSource.DataSource = courses;
			cmbCurso.DataSource = courseBindingSource;
			courseBindingSource.ResetBindings(true);
			cmbCurso.DisplayMember = nameof(Course.Name);
			cmbCurso.ValueMember = nameof(Course.RecordID);
			cmbCurso.SelectedIndex = 0;
		}

		private void cadastraDisciplina(Discipline value)
		{
			if (podeCadastrar())
			{
				new DisciplineRepository().Add(value);
			}
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
				IQueryable<Discipline> disciplines = new DisciplineRepository()
					.GetWhereCourse((Course) cmbCurso.SelectedItem);
				foreach (Discipline discipline in disciplines)
				{
					collection.Add(discipline.Name);
				}

				respostaBanco.Close();
				banco.fechaConexao();
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
			ICollection<Discipline> disciplines = new List<Discipline>() { (Discipline) cmbDisciplina.SelectedItem };
			var subject = new Subject(disciplines, txtNomeMateria.Text);

			if (cadastraMateria(subject))
			{
				limparForm();
				atualizaCodigoMateria();
				Mensagem.cadastradoEfetuado();
			}
		}

		private void btnCancelarMateria_Click(object sender, EventArgs e) => Close();

		private void carregaDisciplinas()
		{
			IQueryable<Discipline> disciplines = new DisciplineRepository().Get(true);

			disciplineBindingSource.DataSource = disciplines;
			cmbDisciplina.DataSource = disciplineBindingSource;
			disciplineBindingSource.ResetBindings(true);
			cmbDisciplina.DisplayMember = nameof(Course.Name);
			cmbDisciplina.ValueMember = nameof(Course.RecordID);
			cmbDisciplina.SelectedIndex = 0;
		}

		private void buscaCurso()
		{
			var discipline = (Discipline) cmbDisciplina.SelectedItem;
			txtCurso.Text = discipline?.Courses?.FirstOrDefault()?.Name;
		}

		private bool cadastraMateria(Subject value)
		{
			if (podeCadastrar())
			{
				string query = $"INSERT INTO htc3 VALUES(NULL, '{value.Name}', " +
							   $"{ value.Disciplines.FirstOrDefault()?.RecordID}, NULL)";
				return banco.executeComando(query);
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
		#endregion
	}
}
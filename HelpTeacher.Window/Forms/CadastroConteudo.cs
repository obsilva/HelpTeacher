// This Source Code Form is subject to the terms of the Mozilla 
// Public License, v. 2.0. If a copy of the MPL was not distributed 
// with this file, You can obtain one at https://mozilla.org/MPL/2.0/

// Authors: 
//		Otávio Bueno Silva <obsilva94@gmail.com>

using System;
using System.Data.Common;
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
		#endregion


		#region Properties
		private ConnectionManager ConnectionManager { get; }
		#endregion


		#region Constructors
		public CadastroConteudo(int pag)
		{
			ConnectionManager = new ConnectionManager();


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
			try
			{
				var course = new Course(txtNomeCurso.Text);
				cadastraCurso(course);
				limparForm();
				atualizaCodigoCurso();
				Mensagem.cadastradoEfetuado();
			}
			catch (ArgumentNullException)
			{
				Mensagem.campoEmBranco();
			}
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
			using (DbDataReader dataReader = ConnectionManager.ExecuteReader("SHOW TABLE STATUS LIKE 'htc1'"))
			{
				dataReader.Read();
				txtCodigoCurso.Text = dataReader["Auto_increment"].ToString();
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
			try
			{
				var discipline = new Discipline(cmbCurso.SelectedItem as Course, txtNomeDisciplina.Text);

				cadastraDisciplina(discipline);
				limparForm();
				atualizaCodigoDisciplina();
				Mensagem.cadastradoEfetuado();
			}
			catch (ArgumentNullException)
			{
				Mensagem.campoEmBranco();
			}
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
			using (DbDataReader dataReader = ConnectionManager.ExecuteReader("SHOW TABLE STATUS LIKE 'htc2'"))
			{
				dataReader.Read();
				txtCodigoDisciplina.Text = dataReader["Auto_increment"].ToString();
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
			try
			{
				var subject = new Subject(cmbDisciplina.SelectedItem as Discipline, txtNomeMateria.Text);

				cadastraMateria(subject);
				limparForm();
				atualizaCodigoMateria();
				Mensagem.cadastradoEfetuado();
			}
			catch (ArgumentNullException)
			{
				Mensagem.campoEmBranco();
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
			txtCurso.Text = discipline?.Course?.Name;
		}

		private void cadastraMateria(Subject value)
		{
			if (podeCadastrar())
			{
				new SubjectRepository().Add(value);
			}
		}

		private void atualizaCodigoMateria()
		{
			using (DbDataReader dataReader = ConnectionManager.ExecuteReader("SHOW TABLE STATUS LIKE 'htc3'"))
			{
				dataReader.Read();
				txtCodigoMateria.Text = dataReader["Auto_increment"].ToString();
			}
		}

		private void autoCompleteMaterias()
		{
			collection.Clear();

			if (cmbDisciplina.SelectedIndex != -1)
			{
				IQueryable<Subject> subjects = new SubjectRepository()
					.GetWhereDiscipline((Discipline) cmbDisciplina.SelectedItem);

				foreach (Subject subject in subjects)
				{
					collection.Add(subject.Name);
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
				/* Curso já existe */
				if (collection.Contains(txtNomeCurso.Text))
				{
					Mensagem.cursoExistente();
					return false;
				}
			}
			else if (tabControlConteudo.SelectedTab == tabDisciplinas)
			{
				/* Disciplina já existe */
				if (collection.Contains(txtNomeDisciplina.Text))
				{
					Mensagem.disciplinaExistente();
					return false;
				}
			}
			else
			{
				/* Matéria já existe */
				if (collection.Contains(txtNomeMateria.Text))
				{
					Mensagem.materiaExistente();
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
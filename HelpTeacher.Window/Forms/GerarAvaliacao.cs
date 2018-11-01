// This Source Code Form is subject to the terms of the Mozilla 
// Public License, v. 2.0. If a copy of the MPL was not distributed 
// with this file, You can obtain one at https://mozilla.org/MPL/2.0/

// Authors: 
//		Otávio Bueno Silva <obsilva94@gmail.com>

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using HelpTeacher.Classes;
using HelpTeacher.Domain.Entities;
using HelpTeacher.Repository;
using HelpTeacher.Repository.Repositories;

using Word = Microsoft.Office.Interop.Word;

namespace HelpTeacher.Forms
{
	public partial class GerarAvaliacao : Form
	{
		private BindingSource courseBindingSource = new BindingSource();
		private BindingSource disciplineBindingSource = new BindingSource();
		private BindingSource questionBindingSource = new BindingSource();
		private BindingSource subjectBindingSource = new BindingSource();
		private string codigoAvaliacao;
		private Label[] lblMateria;
		private NumericUpDown[] numQuantidadeQuestoes;
		private Label[] lblDe;
		private TextBox[] txtQuantidadeMaxima;
		private int countGlob = 0;


		#region Properties
		private ConnectionManager ConnectionManager { get; }
		#endregion


		public GerarAvaliacao()
		{
			ConnectionManager = new ConnectionManager();

			InitializeComponent();

			preencheComboCursos();
		}

		private void cmbCurso_SelectedIndexChanged(object sender, EventArgs e)
		{
			preencheComboDisciplinas();
			insereComponentes();

			questionBindingSource.Clear();
			lstCodigosAleatorios.Items.Clear();
		}

		private void cmbDisciplina_SelectedIndexChanged(object sender, EventArgs e)
		{
			preencheCheckMaterias();
			insereComponentes();

			questionBindingSource.Clear();
			lstCodigosAleatorios.Items.Clear();
		}

		private void chkMaterias_SelectedIndexChanged(object sender, EventArgs e)
		{
			insereComponentes();
			recuperaCodigos();
		}

		private void questoesMaterias_ValueChanged(object sender, EventArgs e)
		{
			int somatoria = 0;

			for (int count = 0; count < lblMateria.Length; count++)
			{
				somatoria += (int) numQuantidadeQuestoes[count].Value;
			}
			txtTotalQuestoes.Text = somatoria.ToString();
		}

		private void chkAvaUnica_CheckedChanged(object sender, EventArgs e) => recuperaCodigos();

		private void rbObjetiva_CheckedChanged(object sender, EventArgs e) => recuperaCodigos();

		private void rbDissertativa_CheckedChanged(object sender, EventArgs e) => recuperaCodigos();

		private void bntGerarAvaliacao_Click(object sender, EventArgs e) => geraAvaliacao();

		// FUNÇÕES  FUNÇÕES  FUNÇÕES  FUNÇÕES  FUNÇÕES  FUNÇÕES  FUNÇÕES  FUNÇÕES  FUNÇÕES  FUNÇÕES  FUNÇÕES  //
		// FUNÇÕES  FUNÇÕES  FUNÇÕES  FUNÇÕES  FUNÇÕES  FUNÇÕES  FUNÇÕES  FUNÇÕES  FUNÇÕES  FUNÇÕES  FUNÇÕES  //
		private void preencheComboCursos()
		{
			IQueryable<Course> courses = new CourseRepository().Get(true);

			courseBindingSource.DataSource = courses;
			cmbCurso.DataSource = courseBindingSource;
			courseBindingSource.ResetBindings(true);
			cmbCurso.DisplayMember = nameof(Course.Name);
			cmbCurso.ValueMember = nameof(Course.RecordID);
			cmbCurso.SelectedIndex = 0;
		}

		private void preencheComboDisciplinas()
		{
			if (cmbCurso.SelectedIndex != -1)
			{
				IQueryable<Discipline> disciplines = new DisciplineRepository()
					.GetWhereCourse((Course) cmbCurso.SelectedItem, true);

				disciplineBindingSource.DataSource = disciplines;
				cmbDisciplina.DataSource = disciplineBindingSource;
				disciplineBindingSource.ResetBindings(true);
				cmbDisciplina.DisplayMember = nameof(Course.Name);
				cmbDisciplina.ValueMember = nameof(Course.RecordID);
				cmbDisciplina.SelectedIndex = 0;
			}
			else
			{
				disciplineBindingSource.Clear();
				cmbDisciplina.DataSource = disciplineBindingSource;
				disciplineBindingSource.ResetBindings(true);
			}
		}

		private void preencheCheckMaterias()
		{
			IQueryable<Subject> subjects = new SubjectRepository().
				GetWhereDiscipline((Discipline) cmbDisciplina.SelectedItem, true);

			subjectBindingSource.DataSource = subjects;
			chkMaterias.DataSource = subjectBindingSource;
			subjectBindingSource.ResetBindings(true);
			chkMaterias.DisplayMember = nameof(Discipline.Name);
			chkMaterias.ValueMember = nameof(Discipline.RecordID);
		}

		private void recuperaCodigos()
		{
			IQueryable<Question> questions = new List<Question>().AsQueryable();
			questionBindingSource.Clear();
			countGlob = 0;

			/* Avaliação Mista */
			if (radMista.Checked)
			{
				if (chkAvaliacaoInedita.Checked)
				{
					foreach (Subject subject in chkMaterias.CheckedItems)
					{
						questions = new QuestionRepository().GetWhereSubject(subject, true, true, false);
						questions = questions.Concat(new QuestionRepository().GetWhereSubject(subject, true, false, false));
					}
				}
				else
				{
					foreach (Subject subject in chkMaterias.CheckedItems)
					{
						questions = new QuestionRepository().GetWhereSubject(subject, true);
					}
				}
			}
			/* Avaliação Dissertativa */
			else if (radDissertativa.Checked)
			{
				if (chkAvaliacaoInedita.Checked)
				{
					foreach (Subject subject in chkMaterias.CheckedItems)
					{
						questions = new QuestionRepository().GetWhereSubject(subject, true, true, true);
					}
				}

				else
				{
					foreach (Subject subject in chkMaterias.CheckedItems)
					{
						questions = new QuestionRepository().GetWhereSubject(subject, true, true);
					}
				}
			}
			/* Avaliação Objetiva */
			else if (radObjetiva.Checked)
			{
				if (chkAvaliacaoInedita.Checked)
				{
					foreach (Subject subject in chkMaterias.CheckedItems)
					{
						questions = new QuestionRepository().GetWhereSubject(subject, true, false, true);
					}
				}
				else
				{
					foreach (Subject subject in chkMaterias.CheckedItems)
					{
						questions = new QuestionRepository().GetWhereSubject(subject, true, false);
					}
				}
			}

			atualizaValorCampos(questions);
		}

		private void atualizaValorCampos(IQueryable<Question> questions)
		{
			if (questions.Count() > 0)
			{
				questions.OrderBy(p => p.RecordID);

				questionBindingSource.DataSource = questions;
				lstCodigos.DataSource = questionBindingSource;
				questionBindingSource.ResetBindings(true);
				lstCodigos.DisplayMember = nameof(Question.RecordID);
				lstCodigos.ValueMember = nameof(Question.RecordID);


				txtQuantidadeMaxima[countGlob].Text = questions.Count().ToString();
				numQuantidadeQuestoes[countGlob].Maximum = questions.Count();
				numQuantidadeQuestoes[countGlob].Minimum = 1;
				numQuantidadeQuestoes[countGlob].Value = 1;

			}

			countGlob++;
		}

		private void insereComponentes()
		{
			if (lblMateria != null)
			{
				for (int count = 0; count < lblMateria.Length; count++)
				{
					lblMateria[count].Dispose();
					numQuantidadeQuestoes[count].Dispose();
					lblDe[count].Dispose();
					txtQuantidadeMaxima[count].Dispose();
				}
			}

			lblMateria = new Label[chkMaterias.CheckedItems.Count];
			numQuantidadeQuestoes = new NumericUpDown[chkMaterias.CheckedItems.Count];
			lblDe = new Label[chkMaterias.CheckedItems.Count];
			txtQuantidadeMaxima = new TextBox[chkMaterias.CheckedItems.Count];
			int xInicial = 185;
			int yInicial = 300;
			int x;
			int y;

			txtTotalQuestoes.Text = chkMaterias.CheckedItems.Count.ToString();
			if (chkMaterias.CheckedItems.Count < 9)
			{
				yInicial -= (chkMaterias.CheckedItems.Count * 16);
				Size = new Size(798, 483);
			}
			else
			{
				int aumentoTamanho = (chkMaterias.CheckedItems.Count - 8) * 33;
				int localizacao = aumentoTamanho / 2;
				yInicial -= (8 * 16);

				Height = 483 + aumentoTamanho;
				label3.Top = 176 + localizacao;
				lstCodigos.Top = 193 + localizacao;
				label5.Top = 266 + localizacao;
				lstCodigosAleatorios.Top = 283 + localizacao;
				bntGerarAvaliacao.Top = 361 + localizacao;
			}

			for (int count = 0; count < chkMaterias.CheckedItems.Count; count++)
			{
				var subject = (Subject) chkMaterias.CheckedItems[count];
				lblMateria[count] = new Label();
				numQuantidadeQuestoes[count] = new NumericUpDown();
				lblDe[count] = new Label();
				txtQuantidadeMaxima[count] = new TextBox();
				// 
				// Label matéria
				//
				y = yInicial + (count * 33);
				lblMateria[count].AutoSize = false;
				lblMateria[count].Location = new System.Drawing.Point(xInicial, (y + 3));
				lblMateria[count].Size = new System.Drawing.Size(409, 14);
				lblMateria[count].Text = $"Quantidade de questões de {subject.Name}";
				// 
				// NumericUpDown
				//
				x = xInicial + 6 + lblMateria[count].Width;
				numQuantidadeQuestoes[count].Location = new System.Drawing.Point(x, y);
				numQuantidadeQuestoes[count].Size = new System.Drawing.Size(65, 22);
				numQuantidadeQuestoes[count].Maximum = 0;
				numQuantidadeQuestoes[count].Minimum = 0;
				numQuantidadeQuestoes[count].ValueChanged += new System.EventHandler(
							questoesMaterias_ValueChanged);
				// 
				// Label de
				//
				x = numQuantidadeQuestoes[count].Location.X + 71;
				lblDe[count].AutoSize = false;
				lblDe[count].Location = new System.Drawing.Point(x, (y + 3));
				lblDe[count].Size = new System.Drawing.Size(23, 14);
				lblDe[count].Text = "de";
				// 
				// TextBox
				//
				x = lblDe[count].Location.X + 29;
				txtQuantidadeMaxima[count].Location = new System.Drawing.Point(x, y);
				txtQuantidadeMaxima[count].Size = new System.Drawing.Size(65, 22);
				txtQuantidadeMaxima[count].Text = "0";
				txtQuantidadeMaxima[count].ReadOnly = true;

				Controls.Add(lblMateria[count]);
				Controls.Add(numQuantidadeQuestoes[count]);
				Controls.Add(lblDe[count]);
				Controls.Add(txtQuantidadeMaxima[count]);
			}
		}

		private void geraCodigosAleatorios()
		{
			int count = 0;
			string query;

			lstCodigosAleatorios.Items.Clear();
			foreach (Subject subject in chkMaterias.CheckedItems)
			{
				/* Avaliação Mista */
				if (radMista.Checked)
				{
					if (chkAvaliacaoInedita.Checked)
					{
						query = $"SELECT B1_COD FROM htb1 WHERE B1_MATERIA = {subject.RecordID} AND " +
								$"B1_USADA IS NULL AND D_E_L_E_T IS NULL ORDER BY RAND() LIMIT " +
								$"{numQuantidadeQuestoes[count].Value}";
					}
					else
					{
						query = $"SELECT B1_COD FROM htb1 WHERE B1_MATERIA = {subject.RecordID} AND " +
								$"D_E_L_E_T IS NULL ORDER BY RAND() LIMIT { numQuantidadeQuestoes[count].Value}";
					}
				}
				/* Avaliação Dissertativa */
				else if (radDissertativa.Checked)
				{
					if (chkAvaliacaoInedita.Checked)
					{
						query = $"SELECT B1_COD FROM htb1 WHERE B1_MATERIA = {subject.RecordID} AND " +
								$"B1_USADA IS NULL AND B1_OBJETIV IS NULL AND D_E_L_E_T IS NULL " +
								$"ORDER BY RAND() LIMIT {numQuantidadeQuestoes[count].Value}";
					}
					else
					{
						query = $"SELECT B1_COD FROM htb1 WHERE B1_MATERIA = {subject.RecordID} AND " +
								$"B1_OBJETIV IS NULL AND D_E_L_E_T IS NULL ORDER BY RAND() " +
								$"LIMIT {numQuantidadeQuestoes[count].Value}";
					}
				}
				/* Avaliação Objetiva */
				else
				{
					if (chkAvaliacaoInedita.Checked)
					{
						query = $"SELECT B1_COD FROM htb1 WHERE B1_MATERIA =  {subject.RecordID} AND " +
								$"B1_USADA IS NULL AND B1_OBJETIV IS NOT NULL AND D_E_L_E_T IS NULL " +
								$"ORDER BY RAND() LIMIT {numQuantidadeQuestoes[count].Value}";
					}
					else
					{
						query = $"SELECT B1_COD FROM htb1 WHERE B1_MATERIA =  {subject.RecordID} AND " +
								$"B1_OBJETIV IS NOT NULL AND D_E_L_E_T IS NULL ORDER BY RAND() " +
								$"LIMIT {numQuantidadeQuestoes[count].Value}";
					}
				}

				atualizaCodigosAleatorios(query);
				count++;
			}
		}

		private void atualizaCodigosAleatorios(string query)
		{
			using (DbDataReader dataReader = ConnectionManager.ExecuteReader(query))
			{
				if (dataReader.HasRows)
				{
					while (dataReader.Read())
					{
						lstCodigosAleatorios.Items.Add(dataReader.GetString(0));
					}
				}
			}
		}

		private void geraDocumentoWord()
		{
			object fimDocumento = "\\endofdoc";
			Word.Range wordRange;
			object range;
			int numQuestao = 0;

			/* Abre a aplicação word e faz uma cópia do documento mapeado */
			object template = Path.GetFullPath(@"..\modelo.docx");
			var appWord = new Word.Application
			{
				Visible = true
			};

			Word.Document document = appWord.Documents.Add(ref template, true);
			document.Paragraphs.Alignment = Word.WdParagraphAlignment.
					wdAlignParagraphJustify;

			string[] disciplina = cmbDisciplina.Text.Split(new char[] { '(', ')' },
					StringSplitOptions.RemoveEmptyEntries);
			substituiTagsWord(document, "@disciplina", disciplina[1]);

			/* Busca as questões no banco e insere no Word */
			while (numQuestao < Convert.ToInt32(txtTotalQuestoes.Text))
			{
				string query = $"SELECT RTRIM(B1_QUEST), B1_ARQUIVO FROM htb1 WHERE B1_COD = " +
							   $"{lstCodigosAleatorios.Items[numQuestao]}";
				using (DbDataReader dataReader = ConnectionManager.ExecuteReader(query))
				{
					if (dataReader.HasRows)
					{
						dataReader.Read();

						/* Questão */
						wordRange = document.Bookmarks.get_Item(ref fimDocumento).Range;
						wordRange.InsertParagraphAfter();
						wordRange.InsertAfter((numQuestao + 1) + ") " +
								dataReader.GetString(0) + Environment.NewLine);

						/* Arquivos */
						if (!Convert.IsDBNull(dataReader["B1_ARQUIVO"]))
						{
							string[] nomeArquivo = dataReader.GetString(1).
									Split(new char[] { ',', ' ' },
									StringSplitOptions.RemoveEmptyEntries);
							string[] arquivo = Directory.GetFiles(@"..\_files\",
									nomeArquivo[0] + ".*");
							string caminhoImagem = Path.GetFullPath(arquivo[0]);
							string extensao = Path.GetExtension(arquivo[0]);

							range = document.Bookmarks.get_Item(ref fimDocumento).Range;

							/* Dois arquivos */
							if (nomeArquivo.Length > 1)
							{
								if (extensao == ".png" || extensao == ".jpg") //imagem
								{
									document.InlineShapes.AddPicture(caminhoImagem,
											false, true, ref range);
								}
								else
								{
									Process.Start(arquivo[0]);
								}

								arquivo = Directory.GetFiles(@"..\_files\",
										nomeArquivo[1] + ".*");
								caminhoImagem = Path.GetFullPath(arquivo[0]);
								extensao = Path.GetExtension(arquivo[0]);

								if (extensao == ".png" || extensao == ".jpg")
								{
									document.InlineShapes.AddPicture(caminhoImagem,
											false, true, ref range);
								}
								else
								{
									Process.Start(arquivo[0]);
								}
							}
							/* Somente 1 arquivo */
							else
							{
								if (extensao == ".png" || extensao == ".jpg")
								{
									document.InlineShapes.AddPicture(caminhoImagem,
											false, true, ref range);
								}
								else
								{
									Process.Start(arquivo[0]);
								}
							}
							wordRange = document.Bookmarks.get_Item(ref fimDocumento).Range;
							wordRange.InsertParagraphAfter();
						}
					}
				}
				numQuestao++;
			}
		}

		private void substituiTagsWord(Word.Document document, object parametro, object texto)
		{
			object missing = System.Reflection.Missing.Value;
			Word.Range rng = document.Range(ref missing, ref missing);
			object FindText = parametro;
			object ReplaceWith = texto;
			object MatchWholeWord = true;
			object Forward = false;

			rng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing,
					ref missing, ref missing, ref Forward, ref missing, ref missing,
					ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);
		}

		private void insereHistorico()
		{
			var questions = new List<Question>();
			var subjects = new Subject[chkMaterias.CheckedItems.Count];

			chkMaterias.CheckedItems.CopyTo(subjects, 0);
			foreach (object item in lstCodigosAleatorios.Items)
			{
				questions.Add(new QuestionRepository().Get(Convert.ToInt32(item.ToString())));
			}

			var exam = new Exam(questions, subjects)
			{
				GeneratedDate = DateTime.Now,
				HasOnlyUnusedQuestion = chkAvaliacaoInedita.Checked,
				IsRecordActive = true,
				Type = radMista.Checked ? 'M' : (radObjetiva.Checked ? 'O' : 'D')
			};

			new ExamRepository().Add(exam);
		}

		private void marcaQuestoesComoUsadas()
		{
			foreach (Question item in lstCodigosAleatorios.SelectedItems)
			{
				item.WasUsed = true;
				new QuestionRepository().Update(item);
			}

		}

		private void geraAvaliacao()
		{
			if (!txtTotalQuestoes.Text.Equals("0") && !txtTotalQuestoes.Text.Equals(""))
			{
				geraCodigosAleatorios();
				geraDocumentoWord();
				if (!Mensagem.gerarAvaliacaoNovamente())
				{
					insereHistorico();

					marcaQuestoesComoUsadas();
					recuperaCodigos();
				}
				else
				{
					geraAvaliacao();
				}
			}
			else
			{
				Mensagem.avaliacaoSemQuestoes();
			}
		}
	}
}
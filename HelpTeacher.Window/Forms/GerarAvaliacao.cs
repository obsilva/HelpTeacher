using System;
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
		private ConnectionManager banco = new ConnectionManager();
		private MySql.Data.MySqlClient.MySqlDataReader respostaBanco;
		private BindingSource courseBindingSource = new BindingSource();
		private BindingSource disciplineBindingSource = new BindingSource();
		private BindingSource subjectBindingSource = new BindingSource();
		private string codigoAvaliacao;
		private Label[] lblMateria;
		private NumericUpDown[] numQuantidadeQuestoes;
		private Label[] lblDe;
		private TextBox[] txtQuantidadeMaxima;
		private int countGlob = 0;


		public GerarAvaliacao()
		{
			InitializeComponent();

			atualizaCodigoaAvaliacao();
			preencheComboCursos();
		}

		private void cmbCurso_SelectedIndexChanged(object sender, EventArgs e)
		{
			preencheComboDisciplinas();
			insereComponentes();

			lstCodigos.Items.Clear();
			lstCodigosAleatorios.Items.Clear();
		}

		private void cmbDisciplina_SelectedIndexChanged(object sender, EventArgs e)
		{
			preencheCheckMaterias();
			insereComponentes();

			lstCodigos.Items.Clear();
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

			var numAtivado = sender as NumericUpDown;

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
		private void atualizaCodigoaAvaliacao()
		{
			if (banco.executeComando("SHOW TABLE STATUS LIKE 'htd1'",
					ref respostaBanco))
			{
				respostaBanco.Read();
				codigoAvaliacao = respostaBanco["Auto_increment"].ToString();
				respostaBanco.Close();
				banco.fechaConexao();
			}
		}

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
			lstCodigos.Items.Clear();
			countGlob = 0;

			/* Avaliação Mista */
			if (radMista.Checked)
			{
				if (chkAvaliacaoInedita.Checked)
				{
					foreach (Subject subject in chkMaterias.CheckedItems)
					{
						if (banco.executeComando($"SELECT B1_COD FROM htb1 WHERE B1_MATERIA = {subject.RecordID} AND " +
												 $"B1_USADA IS NULL AND D_E_L_E_T IS NULL ORDER BY B1_COD",
												ref respostaBanco))
						{
							atualizaValorCampos();
						}
					}
				}
				else
				{
					foreach (Subject subject in chkMaterias.CheckedItems)
					{
						if (banco.executeComando($"SELECT B1_COD FROM htb1 WHERE B1_MATERIA = {subject.RecordID} AND " +
												 $"D_E_L_E_T IS NULL ORDER BY B1_COD", ref respostaBanco))
						{
							atualizaValorCampos();
						}
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
						if (banco.executeComando($"SELECT B1_COD FROM htb1 WHERE B1_MATERIA = {subject.RecordID} AND " +
												 $"B1_USADA IS NULL AND B1_OBJETIV IS NULL AND D_E_L_E_T IS NULL " +
												 $"ORDER BY B1_COD", ref respostaBanco))
						{
							atualizaValorCampos();
						}
					}
				}

				else
				{
					foreach (Subject subject in chkMaterias.CheckedItems)
					{
						if (banco.executeComando($"SELECT B1_COD FROM htb1 WHERE B1_MATERIA = {subject.RecordID} AND " +
												 $"B1_OBJETIV IS NULL AND D_E_L_E_T IS NULL ORDER BY B1_COD",
												ref respostaBanco))
						{
							atualizaValorCampos();
						}
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
						if (banco.executeComando($"SELECT B1_COD FROM htb1 WHERE B1_MATERIA = {subject.RecordID} AND " +
												 $"B1_USADA IS NULL AND B1_OBJETIV IS NOT NULL AND D_E_L_E_T IS NULL " +
												 $"ORDER BY B1_COD", ref respostaBanco))
						{
							atualizaValorCampos();
						}
					}
				}
				else
				{
					foreach (Subject subject in chkMaterias.CheckedItems)
					{
						if (banco.executeComando($"SELECT B1_COD FROM htb1 WHERE B1_MATERIA = {subject.RecordID} AND " +
												 $"B1_OBJETIV IS NOT NULL AND D_E_L_E_T IS NULL ORDER BY B1_COD",
												ref respostaBanco))
						{
							atualizaValorCampos();
						}
					}
				}
			}
		}

		private void atualizaValorCampos()
		{
			int numeroDeCodigos = 0;

			if (respostaBanco.HasRows)
			{
				while (respostaBanco.Read())
				{
					lstCodigos.Items.Add(respostaBanco.GetString(0));
					numeroDeCodigos++;
				}
				numQuantidadeQuestoes[countGlob].Maximum =
							numeroDeCodigos;
				numQuantidadeQuestoes[countGlob].Value = 1;
				txtQuantidadeMaxima[countGlob].Text =
							numeroDeCodigos.ToString();
			}
			else
			{
				numQuantidadeQuestoes[countGlob].Maximum = 0;
				txtQuantidadeMaxima[countGlob].Text = "0";
			}
			countGlob++;
			ordenarListBox(lstCodigos);

			respostaBanco.Close();
			banco.fechaConexao();
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
				numQuantidadeQuestoes[count].Minimum = 1;
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

			lstCodigosAleatorios.Items.Clear();
			foreach (Subject subject in chkMaterias.CheckedItems)
			{
				/* Avaliação Mista */
				if (radMista.Checked)
				{
					if (chkAvaliacaoInedita.Checked)
					{
						if (banco.executeComando($"SELECT B1_COD FROM htb1 WHERE B1_MATERIA = {subject.RecordID} AND " +
												 $"B1_USADA IS NULL AND D_E_L_E_T IS NULL ORDER BY RAND() LIMIT " +
												 $"{numQuantidadeQuestoes[count].Value}", ref respostaBanco))
						{
							atualizaCodigosAleatorios();
						}
					}
					else
					{
						if (banco.executeComando($"SELECT B1_COD FROM htb1 WHERE B1_MATERIA = {subject.RecordID} AND " +
												 $"D_E_L_E_T IS NULL ORDER BY RAND() LIMIT " +
												 $"{ numQuantidadeQuestoes[count].Value}", ref respostaBanco))
						{
							atualizaCodigosAleatorios();
						}
					}
				}
				/* Avaliação Dissertativa */
				else if (radDissertativa.Checked)
				{
					if (chkAvaliacaoInedita.Checked)
					{
						if (banco.executeComando($"SELECT B1_COD FROM htb1 WHERE B1_MATERIA = {subject.RecordID} AND " +
												 $"B1_USADA IS NULL AND B1_OBJETIV IS NULL AND D_E_L_E_T IS NULL " +
												 $"ORDER BY RAND() LIMIT {numQuantidadeQuestoes[count].Value}",
							ref respostaBanco))
						{
							atualizaCodigosAleatorios();
						}
					}
					else
					{
						if (banco.executeComando($"SELECT B1_COD FROM htb1 WHERE B1_MATERIA = {subject.RecordID} AND " +
												 $"B1_OBJETIV IS NULL AND D_E_L_E_T IS NULL ORDER BY RAND() " +
												 $"LIMIT {numQuantidadeQuestoes[count].Value}", ref respostaBanco))
						{
							atualizaCodigosAleatorios();
						}
					}
				}
				/* Avaliação Objetiva */
				else
				{
					if (chkAvaliacaoInedita.Checked)
					{
						if (banco.executeComando($"SELECT B1_COD FROM htb1 WHERE B1_MATERIA =  {subject.RecordID} AND " +
												 $"B1_USADA IS NULL AND B1_OBJETIV IS NOT NULL AND D_E_L_E_T IS NULL " +
												 $"ORDER BY RAND() LIMIT {numQuantidadeQuestoes[count].Value}",
								ref respostaBanco))
						{
							atualizaCodigosAleatorios();
						}
					}
					else
					{
						if (banco.executeComando($"SELECT B1_COD FROM htb1 WHERE B1_MATERIA =  {subject.RecordID} AND " +
												 $"B1_OBJETIV IS NOT NULL AND D_E_L_E_T IS NULL ORDER BY RAND() " +
												 $"LIMIT {numQuantidadeQuestoes[count].Value}", ref respostaBanco))
						{
							atualizaCodigosAleatorios();
						}
					}
				}
				count++;
			}
		}

		private void atualizaCodigosAleatorios()
		{
			if (respostaBanco.HasRows)
			{
				while (respostaBanco.Read())
				{
					lstCodigosAleatorios.Items.Add(
							respostaBanco.GetString(0));
				}
			}
			respostaBanco.Close();
			banco.fechaConexao();
		}

		private void ordenarListBox(ListBox listBox)
		{
			string[] itens = listBox.Items.Cast<string>().ToArray();
			listBox.Items.Clear();
			IOrderedEnumerable<string> ordenado = itens.OrderBy(p => Int32.Parse(p));
			foreach (string item in ordenado)
			{
				listBox.Items.Add(item);
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
				if (banco.executeComando("SELECT RTRIM(B1_QUEST), B1_ARQUIVO " +
							"FROM htb1 " +
							"WHERE B1_COD = " + lstCodigosAleatorios.Items[numQuestao],
								ref respostaBanco))
				{
					if (respostaBanco.HasRows)
					{
						respostaBanco.Read();

						/* Questão */
						wordRange = document.Bookmarks.get_Item(ref fimDocumento).Range;
						wordRange.InsertParagraphAfter();
						wordRange.InsertAfter((numQuestao + 1) + ") " +
								respostaBanco.GetString(0) + Environment.NewLine);

						/* Arquivos */
						if (!Convert.IsDBNull(respostaBanco["B1_ARQUIVO"]))
						{
							string[] nomeArquivo = respostaBanco.GetString(1).
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
					respostaBanco.Close();
					banco.fechaConexao();
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

		private bool insereHistorico()
		{
			string data = DateTime.Now.ToShortDateString();
			string codigosQuestoes = lstCodigosAleatorios.Items[0].ToString();
			string codigosMateria = "";


			for (int cont = 1; cont < lstCodigosAleatorios.Items.Count; cont++)
			{
				codigosQuestoes += String.Concat(", ", lstCodigosAleatorios.Items[cont].ToString());
			}

			foreach (Subject subject in chkMaterias.CheckedItems)
			{
				codigosMateria = String.Concat(codigosMateria, $"{subject.RecordID}, ");
			}
			codigosMateria = codigosMateria.Substring(0, (codigosMateria.Length - 2));

			if (banco.executeComando("INSERT INTO htd1 VALUES (NULL, 'M', NULL, '" + codigosQuestoes + "', '" +
						codigosMateria + "', '" + data + "', NULL)"))
			{
				if (radMista.Checked && !chkAvaliacaoInedita.Checked)
				{
					return true;
				}

				if (chkAvaliacaoInedita.Checked)
				{
					if (!banco.executeComando("UPDATE htd1 SET D1_INEDITA = '*' WHERE D1_COD = " + codigoAvaliacao))
					{
						return false;
					}
				}

				if (radObjetiva.Checked)
				{
					if (!banco.executeComando("UPDATE htd1 SET D1_TIPO = 'O' WHERE D1_COD = " + codigoAvaliacao))
					{
						return false;
					}
				}
				else if (radDissertativa.Checked)
				{
					if (!banco.executeComando("UPDATE htd1 SET D1_TIPO = 'D' WHERE D1_COD = " + codigoAvaliacao))
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		private bool marcaQuestoesComoUsadas()
		{
			foreach (string questao in lstCodigosAleatorios.Items)
			{
				if (!banco.executeComando("UPDATE htb1 SET B1_USADA = '*' WHERE B1_COD = " + questao))
				{
					return false;
				}
			}
			return true;
		}

		private void geraAvaliacao()
		{
			if (!txtTotalQuestoes.Text.Equals("0") && !txtTotalQuestoes.Text.Equals(""))
			{
				geraCodigosAleatorios();
				geraDocumentoWord();
				if (!Mensagem.gerarAvaliacaoNovamente())
				{
					if (insereHistorico())
					{
						if (marcaQuestoesComoUsadas())
						{
							recuperaCodigos();
							atualizaCodigoaAvaliacao();
						}
						else
						{
							Mensagem.erroAlteracao();
						}
					}
					else
					{
						Mensagem.erroCadastro();
					}
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
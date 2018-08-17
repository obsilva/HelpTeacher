using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using HelpTeacher.Classes;
using Word = Microsoft.Office.Interop.Word;

namespace HelpTeacher.Forms
{
    public partial class GerarAvaliacao : Form
    {
        private ConexaoBanco banco = new ConexaoBanco();
        private MySql.Data.MySqlClient.MySqlDataReader respostaBanco;
        private String codigoAvaliacao;
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

            NumericUpDown numAtivado = sender as NumericUpDown;

            for (int count = 0; count < lblMateria.Length; count++)
            {
                somatoria += (int)this.numQuantidadeQuestoes[count].Value;
            }
            txtTotalQuestoes.Text = somatoria.ToString();
        }

        private void chkAvaUnica_CheckedChanged(object sender, EventArgs e)
        {
            recuperaCodigos();
        }

        private void rbObjetiva_CheckedChanged(object sender, EventArgs e)
        {
            recuperaCodigos();
        }

        private void rbDissertativa_CheckedChanged(object sender, EventArgs e)
        {
            recuperaCodigos();
        }

        private void bntGerarAvaliacao_Click(object sender, EventArgs e)
        {
            geraAvaliacao();
        }

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
            if (banco.executeComando("SELECT C1_COD, C1_NOME " +
                                    "FROM htc1 " +
                                    "WHERE D_E_L_E_T IS NULL", ref respostaBanco))
            {
                if (respostaBanco.HasRows)
                {
                    cmbCurso.Items.Clear();
                    while (respostaBanco.Read())
                    {
                        cmbCurso.Items.Add("(" + respostaBanco.GetString(0) + ") " + respostaBanco.GetString(1));
                    }
                    cmbCurso.SelectedIndex = 0;
                }
                respostaBanco.Close();
                banco.fechaConexao();
            }
        }

        private void preencheComboDisciplinas()
        {
            if (!cmbCurso.Text.Equals(""))
            {
                String[] curso = cmbCurso.Text.Split(new char[] { '(', ')', ' ' },
                                 StringSplitOptions.RemoveEmptyEntries);

                if (banco.executeComando("SELECT C2_COD, C2_NOME " +
                                        "FROM htc2 " +
                                        "WHERE D_E_L_E_T IS NULL " +
                                            "and C2_CURSO = " + Convert.ToInt32(curso[0]), ref respostaBanco))
                {
                    cmbDisciplina.Items.Clear();
                    if (respostaBanco.HasRows)
                    {
                        while (respostaBanco.Read())
                        {
                            cmbDisciplina.Items.Add("(" + respostaBanco.GetString(0) + ") " + respostaBanco.GetString(1));
                        }
                        cmbDisciplina.SelectedIndex = 0;
                    }
                    respostaBanco.Close();
                    banco.fechaConexao();
                }
            }
        }

        private void preencheCheckMaterias()
        {
            String[] codigo = cmbDisciplina.Text.Split(new char[] { '(', ')', ' ' },
                            StringSplitOptions.RemoveEmptyEntries);

            chkMaterias.Items.Clear();
            if (banco.executeComando("SELECT C3_COD, C3_NOME " +
                    "FROM htc3 " +
                        "INNER JOIN htb1 " +
                            "ON C3_COD = B1_MATERIA " +
                    "WHERE C3_DISCIPL = " + Convert.ToInt32(codigo[0]) +
                        " AND htc3.D_E_L_E_T IS NULL " +
                    "GROUP BY C3_COD", ref respostaBanco))
            {
                if (respostaBanco.HasRows)
                {
                    while (respostaBanco.Read())
                    {
                        chkMaterias.Items.Add("(" + respostaBanco.GetString(0) + ") " + respostaBanco.GetString(1));
                    }
                }
                respostaBanco.Close();
                banco.fechaConexao();
            }
        }

        private void recuperaCodigos()
        {
            lstCodigos.Items.Clear();
            this.countGlob = 0;

            /* Avaliação Mista */
            if (radMista.Checked)
            {
                if (chkAvaliacaoInedita.Checked)
                {
                    foreach (String materia in chkMaterias.CheckedItems)
                    {
                        String[] codigo = materia.Split(new char[] { '(', ')', ' ' },
                                    StringSplitOptions.RemoveEmptyEntries);
                        if (banco.executeComando("SELECT B1_COD " +
                                    "FROM htb1 " +
                                    "WHERE B1_MATERIA = " + codigo[0] + " AND " +
                                        "B1_USADA IS NULL AND " +
                                        "D_E_L_E_T IS NULL " +
                                    "ORDER BY B1_COD", ref respostaBanco))
                        {
                            atualizaValorCampos();
                        }
                    }
                }
                else
                {
                    foreach (String materia in chkMaterias.CheckedItems)
                    {
                        String[] codigo = materia.Split(new char[] { '(', ')', ' ' },
                                    StringSplitOptions.RemoveEmptyEntries);
                        if (banco.executeComando("SELECT B1_COD " +
                                    "FROM htb1 " +
                                    "WHERE B1_MATERIA = " + codigo[0] + " AND " +
                                        "D_E_L_E_T IS NULL " +
                                    "ORDER BY B1_COD", ref respostaBanco))
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
                    foreach (String materia in chkMaterias.CheckedItems)
                    {
                        String[] codigo = materia.Split(new char[] { '(', ')', ' ' },
                                    StringSplitOptions.RemoveEmptyEntries);
                        if (banco.executeComando("SELECT B1_COD " +
                                    "FROM htb1 " +
                                    "WHERE B1_MATERIA = " + codigo[0] + " AND " +
                                        "B1_USADA IS NULL AND " +
                                        "B1_OBJETIV IS NULL AND " +
                                        "D_E_L_E_T IS NULL " +
                                    "ORDER BY B1_COD", ref respostaBanco))
                        {
                            atualizaValorCampos();
                        }
                    }
                }

                else
                {
                    foreach (String materia in chkMaterias.CheckedItems)
                    {
                        String[] codigo = materia.Split(new char[] { '(', ')', ' ' },
                                    StringSplitOptions.RemoveEmptyEntries);
                        if (banco.executeComando("SELECT B1_COD " +
                                    "FROM htb1 " +
                                    "WHERE B1_MATERIA = " + codigo[0] + " AND " +
                                        "B1_OBJETIV IS NULL AND " +
                                        "D_E_L_E_T IS NULL " +
                                    "ORDER BY B1_COD", ref respostaBanco))
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
                    foreach (String materia in chkMaterias.CheckedItems)
                    {
                        String[] codigo = materia.Split(new char[] { '(', ')', ' ' },
                                    StringSplitOptions.RemoveEmptyEntries);
                        if (banco.executeComando("SELECT B1_COD " +
                                    "FROM htb1 " +
                                    "WHERE B1_MATERIA = " + codigo[0] + " AND " +
                                        "B1_USADA IS NULL AND " +
                                        "B1_OBJETIV IS NOT NULL AND " +
                                        "D_E_L_E_T IS NULL " +
                                    "ORDER BY B1_COD", ref respostaBanco))
                        {
                            atualizaValorCampos();
                        }
                    }
                }
                else
                {
                    foreach (String materia in chkMaterias.CheckedItems)
                    {
                        String[] codigo = materia.Split(new char[] { '(', ')', ' ' },
                                    StringSplitOptions.RemoveEmptyEntries);
                        if (banco.executeComando("SELECT B1_COD " +
                                    "FROM htb1 " +
                                    "WHERE B1_MATERIA = " + codigo[0] + " AND " +
                                        "B1_OBJETIV IS NOT NULL AND " +
                                        "D_E_L_E_T IS NULL " +
                                    "ORDER BY B1_COD", ref respostaBanco))
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
                numQuantidadeQuestoes[this.countGlob].Maximum =
                            numeroDeCodigos;
                numQuantidadeQuestoes[this.countGlob].Value = 1;
                txtQuantidadeMaxima[this.countGlob].Text =
                            numeroDeCodigos.ToString();
            }
            else
            {
                numQuantidadeQuestoes[this.countGlob].Maximum = 0;
                txtQuantidadeMaxima[this.countGlob].Text = "0";
            }
            this.countGlob++;
            ordenarListBox(lstCodigos);

            respostaBanco.Close();
            banco.fechaConexao();
        }

        private void insereComponentes()
        {
            if (lblMateria != null)
                for (int count = 0; count < lblMateria.Length; count++)
                {
                    this.lblMateria[count].Dispose();
                    this.numQuantidadeQuestoes[count].Dispose();
                    this.lblDe[count].Dispose();
                    this.txtQuantidadeMaxima[count].Dispose();
                }

            this.lblMateria = new Label[chkMaterias.CheckedItems.Count];
            this.numQuantidadeQuestoes = new NumericUpDown[chkMaterias.CheckedItems.Count];
            this.lblDe = new Label[chkMaterias.CheckedItems.Count];
            this.txtQuantidadeMaxima = new TextBox[chkMaterias.CheckedItems.Count];
            int xInicial = 185;
            int yInicial = 300;
            int x;
            int y;

            txtTotalQuestoes.Text = chkMaterias.CheckedItems.Count.ToString();
            if (chkMaterias.CheckedItems.Count < 9)
            {
                yInicial -= (chkMaterias.CheckedItems.Count * 16);
                this.Size = new Size(798, 483);
            }
            else
            {
                int aumentoTamanho = (chkMaterias.CheckedItems.Count - 8) * 33;
                int localizacao = aumentoTamanho / 2;
                yInicial -= (8 * 16);

                this.Height = 483 + aumentoTamanho;
                this.label3.Top = 176 + localizacao;
                this.lstCodigos.Top = 193 + localizacao;
                this.label5.Top = 266 + localizacao;
                this.lstCodigosAleatorios.Top = 283 + localizacao;
                this.bntGerarAvaliacao.Top = 361 + localizacao;
            }

            for (int count = 0; count < chkMaterias.CheckedItems.Count; count++)
            {
                String[] materia = chkMaterias.CheckedItems[count].ToString().Split(
                            new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                this.lblMateria[count] = new Label();
                this.numQuantidadeQuestoes[count] = new NumericUpDown();
                this.lblDe[count] = new Label();
                this.txtQuantidadeMaxima[count] = new TextBox();
                // 
                // Label matéria
                //
                y = yInicial + (count * 33);
                this.lblMateria[count].AutoSize = false;
                this.lblMateria[count].Location = new System.Drawing.Point(xInicial, (y + 3));
                this.lblMateria[count].Size = new System.Drawing.Size(409, 14);
                this.lblMateria[count].Text = "Quantidade de questões  de" + materia[1];
                // 
                // NumericUpDown
                //
                x = xInicial + 6 + lblMateria[count].Width;
                this.numQuantidadeQuestoes[count].Location = new System.Drawing.Point(x, y);
                this.numQuantidadeQuestoes[count].Size = new System.Drawing.Size(65, 22);
                this.numQuantidadeQuestoes[count].Minimum = 1;
                this.numQuantidadeQuestoes[count].ValueChanged += new System.EventHandler(
                            this.questoesMaterias_ValueChanged);
                // 
                // Label de
                //
                x = numQuantidadeQuestoes[count].Location.X + 71;
                this.lblDe[count].AutoSize = false;
                this.lblDe[count].Location = new System.Drawing.Point(x, (y + 3));
                this.lblDe[count].Size = new System.Drawing.Size(23, 14);
                this.lblDe[count].Text = "de";
                // 
                // TextBox
                //
                x = lblDe[count].Location.X + 29;
                this.txtQuantidadeMaxima[count].Location = new System.Drawing.Point(x, y);
                this.txtQuantidadeMaxima[count].Size = new System.Drawing.Size(65, 22);
                this.txtQuantidadeMaxima[count].Text = "0";
                this.txtQuantidadeMaxima[count].ReadOnly = true;

                this.Controls.Add(this.lblMateria[count]);
                this.Controls.Add(this.numQuantidadeQuestoes[count]);
                this.Controls.Add(this.lblDe[count]);
                this.Controls.Add(this.txtQuantidadeMaxima[count]);
            }
        }

        private void geraCodigosAleatorios()
        {
            int count = 0;

            lstCodigosAleatorios.Items.Clear();
            foreach (String materia in chkMaterias.CheckedItems)
            {
                String[] codigoMateria = materia.Split(new char[] { '(', ')', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                 /* Avaliação Mista */
                if (radMista.Checked)
                {
                    if (chkAvaliacaoInedita.Checked)
                    {
                        if (banco.executeComando("SELECT B1_COD " +
                                "FROM htb1 " +
                                "WHERE B1_MATERIA = " + codigoMateria[0] + " AND " +
                                    "B1_USADA IS NULL AND " +
                                    "D_E_L_E_T IS NULL " +
                                "ORDER BY RAND() " +
                                "LIMIT " + numQuantidadeQuestoes[count].Value,
                                ref respostaBanco))
                        {
                            atualizaCodigosAleatorios();
                        }
                    }
                    else
                    {
                        if (banco.executeComando("SELECT B1_COD " +
                                "FROM htb1 " +
                                "WHERE B1_MATERIA = " + codigoMateria[0] + " AND " +
                                    "D_E_L_E_T IS NULL " +
                                "ORDER BY RAND() " +
                                "LIMIT " + numQuantidadeQuestoes[count].Value,
                                ref respostaBanco))
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
                        if (banco.executeComando("SELECT B1_COD " +
                                "FROM htb1 " +
                                "WHERE B1_MATERIA = " + codigoMateria[0] + " AND " +
                                    "B1_USADA IS NULL AND " +
                                    "B1_OBJETIV IS NULL AND " +
                                    "D_E_L_E_T IS NULL " +
                                "ORDER BY RAND() " +
                                "LIMIT " + numQuantidadeQuestoes[count].Value,
                                ref respostaBanco))
                        {
                            atualizaCodigosAleatorios();
                        }
                    }
                    else
                    {
                        if (banco.executeComando("SELECT B1_COD " +
                                "FROM htb1 " +
                                "WHERE B1_MATERIA = " + codigoMateria[0] + " AND " +
                                    "B1_OBJETIV IS NULL AND " +
                                    "D_E_L_E_T IS NULL " +
                                "ORDER BY RAND() " +
                                "LIMIT " + numQuantidadeQuestoes[count].Value,
                                ref respostaBanco))
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
                        if (banco.executeComando("SELECT B1_COD " +
                                "FROM htb1 " +
                                "WHERE B1_MATERIA = " + codigoMateria[0] + " AND " +
                                    "B1_USADA IS NULL AND " +
                                    "B1_OBJETIV IS NOT NULL AND " +
                                    "D_E_L_E_T IS NULL " +
                                "ORDER BY RAND() " +
                                "LIMIT " + numQuantidadeQuestoes[count].Value,
                                ref respostaBanco))
                        {
                            atualizaCodigosAleatorios();
                        }
                    }
                    else
                    {
                        if (banco.executeComando("SELECT B1_COD " +
                                "FROM htb1 " +
                                "WHERE B1_MATERIA = " + codigoMateria[0] + " AND " +
                                    "B1_OBJETIV IS NOT NULL AND " +
                                    "D_E_L_E_T IS NULL " +
                                "ORDER BY RAND() " +
                                "LIMIT " + numQuantidadeQuestoes[count].Value,
                                ref respostaBanco))
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
            String[] itens = listBox.Items.Cast<string>().ToArray();
            listBox.Items.Clear();
            var ordenado = itens.OrderBy(p => int.Parse(p));
            foreach (var item in ordenado)
            {
                listBox.Items.Add(item.ToString());
            }
        }

        private void geraDocumentoWord()
        {
            Object fimDocumento = "\\endofdoc";
            Word.Range wordRange;
            Object range;
            int numQuestao = 0;

            /* Abre a aplicação word e faz uma cópia do documento mapeado */
            Object template = Path.GetFullPath(@"..\modelo.docx");
            Word.Application appWord = new Word.Application();
            appWord.Visible = true;

            Word.Document document = appWord.Documents.Add(ref template, true);
            document.Paragraphs.Alignment = Word.WdParagraphAlignment.
                    wdAlignParagraphJustify;

            String[] disciplina = cmbDisciplina.Text.Split(new char[] { '(', ')' },
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
                            String[] nomeArquivo = respostaBanco.GetString(1).
                                    Split(new char[] { ',', ' ' },
                                    StringSplitOptions.RemoveEmptyEntries);
                            String[] arquivo = Directory.GetFiles(@"..\_files\",
                                    nomeArquivo[0] + ".*");
                            String caminhoImagem = Path.GetFullPath(arquivo[0]);
                            String extensao = Path.GetExtension(arquivo[0]);

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

        private void substituiTagsWord(Word.Document document, Object parametro, Object texto)
        {
            Object missing = System.Reflection.Missing.Value;
            Word.Range rng = document.Range(ref missing, ref missing);
            Object FindText = parametro;
            Object ReplaceWith = texto;
            Object MatchWholeWord = true;
            Object Forward = false;

            rng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing,
                    ref missing, ref missing, ref Forward, ref missing, ref missing,
                    ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);
        }

        private Boolean insereHistorico()
        {
            String data = DateTime.Now.ToShortDateString();
            String codigosQuestoes = lstCodigosAleatorios.Items[0].ToString();
            String codigosMateria = "";


            for (int cont = 1; cont < lstCodigosAleatorios.Items.Count; cont++)
            {
                codigosQuestoes += String.Concat(", ",
                        lstCodigosAleatorios.Items[cont].ToString());
            }

            foreach (String materia in chkMaterias.CheckedItems)
            {
                String[] codigoTemp = materia.Split(new char[] { '(', 
                        ')', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                codigosMateria = String.Concat(codigosMateria, codigoTemp[0] + ", ");
            }
            codigosMateria = codigosMateria.Substring(0, (codigosMateria.Length - 2));

            if (banco.executeComando("INSERT INTO htd1 VALUES (NULL, " +
                        "'M', NULL, '" + codigosQuestoes + "', '" +
                        codigosMateria + "', '" + data + "', NULL)"))
            {
                if (radMista.Checked && !chkAvaliacaoInedita.Checked)
                    return true;

                if (chkAvaliacaoInedita.Checked)
                {
                    if (!banco.executeComando("UPDATE htd1 SET D1_INEDITA = '*' " +
                            "WHERE D1_COD = " + codigoAvaliacao))
                        return false;
                }

                if (radObjetiva.Checked)
                {
                    if (!banco.executeComando("UPDATE htd1 SET D1_TIPO = 'O' " +
                            "WHERE D1_COD = " + codigoAvaliacao))
                        return false;
                }
                else if (radDissertativa.Checked)
                {
                    if (!banco.executeComando("UPDATE htd1 SET D1_TIPO = 'D' " +
                            "WHERE D1_COD = " + codigoAvaliacao))
                        return false;
                }
                return true;
            }
            return false;
        }

        private Boolean marcaQuestoesComoUsadas()
        {
            foreach (String questao in lstCodigosAleatorios.Items)
            {
                if (!banco.executeComando("UPDATE htb1 SET B1_USADA = '*' " +
                        "WHERE B1_COD = " + questao))
                    return false;
            }
            return true;
        }

        private void geraAvaliacao()
        {
            if (!txtTotalQuestoes.Text.Equals("0") &&
                    !txtTotalQuestoes.Text.Equals(""))
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
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
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace HelpTeacher.Forms
{
    public partial class GerarAvaliacao : Form
    {
        private ConexaoBanco banco = new ConexaoBanco();
        private MySql.Data.MySqlClient.MySqlDataReader respostaBanco;
        private MySql.Data.MySqlClient.MySqlDataReader respostaBanco2;
        private String codigoAvaliacao;
        private Label[] lblMateria;
        private NumericUpDown[] numQuantidadeQuestoes;
        private Label[] lblDe;
        private TextBox[] txtQuantidadeMaxima;
        private int countGlob = 0;
        private int contadorAvaliacao = 0;


        public GerarAvaliacao()
        {
            InitializeComponent();
            atualizaCodigoaAvaliacao();
            preencheCampos();
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
            if (tbProfessor.Text == "")
            {
                Mensagem.campoEmBranco();
                tbProfessor.Focus();
            }
               
            else if (tbEscola.Text == "")
            {
                Mensagem.campoEmBranco();
                tbEscola.Focus();
            }
                
            else
            {
                salvaCampos();
                geraAvaliacao();
            }               
        }

        // FUNÇÕES  FUNÇÕES  FUNÇÕES  FUNÇÕES  FUNÇÕES  FUNÇÕES  FUNÇÕES  FUNÇÕES  FUNÇÕES  FUNÇÕES  FUNÇÕES  //
        // FUNÇÕES  FUNÇÕES  FUNÇÕES  FUNÇÕES  FUNÇÕES  FUNÇÕES  FUNÇÕES  FUNÇÕES  FUNÇÕES  FUNÇÕES  FUNÇÕES  //
      
        private void salvaCampos()
        {        
            //SALVA NOME PROFESSOR
            if(tbProfessor.Text != "")
            {
                String arquivoProf = Path.GetFullPath(@"..\Arquivos\professor.txt");
                String newFile = Path.GetFullPath(@"..\Arquivos\professor2.txt");
                if (File.Exists(arquivoProf))
                {
                    String line;
                    StreamReader rd = new StreamReader(arquivoProf);
                    StreamWriter wr = new StreamWriter(newFile);
                    wr.WriteLine(tbProfessor.Text);
                    //Lê o próximo caractere sem alterar o estado do leitor
                    while (rd.Peek() != -1)
                    {
                        line = rd.ReadLine();
                        if (line != tbProfessor.Text)
                            wr.WriteLine(line);
                    }
                    rd.Close();
                    wr.Close();
                    File.Delete(arquivoProf);
                    File.Move(newFile, arquivoProf);
                }
                else
                {
                    StreamWriter wr = new StreamWriter(arquivoProf);
                    wr.WriteLine(tbProfessor.Text);
                    wr.Close();
                }
            }
           

            //SALVA NOME ESCOLA
            if(tbEscola.Text != "")
            {
                String arquivoEscola = Path.GetFullPath(@"..\Arquivos\escola.txt");
                String newFileEscola = Path.GetFullPath(@"..\Arquivos\escola2.txt");
                if (File.Exists(arquivoEscola))
                {
                    String line;
                    StreamReader rd = new StreamReader(arquivoEscola);
                    StreamWriter wr = new StreamWriter(newFileEscola);
                    wr.WriteLine(tbEscola.Text);
                    //Lê o próximo caractere sem alterar o estado do leitor
                    while (rd.Peek() != -1)
                    {
                        line = rd.ReadLine();
                        if (line != tbEscola.Text)
                            wr.WriteLine(line);
                    }
                    rd.Close();
                    wr.Close();
                    File.Delete(arquivoEscola);
                    File.Move(newFileEscola, arquivoEscola);
                }
                else
                {
                    StreamWriter wr = new StreamWriter(arquivoEscola);
                    wr.WriteLine(tbEscola.Text);
                    wr.Close();
                }
            }            
           
            //SALVA NOME ICONE
            if(pictIcone.Image != null && pictIcone.ImageLocation != null)
            {
                String caminho = Path.GetFullPath(@"..\Arquivos\icone.txt");
                String nomeIcone = Path.GetFileName(pictIcone.ImageLocation);
                StreamWriter wr = new StreamWriter(caminho);
                wr.WriteLine(nomeIcone);
                wr.Close();                             
            }

            //SALVA VALOR DA AVALIÇÃO
            if(tbValorAvaliacao.Text != "")
            {
                String arquivoValor = Path.GetFullPath(@"..\Arquivos\valor.txt");
                String newFileValor = Path.GetFullPath(@"..\Arquivos\valor2.txt");
                if (File.Exists(arquivoValor))
                {
                    String line;
                    StreamReader rd = new StreamReader(arquivoValor);
                    StreamWriter wr = new StreamWriter(newFileValor);
                    wr.WriteLine(tbValorAvaliacao.Text);
                    //Lê o próximo caractere sem alterar o estado do leitor
                    while (rd.Peek() != -1)
                    {
                        line = rd.ReadLine();
                        if (line != tbValorAvaliacao.Text)
                            wr.WriteLine(line);
                    }
                    rd.Close();
                    wr.Close();
                    File.Delete(arquivoValor);
                    File.Move(newFileValor, arquivoValor);
                }
                else
                {
                    StreamWriter wr = new StreamWriter(arquivoValor);
                    wr.WriteLine(tbValorAvaliacao.Text);
                    wr.Close();
                }
            }            
        }

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
                                    "FROM htc1", ref respostaBanco))
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
                                            "WHERE C2_CURSO = " + Convert.ToInt32(curso[0]), ref respostaBanco))
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
                        "INNER JOIN htb3 " +
                            "ON C3_COD = B3_MATERIA " +
                            "AND B3_DELETED = '0' " +
                    "WHERE C3_DISCIP = " + Convert.ToInt32(codigo[0]) +                        
                    " GROUP BY C3_COD", ref respostaBanco))
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

        private void preencheCampos()
        {
            //ULTIMO ICONE USADO
            String caminho = Path.GetFullPath(@"..\Arquivos\icone.txt");
            if (File.Exists(caminho))
            {
                String nome;
                String caminhoImagem;
                StreamReader rd = new StreamReader(caminho);
                nome = rd.ReadLine();
                rd.Close();
                caminhoImagem = Path.GetFullPath(@"..\Modelos\iconesProvas\" + nome);
                pictIcone.ImageLocation = caminhoImagem;               
            }      

            //ULTIMO PROFESSOR
            String caminhoProf = Path.GetFullPath(@"..\Arquivos\professor.txt");
            if (File.Exists(caminhoProf))
            {
                String nome;                
                StreamReader rd = new StreamReader(caminhoProf);
                nome = rd.ReadLine();
                rd.Close();
                tbProfessor.Text = nome;                
            }        

            //ULTIMA ESCOLA
            String caminhoEscola = Path.GetFullPath(@"..\Arquivos\escola.txt");
            if (File.Exists(caminhoEscola))
            {
                String nome;
                StreamReader rd = new StreamReader(caminhoEscola);
                nome = rd.ReadLine();
                rd.Close();
                tbEscola.Text = nome;
            } 

            //ULTIMO VALOR
            String caminhoValor = Path.GetFullPath(@"..\Arquivos\valor.txt");
            if (File.Exists(caminhoValor))
            {
                String vlr;
                StreamReader rd = new StreamReader(caminhoValor);
                vlr = rd.ReadLine();
                rd.Close();
                tbValorAvaliacao.Text = vlr;
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
                                    "INNER JOIN htb3 ON B1_COD = B3_QUESTAO " +
                                    "WHERE B3_MATERIA = " + codigo[0] + " AND " +
                                        "B3_USADA = '0' AND " +
                                        "B3_DELETED = '0' " +
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
                                   "FROM htb3 " +
                                   "INNER JOIN htb1 ON B1_COD = B3_QUESTAO " +
                                   "WHERE B3_MATERIA = " + codigo[0] + " AND " +                                      
                                       "B3_DELETED = '0' " +
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
                                    "INNER JOIN htb3 ON B1_COD = B3_QUESTAO " +
                                    "AND B1_OBJETIV = '0' " +
                                    "WHERE B3_MATERIA = " + codigo[0] + " AND " +
                                        "B3_USADA = '0' AND " +                                        
                                        "B3_DELETED = '0' " +
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
                                        "INNER JOIN htb3 ON B1_COD = B3_QUESTAO AND " +
                                        "B1_OBJETIV = '0' " +
                                     "WHERE B3_MATERIA = " + codigo[0] + " AND " +                                        
                                        "B3_DELETED = '0' " +
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
                                    "INNER JOIN htb3 ON B1_COD = B3_QUESTAO " +
                                    "AND B1_OBJETIV = '1' " +
                                    "WHERE B3_MATERIA = " + codigo[0] + " AND " +
                                        "B3_USADA = '0' AND " +
                                        "B3_DELETED = '0' " +
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
                                        "INNER JOIN htb3 ON B1_COD = B3_QUESTAO " +
                                         "AND B1_OBJETIV = '1' " +
                                     "WHERE B3_MATERIA = " + codigo[0] + " AND " +
                                        "B3_DELETED = '0' " +
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
            int xInicial = 184;
            int yInicial = 330;
            int x;
            int y;

            txtTotalQuestoes.Text = chkMaterias.CheckedItems.Count.ToString();
            if (chkMaterias.CheckedItems.Count < 9)
            {
                yInicial -= (chkMaterias.CheckedItems.Count * 16);
                this.Size = new Size(1208, 631);
            }
            else
            {
                int aumentoTamanho = (chkMaterias.CheckedItems.Count - 8) * 33;
                int localizacao = aumentoTamanho / 2;
                yInicial -= (8 * 16);

                this.Height = 483 + aumentoTamanho;                
                this.lstCodigos.Top = 193 + localizacao;               
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
                this.lblMateria[count].Size = new System.Drawing.Size(160, 14);
                this.lblMateria[count].Text =  materia[1];
                // 
                // NumericUpDown
                //
                x = xInicial + lblMateria[count].Width;
                this.numQuantidadeQuestoes[count].Location = new System.Drawing.Point(x, y);
                this.numQuantidadeQuestoes[count].Size = new System.Drawing.Size(45, 22);
                this.numQuantidadeQuestoes[count].Minimum = 1;
                this.numQuantidadeQuestoes[count].ValueChanged += new System.EventHandler(
                            this.questoesMaterias_ValueChanged);
                // 
                // Label de
                //
                x = numQuantidadeQuestoes[count].Location.X + 50;
                this.lblDe[count].AutoSize = false;
                this.lblDe[count].Location = new System.Drawing.Point(x, (y + 3));
                this.lblDe[count].Size = new System.Drawing.Size(23, 14);
                this.lblDe[count].Text = "de";
                // 
                // TextBox
                //
                x = lblDe[count].Location.X + 29;
                this.txtQuantidadeMaxima[count].Location = new System.Drawing.Point(x, y);
                this.txtQuantidadeMaxima[count].Size = new System.Drawing.Size(35, 22);
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
                                "INNER JOIN htb3 "  +
                                    "ON B1_COD = B3_QUESTAO " +
                                "WHERE B3_MATERIA = " + codigoMateria[0] + " AND " +
                                    "B3_USADA = '0' AND " +
                                    "B3_DELETED = '0' " +
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
                                "INNER JOIN htb3 " +
                                    "ON B1_COD = B3_QUESTAO " +
                                "WHERE B3_MATERIA = " + codigoMateria[0] + " AND " +
                                    "B3_DELETED = '0' " +
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
                                "INNER JOIN htb3 " +
                                    "ON B1_COD = B3_QUESTAO " +
                                    "AND B1_OBJETIV = '0' " +
                                "WHERE B3_MATERIA = " + codigoMateria[0] + " AND " +
                                    "B3_USADA = '0' AND " +                                   
                                    "B3_DELETED = '0' " +
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
                                 "INNER JOIN htb3 " +
                                     "ON B1_COD = B3_QUESTAO " +
                                     "AND B1_OBJETIV = '0' " +
                                 "WHERE B3_MATERIA = " + codigoMateria[0] + " AND " +                                     
                                     "B3_DELETED = '0' " +
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
                                "INNER JOIN htb3 " +
                                    "ON B1_COD = B3_QUESTAO " +
                                    "AND B1_OBJETIV = '1' " +
                                "WHERE B3_MATERIA = " + codigoMateria[0] + " AND " +
                                    "B3_USADA = '0' AND " +
                                    "B3_DELETED = '0' " +
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
                                "INNER JOIN htb3 " +
                                    "ON B1_COD = B3_QUESTAO " +
                                    "AND B1_OBJETIV = '1' " +
                                "WHERE B3_MATERIA = " + codigoMateria[0] + " AND " +
                                    "B3_DELETED = '0' " +
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

        private String modeloSelecionado(String tipo)
        {
            String modelo = "";
            if(radModelo1.Checked)
                modelo = Path.GetFullPath(@"..\Modelos\modelo1." + tipo);
            else if (radModelo2.Checked)
                modelo = Path.GetFullPath(@"..\Modelos\modelo2." + tipo);
            else if (radModelo3.Checked)
                modelo = Path.GetFullPath(@"..\Modelos\modelo3." + tipo);
            else if (radModelo4.Checked)
                modelo = Path.GetFullPath(@"..\Modelos\modelo4." + tipo);

            return modelo;
        }

        private void geraPDF()
        {
            int numQuestao = 0;
            var materias = new StringBuilder();
            materias = nomesMaterias();
            String documentos = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            String pastaHistorico = documentos + "\\HelpTeacher\\Historico\\";

            String[] disciplina = cmbDisciplina.Text.Split(new char[] { '(', ')' },
                    StringSplitOptions.RemoveEmptyEntries);

            String input = modeloSelecionado("pdf");
            String newFile = Path.GetFullPath(pastaHistorico + disciplina[1] + '-' + materias + (contadorAvaliacao++) + ".pdf");

            // open the reader 
            PdfReader reader = new PdfReader(input);
            iTextSharp.text.Rectangle size = reader.GetPageSizeWithRotation(1);
            Document document = new Document(size);
            // open the writer           
            FileStream fs = new FileStream(newFile, FileMode.Create, FileAccess.Write);
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            document.Open();
            PdfContentByte cb = writer.DirectContent;
            //Seta fonte e tamanho
            BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false, false);
            cb.SetFontAndSize(bf, 16);
            cb.BeginText();

            if (radModelo4.Checked)
            {
                String[] curso = cmbCurso.Text.Split(new char[] { ')' },
                    StringSplitOptions.RemoveEmptyEntries);

                cb.ShowTextAligned(Element.ALIGN_CENTER, tbEscola.Text, 290, 780, 0);
                cb.ShowTextAligned(Element.ALIGN_CENTER, curso[1], 290, 760, 0);
            }
            else
            {
                cb.ShowTextAligned(Element.ALIGN_CENTER, tbEscola.Text, 290, 780, 0);
                cb.ShowTextAligned(Element.ALIGN_CENTER, "Avaliação de " + disciplina[1], 290, 760, 0);
            }

            cb.EndText();

            //insere icone
            var icone = iTextSharp.text.Image.GetInstance(pictIcone.ImageLocation);
            icone.ScaleToFit(70, 60); //tamanho da imagem
            icone.SetAbsolutePosition(30, 760);     //posicao da imagem 
            document.Add(icone);

            // create the new page and add it to the pdf 
            PdfImportedPage page = writer.GetImportedPage(reader, 1);
            cb.AddTemplate(page, 0, 0);

            //Propriedades do Parágrafo
            cb.SetFontAndSize(bf, 14);
            var paragraph = new iTextSharp.text.Paragraph();
            paragraph.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
             

            /* Busca as questões no banco e insere no Word */
            while (numQuestao < Convert.ToInt32(txtTotalQuestoes.Text))
            {
                if (banco.executeComando("SELECT RTRIM(B1_QUESTAO), B2_ARQUIVO " +
                            "FROM htb1 " +
                            "LEFT JOIN htb2 ON B2_QUESTAO = B1_COD " +
                            "WHERE B1_COD = " + lstCodigosAleatorios.Items[numQuestao],
                                ref respostaBanco))
                {
                    if (respostaBanco.HasRows)
                    {
                        respostaBanco.Read();

                        /* Questão */

                        if (numQuestao == 0)
                        {
                            //Método Alternativo de Resolução de Problemas
                            paragraph = new iTextSharp.text.Paragraph(" ");
                            document.Add(paragraph);
                        }

                        String questao = (numQuestao + 1) + ") " + respostaBanco.GetString(0);
                        paragraph = new iTextSharp.text.Paragraph(questao);
                        if (numQuestao == 0)
                            paragraph.SpacingBefore = 200;
                        else
                            paragraph.SpacingBefore = 50;
                        document.Add(paragraph);

                        /* Arquivos */
                        if (banco.executeComando("SELECT B2_QUESTAO, B2_ARQUIVO " +
                             "FROM htb2 " +
                             "WHERE B2_QUESTAO = " + lstCodigosAleatorios.Items[numQuestao],
                                 ref respostaBanco2))
                        {
                            if (respostaBanco2.HasRows)
                            {
                                while (respostaBanco2.Read())
                                {
                                    if (!Convert.IsDBNull(respostaBanco["B2_ARQUIVO"]))
                                    {
                                        String[] nomeArquivo = respostaBanco2.GetString(1).
                                                Split(new char[] { ',', ' ' },
                                                StringSplitOptions.RemoveEmptyEntries);
                                        String[] arquivo = Directory.GetFiles(@"..\_files\",
                                                nomeArquivo[0] + ".*");
                                        String caminhoImagem = Path.GetFullPath(arquivo[0]);
                                        String extensao = Path.GetExtension(arquivo[0]);

                                        /* Somente 1 arquivo */
                                        if (extensao == ".png" || extensao == ".jpg")
                                        {
                                            insereImagemPdf(document, caminhoImagem);
                                        }
                                        else
                                        {
                                            Process.Start(arquivo[0]);
                                        }
                                    }
                                }
                            }
                            respostaBanco.Close();
                            banco.fechaConexao();
                        }
                        numQuestao++;
                    }
                }
            }
                // close the streams
                document.Close();
                fs.Close();
                writer.Close();
                reader.Close();
                Process.Start(newFile);                       
        }

        private void insereImagemPdf(Document document, String caminhoImagem)
        {
            // Obtêm as propriedades da figura
            int imageWidth, imageHeight;
            FileStream stream = new FileStream(caminhoImagem, FileMode.Open);
            using (System.Drawing.Image image = System.Drawing.Image.FromStream(stream, false, false))
            {
                imageWidth = image.Width;
                imageHeight = image.Height;
            }
            stream.Close();
            var imagem = iTextSharp.text.Image.GetInstance(caminhoImagem);  
            if (imageHeight > 375)
                imagem.ScaleToFit(imageWidth, 375); //tamanho da imagem
            document.Add(imagem);
        }

        private StringBuilder nomesMaterias()
        {
            int cont = 0;
            var resultado = new StringBuilder();

            foreach (String temp1 in chkMaterias.CheckedItems)
            {
                cont++;
            }
            foreach (String temp in chkMaterias.CheckedItems)
            {
                cont--;
                String[] tempMaterias = temp.Split(new char[] { ')' }, StringSplitOptions.RemoveEmptyEntries);
                if (cont == 0)
                    resultado.Append(tempMaterias[1]);
                else if (cont == 1)
                    resultado.Append(tempMaterias[1] + " e");
                else
                    resultado.Append(tempMaterias[1] + ',');
            }
            return resultado;
        }

        /*private String copiaModeloWord()
        {
            String[] disciplina = cmbDisciplina.Text.Split(new char[] { '(', ')' },
                    StringSplitOptions.RemoveEmptyEntries);
            StringBuilder materias = nomesMaterias();
            String modelo = "";
            String documentos = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            String pastaHistorico = documentos + "\\HelpTeacher\\Historico\\" + disciplina[1] + '-' + materias + ".docx";

            if (radModelo1.Checked)
                modelo = Path.GetFullPath(@"..\Modelos\modelo1.docx");
            else if (radModelo2.Checked)
                modelo = Path.GetFullPath(@"..\Modelos\modelo2.docx");
            else if (radModelo3.Checked)
                modelo = Path.GetFullPath(@"..\Modelos\modelo3.docx");
            else if (radModelo4.Checked)
                modelo = Path.GetFullPath(@"..\Modelos\modelo4.docx");

            File.Copy(modelo, pastaHistorico, true);         
            return pastaHistorico;
        }*/

        private void geraDocumentoWord()
        {
            Object fimDocumento = "\\endofdoc";
            Word.Range wordRange;
            Object range;
            int numQuestao = 0;
            Object template = modeloSelecionado("docx");
            
            /* Abre a aplicação word e faz uma cópia do documento mapeado */          
            Word.Application appWord = new Word.Application();
            appWord.Visible = true;

            Word.Document document = appWord.Documents.Add(ref template, true);
            document.Paragraphs.Alignment = Word.WdParagraphAlignment.
                    wdAlignParagraphJustify;

            //Insere ícone
            if (pictIcone.Image != null && pictIcone.ImageLocation != null)
            {
                var shape = document.InlineShapes.AddPicture(pictIcone.ImageLocation, false, true);
                shape.Width = 70;
                shape.Height = 60;
            }         

            String[] disciplina = cmbDisciplina.Text.Split(new char[] { '(', ')' },
                    StringSplitOptions.RemoveEmptyEntries);

            substituiTagsWord(document, "@disciplina", disciplina[1]);
            substituiTagsWord(document, "@data", dataAvaliacao.Text);
            substituiTagsWord(document, "@valor", tbValorAvaliacao.Text);
            substituiTagsWord(document, "@professor", tbProfessor.Text);
            substituiTagsWord(document, "@escola", tbEscola.Text);
            
            if(radModelo3.Checked)
            {   
                var resultado = nomesMaterias();
                substituiTagsWord(document, "@materia", resultado.ToString());
            }
                
            else if (radModelo4.Checked)
            {
                String[] curso = cmbCurso.Text.Split(new char[] { ')' },
                    StringSplitOptions.RemoveEmptyEntries);
                substituiTagsWord(document, "@curso", curso[1]);
            }                

            /* Busca as questões no banco e insere no Word */
            while (numQuestao < Convert.ToInt32(txtTotalQuestoes.Text))
            {               
               if (banco.executeComando("SELECT RTRIM(B1_QUESTAO), B2_ARQUIVO " +
                            "FROM htb1 " +
                            "LEFT JOIN htb2 ON B2_QUESTAO = B1_COD " +
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
                        if (banco.executeComando("SELECT B2_QUESTAO, B2_ARQUIVO " +
                             "FROM htb2 " +                             
                             "WHERE B2_QUESTAO = " + lstCodigosAleatorios.Items[numQuestao],
                                 ref respostaBanco2))
                        {
                            if(respostaBanco2.HasRows)
                            {
                                while(respostaBanco2.Read())
                                {
                                    if (!Convert.IsDBNull(respostaBanco["B2_ARQUIVO"]))
                                    {
                                        String[] nomeArquivo = respostaBanco2.GetString(1).
                                                Split(new char[] { ',', ' ' },
                                                StringSplitOptions.RemoveEmptyEntries);
                                        String[] arquivo = Directory.GetFiles(@"..\_files\",
                                                nomeArquivo[0] + ".*");
                                        String caminhoImagem = Path.GetFullPath(arquivo[0]);
                                        String extensao = Path.GetExtension(arquivo[0]);

                                        range = document.Bookmarks.get_Item(ref fimDocumento).Range;

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
                    }
                    respostaBanco.Close();
                    respostaBanco2.Close();
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
            int codigoAvaliacao = 0;

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

            if (banco.executeComando("INSERT INTO htd1 VALUES (NULL, '" +
                       0  + "', '" + data + "')"))
            {
                if (banco.executeComando("SELECT MAX(D1_COD) FROM htd1", ref respostaBanco))
                {
                    if(respostaBanco.HasRows)
                    {
                        respostaBanco.Read();
                        codigoAvaliacao = Convert.ToInt32(respostaBanco.GetString(0));                       
                    }
                }            

                if (chkAvaliacaoInedita.Checked)
                {
                    if (!banco.executeComando("UPDATE htd1 SET D1_INEDITA = '1' " +
                            "WHERE D1_COD = " + codigoAvaliacao))
                        return false;
                }

                foreach (String questao in lstCodigosAleatorios.Items)
                {
                    if (!banco.executeComando("INSERT INTO htd2 VALUES (" + codigoAvaliacao + ",'" +
                            questao + "', '" + 0 + "')"))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;   
      }

        private Boolean marcaQuestoesComoUsadas()
        {
            foreach (String questao in lstCodigosAleatorios.Items)
            {
                if (!banco.executeComando("UPDATE htb3 SET B3_USADA = '1' " +
                        "WHERE B3_QUESTAO = " + questao))
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
                if(radWord.Checked)
                   geraDocumentoWord();
                else if(radPdf.Checked)
                   geraPDF();

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

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictIcone.Image = null;
        }

        private void btnIcone_Click(object sender, EventArgs e)
        {
            if (opnIcone.ShowDialog() == DialogResult.OK)
            {
                //COPIA IMAGEM
                String origem = Path.GetFileName(opnIcone.FileName);
                String destino = Path.GetFullPath(@"..\Modelos\iconesProvas\" + origem);
                if(opnIcone.FileName != destino)
                     File.Copy(opnIcone.FileName, destino, true);
                pictIcone.ImageLocation = destino;
            }
        }

        private void tbProfessor_TextChanged(object sender, EventArgs e)
        {
            preencheCampoProfessor();
        }

        private void tbEscola_TextChanged(object sender, EventArgs e)
        {
            preencheCampoEscola();
        }
 
        private void preencheCampoProfessor()
        {            
            AutoCompleteStringCollection dadosLista = new AutoCompleteStringCollection();
            String arquivo = Path.GetFullPath(@"..\Arquivos\professor.txt");           
            if (File.Exists(arquivo))
            {
                String line;
                StreamReader rd = new StreamReader(arquivo);             
               
                //Lê o próximo caractere sem alterar o estado do leitor
                while (rd.Peek() != -1)
                {
                    line = rd.ReadLine();
                    dadosLista.Add(line);
                }
                tbProfessor.AutoCompleteCustomSource = dadosLista;
                tbProfessor.AutoCompleteMode = AutoCompleteMode.Suggest;
                tbProfessor.AutoCompleteSource = AutoCompleteSource.CustomSource; 
                rd.Close();            
            }
        }

        private void preencheCampoEscola()
        {          
            AutoCompleteStringCollection dadosLista = new AutoCompleteStringCollection();
            String arquivo = Path.GetFullPath(@"..\Arquivos\escola.txt");
            if (File.Exists(arquivo))
            {
                String line;
                StreamReader rd = new StreamReader(arquivo);

                //Lê o próximo caractere sem alterar o estado do leitor
                while (rd.Peek() != -1)
                {
                    line = rd.ReadLine();
                    dadosLista.Add(line);
                }
                tbEscola.AutoCompleteCustomSource = dadosLista;
                tbEscola.AutoCompleteMode = AutoCompleteMode.Suggest;
                tbEscola.AutoCompleteSource = AutoCompleteSource.CustomSource;
                rd.Close();
            }
        }

        private void preencheValorAvaliacao()
        {
            AutoCompleteStringCollection dadosLista = new AutoCompleteStringCollection();
            String arquivo = Path.GetFullPath(@"..\Arquivos\valor.txt");
            if (File.Exists(arquivo))
            {
                String line;
                StreamReader rd = new StreamReader(arquivo);

                //Lê o próximo caractere sem alterar o estado do leitor
                while (rd.Peek() != -1)
                {
                    line = rd.ReadLine();
                    dadosLista.Add(line);
                }
                tbValorAvaliacao.AutoCompleteCustomSource = dadosLista;
                tbValorAvaliacao.AutoCompleteMode = AutoCompleteMode.Suggest;
                tbValorAvaliacao.AutoCompleteSource = AutoCompleteSource.CustomSource;
                rd.Close();
            }
        }

        private void tbValorAvaliacao_TextChanged(object sender, EventArgs e)
        {
            preencheValorAvaliacao();
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void pictModelo1_Click(object sender, EventArgs e)
        {
            radModelo1.Select();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            radModelo2.Select();
        }

        private void pictModelo3_Click(object sender, EventArgs e)
        {
            radModelo3.Select();
        }

        private void pictModelo4_Click(object sender, EventArgs e)
        {
            radModelo4.Select();
        }

        private void pictIcone_Click(object sender, EventArgs e)
        {
            btnIcone_Click(btnIcone, e);
        }      
    }
}
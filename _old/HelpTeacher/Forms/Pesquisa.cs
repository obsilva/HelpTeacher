using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using HelpTeacher.Classes;

namespace HelpTeacher.Forms
{
    public partial class Pesquisa : Form
    {
        private ConexaoBanco banco = new ConexaoBanco();
        private MySql.Data.MySqlClient.MySqlDataReader respostaBanco;
        private MySql.Data.MySqlClient.MySqlDataReader respostaBanco2;
        private MySql.Data.MySqlClient.MySqlDataAdapter adaptador;
        private DataSet ds;
        private String deletados;

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
        private void tabControlPesquisa_SelectedIndexChanged(object sender, EventArgs e)
        {
            ajustaTamanho();
        }

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
            this.Dock = DockStyle.None;

            if (tabControlPesquisa.SelectedTab == pageQuestoes ||
                    tabControlPesquisa.SelectedTab == pagePesquisaGeral)
            {
                this.Size = new Size(1090, 635);

                if (tabControlPesquisa.SelectedTab == pagePesquisaGeral)
                {
                    this.Dock = DockStyle.Fill;
                }
            }
            else if (tabControlPesquisa.SelectedTab == pageAvaliacoes)
            {
                this.Size = new Size(900, 635);
            }
            else
            {
                this.Size = new Size(500, 355);
            }
        }

        private void atualizaCursos(ComboBox combo)
        {
            String[] codigo = combo.Text.Split(new char[] { '(', ')' },
                        StringSplitOptions.RemoveEmptyEntries);

            if (banco.executeComando("SELECT C1_COD, C1_NOME " +
                        "FROM htc1 " +
                        "WHERE C1_COD <> " + codigo[0] +                       
                        " ORDER BY C1_COD", ref respostaBanco))
            {
                if (respostaBanco.HasRows)
                {
                    while (respostaBanco.Read())
                    {
                        combo.Items.Add("(" + respostaBanco.GetString(0) + ") " +
                                    respostaBanco.GetString(1));
                    }
                }
                respostaBanco.Close();
                banco.fechaConexao();
            }
        }

        private void atualizaDisciplinas(ComboBox combo)
        {
            String[] codigo = combo.Text.Split(new char[] { '(', ')' },
                        StringSplitOptions.RemoveEmptyEntries);

            if (banco.executeComando("SELECT C2_COD, C2_NOME " +
                        "FROM htc2 " +
                        "WHERE C2_COD <> " + codigo[0] +                           
                        " ORDER BY C2_COD", ref respostaBanco))
            {
                if (respostaBanco.HasRows)
                {
                    while (respostaBanco.Read())
                    {
                        combo.Items.Add("(" + respostaBanco.GetString(0) + ") " +
                                    respostaBanco.GetString(1));
                    }
                }
                respostaBanco.Close();
                banco.fechaConexao();
            }
        }

        /* setaCursorClick
         * 
         * Seta o cursor na posição clicada no textBox
         */
        private void setaCursorClick(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            MouseEventArgs evento = e as MouseEventArgs;
            txt.Select(txt.GetCharIndexFromPosition(evento.Location), 0);
        }



        // PAGE QUESTÕES  PAGE QUESTÕES  PAGE QUESTÕES  PAGE QUESTÕES  PAGE QUESTÕES  PAGE QUESTÕES  PAGE QUESTÕES  //
        // PAGE QUESTÕES  PAGE QUESTÕES  PAGE QUESTÕES  PAGE QUESTÕES  PAGE QUESTÕES  PAGE QUESTÕES  PAGE QUESTÕES  //
        private void pageQuestoes_Enter(object sender, EventArgs e)
        {
            cmbCampoQuestoes.SelectedIndex = 0;
        }

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
                btnEditarQuestao.Enabled = false;
            else
                btnEditarQuestao.Enabled = true;
        }

        private void radTrueQuestoes_CheckedChanged(object sender, EventArgs e)
        {
            chamaPesquisaQuestoes();
        }

        private void radQuestoesDeletadas_CheckedChanged(object sender, EventArgs e)
        {
            ativaDesativaEdicaoQuestao(false);
            chamaPesquisaQuestoes();
        }

        private void btnEditarQuestao_Click(object sender, EventArgs e)
        {
            ativaDesativaEdicaoQuestao(true);
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

        private void btnBackspace1_Click(object sender, EventArgs e)
        {
            txtArquivo1.Clear();
        }

        private void btnBackspace2_Click(object sender, EventArgs e)
        {
            txtArquivo2.Clear();
        }

        private void btnSalvarQuestoes_Click(object sender, EventArgs e)
        {
            if (salvaQuestaoModificada())
            {
                ativaDesativaEdicaoQuestao(false);
                chamaPesquisaQuestoes();
                Mensagem.dadosAlterados();
            }
            else
                Mensagem.erroAlteracao();
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
                txtQuestao.Text = respostaBanco["B1_QUESTAO"].ToString();
                cmbCursosQuestoes.Items.Add("(" + respostaBanco["C1_COD"].ToString() +
                    ") " + respostaBanco["C1_NOME"].ToString());
                chkDisciplinasQuestoes.Items.Add("(" + respostaBanco["C2_COD"].ToString() +
                    ") " + respostaBanco["C2_NOME"].ToString());
                chkDisciplinasQuestoes.SetItemChecked(0, true);
                chkMateriasQuestoes.Items.Add("(" + respostaBanco["B3_MATERIA"].ToString() +
                    ") " + respostaBanco["C3_NOME"].ToString());
                chkMateriasQuestoes.SetItemChecked(0, true);

                if (respostaBanco["B1_OBJETIV"].ToString().Equals("*"))
                    radObjetiva.Checked = true;
                else
                    radDissertativa.Checked = true;

                if (respostaBanco["B3_USADA"].ToString().Equals("*"))
                    chkQuestaoUsada.Checked = true;
                else
                    chkQuestaoUsada.Checked = false;

                if (respostaBanco["B3_PADRAO"].ToString().Equals("*"))
                    chkQuestaoPadrao.Checked = true;
                else
                    chkQuestaoPadrao.Checked = false;

                if (respostaBanco["B3_DELETED"].ToString().Equals("*"))
                    chkQuestaoDeletada.Checked = true;
                else
                    chkQuestaoDeletada.Checked = false;
                
                /* Arquivos */
                if(banco.executeComando("SELECT B2_ARQUIVO FROM htb2 " +
                        "WHERE B2_QUESTAO = " + txtCodigoQuestao.Text, ref respostaBanco2))
                {
                    if(respostaBanco2.HasRows)
                    {
                        int cont = 0;
                        while(respostaBanco2.Read())
                        {
                            String nome = respostaBanco2["B2_ARQUIVO"].ToString();
                            String caminho = Path.GetFullPath(@"..\_files\" + nome);
                            String[] arquivo = respostaBanco2["B2_ARQUIVO"].ToString().Split(new char[] { '_'},
                                    StringSplitOptions.RemoveEmptyEntries);

                            if (arquivo[1] == "1")
                                txtArquivo1.Text = caminho;
                            else
                                txtArquivo2.Text = caminho;
                            cont++;
                        }
                    }
                    else
                    {
                        txtArquivo1.Clear();
                        txtArquivo2.Clear();
                    }
                    respostaBanco2.Close();
                    /*if (respostaBanco2["B2_ARQUIVO"].ToString().Contains(','))
                    {
                        String[] nomes = respostaBanco2["B2_ARQUIVO"].ToString().Split(new char[] { ',', ' ' },
                                    StringSplitOptions.RemoveEmptyEntries);

                        String[] arquivos = Directory.GetFiles(@"..\_files", nomes[0] + ".*");
                        String caminho = Path.GetFullPath(arquivos[0]);
                        txtArquivo1.Text = caminho;

                        arquivos = Directory.GetFiles(@"..\_files", nomes[1] + ".*");
                        caminho = Path.GetFullPath(arquivos[0]);
                        txtArquivo2.Text = caminho;
                    }
                    else
                    {
                        String[] arquivo = Directory.GetFiles(@"..\_files",
                            respostaBanco["B2_ARQUIVO"].ToString() + ".*");
                        String caminho = Path.GetFullPath(arquivo[0]);
                        txtArquivo1.Text = caminho;
                        txtArquivo2.Clear();
                    }*/
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
                deletados = "1";
                chkQuestaoDeletada.Checked = true;
            }                
            else
            {
                deletados = "0";
                chkQuestaoDeletada.Checked = false;
            }                

            switch (cmbCampoQuestoes.Text)
            {
                case "Código":
                    pesquisaQuestoesEdit("B1_COD", txtPesquisaQuestoes.Text,
                                deletados);
                    break;
                case "Pergunta":
                    pesquisaQuestoesEdit("B1_QUESTAO", txtPesquisaQuestoes.Text,
                                deletados);
                    break;
                case "Objetivas?":
                    pesquisaQuestoesEdit("B1_OBJETIV", radFalseQuestoes.Checked,
                                deletados);
                    break;                
                    
                case "Arquivo?":
                    pesquisaQuestoesEdit("B2_ARQUIVO", radFalseQuestoes.Checked,
                                deletados);
                    break;
                case "Usada?":
                    pesquisaQuestoesEdit("B3_USADA", radFalseQuestoes.Checked,
                                deletados);
                    break;
                case "Matéria":
                    pesquisaQuestoesEdit("B3_MATERIA", txtPesquisaQuestoes.Text,
                                deletados);
                    break;
                case "Padrão?":
                    pesquisaQuestoesEdit("B3_PADRAO", radFalseQuestoes.Checked,
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

        private void ativaDesativaEdicaoQuestao(Boolean ativa)
        {
            cmbCampoQuestoes.Enabled = !ativa;
            txtPesquisaQuestoes.Enabled = !ativa;
            pnlBooleanQuestoes.Enabled = !ativa;
            btnEditarQuestao.Enabled = !ativa;
            pnlQuestao.Enabled = ativa;

            if (radQuestoesDeletadas.Checked)
                chkQuestaoDeletada.Checked = true;
            else
                chkQuestaoDeletada.Checked = false;
        }

        private Boolean insereArquivo(String arquivo)
        {
            if (banco.executeComando("INSERT INTO htb2 VALUES (" +
                                 txtCodigoQuestao.Text + ",'" +
                                 arquivo + "','" + 0 + "')"))                                
             {                  
                  copiaArquivo(arquivo);
                  return true;
             }
            return false;
        }

        private Boolean atualizaArquivo(String arquivo)
        {
                if (banco.executeComando("UPDATE htb2 SET B2_ARQUIVO = '" +
                                    arquivo + "'" +
                                " WHERE B2_QUESTAO = " + txtCodigoQuestao.Text))
                 {
                       apagaArquivos(arquivo);
                       copiaArquivo(arquivo);
                       return true;
                 }
            return false;
        }

        private Boolean apagaArquivoBanco(String codigo, String arquivo)
        {           
            if (banco.executeComando("DELETE FROM htb2 " +
                                 "WHERE B2_QUESTAO = " + codigo +
                                 " AND B2_ARQUIVO = '" + codigo + arquivo + "'"))
            {
                apagaArquivos(arquivo);               
                return true;
            }
            return false;
        }

        private Boolean salvaQuestaoModificada()
        {
            String arquivo = "";

            /* Dissertativa ou Objetiva */
            if (radDissertativa.Checked)
            {
                if (!banco.executeComando("UPDATE htb1 SET B1_OBJETIV = '0' " +
                            "WHERE B1_COD = " + txtCodigoQuestao.Text))
                {
                    return false;
                }
            }
            else
            {
                if (!banco.executeComando("UPDATE htb1 SET B1_OBJETIV = '1' " +
                            "WHERE B1_COD = " + txtCodigoQuestao.Text))
                {
                    return false;
                }
            }

            /* Questão */
            if (!banco.executeComando("UPDATE htb1 SET B1_QUESTAO = '" +
                        txtQuestao.Text + "' WHERE B1_COD = " +
                        txtCodigoQuestao.Text))
            {
                return false;
            }
            
            /* ARQUIVOS */
            if(!txtArquivo1.Text.Equals(""))
            {
                /* VERIFICA SE A QUESTAO JA TEM ALGUM ARQUIVO */
                arquivo = txtCodigoQuestao.Text + "_1";
                if(banco.executeComando("SELECT B2_QUESTAO FROM htb2 " +
                                "WHERE B2_QUESTAO = " + txtCodigoQuestao.Text, ref respostaBanco))                             
                {
                    if(respostaBanco.HasRows)
                    {
                        /* SE A QUESTAO JA TEM ARQUIVOS, VERIFICA SE É O MESMO ARQUIVO */
                        if(banco.executeComando("SELECT B2_QUESTAO FROM htb2 " +
                                "WHERE B2_QUESTAO = " + txtCodigoQuestao.Text +
                                " AND B2_ARQUIVO = '" + arquivo + "'", ref respostaBanco2))
                         {
                             if (!respostaBanco2.HasRows)
                             {
                                 respostaBanco2.Close();
                                 /* SE É O MESMO ARQUIVO, ATUALIZA O ARQUIVO 
                                        SE NAO FOR O MESMO ARQUIVO, INSERE */
                                 if (banco.executeComando("SELECT B2_QUESTAO FROM htb2 " +
                                     "WHERE B2_QUESTAO = " + txtCodigoQuestao.Text +
                                     " AND B2_ARQUIVO  LIKE '%" + "_1" + "'", ref respostaBanco2))
                                 {
                                     if (respostaBanco2.HasRows)
                                         atualizaArquivo(arquivo);                                
                                     else
                                         insereArquivo(arquivo);                                     
                                 }
                             }
                         }
                    }
                    else
                    {
                        insereArquivo(arquivo);
                    }                  
                }                
           }

            else if (txtArquivo1.Text.Equals(""))
            {
                /* VERIFICA SE A QUESTAO POSSUI ARQUIVO */ 
                if (banco.executeComando("SELECT B2_QUESTAO FROM htb2 " +
                                "WHERE B2_QUESTAO = " + txtCodigoQuestao.Text +
                                " AND B2_ARQUIVO LIKE '%" + "_1'", ref respostaBanco))
                {
                    if (respostaBanco.HasRows)
                    {
                        apagaArquivoBanco(txtCodigoQuestao.Text, "_1");
                    }                   
                }
            }

            respostaBanco.Close();
            respostaBanco2.Close();

            arquivo = txtCodigoQuestao.Text + "_2";
            if (!txtArquivo2.Text.Equals(""))
            {                
                /* VERIFICA SE A QUESTAO JA TEM ALGUM ARQUIVO */
                if (banco.executeComando("SELECT B2_QUESTAO FROM htb2 " +
                                "WHERE B2_QUESTAO = " + txtCodigoQuestao.Text, ref respostaBanco))
                {
                    if (respostaBanco.HasRows)
                    {
                        /* SE A QUESTAO JA TEM ARQUIVOS, VERIFICA SE É O MESMO ARQUIVO */
                        if (banco.executeComando("SELECT B2_QUESTAO FROM htb2 " +
                                "WHERE B2_QUESTAO = " + txtCodigoQuestao.Text +
                                " AND B2_ARQUIVO = '" + arquivo + "'", ref respostaBanco2))
                        {
                            if (!respostaBanco2.HasRows)
                            {
                                respostaBanco2.Close();
                                /* SE É O MESMO ARQUIVO, ATUALIZA O ARQUIVO 
                                    SE NAO FOR O MESMO ARQUIVO, INSERE */
                                if (banco.executeComando("SELECT B2_QUESTAO FROM htb2 " +
                                    "WHERE B2_QUESTAO = " + txtCodigoQuestao.Text +
                                    " AND B2_ARQUIVO LIKE '%" + "_2" + "'", ref respostaBanco2))
                                {
                                    if(respostaBanco2.HasRows)                                    
                                        atualizaArquivo(arquivo);                                       
                                    else
                                        insereArquivo(arquivo);
                                }                                
                            }
                        }
                    }
                    else
                    {
                        insereArquivo(arquivo);
                    }
                }
            }

            else if (txtArquivo2.Text.Equals(""))
            {
                /* VERIFICA SE TEM ALGUM ARQUIVO _2 PARA SER DELETADO */
                if (banco.executeComando("SELECT B2_QUESTAO FROM htb2 " +
                                "WHERE B2_QUESTAO = " + txtCodigoQuestao.Text +
                                " AND B2_ARQUIVO LIKE '%" + "_2'", ref respostaBanco))
                {
                    if (respostaBanco.HasRows)
                    {
                        apagaArquivoBanco(txtCodigoQuestao.Text, "_2");
                    }
                }
            }

            respostaBanco.Close();
            respostaBanco2.Close();

            /* Usada */
            if (chkQuestaoUsada.Checked)
            {
                if (!banco.executeComando("UPDATE htb3 SET B3_USADA = '1' " +
                            "WHERE B3_QUESTAO = " + txtCodigoQuestao.Text))
                {
                    return false;
                }
            }
            else
            {
                if (!banco.executeComando("UPDATE htb3 SET B3_USADA = '0' " +
                            "WHERE B3_QUESTAO = " + txtCodigoQuestao.Text))
                {
                    return false;
                }
            }

            /* Padrão */
            if (chkQuestaoPadrao.Checked)
            {
                if (!banco.executeComando("UPDATE htb3 SET B3_PADRAO = '1' " +
                            "WHERE B3_QUESTAO = " + txtCodigoQuestao.Text))
                {
                    return false;
                }
            }
            else
            {
                if (!banco.executeComando("UPDATE htb3 SET B3_PADRAO = '0' " +
                            "WHERE B3_QUESTAO = " + txtCodigoQuestao.Text))
                {
                    return false;
                }
            }

            /* Deletada */
            if (chkQuestaoDeletada.Checked)
            {
                if (banco.executeComando("UPDATE htb3 SET B3_DELETED = '1' " +
                            "WHERE B3_QUESTAO = " + txtCodigoQuestao.Text))
                {
                    return true;                 
                }
                return false;
            }
            else
            {
                if (banco.executeComando("UPDATE htb3 SET B3_DELETED = '0' " +
                            "WHERE B3_QUESTAO = " + txtCodigoQuestao.Text))
                {
                    return true;
                }
                return false;
            }
            return true;
        }
        /* String primeiroArquivo = Path.Combine(@"..\_files", (txtCodigoQuestao.Text + "_1"));

            if (txtArquivo1.Text.Equals(""))
            {
                String extensao = Path.GetExtension(txtArquivo2.Text);
                primeiroArquivo = String.Concat(primeiroArquivo, extensao);

                if (!txtArquivo2.Text.Contains(@"_files\" + txtCodigoQuestao.Text))
                    File.Copy(txtArquivo2.Text, primeiroArquivo, true);
                else
                    File.Move(txtArquivo2.Text, primeiroArquivo);
            }
            else
            {
                String extensao1 = Path.GetExtension(txtArquivo1.Text);
                primeiroArquivo = String.Concat(primeiroArquivo, extensao1);

                if (!txtArquivo1.Text.Contains(@"_files\" + txtCodigoQuestao.Text))
                    File.Copy(txtArquivo1.Text, primeiroArquivo, true);
                else
                    File.Move(txtArquivo1.Text, primeiroArquivo);
                if (!txtArquivo2.Text.Equals(""))
                {
                    String segundoArquivo = Path.Combine(@"..\_files", (txtCodigoQuestao.Text + "_2"));
                    String extensao2 = Path.GetExtension(txtArquivo2.Text);
                    segundoArquivo = String.Concat(segundoArquivo, extensao2);

                    if (!txtArquivo2.Text.Contains(@"_files\" + txtCodigoQuestao.Text))
                        File.Copy(txtArquivo2.Text, segundoArquivo, true);
                    else
                        File.Move(txtArquivo2.Text, segundoArquivo);
                }
            }
         */

        private void copiaArquivo(String arquivo)
        {
            String[] tipo = arquivo.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

            if (tipo[1] == "1")
            {
                String extensao = Path.GetExtension(txtArquivo1.Text);
                String nome = String.Concat(arquivo, extensao);
                String caminho = Path.Combine(@"..\_files", nome);

                if (!txtArquivo1.Text.Contains(@"_files\" + txtCodigoQuestao.Text))
                    File.Copy(txtArquivo1.Text, caminho, true);
                else
                    File.Move(txtArquivo1.Text, caminho);
            }

            else if(tipo[1] == "2")
            {
                String extensao = Path.GetExtension(txtArquivo2.Text);
                String nome = String.Concat(arquivo, extensao);
                String caminho = Path.Combine(@"..\_files", nome);

                if (!txtArquivo2.Text.Contains(@"_files\" + txtCodigoQuestao.Text))
                    File.Copy(txtArquivo2.Text, caminho, true);
                else
                    File.Move(txtArquivo2.Text, caminho);
            }               
           
        }

        /* apagaArquivos
         * 
         * Verifica se existe algum arquivo relacionada a
         * questão e se tiver, apaga o arquivo
         */
        private void apagaArquivos(String arq)
        {
                String[] arquivos = Directory.GetFiles(@"..\_files",
                            txtCodigoQuestao.Text + arq + "*");
                foreach (String arquivo in arquivos)
                {
                    String caminho = Path.GetFullPath(arquivo);

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
        private void pageCursos_Enter(object sender, EventArgs e)
        {
            cmbCampoCursos.SelectedIndex = 0;
        }

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

        private void radCursosDeletados_CheckedChanged(object sender, EventArgs e)
        {
            txtPesquisaCursos.Clear();
        }

        private void txtPesquisaCursos_TextChanged(object sender, EventArgs e)
        {
            chamaPesquisaCursos();
        }

        private void btnEditarCursos_Click(object sender, EventArgs e)
        {
            ativaDesativaEdicaoCurso(true);
        }

        private void btnSalvarCurso_Click(object sender, EventArgs e)
        {
            if (salvaCursoModificado())
            {
                ativaDesativaEdicaoCurso(false);
                chamaPesquisaCursos();
                Mensagem.dadosAlterados();
            }
            else
                Mensagem.erroAlteracao();
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
                deletados = "1";
            else
                deletados = "0";

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
                pnlDeletadosCursos.Hide();
        }

        private void ativaDesativaEdicaoCurso(Boolean ativa)
        {
            cmbCampoCursos.Enabled = !ativa;
            txtPesquisaCursos.Enabled = !ativa;
            pnlDeletadosCursos.Enabled = !ativa;
            btnEditarCursos.Enabled = !ativa;
            pnlCursos.Enabled = ativa;
        }

        private Boolean deletaCurso(int opcao)
        {
            //PEGA CODIGO DISCIPLINA
            if (banco.executeComando("SELECT C2_COD FROM htc2 " +
                        "WHERE C2_CURSO = " + txtCodigoCurso.Text, ref respostaBanco))
            {
                if (respostaBanco.HasRows)
                {
                    respostaBanco.Read();
                    String codigoDisciplina = respostaBanco.GetString(0);
                    respostaBanco.Close();

                    //PEGA CODIGO MATERIA
                    if (banco.executeComando("SELECT C3_COD FROM htc3 " +
                           "WHERE C3_DISCIPL = '" + codigoDisciplina + "'", ref respostaBanco))
                    {
                        if (respostaBanco.HasRows)
                        {
                            respostaBanco.Read();
                            String codigoMateria = respostaBanco.GetString(0);
                            respostaBanco.Close();
                            if (banco.executeComando("UPDATE htb3 SET B3_DELETED = '"  + opcao +
                                "' WHERE B3_MATERIA = '" + codigoMateria + "'"))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        private Boolean salvaCursoModificado()
        {
            if (!banco.executeComando("UPDATE htc1 SET C1_NOME = '" + txtNomeCurso.Text +
                        "' WHERE C1_COD = " + txtCodigoCurso.Text))
            {
                return false;
            }

            //Deletada
            if (chkCursoDeletado.Checked)
            {
                deletaCurso(1);
            }
            else
            {
               deletaCurso(0);
            }
            return true;
        }

        // PAGE DISCIPLINAS  PAGE DISCIPLINAS  PAGE DISCIPLINAS  PAGE DISCIPLINAS  PAGE DISCIPLINAS  PAGE DISCIPLINAS  //
        // PAGE DISCIPLINAS  PAGE DISCIPLINAS  PAGE DISCIPLINAS  PAGE DISCIPLINAS  PAGE DISCIPLINAS  PAGE DISCIPLINAS  //
        private void pageDisciplina_Enter(object sender, EventArgs e)
        {
            cmbCampoDisciplinas.SelectedIndex = 0;
        }

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

        private void txtPesquisaDisciplina_TextChanged(object sender, EventArgs e)
        {
            chamaPesquisaDisciplinas();
        }

        private void radDisciplinasDeletadas_CheckedChanged(object sender, EventArgs e)
        {
            txtPesquisaDisciplina.Clear();
        }

        private void btnEditarDisciplina_Click(object sender, EventArgs e)
        {
            ativaDesativaEdicaoDisc(true);
            atualizaCursos(cmbCursosDisciplina);
        }

        private void bntSalvarDisc_Click(object sender, EventArgs e)
        {
            if (salvaDiscModificada())
            {
                ativaDesativaEdicaoDisc(false);
                chamaPesquisaDisciplinas();
                Mensagem.dadosAlterados();
            }
            else
                Mensagem.erroAlteracao();
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
                cmbCursosDisciplina.Items.Clear();

                txtCodigoDisciplina.Text = respostaBanco["C2_COD"].ToString();
                textNomeDisciplina.Text = respostaBanco["C2_NOME"].ToString();
                cmbCursosDisciplina.Items.Add("(" + respostaBanco["C2_CURSO"].ToString() +
                    ") " + respostaBanco["C1_NOME"].ToString());

                /*if (respostaBanco["D_E_L_E_T"].ToString().Equals("*"))
                    chkDisciplinaDeletada.Checked = true;
                else
                    chkDisciplinaDeletada.Checked = false;*/

                respostaBanco.Close();
                banco.fechaConexao();
                cmbCursosDisciplina.SelectedIndex = 0;
            }
            else
            {
                btnEditarDisciplina.Enabled = false;
                txtCodigoDisciplina.Clear();
                chkDisciplinaDeletada.Checked = false;
                textNomeDisciplina.Clear();
                cmbCursosDisciplina.Items.Clear();
                respostaBanco.Close();
                banco.fechaConexao();
            }
        }

        private void chamaPesquisaDisciplinas()
        {
            if (radDisciplinasDeletadas.Checked)
                deletados = "1";
            else
                deletados = "0";

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
                pnlDeletadosDisciplinas.Hide();
        }

        private void ativaDesativaEdicaoDisc(Boolean ativa)
        {
            cmbCampoDisciplinas.Enabled = !ativa;
            txtPesquisaDisciplina.Enabled = !ativa;
            pnlDeletadosDisciplinas.Enabled = !ativa;
            btnEditarDisciplina.Enabled = !ativa;
            pnlDisciplina.Enabled = ativa;
        }

        private Boolean apagaDisciplina(int opcao)
        {
            //PEGA CODIGO MATERIA
            if (banco.executeComando("SELECT C3_COD FROM htc3 " +
                   "WHERE C3_DISCIPL = '" + txtCodigoDisciplina.Text + "'", ref respostaBanco))
            {
                if (respostaBanco.HasRows)
                {
                    respostaBanco.Read();
                    String codigoMateria = respostaBanco.GetString(0);
                    respostaBanco.Close();
                    if (banco.executeComando("UPDATE htb3 SET B3_DELETED = '" + opcao +
                        "' WHERE B3_MATERIA = '" + codigoMateria + "'"))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private Boolean salvaDiscModificada()
        {
            String[] curso = cmbCursosDisciplina.Text.Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);

            if (!banco.executeComando("UPDATE htc2 SET C2_NOME = '" +
                            textNomeDisciplina.Text + "', C2_CURSO =  " +
                            curso[0] +
                        " WHERE C2_COD = " + txtCodigoDisciplina.Text))
            {
                return false;
            }

            /* Deletado */
            if (chkDisciplinaDeletada.Checked)
            {
                apagaDisciplina(1);
            }
            else
            {
                apagaDisciplina(0);
            }
            return true;
        }

        // PAGE MATÉRIAS  PAGE MATÉRIAS  PAGE MATÉRIAS  PAGE MATÉRIAS  PAGE MATÉRIAS  PAGE MATÉRIAS  PAGE MATÉRIAS  //
        // PAGE MATÉRIAS  PAGE MATÉRIAS  PAGE MATÉRIAS  PAGE MATÉRIAS  PAGE MATÉRIAS  PAGE MATÉRIAS  PAGE MATÉRIAS  //
        private void pageMateria_Enter(object sender, EventArgs e)
        {
            cmbCampoMateria.SelectedIndex = 0;
        }

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

        private void txtPesquisaMateria_TextChanged(object sender, EventArgs e)
        {
            chamaPesquisaMaterias();
        }

        private void radMateriasDeletadas_CheckedChanged(object sender, EventArgs e)
        {
            txtPesquisaMateria.Clear();
        }

        private void btnEditarMateria_Click(object sender, EventArgs e)
        {
            ativaDesativaEdicaoMateria(true);
            atualizaDisciplinas(cmbDisciplinaMateria);
        }

        private void cmbDisciplinaMateria_SelectedIndexChanged(object sender, EventArgs e)
        {
            recuperaNomeCurso();
        }

        private void bntSalvarMateria_Click(object sender, EventArgs e)
        {
            if (salvaMateriaModificada())
            {
                ativaDesativaEdicaoMateria(false);
                chamaPesquisaMaterias();
                Mensagem.dadosAlterados();
            }
            else
                Mensagem.erroAlteracao();
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
                cmbDisciplinaMateria.Items.Clear();

                txtCodigoMateria.Text = respostaBanco["C3_COD"].ToString();
                txtNomeMateria.Text = respostaBanco["C3_NOME"].ToString();
                cmbDisciplinaMateria.Items.Add("(" + respostaBanco["C3_DISCIP"].ToString() +
                    ") " + respostaBanco["C2_NOME"].ToString());  

                /*if (respostaBanco["D_E_L_E_T"].ToString().Equals("*"))
                    chkMateriaDeletada.Checked = true;
                else
                    chkMateriaDeletada.Checked = false;*/

                respostaBanco.Close();
                banco.fechaConexao();
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
                respostaBanco.Close();
                banco.fechaConexao();
            }
        }

        private void chamaPesquisaMaterias()
        {
            if (radMateriasDeletadas.Checked)
                deletados = "1";
            else
                deletados = "0";

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
                    pesquisaMateriasEdit("C3_DISCIP", txtPesquisaMateria.Text,
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
                pnlDeletadosMaterias.Hide();
        }

        private void ativaDesativaEdicaoMateria(Boolean ativa)
        {
            cmbCampoMateria.Enabled = !ativa;
            txtPesquisaMateria.Enabled = !ativa;
            pnlDeletadosMaterias.Enabled = !ativa;
            btnEditarMateria.Enabled = !ativa;
            pnlMateria.Enabled = ativa;
        }

        private Boolean salvaMateriaModificada()
        {
            String[] disciplina = cmbDisciplinaMateria.Text.Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);

            if (!banco.executeComando("UPDATE htc3 SET C3_NOME = '" +
                            txtNomeMateria.Text + "', C3_DISCIP =  " +
                            disciplina[0] +
                        " WHERE C3_COD = " + txtCodigoMateria.Text))
            {
                return false;
            }

            /* Deletada */
            if (chkMateriaDeletada.Checked)
            {
                if (!banco.executeComando("UPDATE htc3 SET C3_DELETED = '1' " +
                        "WHERE C3_COD = " + txtCodigoMateria.Text))
                {
                    return false;
                }
            }
            else
            {
                if (!banco.executeComando("UPDATE htc3 SET C3_DELETED = '0' " +
                        "WHERE C3_COD = " + txtCodigoMateria.Text))
                {
                    return false;
                }
            }

            return true;
        }

        private void recuperaNomeCurso()
        {
            String[] codMateria = cmbDisciplinaMateria.Text.Split(new char[] 
                    { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);

            if (banco.executeComando("SELECT C2_NOME " +
                        "FROM htc2 " +
                            "INNER JOIN htc1 " +
                                "ON C2_CURSO = C1_COD " +
                        "WHERE C2_COD = " + codMateria[0], ref respostaBanco))
            {
                respostaBanco.Read();
                txtCursoMateria.Text = respostaBanco.GetString(0);

                respostaBanco.Close();
                banco.fechaConexao();
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
            cmbAvaliacoes.Items.Clear();
            cmbDisciplinasAvaliacoes.Items.Clear();
            cmbMateriasAvaliacoes.Items.Clear();
            txtAvaliacao.Clear();

            cmbAvaliacoes.Text = "Selecionar...";
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
            cmbMateriasAvaliacoes.Items.Clear();

            chamaPesquisaAvaliacoes();
        }

        private void cmbDisciplinasAvaliacoes_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbMateriasAvaliacoes.Text = "Opcional";
            cmbMateriasAvaliacoes.Enabled = true;
            atualizaComboMaterias();

            chamaPesquisaAvaliacoes();
        }

        private void cmbMateriasAvaliacoes_SelectedIndexChanged(object sender, EventArgs e)
        {
            chamaPesquisaAvaliacoes();
        }

        private void dateAvalicao_ValueChanged(object sender, EventArgs e)
        {
            chamaPesquisaAvaliacoes();
        }

        private void cmbAvaliacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAvaliacao.Clear();
            mostraAvaliacao();
        }

        private void chkAvaliacoesDeletadas_CheckedChanged(object sender, EventArgs e)
        {
            chamaPesquisaAvaliacoes();
        }

        private void btnLimparRegistro_Click(object sender, EventArgs e)
        {
            if (apagaRegistro())
            {
                Mensagem.dadosAlterados();
                chamaPesquisaAvaliacoes();
            }
            else
                Mensagem.erroAlteracao();
        }

        private void btnLimparHistorico_Click(object sender, EventArgs e)
        {
            if (apagaHistorico())
            {
                Mensagem.dadosAlterados();
                chamaPesquisaAvaliacoes();
            }
            else
                Mensagem.erroAlteracao();
        }

        private void chamaPesquisaAvaliacoes()
        {
            if (chkAvaliacoesDeletadas.Checked)
                deletados = "1";
            else
                deletados = "0";

            if (radData.Checked)
            {
                pesquisaHistoricoEdit("D1_DATA", dateAvalicao.Text, deletados);
            }
            else
            {
                if (!cmbMateriasAvaliacoes.Text.Equals("Opcional"))
                {
                    String[] codigoMateria = cmbMateriasAvaliacoes.Text.Split
                    (new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);

                    pesquisaHistoricoEdit("B3_MATERIA", codigoMateria[0], deletados);
                }
                else if (!cmbDisciplinasAvaliacoes.Text.Equals("Opcional"))
                {
                    String[] codigoDisciplina = cmbDisciplinasAvaliacoes.Text.
                            Split(new char[] { '(', ')' }, StringSplitOptions.
                            RemoveEmptyEntries);

                    pesquisaHistoricoEdit("C2_COD", codigoDisciplina[0], deletados);
                }
                else
                {
                    String[] codigoCurso = cmbCursosAvaliacoes.Text.Split(new char[] 
                            { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);

                    pesquisaHistoricoEdit("C1_COD", codigoCurso[0], deletados);
                }
            }
        }
        
        private void atualizaComboCursos()
        {
            cmbCursosAvaliacoes.Items.Clear();
            cmbAvaliacoes.Items.Clear();

            if (banco.executeComando("SELECT C1_COD, C1_NOME " + 
                    "FROM htc1", ref respostaBanco))
            {
                if (respostaBanco.HasRows)
                {
                    while (respostaBanco.Read())
                    {
                        cmbCursosAvaliacoes.Items.Add("(" + respostaBanco.GetString(0) +
                            ")" + respostaBanco.GetString(1));
                    }
                }
                respostaBanco.Close();
                banco.fechaConexao();
            }
        }

        private void atualizaComboDisicplina()
        {
            String[] codigoCurso = cmbCursosAvaliacoes.Text.Split(new char[] 
                { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
            
            cmbDisciplinasAvaliacoes.Items.Clear();
            if (banco.executeComando("SELECT C2_COD, C2_NOME " + 
                    "FROM htc2 " + 
                    "WHERE C2_CURSO = " + codigoCurso[0], ref respostaBanco))
            {
                if (respostaBanco.HasRows)
                {
                    while (respostaBanco.Read())
                    {
                        cmbDisciplinasAvaliacoes.Items.Add("(" + respostaBanco.
                                GetString(0) + ")" + respostaBanco.GetString(1));
                    }
                }
                respostaBanco.Close();
                banco.fechaConexao();
            }
        }

        private void atualizaComboMaterias()
        {
            String[] codigoDisciplina = cmbDisciplinasAvaliacoes.
                    Text.Split(new char[] { '(', ')' },
                    StringSplitOptions.RemoveEmptyEntries);

            cmbMateriasAvaliacoes.Items.Clear();
            if (banco.executeComando("SELECT C3_COD, C3_NOME " + 
                    "FROM htc3 " + 
                    "WHERE C3_DISCIP = " + codigoDisciplina[0], ref respostaBanco))
            {
                if (respostaBanco.HasRows)
                {
                    while (respostaBanco.Read())
                    {
                        cmbMateriasAvaliacoes.Items.Add("(" + respostaBanco.
                                GetString(0) + ")" + respostaBanco.GetString(1));
                    }
                }
                respostaBanco.Close();
                banco.fechaConexao();
            }
        }

        private void atualizaCamposAvaliacao()
        {
            cmbAvaliacoes.Items.Clear();

            if (respostaBanco.HasRows)
            {
                while (respostaBanco.Read())
                {                    
                    cmbAvaliacoes.Items.Add("(" + respostaBanco.GetString(0) + ") " + 
                            respostaBanco.GetString(1) + " - " + respostaBanco.GetString(3));
                }
                respostaBanco.Close();
                banco.fechaConexao();
                cmbAvaliacoes.SelectedIndex = 0;
                btnLimparRegistro.Enabled = true;
            }
            else
            {
                txtAvaliacao.Clear();
                cmbAvaliacoes.Text = "Sem histórico";
                btnLimparRegistro.Enabled = false;
                respostaBanco.Close();
                banco.fechaConexao();
            }
        }

        private void mostraAvaliacao()
        {
            String[] codigoAvaliacao = cmbAvaliacoes.Text.Split(new char[] 
                { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
            String[] codigosQuestoes = new String[100];

            /* Recupera os códigos das questões da avaliação */
            if (banco.executeComando("SELECT D2_QUESTAO " + 
                        "FROM htd1 " +
                        "INNER JOIN htd2 ON D2_PROVA = D1_COD " +
                        "WHERE D1_COD = " + codigoAvaliacao[0], 
                        ref respostaBanco))
            {
                if (respostaBanco.HasRows)
                {
                    int count = 0;
                    int contador = 0;

                    while (respostaBanco.Read())
                    {
                         codigosQuestoes[contador] = respostaBanco.GetString(0);
                         contador++;
                    }
                    
                    respostaBanco.Close();
                    banco.fechaConexao();
                    /* Recupera as questões da avaliação */
                    while (contador > 0)
                    {
                        if (banco.executeComando("SELECT B1_QUESTAO " +
                            "FROM htb1 " +
                            "WHERE B1_COD = " + codigosQuestoes[count], ref respostaBanco))
                        {
                            if (respostaBanco.HasRows)
                            {
                                respostaBanco.Read();
                                txtAvaliacao.Text += (++count) + ") " + 
                                        respostaBanco.GetString(0) +
                                        Environment.NewLine +
                                        Environment.NewLine;
                                contador--;
                            }
                           
                        }
                    }
                    respostaBanco.Close();
                    banco.fechaConexao();
                }
                else
                {
                    txtAvaliacao.Text = "Sem histórico";
                    respostaBanco.Close();
                    banco.fechaConexao();
                }  
            }
        }

        private Boolean apagaRegistro()
        {
            String[] codigoAvaliacao = cmbAvaliacoes.Text.Split(
                    new char[] { '(', ')' }, StringSplitOptions.
                    RemoveEmptyEntries);

            if (banco.executeComando("UPDATE htd2 SET D2_DELETED = '1' " +
                        "WHERE D2_PROVA = " + codigoAvaliacao[0]))
            {
                return true;
            }
            return false;
        }

        private Boolean apagaHistorico()
        {
            if (banco.executeComando("UPDATE htd2 SET D2_DELETED = '1'"))
            {
                return true;
            }
            return false;
        }

        // PAGE PESQUISA GERAL  PAGE PESQUISA GERAL  PAGE PESQUISA GERAL  PAGE PESQUISA GERAL  PAGE PESQUISA GERAL  //
        // PAGE PESQUISA GERAL  PAGE PESQUISA GERAL  PAGE PESQUISA GERAL  PAGE PESQUISA GERAL  PAGE PESQUISA GERAL  //
        private void pagePesquisaGeral_Enter(object sender, EventArgs e)
        {
            cmbTabela.SelectedIndex = 0;
        }

        private void pagePesquisaGeral_Leave(object sender, EventArgs e)
        {
            txtPesquisaGeral.Clear();
        }

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

        private void txtPesquisaGeral_TextChanged(object sender, EventArgs e)
        {
            chamaPesquisaGeral();
        }

        private void radTrueGeral_CheckedChanged(object sender, EventArgs e)
        {
            chamaPesquisaGeral();
        }

        private void radGeralDeletados_CheckedChanged(object sender, EventArgs e)
        {
            chamaPesquisaGeral();
        }

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
                deletados = "1";
            else
                deletados = "0";

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
                            pesquisaQuestoesGrid("B1_QUESTAO", txtPesquisaGeral.Text,
                                    deletados);
                            break;
                        case "Objetivas?":
                            pesquisaQuestoesGrid("B1_OBJETIV", radFalseGeral.Checked,
                                    deletados);
                            break;
                        case "Alternativa A":
                            pesquisaQuestoesGrid("B1_QUESTAO", "a) " +
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
                            pesquisaQuestoesGrid("B1_QUESTAO", "d) " +
                                    txtPesquisaGeral.Text, deletados);
                            break;
                        case "Alternativa E":
                            pesquisaQuestoesGrid("B1_QUESTAO", "e) " +
                                    txtPesquisaGeral.Text, deletados);
                            break;
                        case "Arquivo?":
                            pesquisaQuestoesGrid("B2_ARQUIVO", radFalseGeral.Checked,
                                    deletados);
                            break;
                        case "Usada?":
                            pesquisaQuestoesGrid("B3_USADA", radFalseGeral.Checked,
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
                            pesquisaQuestoesGrid("B3_PADRAO", radFalseGeral.Checked,
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
                            
                        case "Inédita?":
                            pesquisaHistoricoGrid("D1_INEDITA", radFalseGeral.Checked,
                                    deletados);
                            break;
                        case "Questão":
                            pesquisaHistoricoGrid("D2_QUESTAO", txtPesquisaGeral.Text,
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
        private void pesquisaQuestoesEdit(String campo, String condicao, String deletados)
        {
            if (banco.executeComando("SELECT htb1.*, htb3.*, C3_NOME, " +
                        "C2_COD, C2_NOME, C1_COD, C1_NOME " +
                    "FROM htb1 " +                      
                        "INNER JOIN htb3 " +
                            "ON B3_QUESTAO = B1_COD " +
                        "INNER JOIN htc3 " +
                            "ON B3_MATERIA = C3_COD " +
                        "INNER JOIN htc2 " +
                            "ON C3_DISCIP = C2_COD " +
                        "INNER JOIN htc1 " +
                            "ON C2_CURSO = C1_COD " +
                    "WHERE " + campo + " LIKE '%" + condicao + "%' AND " +
                        "B3_DELETED = '"  + deletados  + "' " +
                    "ORDER BY B1_COD", ref respostaBanco))
            {
                atualizaCamposQuestoes();
            }
        }

        private void pesquisaQuestoesEdit(String campo, Boolean isNull, String deletados)
        {
            if (isNull)
            {
                if (banco.executeComando("SELECT C3_COD, C3_NOME, " +
                            "C2_COD, C2_NOME, C1_COD, C1_NOME " +
                        "FROM htb3 " +
                            "INNER JOIN htc3 " +
                                "ON B3_MATERIA = C3_COD " +
                            "INNER JOIN htc2 " +
                                "ON C3_DISCIP = C2_COD " +
                            "INNER JOIN htc1 " +
                                "ON C2_CURSO = C1_COD " +
                        "WHERE " + campo + " IS NULL AND " +
                            "B3_DELETED = '" + deletados + "'" +
                        "ORDER BY B1_COD", ref respostaBanco))
                {
                    atualizaCamposQuestoes();
                }
            }
            else
            {
                if (banco.executeComando("SELECT C3_COD, C3_NOME, " +
                            "C2_COD, C2_NOME, C1_COD, C1_NOME " +
                        "FROM htb3 " +
                            "INNER JOIN htc3 " +
                                "ON B3_MATERIA = C3_COD " +
                            "INNER JOIN htc2 " +
                                "ON C3_DISCIP = C2_COD " +
                            "INNER JOIN htc1 " +
                                "ON C2_CURSO = C1_COD " +
                        "WHERE " + campo + " IS NOT NULL AND " +
                            "B3_DELETED = '" + deletados + "' " +
                        "ORDER BY B1_COD", ref respostaBanco))
                {
                    atualizaCamposQuestoes();
                }
            }
        }

        private void pesquisaQuestoesGrid(String campo, String condicao, String deletados)
        {
            if (banco.executeComando("SELECT B1_COD AS 'Codigo', " +
                        "B1_OBJETIV AS 'Objetiva?', " +
                        "B3_USADA AS 'Usada?', C1_NOME AS 'Curso', " +
                        "C2_NOME AS 'Disciplina', C3_NOME AS 'Materia', " + 
                        "B3_PADRAO AS 'Padrao?', B1_QUESTAO AS 'Questao'" +
                    "FROM htb1 " +
                        "INNER JOIN htb3 " +
                            "ON B1_COD = B3_QUESTAO " +                       
                        "INNER JOIN htc3 " +
                            "ON B3_MATERIA = C3_COD " +
                        "INNER JOIN htc2 " +
                            "ON C3_DISCIP = C2_COD " +
                        "INNER JOIN htc1 " +
                            "ON C2_CURSO = C1_COD " +
                    "WHERE " + campo + " LIKE '%" + condicao + "%' AND " +
                        "B3_DELETED = '" + deletados +
                    "' ORDER BY B1_COD", ref adaptador))
            {
                atualizaGridGeral();
            }
        }

        private void pesquisaQuestoesGrid(String campo, Boolean isNull, String deletados)
        {
            if (isNull)
            {
                if (banco.executeComando("SELECT B1_COD AS 'Codigo', " +
                        "B1_OBJETIV AS 'Objetiva?', B2_ARQUIVO AS 'Arquivo?', " +
                        "B3_USADA AS 'Usada?', C1_NOME AS 'Curso', " +
                        "C2_NOME AS 'Disciplina', C3_NOME AS 'Materia', " +
                        "B3_PADRAO AS 'Padrao?', B1_QUESTAO AS 'Questao'" +
                    "FROM htb3 " +
                        "INNER JOIN htc3 " +
                            "ON B3_MATERIA = C3_COD " +
                        "INNER JOIN htc2 " +
                            "ON C3_DISCIP = C2_COD " +
                        "INNER JOIN htc1 " +
                            "ON C2_CURSO = C1_COD " +
                    "WHERE " + campo + " IS NULL AND " +
                        "B3_DELETED = '" + deletados + "' " +
                    "ORDER BY B1_COD", ref adaptador))
                {
                    atualizaGridGeral();
                }
            }
            else
            {
                if (banco.executeComando("SELECT B1_COD AS 'Codigo', " +
                        "B1_OBJETIV AS 'Objetiva?', B2_ARQUIVO AS 'Arquivo?', " +
                        "B3_USADA AS 'Usada?', C1_NOME AS 'Curso', " +
                        "C2_NOME AS 'Disciplina', C3_NOME AS 'Materia', " +
                        "B3_PADRAO AS 'Padrao?', B1_QUESTAO AS 'Questao'" +
                    "FROM htb3 " +
                        "INNER JOIN htc3 " +
                            "ON B3_MATERIA = C3_COD " +
                        "INNER JOIN htc2 " +
                            "ON C3_DISCIP = C2_COD " +
                        "INNER JOIN htc1 " +
                            "ON C2_CURSO = C1_COD " +
                    "WHERE " + campo + " IS NOT NULL AND " +
                        "B3_DELETED = '" + deletados + "' " +
                    "ORDER BY B1_COD", ref adaptador))
                {
                    atualizaGridGeral();
                }
            }
        }

        // CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  //
        // CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  CURSO  //
        private void pesquisaCursosEdit(String campo, String condicao, String deletados)
        {
            if (banco.executeComando("SELECT * " +
                    "FROM htc1 " +
                    "WHERE " + campo + " LIKE '%" + condicao + 
                        "%' " +
                    "ORDER BY C1_COD", ref respostaBanco))
            {
                atualizaCamposCursos();
            }
        }

        private void pesquisaCursosGrid(String campo, String condicao, String deletados)
        {
            if (banco.executeComando("SELECT C1_COD AS 'Codigo', " +
                        "C1_NOME AS 'Curso'" +
                    "FROM htc1 " +
                    "WHERE " + campo + " LIKE '%" + condicao + "%' " +                        
                    "ORDER BY C1_COD", ref adaptador))
            {
                atualizaGridGeral();
            }
        }

        // DISCIPLINA  DISCIPLINA  DISCIPLINA  DISCIPLINA  DISCIPLINA  DISCIPLINA  DISCIPLINA  DISCIPLINA  //
        // DISCIPLINA  DISCIPLINA  DISCIPLINA  DISCIPLINA  DISCIPLINA  DISCIPLINA  DISCIPLINA  DISCIPLINA  //
        private void pesquisaDisciplinasEdit(String campo, String condicao, String deletados)
        {
            if (banco.executeComando("SELECT htc2.*, C1_NOME " +
                    "FROM htc2 " +
                        "INNER JOIN htc1 " +
                            "ON C2_CURSO = C1_COD " +
                    "WHERE " + campo + " LIKE '%" + condicao + 
                        "%' " +
                    " ORDER BY C2_COD", ref respostaBanco))
            {
                atualizaCamposDisciplinas();
            }
        }

        private void pesquisaDisciplinasGrid(String campo, String condicao, String deletados)
        {
            if (banco.executeComando("SELECT C2_COD AS 'Codigo', " + 
                        "C2_NOME AS 'Disciplina', C1_NOME AS 'Curso'" +
                    "FROM htc2 " +
                        "INNER JOIN htc1 " +
                            "ON C2_CURSO = C1_COD " +
                    "WHERE " + campo + " LIKE '%" + condicao + "%' " +                      
                    "ORDER BY C2_COD", ref adaptador))
            {
                atualizaGridGeral();
            }
        }

        // MATÉRIA  MATÉRIA  MATÉRIA  MATÉRIA  MATÉRIA  MATÉRIA  MATÉRIA  MATÉRIA  MATÉRIA  MATÉRIA  MATÉRIA  //
        // MATÉRIA  MATÉRIA  MATÉRIA  MATÉRIA  MATÉRIA  MATÉRIA  MATÉRIA  MATÉRIA  MATÉRIA  MATÉRIA  MATÉRIA  //
        private void pesquisaMateriasEdit(String campo, String condicao, String deletados)
        {
            if (banco.executeComando("SELECT htc3.*, C2_NOME " +
                        "FROM htc3 " +
                            "INNER JOIN htc2 " +
                                "ON C3_DISCIP = C2_COD " +
                        "WHERE " + campo + " LIKE '%" + condicao + 
                            "%' " +
                        "ORDER BY C3_COD", ref respostaBanco))
            {
                atualizaCamposMaterias();
            }
        }

        private void pesquisaMateriasGrid(String campo, String condicao, String deletados)
        {
            if (banco.executeComando("SELECT C3_COD AS 'Codigo', " +
                        "C3_NOME AS 'Materia', C2_NOME AS 'Disciplina', " + 
                        "C1_NOME AS 'Curso'" + 
                    "FROM htc3 " +
                        "INNER JOIN htc2 " +
                            "ON C3_DISCIP = C2_COD " +
                        "INNER JOIN htc1 " +
                            "ON C2_CURSO = C1_COD " +
                    "WHERE " + campo + " LIKE '%" + condicao + 
                        "%' " +
                    " ORDER BY C3_COD", ref adaptador))
            {
                atualizaGridGeral();
            }
        }

        // AVALIAÇÕES  AVALIAÇÕES  AVALIAÇÕES  AVALIAÇÕES  AVALIAÇÕES  AVALIAÇÕES  AVALIAÇÕES  AVALIAÇÕES  //
        // AVALIAÇÕES  AVALIAÇÕES  AVALIAÇÕES  AVALIAÇÕES  AVALIAÇÕES  AVALIAÇÕES  AVALIAÇÕES  AVALIAÇÕES  //
        private void pesquisaHistoricoEdit(String campo, String condicao, String deletados)
        {
            if (banco.executeComando("SELECT D1_COD, D1_DATA, " + 
                        "D2_QUESTAO, C2_NOME " + 
                    "FROM htd1 " + 
                        "INNER JOIN htd2 " +
                            "ON D2_PROVA = D1_COD " +
                        "INNER JOIN htb3 " +
                            "ON B3_QUESTAO = D2_QUESTAO " +
                        "INNER JOIN htc3 " +
                            "ON B3_MATERIA = C3_COD " + 
                        "INNER JOIN htc2 " + 
    	                    "ON C3_DISCIP = C2_COD " + 
                        "INNER JOIN htc1 " +
    	                    "ON C2_CURSO = C1_COD " +
                    "WHERE " + campo + " LIKE '%" + condicao + "%' " +  
                    "AND D2_DELETED = '" + deletados +  
                    "' GROUP BY D2_PROVA", ref respostaBanco))
            {
                atualizaCamposAvaliacao();
            }
        }

        private void pesquisaHistoricoGrid(String campo, String condicao, String deletados)
        {
            if (banco.executeComando("SELECT D1_COD AS 'Codigo', D1_DATA " +
                        "AS 'Data', D1_INEDITA AS 'Inedita?', " +
                        "D2_QUESTAO AS 'Questoes', C1_NOME AS 'Curso', " +
                        "C2_NOME AS 'Disciplina', B3_MATERIA AS 'Codigos Materias'" + 
                    "FROM htd2 " +
                        "INNER JOIN htd1 " +
                            "ON D2_PROVA = D1_COD " +
                        "INNER JOIN htb1 " +
                            "ON B1_COD = D2_QUESTAO " +
                        "INNER JOIN htb3 " +
                            "ON B3_QUESTAO = B1_COD " +
                        "INNER JOIN htc3 " +
                            "ON B3_MATERIA = C3_COD " +
                        "INNER JOIN htc2 " +
                            "ON C3_DISCIP = C2_COD " +
                        "INNER JOIN htc1 " +
                            "ON C2_CURSO = C1_COD " +
                    "WHERE " + campo + " LIKE '%" + condicao + "%' " +
                        "AND D2_DELETED = '" + deletados + "' " +
                    "GROUP BY D2_PROVA", ref adaptador))
            {
                atualizaGridGeral();
            }
        }

        private void pesquisaHistoricoGrid(String campo, Boolean isNull, String deletados)
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
                                "ON C3_DISCIP = C2_COD " +
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
                                "ON C3_DISCIP = C2_COD " +
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

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            ativaDesativaEdicaoQuestao(false);
        }
    }
}
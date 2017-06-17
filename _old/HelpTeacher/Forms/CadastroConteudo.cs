using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HelpTeacher.Classes;

namespace HelpTeacher.Forms
{
    public partial class CadastroConteudo : Form
    {
        private ConexaoBanco banco = new ConexaoBanco();
        private MySql.Data.MySqlClient.MySqlDataReader respostaBanco;
        AutoCompleteStringCollection collection = new AutoCompleteStringCollection();

        public CadastroConteudo(int pag)
        {
            InitializeComponent();

            if (pag == 1)
                tabControlConteudo.SelectedTab = tabCursos;
            else if (pag == 2)
                tabControlConteudo.SelectedTab = tabDisciplinas;
            else
                tabControlConteudo.SelectedTab = tabMaterias;
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
            if (cadastraCurso())
            {
                limparForm();
                atualizaCodigoCurso();
                Mensagem.cadastradoEfetuado();
            }
        }

        private void btnCancelarCurso_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private Boolean cadastraCurso()
        {
            if (podeCadastrar())
            {
                if (banco.executeComando("INSERT INTO htc1 VALUES (NULL,'" +
                            txtNomeCurso.Text + "')"))
                {
                    return true;
                }
            }
            return false;
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
            if (banco.executeComando("SELECT C1_NOME " +
                            "FROM htc1", ref respostaBanco))
            {
                if (respostaBanco.HasRows)
                {
                    while (respostaBanco.Read())
                    {
                        collection.Add(respostaBanco.GetString(0));
                    }
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

        private void cmbCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            autoCompleteDisciplinas();
        }

        private void bntSalvarDisc_Click(object sender, EventArgs e)
        {
            if (cadastraDisciplina())
            {
                limparForm();
                atualizaCodigoDisciplina();
                Mensagem.cadastradoEfetuado();
            }
        }

        private void btnCancelarDisc_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private Boolean carregaCursos()
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
                    respostaBanco.Close();
                    banco.fechaConexao();
                    return true;
                }
                respostaBanco.Close();
                banco.fechaConexao();
            }
            return false;
        }

        private Boolean cadastraDisciplina()
        {
            if (podeCadastrar())
            {
                String[] codigoCurso = cmbCurso.Text.Split(new char[] { '(', ')' },
                            StringSplitOptions.RemoveEmptyEntries);

                if (banco.executeComando("INSERT INTO htc2 VALUES (NULL, '" +
                            txtNomeDisciplina.Text + "', " +
                            codigoCurso[0] + ")"))
                {
                    return true;
                }
                return false;
            }
            return false;
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
            if (!cmbCurso.Text.Equals(""))
            {
                String[] codigoCurso = cmbCurso.Text.Split(new char[] { '(', ')' },
                                StringSplitOptions.RemoveEmptyEntries);

                if (banco.executeComando("SELECT C2_NOME " +
                                "FROM htc2 " +
                                "WHERE C2_CURSO = " + codigoCurso[0], ref respostaBanco))
                {
                    if (respostaBanco.HasRows)
                    {
                        while (respostaBanco.Read())
                        {
                            collection.Add(respostaBanco.GetString(0));
                        }
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

        private void btnCancelarMateria_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private Boolean carregaDisciplinas()
        {
            if (banco.executeComando("SELECT C2_COD, C2_NOME " +
                        "FROM htc2", ref respostaBanco))
            {

                if (respostaBanco.HasRows)
                {
                    cmbDisciplina.Items.Clear();
                    while (respostaBanco.Read())
                    {
                        cmbDisciplina.Items.Add("(" + respostaBanco.GetString(0) + ") " + respostaBanco.GetString(1));
                    }
                    cmbDisciplina.SelectedIndex = 0;
                    respostaBanco.Close();
                    banco.fechaConexao();
                    return true;
                }
                respostaBanco.Close();
                banco.fechaConexao();
            }
            return false;
        }

        private void buscaCurso()
        {
            String[] codigoCurso = cmbDisciplina.Text.Split(new char[] { '(', ')', ' ' },
                            StringSplitOptions.RemoveEmptyEntries);

            if (banco.executeComando("SELECT C1_NOME " +
                        "FROM htc1 " +
                            "INNER JOIN htc2 " +
                                "ON C1_COD = C2_CURSO " +
                        "WHERE C2_COD = " + codigoCurso[0], ref respostaBanco))
            {
                if (respostaBanco.HasRows)
                {
                    respostaBanco.Read();
                    txtCurso.Text = respostaBanco.GetString(0);
                }
                respostaBanco.Close();
                banco.fechaConexao();
            }
        }

        private Boolean cadastraMateria()
        {
            if (podeCadastrar())
            {
                String[] codigoDisciplina = cmbDisciplina.Text.Split(new char[] { '(', ')' },
                            StringSplitOptions.RemoveEmptyEntries);

                if (banco.executeComando("INSERT INTO htc3 VALUES (NULL, '" +
                            txtNomeMateria.Text + "'," + codigoDisciplina[0] + ",'" + 0 + "')"))
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
            if (!cmbCurso.Text.Equals(""))
            {
                String[] codigoDisciplina = cmbDisciplina.Text.Split(new char[] { '(', ')' },
                                StringSplitOptions.RemoveEmptyEntries);

                if (banco.executeComando("SELECT C3_NOME " +
                                "FROM htc3 " +
                                "WHERE C3_DISCIP = " + codigoDisciplina[0], ref respostaBanco))
                {
                    if (respostaBanco.HasRows)
                    {
                        while (respostaBanco.Read())
                        {
                            collection.Add(respostaBanco.GetString(0));
                        }
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

        private Boolean podeCadastrar()
        {
            if (tabControlConteudo.SelectedTab == tabCursos)
            {
                /* Sem nome */
                if (txtNomeCurso.Text.Equals(""))
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
                return true;
            }
            else if (tabControlConteudo.SelectedTab == tabDisciplinas)
            {
                String[] codigoCurso = cmbCurso.Text.Split(new char[] { '(', ')' },
                            StringSplitOptions.RemoveEmptyEntries);

                /* Sem nome */
                if (txtNomeDisciplina.Text.Equals(""))
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
                return true;
            }
            else
            {
                String[] codigoDisciplina = cmbDisciplina.Text.Split(new char[] { '(', ')' },
                            StringSplitOptions.RemoveEmptyEntries);

                /* Sem nome */
                if (txtNomeMateria.Text.Equals(""))
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
                return true;
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
    }
}

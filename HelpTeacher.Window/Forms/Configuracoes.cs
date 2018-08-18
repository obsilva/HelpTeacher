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
using HelpTeacher.Domain.Entities;

namespace HelpTeacher.Forms
{
    public partial class Configuracoes : Form
    {
        private ConexaoBanco banco = new ConexaoBanco();
        private MySql.Data.MySqlClient.MySqlDataReader respostaBanco;

        public Configuracoes()
        {
            InitializeComponent();
        }

        private void Configuracoes_Load(object sender, EventArgs e)
        {
            txtLogin.Text = User.Instance.Username;
        }

        private void txtLogin_Click(object sender, EventArgs e)
        {
            MouseEventArgs evento = e as MouseEventArgs;
            txtLogin.Select(txtLogin.GetCharIndexFromPosition(evento.Location), 0);
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (alteraDados())
            {
                Mensagem.dadosAlterados();
                if (atualizaDadosUsuario())
                {
                    this.Close();
                }
                else
                {
                    Mensagem.erroAlteracao();
                }
            }
            else
            {
                Mensagem.erroAlteracao();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private Boolean alteraDados()
        {

            if (banco.executeComando("UPDATE hta1 SET A1_LOGIN = '" + txtLogin.Text +
                        "' WHERE A1_COD = " + User.Instance.ID.ToString()))
            {
                if (!(txtNovaSenha.Text.Equals("") && txtConfirmacao.Text.Equals("")))
                {
                    if (txtNovaSenha.Text == txtConfirmacao.Text)
                    {
                        if (!banco.executeComando("UPDATE hta1 SET A1_PWD = '" +
                                    MD5.gerarHash(txtNovaSenha.Text) + "' " +
                                    "WHERE A1_COD = " + User.Instance.ID.ToString()))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        Mensagem.senhasDiferentes();
                        return false;
                    }
                }
                if (chkStopBD.Checked)
                {
                    if (!banco.executeComando("UPDATE hta1 SET A1_STOPBD = '1'" +
                                "WHERE A1_COD = " + User.Instance.ID.ToString()))
                    {
                        return false;
                    }
                }
                else
                {
                    if (!banco.executeComando("UPDATE hta1 SET A1_STOPBD = '0' " +
                                "WHERE A1_COD = " + User.Instance.ID.ToString()))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        private Boolean atualizaDadosUsuario()
        {
            if (banco.executeComando("SELECT * " +
                        "FROM hta1 " +
                        "WHERE A1_COD = " + User.Instance.ID.ToString(), ref respostaBanco))
            {
                respostaBanco.Read();
				User.Instance.Username = respostaBanco["A1_LOGIN"].ToString();
				User.Instance.Password = respostaBanco["A1_PWD"].ToString();

                banco.fechaConexao();
                respostaBanco.Close();
                return true;
            }
            return false;
        }
    }
}

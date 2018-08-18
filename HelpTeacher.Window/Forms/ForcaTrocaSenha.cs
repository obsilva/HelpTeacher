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
    public partial class ForcaTrocaSenha : Form
    {
        private ConexaoBanco banco = new ConexaoBanco();

        public ForcaTrocaSenha()
        {
            InitializeComponent();
        }

        private void ForcaTrocaSenha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
        }

        private void txtConfirmacao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                trocaSenha();
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            trocaSenha();
        }
        
        private void trocaSenha()
        {
            if (txtNovaSenha.Text == txtConfirmacao.Text)
            {
                banco.executeComando("UPDATE hta1 " +
                        "SET A1_PWD = '" + MD5.gerarHash(txtNovaSenha.Text) +
                            "', A1_ALTPWD = 0 " +
                        "WHERE A1_COD = " + User.Instance.ID.ToString());
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                Mensagem.senhasDiferentes();
            }
        }
    }
}

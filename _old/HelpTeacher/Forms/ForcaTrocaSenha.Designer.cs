namespace HelpTeacher.Forms
{
    partial class ForcaTrocaSenha
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ForcaTrocaSenha));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNovaSenha = new System.Windows.Forms.TextBox();
            this.txtConfirmacao = new System.Windows.Forms.TextBox();
            this.btnAlterar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nova senha";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "Confirmar nova senha";
            // 
            // txtNovaSenha
            // 
            this.txtNovaSenha.Location = new System.Drawing.Point(38, 35);
            this.txtNovaSenha.Name = "txtNovaSenha";
            this.txtNovaSenha.PasswordChar = '*';
            this.txtNovaSenha.Size = new System.Drawing.Size(298, 22);
            this.txtNovaSenha.TabIndex = 2;
            // 
            // txtConfirmacao
            // 
            this.txtConfirmacao.Location = new System.Drawing.Point(38, 106);
            this.txtConfirmacao.Name = "txtConfirmacao";
            this.txtConfirmacao.PasswordChar = '*';
            this.txtConfirmacao.Size = new System.Drawing.Size(298, 22);
            this.txtConfirmacao.TabIndex = 3;
            this.txtConfirmacao.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtConfirmacao_KeyPress);
            // 
            // btnAlterar
            // 
            this.btnAlterar.Location = new System.Drawing.Point(117, 149);
            this.btnAlterar.Name = "btnAlterar";
            this.btnAlterar.Size = new System.Drawing.Size(137, 35);
            this.btnAlterar.TabIndex = 4;
            this.btnAlterar.Text = "ALTERAR SENHA";
            this.btnAlterar.UseVisualStyleBackColor = true;
            this.btnAlterar.Click += new System.EventHandler(this.btnAlterar_Click);
            // 
            // ForcaTrocaSenha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 196);
            this.ControlBox = false;
            this.Controls.Add(this.btnAlterar);
            this.Controls.Add(this.txtConfirmacao);
            this.Controls.Add(this.txtNovaSenha);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Verdana", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "ForcaTrocaSenha";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Alterar Senha";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ForcaTrocaSenha_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNovaSenha;
        private System.Windows.Forms.TextBox txtConfirmacao;
        private System.Windows.Forms.Button btnAlterar;
    }
}
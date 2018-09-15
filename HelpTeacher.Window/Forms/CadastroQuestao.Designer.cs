namespace HelpTeacher.Forms
{
    partial class CadastroQuestao
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CadastroQuestao));
			this.grpTipoQuestao = new System.Windows.Forms.GroupBox();
			this.radObjetiva = new System.Windows.Forms.RadioButton();
			this.radDissertativa = new System.Windows.Forms.RadioButton();
			this.cmbCursos = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.chkDisciplinas = new System.Windows.Forms.CheckedListBox();
			this.chkMaterias = new System.Windows.Forms.CheckedListBox();
			this.txtQuestao = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txtArquivo1 = new System.Windows.Forms.TextBox();
			this.txtArquivo2 = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.btnArquivo1 = new System.Windows.Forms.Button();
			this.btnArquivo2 = new System.Windows.Forms.Button();
			this.opnArquivo = new System.Windows.Forms.OpenFileDialog();
			this.pnlAlternativas = new System.Windows.Forms.Panel();
			this.txtAlternativaE = new System.Windows.Forms.TextBox();
			this.txtAlternativaD = new System.Windows.Forms.TextBox();
			this.txtAlternativaC = new System.Windows.Forms.TextBox();
			this.txtAlternativaB = new System.Windows.Forms.TextBox();
			this.txtAlternativaA = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.btnSalvar = new System.Windows.Forms.Button();
			this.btnCancelar = new System.Windows.Forms.Button();
			this.label11 = new System.Windows.Forms.Label();
			this.txtCodQuestao = new System.Windows.Forms.TextBox();
			this.btnBackspace1 = new System.Windows.Forms.Button();
			this.btnBackspace2 = new System.Windows.Forms.Button();
			this.grpTipoQuestao.SuspendLayout();
			this.pnlAlternativas.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpTipoQuestao
			// 
			this.grpTipoQuestao.Controls.Add(this.radObjetiva);
			this.grpTipoQuestao.Controls.Add(this.radDissertativa);
			this.grpTipoQuestao.Location = new System.Drawing.Point(14, 52);
			this.grpTipoQuestao.Name = "grpTipoQuestao";
			this.grpTipoQuestao.Size = new System.Drawing.Size(214, 108);
			this.grpTipoQuestao.TabIndex = 0;
			this.grpTipoQuestao.TabStop = false;
			this.grpTipoQuestao.Text = "Tipo de Questão";
			// 
			// radObjetiva
			// 
			this.radObjetiva.AutoSize = true;
			this.radObjetiva.Location = new System.Drawing.Point(25, 67);
			this.radObjetiva.Name = "radObjetiva";
			this.radObjetiva.Size = new System.Drawing.Size(78, 18);
			this.radObjetiva.TabIndex = 1;
			this.radObjetiva.Text = "Objetiva";
			this.radObjetiva.UseVisualStyleBackColor = true;
			this.radObjetiva.CheckedChanged += new System.EventHandler(this.radObjetiva_CheckedChanged);
			// 
			// radDissertativa
			// 
			this.radDissertativa.AutoSize = true;
			this.radDissertativa.Checked = true;
			this.radDissertativa.Location = new System.Drawing.Point(25, 31);
			this.radDissertativa.Name = "radDissertativa";
			this.radDissertativa.Size = new System.Drawing.Size(100, 18);
			this.radDissertativa.TabIndex = 0;
			this.radDissertativa.TabStop = true;
			this.radDissertativa.Text = "Dissertativa";
			this.radDissertativa.UseVisualStyleBackColor = true;
			// 
			// cmbCursos
			// 
			this.cmbCursos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbCursos.FormattingEnabled = true;
			this.cmbCursos.Location = new System.Drawing.Point(14, 185);
			this.cmbCursos.Name = "cmbCursos";
			this.cmbCursos.Size = new System.Drawing.Size(214, 22);
			this.cmbCursos.TabIndex = 1;
			this.cmbCursos.SelectedIndexChanged += new System.EventHandler(this.cbbCursos_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(15, 168);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(44, 14);
			this.label1.TabIndex = 2;
			this.label1.Text = "Curso";
			// 
			// chkDisciplinas
			// 
			this.chkDisciplinas.CheckOnClick = true;
			this.chkDisciplinas.FormattingEnabled = true;
			this.chkDisciplinas.HorizontalScrollbar = true;
			this.chkDisciplinas.Location = new System.Drawing.Point(234, 16);
			this.chkDisciplinas.Name = "chkDisciplinas";
			this.chkDisciplinas.Size = new System.Drawing.Size(362, 191);
			this.chkDisciplinas.TabIndex = 2;
			this.chkDisciplinas.SelectedIndexChanged += new System.EventHandler(this.chkDisciplinas_SelectedIndexChanged);
			// 
			// chkMaterias
			// 
			this.chkMaterias.CheckOnClick = true;
			this.chkMaterias.FormattingEnabled = true;
			this.chkMaterias.HorizontalScrollbar = true;
			this.chkMaterias.Location = new System.Drawing.Point(602, 16);
			this.chkMaterias.Name = "chkMaterias";
			this.chkMaterias.Size = new System.Drawing.Size(372, 191);
			this.chkMaterias.TabIndex = 3;
			// 
			// txtQuestao
			// 
			this.txtQuestao.AcceptsTab = true;
			this.txtQuestao.Location = new System.Drawing.Point(14, 248);
			this.txtQuestao.Multiline = true;
			this.txtQuestao.Name = "txtQuestao";
			this.txtQuestao.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtQuestao.Size = new System.Drawing.Size(497, 103);
			this.txtQuestao.TabIndex = 4;
			this.txtQuestao.Click += new System.EventHandler(this.setaCursorClick);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(14, 231);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(61, 14);
			this.label2.TabIndex = 7;
			this.label2.Text = "Questão";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(14, 380);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(66, 14);
			this.label3.TabIndex = 8;
			this.label3.Text = "Arquivo 1";
			// 
			// txtArquivo1
			// 
			this.txtArquivo1.Location = new System.Drawing.Point(65, 397);
			this.txtArquivo1.Name = "txtArquivo1";
			this.txtArquivo1.ReadOnly = true;
			this.txtArquivo1.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
			this.txtArquivo1.Size = new System.Drawing.Size(391, 22);
			this.txtArquivo1.TabIndex = 6;
			// 
			// txtArquivo2
			// 
			this.txtArquivo2.Location = new System.Drawing.Point(65, 457);
			this.txtArquivo2.Name = "txtArquivo2";
			this.txtArquivo2.ReadOnly = true;
			this.txtArquivo2.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
			this.txtArquivo2.Size = new System.Drawing.Size(391, 22);
			this.txtArquivo2.TabIndex = 8;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(14, 440);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(66, 14);
			this.label4.TabIndex = 11;
			this.label4.Text = "Arquivo 2";
			// 
			// btnArquivo1
			// 
			this.btnArquivo1.Location = new System.Drawing.Point(17, 397);
			this.btnArquivo1.Name = "btnArquivo1";
			this.btnArquivo1.Size = new System.Drawing.Size(42, 22);
			this.btnArquivo1.TabIndex = 5;
			this.btnArquivo1.Text = "...";
			this.btnArquivo1.UseVisualStyleBackColor = true;
			this.btnArquivo1.Click += new System.EventHandler(this.btnArquivo1_Click);
			// 
			// btnArquivo2
			// 
			this.btnArquivo2.Location = new System.Drawing.Point(17, 457);
			this.btnArquivo2.Name = "btnArquivo2";
			this.btnArquivo2.Size = new System.Drawing.Size(42, 22);
			this.btnArquivo2.TabIndex = 7;
			this.btnArquivo2.Text = "...";
			this.btnArquivo2.UseVisualStyleBackColor = true;
			this.btnArquivo2.Click += new System.EventHandler(this.btnArquivo2_Click);
			// 
			// opnArquivo
			// 
			this.opnArquivo.FileName = "openFileDialog1";
			this.opnArquivo.Title = "Selecione um Arquivo";
			// 
			// pnlAlternativas
			// 
			this.pnlAlternativas.Controls.Add(this.txtAlternativaE);
			this.pnlAlternativas.Controls.Add(this.txtAlternativaD);
			this.pnlAlternativas.Controls.Add(this.txtAlternativaC);
			this.pnlAlternativas.Controls.Add(this.txtAlternativaB);
			this.pnlAlternativas.Controls.Add(this.txtAlternativaA);
			this.pnlAlternativas.Controls.Add(this.label10);
			this.pnlAlternativas.Controls.Add(this.label9);
			this.pnlAlternativas.Controls.Add(this.label8);
			this.pnlAlternativas.Controls.Add(this.label7);
			this.pnlAlternativas.Controls.Add(this.label6);
			this.pnlAlternativas.Controls.Add(this.label5);
			this.pnlAlternativas.Enabled = false;
			this.pnlAlternativas.Location = new System.Drawing.Point(517, 248);
			this.pnlAlternativas.Name = "pnlAlternativas";
			this.pnlAlternativas.Size = new System.Drawing.Size(457, 235);
			this.pnlAlternativas.TabIndex = 9;
			// 
			// txtAlternativaE
			// 
			this.txtAlternativaE.AcceptsTab = true;
			this.txtAlternativaE.Location = new System.Drawing.Point(34, 186);
			this.txtAlternativaE.Multiline = true;
			this.txtAlternativaE.Name = "txtAlternativaE";
			this.txtAlternativaE.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtAlternativaE.Size = new System.Drawing.Size(422, 35);
			this.txtAlternativaE.TabIndex = 4;
			this.txtAlternativaE.Click += new System.EventHandler(this.setaCursorClick);
			// 
			// txtAlternativaD
			// 
			this.txtAlternativaD.AcceptsTab = true;
			this.txtAlternativaD.Location = new System.Drawing.Point(34, 145);
			this.txtAlternativaD.Multiline = true;
			this.txtAlternativaD.Name = "txtAlternativaD";
			this.txtAlternativaD.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtAlternativaD.Size = new System.Drawing.Size(422, 35);
			this.txtAlternativaD.TabIndex = 3;
			this.txtAlternativaD.Click += new System.EventHandler(this.setaCursorClick);
			// 
			// txtAlternativaC
			// 
			this.txtAlternativaC.AcceptsTab = true;
			this.txtAlternativaC.Location = new System.Drawing.Point(34, 104);
			this.txtAlternativaC.Multiline = true;
			this.txtAlternativaC.Name = "txtAlternativaC";
			this.txtAlternativaC.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtAlternativaC.Size = new System.Drawing.Size(422, 35);
			this.txtAlternativaC.TabIndex = 2;
			this.txtAlternativaC.Click += new System.EventHandler(this.setaCursorClick);
			// 
			// txtAlternativaB
			// 
			this.txtAlternativaB.AcceptsTab = true;
			this.txtAlternativaB.Location = new System.Drawing.Point(34, 63);
			this.txtAlternativaB.Multiline = true;
			this.txtAlternativaB.Name = "txtAlternativaB";
			this.txtAlternativaB.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtAlternativaB.Size = new System.Drawing.Size(422, 35);
			this.txtAlternativaB.TabIndex = 1;
			this.txtAlternativaB.Click += new System.EventHandler(this.setaCursorClick);
			// 
			// txtAlternativaA
			// 
			this.txtAlternativaA.AcceptsTab = true;
			this.txtAlternativaA.Location = new System.Drawing.Point(34, 22);
			this.txtAlternativaA.Multiline = true;
			this.txtAlternativaA.Name = "txtAlternativaA";
			this.txtAlternativaA.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtAlternativaA.Size = new System.Drawing.Size(422, 35);
			this.txtAlternativaA.TabIndex = 0;
			this.txtAlternativaA.Click += new System.EventHandler(this.setaCursorClick);
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(3, 189);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(20, 14);
			this.label10.TabIndex = 5;
			this.label10.Text = "e)";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(3, 148);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(20, 14);
			this.label9.TabIndex = 4;
			this.label9.Text = "d)";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(3, 107);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(18, 14);
			this.label8.TabIndex = 3;
			this.label8.Text = "c)";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(3, 66);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(20, 14);
			this.label7.TabIndex = 2;
			this.label7.Text = "b)";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(3, 25);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(20, 14);
			this.label6.TabIndex = 1;
			this.label6.Text = "a)";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(3, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(82, 14);
			this.label5.TabIndex = 0;
			this.label5.Text = "Alternativas";
			// 
			// btnSalvar
			// 
			this.btnSalvar.Image = ((System.Drawing.Image)(resources.GetObject("btnSalvar.Image")));
			this.btnSalvar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnSalvar.Location = new System.Drawing.Point(379, 511);
			this.btnSalvar.Name = "btnSalvar";
			this.btnSalvar.Size = new System.Drawing.Size(120, 50);
			this.btnSalvar.TabIndex = 10;
			this.btnSalvar.Text = "Salvar";
			this.btnSalvar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnSalvar.UseVisualStyleBackColor = true;
			this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
			// 
			// btnCancelar
			// 
			this.btnCancelar.Location = new System.Drawing.Point(574, 511);
			this.btnCancelar.Name = "btnCancelar";
			this.btnCancelar.Size = new System.Drawing.Size(120, 50);
			this.btnCancelar.TabIndex = 16;
			this.btnCancelar.Text = "Cancelar";
			this.btnCancelar.UseVisualStyleBackColor = true;
			this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(15, 19);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(134, 14);
			this.label11.TabIndex = 14;
			this.label11.Text = "Código da Questão:";
			// 
			// txtCodQuestao
			// 
			this.txtCodQuestao.Location = new System.Drawing.Point(155, 16);
			this.txtCodQuestao.Name = "txtCodQuestao";
			this.txtCodQuestao.ReadOnly = true;
			this.txtCodQuestao.Size = new System.Drawing.Size(65, 22);
			this.txtCodQuestao.TabIndex = 17;
			this.txtCodQuestao.Text = "1";
			// 
			// btnBackspace1
			// 
			this.btnBackspace1.Image = ((System.Drawing.Image)(resources.GetObject("btnBackspace1.Image")));
			this.btnBackspace1.Location = new System.Drawing.Point(465, 396);
			this.btnBackspace1.Name = "btnBackspace1";
			this.btnBackspace1.Size = new System.Drawing.Size(46, 22);
			this.btnBackspace1.TabIndex = 18;
			this.btnBackspace1.UseVisualStyleBackColor = true;
			this.btnBackspace1.Click += new System.EventHandler(this.btnBackspace1_Click);
			// 
			// btnBackspace2
			// 
			this.btnBackspace2.Image = ((System.Drawing.Image)(resources.GetObject("btnBackspace2.Image")));
			this.btnBackspace2.Location = new System.Drawing.Point(465, 456);
			this.btnBackspace2.Name = "btnBackspace2";
			this.btnBackspace2.Size = new System.Drawing.Size(46, 22);
			this.btnBackspace2.TabIndex = 19;
			this.btnBackspace2.UseVisualStyleBackColor = true;
			this.btnBackspace2.Click += new System.EventHandler(this.btnBackspace2_Click);
			// 
			// CadastroQuestao
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(989, 582);
			this.Controls.Add(this.btnBackspace2);
			this.Controls.Add(this.btnBackspace1);
			this.Controls.Add(this.txtCodQuestao);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.btnCancelar);
			this.Controls.Add(this.btnSalvar);
			this.Controls.Add(this.pnlAlternativas);
			this.Controls.Add(this.btnArquivo2);
			this.Controls.Add(this.btnArquivo1);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.txtArquivo2);
			this.Controls.Add(this.txtArquivo1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtQuestao);
			this.Controls.Add(this.chkMaterias);
			this.Controls.Add(this.chkDisciplinas);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cmbCursos);
			this.Controls.Add(this.grpTipoQuestao);
			this.Font = new System.Drawing.Font("Verdana", 9F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "CadastroQuestao";
			this.Text = "Cadastro de Questões";
			this.grpTipoQuestao.ResumeLayout(false);
			this.grpTipoQuestao.PerformLayout();
			this.pnlAlternativas.ResumeLayout(false);
			this.pnlAlternativas.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpTipoQuestao;
        private System.Windows.Forms.RadioButton radObjetiva;
        private System.Windows.Forms.RadioButton radDissertativa;
        private System.Windows.Forms.ComboBox cmbCursos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox chkDisciplinas;
        private System.Windows.Forms.CheckedListBox chkMaterias;
        private System.Windows.Forms.TextBox txtQuestao;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtArquivo1;
        private System.Windows.Forms.TextBox txtArquivo2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnArquivo1;
        private System.Windows.Forms.Button btnArquivo2;
        private System.Windows.Forms.OpenFileDialog opnArquivo;
        private System.Windows.Forms.Panel pnlAlternativas;
        private System.Windows.Forms.TextBox txtAlternativaA;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtAlternativaE;
        private System.Windows.Forms.TextBox txtAlternativaD;
        private System.Windows.Forms.TextBox txtAlternativaC;
        private System.Windows.Forms.TextBox txtAlternativaB;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtCodQuestao;
        private System.Windows.Forms.Button btnBackspace1;
        private System.Windows.Forms.Button btnBackspace2;
    }
}
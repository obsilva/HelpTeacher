namespace HelpTeacher.Forms
{
    partial class GerarAvaliacao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GerarAvaliacao));
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCurso = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbDisciplina = new System.Windows.Forms.ComboBox();
            this.chkMaterias = new System.Windows.Forms.CheckedListBox();
            this.chkAvaliacaoInedita = new System.Windows.Forms.CheckBox();
            this.bntGerarAvaliacao = new System.Windows.Forms.Button();
            this.radObjetiva = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radMista = new System.Windows.Forms.RadioButton();
            this.radDissertativa = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lstCodigos = new System.Windows.Forms.ListBox();
            this.lstCodigosAleatorios = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTotalQuestoes = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(182, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Curso";
            // 
            // cmbCurso
            // 
            this.cmbCurso.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCurso.FormattingEnabled = true;
            this.cmbCurso.Location = new System.Drawing.Point(185, 28);
            this.cmbCurso.Name = "cmbCurso";
            this.cmbCurso.Size = new System.Drawing.Size(270, 22);
            this.cmbCurso.TabIndex = 4;
            this.cmbCurso.SelectedIndexChanged += new System.EventHandler(this.cmbCurso_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(182, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Disciplina";
            // 
            // cmbDisciplina
            // 
            this.cmbDisciplina.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDisciplina.FormattingEnabled = true;
            this.cmbDisciplina.Location = new System.Drawing.Point(185, 82);
            this.cmbDisciplina.Name = "cmbDisciplina";
            this.cmbDisciplina.Size = new System.Drawing.Size(270, 22);
            this.cmbDisciplina.TabIndex = 6;
            this.cmbDisciplina.SelectedIndexChanged += new System.EventHandler(this.cmbDisciplina_SelectedIndexChanged);
            // 
            // chkMaterias
            // 
            this.chkMaterias.CheckOnClick = true;
            this.chkMaterias.FormattingEnabled = true;
            this.chkMaterias.Location = new System.Drawing.Point(461, 15);
            this.chkMaterias.Name = "chkMaterias";
            this.chkMaterias.Size = new System.Drawing.Size(305, 140);
            this.chkMaterias.TabIndex = 9;
            this.chkMaterias.SelectedIndexChanged += new System.EventHandler(this.chkMaterias_SelectedIndexChanged);
            // 
            // chkAvaliacaoInedita
            // 
            this.chkAvaliacaoInedita.AutoSize = true;
            this.chkAvaliacaoInedita.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAvaliacaoInedita.Location = new System.Drawing.Point(270, 131);
            this.chkAvaliacaoInedita.Name = "chkAvaliacaoInedita";
            this.chkAvaliacaoInedita.Size = new System.Drawing.Size(131, 20);
            this.chkAvaliacaoInedita.TabIndex = 10;
            this.chkAvaliacaoInedita.Text = "Avaliação Inédita";
            this.chkAvaliacaoInedita.UseVisualStyleBackColor = true;
            this.chkAvaliacaoInedita.CheckedChanged += new System.EventHandler(this.chkAvaUnica_CheckedChanged);
            // 
            // bntGerarAvaliacao
            // 
            this.bntGerarAvaliacao.Location = new System.Drawing.Point(13, 361);
            this.bntGerarAvaliacao.Name = "bntGerarAvaliacao";
            this.bntGerarAvaliacao.Size = new System.Drawing.Size(142, 51);
            this.bntGerarAvaliacao.TabIndex = 13;
            this.bntGerarAvaliacao.Text = "Gerar Avaliação";
            this.bntGerarAvaliacao.UseVisualStyleBackColor = true;
            this.bntGerarAvaliacao.Click += new System.EventHandler(this.bntGerarAvaliacao_Click);
            // 
            // radObjetiva
            // 
            this.radObjetiva.AutoSize = true;
            this.radObjetiva.Location = new System.Drawing.Point(9, 48);
            this.radObjetiva.Name = "radObjetiva";
            this.radObjetiva.Size = new System.Drawing.Size(78, 18);
            this.radObjetiva.TabIndex = 16;
            this.radObjetiva.Text = "Objetiva";
            this.radObjetiva.UseVisualStyleBackColor = true;
            this.radObjetiva.CheckedChanged += new System.EventHandler(this.rbObjetiva_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radMista);
            this.groupBox1.Controls.Add(this.radDissertativa);
            this.groupBox1.Controls.Add(this.radObjetiva);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(164, 107);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tipo de prova";
            // 
            // radMista
            // 
            this.radMista.AutoSize = true;
            this.radMista.Checked = true;
            this.radMista.Location = new System.Drawing.Point(9, 24);
            this.radMista.Name = "radMista";
            this.radMista.Size = new System.Drawing.Size(58, 18);
            this.radMista.TabIndex = 18;
            this.radMista.TabStop = true;
            this.radMista.Text = "Mista";
            this.radMista.UseVisualStyleBackColor = true;
            // 
            // radDissertativa
            // 
            this.radDissertativa.AutoSize = true;
            this.radDissertativa.Location = new System.Drawing.Point(9, 79);
            this.radDissertativa.Name = "radDissertativa";
            this.radDissertativa.Size = new System.Drawing.Size(100, 18);
            this.radDissertativa.TabIndex = 17;
            this.radDissertativa.Text = "Dissertativa";
            this.radDissertativa.UseVisualStyleBackColor = true;
            this.radDissertativa.CheckedChanged += new System.EventHandler(this.rbDissertativa_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 176);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(143, 14);
            this.label3.TabIndex = 21;
            this.label3.Text = "Questões Disponíveis";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 266);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 14);
            this.label5.TabIndex = 22;
            this.label5.Text = "Questões Usadas";
            // 
            // lstCodigos
            // 
            this.lstCodigos.FormattingEnabled = true;
            this.lstCodigos.ItemHeight = 14;
            this.lstCodigos.Location = new System.Drawing.Point(13, 193);
            this.lstCodigos.Name = "lstCodigos";
            this.lstCodigos.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lstCodigos.Size = new System.Drawing.Size(140, 60);
            this.lstCodigos.TabIndex = 23;
            // 
            // lstCodigosAleatorios
            // 
            this.lstCodigosAleatorios.FormattingEnabled = true;
            this.lstCodigosAleatorios.ItemHeight = 14;
            this.lstCodigosAleatorios.Location = new System.Drawing.Point(10, 283);
            this.lstCodigosAleatorios.Name = "lstCodigosAleatorios";
            this.lstCodigosAleatorios.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lstCodigosAleatorios.Size = new System.Drawing.Size(143, 60);
            this.lstCodigosAleatorios.TabIndex = 27;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 133);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 14);
            this.label7.TabIndex = 32;
            this.label7.Text = "Total Questões";
            // 
            // txtTotalQuestoes
            // 
            this.txtTotalQuestoes.Location = new System.Drawing.Point(111, 129);
            this.txtTotalQuestoes.Name = "txtTotalQuestoes";
            this.txtTotalQuestoes.ReadOnly = true;
            this.txtTotalQuestoes.Size = new System.Drawing.Size(65, 22);
            this.txtTotalQuestoes.TabIndex = 33;
            // 
            // GerarAvaliacao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(782, 444);
            this.Controls.Add(this.txtTotalQuestoes);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lstCodigosAleatorios);
            this.Controls.Add(this.lstCodigos);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.bntGerarAvaliacao);
            this.Controls.Add(this.chkAvaliacaoInedita);
            this.Controls.Add(this.chkMaterias);
            this.Controls.Add(this.cmbDisciplina);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbCurso);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Verdana", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "GerarAvaliacao";
            this.Text = "Gerar Avaliação";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbCurso;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbDisciplina;
        private System.Windows.Forms.CheckedListBox chkMaterias;
        private System.Windows.Forms.CheckBox chkAvaliacaoInedita;
        private System.Windows.Forms.Button bntGerarAvaliacao;
        private System.Windows.Forms.RadioButton radObjetiva;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radMista;
        private System.Windows.Forms.RadioButton radDissertativa;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox lstCodigos;
        private System.Windows.Forms.ListBox lstCodigosAleatorios;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtTotalQuestoes;
    }
}
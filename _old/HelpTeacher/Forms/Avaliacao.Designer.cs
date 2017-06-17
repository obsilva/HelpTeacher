namespace HelpTeacher.Forms
{
    partial class Avaliacao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Avaliacao));
            this.bntGerarAvaliacao = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.tabCabecalho = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radPdf = new System.Windows.Forms.RadioButton();
            this.radWord = new System.Windows.Forms.RadioButton();
            this.txtTotalQuestoes = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lstCodigosAleatorios = new System.Windows.Forms.ListBox();
            this.lstCodigos = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radMista = new System.Windows.Forms.RadioButton();
            this.radDissertativa = new System.Windows.Forms.RadioButton();
            this.radObjetiva = new System.Windows.Forms.RadioButton();
            this.chkAvaliacaoInedita = new System.Windows.Forms.CheckBox();
            this.chkMaterias = new System.Windows.Forms.CheckedListBox();
            this.cmbDisciplina = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbCurso = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbEscola = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbProfessor = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnIcone = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.pictIcone = new System.Windows.Forms.PictureBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbValorAvaliacao = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dataAvaliacao = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.picCabecalho = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.tabCabecalho.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictIcone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCabecalho)).BeginInit();
            this.SuspendLayout();
            // 
            // bntGerarAvaliacao
            // 
            this.bntGerarAvaliacao.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bntGerarAvaliacao.Image = ((System.Drawing.Image)(resources.GetObject("bntGerarAvaliacao.Image")));
            this.bntGerarAvaliacao.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bntGerarAvaliacao.Location = new System.Drawing.Point(113, 592);
            this.bntGerarAvaliacao.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bntGerarAvaliacao.Name = "bntGerarAvaliacao";
            this.bntGerarAvaliacao.Size = new System.Drawing.Size(145, 60);
            this.bntGerarAvaliacao.TabIndex = 14;
            this.bntGerarAvaliacao.Text = "Gerar";
            this.bntGerarAvaliacao.UseVisualStyleBackColor = true;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(357, 592);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(137, 60);
            this.btnCancelar.TabIndex = 17;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // tabCabecalho
            // 
            this.tabCabecalho.Controls.Add(this.tabPage1);
            this.tabCabecalho.Controls.Add(this.tabPage2);
            this.tabCabecalho.Controls.Add(this.tabPage3);
            this.tabCabecalho.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabCabecalho.Location = new System.Drawing.Point(16, 15);
            this.tabCabecalho.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabCabecalho.Name = "tabCabecalho";
            this.tabCabecalho.SelectedIndex = 0;
            this.tabCabecalho.Size = new System.Drawing.Size(586, 573);
            this.tabCabecalho.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.txtTotalQuestoes);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.lstCodigosAleatorios);
            this.tabPage1.Controls.Add(this.lstCodigos);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.chkAvaliacaoInedita);
            this.tabPage1.Controls.Add(this.chkMaterias);
            this.tabPage1.Controls.Add(this.cmbDisciplina);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.cmbCurso);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(578, 544);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Questões";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.picCabecalho);
            this.tabPage2.Controls.Add(this.tbEscola);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.dataAvaliacao);
            this.tabPage2.Controls.Add(this.tbProfessor);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.tbValorAvaliacao);
            this.tabPage2.Controls.Add(this.btnIcone);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.pictIcone);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(578, 544);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Cabeçalho";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(578, 544);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Instruções";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radPdf);
            this.groupBox4.Controls.Add(this.radWord);
            this.groupBox4.Location = new System.Drawing.Point(22, 162);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(164, 106);
            this.groupBox4.TabIndex = 47;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Gerar documento";
            // 
            // radPdf
            // 
            this.radPdf.AutoSize = true;
            this.radPdf.Location = new System.Drawing.Point(12, 63);
            this.radPdf.Name = "radPdf";
            this.radPdf.Size = new System.Drawing.Size(51, 20);
            this.radPdf.TabIndex = 1;
            this.radPdf.Text = "PDF";
            this.radPdf.UseVisualStyleBackColor = true;
            // 
            // radWord
            // 
            this.radWord.AutoSize = true;
            this.radWord.Checked = true;
            this.radWord.Location = new System.Drawing.Point(12, 30);
            this.radWord.Name = "radWord";
            this.radWord.Size = new System.Drawing.Size(59, 20);
            this.radWord.TabIndex = 0;
            this.radWord.TabStop = true;
            this.radWord.Text = "Word";
            this.radWord.UseVisualStyleBackColor = true;
            // 
            // txtTotalQuestoes
            // 
            this.txtTotalQuestoes.Location = new System.Drawing.Point(148, 313);
            this.txtTotalQuestoes.Name = "txtTotalQuestoes";
            this.txtTotalQuestoes.ReadOnly = true;
            this.txtTotalQuestoes.Size = new System.Drawing.Size(38, 23);
            this.txtTotalQuestoes.TabIndex = 46;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 322);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(108, 16);
            this.label7.TabIndex = 45;
            this.label7.Text = "Total Questões";
            // 
            // lstCodigosAleatorios
            // 
            this.lstCodigosAleatorios.FormattingEnabled = true;
            this.lstCodigosAleatorios.ItemHeight = 16;
            this.lstCodigosAleatorios.Location = new System.Drawing.Point(112, 143);
            this.lstCodigosAleatorios.Name = "lstCodigosAleatorios";
            this.lstCodigosAleatorios.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lstCodigosAleatorios.Size = new System.Drawing.Size(61, 4);
            this.lstCodigosAleatorios.TabIndex = 44;
            this.lstCodigosAleatorios.Visible = false;
            // 
            // lstCodigos
            // 
            this.lstCodigos.FormattingEnabled = true;
            this.lstCodigos.ItemHeight = 16;
            this.lstCodigos.Location = new System.Drawing.Point(31, 143);
            this.lstCodigos.Name = "lstCodigos";
            this.lstCodigos.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lstCodigos.Size = new System.Drawing.Size(58, 4);
            this.lstCodigos.TabIndex = 43;
            this.lstCodigos.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radMista);
            this.groupBox1.Controls.Add(this.radDissertativa);
            this.groupBox1.Controls.Add(this.radObjetiva);
            this.groupBox1.Location = new System.Drawing.Point(22, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(164, 107);
            this.groupBox1.TabIndex = 42;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tipo de prova";
            // 
            // radMista
            // 
            this.radMista.AutoSize = true;
            this.radMista.Checked = true;
            this.radMista.Location = new System.Drawing.Point(9, 21);
            this.radMista.Name = "radMista";
            this.radMista.Size = new System.Drawing.Size(61, 20);
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
            this.radDissertativa.Size = new System.Drawing.Size(104, 20);
            this.radDissertativa.TabIndex = 17;
            this.radDissertativa.Text = "Dissertativa";
            this.radDissertativa.UseVisualStyleBackColor = true;
            // 
            // radObjetiva
            // 
            this.radObjetiva.AutoSize = true;
            this.radObjetiva.Location = new System.Drawing.Point(9, 49);
            this.radObjetiva.Name = "radObjetiva";
            this.radObjetiva.Size = new System.Drawing.Size(81, 20);
            this.radObjetiva.TabIndex = 16;
            this.radObjetiva.Text = "Objetiva";
            this.radObjetiva.UseVisualStyleBackColor = true;
            // 
            // chkAvaliacaoInedita
            // 
            this.chkAvaliacaoInedita.AutoSize = true;
            this.chkAvaliacaoInedita.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAvaliacaoInedita.Location = new System.Drawing.Point(22, 287);
            this.chkAvaliacaoInedita.Name = "chkAvaliacaoInedita";
            this.chkAvaliacaoInedita.Size = new System.Drawing.Size(131, 20);
            this.chkAvaliacaoInedita.TabIndex = 41;
            this.chkAvaliacaoInedita.Text = "Avaliação Inédita";
            this.chkAvaliacaoInedita.UseVisualStyleBackColor = true;
            // 
            // chkMaterias
            // 
            this.chkMaterias.CheckOnClick = true;
            this.chkMaterias.FormattingEnabled = true;
            this.chkMaterias.Location = new System.Drawing.Point(219, 162);
            this.chkMaterias.Name = "chkMaterias";
            this.chkMaterias.Size = new System.Drawing.Size(270, 130);
            this.chkMaterias.TabIndex = 40;
            // 
            // cmbDisciplina
            // 
            this.cmbDisciplina.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDisciplina.FormattingEnabled = true;
            this.cmbDisciplina.Location = new System.Drawing.Point(219, 103);
            this.cmbDisciplina.Name = "cmbDisciplina";
            this.cmbDisciplina.Size = new System.Drawing.Size(270, 24);
            this.cmbDisciplina.TabIndex = 39;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(216, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 16);
            this.label2.TabIndex = 38;
            this.label2.Text = "Disciplina";
            // 
            // cmbCurso
            // 
            this.cmbCurso.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCurso.FormattingEnabled = true;
            this.cmbCurso.Location = new System.Drawing.Point(219, 50);
            this.cmbCurso.Name = "cmbCurso";
            this.cmbCurso.Size = new System.Drawing.Size(270, 24);
            this.cmbCurso.TabIndex = 37;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(216, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 16);
            this.label1.TabIndex = 36;
            this.label1.Text = "Curso";
            // 
            // tbEscola
            // 
            this.tbEscola.Location = new System.Drawing.Point(177, 136);
            this.tbEscola.Name = "tbEscola";
            this.tbEscola.Size = new System.Drawing.Size(332, 23);
            this.tbEscola.TabIndex = 29;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(27, 139);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(152, 16);
            this.label10.TabIndex = 28;
            this.label10.Text = "Instituição de ensino:";
            // 
            // tbProfessor
            // 
            this.tbProfessor.Location = new System.Drawing.Point(177, 102);
            this.tbProfessor.Name = "tbProfessor";
            this.tbProfessor.Size = new System.Drawing.Size(332, 23);
            this.tbProfessor.TabIndex = 27;
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(162, 188);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(53, 22);
            this.button1.TabIndex = 25;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnIcone
            // 
            this.btnIcone.Location = new System.Drawing.Point(93, 188);
            this.btnIcone.Name = "btnIcone";
            this.btnIcone.Size = new System.Drawing.Size(53, 22);
            this.btnIcone.TabIndex = 24;
            this.btnIcone.Text = "...";
            this.btnIcone.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(27, 105);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(95, 16);
            this.label9.TabIndex = 26;
            this.label9.Text = "Professor(a):";
            // 
            // pictIcone
            // 
            this.pictIcone.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.pictIcone.Image = ((System.Drawing.Image)(resources.GetObject("pictIcone.Image")));
            this.pictIcone.Location = new System.Drawing.Point(93, 216);
            this.pictIcone.Name = "pictIcone";
            this.pictIcone.Size = new System.Drawing.Size(122, 91);
            this.pictIcone.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictIcone.TabIndex = 23;
            this.pictIcone.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(26, 192);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 16);
            this.label8.TabIndex = 22;
            this.label8.Text = "Ícone:";
            // 
            // tbValorAvaliacao
            // 
            this.tbValorAvaliacao.Location = new System.Drawing.Point(177, 65);
            this.tbValorAvaliacao.Name = "tbValorAvaliacao";
            this.tbValorAvaliacao.Size = new System.Drawing.Size(100, 23);
            this.tbValorAvaliacao.TabIndex = 21;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(135, 16);
            this.label6.TabIndex = 20;
            this.label6.Text = "Valor da Avaliação:";
            // 
            // dataAvaliacao
            // 
            this.dataAvaliacao.CalendarFont = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataAvaliacao.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dataAvaliacao.Location = new System.Drawing.Point(177, 31);
            this.dataAvaliacao.Name = "dataAvaliacao";
            this.dataAvaliacao.Size = new System.Drawing.Size(100, 23);
            this.dataAvaliacao.TabIndex = 19;
            this.dataAvaliacao.Value = new System.DateTime(2016, 6, 27, 0, 0, 0, 0);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 16);
            this.label4.TabIndex = 18;
            this.label4.Text = "Data da Avaliação:";
            // 
            // picCabecalho
            // 
            this.picCabecalho.Location = new System.Drawing.Point(29, 361);
            this.picCabecalho.Name = "picCabecalho";
            this.picCabecalho.Size = new System.Drawing.Size(495, 172);
            this.picCabecalho.TabIndex = 30;
            this.picCabecalho.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 333);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 16);
            this.label3.TabIndex = 31;
            this.label3.Text = "Modelo:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(93, 327);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(53, 22);
            this.button2.TabIndex = 32;
            this.button2.Text = "...";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // Avaliacao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 660);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.tabCabecalho);
            this.Controls.Add(this.bntGerarAvaliacao);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Avaliacao";
            this.Text = "Avaliacao";
            this.tabCabecalho.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictIcone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCabecalho)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bntGerarAvaliacao;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.TabControl tabCabecalho;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton radPdf;
        private System.Windows.Forms.RadioButton radWord;
        private System.Windows.Forms.TextBox txtTotalQuestoes;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListBox lstCodigosAleatorios;
        private System.Windows.Forms.ListBox lstCodigos;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radMista;
        private System.Windows.Forms.RadioButton radDissertativa;
        private System.Windows.Forms.RadioButton radObjetiva;
        private System.Windows.Forms.CheckBox chkAvaliacaoInedita;
        private System.Windows.Forms.CheckedListBox chkMaterias;
        private System.Windows.Forms.ComboBox cmbDisciplina;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbCurso;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbEscola;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dataAvaliacao;
        private System.Windows.Forms.TextBox tbProfessor;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbValorAvaliacao;
        private System.Windows.Forms.Button btnIcone;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox pictIcone;
        private System.Windows.Forms.PictureBox picCabecalho;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
    }
}
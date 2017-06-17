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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GerarAvaliacao));
            this.bntGerarAvaliacao = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.opnIcone = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabQuestoes = new System.Windows.Forms.TabPage();
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
            this.tabFormat = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbEscola = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbProfessor = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbValorAvaliacao = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dataAvaliacao = new System.Windows.Forms.DateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pictModelo4 = new System.Windows.Forms.PictureBox();
            this.radModelo4 = new System.Windows.Forms.RadioButton();
            this.pictModelo3 = new System.Windows.Forms.PictureBox();
            this.radModelo3 = new System.Windows.Forms.RadioButton();
            this.radModelo2 = new System.Windows.Forms.RadioButton();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictModelo1 = new System.Windows.Forms.PictureBox();
            this.radModelo1 = new System.Windows.Forms.RadioButton();
            this.chkNumeroPagina = new System.Windows.Forms.CheckBox();
            this.grpTexto = new System.Windows.Forms.GroupBox();
            this.cboAlinhamentoTexto = new System.Windows.Forms.ComboBox();
            this.cboFonteTexto = new System.Windows.Forms.ComboBox();
            this.grpEstilosFonteTexto = new System.Windows.Forms.GroupBox();
            this.chkSublinhadoTexto = new System.Windows.Forms.CheckBox();
            this.chkNegritoTexto = new System.Windows.Forms.CheckBox();
            this.chkItalicoTexto = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.nudTamanhoFonteTexto = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.pictIcone = new System.Windows.Forms.PictureBox();
            this.btnIcone = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabQuestoes.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabFormat.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictModelo4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictModelo3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictModelo1)).BeginInit();
            this.grpTexto.SuspendLayout();
            this.grpEstilosFonteTexto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTamanhoFonteTexto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictIcone)).BeginInit();
            this.SuspendLayout();
            // 
            // bntGerarAvaliacao
            // 
            this.bntGerarAvaliacao.Image = ((System.Drawing.Image)(resources.GetObject("bntGerarAvaliacao.Image")));
            this.bntGerarAvaliacao.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bntGerarAvaliacao.Location = new System.Drawing.Point(16, 507);
            this.bntGerarAvaliacao.Name = "bntGerarAvaliacao";
            this.bntGerarAvaliacao.Size = new System.Drawing.Size(173, 74);
            this.bntGerarAvaliacao.TabIndex = 13;
            this.bntGerarAvaliacao.Text = "Gerar Avaliação";
            this.bntGerarAvaliacao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bntGerarAvaliacao.UseVisualStyleBackColor = true;
            this.bntGerarAvaliacao.Click += new System.EventHandler(this.bntGerarAvaliacao_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // opnIcone
            // 
            this.opnIcone.FileName = "opnIcone";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Arquivos Imagens (*.jpg) | (*.png)";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabQuestoes);
            this.tabControl1.Controls.Add(this.tabFormat);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1080, 489);
            this.tabControl1.TabIndex = 14;
            // 
            // tabQuestoes
            // 
            this.tabQuestoes.Controls.Add(this.groupBox4);
            this.tabQuestoes.Controls.Add(this.txtTotalQuestoes);
            this.tabQuestoes.Controls.Add(this.label7);
            this.tabQuestoes.Controls.Add(this.lstCodigosAleatorios);
            this.tabQuestoes.Controls.Add(this.lstCodigos);
            this.tabQuestoes.Controls.Add(this.groupBox1);
            this.tabQuestoes.Controls.Add(this.chkAvaliacaoInedita);
            this.tabQuestoes.Controls.Add(this.chkMaterias);
            this.tabQuestoes.Controls.Add(this.cmbDisciplina);
            this.tabQuestoes.Controls.Add(this.label2);
            this.tabQuestoes.Controls.Add(this.cmbCurso);
            this.tabQuestoes.Controls.Add(this.label1);
            this.tabQuestoes.Location = new System.Drawing.Point(4, 23);
            this.tabQuestoes.Name = "tabQuestoes";
            this.tabQuestoes.Padding = new System.Windows.Forms.Padding(3);
            this.tabQuestoes.Size = new System.Drawing.Size(1072, 462);
            this.tabQuestoes.TabIndex = 0;
            this.tabQuestoes.Text = "Questões";
            this.tabQuestoes.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radPdf);
            this.groupBox4.Controls.Add(this.radWord);
            this.groupBox4.Location = new System.Drawing.Point(6, 154);
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
            this.radPdf.Size = new System.Drawing.Size(49, 18);
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
            this.radWord.Size = new System.Drawing.Size(58, 18);
            this.radWord.TabIndex = 0;
            this.radWord.TabStop = true;
            this.radWord.Text = "Word";
            this.radWord.UseVisualStyleBackColor = true;
            // 
            // txtTotalQuestoes
            // 
            this.txtTotalQuestoes.Location = new System.Drawing.Point(135, 315);
            this.txtTotalQuestoes.Name = "txtTotalQuestoes";
            this.txtTotalQuestoes.ReadOnly = true;
            this.txtTotalQuestoes.Size = new System.Drawing.Size(38, 22);
            this.txtTotalQuestoes.TabIndex = 46;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 323);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 14);
            this.label7.TabIndex = 45;
            this.label7.Text = "Total Questões";
            // 
            // lstCodigosAleatorios
            // 
            this.lstCodigosAleatorios.FormattingEnabled = true;
            this.lstCodigosAleatorios.ItemHeight = 14;
            this.lstCodigosAleatorios.Location = new System.Drawing.Point(75, 130);
            this.lstCodigosAleatorios.Name = "lstCodigosAleatorios";
            this.lstCodigosAleatorios.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lstCodigosAleatorios.Size = new System.Drawing.Size(61, 18);
            this.lstCodigosAleatorios.TabIndex = 44;
            this.lstCodigosAleatorios.Visible = false;
            // 
            // lstCodigos
            // 
            this.lstCodigos.FormattingEnabled = true;
            this.lstCodigos.ItemHeight = 14;
            this.lstCodigos.Location = new System.Drawing.Point(11, 130);
            this.lstCodigos.Name = "lstCodigos";
            this.lstCodigos.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lstCodigos.Size = new System.Drawing.Size(58, 18);
            this.lstCodigos.TabIndex = 43;
            this.lstCodigos.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radMista);
            this.groupBox1.Controls.Add(this.radDissertativa);
            this.groupBox1.Controls.Add(this.radObjetiva);
            this.groupBox1.Location = new System.Drawing.Point(6, 17);
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
            // 
            // radObjetiva
            // 
            this.radObjetiva.AutoSize = true;
            this.radObjetiva.Location = new System.Drawing.Point(9, 49);
            this.radObjetiva.Name = "radObjetiva";
            this.radObjetiva.Size = new System.Drawing.Size(78, 18);
            this.radObjetiva.TabIndex = 16;
            this.radObjetiva.Text = "Objetiva";
            this.radObjetiva.UseVisualStyleBackColor = true;
            // 
            // chkAvaliacaoInedita
            // 
            this.chkAvaliacaoInedita.AutoSize = true;
            this.chkAvaliacaoInedita.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAvaliacaoInedita.Location = new System.Drawing.Point(15, 278);
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
            this.chkMaterias.Location = new System.Drawing.Point(179, 154);
            this.chkMaterias.Name = "chkMaterias";
            this.chkMaterias.Size = new System.Drawing.Size(270, 123);
            this.chkMaterias.TabIndex = 40;
            // 
            // cmbDisciplina
            // 
            this.cmbDisciplina.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDisciplina.FormattingEnabled = true;
            this.cmbDisciplina.Location = new System.Drawing.Point(179, 87);
            this.cmbDisciplina.Name = "cmbDisciplina";
            this.cmbDisciplina.Size = new System.Drawing.Size(270, 22);
            this.cmbDisciplina.TabIndex = 39;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(176, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 16);
            this.label2.TabIndex = 38;
            this.label2.Text = "Disciplina";
            // 
            // cmbCurso
            // 
            this.cmbCurso.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCurso.FormattingEnabled = true;
            this.cmbCurso.Location = new System.Drawing.Point(179, 33);
            this.cmbCurso.Name = "cmbCurso";
            this.cmbCurso.Size = new System.Drawing.Size(270, 22);
            this.cmbCurso.TabIndex = 37;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(176, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 16);
            this.label1.TabIndex = 36;
            this.label1.Text = "Curso";
            // 
            // tabFormat
            // 
            this.tabFormat.Controls.Add(this.groupBox2);
            this.tabFormat.Controls.Add(this.chkNumeroPagina);
            this.tabFormat.Controls.Add(this.grpTexto);
            this.tabFormat.Controls.Add(this.button1);
            this.tabFormat.Controls.Add(this.label4);
            this.tabFormat.Controls.Add(this.pictIcone);
            this.tabFormat.Controls.Add(this.btnIcone);
            this.tabFormat.Location = new System.Drawing.Point(4, 23);
            this.tabFormat.Name = "tabFormat";
            this.tabFormat.Padding = new System.Windows.Forms.Padding(3);
            this.tabFormat.Size = new System.Drawing.Size(1072, 462);
            this.tabFormat.TabIndex = 1;
            this.tabFormat.Text = "Formatação";
            this.tabFormat.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.groupBox2.Controls.Add(this.tbEscola);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.tbProfessor);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.tbValorAvaliacao);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.dataAvaliacao);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Location = new System.Drawing.Point(320, 15);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(714, 562);
            this.groupBox2.TabIndex = 65;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Cabeçalho";
            // 
            // tbEscola
            // 
            this.tbEscola.Location = new System.Drawing.Point(442, 62);
            this.tbEscola.Name = "tbEscola";
            this.tbEscola.Size = new System.Drawing.Size(253, 22);
            this.tbEscola.TabIndex = 12;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(293, 65);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(144, 14);
            this.label10.TabIndex = 11;
            this.label10.Text = "Instituição de ensino:";
            // 
            // tbProfessor
            // 
            this.tbProfessor.Location = new System.Drawing.Point(442, 28);
            this.tbProfessor.Name = "tbProfessor";
            this.tbProfessor.Size = new System.Drawing.Size(253, 22);
            this.tbProfessor.TabIndex = 10;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(293, 33);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 14);
            this.label9.TabIndex = 9;
            this.label9.Text = "Professor(a):";
            // 
            // tbValorAvaliacao
            // 
            this.tbValorAvaliacao.Location = new System.Drawing.Point(171, 62);
            this.tbValorAvaliacao.Name = "tbValorAvaliacao";
            this.tbValorAvaliacao.Size = new System.Drawing.Size(100, 22);
            this.tbValorAvaliacao.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 65);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(126, 14);
            this.label6.TabIndex = 3;
            this.label6.Text = "Valor da Avaliação:";
            // 
            // dataAvaliacao
            // 
            this.dataAvaliacao.CalendarFont = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataAvaliacao.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dataAvaliacao.Location = new System.Drawing.Point(171, 28);
            this.dataAvaliacao.Name = "dataAvaliacao";
            this.dataAvaliacao.Size = new System.Drawing.Size(100, 22);
            this.dataAvaliacao.TabIndex = 2;
            this.dataAvaliacao.Value = new System.DateTime(2016, 6, 27, 0, 0, 0, 0);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(21, 33);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(125, 14);
            this.label11.TabIndex = 1;
            this.label11.Text = "Data da Avaliação:";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.groupBox3.Controls.Add(this.pictModelo4);
            this.groupBox3.Controls.Add(this.radModelo4);
            this.groupBox3.Controls.Add(this.pictModelo3);
            this.groupBox3.Controls.Add(this.radModelo3);
            this.groupBox3.Controls.Add(this.radModelo2);
            this.groupBox3.Controls.Add(this.pictureBox2);
            this.groupBox3.Controls.Add(this.pictModelo1);
            this.groupBox3.Controls.Add(this.radModelo1);
            this.groupBox3.Location = new System.Drawing.Point(23, 100);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(685, 324);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Modelos";
            // 
            // pictModelo4
            // 
            this.pictModelo4.Image = ((System.Drawing.Image)(resources.GetObject("pictModelo4.Image")));
            this.pictModelo4.Location = new System.Drawing.Point(360, 203);
            this.pictModelo4.Name = "pictModelo4";
            this.pictModelo4.Size = new System.Drawing.Size(296, 115);
            this.pictModelo4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictModelo4.TabIndex = 7;
            this.pictModelo4.TabStop = false;
            // 
            // radModelo4
            // 
            this.radModelo4.AutoSize = true;
            this.radModelo4.Location = new System.Drawing.Point(360, 179);
            this.radModelo4.Name = "radModelo4";
            this.radModelo4.Size = new System.Drawing.Size(82, 18);
            this.radModelo4.TabIndex = 6;
            this.radModelo4.TabStop = true;
            this.radModelo4.Text = "Modelo 4";
            this.radModelo4.UseVisualStyleBackColor = true;
            // 
            // pictModelo3
            // 
            this.pictModelo3.Image = ((System.Drawing.Image)(resources.GetObject("pictModelo3.Image")));
            this.pictModelo3.Location = new System.Drawing.Point(12, 203);
            this.pictModelo3.Name = "pictModelo3";
            this.pictModelo3.Size = new System.Drawing.Size(311, 115);
            this.pictModelo3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictModelo3.TabIndex = 5;
            this.pictModelo3.TabStop = false;
            // 
            // radModelo3
            // 
            this.radModelo3.AutoSize = true;
            this.radModelo3.Location = new System.Drawing.Point(12, 179);
            this.radModelo3.Name = "radModelo3";
            this.radModelo3.Size = new System.Drawing.Size(82, 18);
            this.radModelo3.TabIndex = 4;
            this.radModelo3.TabStop = true;
            this.radModelo3.Text = "Modelo 3";
            this.radModelo3.UseVisualStyleBackColor = true;
            // 
            // radModelo2
            // 
            this.radModelo2.AutoSize = true;
            this.radModelo2.Location = new System.Drawing.Point(360, 31);
            this.radModelo2.Name = "radModelo2";
            this.radModelo2.Size = new System.Drawing.Size(82, 18);
            this.radModelo2.TabIndex = 2;
            this.radModelo2.TabStop = true;
            this.radModelo2.Text = "Modelo 2";
            this.radModelo2.UseVisualStyleBackColor = true;
            // 
            // pictureBox2
            // 
            this.pictureBox2.ErrorImage = null;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(360, 55);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(296, 108);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // pictModelo1
            // 
            this.pictModelo1.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.pictModelo1.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictModelo1.ErrorImage")));
            this.pictModelo1.Image = ((System.Drawing.Image)(resources.GetObject("pictModelo1.Image")));
            this.pictModelo1.Location = new System.Drawing.Point(12, 55);
            this.pictModelo1.Name = "pictModelo1";
            this.pictModelo1.Size = new System.Drawing.Size(311, 108);
            this.pictModelo1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictModelo1.TabIndex = 1;
            this.pictModelo1.TabStop = false;
            // 
            // radModelo1
            // 
            this.radModelo1.AutoSize = true;
            this.radModelo1.Checked = true;
            this.radModelo1.Location = new System.Drawing.Point(12, 31);
            this.radModelo1.Name = "radModelo1";
            this.radModelo1.Size = new System.Drawing.Size(82, 18);
            this.radModelo1.TabIndex = 0;
            this.radModelo1.TabStop = true;
            this.radModelo1.Text = "Modelo 1";
            this.radModelo1.UseVisualStyleBackColor = true;
            // 
            // chkNumeroPagina
            // 
            this.chkNumeroPagina.AutoSize = true;
            this.chkNumeroPagina.Checked = true;
            this.chkNumeroPagina.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNumeroPagina.Location = new System.Drawing.Point(23, 438);
            this.chkNumeroPagina.Name = "chkNumeroPagina";
            this.chkNumeroPagina.Size = new System.Drawing.Size(262, 18);
            this.chkNumeroPagina.TabIndex = 64;
            this.chkNumeroPagina.Text = "Inserir número de páginas no rodapé";
            this.chkNumeroPagina.UseVisualStyleBackColor = true;
            // 
            // grpTexto
            // 
            this.grpTexto.Controls.Add(this.cboAlinhamentoTexto);
            this.grpTexto.Controls.Add(this.cboFonteTexto);
            this.grpTexto.Controls.Add(this.grpEstilosFonteTexto);
            this.grpTexto.Controls.Add(this.label8);
            this.grpTexto.Controls.Add(this.nudTamanhoFonteTexto);
            this.grpTexto.Controls.Add(this.label13);
            this.grpTexto.Controls.Add(this.label14);
            this.grpTexto.Location = new System.Drawing.Point(6, 15);
            this.grpTexto.Name = "grpTexto";
            this.grpTexto.Size = new System.Drawing.Size(296, 272);
            this.grpTexto.TabIndex = 60;
            this.grpTexto.TabStop = false;
            this.grpTexto.Text = "Texto";
            // 
            // cboAlinhamentoTexto
            // 
            this.cboAlinhamentoTexto.DisplayMember = "1";
            this.cboAlinhamentoTexto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAlinhamentoTexto.FormattingEnabled = true;
            this.cboAlinhamentoTexto.Items.AddRange(new object[] {
            "À esquerda",
            "Centralizado",
            "À direita",
            "Justificado"});
            this.cboAlinhamentoTexto.Location = new System.Drawing.Point(109, 232);
            this.cboAlinhamentoTexto.Name = "cboAlinhamentoTexto";
            this.cboAlinhamentoTexto.Size = new System.Drawing.Size(159, 22);
            this.cboAlinhamentoTexto.TabIndex = 43;
            this.cboAlinhamentoTexto.ValueMember = "1";
            // 
            // cboFonteTexto
            // 
            this.cboFonteTexto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFonteTexto.FormattingEnabled = true;
            this.cboFonteTexto.Items.AddRange(new object[] {
            "Arial",
            "Arial Black",
            "Batang",
            "Calibri",
            "Calibri Light",
            "Cambria",
            "Century Gothic",
            "Comic Sans MS",
            "Gadugi",
            "Georgia",
            "Impact",
            "Lucida Calligraphy",
            "Lucida Sans Unicode",
            "Segoe UI",
            "Segoe UI Black",
            "Tahoma",
            "Times New Roman",
            "Verdana"});
            this.cboFonteTexto.Location = new System.Drawing.Point(109, 20);
            this.cboFonteTexto.Name = "cboFonteTexto";
            this.cboFonteTexto.Size = new System.Drawing.Size(159, 22);
            this.cboFonteTexto.TabIndex = 42;
            // 
            // grpEstilosFonteTexto
            // 
            this.grpEstilosFonteTexto.Controls.Add(this.chkSublinhadoTexto);
            this.grpEstilosFonteTexto.Controls.Add(this.chkNegritoTexto);
            this.grpEstilosFonteTexto.Controls.Add(this.chkItalicoTexto);
            this.grpEstilosFonteTexto.Location = new System.Drawing.Point(9, 100);
            this.grpEstilosFonteTexto.Name = "grpEstilosFonteTexto";
            this.grpEstilosFonteTexto.Size = new System.Drawing.Size(226, 108);
            this.grpEstilosFonteTexto.TabIndex = 6;
            this.grpEstilosFonteTexto.TabStop = false;
            this.grpEstilosFonteTexto.Text = "Estilo da fonte";
            // 
            // chkSublinhadoTexto
            // 
            this.chkSublinhadoTexto.AutoSize = true;
            this.chkSublinhadoTexto.Location = new System.Drawing.Point(7, 79);
            this.chkSublinhadoTexto.Name = "chkSublinhadoTexto";
            this.chkSublinhadoTexto.Size = new System.Drawing.Size(96, 18);
            this.chkSublinhadoTexto.TabIndex = 2;
            this.chkSublinhadoTexto.Text = "Sublinhado";
            this.chkSublinhadoTexto.UseVisualStyleBackColor = true;
            // 
            // chkNegritoTexto
            // 
            this.chkNegritoTexto.AutoSize = true;
            this.chkNegritoTexto.Location = new System.Drawing.Point(7, 55);
            this.chkNegritoTexto.Name = "chkNegritoTexto";
            this.chkNegritoTexto.Size = new System.Drawing.Size(72, 18);
            this.chkNegritoTexto.TabIndex = 1;
            this.chkNegritoTexto.Text = "Negrito";
            this.chkNegritoTexto.UseVisualStyleBackColor = true;
            // 
            // chkItalicoTexto
            // 
            this.chkItalicoTexto.AutoSize = true;
            this.chkItalicoTexto.Location = new System.Drawing.Point(6, 30);
            this.chkItalicoTexto.Name = "chkItalicoTexto";
            this.chkItalicoTexto.Size = new System.Drawing.Size(64, 18);
            this.chkItalicoTexto.TabIndex = 0;
            this.chkItalicoTexto.Text = "Itálico";
            this.chkItalicoTexto.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 235);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 14);
            this.label8.TabIndex = 4;
            this.label8.Text = "Alinhamento";
            // 
            // nudTamanhoFonteTexto
            // 
            this.nudTamanhoFonteTexto.Location = new System.Drawing.Point(109, 65);
            this.nudTamanhoFonteTexto.Maximum = new decimal(new int[] {
            72,
            0,
            0,
            0});
            this.nudTamanhoFonteTexto.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.nudTamanhoFonteTexto.Name = "nudTamanhoFonteTexto";
            this.nudTamanhoFonteTexto.Size = new System.Drawing.Size(63, 22);
            this.nudTamanhoFonteTexto.TabIndex = 3;
            this.nudTamanhoFonteTexto.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(13, 67);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(64, 14);
            this.label13.TabIndex = 1;
            this.label13.Text = "Tamanho";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(13, 28);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(43, 14);
            this.label14.TabIndex = 0;
            this.label14.Text = "Fonte";
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(142, 296);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(53, 22);
            this.button1.TabIndex = 8;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 300);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 14);
            this.label4.TabIndex = 5;
            this.label4.Text = "Ícone:";
            // 
            // pictIcone
            // 
            this.pictIcone.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.pictIcone.Image = ((System.Drawing.Image)(resources.GetObject("pictIcone.Image")));
            this.pictIcone.Location = new System.Drawing.Point(73, 333);
            this.pictIcone.Name = "pictIcone";
            this.pictIcone.Size = new System.Drawing.Size(122, 91);
            this.pictIcone.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictIcone.TabIndex = 6;
            this.pictIcone.TabStop = false;
            // 
            // btnIcone
            // 
            this.btnIcone.Location = new System.Drawing.Point(73, 296);
            this.btnIcone.Name = "btnIcone";
            this.btnIcone.Size = new System.Drawing.Size(53, 22);
            this.btnIcone.TabIndex = 7;
            this.btnIcone.Text = "...";
            this.btnIcone.UseVisualStyleBackColor = true;
            // 
            // GerarAvaliacao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1192, 593);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.bntGerarAvaliacao);
            this.Font = new System.Drawing.Font("Verdana", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "GerarAvaliacao";
            this.Text = "Gerar Avaliação";
            this.tabControl1.ResumeLayout(false);
            this.tabQuestoes.ResumeLayout(false);
            this.tabQuestoes.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabFormat.ResumeLayout(false);
            this.tabFormat.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictModelo4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictModelo3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictModelo1)).EndInit();
            this.grpTexto.ResumeLayout(false);
            this.grpTexto.PerformLayout();
            this.grpEstilosFonteTexto.ResumeLayout(false);
            this.grpEstilosFonteTexto.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTamanhoFonteTexto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictIcone)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bntGerarAvaliacao;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.OpenFileDialog opnIcone;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabQuestoes;
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
        private System.Windows.Forms.TabPage tabFormat;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbEscola;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbProfessor;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbValorAvaliacao;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dataAvaliacao;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.PictureBox pictModelo4;
        private System.Windows.Forms.RadioButton radModelo4;
        private System.Windows.Forms.PictureBox pictModelo3;
        private System.Windows.Forms.RadioButton radModelo3;
        private System.Windows.Forms.RadioButton radModelo2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictModelo1;
        private System.Windows.Forms.RadioButton radModelo1;
        private System.Windows.Forms.CheckBox chkNumeroPagina;
        private System.Windows.Forms.GroupBox grpTexto;
        private System.Windows.Forms.ComboBox cboAlinhamentoTexto;
        private System.Windows.Forms.ComboBox cboFonteTexto;
        private System.Windows.Forms.GroupBox grpEstilosFonteTexto;
        private System.Windows.Forms.CheckBox chkSublinhadoTexto;
        private System.Windows.Forms.CheckBox chkNegritoTexto;
        private System.Windows.Forms.CheckBox chkItalicoTexto;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown nudTamanhoFonteTexto;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictIcone;
        private System.Windows.Forms.Button btnIcone;
    }
}
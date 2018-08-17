namespace HelpTeacher.Forms
{
    partial class CadastroConteudo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CadastroConteudo));
            this.tabControlConteudo = new System.Windows.Forms.TabControl();
            this.tabCursos = new System.Windows.Forms.TabPage();
            this.txtCodigoCurso = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnCancelarCurso = new System.Windows.Forms.Button();
            this.btnSalvarCurso = new System.Windows.Forms.Button();
            this.txtNomeCurso = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabDisciplinas = new System.Windows.Forms.TabPage();
            this.txtCodigoDisciplina = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnCancelarDisc = new System.Windows.Forms.Button();
            this.bntSalvarDisc = new System.Windows.Forms.Button();
            this.cmbCurso = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNomeDisciplina = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabMaterias = new System.Windows.Forms.TabPage();
            this.txtCodigoMateria = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtCurso = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnCancelarMateria = new System.Windows.Forms.Button();
            this.bntSalvarMateria = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNomeMateria = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbDisciplina = new System.Windows.Forms.ComboBox();
            this.tabControlConteudo.SuspendLayout();
            this.tabCursos.SuspendLayout();
            this.tabDisciplinas.SuspendLayout();
            this.tabMaterias.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlConteudo
            // 
            this.tabControlConteudo.Controls.Add(this.tabCursos);
            this.tabControlConteudo.Controls.Add(this.tabDisciplinas);
            this.tabControlConteudo.Controls.Add(this.tabMaterias);
            this.tabControlConteudo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlConteudo.Location = new System.Drawing.Point(0, 0);
            this.tabControlConteudo.Name = "tabControlConteudo";
            this.tabControlConteudo.SelectedIndex = 0;
            this.tabControlConteudo.Size = new System.Drawing.Size(453, 279);
            this.tabControlConteudo.TabIndex = 1;
            // 
            // tabCursos
            // 
            this.tabCursos.BackColor = System.Drawing.Color.Transparent;
            this.tabCursos.Controls.Add(this.txtCodigoCurso);
            this.tabCursos.Controls.Add(this.label7);
            this.tabCursos.Controls.Add(this.btnCancelarCurso);
            this.tabCursos.Controls.Add(this.btnSalvarCurso);
            this.tabCursos.Controls.Add(this.txtNomeCurso);
            this.tabCursos.Controls.Add(this.label1);
            this.tabCursos.Location = new System.Drawing.Point(4, 23);
            this.tabCursos.Name = "tabCursos";
            this.tabCursos.Padding = new System.Windows.Forms.Padding(3);
            this.tabCursos.Size = new System.Drawing.Size(445, 252);
            this.tabCursos.TabIndex = 0;
            this.tabCursos.Text = "Cursos";
            this.tabCursos.Enter += new System.EventHandler(this.tabCursos_Enter);
            // 
            // txtCodigoCurso
            // 
            this.txtCodigoCurso.Location = new System.Drawing.Point(159, 54);
            this.txtCodigoCurso.Name = "txtCodigoCurso";
            this.txtCodigoCurso.ReadOnly = true;
            this.txtCodigoCurso.Size = new System.Drawing.Size(65, 22);
            this.txtCodigoCurso.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(36, 57);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(117, 14);
            this.label7.TabIndex = 18;
            this.label7.Text = "Código do Curso:";
            // 
            // btnCancelarCurso
            // 
            this.btnCancelarCurso.Location = new System.Drawing.Point(244, 180);
            this.btnCancelarCurso.Name = "btnCancelarCurso";
            this.btnCancelarCurso.Size = new System.Drawing.Size(120, 50);
            this.btnCancelarCurso.TabIndex = 17;
            this.btnCancelarCurso.Text = "Cancelar";
            this.btnCancelarCurso.UseVisualStyleBackColor = true;
            this.btnCancelarCurso.Click += new System.EventHandler(this.btnCancelarCurso_Click);
            // 
            // btnSalvarCurso
            // 
            this.btnSalvarCurso.Image = ((System.Drawing.Image)(resources.GetObject("btnSalvarCurso.Image")));
            this.btnSalvarCurso.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalvarCurso.Location = new System.Drawing.Point(79, 180);
            this.btnSalvarCurso.Name = "btnSalvarCurso";
            this.btnSalvarCurso.Size = new System.Drawing.Size(120, 50);
            this.btnSalvarCurso.TabIndex = 11;
            this.btnSalvarCurso.Text = "Salvar";
            this.btnSalvarCurso.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSalvarCurso.UseVisualStyleBackColor = true;
            this.btnSalvarCurso.Click += new System.EventHandler(this.btnSalvarCurso_Click);
            // 
            // txtNomeCurso
            // 
            this.txtNomeCurso.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtNomeCurso.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtNomeCurso.Location = new System.Drawing.Point(90, 95);
            this.txtNomeCurso.Name = "txtNomeCurso";
            this.txtNomeCurso.Size = new System.Drawing.Size(314, 22);
            this.txtNomeCurso.TabIndex = 1;
            this.txtNomeCurso.Click += new System.EventHandler(this.setaCursorClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Curso ";
            // 
            // tabDisciplinas
            // 
            this.tabDisciplinas.BackColor = System.Drawing.Color.Transparent;
            this.tabDisciplinas.Controls.Add(this.txtCodigoDisciplina);
            this.tabDisciplinas.Controls.Add(this.label8);
            this.tabDisciplinas.Controls.Add(this.btnCancelarDisc);
            this.tabDisciplinas.Controls.Add(this.bntSalvarDisc);
            this.tabDisciplinas.Controls.Add(this.cmbCurso);
            this.tabDisciplinas.Controls.Add(this.label3);
            this.tabDisciplinas.Controls.Add(this.txtNomeDisciplina);
            this.tabDisciplinas.Controls.Add(this.label2);
            this.tabDisciplinas.Location = new System.Drawing.Point(4, 23);
            this.tabDisciplinas.Name = "tabDisciplinas";
            this.tabDisciplinas.Padding = new System.Windows.Forms.Padding(3);
            this.tabDisciplinas.Size = new System.Drawing.Size(445, 252);
            this.tabDisciplinas.TabIndex = 1;
            this.tabDisciplinas.Text = "Disciplinas";
            this.tabDisciplinas.Enter += new System.EventHandler(this.tabDisciplinas_Enter);
            // 
            // txtCodigoDisciplina
            // 
            this.txtCodigoDisciplina.Location = new System.Drawing.Point(168, 38);
            this.txtCodigoDisciplina.Name = "txtCodigoDisciplina";
            this.txtCodigoDisciplina.ReadOnly = true;
            this.txtCodigoDisciplina.Size = new System.Drawing.Size(65, 22);
            this.txtCodigoDisciplina.TabIndex = 21;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(24, 41);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(138, 14);
            this.label8.TabIndex = 20;
            this.label8.Text = "Código da Disciplina:";
            // 
            // btnCancelarDisc
            // 
            this.btnCancelarDisc.Location = new System.Drawing.Point(244, 180);
            this.btnCancelarDisc.Name = "btnCancelarDisc";
            this.btnCancelarDisc.Size = new System.Drawing.Size(120, 50);
            this.btnCancelarDisc.TabIndex = 19;
            this.btnCancelarDisc.Text = "Cancelar";
            this.btnCancelarDisc.UseVisualStyleBackColor = true;
            this.btnCancelarDisc.Click += new System.EventHandler(this.btnCancelarDisc_Click);
            // 
            // bntSalvarDisc
            // 
            this.bntSalvarDisc.Image = ((System.Drawing.Image)(resources.GetObject("bntSalvarDisc.Image")));
            this.bntSalvarDisc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bntSalvarDisc.Location = new System.Drawing.Point(79, 180);
            this.bntSalvarDisc.Name = "bntSalvarDisc";
            this.bntSalvarDisc.Size = new System.Drawing.Size(120, 50);
            this.bntSalvarDisc.TabIndex = 18;
            this.bntSalvarDisc.Text = "Salvar";
            this.bntSalvarDisc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bntSalvarDisc.UseVisualStyleBackColor = true;
            this.bntSalvarDisc.Click += new System.EventHandler(this.bntSalvarDisc_Click);
            // 
            // cmbCurso
            // 
            this.cmbCurso.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCurso.FormattingEnabled = true;
            this.cmbCurso.Location = new System.Drawing.Point(99, 114);
            this.cmbCurso.Name = "cmbCurso";
            this.cmbCurso.Size = new System.Drawing.Size(314, 22);
            this.cmbCurso.TabIndex = 3;
            this.cmbCurso.SelectedIndexChanged += new System.EventHandler(this.cmbCurso_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 14);
            this.label3.TabIndex = 2;
            this.label3.Text = "Curso ";
            // 
            // txtNomeDisciplina
            // 
            this.txtNomeDisciplina.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtNomeDisciplina.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtNomeDisciplina.Location = new System.Drawing.Point(99, 76);
            this.txtNomeDisciplina.Name = "txtNomeDisciplina";
            this.txtNomeDisciplina.Size = new System.Drawing.Size(314, 22);
            this.txtNomeDisciplina.TabIndex = 1;
            this.txtNomeDisciplina.Click += new System.EventHandler(this.setaCursorClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 14);
            this.label2.TabIndex = 0;
            this.label2.Text = "Disciplina ";
            // 
            // tabMaterias
            // 
            this.tabMaterias.BackColor = System.Drawing.Color.Transparent;
            this.tabMaterias.Controls.Add(this.txtCodigoMateria);
            this.tabMaterias.Controls.Add(this.label9);
            this.tabMaterias.Controls.Add(this.txtCurso);
            this.tabMaterias.Controls.Add(this.label6);
            this.tabMaterias.Controls.Add(this.btnCancelarMateria);
            this.tabMaterias.Controls.Add(this.bntSalvarMateria);
            this.tabMaterias.Controls.Add(this.label5);
            this.tabMaterias.Controls.Add(this.txtNomeMateria);
            this.tabMaterias.Controls.Add(this.label4);
            this.tabMaterias.Controls.Add(this.cmbDisciplina);
            this.tabMaterias.Location = new System.Drawing.Point(4, 23);
            this.tabMaterias.Name = "tabMaterias";
            this.tabMaterias.Padding = new System.Windows.Forms.Padding(3);
            this.tabMaterias.Size = new System.Drawing.Size(445, 252);
            this.tabMaterias.TabIndex = 2;
            this.tabMaterias.Text = "Matérias";
            this.tabMaterias.Enter += new System.EventHandler(this.tabMaterias_Enter);
            // 
            // txtCodigoMateria
            // 
            this.txtCodigoMateria.Location = new System.Drawing.Point(157, 21);
            this.txtCodigoMateria.Name = "txtCodigoMateria";
            this.txtCodigoMateria.ReadOnly = true;
            this.txtCodigoMateria.Size = new System.Drawing.Size(65, 22);
            this.txtCodigoMateria.TabIndex = 25;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(24, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(127, 14);
            this.label9.TabIndex = 24;
            this.label9.Text = "Código da Matéria:";
            // 
            // txtCurso
            // 
            this.txtCurso.Location = new System.Drawing.Point(95, 135);
            this.txtCurso.Name = "txtCurso";
            this.txtCurso.ReadOnly = true;
            this.txtCurso.Size = new System.Drawing.Size(314, 22);
            this.txtCurso.TabIndex = 23;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(45, 138);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 14);
            this.label6.TabIndex = 22;
            this.label6.Text = "Curso";
            // 
            // btnCancelarMateria
            // 
            this.btnCancelarMateria.Location = new System.Drawing.Point(244, 180);
            this.btnCancelarMateria.Name = "btnCancelarMateria";
            this.btnCancelarMateria.Size = new System.Drawing.Size(120, 50);
            this.btnCancelarMateria.TabIndex = 21;
            this.btnCancelarMateria.Text = "Cancelar";
            this.btnCancelarMateria.UseVisualStyleBackColor = true;
            this.btnCancelarMateria.Click += new System.EventHandler(this.btnCancelarMateria_Click);
            // 
            // bntSalvarMateria
            // 
            this.bntSalvarMateria.Image = ((System.Drawing.Image)(resources.GetObject("bntSalvarMateria.Image")));
            this.bntSalvarMateria.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bntSalvarMateria.Location = new System.Drawing.Point(79, 180);
            this.bntSalvarMateria.Name = "bntSalvarMateria";
            this.bntSalvarMateria.Size = new System.Drawing.Size(120, 50);
            this.bntSalvarMateria.TabIndex = 20;
            this.bntSalvarMateria.Text = "Salvar";
            this.bntSalvarMateria.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bntSalvarMateria.UseVisualStyleBackColor = true;
            this.bntSalvarMateria.Click += new System.EventHandler(this.bntSalvarMateria_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 100);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 14);
            this.label5.TabIndex = 4;
            this.label5.Text = "Disciplina";
            // 
            // txtNomeMateria
            // 
            this.txtNomeMateria.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtNomeMateria.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtNomeMateria.Location = new System.Drawing.Point(95, 59);
            this.txtNomeMateria.Name = "txtNomeMateria";
            this.txtNomeMateria.Size = new System.Drawing.Size(314, 22);
            this.txtNomeMateria.TabIndex = 3;
            this.txtNomeMateria.Click += new System.EventHandler(this.setaCursorClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 14);
            this.label4.TabIndex = 2;
            this.label4.Text = "Matéria";
            // 
            // cmbDisciplina
            // 
            this.cmbDisciplina.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDisciplina.FormattingEnabled = true;
            this.cmbDisciplina.Location = new System.Drawing.Point(95, 97);
            this.cmbDisciplina.Name = "cmbDisciplina";
            this.cmbDisciplina.Size = new System.Drawing.Size(314, 22);
            this.cmbDisciplina.TabIndex = 0;
            this.cmbDisciplina.SelectedIndexChanged += new System.EventHandler(this.comboBoxDisciplina_SelectedIndexChanged);
            // 
            // CadastroConteudo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 279);
            this.Controls.Add(this.tabControlConteudo);
            this.Font = new System.Drawing.Font("Verdana", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CadastroConteudo";
            this.Text = "Cadastro de Conteúdo";
            this.tabControlConteudo.ResumeLayout(false);
            this.tabCursos.ResumeLayout(false);
            this.tabCursos.PerformLayout();
            this.tabDisciplinas.ResumeLayout(false);
            this.tabDisciplinas.PerformLayout();
            this.tabMaterias.ResumeLayout(false);
            this.tabMaterias.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlConteudo;
        private System.Windows.Forms.TabPage tabCursos;
        private System.Windows.Forms.TextBox txtNomeCurso;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabDisciplinas;
        private System.Windows.Forms.ComboBox cmbCurso;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNomeDisciplina;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabMaterias;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNomeMateria;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbDisciplina;
        private System.Windows.Forms.Button btnSalvarCurso;
        private System.Windows.Forms.Button btnCancelarCurso;
        private System.Windows.Forms.Button btnCancelarDisc;
        private System.Windows.Forms.Button bntSalvarDisc;
        private System.Windows.Forms.Button btnCancelarMateria;
        private System.Windows.Forms.Button bntSalvarMateria;
        private System.Windows.Forms.TextBox txtCurso;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCodigoCurso;
        private System.Windows.Forms.TextBox txtCodigoDisciplina;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtCodigoMateria;
        private System.Windows.Forms.Label label9;
    }
}
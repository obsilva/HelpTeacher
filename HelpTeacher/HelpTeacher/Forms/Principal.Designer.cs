namespace HelpTeacher.Forms
{
    partial class Principal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Principal));
            this.barraMenu = new System.Windows.Forms.MenuStrip();
            this.menuQuestoes = new System.Windows.Forms.ToolStripMenuItem();
            this.cadastrarMenuQuestoes = new System.Windows.Forms.ToolStripMenuItem();
            this.pesquisarMenuQuestoes = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConteudo = new System.Windows.Forms.ToolStripMenuItem();
            this.cadastrarMenuConteudo = new System.Windows.Forms.ToolStripMenuItem();
            this.cursoMenuCadastrar = new System.Windows.Forms.ToolStripMenuItem();
            this.disciplinaMenuCadastrar = new System.Windows.Forms.ToolStripMenuItem();
            this.materiaMenuCadastrar = new System.Windows.Forms.ToolStripMenuItem();
            this.pesquisarMenuConteudo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAvaliacoes = new System.Windows.Forms.ToolStripMenuItem();
            this.gerarMenuAvaliacoes = new System.Windows.Forms.ToolStripMenuItem();
            this.historicoMenuAvaliacoes = new System.Windows.Forms.ToolStripMenuItem();
            this.menuUsuario = new System.Windows.Forms.ToolStripMenuItem();
            this.configuracoesMenuUsuario = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutMenuUsuario = new System.Windows.Forms.ToolStripMenuItem();
            this.sairMenuUsuario = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAjuda = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlPrincipal = new System.Windows.Forms.Panel();
            this.backupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.barraMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // barraMenu
            // 
            this.barraMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(29)))), ((int)(((byte)(33)))));
            this.barraMenu.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.barraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuQuestoes,
            this.menuConteudo,
            this.menuAvaliacoes,
            this.menuUsuario,
            this.menuAjuda,
            this.backupToolStripMenuItem});
            this.barraMenu.Location = new System.Drawing.Point(0, 0);
            this.barraMenu.Name = "barraMenu";
            this.barraMenu.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.barraMenu.Size = new System.Drawing.Size(1232, 30);
            this.barraMenu.TabIndex = 0;
            this.barraMenu.Text = "menuStrip1";
            // 
            // menuQuestoes
            // 
            this.menuQuestoes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cadastrarMenuQuestoes,
            this.pesquisarMenuQuestoes});
            this.menuQuestoes.Name = "menuQuestoes";
            this.menuQuestoes.Size = new System.Drawing.Size(102, 26);
            this.menuQuestoes.Text = "Questões";
            // 
            // cadastrarMenuQuestoes
            // 
            this.cadastrarMenuQuestoes.Image = ((System.Drawing.Image)(resources.GetObject("cadastrarMenuQuestoes.Image")));
            this.cadastrarMenuQuestoes.Name = "cadastrarMenuQuestoes";
            this.cadastrarMenuQuestoes.Size = new System.Drawing.Size(170, 26);
            this.cadastrarMenuQuestoes.Text = "Cadastrar";
            this.cadastrarMenuQuestoes.Click += new System.EventHandler(this.cadastrarMenuQuestoes_Click);
            // 
            // pesquisarMenuQuestoes
            // 
            this.pesquisarMenuQuestoes.Image = ((System.Drawing.Image)(resources.GetObject("pesquisarMenuQuestoes.Image")));
            this.pesquisarMenuQuestoes.Name = "pesquisarMenuQuestoes";
            this.pesquisarMenuQuestoes.Size = new System.Drawing.Size(170, 26);
            this.pesquisarMenuQuestoes.Text = "Pesquisar";
            this.pesquisarMenuQuestoes.Click += new System.EventHandler(this.pesquisarMenuQuestoes_Click);
            // 
            // menuConteudo
            // 
            this.menuConteudo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cadastrarMenuConteudo,
            this.pesquisarMenuConteudo});
            this.menuConteudo.Name = "menuConteudo";
            this.menuConteudo.Size = new System.Drawing.Size(102, 26);
            this.menuConteudo.Text = "Conteúdo";
            // 
            // cadastrarMenuConteudo
            // 
            this.cadastrarMenuConteudo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cursoMenuCadastrar,
            this.disciplinaMenuCadastrar,
            this.materiaMenuCadastrar});
            this.cadastrarMenuConteudo.Image = ((System.Drawing.Image)(resources.GetObject("cadastrarMenuConteudo.Image")));
            this.cadastrarMenuConteudo.Name = "cadastrarMenuConteudo";
            this.cadastrarMenuConteudo.Size = new System.Drawing.Size(170, 26);
            this.cadastrarMenuConteudo.Text = "Cadastrar";
            // 
            // cursoMenuCadastrar
            // 
            this.cursoMenuCadastrar.Image = ((System.Drawing.Image)(resources.GetObject("cursoMenuCadastrar.Image")));
            this.cursoMenuCadastrar.Name = "cursoMenuCadastrar";
            this.cursoMenuCadastrar.Size = new System.Drawing.Size(190, 26);
            this.cursoMenuCadastrar.Text = "Cursos";
            this.cursoMenuCadastrar.Click += new System.EventHandler(this.cursoMenuCadastrar_Click);
            // 
            // disciplinaMenuCadastrar
            // 
            this.disciplinaMenuCadastrar.Image = ((System.Drawing.Image)(resources.GetObject("disciplinaMenuCadastrar.Image")));
            this.disciplinaMenuCadastrar.Name = "disciplinaMenuCadastrar";
            this.disciplinaMenuCadastrar.Size = new System.Drawing.Size(190, 26);
            this.disciplinaMenuCadastrar.Text = "Disciplinas";
            this.disciplinaMenuCadastrar.Click += new System.EventHandler(this.disciplinaMenuCadastrar_Click);
            // 
            // materiaMenuCadastrar
            // 
            this.materiaMenuCadastrar.Image = ((System.Drawing.Image)(resources.GetObject("materiaMenuCadastrar.Image")));
            this.materiaMenuCadastrar.Name = "materiaMenuCadastrar";
            this.materiaMenuCadastrar.Size = new System.Drawing.Size(190, 26);
            this.materiaMenuCadastrar.Text = "Matérias";
            this.materiaMenuCadastrar.Click += new System.EventHandler(this.materiaMenuCadastrar_Click);
            // 
            // pesquisarMenuConteudo
            // 
            this.pesquisarMenuConteudo.Image = ((System.Drawing.Image)(resources.GetObject("pesquisarMenuConteudo.Image")));
            this.pesquisarMenuConteudo.Name = "pesquisarMenuConteudo";
            this.pesquisarMenuConteudo.Size = new System.Drawing.Size(170, 26);
            this.pesquisarMenuConteudo.Text = "Pesquisar";
            this.pesquisarMenuConteudo.Click += new System.EventHandler(this.pesquisarMenuConteudo_Click);
            // 
            // menuAvaliacoes
            // 
            this.menuAvaliacoes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gerarMenuAvaliacoes,
            this.historicoMenuAvaliacoes});
            this.menuAvaliacoes.Name = "menuAvaliacoes";
            this.menuAvaliacoes.Size = new System.Drawing.Size(122, 26);
            this.menuAvaliacoes.Text = "Avaliações";
            // 
            // gerarMenuAvaliacoes
            // 
            this.gerarMenuAvaliacoes.Image = ((System.Drawing.Image)(resources.GetObject("gerarMenuAvaliacoes.Image")));
            this.gerarMenuAvaliacoes.Name = "gerarMenuAvaliacoes";
            this.gerarMenuAvaliacoes.Size = new System.Drawing.Size(180, 26);
            this.gerarMenuAvaliacoes.Text = "Gerar Nova";
            this.gerarMenuAvaliacoes.Click += new System.EventHandler(this.gerarMenuAvaliacoes_Click);
            // 
            // historicoMenuAvaliacoes
            // 
            this.historicoMenuAvaliacoes.Image = ((System.Drawing.Image)(resources.GetObject("historicoMenuAvaliacoes.Image")));
            this.historicoMenuAvaliacoes.Name = "historicoMenuAvaliacoes";
            this.historicoMenuAvaliacoes.Size = new System.Drawing.Size(180, 26);
            this.historicoMenuAvaliacoes.Text = "Histórico";
            this.historicoMenuAvaliacoes.Click += new System.EventHandler(this.historicoMenuAvaliacoes_Click);
            // 
            // menuUsuario
            // 
            this.menuUsuario.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configuracoesMenuUsuario,
            this.logoutMenuUsuario,
            this.sairMenuUsuario});
            this.menuUsuario.Name = "menuUsuario";
            this.menuUsuario.Size = new System.Drawing.Size(92, 26);
            this.menuUsuario.Text = "Usuário";
            // 
            // configuracoesMenuUsuario
            // 
            this.configuracoesMenuUsuario.Image = ((System.Drawing.Image)(resources.GetObject("configuracoesMenuUsuario.Image")));
            this.configuracoesMenuUsuario.Name = "configuracoesMenuUsuario";
            this.configuracoesMenuUsuario.Size = new System.Drawing.Size(210, 26);
            this.configuracoesMenuUsuario.Text = "Configurações";
            this.configuracoesMenuUsuario.Click += new System.EventHandler(this.configuracoesMenuUsuario_Click);
            // 
            // logoutMenuUsuario
            // 
            this.logoutMenuUsuario.Image = ((System.Drawing.Image)(resources.GetObject("logoutMenuUsuario.Image")));
            this.logoutMenuUsuario.Name = "logoutMenuUsuario";
            this.logoutMenuUsuario.Size = new System.Drawing.Size(210, 26);
            this.logoutMenuUsuario.Text = "Logout";
            this.logoutMenuUsuario.Click += new System.EventHandler(this.logoutMenuUsuario_Click);
            // 
            // sairMenuUsuario
            // 
            this.sairMenuUsuario.Image = ((System.Drawing.Image)(resources.GetObject("sairMenuUsuario.Image")));
            this.sairMenuUsuario.Name = "sairMenuUsuario";
            this.sairMenuUsuario.Size = new System.Drawing.Size(210, 26);
            this.sairMenuUsuario.Text = "Sair";
            this.sairMenuUsuario.Click += new System.EventHandler(this.sairMenuUsuario_Click);
            // 
            // menuAjuda
            // 
            this.menuAjuda.Name = "menuAjuda";
            this.menuAjuda.Size = new System.Drawing.Size(72, 26);
            this.menuAjuda.Text = "Ajuda";
            this.menuAjuda.Click += new System.EventHandler(this.menuAjuda_Click);
            // 
            // pnlPrincipal
            // 
            this.pnlPrincipal.BackColor = System.Drawing.Color.Transparent;
            this.pnlPrincipal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnlPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPrincipal.Location = new System.Drawing.Point(0, 30);
            this.pnlPrincipal.Name = "pnlPrincipal";
            this.pnlPrincipal.Size = new System.Drawing.Size(1232, 633);
            this.pnlPrincipal.TabIndex = 1;
            // 
            // backupToolStripMenuItem
            // 
            this.backupToolStripMenuItem.Name = "backupToolStripMenuItem";
            this.backupToolStripMenuItem.Size = new System.Drawing.Size(82, 26);
            this.backupToolStripMenuItem.Text = "Backup";
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1232, 663);
            this.Controls.Add(this.pnlPrincipal);
            this.Controls.Add(this.barraMenu);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Verdana", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.barraMenu;
            this.MinimumSize = new System.Drawing.Size(1248, 702);
            this.Name = "Principal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Help Teacher";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.barraMenu.ResumeLayout(false);
            this.barraMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip barraMenu;
        private System.Windows.Forms.ToolStripMenuItem menuQuestoes;
        private System.Windows.Forms.ToolStripMenuItem cadastrarMenuQuestoes;
        private System.Windows.Forms.ToolStripMenuItem pesquisarMenuQuestoes;
        private System.Windows.Forms.ToolStripMenuItem menuConteudo;
        private System.Windows.Forms.ToolStripMenuItem menuAvaliacoes;
        private System.Windows.Forms.ToolStripMenuItem gerarMenuAvaliacoes;
        private System.Windows.Forms.ToolStripMenuItem historicoMenuAvaliacoes;
        private System.Windows.Forms.ToolStripMenuItem menuUsuario;
        private System.Windows.Forms.ToolStripMenuItem configuracoesMenuUsuario;
        private System.Windows.Forms.ToolStripMenuItem logoutMenuUsuario;
        private System.Windows.Forms.ToolStripMenuItem sairMenuUsuario;
        private System.Windows.Forms.ToolStripMenuItem pesquisarMenuConteudo;
        private System.Windows.Forms.Panel pnlPrincipal;
        private System.Windows.Forms.ToolStripMenuItem cadastrarMenuConteudo;
        private System.Windows.Forms.ToolStripMenuItem cursoMenuCadastrar;
        private System.Windows.Forms.ToolStripMenuItem disciplinaMenuCadastrar;
        private System.Windows.Forms.ToolStripMenuItem materiaMenuCadastrar;
        private System.Windows.Forms.ToolStripMenuItem menuAjuda;
        private System.Windows.Forms.ToolStripMenuItem backupToolStripMenuItem;

    }
}
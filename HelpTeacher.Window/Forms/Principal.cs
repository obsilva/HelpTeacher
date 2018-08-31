using HelpTeacher.Classes;
using System;
using System.IO;
using System.Windows.Forms;

namespace HelpTeacher.Forms
{
	public partial class Principal : Form
	{
		private ConexaoBanco banco = new ConexaoBanco();
		private CadastroQuestao cadastraQuestao;
		private CadastroConteudo cadastraConteudo;
		private GerarAvaliacao geraAvaliacao;
		private Configuracoes config;
		private Pesquisa pesquisa;
		private Ajuda ajuda;

		public Principal()
		{
			InitializeComponent();

			verificaPasta();
		}
		// FUNÇÕES GERAIS  FUNÇÕES GERAIS  FUNÇÕES GERAIS  FUNÇÕES GERAIS  FUNÇÕES GERAIS  FUNÇÕES GERAIS  //
		// FUNÇÕES GERAIS  FUNÇÕES GERAIS  FUNÇÕES GERAIS  FUNÇÕES GERAIS  FUNÇÕES GERAIS  FUNÇÕES GERAIS  //
		private void verificaPasta()
		{
			string pastaArquivos = @"..\_files";
			if (!Directory.Exists(pastaArquivos))
			{
				Directory.CreateDirectory(pastaArquivos);
			}
		}

		/* abreTela
		 * 
		 * Faz as configurações para o form ser aberto e
		 * adicionado ao painel principal, minimizando as
		 * outras janelas
		 */
		private void abre(Form tela)
		{
			tela.TopLevel = false;
			tela.AutoScroll = true;
			minimizaJanelas();
			pnlPrincipal.Controls.Add(tela);
			tela.Show();
		}

		/* minimizaJanelas
		 * 
		 * Verifica as janelas abertas as minimiza
		 */
		private void minimizaJanelas()
		{
			if (pnlPrincipal.Controls.Count > 0)
			{
				if (pnlPrincipal.Contains(cadastraQuestao))
				{
					cadastraQuestao.WindowState = FormWindowState.Minimized;
				}

				if (pnlPrincipal.Contains(cadastraConteudo))
				{
					cadastraConteudo.WindowState = FormWindowState.Minimized;
				}

				if (pnlPrincipal.Contains(geraAvaliacao))
				{
					geraAvaliacao.WindowState = FormWindowState.Minimized;
				}

				if (pnlPrincipal.Contains(config))
				{
					config.WindowState = FormWindowState.Minimized;
				}

				if (pnlPrincipal.Contains(pesquisa))
				{
					pesquisa.WindowState = FormWindowState.Minimized;
				}

				if (pnlPrincipal.Contains(ajuda))
				{
					ajuda.WindowState = FormWindowState.Minimized;
				}
			}
		}

		/* fechaJanelas
		 * 
		 * Verifica as janelas abertas as fecha
		 */
		private void fechaJanelas()
		{
			if (pnlPrincipal.Controls.Count > 0)
			{
				if (pnlPrincipal.Contains(cadastraQuestao))
				{
					cadastraQuestao.Close();
				}

				if (pnlPrincipal.Contains(cadastraConteudo))
				{
					cadastraConteudo.Close();
				}

				if (pnlPrincipal.Contains(geraAvaliacao))
				{
					geraAvaliacao.Close();
				}

				if (pnlPrincipal.Contains(config))
				{
					config.Close();
				}

				if (pnlPrincipal.Contains(pesquisa))
				{
					pesquisa.Close();
				}

				if (pnlPrincipal.Contains(ajuda))
				{
					ajuda.Close();
				}
			}
		}


		// QUESTÕES  QUESTÕES  QUESTÕES  QUESTÕES  QUESTÕES  QUESTÕES  QUESTÕES  QUESTÕES  QUESTÕES  QUESTÕES  //
		// QUESTÕES  QUESTÕES  QUESTÕES  QUESTÕES  QUESTÕES  QUESTÕES  QUESTÕES  QUESTÕES  QUESTÕES  QUESTÕES  //
		private void cadastrarMenuQuestoes_Click(object sender, EventArgs e)
		{
			telaCadastroQuestao();
			cadastrarMenuQuestoes.Enabled = false;
		}

		private void telaCadastroQuestao()
		{
			cadastraQuestao = new CadastroQuestao();
			cadastraQuestao.FormClosed += new FormClosedEventHandler(ativaCadastroQuestoes);
			abre(cadastraQuestao);
		}

		private void ativaCadastroQuestoes(object sender, FormClosedEventArgs e) => cadastrarMenuQuestoes.Enabled = true;
		// ********************************************************************************************************* //
		private void pesquisarMenuQuestoes_Click(object sender, EventArgs e) => busca(1);

		// CONTEÚDO  CONTEÚDO  CONTEÚDO  CONTEÚDO  CONTEÚDO  CONTEÚDO  CONTEÚDO  CONTEÚDO  CONTEÚDO  CONTEÚDO  //
		// CONTEÚDO  CONTEÚDO  CONTEÚDO  CONTEÚDO  CONTEÚDO  CONTEÚDO  CONTEÚDO  CONTEÚDO  CONTEÚDO  CONTEÚDO  //
		private void cursoMenuCadastrar_Click(object sender, EventArgs e)
		{
			telaCadastroConteudo(1);
			cadastrarMenuConteudo.Enabled = false;
		}

		private void disciplinaMenuCadastrar_Click(object sender, EventArgs e)
		{
			telaCadastroConteudo(2);
			cadastrarMenuConteudo.Enabled = false;
		}

		private void materiaMenuCadastrar_Click(object sender, EventArgs e)
		{
			telaCadastroConteudo(3);
			cadastrarMenuConteudo.Enabled = false;
		}

		private void telaCadastroConteudo(int pag)
		{
			cadastraConteudo = new CadastroConteudo(pag);
			cadastraConteudo.FormClosed += new FormClosedEventHandler(ativaMenuConteudo);
			abre(cadastraConteudo);
		}

		private void ativaMenuConteudo(object sender, FormClosedEventArgs e) => cadastrarMenuConteudo.Enabled = true;
		// ********************************************************************************************************* //
		private void pesquisarMenuConteudo_Click(object sender, EventArgs e) => busca(2);

		// AVALIAÇÕES  AVALIAÇÕES  AVALIAÇÕES  AVALIAÇÕES  AVALIAÇÕES  AVALIAÇÕES  AVALIAÇÕES  AVALIAÇÕES  //
		// AVALIAÇÕES  AVALIAÇÕES  AVALIAÇÕES  AVALIAÇÕES  AVALIAÇÕES  AVALIAÇÕES  AVALIAÇÕES  AVALIAÇÕES  //
		private void gerarMenuAvaliacoes_Click(object sender, EventArgs e)
		{
			telaGeraAvaliacao();
			gerarMenuAvaliacoes.Enabled = false;
		}

		private void telaGeraAvaliacao()
		{
			geraAvaliacao = new GerarAvaliacao();
			geraAvaliacao.FormClosed += new FormClosedEventHandler(ativaGerarAvaliacao);
			abre(geraAvaliacao);
		}

		private void ativaGerarAvaliacao(object sender, FormClosedEventArgs e) => gerarMenuAvaliacoes.Enabled = true;
		// ********************************************************************************************************* //
		private void historicoMenuAvaliacoes_Click(object sender, EventArgs e) => busca(3);

		// USUÁRIO  USUÁRIO  USUÁRIO  USUÁRIO  USUÁRIO  USUÁRIO  USUÁRIO  USUÁRIO  USUÁRIO  USUÁRIO  USUÁRIO  //
		// USUÁRIO  USUÁRIO  USUÁRIO  USUÁRIO  USUÁRIO  USUÁRIO  USUÁRIO  USUÁRIO  USUÁRIO  USUÁRIO  USUÁRIO  //
		private void configuracoesMenuUsuario_Click(object sender, EventArgs e)
		{
			telaConfiguracoes();
			configuracoesMenuUsuario.Enabled = false;
		}

		private void telaConfiguracoes()
		{
			config = new Configuracoes();
			config.FormClosed += new FormClosedEventHandler(ativaConfiguracoes);
			abre(config);
		}

		private void ativaConfiguracoes(object sender, FormClosedEventArgs e) => configuracoesMenuUsuario.Enabled = true;
		// ********************************************************************************************************* //
		private void logoutMenuUsuario_Click(object sender, EventArgs e)
		{
			var loga = new Login();

			if (Mensagem.logout())
			{
				fechaJanelas();
				Hide();

				if (loga.ShowDialog() == DialogResult.OK)
				{
					Refresh();
					Show();
				}
			}
		}
		// ********************************************************************************************************* //
		private void sairMenuUsuario_Click(object sender, EventArgs e) => Application.Exit();

		// AJUDA  AJUDA  AJUDA  AJUDA  AJUDA  AJUDA  AJUDA  AJUDA  AJUDA  AJUDA  AJUDA  AJUDA  AJUDA  AJUDA  //
		// AJUDA  AJUDA  AJUDA  AJUDA  AJUDA  AJUDA  AJUDA  AJUDA  AJUDA  AJUDA  AJUDA  AJUDA  AJUDA  AJUDA  //
		private void menuAjuda_Click(object sender, EventArgs e)
		{
			telaAjuda();
			menuAjuda.Enabled = false;
		}

		private void telaAjuda()
		{
			ajuda = new Ajuda();
			ajuda.FormClosed += new FormClosedEventHandler(ativaAjuda);
			abre(ajuda);
		}

		private void ativaAjuda(object sender, FormClosedEventArgs e) => menuAjuda.Enabled = true;




		// PESQUISA  PESQUISA  PESQUISA  PESQUISA  PESQUISA  PESQUISA  PESQUISA  PESQUISA  PESQUISA  PESQUISA  //
		// PESQUISA  PESQUISA  PESQUISA  PESQUISA  PESQUISA  PESQUISA  PESQUISA  PESQUISA  PESQUISA  PESQUISA  //
		private void busca(int origem)
		{
			telaPesquisa(origem);

			pesquisarMenuQuestoes.Enabled = false;
			pesquisarMenuConteudo.Enabled = false;
			historicoMenuAvaliacoes.Enabled = false;
		}

		private void telaPesquisa(int origem)
		{
			pesquisa = new Pesquisa(origem);
			pesquisa.FormClosed += new FormClosedEventHandler(ativaPesquisaQuestoes);
			abre(pesquisa);
		}

		private void ativaPesquisaQuestoes(object sender, FormClosedEventArgs e)
		{
			pesquisarMenuQuestoes.Enabled = true;
			pesquisarMenuConteudo.Enabled = true;
			historicoMenuAvaliacoes.Enabled = true;
		}
	}
}

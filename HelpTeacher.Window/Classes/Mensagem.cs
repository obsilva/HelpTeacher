// This Source Code Form is subject to the terms of the Mozilla 
// Public License, v. 2.0. If a copy of the MPL was not distributed 
// with this file, You can obtain one at https://mozilla.org/MPL/2.0/

// Authors: 
//		Otávio Bueno Silva <obsilva94@gmail.com>

using System.Windows.Forms;

namespace HelpTeacher.Classes
{
	public static class Mensagem
	{
		// BANCO  BANCO BANCO  BANCO BANCO  BANCO BANCO  BANCO BANCO BANCO  BANCO  BANCO  BANCO  BANCO  //
		// BANCO  BANCO BANCO  BANCO BANCO  BANCO BANCO  BANCO BANCO BANCO  BANCO  BANCO  BANCO  BANCO  //
		public static void falhaNaConexao() => MessageBox.Show("Sem conexão com o banco de dados!", "Conexão Falhou",
					MessageBoxButtons.OK, MessageBoxIcon.Error);

		public static void inicializandoBanco() => MessageBox.Show("Tentando iniciar banco de dados. \nAguarde um momento.",
					"Falha na Conexão", MessageBoxButtons.OK, MessageBoxIcon.Information);

		public static void falhaInicializacaoBanco() => MessageBox.Show("Falha na inialização do banco. \nTente novamente ou contate o administrador do sistema.",
					"Falha Inicialização Banco", MessageBoxButtons.OK, MessageBoxIcon.Error);

		public static void tabelasBancoFaltando() => MessageBox.Show("O Help Teacher tentará criar as tabelas automaticamente. \nAguarde a finalização do procedimento.",
					"Tabelas Faltando", MessageBoxButtons.OK, MessageBoxIcon.Error);
		public static void erroComandoSQL() => MessageBox.Show("Erro ao executar comando SQL. \nVerifique sua conexão com o banco.",
					"Erro SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);

		public static void falhaEncerramentoBanco() => MessageBox.Show("Falha no fechamento do banco.", "Falha Encerramento Banco",
					MessageBoxButtons.OK, MessageBoxIcon.Error);

		// LOGIN  LOGIN LOGIN  LOGIN LOGIN  LOGIN LOGIN  LOGIN LOGIN LOGIN  LOGIN  LOGIN  LOGIN  LOGIN  //
		// LOGIN  LOGIN LOGIN  LOGIN LOGIN  LOGIN LOGIN  LOGIN LOGIN LOGIN  LOGIN  LOGIN  LOGIN  LOGIN  //
		public static void loginInvalido() => MessageBox.Show("Login ou senha inválidos. Tente novamente.", "Erro Login",
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

		// CADASTRO QUESTAO CADASTRO QUESTAO  CADASTRO QUESTAO CADASTRO QUESTAO CADASTRO QUESTAO  //
		// CADASTRO QUESTAO CADASTRO QUESTAO  CADASTRO QUESTAO CADASTRO QUESTAO CADASTRO QUESTAO  //
		public static void alternativasEmBranco() => MessageBox.Show("Alternativa não preenchida.", "Alternativa em Branco",
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

		// CADASTRO CURSO CADASTRO CURSO  CADASTRO CURSO CADASTRO CURSO CADASTRO CURSO  CADASTRO CURSO  //
		// CADASTRO CURSO CADASTRO CURSO  CADASTRO CURSO CADASTRO CURSO CADASTRO CURSO  CADASTRO CURSO  //
		public static void cursoExistente() => MessageBox.Show("Já existe um registro relacionado a este curso. \nVerifique e tente novamente",
					"Curso Existente", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

		// CADASTRO DISCIPLINA CADASTRO DISCIPLINA  CADASTRO DISCIPLINA CADASTRO DISCIPLINA CADASTRO  //
		// CADASTRO DISCIPLINA CADASTRO DISCIPLINA  CADASTRO DISCIPLINA CADASTRO DISCIPLINA CADASTRO  //
		public static void disciplinaExistente() => MessageBox.Show("Já existe um registro relacionado a esta disciplina. \nVerifique e tente novamente",
					"Disciplina Existente", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		// CADASTRO MATÉRIA CADASTRO MATÉRIA  CADASTRO MATÉRIA CADASTRO MATÉRIA CADASTRO MATÉRIA  //
		// CADASTRO MATÉRIA CADASTRO MATÉRIA  CADASTRO MATÉRIA CADASTRO MATÉRIA CADASTRO MATÉRIA  //
		public static void materiaExistente() => MessageBox.Show("Já existe um registro relacionado a esta matéria. \nVerifique e tente novamente",
					"Matéria Existente", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

		// GERAÇÃO AVALIAÇÃO  GERAÇÃO AVALIAÇÃO  GERAÇÃO AVALIAÇÃO  GERAÇÃO AVALIAÇÃO  GERAÇÃO AVALIAÇÃO  GERAÇÃO  //
		// GERAÇÃO AVALIAÇÃO  GERAÇÃO AVALIAÇÃO  GERAÇÃO AVALIAÇÃO  GERAÇÃO AVALIAÇÃO  GERAÇÃO AVALIAÇÃO  GERAÇÃO  //
		public static void avaliacaoSemQuestoes() => MessageBox.Show("É necessário ter ao menos uma questão para gerar a avaliação.",
					"Sem Questões", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

		public static bool gerarAvaliacaoNovamente()
		{
			if (MessageBox.Show("Deseja gerar uma nova avaliação? \n" +
					"Não haverá registros da avaliação atual.", "Gerar Novamente",
					MessageBoxButtons.YesNo, MessageBoxIcon.Question,
					MessageBoxDefaultButton.Button2) == DialogResult.Yes)
			{
				return true;
			}
			return false;
		}

		// PESQUISA PESQUISA  PESQUISA PESQUISA PESQUISA PESQUISA  PESQUISA  PESQUISA  PESQUISA  PESQUISA  //
		// PESQUISA PESQUISA  PESQUISA PESQUISA PESQUISA PESQUISA  PESQUISA  PESQUISA  PESQUISA  PESQUISA  //
		public static void erroInesperado() => MessageBox.Show("Um erro inesperado ocorreu. \nContate o administrador do sistema.",
					"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

		// CONFIGURAÇÕES CONFIGURAÇÕES  CONFIGURAÇÕES CONFIGURAÇÕES CONFIGURAÇÕES CONFIGURAÇÕES  CONFIGURAÇÕES  //
		// CONFIGURAÇÕES CONFIGURAÇÕES  CONFIGURAÇÕES CONFIGURAÇÕES CONFIGURAÇÕES CONFIGURAÇÕES  CONFIGURAÇÕES  //
		public static bool logout()
		{
			if (MessageBox.Show("Tem certeza de que deseja fazer logout?",
					"Fazer Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
					MessageBoxDefaultButton.Button2) == DialogResult.Yes)
			{
				return true;
			}
			return false;
		}

		// GERAIS  GERAIS GERAIS  GERAIS GERAIS  GERAIS GERAIS  GERAIS GERAIS GERAIS  GERAIS  GERAIS  //
		// GERAIS  GERAIS GERAIS  GERAIS GERAIS  GERAIS GERAIS  GERAIS GERAIS GERAIS  GERAIS  GERAIS  //
		public static void procedimentoFinalizado() => MessageBox.Show("Inicie a aplicação para tentar novamente", "Procedimento Finalizado",
					MessageBoxButtons.OK, MessageBoxIcon.Information);

		public static void senhasDiferentes() => MessageBox.Show("Senhas incompatíveis. Tente novamente.", "Senhas Diferentes",
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

		public static void cadastradoEfetuado() => MessageBox.Show("Cadastro efetuado com sucesso.", "Cadastrado Efetuado",
					MessageBoxButtons.OK, MessageBoxIcon.Information);

		public static void erroCadastro() => MessageBox.Show("Falha ao tentar fazer o cadastro. \nVerifique sua conexão com o banco e tente novamente",
					"Erro Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Error);

		public static void dadosAlterados() => MessageBox.Show("Alteração efetuada com sucesso.", "Dados Alterado",
					 MessageBoxButtons.OK, MessageBoxIcon.Information);

		public static void erroAlteracao() => MessageBox.Show("Falha ao tentar modificar os dados. \nVerifique sua conexão com o banco e tente novamente",
					"Erro Alteração", MessageBoxButtons.OK, MessageBoxIcon.Error);

		public static void selecionarMateria() => MessageBox.Show("Nenhuma matéria selecionada.", "Selecionar Matéria",
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

		public static void campoEmBranco() => MessageBox.Show("Preencha todos os campos.", "Campo em Branco",
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
	}
}

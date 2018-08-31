using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace HelpTeacher.Classes
{
	internal class ConexaoBanco
	{
		private MySqlConnection conexao;
		private MySqlCommand comando;

		private bool abreConexao()
		{
			try
			{
				conexao = new MySqlConnection("server = 127.0.0.1; database = helpteacher; uid = root; pwd = 123456");
				conexao.Open();
				return true;
			}
			catch
			{
				startBanco();
				try
				{
					conexao = new MySqlConnection("server = 127.0.0.1; database = helpteacher; uid = root; pwd = 123456");
					conexao.Open();
					return true;
				}
				catch
				{
					Mensagem.falhaNaConexao();
					return false;
				}
			}
		}

		/* startBanco
		 * 
		 * Inicializa o banco de dados
		 */
		private void startBanco()
		{
			var startInfo = new ProcessStartInfo("cmd.exe")
			{
				RedirectStandardInput = true,
				RedirectStandardOutput = true,
				UseShellExecute = false,
				WindowStyle = ProcessWindowStyle.Hidden,
				FileName = @"C:\xampp\xampp_start.exe"
			};

			try
			{
				using (var execProcess = Process.Start(startInfo))
				{
					Mensagem.inicializandoBanco();
					execProcess.WaitForExit();
				}
			}
			catch
			{
				Mensagem.falhaInicializacaoBanco();
			}
		}

		public void fechaConexao() => conexao.Close();

		/* stopBanco
		 * 
		 * Para a execução do banco de dados
		 */
		public void stopBanco()
		{
			var startInfo = new ProcessStartInfo("cmd.exe")
			{
				RedirectStandardInput = true,
				RedirectStandardOutput = true,
				UseShellExecute = false,
				WindowStyle = ProcessWindowStyle.Minimized,
				FileName = @"C:\xampp\xampp_stop.exe"
			};

			try
			{
				using (var execProcess = Process.Start(startInfo))
				{
					execProcess.WaitForExit();
				}
			}
			catch
			{
				Mensagem.falhaEncerramentoBanco();
			}
		}

		public bool conexaoOK()
		{
			if (abreConexao())
			{
				fechaConexao();
				return true;
			}
			else
			{
				return false;
			}
		}

		/* executeComando
		 * 
		 * Função sobrecarregada. Executa um comando, em suas duas implementações, mas na primeira
		 * apenas executa um comando, sem retorno, na segunda implementação executa o comando
		 * e retorna o resultado da execução através do DataReader passado por referência, e na
		 * terceira, retorna o resultado através do DataAdapter passado por referência
		 */
		public bool executeComando(string comand)
		{
			if (abreConexao())
			{
				try
				{
					comando = new MySqlCommand(comand, conexao);
					comando.ExecuteNonQuery();
					fechaConexao();
					return true;
				}
				catch
				{
					fechaConexao();
					Mensagem.erroComandoSQL();
				}
			}
			return false;
		}

		public bool executeComando(string comand, ref MySqlDataReader result)
		{
			if (abreConexao())
			{
				try
				{
					comando = new MySqlCommand(comand, conexao);
					result = comando.ExecuteReader();
					return true;
				}
				catch
				{
					fechaConexao();
					Mensagem.erroComandoSQL();
				}
			}
			return false;
		}

		public bool executeComando(string comand, ref MySqlDataAdapter result)
		{
			if (abreConexao())
			{
				try
				{
					result = new MySqlDataAdapter(comand, conexao);
					return true;
				}
				catch
				{
					fechaConexao();
					Mensagem.erroComandoSQL();
				}
			}
			return false;
		}
	}
}
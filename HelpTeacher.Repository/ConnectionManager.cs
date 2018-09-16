using MySql.Data.MySqlClient;

namespace HelpTeacher.Repository
{
	/// <summary>Gerencia a conexão com o banco de dados.</summary>
	public class ConnectionManager
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
				try
				{
					conexao = new MySqlConnection("server = 127.0.0.1; database = helpteacher; uid = root; pwd = 123456");
					conexao.Open();
					return true;
				}
				catch
				{
					return false;
				}
			}
		}

		public void fechaConexao() => conexao.Close();

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
				}
			}
			return false;
		}
	}
}

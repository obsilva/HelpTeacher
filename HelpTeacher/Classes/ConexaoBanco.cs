using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace HelpTeacher.Classes
{
    class ConexaoBanco
    {
        private MySqlConnection conexao;
        private MySqlCommand comando;

        private Boolean abreConexao()
        {
            try
            {
                conexao = new MySqlConnection("server = 127.0.0.1; database = helpteacher; uid = root");
                conexao.Open();
                return true;
            }
            catch
            {
                startBanco();
                try
                {
                    conexao = new MySqlConnection("server = 127.0.0.1; database = helpteacher; uid = root");
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
            ProcessStartInfo startInfo = new ProcessStartInfo("cmd.exe");

            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = @"C:\xampp\xampp_start.exe";

            try
            {
                using (Process execProcess = Process.Start(startInfo))
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

        public void fechaConexao()
        {
            conexao.Close();
        }

        /* stopBanco
         * 
         * Para a execução do banco de dados
         */
        public void stopBanco()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo("cmd.exe");

            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            startInfo.WindowStyle = ProcessWindowStyle.Minimized;
            startInfo.FileName = @"C:\xampp\xampp_stop.exe";

            try
            {
                using (Process execProcess = Process.Start(startInfo))
                {
                    execProcess.WaitForExit();
                }
            }
            catch
            {
                Mensagem.falhaEncerramentoBanco();
            }
        }

        public Boolean conexaoOK()
        {
            if (abreConexao())
            {
                fechaConexao();
                return true;
            }
            else
                return false;
        }
        
        /* executeComando
         * 
         * Função sobrecarregada. Executa um comando, em suas duas implementações, mas na primeira
         * apenas executa um comando, sem retorno, na segunda implementação executa o comando
         * e retorna o resultado da execução através do DataReader passado por referência, e na
         * terceira, retorna o resultado através do DataAdapter passado por referência
         */
        public Boolean executeComando(String comand)
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

        public Boolean executeComando(String comand, ref MySqlDataReader result)
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

        public Boolean executeComando(String comand, ref MySqlDataAdapter result)
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
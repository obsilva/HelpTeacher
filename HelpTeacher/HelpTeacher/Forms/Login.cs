using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HelpTeacher.Classes;

namespace HelpTeacher.Forms
{
    public partial class Login : Form
    {
        private ConexaoBanco banco = new ConexaoBanco();
        private MySql.Data.MySqlClient.MySqlDataReader respostaBanco;

        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            if (!testaBanco())
            {
                Application.Exit();
            }
        }

        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {                
               fazLogin();
            }
        }

        private void btnEntrarLogin_Click(object sender, EventArgs e)
        {
            fazLogin();
        }

        private void btnCancelarLogin_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private Boolean testaBanco()
        {
            if (banco.conexaoOK())
            {
                if (tabelasOK())
                {
                    return true;
                }
            }
            return false;
        }
       

        /* tabelasOK
         * 
         * Testa se as tebelas necessárias existem. Se alguma tabela não existir,
         * chama a função que tenta criar todas as tabelas
         */
        public Boolean tabelasOK()
        {
            if (banco.executeComando("SELECT * " +
                        "FROM INFORMATION_SCHEMA.TABLES " +
                        "WHERE table_type = 'BASE TABLE' " +
                            "AND table_schema='helpteacher' " +
                        "ORDER BY table_name ASC", ref respostaBanco))
            {
                if (respostaBanco.HasRows)
                {
                    respostaBanco.Read();
                    if (respostaBanco["TABLE_NAME"].ToString().Equals("hta1"))
                    {
                        respostaBanco.Read();
                        if (respostaBanco["TABLE_NAME"].ToString().Equals("htb1"))
                        {
                            respostaBanco.Read();
                            if (respostaBanco["TABLE_NAME"].ToString().Equals("htb2"))
                            {
                                respostaBanco.Read();
                                if (respostaBanco["TABLE_NAME"].ToString().Equals("htb3"))
                                {
                                    respostaBanco.Read();
                                    if (respostaBanco["TABLE_NAME"].ToString().Equals("htc1"))
                                    {
                                        respostaBanco.Read();
                                        if (respostaBanco["TABLE_NAME"].ToString().Equals("htc2"))
                                        {
                                            respostaBanco.Read();
                                            if (respostaBanco["TABLE_NAME"].ToString().Equals("htc3"))
                                            {
                                                respostaBanco.Read();
                                                if (respostaBanco["TABLE_NAME"].ToString().Equals("htd1"))
                                                {
                                                    respostaBanco.Read();
                                                    if (respostaBanco["TABLE_NAME"].ToString().Equals("htd2"))
                                                    {
                                                        respostaBanco.Read();
                                                        banco.fechaConexao();
                                                        respostaBanco.Close();
                                                        return true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                banco.fechaConexao();
                respostaBanco.Close();
                Mensagem.tabelasBancoFaltando();
                criaTabelas();
            }
            return false;
        }

        /* criaTabelas
         * 
         * Cria as tabelas faltantes no banco de dados
         */
        private void criaTabelas()
        {
            /* USUÁRIOS */
            banco.executeComando("CREATE TABLE IF NOT EXISTS hta1 (" +
                        "A1_COD	TINYINT UNSIGNED NOT NULL AUTO_INCREMENT, " +
                        "A1_USUARIO	VARCHAR(50) NOT NULL, " +
                        "A1_SENHA VARCHAR(32) NOT NULL, " +    
                        "CONSTRAINT PK_HTA1	PRIMARY KEY (A1_COD)" +
                    ") ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8");

            banco.executeComando("INSERT INTO hta1 VALUES" +
                    "(1, 'admin', '21232F297A57A5A743894A0E4A801FC3'), " +
                    "(2, 'user', 'D41D8CD98F00B204E9800998ECF8427E')");

            /* QUESTÕES */
            banco.executeComando("CREATE TABLE IF NOT EXISTS htb1 (" +
                        "B1_COD	SMALLINT UNSIGNED NOT NULL AUTO_INCREMENT, " +
                        "B1_QUESTAO	TEXT NOT NULL, " +
                        "B1_OBJETIV	ENUM('0', '1') NOT NULL, " +    
                        "CONSTRAINT PK_HTB1 PRIMARY KEY(B1_COD)" +
                    ") ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8");

            banco.executeComando("CREATE TABLE IF NOT EXISTS htb2 (" +
                        "B2_QUESTAO	SMALLINT UNSIGNED NOT NULL, " +
                        "B2_ARQUIVO	VARCHAR(16) NOT NULL, " +
                        "B2_DELETED	ENUM('0', '1') NOT NULL DEFAULT '0', " +    
                        "CONSTRAINT PK_HTB2 PRIMARY KEY (B2_QUESTAO, B2_ARQUIVO), " +	
                        "CONSTRAINT FK_HTB2_HTB1 FOREIGN KEY (B2_QUESTAO) " +
                        "REFERENCES HTB1 (B1_COD) ON DELETE RESTRICT ON UPDATE CASCADE" +
                  ") ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8");

            /* TABELA DE MATERIA DAS QUESTOES */
            banco.executeComando("CREATE TABLE IF NOT EXISTS htb2 (" +
                        "B3_COD	SMALLINT UNSIGNED NOT NULL AUTO_INCREMENT, " +
                        "B3_QUESTAO	SMALLINT UNSIGNED NOT NULL, " +
                        "B3_MATERIA	SMALLINT UNSIGNED NOT NULL, " +
                        "B3_PADRAO ENUM('0', '1') NOT NULL, " +
                        "B3_USADA ENUM('0', '1') NOT NULL, " +
                        "B3_DELETED	ENUM('0', '1') NOT NULL DEFAULT '0', " +    
                        "CONSTRAINT PK_HTB3 PRIMARY KEY (B3_COD), " +        
	                    "CONSTRAINT FK_HTB3_HTB1 FOREIGN KEY (B3_QUESTAO) " +
		                "REFERENCES HTB1 (B1_COD) ON DELETE RESTRICT ON UPDATE CASCADE, " +	
                        "CONSTRAINT FK_HTB3_HTC3 FOREIGN KEY (B3_MATERIA) " +
                        "REFERENCES HTC3 (C3_COD) ON DELETE RESTRICT ON UPDATE CASCADE, " +	
                        "CONSTRAINT UN_QUESTAO_MATERIA UNIQUE (B3_QUESTAO, B3_MATERIA)" +                        
                  ") ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8");

            /* CURSOS */
            banco.executeComando("CREATE TABLE IF NOT EXISTS htc1 (" +
                        "C1_COD	TINYINT UNSIGNED NOT NULL AUTO_INCREMENT, " +
                        "C1_NOME VARCHAR(40) NOT NULL, " +     
                        "CONSTRAINT PK_HTC1 PRIMARY KEY (C1_COD)" +
                    ") ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8");

            /* DISCIPLINAS */
            banco.executeComando("CREATE TABLE IF NOT EXISTS htc2 (" +
                        "C2_COD	TINYINT UNSIGNED NOT NULL AUTO_INCREMENT, " +
                        "C2_NOME VARCHAR(90) NOT NULL, " +
                        "C2_CURSO TINYINT UNSIGNED NOT NULL, " +    
                        "CONSTRAINT PK_HTC2	PRIMARY KEY (C2_COD), " +                               
	                    "CONSTRAINT FK_HTC2_HTC1 FOREIGN KEY (C2_CURSO) " +
		                "REFERENCES HTC1 (C1_COD) ON DELETE RESTRICT ON UPDATE CASCADE" +
                    ") ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8");

            /* MATÉRIAS */
            banco.executeComando("CREATE TABLE IF NOT EXISTS htc3 (" +
                        "C3_COD SMALLINT UNSIGNED NOT NULL AUTO_INCREMENT, " +
                        "C3_NOME VARCHAR(120) NOT NULL, " +
                        "C3_DISCIP TINYINT UNSIGNED NOT NULL, " +
                        "C3_DELETED	ENUM('0', '1') NOT NULL DEFAULT '0', " +    
                        "CONSTRAINT PK_HTC3 PRIMARY KEY (C3_COD), " +        
	                    "CONSTRAINT FK_HTC3_HTC2 FOREIGN KEY (C3_DISCIP) " +
		                "REFERENCES HTC2 (C2_COD) ON DELETE RESTRICT ON UPDATE CASCADE" +
                    ") ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8");

            /* AVALIAÇÕES */
            banco.executeComando("CREATE TABLE IF NOT EXISTS htd1 (" +
                       "D1_COD SMALLINT UNSIGNED NOT NULL AUTO_INCREMENT, " +
                       "D1_INEDITA ENUM('0', '1') NOT NULL, " +
	                   "D1_DATA	DATE NOT NULL, " +    
                       "CONSTRAINT PK_HTD1 PRIMARY KEY(D1_COD)" +
                    ") ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8");

            banco.executeComando("CREATE TABLE IF NOT EXISTS htd2 (" +
                       "D2_PROVA SMALLINT UNSIGNED NOT NULL, " +
                       "D2_QUESTAO SMALLINT UNSIGNED NOT NULL, " +                       
                       "D2_DELETED ENUM('0', '1') NOT NULL DEFAULT '0' " +
                       "CONSTRAINT PK_HTD2 PRIMARY KEY(D2_PROVA, D2_QUESTAO), " +
                       "CONSTRAINT FK_HTD2_HTD1	FOREIGN KEY (D2_PROVA) " +
                       "REFERENCES HTD1 (D1_COD) ON DELETE RESTRICT ON UPDATE CASCADE, " +
                       "CONSTRAINT FK_HTD2_HTB3	FOREIGN KEY (D2_QUESTAO) " +
                       "REFERENCES HTB3 (B3_COD) ON DELETE RESTRICT ON UPDATE CASCADE" +
                    ") ENGINE = InnoDB DEFAULT CHARSET = utf8;");            

            /* ÍNDICES */
            /*banco.executeComando("ALTER TABLE htb1 " +
                    "ADD KEY B1_MATERIA (B1_MATERIA)");
            banco.executeComando("ALTER TABLE htc2 " +
                    "ADD KEY C2_CURSO (C2_CURSO)");
            banco.executeComando("ALTER TABLE htc3 " +
                    "ADD KEY C3_DISCIPL (C3_DISCIPL)");

            /* CHAVES */
            /*banco.executeComando("ALTER TABLE htb1 " +
                    "ADD CONSTRAINT FK_C3_COD FOREIGN KEY (B1_MATERIA) " +
                        "REFERENCES htc3 (C3_COD) ON UPDATE CASCADE");
            banco.executeComando("ALTER TABLE htc2 " +
                    "ADD CONSTRAINT FK_C1_COD FOREIGN KEY (C2_CURSO) " +
                        "REFERENCES htc1 (C1_COD) ON UPDATE CASCADE");
            banco.executeComando("ALTER TABLE htc3 " +
                    "ADD CONSTRAINT FK_C2_COD FOREIGN KEY (C3_DISCIPL) " +
                        "REFERENCES htc2 (C2_COD) ON UPDATE CASCADE");*/

            Mensagem.procedimentoFinalizado();
        }

        private void fazLogin()
        {
            if (txtLogin.Text.Contains("'"))
                txtLogin.Text = txtLogin.Text.Replace("'", "''");

            if (banco.executeComando("SELECT * " +
                    "FROM hta1 " +
                    "WHERE A1_USUARIO = '" + txtLogin.Text + "' AND " +
                        "A1_SENHA = '" + MD5.gerarHash(txtPassword.Text) +
                        "'", ref respostaBanco))
            {
                if (respostaBanco.HasRows)
                {
                    respostaBanco.Read();
                    Usuario.ID = Convert.ToInt32(respostaBanco["A1_COD"].ToString());
                    Usuario.Login = respostaBanco["A1_USUARIO"].ToString();
                    Usuario.Password = respostaBanco["A1_SENHA"].ToString();                    
                    
                    banco.fechaConexao();
                    respostaBanco.Close();                    
                }
                else
                {
                    Mensagem.loginInvalido();
                    txtLogin.Focus();
                    txtLogin.SelectAll();
                }
                banco.fechaConexao();
                respostaBanco.Close();
            }
        }
    }
}

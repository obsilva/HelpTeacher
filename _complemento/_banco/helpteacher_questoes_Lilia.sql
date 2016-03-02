-- phpMyAdmin SQL Dump
-- version 4.4.14
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: 10-Nov-2015 às 04:08
-- Versão do servidor: 5.6.26
-- PHP Version: 5.6.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `helpteacher`
--

-- --------------------------------------------------------

--
-- Estrutura da tabela `hta1`
--

CREATE TABLE IF NOT EXISTS `hta1` (
  `A1_COD` int(10) unsigned NOT NULL,
  `A1_LOGIN` varchar(50) NOT NULL,
  `A1_PWD` varchar(32) NOT NULL,
  `A1_ALTPWD` varchar(1) DEFAULT NULL,
  `A1_STOPBD` varchar(1) DEFAULT NULL
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

--
-- Extraindo dados da tabela `hta1`
--

INSERT INTO `hta1` (`A1_COD`, `A1_LOGIN`, `A1_PWD`, `A1_ALTPWD`, `A1_STOPBD`) VALUES
(1, 'admin', '21232F297A57A5A743894A0E4A801FC3', NULL, NULL),
(2, 'user', 'D41D8CD98F00B204E9800998ECF8427E', NULL, NULL);

-- --------------------------------------------------------

--
-- Estrutura da tabela `htb1`
--

CREATE TABLE IF NOT EXISTS `htb1` (
  `B1_COD` int(10) unsigned NOT NULL,
  `B1_QUEST` text NOT NULL,
  `B1_OBJETIV` varchar(1) DEFAULT NULL,
  `B1_ARQUIVO` varchar(520) DEFAULT NULL,
  `B1_USADA` varchar(1) DEFAULT NULL,
  `B1_MATERIA` int(10) unsigned NOT NULL,
  `B1_PADRAO` varchar(1) DEFAULT NULL,
  `D_E_L_E_T` varchar(1) DEFAULT NULL
) ENGINE=InnoDB AUTO_INCREMENT=60 DEFAULT CHARSET=utf8;

--
-- Extraindo dados da tabela `htb1`
--

INSERT INTO `htb1` (`B1_COD`, `B1_QUEST`, `B1_OBJETIV`, `B1_ARQUIVO`, `B1_USADA`, `B1_MATERIA`, `B1_PADRAO`, `D_E_L_E_T`) VALUES
(1, 'Seja A = {1,2, {2},{1,2}} determinar P (A), ou seja o conjunto das partes de A.', NULL, NULL, NULL, 1, NULL, NULL),
(2, 'Se A não está contido B e B = {10, 23, 12, {1,2}}, então A pode ser:\r\n\r\nA) {10}\r\nB) {1}\r\nC) {10, 23, 12}\r\nD) {15, 12}∩{13,12}\r\nE) {10, 23, 12, {1,2}}', '*', NULL, NULL, 1, NULL, NULL),
(3, 'Qual o resultado da operação (A – B) n (B – A):', NULL, '3_1', NULL, 1, NULL, NULL),
(4, 'Escreva na forma tabular os conjuntos dos intervalos a seguir:', NULL, '4_1', NULL, 1, NULL, NULL),
(5, 'Um professor, ao lecionar Teoria dos Conjuntos em uma certa turma, realizou uma pesquisa sobre as preferências clubísticas de seus n\r\nalunos, tendo chegado ao seguinte resultado:\r\n\r\n• 23 alunos torcem pelo Paysandu Sport Club;\r\n• 23 alunos torcem pelo Clube do Remo;\r\n• 15 alunos torcem pelo Clube de Regatas Vasco da Gama;\r\n• 6 alunos torcem pelo Paysandu e pelo Vasco;\r\n• 5 alunos torcem pelo Vasco e pelo Remo.\r\n\r\nSe designarmos por A o conjunto dos torcedores do Paysandu, por B o conjunto dos torcedores do Remo e por C o conjunto dos torcedores do Vasco,\r\ntodos da referida turma, teremos, evidentemente, A n B = Ø. Responda qual o número de alunos desta turma (n).', NULL, NULL, NULL, 1, NULL, NULL),
(6, 'Se A, B e A união B são conjuntos com 55, 26 e 15 elementos, respectivamente, então qual o número de elementos do conjunto A interseção B?', NULL, NULL, NULL, 1, NULL, NULL),
(7, 'Seja A = {1,2,3,4} e B = {a,b} determinar AxB.', NULL, NULL, NULL, 1, NULL, NULL),
(8, 'Escreva uma das possíveis relações binários dos conjuntos da Questão 7 (R contém A × B).', NULL, NULL, NULL, 1, NULL, NULL),
(9, 'Um levantamento sócio-econômico entre os habitantes de uma cidade revelou que, exatamente: 17% têm casa própria;22% têm automóvel; 8% têm casa própria e automóvel. Qual o percentual dos que não têm casa própria nem automóvel?', NULL, NULL, NULL, 1, NULL, NULL),
(10, 'Dez mil aparelhos de TV foram examinados depois de um ano de uso e constatou-se que 4.000 deles apresentavam problemas de\r\nimagem, 2.800 tinham problemas de som e 3.500 não apresentavam nenhum dos tipos de problema citados. Qual o número de aparelhos que apresentavam somente problemas de imagem.', NULL, NULL, NULL, 1, NULL, NULL),
(11, 'Prove por indução que 13 + 23 + 33 + ... + n3 = (n2/4)(n + 1)2, onde n é número natural.', NULL, NULL, NULL, 2, NULL, NULL),
(12, 'Prove por indução que 1.2 + 2.3 + 3.4 + ... + n(n + 1) = (n/3)(n + 1)(n + 2), onde n é número natural.', NULL, NULL, NULL, 2, NULL, NULL),
(13, 'Prove por indução que', NULL, '13_1', NULL, 2, NULL, NULL),
(14, 'Prove por indução que 1 + 3 + 5 + ... + (2n - 1) = n2 ; n =1:', NULL, NULL, NULL, 2, NULL, NULL),
(15, 'Na divisão do inteiro a = 427 por um inteiro positivo b, o quociente é 12 e o resto é r. Achar os possíveis valores de b e r.', NULL, NULL, NULL, 2, NULL, NULL),
(16, 'Sejam a, b e c inteiros. Mostrar que:\r\n\r\n(a) se a | b, então a | bc.\r\n\r\n(b) se a | b e se a | c, então a2 | bc.', NULL, NULL, NULL, 2, NULL, NULL),
(17, 'Verdadeiro ou falso: se a | (b + c), então a | b ou a | c.', NULL, NULL, NULL, 2, NULL, NULL),
(18, 'Na palavra NORTE, quantos anagramas podem ser formados? Quantos começam com vogal?', NULL, NULL, NULL, 3, NULL, NULL),
(19, 'Os resultados do último sorteio da Mega-Sena foram os números 04, 10, 26, 37, 47 e 57. De quantas maneiras distintas pode ter ocorrido essa sequência de resultados?', NULL, NULL, NULL, 3, NULL, NULL),
(20, 'Considere todos os números formados por seis algarismos distintos obtidos permutando-se, de todas as formas possíveis, os algarismos 1, 2, 3, 4, 5 e 6. Determine quantos números é possível formar (no total) e quantos números se iniciam com o algarismo 1.', NULL, NULL, NULL, 3, NULL, NULL),
(21, 'Utilizando o nome COPACABANA, calcule o número de anagramas formados desconsiderando aqueles em que ocorrem repetições consecutivas de letras.', NULL, NULL, NULL, 3, NULL, NULL),
(22, 'Em um torneio de futsal um time obteve 8 vitórias, 5 empates e 2 derrotas, nas 15 partidas disputadas. De quantas maneiras distintas esses resultados podem ter ocorrido?', NULL, NULL, NULL, 3, NULL, NULL),
(23, 'Em uma sala de aula existem 12 alunas, onde uma delas chama-se Carla, e 8 alunos, onde um deles atende pelo nome de Luiz. Deseja-se formar comissões de 5 alunas e 4 alunos. Determine o número de comissões, onde simultaneamente participam Carla e Luiz.', NULL, NULL, NULL, 3, NULL, NULL),
(24, 'Um time de futebol é composto de 11 jogadores, sendo 1 goleiro, 4 zagueiros, 4 meio campistas e 2 atacantes. Considerando-se que o técnico dispõe de 3 goleiros, 8 zagueiros, 10 meio campistas e 6 atacantes, determine o número de maneiras possíveis que esse time pode ser formado.', NULL, NULL, NULL, 3, NULL, NULL),
(25, 'Um pesquisador científico precisa escolher três cobaias, num grupo de oito cobaias. Determine o número de maneiras que ele pode realizar a escolha.', NULL, NULL, NULL, 3, NULL, NULL),
(26, 'No jogo de basquetebol, cada time entra em quadra com cinco jogadores. Considerando-se que um time para disputar um campeonato necessita de pelo menos 12 jogadores, e que desses, 2 são titulares absolutos, determine o número de equipes que o técnico poderá formar com o restante dos jogadores, sendo que eles atuam em qualquer posição.', NULL, NULL, NULL, 3, NULL, NULL),
(27, 'Um número de telefone é formado por 8 algarismos. Determine quantos números de telefone podemos formar com algarismos diferentes, que comecem com 2 e terminem com 8.', NULL, NULL, NULL, 3, NULL, NULL),
(28, 'Em uma urna de sorteio de prêmios existem dez bolas enumeradas de 0 a 9. Determine o número de possibilidades existentes num sorteio cujo prêmio é formado por uma sequência de 6 algarismos.', NULL, NULL, NULL, 3, NULL, NULL),
(29, 'Em uma escola está sendo realizado um torneio de futebol de salão, no qual dez times estão participando. Quantos jogos podem ser realizados entre os times participantes em turno e returno?', NULL, NULL, NULL, 3, NULL, NULL),
(30, 'Otávio, João, Mário, Luís, Pedro, Roberto e Fábio estão apostando corrida. Quantos são os agrupamentos possíveis para os três primeiros colocados?', NULL, NULL, NULL, 3, NULL, NULL),
(31, 'Descreva com suas palavras, e se possível ilustrando, quais camadas são acessadas para que um programa em alto-nível seja, de fato, executado pelo processador.\r\n', NULL, NULL, NULL, 4, NULL, NULL),
(32, 'Descreva com suas palavras, e se possível ilustrando, quais camadas são acessadas para que um programa em alto-nível seja, de fato, executado pelo processador.\r\n', NULL, NULL, NULL, 5, NULL, NULL),
(33, 'Descreva com suas palavras, e se possível ilustrando, quais camadas são acessadas para que um programa em alto-nível seja, de fato, executado pelo processador.\r\n', NULL, NULL, NULL, 6, NULL, NULL),
(34, 'Descreva com suas palavras o nível ISA.', NULL, NULL, NULL, 6, NULL, NULL),
(35, 'Cite as três principais características da ISA.', NULL, NULL, NULL, 6, NULL, NULL),
(36, 'Caso você deseje analisar o código Assembly de um programa, você pode instruir ao compilador que termine a compilação assim que gerar o código em linguagem de montagem, sem gerar o código em linguagem de máquina. Desta forma, imagine que este processo foi utilizado e gerado o seguinte código em linguagem de montagem:\r\n\r\nsll $t1, $s0, 2\r\nadd $t1, $s1, $t1\r\nlw $t0, 0($t1)\r\nlw $t2, 4($t1)\r\nsw $t2, 0($t1)\r\nsw $t0, 4($t1)\r\n\r\nTente determinar o possível código em linguagem C que gerou estas instruções.', NULL, NULL, NULL, 5, NULL, NULL),
(37, 'Determine o código na linguagem de montagem do MIPS para o seguintes trechos em C (cada tópico é de um código diferente):\r\n\r\n• g = h + A[h];\r\n• while(a!=b) a = A[c] + 1;\r\n• A[12] = h + A[8];\r\n• for(int i=0; i != 10; i++) A[i] = 0;\r', NULL, NULL, NULL, 5, NULL, NULL),
(38, ' Qual o principal representante dos processadores RISC atualmente?\r', NULL, NULL, NULL, 4, NULL, NULL),
(39, 'Em RISC, quais os dois principais tipos de instruções? Cite exemplos destes.', NULL, NULL, NULL, 4, NULL, NULL),
(40, 'A Figura a seguir, obtida de (Trovão, 2015), apresenta a evolução das principais ISAs. Nela podemos constatar que a maioria das máquinas atuais possui uma mistura de CISC e RISC, desde 1999, sendo atualmente utilizada a ISA x86 64 na grande maioria das máquinas. Qual a vantagem desta mistura em relação a CISC e/ou RISC puros?', NULL, '40_1', NULL, 6, NULL, NULL),
(41, 'Angélica concede a Otávia, pelo prazo de vinte anos, direito real de usufruto sobre imóvel de que é proprietária. O direito real é constituído por meio de escritura pública, que é registrada no competente Cartório do Registro de Imóveis. Cinco anos depois da constituição do usufruto, Otávia falece, deixando como única herdeira sua filha Patrícia. Sobre esse caso, assinale a afirmativa correta.\r\n\r\na) Patrícia herda o direito real de usufruto sobre o imóvel.\r\nb) Patrícia adquire somente o direito de uso sobre o imóvel.\r\nc) O direito real de usufruto extingue-se com o falecimento de Otávia.\r\nd) Patrícia deve ingressar em juízo para obter sentença constitutiva do seu direito real de usufruto sobre o imóvel. \r\ne)  N.D.A', '*', NULL, NULL, 7, NULL, NULL),
(42, 'Ester, viúva, tinha duas filhas muito ricas, Marina e Carina. Como as filhas não necessitam de seus bens, Ester deseja beneficiar sua irmã, Ruth, por ocasião de sua morte, destinando-lhe toda a sua herança, bens que vieram de seus pais, também pais de Ruth. Ester o(a) procura como advogado(a), indagando se é possível deixar todos os seus bens para sua irmã. Deseja fazê-lo por meio de testamento público, devidamente lavrado em Cartório de Notas, porque suas filhas estão de acordo com esse seu desejo. Assinale a opção que indica a orientação correta a ser transmitida a Ester.\r\n\r\na) Em virtude de ter descendentes, Ester não pode dispor de seus bens por testamento.\r\nb) Ester só pode dispor de 1/3 de seu patrimônio em favor de Ruth, cabendo o restante de sua herança às suas filhas Marina e Carina, dividindo-se igualmente o patrimônio.\r\nc) Ester pode dispor de todo o seu patrimônio em favor de Ruth, já que as filhas estão de acordo.\r\nd) Ester pode dispor de 50% de seu patrimônio em favor de Ruth, cabendo os outros 50% necessariamente às suas filhas, Marina e Carina, na proporção de 25% para cada uma.\r\ne) N.D.A', '*', NULL, NULL, 7, NULL, NULL),
(43, 'Mateus é proprietário de um terreno situado em área rural do estado de Minas Gerais. Por meio de escritura pública levada ao cartório do registro de imóveis, Mateus concede, pelo prazo de vinte anos, em favor de Francisco, direito real de superfície sobre o aludido terreno. A escritura prevê que Francisco deverá ali construir um edifício que servirá de escola para a população local. A escritura ainda prevê que, em contrapartida à concessão da superfície, Francisco deverá pagar a Mateus a quantia de R$ 30.000,00 (trinta mil reais). A escritura também prevê que, em caso de alienação do direito de superfície por Francisco, Mateus terá direito a receber quantia equivalente a 3% do valor da transação.Nesse caso, é correto afirmar que :\r\n\r\na) é nula a concessão de direito de superfície por prazo determinado, haja vista só se admitir, no direito brasileiro, a concessão perpétua.\r\nb) é nula a cláusula que prevê o pagamento de remuneração em contrapartida à concessão do direito de superfície, haja vista ser a concessão ato essencialmente gratuito.\r\nc) é nula a cláusula que estipula em favor de Mateus o pagamento de determinada quantia em caso de alienação do direito de superfície.\r\nd) é nula a cláusula que obriga Francisco a construir um edifício no terreno.\r\ne) N.D.A', '*', NULL, NULL, 7, NULL, NULL),
(44, 'Gilvan (devedor) contrai empréstimo com Haroldo (credor) para o pagamento com juros do valor do mútuo no montante de R$ 10.000,00. Para facilitar a percepção do crédito, a parte do polo ativo obrigacional ainda facultou, no instrumento contratual firmado, o pagamento do montante no termo avençado ou a entrega do único cavalo da raça manga larga marchador da fazenda, conforme escolha a ser feita pelo devedor. Ante os fatos narrados, assinale a afirmativa correta.\r\n\r\na) Trata-se de obrigação alternativa.\r\nb) Cuida-se de obrigação de solidariedade em que ambas as prestações são infungíveis.\r\nc) Acaso o animal morra antes da concentração, extingue-se a obrigação.\r\nd) O contrato é eivado de nulidade, eis que a escolha da prestação cabe ao credor.\r\ne) N.D.A', '*', NULL, NULL, 7, NULL, NULL),
(45, 'Flávia vendeu para Quitéria seu apartamento e incluiu, no contrato de compra e venda, cláusula pela qual se reservava o direito de recomprá-lo no prazo máximo de 2 (dois) anos. Antes de expirado o referido prazo, Flávia pretendeu exercer seu direito, mas Quitéria se recusou a receber o preço. Sobre o fato narrado, assinale a afirmativa correta.\r\n\r\na) A cláusula pela qual Flávia se reservava o direito de recomprar o imóvel é ilícita e abusiva, uma vez que Quitéria, ao se tornar proprietária do bem, passa a ter total e irrestrito poder de disposição sobre ele.\r\nb) A cláusula pela qual Flávia se reservava o direito de recomprar o imóvel é válida, mas se torna ineficaz diante da justa recusa de Quitéria em receber o preço devido.\r\nc) A disposição incluída no contrato é uma cláusula de preferência, a impor ao comprador a obrigação de oferecer ao vendedor a coisa, mas somente quando decidir vendê-la.\r\nd) A disposição incluída no contrato é uma cláusula de retrovenda, entendida como o ajuste por meio do qual o vendedor se reserva o direito de resolver o contrato de compra e venda mediante pagamento do preço recebido e das despesas, recuperando a coisa imóvel. \r\ne) N.D.A', '*', NULL, NULL, 7, NULL, NULL),
(46, 'Laura formou-se em uma prestigiada Faculdade de Direito, mas sua prática advocatícia foi limitada, o que a impediu de ter experiência maior no trato com os clientes. Realizou seus primeiros processos para amigos e parentes, cobrando módicas quantias referentes a honorários advocatícios. Ao receber a cliente Telma, próspera empresária, e aceitar defender os seus interesses judicialmente, fica em dúvida quanto aos termos de cobrança inicial dos honorários pactuados. Em razão disso, consulta o advogado Luciano, que lhe informa, segundo os termos do Estatuto da Advocacia, que salvo estipulação em contrário,\r\n\r\na) metade dos honorários é devida no início do serviço.\r\nb) um quinto dos honorários é devido ao início do processo judicial.\r\nc) a integralidade dos honorários é devida até a decisão de primeira instância.\r\nd) um terço dos honorários é devido no início do serviço.\r\ne) N.D.A', '*', NULL, NULL, 8, NULL, NULL),
(47, 'O advogado Nelson, após estabelecer seu escritório em local estratégico nas proximidades dos prédios que abrigam os órgãos judiciários representantes de todas as esferas da Justiça, resolve publicar anúncio em que, além dos seus títulos acadêmicos, expõe a sua vasta experiência profissional, indicando os vários cargos governamentais ocupados, inclusive o de Ministro de prestigiada área social. Nos termos do Código de Ética da Advocacia, assinale a afirmativa correta.\r\n\r\na) O anúncio está adequado aos termos do Código, pois indica os títulos acadêmicos e a experiência profissional.\r\nb) O anúncio está adequado aos termos do Código, por não conter adjetivações ou referências elogiosas ao profissional.\r\nc) O anúncio colide com as normas do Código, pois a referência a títulos acadêmicos é vedada por indicar a possibilidade de captação de clientela.\r\nd) O anúncio colide com as normas do Código, que proíbem a referência a cargos públicos capazes de gerar captação de clientela.\r\ne) N.D.A', '*', NULL, NULL, 8, NULL, NULL),
(48, 'Deise é uma próspera advogada e passou a buscar novos desafios, sendo eleita Deputada Estadual. Por força de suas raras habilidades políticas, foi eleita integrante da Mesa Diretora da Assembleia Legislativa do Estado Z. Ao ocupar esse honroso cargo procurou conciliar sua atividade parlamentar com o exercício da advocacia, sendo seu escritório agora administrado pela filha. Nos termos do Estatuto da Advocacia, assinale a afirmativa correta.\r\n\r\na) A atividade parlamentar de Deise é incompatível com o exercício da advocacia.\r\nb) A participação de Deise na Mesa Diretora a torna incompatível com o exercício da advocacia.\r\nc) A função de Deise como integrante da Mesa Diretora do Parlamento Estadual é conciliável com o exercício da advocacia.\r\nd) A atividade parlamentar de Deise na Mesa Diretora pode ser conciliada com o exercício da advocacia em prol dos necessitados.\r\ne) N.D.A', '*', NULL, NULL, 8, NULL, NULL),
(49, 'A advogada Maria foi presa em flagrante por furto cometido no interior de uma loja de departamentos. Na Delegacia, teve a assistência de advogado por ela constituído. O auto de prisão foi lavrado sem a presença de representante da Ordem dos Advogados do Brasil, fato que levou o advogado de Maria a arguir sua nulidade.Sobre a hipótese, assinale a afirmativa correta.\r\n\r\na) O auto de prisão em flagrante não é nulo, pois só é obrigatória a presença de representante da OAB quando a prisão decorre de motivo ligado ao exercício da advocacia.\r\nb) O auto de prisão em flagrante não é nulo, pois a presença de representante da OAB é facultativa em qualquer caso, podendo sempre ser suprida pela presença de advogado indicado pelo preso.\r\nc) O auto de prisão em flagrante é nulo, pois advogados não podem ser presos por crimes afiançáveis.\r\nd) O auto de prisão em flagrante é nulo, pois a presença de representante da OAB em caso de prisão em flagrante de advogado é sempre obrigatória.\r\ne) N.D.A', '*', NULL, NULL, 8, NULL, NULL),
(50, 'Hans Kelsen, ao abordar o tema da interpretação jurídica no seu livro Teoria Pura do Direito, fala em ato de vontade e ato de conhecimento. Em relação à aplicação do Direito por um órgão jurídico, assinale a afirmativa correta da interpretação.\r\n\r\na) Prevalece como ato de conhecimento, pois o Direito é atividade científica e, assim, capaz de prover precisão técnica no âmbito de sua aplicação por agentes competentes.\r\nb) Predomina como puro ato de conhecimento, em que o agente escolhe, conforme seu arbítrio, qualquer norma que entenda como válida e capaz de regular o caso concreto.\r\nc) A interpretação cognoscitiva combina-se a um ato de vontade em que o órgão aplicador efetua uma escolha entre as possibilidades reveladas por meio da mesma interpretação cognoscitiva.\r\nd) A interpretação gramatical prevalece como sendo a única capaz de revelar o conhecimento apropriado da mens legis.\r\ne) N.D.A', '*', NULL, NULL, 9, NULL, NULL),
(51, 'Determinado Estado da Federação vivencia sérios problemas de segurança pública, sendo frequentes as fugas dos presos transportados para participar de atos processuais realizados no âmbito do Poder Judiciário. Para remediar essa situação, foi editada uma lei estadual estabelecendo a possibilidade de utilização do sistema de videoconferência no âmbito do Estado. Diante de tal quadro, assinale a afirmativa que se ajusta à ordem constitucional.\r\n\r\na) A lei estadual é constitucional, pois a matéria se insere na competência local dos Estados-membros, versando sobre assunto de interesse local.\r\nb) A lei estadual é inconstitucional, pois afrontou a competência privativa da União de legislar sobre Direito Processual Penal.\r\nc) A lei estadual é constitucional, pois a matéria se insere no âmbito da competência delegada da União, versando sobre direito processual.\r\nd) A lei estadual é inconstitucional, pois comando normativo dessa natureza, por força do princípio da simetria, deveria estar previsto na Constituição Estadual. \r\ne) N.D.A', '*', NULL, NULL, 9, NULL, NULL),
(52, 'Dois advogados, com grande experiência profissional e com a justa preocupação de se manterem atualizados, concluem que algumas ideias vêm influenciando mais profundamente a percepção dos operadores do direito a respeito da ordem jurídica. Um deles lembra que a Constituição brasileira vem funcionando como verdadeiro “filtro”, de forma a influenciar todas as normas do ordenamento pátrio com os seus valores. O segundo, concordando, adiciona que o crescente reconhecimento da natureza normativo-jurídica dos princípios pelos tribunais, especialmente pelo Supremo Tribunal Federal, tem aproximado as concepções de direito e justiça (buscada no diálogo racional) e oferecido um papel de maior destaque aos magistrados. As posições apresentadas pelos advogados mantêm relação com uma concepção teórico-jurídica que, no Brasil e em outros países, vem sendo denominada de\r\n\r\na) neoconstitucionalismo\r\nb) positivismo-normativista.\r\nc) neopositivismo.\r\nd) jusnaturalismo.\r\ne) N.D.A', '*', NULL, NULL, 9, NULL, NULL),
(53, 'Ocorreu um grande escândalo de desvio de verbas públicas na administração pública federal, o que ensejou a instauração de uma Comissão Parlamentar de Inquérito (CPI), requerida pelos deputados federais de oposição. Surpreendentemente, os oponentes da CPI conseguem que o inexperiente deputado M seja alçado à condição de Presidente da Comissão. Por não possuir formação jurídica e desconhecer o trâmite das atividades parlamentares, o referido Presidente, sem consultar os assessores jurídicos da Casa, toma uma série de iniciativas, expedindo ofícios e requisitando informações a diversos órgãos. Posteriormente, veio à tona que apenas uma de suas providências prescindiria de efetivo mandado judicial. Assinale a opção que indica a única providência que o deputado M poderia ter tomado, prescindindo de ordem judicial.\r\n\r\na) Determinação de prisão preventiva de pessoas por condutas que, embora sem flagrância, configuram crime e há comprovado risco de que voltem a ser praticadas.\r\nb) Autorização, ao setor de inteligência da Polícia Judiciária, para que realize a interceptação das comunicações telefônicas (“escuta”) de prováveis envolvidos.\r\nc) Quebra de sigilo fiscal dos servidores públicos que, sem aparente motivo, apresentaram público e notório aumento do seu padrão de consumo.\r\nd) Busca e apreensão de documentos nas residências de sete pessoas supostamente envolvidas no esquema de desvio de verba.\r\ne) N.D.A', '*', NULL, NULL, 9, NULL, NULL),
(54, 'Pedro, reconhecido advogado na área do direito público, é contratado para produzir um parecer sobre situação que envolve o pacto federativo entre Estados brasileiros. Ao estudar mais detidamente a questão, conclui que, para atingir seu objetivo, é necessário analisar o alcance das chamadas cláusulas pétreas. Com base na ordem constitucional brasileira vigente, assinale, dentre as opções abaixo, a única que expressa uma premissa correta sobre o tema e que pode ser usada pelo referido advogado no desenvolvimento de seu parecer.\r\n\r\na) As cláusulas pétreas podem ser invocadas para sustentar a existência de normas constitucionais superiores em face de normas constitucionais inferiores, o que possibilita a existência de normas constitucionais inconstitucionais.\r\nb) Norma introduzida por emenda à constituição se integra plenamente ao texto constitucional, não podendo, portanto, ser submetida a controle de constitucionalidade, ainda que sob alegação de violação à cláusula pétrea.\r\nc) Mudanças propostas por constituinte derivado reformador estão sujeitas ao controle de constitucionalidade, sendo que as normas ali propostas não podem afrontar cláusulas pétreas estabelecidas na Constituição da República.\r\nd) Os direitos e as garantias individuais considerados como cláusulas pétreas estão localizados exclusivamente nos dispositivos do Art. 5º, de modo que é inconstitucional atribuir essa qualidade (cláusula pétrea) a normas fundadas em outros dispositivos constitucionais.\r\ne) N.D.A', '*', NULL, NULL, 9, NULL, NULL),
(55, 'Mário foi citado em processo de execução, em virtude do descumprimento de obrigação consubstanciada em nota promissória por ele emitida. Alegando excesso de execução, por ter efetuado o pagamento parcial da dívida, Mário opôs embargos à execução. Sobre esses embargos, assinale a afirmativa correta.\r\n\r\na) Constituem-se em ação autônoma, razão pela qual serão autuados e distribuídos livremente, em homenagem ao princípio do juiz natural.\r\nb) São cabíveis tanto nas execuções autônomas quanto no cumprimento de sentença.\r\nc) Em regra, suspendem a execução.\r\nd) Seu oferecimento independe de efetivação da penhora, depósito ou caução.\r\ne) N.D.A', '*', NULL, NULL, 10, NULL, NULL),
(56, 'Em ação de alimentos promovida por Yolanda em face de Aurélio, o Juiz determinou que Aurélio deveria arcar, na condição de futuro pai, com os valores devidos à gestante durante a gravidez, destinados a cobrir as despesas adicionais decorrentes da gestação, fixando para tal a quantia “x”. A legislação atinente ao tema dá a Aurélio a possibilidade de defesa. Assinale a opção que indica os termos em que a defesa será exercida.\r\n\r\na) Além dos alimentos gravídicos, o Juiz designará a data para a realização da audiência, que será considerada o termo a quo para o curso do prazo de cinco dias para a defesa do réu.\r\nb) O réu deverá ser informado da fixação dos alimentos gravídicos, de modo que o prazo de cinco dias será contado a partir da juntada do mandado de citação devidamente cumprido.\r\nc) O momento para apresentação da defesa do réu, nesse caso, será a audiência de instrução e julgamento, que terá a data determinada na decisão que fixa os alimentos provisórios.\r\nd) O prazo de 15 dias para o oferecimento de defesa terá início no dia da juntada do mandado que fixou e determinou o pagamento de alimentos gravídicos.\r\ne) N.D.A', '*', NULL, NULL, 10, NULL, NULL),
(57, 'Marcondes, necessitando de dinheiro para comparecer a uma festa no bairro em que residia, decide subtrair R$ 1.000,00 do caixa do açougue de propriedade de seu pai. Para isso, aproveita-se da ausência de seu genitor, que, naquele dia, comemorava seu aniversário de 63 anos, para arrombar a porta do estabelecimento e subtrair a quantia em espécie necessária. Analisando a situação fática, é correto afirmar que:\r\n\r\na) Marcondes não será condenado pela prática de crime, pois é isento de pena, em razão da escusa absolutória.\r\nb) Marcondes deverá responder pelo crime de furto de coisa comum, por ser herdeiro de seu pai.\r\nc) Marcondes deverá responder pelo crime de furto qualificado.\r\nd) Marcondes deverá responder pelos crimes de dano e furto simples em concurso formal.\r\ne) N.D.A', '*', NULL, NULL, 10, NULL, NULL),
(58, 'Reconhecida a prática de um injusto culpável, o juiz realiza o processo de individualização da pena, de acordo com o Art. 68 do Código Penal. Segundo a jurisprudência do Superior Tribunal de Justiça, assinale a afirmativa correta.\r\n\r\na) A condenação com trânsito em julgado por crime praticado em data posterior ao delito pelo qual o agente está sendo julgado pode funcionar como maus antecedentes.\r\nb) Não se mostra possível a compensação da agravante da reincidência com a atenuante da confissão espontânea.\r\nc) Nada impede que a pena intermediária, na segunda fase do critério trifásico, fique acomodada abaixo do mínimo legal.\r\nd) O aumento da pena na terceira fase no roubo circunstanciado exige fundamentação concreta, sendo insuficiente a simples menção ao número de majorantes.\r\ne) N.D.A', '*', NULL, NULL, 10, NULL, NULL),
(59, 'Paulo pretende adquirir um automóvel por meio de sistema de financiamento junto a uma instituição bancária. Para tanto, dirige-se ao estabelecimento comercial para verificar as condições de financiamento e é informado que, quanto maior a renda bruta familiar, maior a dilação do prazo para pagamento e menores os juros. Decide, então, fazer falsa declaração de parentesco ao preencher a ficha cadastral, a fim de aumentar a renda familiar informada, vindo, assim, a obter o financiamento nas condições pretendidas. Considerando a situação narrada e os crimes contra a fé pública, é correto afirmar que Paulo cometeu o delito de \r\n\r\na) falsificação material de documento público.\r\nb) falsidade ideológica.\r\nc) falsificação material de documento particular.\r\nd) falsa identidade. \r\ne) N.D.A', '*', NULL, NULL, 10, NULL, NULL);

-- --------------------------------------------------------

--
-- Estrutura da tabela `htc1`
--

CREATE TABLE IF NOT EXISTS `htc1` (
  `C1_COD` int(10) unsigned NOT NULL,
  `C1_NOME` varchar(40) NOT NULL,
  `D_E_L_E_T` varchar(1) DEFAULT NULL
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

--
-- Extraindo dados da tabela `htc1`
--

INSERT INTO `htc1` (`C1_COD`, `C1_NOME`, `D_E_L_E_T`) VALUES
(1, 'Ciência da Computação', NULL),
(2, 'Direito', NULL);

-- --------------------------------------------------------

--
-- Estrutura da tabela `htc2`
--

CREATE TABLE IF NOT EXISTS `htc2` (
  `C2_COD` int(10) unsigned NOT NULL,
  `C2_NOME` varchar(80) NOT NULL,
  `C2_CURSO` int(11) unsigned NOT NULL,
  `D_E_L_E_T` varchar(1) DEFAULT NULL
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

--
-- Extraindo dados da tabela `htc2`
--

INSERT INTO `htc2` (`C2_COD`, `C2_NOME`, `C2_CURSO`, `D_E_L_E_T`) VALUES
(1, 'Arquitetura de Computadores', 1, NULL),
(2, 'Fundamentos Matemáticos para Computação', 1, NULL),
(3, 'Processo Civil ', 2, NULL),
(4, 'Estátuto ', 2, NULL),
(5, 'Civil', 2, NULL);

-- --------------------------------------------------------

--
-- Estrutura da tabela `htc3`
--

CREATE TABLE IF NOT EXISTS `htc3` (
  `C3_COD` int(10) unsigned NOT NULL,
  `C3_NOME` varchar(120) NOT NULL,
  `C3_DISCIPL` int(10) unsigned NOT NULL,
  `D_E_L_E_T` varchar(1) DEFAULT NULL
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;

--
-- Extraindo dados da tabela `htc3`
--

INSERT INTO `htc3` (`C3_COD`, `C3_NOME`, `C3_DISCIPL`, `D_E_L_E_T`) VALUES
(1, 'Teoria dos Conjuntos', 2, NULL),
(2, 'Teoria dos Números ', 2, NULL),
(3, 'Análise Combinatória', 2, NULL),
(4, 'Arquitetura RISC', 1, NULL),
(5, 'Instruções MIPS', 1, NULL),
(6, 'Processadores: ISA', 1, NULL),
(7, 'Civil', 3, NULL),
(8, 'Ética', 4, NULL),
(9, 'Hermenêutica', 4, NULL),
(10, 'Processo Civil', 5, NULL);

-- --------------------------------------------------------

--
-- Estrutura da tabela `htd1`
--

CREATE TABLE IF NOT EXISTS `htd1` (
  `D1_COD` int(10) unsigned NOT NULL,
  `D1_TIPO` varchar(1) NOT NULL,
  `D1_INEDITA` varchar(1) DEFAULT NULL,
  `D1_QUESTAO` varchar(100) NOT NULL,
  `D1_MATERIA` varchar(50) NOT NULL,
  `D1_DATA` varchar(10) NOT NULL,
  `D_E_L_E_T` varchar(1) DEFAULT NULL
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `hta1`
--
ALTER TABLE `hta1`
  ADD PRIMARY KEY (`A1_COD`);

--
-- Indexes for table `htb1`
--
ALTER TABLE `htb1`
  ADD PRIMARY KEY (`B1_COD`),
  ADD KEY `B1_MATERIA` (`B1_MATERIA`);

--
-- Indexes for table `htc1`
--
ALTER TABLE `htc1`
  ADD PRIMARY KEY (`C1_COD`);

--
-- Indexes for table `htc2`
--
ALTER TABLE `htc2`
  ADD PRIMARY KEY (`C2_COD`),
  ADD KEY `C2_CURSO` (`C2_CURSO`);

--
-- Indexes for table `htc3`
--
ALTER TABLE `htc3`
  ADD PRIMARY KEY (`C3_COD`),
  ADD KEY `C3_DISCIPL` (`C3_DISCIPL`);

--
-- Indexes for table `htd1`
--
ALTER TABLE `htd1`
  ADD PRIMARY KEY (`D1_COD`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `hta1`
--
ALTER TABLE `hta1`
  MODIFY `A1_COD` int(10) unsigned NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=3;
--
-- AUTO_INCREMENT for table `htb1`
--
ALTER TABLE `htb1`
  MODIFY `B1_COD` int(10) unsigned NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=60;
--
-- AUTO_INCREMENT for table `htc1`
--
ALTER TABLE `htc1`
  MODIFY `C1_COD` int(10) unsigned NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=3;
--
-- AUTO_INCREMENT for table `htc2`
--
ALTER TABLE `htc2`
  MODIFY `C2_COD` int(10) unsigned NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=6;
--
-- AUTO_INCREMENT for table `htc3`
--
ALTER TABLE `htc3`
  MODIFY `C3_COD` int(10) unsigned NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=11;
--
-- AUTO_INCREMENT for table `htd1`
--
ALTER TABLE `htd1`
  MODIFY `D1_COD` int(10) unsigned NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=1;
--
-- Constraints for dumped tables
--

--
-- Limitadores para a tabela `htb1`
--
ALTER TABLE `htb1`
  ADD CONSTRAINT `FK_C3_COD` FOREIGN KEY (`B1_MATERIA`) REFERENCES `htc3` (`C3_COD`) ON UPDATE CASCADE;

--
-- Limitadores para a tabela `htc2`
--
ALTER TABLE `htc2`
  ADD CONSTRAINT `FK_C1_COD` FOREIGN KEY (`C2_CURSO`) REFERENCES `htc1` (`C1_COD`) ON UPDATE CASCADE;

--
-- Limitadores para a tabela `htc3`
--
ALTER TABLE `htc3`
  ADD CONSTRAINT `FK_C2_COD` FOREIGN KEY (`C3_DISCIPL`) REFERENCES `htc2` (`C2_COD`) ON UPDATE CASCADE;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

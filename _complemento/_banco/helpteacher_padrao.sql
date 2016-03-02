-- phpMyAdmin SQL Dump
-- version 4.5.1
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: 24-Nov-2015 às 08:23
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
  `A1_COD` int(10) UNSIGNED NOT NULL,
  `A1_LOGIN` varchar(50) NOT NULL,
  `A1_PWD` varchar(32) NOT NULL,
  `A1_ALTPWD` varchar(1) DEFAULT NULL,
  `A1_STOPBD` varchar(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

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
  `B1_COD` int(10) UNSIGNED NOT NULL,
  `B1_QUEST` text NOT NULL,
  `B1_OBJETIV` varchar(1) DEFAULT NULL,
  `B1_ARQUIVO` varchar(520) DEFAULT NULL,
  `B1_USADA` varchar(1) DEFAULT NULL,
  `B1_MATERIA` int(10) UNSIGNED NOT NULL,
  `B1_PADRAO` varchar(1) DEFAULT NULL,
  `D_E_L_E_T` varchar(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estrutura da tabela `htc1`
--

CREATE TABLE IF NOT EXISTS `htc1` (
  `C1_COD` int(10) UNSIGNED NOT NULL,
  `C1_NOME` varchar(40) NOT NULL,
  `D_E_L_E_T` varchar(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estrutura da tabela `htc2`
--

CREATE TABLE IF NOT EXISTS `htc2` (
  `C2_COD` int(10) UNSIGNED NOT NULL,
  `C2_NOME` varchar(80) NOT NULL,
  `C2_CURSO` int(11) UNSIGNED NOT NULL,
  `D_E_L_E_T` varchar(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estrutura da tabela `htc3`
--

CREATE TABLE IF NOT EXISTS `htc3` (
  `C3_COD` int(10) UNSIGNED NOT NULL,
  `C3_NOME` varchar(120) NOT NULL,
  `C3_DISCIPL` int(10) UNSIGNED NOT NULL,
  `D_E_L_E_T` varchar(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estrutura da tabela `htd1`
--

CREATE TABLE IF NOT EXISTS `htd1` (
  `D1_COD` int(10) UNSIGNED NOT NULL,
  `D1_TIPO` varchar(1) NOT NULL,
  `D1_INEDITA` varchar(1) DEFAULT NULL,
  `D1_QUESTAO` varchar(100) NOT NULL,
  `D1_MATERIA` varchar(50) NOT NULL,
  `D1_DATA` varchar(10) NOT NULL,
  `D_E_L_E_T` varchar(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

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
  MODIFY `A1_COD` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
--
-- AUTO_INCREMENT for table `htb1`
--
ALTER TABLE `htb1`
  MODIFY `B1_COD` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `htc1`
--
ALTER TABLE `htc1`
  MODIFY `C1_COD` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `htc2`
--
ALTER TABLE `htc2`
  MODIFY `C2_COD` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `htc3`
--
ALTER TABLE `htc3`
  MODIFY `C3_COD` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `htd1`
--
ALTER TABLE `htd1`
  MODIFY `D1_COD` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;
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

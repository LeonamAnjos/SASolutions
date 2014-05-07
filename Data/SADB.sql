# SQL Manager 2010 Lite for MySQL 4.6.0.5
# ---------------------------------------
# Host     : localhost
# Port     : 3306
# Database : SADB


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES latin1 */;

SET FOREIGN_KEY_CHECKS=0;

CREATE DATABASE `SADB`
    CHARACTER SET 'latin1'
    COLLATE 'latin1_swedish_ci';

USE `sadb`;

#
# Structure for the `end_pais` table : 
#

CREATE TABLE `end_pais` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Nome` varchar(30) NOT NULL,
  `Sigla` varchar(2) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `Sigla` (`Sigla`)
) ENGINE=InnoDB AUTO_INCREMENT=28 DEFAULT CHARSET=latin1;

#
# Structure for the `end_estado` table : 
#

CREATE TABLE `end_estado` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Nome` varchar(30) NOT NULL,
  `Sigla` varchar(2) NOT NULL,
  `PaisID` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `Sigla` (`Sigla`),
  KEY `PaisID` (`PaisID`),
  CONSTRAINT `end_estado_fk_01` FOREIGN KEY (`PaisID`) REFERENCES `end_pais` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=latin1;

#
# Structure for the `end_cidade` table : 
#

CREATE TABLE `end_cidade` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Nome` varchar(40) NOT NULL,
  `DDD` varchar(2) DEFAULT NULL,
  `EstadoID` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `EstadoID` (`EstadoID`),
  CONSTRAINT `end_cidade_fk_01` FOREIGN KEY (`EstadoID`) REFERENCES `end_estado` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=latin1;

#
# Structure for the `end_cep` table : 
#

CREATE TABLE `end_cep` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `CEP` varchar(8) NOT NULL,
  `Logradouro` varchar(60) NOT NULL,
  `Bairro` varchar(40) NOT NULL,
  `CidadeID` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `CEP` (`CEP`),
  KEY `CidadeID` (`CidadeID`),
  CONSTRAINT `end_cep_fk_01` FOREIGN KEY (`CidadeID`) REFERENCES `end_cidade` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

#
# Structure for the `est_fabricante` table : 
#

CREATE TABLE `est_fabricante` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Nome` varchar(40) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

#
# Structure for the `est_grupo` table : 
#

CREATE TABLE `est_grupo` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Descricao` varchar(40) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

#
# Structure for the `est_vendedor` table : 
#

CREATE TABLE `est_vendedor` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Nome` varchar(40) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

#
# Structure for the `fin_cadastro` table : 
#

CREATE TABLE `fin_cadastro` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Tipo` varchar(1) DEFAULT NULL,
  `Nome` varchar(60) NOT NULL,
  `CPF` varchar(14) DEFAULT NULL,
  `RG` varchar(8) DEFAULT NULL,
  `RazaoSocial` varchar(60) DEFAULT NULL,
  `Contato` varchar(50) DEFAULT NULL,
  `EMail` varchar(60) DEFAULT NULL,
  `Telefone` varchar(10) DEFAULT NULL,
  `Celular` varchar(10) DEFAULT NULL,
  `Fax` varchar(10) DEFAULT NULL,
  `DataNascimento` date DEFAULT NULL,
  `EndCorrespCepID` int(11) DEFAULT NULL,
  `EndCorrespNumero` int(11) DEFAULT NULL,
  `EndCorrespComplemento` varchar(40) DEFAULT NULL,
  `DataInclusao` datetime DEFAULT NULL,
  `DataAlteracao` datetime DEFAULT NULL,
  `Situacao` char(1) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `EndCorrespCepID` (`EndCorrespCepID`),
  KEY `Tipo` (`Tipo`,`Nome`),
  CONSTRAINT `fin_cadastro_fk_01` FOREIGN KEY (`EndCorrespCepID`) REFERENCES `end_cep` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=latin1;

#
# Structure for the `est_pedido` table : 
#

CREATE TABLE `est_pedido` (
  `ID` int(11) NOT NULL,
  `Tipo` char(1) NOT NULL,
  `Data` date NOT NULL,
  `CadastroID` int(11) NOT NULL,
  `Hora` time DEFAULT NULL,
  `VendedorID` int(11) DEFAULT NULL,
  `DataValidade` date DEFAULT NULL,
  `Fase` char(1) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `Data` (`Data`),
  KEY `VendedorID` (`VendedorID`),
  KEY `CadastroID` (`CadastroID`),
  CONSTRAINT `est_pedido_fk_01` FOREIGN KEY (`CadastroID`) REFERENCES `fin_cadastro` (`ID`),
  CONSTRAINT `est_pedido_fk_02` FOREIGN KEY (`VendedorID`) REFERENCES `est_vendedor` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

#
# Structure for the `est_subgrupo` table : 
#

CREATE TABLE `est_subgrupo` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Descricao` varchar(40) DEFAULT NULL,
  `GrupoID` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `GrupoID` (`GrupoID`),
  CONSTRAINT `est_subgrupo_fk_01` FOREIGN KEY (`GrupoID`) REFERENCES `est_grupo` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

#
# Structure for the `est_unidade` table : 
#

CREATE TABLE `est_unidade` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Descricao` varchar(40) DEFAULT NULL,
  `Simbolo` varchar(5) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

#
# Structure for the `est_produto` table : 
#

CREATE TABLE `est_produto` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Referencia` varchar(30) DEFAULT NULL,
  `Nome` varchar(100) DEFAULT NULL,
  `UnidadeID` int(11) DEFAULT NULL,
  `FabricanteID` int(11) DEFAULT NULL,
  `GrupoID` int(11) DEFAULT NULL,
  `SubGrupoID` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `GrupoID` (`GrupoID`),
  KEY `SubGrupoID` (`SubGrupoID`),
  KEY `UnidadeID` (`UnidadeID`),
  KEY `FabricanteID` (`FabricanteID`),
  CONSTRAINT `est_produto_fk_01` FOREIGN KEY (`GrupoID`) REFERENCES `est_grupo` (`ID`),
  CONSTRAINT `est_produto_fk_02` FOREIGN KEY (`FabricanteID`) REFERENCES `est_fabricante` (`ID`),
  CONSTRAINT `est_produto_fk_03` FOREIGN KEY (`UnidadeID`) REFERENCES `est_unidade` (`ID`),
  CONSTRAINT `est_produto_fk_04` FOREIGN KEY (`SubGrupoID`) REFERENCES `est_subgrupo` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

#
# Structure for the `est_pedido_item` table : 
#

CREATE TABLE `est_pedido_item` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Pedido` int(11) NOT NULL,
  `Produto` int(11) NOT NULL,
  `Quantidade` float(9,3) NOT NULL,
  `ValorUnitario` double(12,2) NOT NULL,
  `Valor` double(12,2) NOT NULL,
  `ValorDesconto` double(12,2) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `Pedido` (`Pedido`),
  KEY `Produto` (`Produto`),
  CONSTRAINT `est_pedido_item_fk_01` FOREIGN KEY (`Pedido`) REFERENCES `est_pedido` (`Id`),
  CONSTRAINT `est_pedido_item_fk_02` FOREIGN KEY (`Produto`) REFERENCES `est_produto` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

#
# Structure for the `fin_contafinanceira` table : 
#

CREATE TABLE `fin_contafinanceira` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Tipo` varchar(1) DEFAULT NULL,
  `CadastroID` int(11) DEFAULT NULL,
  `EndCobrancaCepID` int(11) DEFAULT NULL,
  `EndCobrancaNumero` int(11) DEFAULT NULL,
  `EndCobrancaComplemento` varchar(40) DEFAULT NULL,
  `Situacao` char(1) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `CadastroID` (`CadastroID`),
  KEY `EndCobrancaCepID` (`EndCobrancaCepID`),
  CONSTRAINT `fin_contafinanceira_fk` FOREIGN KEY (`CadastroID`) REFERENCES `fin_cadastro` (`ID`),
  CONSTRAINT `fin_contafinanceira_fk1` FOREIGN KEY (`EndCobrancaCepID`) REFERENCES `end_cep` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;

#
# Structure for the `product` table : 
#

CREATE TABLE `product` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) DEFAULT NULL,
  `Category` varchar(255) DEFAULT NULL,
  `Discontinued` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=35 DEFAULT CHARSET=latin1;

#
# Structure for the `sis_empresa` table : 
#

CREATE TABLE `sis_empresa` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Nome` varchar(40) NOT NULL,
  `CNPJ` varchar(14) DEFAULT NULL,
  `InscricaoEstadual` varchar(20) DEFAULT NULL,
  `CepID` int(11) DEFAULT NULL,
  `Numero` int(11) DEFAULT NULL,
  `Complemento` varchar(30) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `CepID` (`CepID`),
  CONSTRAINT `sis_empresa_fk_01` FOREIGN KEY (`CepID`) REFERENCES `end_cep` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=latin1;

#
# Structure for the `sis_usuario_grupo` table : 
#

CREATE TABLE `sis_usuario_grupo` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Descricao` varchar(30) NOT NULL,
  `Tipo` char(1) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

#
# Structure for the `sis_usuario` table : 
#

CREATE TABLE `sis_usuario` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Login` varchar(20) NOT NULL,
  `Senha` varchar(20) DEFAULT NULL,
  `Nome` varchar(60) DEFAULT NULL,
  `Email` varchar(100) DEFAULT NULL,
  `Situacao` char(1) DEFAULT NULL,
  `GrupoID` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `Login` (`Login`),
  KEY `GrupoID` (`GrupoID`),
  CONSTRAINT `sis_usuario_fk_01` FOREIGN KEY (`GrupoID`) REFERENCES `sis_usuario_grupo` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=24 DEFAULT CHARSET=latin1;

#
# Data for the `end_pais` table  (LIMIT 0,500)
#

INSERT INTO `end_pais` (`ID`, `Nome`, `Sigla`) VALUES 
  (1,'Brasil','Br'),
  (2,'Argentina','Ar'),
  (21,'Novo','NV'),
  (25,'Pais05','05'),
  (26,'Pais04','04'),
  (27,'Pais06','06');
COMMIT;

#
# Data for the `end_estado` table  (LIMIT 0,500)
#

INSERT INTO `end_estado` (`ID`, `Nome`, `Sigla`, `PaisID`) VALUES 
  (1,'Paraná','PR',1),
  (3,'Mato Grosso do Sul','MS',1),
  (5,'Estadolandia','TS',21),
  (9,'Estado01','01',1);
COMMIT;

#
# Data for the `end_cidade` table  (LIMIT 0,500)
#

INSERT INTO `end_cidade` (`ID`, `Nome`, `DDD`, `EstadoID`) VALUES 
  (1,'Maringá','44',1),
  (2,'Sarandi','44',1),
  (5,'Curitiba','41',1),
  (7,'Umuarama','44',1),
  (11,'Cidade01','00',1),
  (12,'Cidade01','00',1),
  (13,'Cidade01','00',1),
  (17,'Cidade01','00',1),
  (18,'Cidade01','00',1);
COMMIT;

#
# Data for the `end_cep` table  (LIMIT 0,500)
#

INSERT INTO `end_cep` (`ID`, `CEP`, `Logradouro`, `Bairro`, `CidadeID`) VALUES 
  (1,'87020080','Rua Campo Sales','Zona 7',1),
  (2,'87005250','Rua Adolfo Alves Ferreira','Jardim Novo Horizonte',1),
  (3,'00000001','Logradouro01','Bairro01',1);
COMMIT;

#
# Data for the `est_fabricante` table  (LIMIT 0,500)
#

INSERT INTO `est_fabricante` (`ID`, `Nome`) VALUES 
  (1,'SA Solutions');
COMMIT;

#
# Data for the `est_grupo` table  (LIMIT 0,500)
#

INSERT INTO `est_grupo` (`ID`, `Descricao`) VALUES 
  (1,'Eletrônicos'),
  (2,'Livros'),
  (3,'Esporte e Lazer');
COMMIT;

#
# Data for the `fin_cadastro` table  (LIMIT 0,500)
#

INSERT INTO `fin_cadastro` (`ID`, `Tipo`, `Nome`, `CPF`, `RG`, `RazaoSocial`, `Contato`, `EMail`, `Telefone`, `Celular`, `Fax`, `DataNascimento`, `EndCorrespCepID`, `EndCorrespNumero`, `EndCorrespComplemento`, `DataInclusao`, `DataAlteracao`, `Situacao`) VALUES 
  (1,'F','Rosana','12345678912',NULL,NULL,NULL,NULL,'4499333366','4499663344','4499663355','0001-01-01',1,0,NULL,'2010-12-14 00:00:00','2010-12-18 00:00:00','A'),
  (4,'F','Leonam Gustavo','00540938998','70068079',NULL,'Gustavo',NULL,'4433998855',NULL,NULL,'0001-01-01',NULL,0,NULL,'2010-12-14 00:00:00','2010-12-18 00:00:00','A'),
  (12,'F','Nome01','12345678901','70010010',NULL,NULL,'a@b.c','4499778877','4499778855','4477884488','1970-01-29',1,0,NULL,'2011-01-07 00:00:00','2011-01-07 00:00:00','A'),
  (13,'J','Nome02','55555678903333','ISENTO',NULL,'Fulano','a@b.c','4499778877','4499778855','4477884488','1970-01-29',1,0,NULL,'2011-01-07 00:00:00','2011-01-07 00:00:00','A'),
  (14,'F','Nome01','12345678901','70010010',NULL,NULL,'a@b.c','4499778877','4499778855','4477884488','1970-01-29',1,0,NULL,'2011-01-07 00:00:00','2011-01-07 00:00:00','A'),
  (15,'J','Nome02','55555678903333','ISENTO',NULL,'Fulano','a@b.c','4499778877','4499778855','4477884488','1970-01-29',1,0,NULL,'2011-01-07 00:00:00','2011-01-07 00:00:00','A');
COMMIT;

#
# Data for the `est_unidade` table  (LIMIT 0,500)
#

INSERT INTO `est_unidade` (`ID`, `Descricao`, `Simbolo`) VALUES 
  (1,'Kilograma','Kg'),
  (2,'Metro','Mt'),
  (3,'Unidade','Un');
COMMIT;

#
# Data for the `est_subgrupo` table  (LIMIT 0,500)
#

INSERT INTO `est_subgrupo` (`ID`, `Descricao`, `GrupoID`) VALUES 
  (1,'Audio e Video',1),
  (2,'Câmeras digitais e Filmadoras',1);
COMMIT;

#
# Data for the `est_produto` table  (LIMIT 0,500)
#

INSERT INTO `est_produto` (`ID`, `Referencia`, `Nome`, `UnidadeID`, `FabricanteID`, `GrupoID`, `SubGrupoID`) VALUES 
  (1,'1122334455','Camera Sony Muito Boa',3,1,1,2);
COMMIT;

#
# Data for the `fin_contafinanceira` table  (LIMIT 0,500)
#

INSERT INTO `fin_contafinanceira` (`ID`, `Tipo`, `CadastroID`, `EndCobrancaCepID`, `EndCobrancaNumero`, `EndCobrancaComplemento`, `Situacao`) VALUES 
  (1,'F',1,NULL,0,NULL,'A'),
  (2,'B',1,NULL,0,NULL,'A'),
  (3,'C',1,2,1112,NULL,'A'),
  (5,'C',4,1,0,NULL,'A'),
  (6,'F',4,NULL,0,NULL,'I');
COMMIT;

#
# Data for the `product` table  (LIMIT 0,500)
#

INSERT INTO `product` (`Id`, `Name`, `Category`, `Discontinued`) VALUES 
  (34,'Apple','Fruits',0);
COMMIT;

#
# Data for the `sis_empresa` table  (LIMIT 0,500)
#

INSERT INTO `sis_empresa` (`ID`, `Nome`, `CNPJ`, `InscricaoEstadual`, `CepID`, `Numero`, `Complemento`) VALUES 
  (5,'SA Solutions','01234567890123','Isento',2,123,'Zona 7');
COMMIT;

#
# Data for the `sis_usuario_grupo` table  (LIMIT 0,500)
#

INSERT INTO `sis_usuario_grupo` (`ID`, `Descricao`, `Tipo`) VALUES 
  (1,'Administradores','A'),
  (2,'Balconistas','E'),
  (3,'Consumidores','V');
COMMIT;

#
# Data for the `sis_usuario` table  (LIMIT 0,500)
#

INSERT INTO `sis_usuario` (`ID`, `Login`, `Senha`, `Nome`, `Email`, `Situacao`, `GrupoID`) VALUES 
  (6,'leonam.gustavo','A','Leonam Gustavo Santos Anjos','leonam.gustavo@sasolucoes.com.br','A',2),
  (21,'leonam.anjos','jUca10','Leonam Ricardo Santos Anjos','leonam.anjos@sasolucoes.com.br','A',1),
  (22,'luiz.guilherme','A','Luiz Guilherme Santos Anjos','luiz.guilherme@sasolucoes.com.br','A',1),
  (23,'rosana.souza','rosa','Rosana de Souza Rocha','rosana.souza@sasolucoes.com.br','A',3);
COMMIT;



/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
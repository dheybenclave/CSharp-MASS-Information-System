CREATE DATABASE  IF NOT EXISTS `dbms_mass` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `dbms_mass`;
-- MySQL dump 10.13  Distrib 5.6.17, for Win64 (x86_64)
--
-- Host: localhost    Database: dbms_mass
-- ------------------------------------------------------
-- Server version	5.6.21-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `attendance`
--

DROP TABLE IF EXISTS `attendance`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `attendance` (
  `ad_id` int(11) NOT NULL AUTO_INCREMENT,
  `pd_id` int(11) NOT NULL,
  `ad_date` date NOT NULL,
  `last_update` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`ad_id`),
  KEY `pd_id_idx` (`pd_id`),
  CONSTRAINT `pd_id` FOREIGN KEY (`pd_id`) REFERENCES `p_details` (`pd_id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=55 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `attendance`
--

LOCK TABLES `attendance` WRITE;
/*!40000 ALTER TABLE `attendance` DISABLE KEYS */;
INSERT INTO `attendance` VALUES (1,9,'2017-03-12','2017-03-11 16:41:44'),(2,10,'2017-03-12','2017-03-11 16:41:44'),(8,7,'2017-03-13','2017-03-11 16:46:46'),(11,4,'2017-03-13','2017-03-11 16:46:46'),(12,5,'2017-03-13','2017-03-11 16:46:46'),(15,11,'2017-03-13','2017-03-11 16:46:46'),(20,9,'2017-03-13','2017-03-13 15:32:55'),(22,9,'2017-03-19','2017-03-14 08:19:13'),(23,10,'2017-03-19','2017-03-14 08:19:14'),(24,13,'2017-03-19','2017-03-14 08:19:14'),(25,13,'2017-03-12','2017-03-17 09:27:18'),(28,4,'2017-03-31','2017-03-17 09:29:46'),(29,5,'2017-03-31','2017-03-17 09:29:46'),(32,11,'2017-03-31','2017-03-17 09:29:46'),(33,9,'2017-03-29','2017-03-20 17:43:46'),(35,13,'2017-03-29','2017-03-20 17:43:47'),(39,18,'2017-03-29','2017-03-20 17:43:47'),(41,4,'2017-03-29','2017-03-20 17:43:47'),(42,5,'2017-03-29','2017-03-20 17:43:47'),(45,11,'2017-03-29','2017-03-20 17:43:47'),(46,11,'2017-03-29','2017-03-24 00:51:06'),(47,20,'2017-03-12','2017-03-25 00:35:48'),(48,10,'2017-03-29','2017-03-31 16:35:42'),(49,9,'2017-04-01','2017-04-01 06:05:17'),(50,10,'2017-04-01','2017-04-01 06:05:17'),(52,21,'2017-04-01','2017-04-01 06:05:17'),(53,19,'2017-04-01','2017-04-01 06:05:17'),(54,20,'2017-04-01','2017-04-01 06:05:17');
/*!40000 ALTER TABLE `attendance` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `e_details`
--

DROP TABLE IF EXISTS `e_details`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `e_details` (
  `ed_id` int(11) NOT NULL AUTO_INCREMENT,
  `ed_primary_school` varchar(255) DEFAULT NULL,
  `ed_primary_year` varchar(255) DEFAULT NULL,
  `ed_secondary_school` varchar(255) DEFAULT NULL,
  `ed_secondary_year` varchar(255) DEFAULT NULL,
  `ed_tertiary_school` varchar(255) DEFAULT NULL,
  `ed_tertiary_year` varchar(255) DEFAULT NULL,
  `ed_tertiary_course` varchar(255) DEFAULT NULL,
  `ed_vocational_school` varchar(255) DEFAULT NULL,
  `ed_vocational_year` varchar(255) DEFAULT NULL,
  `ed_vocational_course` varchar(255) DEFAULT NULL,
  `ed_honors` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`ed_id`)
) ENGINE=InnoDB AUTO_INCREMENT=22 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `e_details`
--

LOCK TABLES `e_details` WRITE;
/*!40000 ALTER TABLE `e_details` DISABLE KEYS */;
INSERT INTO `e_details` VALUES (4,'Navotas Elementary School ','2011','Navotas National High School ','2017','Navotas Polytechnic College ','','Bacherlor Of Science In Computer Science ','None ','','None ','top 10 2016|'),(5,'Bagumbayan Elementary School ','2011','University High School ','2017','Navotas Polytechnic College ','','Bachelor Of Science In Engineering ','None ','','None ',''),(7,'St Nicholas Institute Of Addicts ','2011','La Institusyon Ala Droga ','2017','Philippine Drug Enforcement Agency ','','Bs Drug Addiction Major In Cocaine ','Certificate Of Opium Merchandise ','','Bs Opium Industry ','Never Tokhang|Palos|El Bato|'),(9,'Bagumbayan Elementary School ','2011','Navotas Elementary High School ','2017','Navotas Polytechnic College ','','Bachelor Of Science In Computer Science ','None ','','None ',''),(10,'Navotas Elementary School ','2011','Navotas National High School ','2017','Navotas Polytechnic College ','','Bachelor Of Science In Business Administration ','None ','','None ',''),(11,'Tondo Elementary School ','2010','Tondonational High School ','2014','Navotas Polytechnic College ','','Bachelor Of Science In Computer Science ','None ','','None ',''),(13,'Navotas Elementary School ','2011','Navotas National High School ','2017','None ','','None ','None ','','None ',''),(18,'Navotas Elementary School ','2011','Governor Andres Pascual College ','2017','None ','','None ','None ','','None ','mr.pogi 2016|'),(19,'','2010','','2014','','','','','','',''),(20,'Navotas Elementary School ','2011','Navota National High School ','2017','Navotas Polytechnic College ','','Bachelor Of Science In Computer Science ','None ','','None ','top top since high school|'),(21,'Navotas Leme ','2010','','2014','','','','','','','');
/*!40000 ALTER TABLE `e_details` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `m_details`
--

DROP TABLE IF EXISTS `m_details`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `m_details` (
  `md_id` int(11) NOT NULL AUTO_INCREMENT,
  `md_rank` varchar(255) DEFAULT NULL,
  `md_position` varchar(255) DEFAULT NULL,
  `md_dateofinvest` varchar(255) DEFAULT NULL,
  `md_dateofpromote` varchar(255) DEFAULT NULL,
  `md_otherministries` varchar(255) DEFAULT NULL,
  `md_awards` varchar(255) DEFAULT NULL,
  `md_violations` varchar(255) DEFAULT NULL,
  `md_sacraments` varchar(255) DEFAULT NULL,
  `md_status` varchar(255) DEFAULT NULL,
  `md_retrieve` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`md_id`)
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `m_details`
--

LOCK TABLES `m_details` WRITE;
/*!40000 ALTER TABLE `m_details` DISABLE KEYS */;
INSERT INTO `m_details` VALUES (4,'Elder','Member','June 12, 1998','May 06, 2013','choir (member)|','none|','none|','baptism|','Active','YES'),(5,'Junior','Member','June 13, 2013','June 22, 2015','','','','','Active','NO'),(7,'Alumni','Member','May 11, 2009','May 05, 2014','','','','','Active','YES'),(9,'Aspirant','Member','December 22, 2016','December 22, 2016','','','','','Active','NO'),(10,'Alumni','Member','May 16, 2006','June 22, 2007','choir ( member )|legion of mary ( member )|','','','baptism|','Leave','NO'),(11,'Senior','Member','May 10, 2009','May 04, 2015','','','','baptism|','Active','NO'),(13,'Senior','Member','May 10, 2009','May 04, 2015','choir ( member )|','','','baptism|','Active','NO'),(18,'Alumni','Member','May 01, 2011','May 02, 2016','','','','','Active','NO'),(19,'Aspirant','Member','December 22, 2016','December 22, 2016','','','','','Active','YES'),(20,'Elder','Member','May 09, 2010','May 10, 2015','none|','','','','Active','YES'),(21,'Alumni','Member','December 22, 2016','December 22, 2016','','','','','Active','NO'),(22,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'NO');
/*!40000 ALTER TABLE `m_details` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `notes`
--

DROP TABLE IF EXISTS `notes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `notes` (
  `notes_id` int(11) NOT NULL AUTO_INCREMENT,
  `notes_date` date DEFAULT NULL,
  `notes_note` text,
  PRIMARY KEY (`notes_id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `notes`
--

LOCK TABLES `notes` WRITE;
/*!40000 ALTER TABLE `notes` DISABLE KEYS */;
INSERT INTO `notes` VALUES (1,'2017-04-01','sdadsa'),(2,'2017-04-01',' sdadsa\r\nadasdsadsa'),(3,'2017-04-19',' sdadsa\r\nadasdsadsaasddasda');
/*!40000 ALTER TABLE `notes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `p_details`
--

DROP TABLE IF EXISTS `p_details`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `p_details` (
  `pd_id` int(11) NOT NULL AUTO_INCREMENT,
  `pd_fullname` varchar(255) NOT NULL,
  `pd_address` varchar(255) NOT NULL,
  `pd_birthday` varchar(255) NOT NULL,
  `pd_birthplace` varchar(255) DEFAULT NULL,
  `pd_age` bigint(11) NOT NULL,
  `pd_email` varchar(255) DEFAULT NULL,
  `pd_contactnumber` varchar(255) DEFAULT NULL,
  `pd_batch` varchar(255) DEFAULT NULL,
  `pd_photo` varchar(255) DEFAULT NULL,
  `pd_mothername` varchar(255) DEFAULT NULL,
  `pd_motheroccupation` varchar(255) DEFAULT NULL,
  `pd_fathername` varchar(255) DEFAULT NULL,
  `pd_fatheroccupation` varchar(255) DEFAULT NULL,
  `pd_contactnumbermother` varchar(255) DEFAULT NULL,
  `pd_contactnumberfather` varchar(255) DEFAULT NULL,
  `pd_lastname` varchar(255) NOT NULL,
  PRIMARY KEY (`pd_id`)
) ENGINE=InnoDB AUTO_INCREMENT=22 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `p_details`
--

LOCK TABLES `p_details` WRITE;
/*!40000 ALTER TABLE `p_details` DISABLE KEYS */;
INSERT INTO `p_details` VALUES (4,'Shara_ D. Natalio','Tuazon Navotas City ','March 04, 1998','Navotas ',19,'sharanatalio@yahoo.com','09432414209','St.john  - 2009 ','4.JPG','Sharline Natalio ','House Wife ','Ramon Natalio ','None ','094623153223','094522353223','Natalio'),(5,'Ralph_ Q. Oliveros','Daanghari,navotas City ','June 22, 1997','Manila ',20,'ralpholiveros@yahoo.com.ph','09876543212','St.paul  - 2010 ','4.JPG','Juanita Oliveros ','Web Designer ','Juanito Oliveros ','Programmer ','09456452345','09675673424','Oliveros'),(7,'Don_Nicholas_ C. El','New Babylonia ','August 17, 1998','Egypt ',19,'dnicholas@yahoo.com','911','St.paul  - 2010 ','7.JPG','Donya Margarita Esperanza El Zamora ','Barker ','Emperador Reynaldo El Zamora ','Bar Tender ','117','8700','El'),(9,'Noel_ A. Atazar','Caloocan Navotas City ','December 22, 1994','Manila ',23,'noelatazar@yahoo.com.ph','09456786435','St.john  - 2010 ','9.JPG','Jenna ','System Analysis ','John ','Developer ','09345645345','09345657565','Atazar'),(10,'Diane_ L. Bensurto','Navotas City ','July 07, 1994','Navotas ',22,'dianebensurto@yahoo.com','09231234123','St.teresita  - 2010 ','10.JPG','Vima Bensurto ','None ','Decease ','None ','09231231231','None','Bensurto'),(11,'Ivy_Rose_ R. Ruiz','Tondo Manila ','September 01, 1998','Tondo ',19,'ivyroseruizkpop@yahoo.com','093512312341','St.terista  - 2009 ','2.jpg','Decease ','None ','Decease ','None ','None','None','Ruiz'),(13,'Mark_John_ L. Bensurto','Daanghari Navotas City ','July 13, 1998','Navotas ',19,'mmbensurto@yahoo.com','none','St.paul  - 2009 ','10.JPG','Mhelody Bensurto ','House Wife ','Mario Bensurto ','Dress Maker ','None','None','Bensurto'),(18,'Brian_ K. Herrera','Daanghari Navotas City ','November 30, 2000','Navotas ',17,'brianherrera@yahoo.com','09123123123','St. Dominic  - 2009 ','19.JPG','Lhyn King Herrera ','Asst Manager ','Nawie Herrera ','Engineer ','','','Herrera'),(19,'Bernard_ H. Lao','Catmon Malabon City ','September 05, 1994','Malabon\'s City ',23,'bernardlao@yahoo.com','','St.tarsicio  - 2010 ','19.JPG','Bernarda Lao ','','Bernardo Lao ','','','','Lao'),(20,'Jax_Xander_ B. Lorenzo','Navotas City ','November 27, 1997','Navotas ',19,'lorenzo@yahoo.com','12345678908','2009  - St.paul ','20.JPG','Zandra ','None ','Jack ','Programmer ','','','Lorenzo'),(21,'Shintaro_ I. Francisco','Navotas ','November 22, 2000','Naovtas ',16,'shin@yahoo.com','234567890-9','St. Dominic  - 2011 ','10.JPG','Atashinsi ','None ','Taro ','None ','','','Francisco');
/*!40000 ALTER TABLE `p_details` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user` (
  `user_id` int(11) NOT NULL AUTO_INCREMENT,
  `user_name` varchar(255) DEFAULT NULL,
  `user_password` varchar(45) DEFAULT NULL,
  `user_position` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`user_id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (1,'dheo','claveria','Admin'),(8,'wal','wal','Secretary'),(9,'jorem','jorem','Coordinator');
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'dbms_mass'
--
/*!50003 DROP PROCEDURE IF EXISTS `CreateUser` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `CreateUser`(
																user_name VARCHAR(255),
                                                                user_password VARCHAR(255),
                                                                user_position VARCHAR(255)

																)
BEGIN
INSERT INTO `dbms_mass`.`user` (`user_name`, `user_password`, `user_position`) 
VALUES (user_name, user_password, user_position);

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `DeleteAll` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `DeleteAll`(pid INT(11))
BEGIN

DELETE FROM `dbms_mass`.`p_details` WHERE `pd_id`= pid;
DELETE FROM `dbms_mass`.`e_details` WHERE `ed_id`= pid;
DELETE FROM `dbms_mass`.`m_details` WHERE `md_id`= pid;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Saveall` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `Saveall`(
                                                        ed_primary_school VARCHAR(255),
                                                        ed_primary_year VARCHAR(255),
                                                        ed_secondary_school VARCHAR(255),
                                                        ed_secondary_year VARCHAR(255),
                                                        ed_tertiary_school VARCHAR(255),
                                                        ed_tertiary_year VARCHAR(255),
                                                        ed_tertiary_course VARCHAR(255),
                                                        ed_vocational_school VARCHAR(255),
                                                        ed_vocational_year VARCHAR(255),
                                                        ed_vocational_course VARCHAR(255),
                                                        ed_honors VARCHAR(255),
                                                        
                                                        md_rank VARCHAR(255),
                                                        md_position VARCHAR(255),
                                                        md_dateofinvest VARCHAR(255),
                                                        md_dateofpromote VARCHAR(255),
                                                        md_otherministries VARCHAR(255),
                                                        md_awards VARCHAR(255),
                                                        md_violations VARCHAR(255),
                                                        md_sacraments VARCHAR(255),
														md_status VARCHAR(255),
                                                        md_retrieve VARCHAR(255),
                                                        
                                                        pd_fullname VARCHAR (255),
														pd_address VARCHAR(255),
														pd_birthday VARCHAR(255),
														pd_birthplace VARCHAR(2555),
														pd_age VARCHAR(255),
                                                        pd_email VARCHAR(255),
                                                        pd_batch VARCHAR(255),
                                                        pd_photo VARCHAR(255),
                                                        pd_mothername VARCHAR(255),
                                                        pd_motheroccupation VARCHAR(255),
														pd_fathername VARCHAR(255),
                                                        pd_fatheroccupation VARCHAR(255),
                                                        pd_contactnumber VARCHAR(255),
                                                        pd_contactnumbermother VARCHAR(255),
                                                        pd_contactnumberfather VARCHAR(255),
                                                        pd_lastname VARCHAR(255)
													 )
BEGIN
/*for educational data*/

INSERT INTO e_details(ed_primary_school,ed_primary_year,ed_secondary_school,ed_secondary_year,ed_tertiary_school,ed_tertiary_year,ed_tertiary_course,ed_vocational_school,ed_vocational_year,ed_vocational_course,ed_honors)
VALUES(ed_primary_school,ed_primary_year,ed_secondary_school,ed_secondary_year,ed_tertiary_school,ed_tertiary_year,ed_tertiary_course,ed_vocational_school,ed_vocational_year,ed_vocational_course,ed_honors);

/*for ministry data*/

INSERT INTO m_details(md_rank,md_position,md_dateofinvest,md_dateofpromote,md_otherministries,md_awards,md_violations,md_sacraments,md_status,md_retrieve)
VALUES(md_rank,md_position,md_dateofinvest,md_dateofpromote,md_otherministries,md_awards,md_violations,md_sacraments,md_status,md_retrieve);

/*for personal data*/

INSERT INTO p_details(pd_fullname, pd_address, pd_birthday,pd_birthplace, pd_age,pd_email,pd_contactnumber,pd_batch,pd_photo,pd_mothername,pd_motheroccupation,pd_fathername,pd_fatheroccupation,pd_contactnumbermother,pd_contactnumberfather,pd_lastname)
VALUES(pd_fullname, pd_address, pd_birthday,pd_birthplace, pd_age,pd_email,pd_contactnumber,pd_batch,pd_photo,pd_mothername,pd_motheroccupation,pd_fathername,pd_fatheroccupation,pd_contactnumbermother,pd_contactnumberfather,pd_lastname);


END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `SaveAttendance` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `SaveAttendance`(
																pd_id INT(11),
                                                                ad_date VARCHAR(255)

																)
BEGIN
INSERT INTO `dbms_mass`.`attendance` (`pd_id`, `ad_date`) 
VALUES (pd_id,ad_date);

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `SaveNotes` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `SaveNotes`(
																notes_date date,
                                                                notes_note VARCHAR(255)

																)
BEGIN
INSERT INTO `dbms_mass`.`notes` (`notes_date`, `notes_note`) 
VALUES (notes_date,notes_note);

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `UpdateDetailsSave` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `UpdateDetailsSave`(
                                                        ed_primary_school VARCHAR(255),
                                                        ed_primary_year VARCHAR(255),
                                                        ed_secondary_school VARCHAR(255),
                                                        ed_secondary_year VARCHAR(255),
                                                        ed_tertiary_school VARCHAR(255),
                                                        ed_tertiary_year VARCHAR(255),
                                                        ed_tertiary_course VARCHAR(255),
                                                        ed_vocational_school VARCHAR(255),
                                                        ed_vocational_year VARCHAR(255),
                                                        ed_vocational_course VARCHAR(255),
                                                        ed_honors VARCHAR(255),
                                                        
                                                        md_rank VARCHAR(255),
                                                        md_position VARCHAR(255),
                                                        md_dateofinvest VARCHAR(255),
                                                        md_dateofpromote VARCHAR(255),
                                                        md_otherministries VARCHAR(255),
                                                        md_awards VARCHAR(255),
                                                        md_violations VARCHAR(255),
                                                        md_sacraments VARCHAR(255),
                                                        md_status VARCHAR(255),
                                                        md_retrieve VARCHAR(255),
                                                        
                                                        pd_fullname VARCHAR (255),
														pd_address VARCHAR(255),
														pd_birthday VARCHAR(255),
														pd_birthplace VARCHAR(2555),
														pd_age VARCHAR(255),
                                                        pd_email VARCHAR(255),
                                                        pd_batch VARCHAR(255),
                                                        pd_photo VARCHAR(255),
                                                        pd_mothername VARCHAR(255),
                                                        pd_motheroccupation VARCHAR(255),
														pd_fathername VARCHAR(255),
                                                        pd_fatheroccupation VARCHAR(255),
                                                        pd_contactnumber VARCHAR(255),
                                                        pd_contactnumbermother VARCHAR(255),
                                                        pd_contactnumberfather VARCHAR(255),
                                                        pd_lastname VARCHAR(255),
														pd_id1 INT(11)
													 )
BEGIN

UPDATE `dbms_mass`.`p_details` SET
 `pd_fullname`= pd_fullname,
 `pd_address`= pd_address, 
 `pd_birthday`= pd_birthday,
 `pd_birthplace`= pd_birthplace,
 `pd_age`= pd_age,
 `pd_email`= pd_email,
 `pd_contactnumber`= pd_contactnumber,
 `pd_batch`= pd_batch,
 `pd_photo`= pd_photo,
 `pd_mothername`= pd_mothername,
 `pd_motheroccupation`= pd_motheroccupation,
 `pd_fathername`= pd_fathername,
 `pd_fatheroccupation`= pd_fatheroccupation,
 `pd_contactnumbermother`= pd_contactnumbermother,
 `pd_contactnumberfather`= pd_contactnumberfather,
`pd_lastname`= pd_lastname 
 WHERE `pd_id`= pd_id1;

 UPDATE `dbms_mass`.`e_details` SET
`ed_primary_school`=ed_primary_school,
`ed_primary_year`=ed_primary_year,
`ed_secondary_school`=ed_secondary_school,
`ed_secondary_year`=ed_secondary_year,
`ed_tertiary_school`=ed_tertiary_school,
`ed_tertiary_year`=ed_tertiary_year,
`ed_tertiary_course`=ed_tertiary_course,
`ed_vocational_school`=ed_vocational_school,
`ed_vocational_year`=ed_vocational_year,
`ed_vocational_course`=ed_vocational_course,
`ed_honors`=ed_honors
WHERE `ed_id`=pd_id1;

UPDATE `dbms_mass`.`m_details` SET
`md_rank`=md_rank,
`md_position`=md_position,
`md_dateofinvest`=md_dateofinvest,
`md_dateofpromote`=md_dateofpromote,
`md_otherministries`=md_otherministries,
`md_awards`=md_awards,
`md_violations`=md_violations,
`md_sacraments`=md_sacraments,
`md_status`= md_status,
`md_retrieve`= md_retrieve
WHERE `md_id`=pd_id1;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-04-07  0:23:13

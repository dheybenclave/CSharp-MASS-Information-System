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
  CONSTRAINT `pd_id` FOREIGN KEY (`pd_id`) REFERENCES `p_details` (`pd_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `attendance`
--

LOCK TABLES `attendance` WRITE;
/*!40000 ALTER TABLE `attendance` DISABLE KEYS */;
INSERT INTO `attendance` VALUES (3,1,'2018-03-23','2018-03-24 01:55:14');
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
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `e_details`
--

LOCK TABLES `e_details` WRITE;
/*!40000 ALTER TABLE `e_details` DISABLE KEYS */;
INSERT INTO `e_details` VALUES (1,'Central ','2010','Main ','2014','Npc ','2018','','','','','top 2  2nd year  |'),(2,'','2010','','2014','','2018','','','','','');
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
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `m_details`
--

LOCK TABLES `m_details` WRITE;
/*!40000 ALTER TABLE `m_details` DISABLE KEYS */;
INSERT INTO `m_details` VALUES (1,'Alumni','Assistant Coordinator','May 11, 2009','December 22, 2012','1|2|3|most outstanding altar server 2012|','','','','Active','NO'),(2,'Aspirant','Member','May 11, 2009','December 22, 2012','','','','','Active','NO');
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `notes`
--

LOCK TABLES `notes` WRITE;
/*!40000 ALTER TABLE `notes` DISABLE KEYS */;
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
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `p_details`
--

LOCK TABLES `p_details` WRITE;
/*!40000 ALTER TABLE `p_details` DISABLE KEYS */;
INSERT INTO `p_details` VALUES (1,'Dheo_ B. Claveria','Navotas City ','January 12, 1998','D ',20,'dheybencalve','09465553870','St Tarcisio  - 2009 ',NULL,'Vima Claveria ','','Danilo Claveria ','','','','Claveria'),(2,'Ming_ M. Ming','Navotas ','January 06, 2009','Navotas ',9,'','','St Paul  - 2009 ',NULL,'Tiger ','Eater Of The Rat ','Lion ','King Of The Jungle ','','','Ming');
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
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (1,'admin','admin','Admin'),(2,'dheo','dheo','Treasurer'),(3,'claveriad','claveria','Co-Coordinator');
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-03-24 10:11:44

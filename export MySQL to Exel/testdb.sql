-- phpMyAdmin SQL Dump
-- version 4.2.7.1
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: 
-- Версия на сървъра: 5.6.20
-- PHP Version: 5.5.15

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `testdb`
--

-- --------------------------------------------------------

--
-- Структура на таблица `cars`
--

CREATE TABLE IF NOT EXISTS `cars` (
`id` int(11) NOT NULL,
  `name` varchar(10) NOT NULL,
  `num` int(11) NOT NULL
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=7 ;

--
-- Схема на данните от таблица `cars`
--

INSERT INTO `cars` (`id`, `name`, `num`) VALUES
(1, 'Toyota', 2),
(2, 'Mazda', 3),
(3, 'Ford', 3),
(4, 'Dacia', 3),
(5, 'Opel', 3),
(6, 'Lada', 3);

-- --------------------------------------------------------

--
-- Структура на таблица `motors`
--

CREATE TABLE IF NOT EXISTS `motors` (
`id` int(11) NOT NULL,
  `name` varchar(20) NOT NULL,
  `count` int(11) NOT NULL
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=3 ;

--
-- Схема на данните от таблица `motors`
--

INSERT INTO `motors` (`id`, `name`, `count`) VALUES
(1, 'ATV', 2),
(2, 'YAMAHA', 2);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `cars`
--
ALTER TABLE `cars`
 ADD PRIMARY KEY (`id`);

--
-- Indexes for table `motors`
--
ALTER TABLE `motors`
 ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `cars`
--
ALTER TABLE `cars`
MODIFY `id` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=7;
--
-- AUTO_INCREMENT for table `motors`
--
ALTER TABLE `motors`
MODIFY `id` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=3;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

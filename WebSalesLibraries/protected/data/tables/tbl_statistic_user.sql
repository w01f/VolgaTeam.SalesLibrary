DROP TABLE IF EXISTS `tbl_statistic_user`;
CREATE TABLE IF NOT EXISTS `tbl_statistic_user` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `id_activity` varchar(32) NOT NULL,
  `login` varchar(128) DEFAULT NULL,
  `first_name` varchar(128) DEFAULT NULL,
  `last_name` varchar(128) DEFAULT NULL,
  `email` varchar(128) DEFAULT NULL,
  `phone` varchar(128) DEFAULT NULL,
  `ip` varchar(64) DEFAULT NULL,
  `os` varchar(256) DEFAULT NULL,
  `device` varchar(256) DEFAULT NULL,
  `browser` varchar(256) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `login` (`login`),
  KEY `id_statistic` (`id_activity`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;
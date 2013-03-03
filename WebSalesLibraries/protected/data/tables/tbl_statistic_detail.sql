DROP TABLE IF EXISTS `tbl_statistic_detail`;
CREATE TABLE IF NOT EXISTS `tbl_statistic_detail` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `id_activity` varchar(32) NOT NULL,
  `tag` varchar(64) NOT NULL,
  `data` varchar(2048) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `tag` (`tag`),
  KEY `id_activity` (`id_activity`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;
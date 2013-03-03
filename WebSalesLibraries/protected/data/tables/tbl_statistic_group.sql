DROP TABLE IF EXISTS `tbl_statistic_group`;
CREATE TABLE IF NOT EXISTS `tbl_statistic_group` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `id_activity` varchar(32) NOT NULL,
  `name` varchar(64) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `id_activity` (`id_activity`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;
DROP TABLE IF EXISTS `tbl_statistic_data`;
CREATE TABLE IF NOT EXISTS `tbl_statistic_data` (
  `id` bigint NOT NULL AUTO_INCREMENT,
  `id_activity` varchar(32) NOT NULL,
  `data` longblob,
  PRIMARY KEY (`id`),
  KEY `id_activity` (`id_activity`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;
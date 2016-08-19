DROP TABLE IF EXISTS `tbl_statistic_link`;
CREATE TABLE IF NOT EXISTS `tbl_statistic_link` (
  `id` bigint NOT NULL AUTO_INCREMENT,
  `id_activity` varchar(32) NOT NULL,
  `id_link` varchar(36) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `id_activity` (`id_activity`),
  KEY `id_link` (`id_link`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;
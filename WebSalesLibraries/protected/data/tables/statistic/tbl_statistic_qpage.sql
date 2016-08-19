DROP TABLE IF EXISTS `tbl_statistic_qpage`;
CREATE TABLE IF NOT EXISTS `tbl_statistic_qpage` (
  `id` bigint NOT NULL AUTO_INCREMENT,
  `id_activity` varchar(32) NOT NULL,
  `id_qpage` varchar(36) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `id_activity` (`id_activity`),
  KEY `id_qpage` (`id_qpage`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;
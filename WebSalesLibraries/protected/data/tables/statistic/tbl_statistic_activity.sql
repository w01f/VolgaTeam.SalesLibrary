DROP TABLE IF EXISTS `tbl_statistic_activity`;
CREATE TABLE IF NOT EXISTS `tbl_statistic_activity` (
  `id` varchar(32) NOT NULL,
  `date_time` datetime NOT NULL,
  `type` varchar(64) NOT NULL,
  `sub_type` varchar(256) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `date_time` (`date_time`,`type`,`sub_type`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS `tbl_cadmin_connection_info`;
CREATE TABLE IF NOT EXISTS `tbl_cadmin_connection_info` (
  `id` varchar(36) NOT NULL,
  `user` varchar(256) NOT NULL,
  `last_update` datetime NOT NULL,
  PRIMARY KEY (`id`),
  KEY `user` (`user`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

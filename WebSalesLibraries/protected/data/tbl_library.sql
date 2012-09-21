DROP TABLE IF EXISTS `tbl_library`;
CREATE TABLE IF NOT EXISTS `tbl_library` (
  `id` varchar(36) NOT NULL,
  `name` varchar(256) NOT NULL,
  `last_update` datetime NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;
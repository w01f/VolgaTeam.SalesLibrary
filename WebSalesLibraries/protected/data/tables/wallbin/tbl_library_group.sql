DROP TABLE IF EXISTS `tbl_library_group`;
CREATE TABLE IF NOT EXISTS `tbl_library_group` (
  `id` varchar(36) NOT NULL,
  `order` int(11) NOT NULL,
  `name` varchar(256) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `order` (`order`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

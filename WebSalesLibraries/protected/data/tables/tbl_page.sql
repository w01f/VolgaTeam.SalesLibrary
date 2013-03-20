DROP TABLE IF EXISTS `tbl_page`;
CREATE TABLE IF NOT EXISTS `tbl_page` (
  `id` varchar(36) NOT NULL,
  `id_library` varchar(36) NOT NULL,
  `name` varchar(256) NOT NULL,
  `order` int(11) NOT NULL,
  `has_columns` tinyint(1) NOT NULL,
  `date_modify` datetime NULL,
  `cached_col_view` longblob,
  PRIMARY KEY (`id`),
  KEY `id_library` (`id_library`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;
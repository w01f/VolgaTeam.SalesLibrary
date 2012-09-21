DROP TABLE IF EXISTS `tbl_page`;
CREATE TABLE IF NOT EXISTS `tbl_page` (
  `id` varchar(36) NOT NULL,
  `id_library` varchar(36) NOT NULL,
  `name` varchar(256) NOT NULL,
  `order` int(11) NOT NULL,
  `has_columns` tinyint(1) NOT NULL,
  `date_modify` datetime NULL,
  `cached_col_view_ie` longblob,
  `cached_col_view_firefox` longblob,
  `cached_col_view_webkit` longblob,
  `cached_col_view_opera` longblob,
  `cached_col_view_mobile` longblob,
  PRIMARY KEY (`id`),
  KEY `id_library` (`id_library`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;
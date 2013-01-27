DROP TABLE IF EXISTS `tbl_user_page_cache`;
CREATE TABLE IF NOT EXISTS `tbl_user_page_cache` (
  `id` varchar(36) NOT NULL,
  `id_user` int(11) NOT NULL,
  `id_page` varchar(36) NOT NULL,
  `id_library` varchar(36) NOT NULL,
  `cached_col_view_ie` longblob,
  `cached_col_view_firefox` longblob,
  `cached_col_view_webkit` longblob,
  `cached_col_view_opera` longblob,
  `cached_col_view_mobile` longblob,
  PRIMARY KEY (`id`),
  KEY `user` (`id_user`),
  KEY `page` (`id_page`),
  KEY `library` (`id_library`)
) ENGINE=MyISAM  DEFAULT CHARSET=utf8 AUTO_INCREMENT=2 ;
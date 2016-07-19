DROP TABLE IF EXISTS `tbl_group_library`;
CREATE TABLE IF NOT EXISTS `tbl_group_library` (
  `id` varchar(36) NOT NULL,
  `id_group` varchar(36) NOT NULL,
  `id_library` varchar(36) NOT NULL,
  `id_page` varchar(36) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `user` (`id_group`),
  KEY `library` (`id_library`),
  KEY `page` (`id_page`)
) ENGINE=MyISAM  DEFAULT CHARSET=utf8 AUTO_INCREMENT=2 ;
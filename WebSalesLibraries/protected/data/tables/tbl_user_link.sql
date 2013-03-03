DROP TABLE IF EXISTS `tbl_user_link`;
CREATE TABLE IF NOT EXISTS `tbl_user_link` (
  `id` varchar(36) NOT NULL,
  `id_user` int(11) NOT NULL,
  `id_link` varchar(36) NOT NULL,
  `id_library` varchar(36) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `user` (`id_user`),
  KEY `link` (`id_link`),
  KEY `library` (`id_library`)
) ENGINE=MyISAM  DEFAULT CHARSET=utf8 AUTO_INCREMENT=2 ;
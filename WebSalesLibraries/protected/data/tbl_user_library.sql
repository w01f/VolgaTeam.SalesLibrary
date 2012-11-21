DROP TABLE IF EXISTS `tbl_user_library`;
CREATE TABLE IF NOT EXISTS `tbl_user_library` (
  `id` varchar(36) NOT NULL,
  `id_user` int(11) NOT NULL,
  `id_library` varchar(36) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `user` (`id_user`),
  KEY `library` (`id_library`)
) ENGINE=MyISAM  DEFAULT CHARSET=utf8 AUTO_INCREMENT=2 ;
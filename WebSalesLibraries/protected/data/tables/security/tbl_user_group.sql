DROP TABLE IF EXISTS `tbl_user_group`;
CREATE TABLE IF NOT EXISTS `tbl_user_group` (
  `id` varchar(36) NOT NULL,
  `id_user` int(11) NOT NULL,
  `id_group` varchar(36) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `user` (`id_user`),
  KEY `group` (`id_group`)
) ENGINE=MyISAM  DEFAULT CHARSET=utf8 AUTO_INCREMENT=2 ;
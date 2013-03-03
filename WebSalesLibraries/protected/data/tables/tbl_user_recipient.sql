DROP TABLE IF EXISTS `tbl_user_recipient`;
CREATE TABLE IF NOT EXISTS `tbl_user_recipient` (
  `id` varchar(36) NOT NULL,
  `id_user` int(11) NOT NULL,
  `recipient` varchar(256) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `user` (`id_user`)
) ENGINE=MyISAM  DEFAULT CHARSET=utf8 AUTO_INCREMENT=2 ;
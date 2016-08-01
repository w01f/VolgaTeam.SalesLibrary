DROP TABLE IF EXISTS `tbl_link_user_profile`;
CREATE TABLE IF NOT EXISTS `tbl_link_user_profile` (
  `id` varchar(36) NOT NULL,
  `id_user` int(11) NOT NULL,
  `config` longblob,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM  DEFAULT CHARSET=utf8 AUTO_INCREMENT=2 ;
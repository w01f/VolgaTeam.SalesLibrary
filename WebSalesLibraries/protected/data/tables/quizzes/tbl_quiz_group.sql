DROP TABLE IF EXISTS `tbl_quiz_group`;
CREATE TABLE IF NOT EXISTS `tbl_quiz_group` (
  `id` varchar(36) NOT NULL,
  `name` varchar(256) NOT NULL,
  `id_parent` varchar(36) NULL,
  `id_top_level` varchar(36) NULL,
  `order` int(11) NOT NULL,
  `config` longblob,
  PRIMARY KEY (`id`),
  KEY `tab` (`id_parent`)
) ENGINE=MyISAM  DEFAULT CHARSET=utf8 AUTO_INCREMENT=2 ;
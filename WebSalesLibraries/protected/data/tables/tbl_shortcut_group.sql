DROP TABLE IF EXISTS `tbl_shortcut_group`;
CREATE TABLE IF NOT EXISTS `tbl_shortcut_group` (
  `id` varchar(36) NOT NULL,
  `order` int(11) NOT NULL,
  `source_path` varchar(512) NOT NULL,
  `config` longblob,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM  DEFAULT CHARSET=utf8 AUTO_INCREMENT=2 ;
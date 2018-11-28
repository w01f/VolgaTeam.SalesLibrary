DROP TABLE IF EXISTS `tbl_shortcut_link`;
CREATE TABLE IF NOT EXISTS `tbl_shortcut_link` (
  `id` varchar(256) NOT NULL,
  `id_group` varchar(256),
  `id_parent` varchar(256),
  `type` varchar(256) NOT NULL,
  `order` int(11) NOT NULL,
  `source_path` varchar(512) NOT NULL,
  `config` longblob,
  PRIMARY KEY (`id`),
  KEY `group` (`id_group`),
  KEY `parent_link` (`id_parent`)
) ENGINE=MyISAM  DEFAULT CHARSET=utf8 AUTO_INCREMENT=2 ;
DROP TABLE IF EXISTS `tbl_shortcut_page`;
CREATE TABLE IF NOT EXISTS `tbl_shortcut_page` (
  `id` varchar(36) NOT NULL,
  `id_tab` varchar(36) NOT NULL,
  `name` varchar(256) NOT NULL,
  `order` int(11) NOT NULL,
  `enabled` tinyint(1) NOT NULL,
  `image_path` varchar(512) NULL,
  `source_path` varchar(512) NOT NULL,
  `config` longblob,
  PRIMARY KEY (`id`),
  KEY `tab` (`id_tab`)
) ENGINE=MyISAM  DEFAULT CHARSET=utf8 AUTO_INCREMENT=2 ;
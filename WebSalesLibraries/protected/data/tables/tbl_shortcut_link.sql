DROP TABLE IF EXISTS `tbl_shortcut_link`;
CREATE TABLE IF NOT EXISTS `tbl_shortcut_link` (
  `id` varchar(36) NOT NULL,
  `id_tab` varchar(36) NOT NULL,
  `id_page` varchar(36),
  `id_group` varchar(36),
  `order` int(11) NOT NULL,
  `type` varchar(64) NOT NULL,
  `source_path` varchar(512) NOT NULL,
  `image_path` varchar(512) NULL,
  `config` longblob,
  PRIMARY KEY (`id`),
  KEY `tab` (`id_tab`),
  KEY `page` (`id_page`)
) ENGINE=MyISAM  DEFAULT CHARSET=utf8 AUTO_INCREMENT=2 ;
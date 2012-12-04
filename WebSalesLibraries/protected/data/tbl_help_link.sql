DROP TABLE IF EXISTS `tbl_help_link`;
CREATE TABLE IF NOT EXISTS `tbl_help_link` (
  `id` varchar(36) NOT NULL,
  `id_tab` varchar(36) NOT NULL,
  `id_page` varchar(36) NOT NULL,
  `name` varchar(256) NOT NULL,
  `order` int(11) NOT NULL,
  `type` varchar(64) NOT NULL,
  `source_path` varchar(512) NOT NULL,
  `enabled` tinyint(1) NOT NULL,
  `image_path` varchar(512) NULL,
  PRIMARY KEY (`id`),
  KEY `tab` (`id_tab`),
  KEY `page` (`id_page`)
) ENGINE=MyISAM  DEFAULT CHARSET=utf8 AUTO_INCREMENT=2 ;
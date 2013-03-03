DROP TABLE IF EXISTS `tbl_help_page`;
CREATE TABLE IF NOT EXISTS `tbl_help_page` (
  `id` varchar(36) NOT NULL,
  `id_tab` varchar(36) NOT NULL,
  `name` varchar(256) NOT NULL,
  `order` int(11) NOT NULL,
  `enabled` tinyint(1) NOT NULL,
  `image_path` varchar(512) NULL,
  PRIMARY KEY (`id`),
  KEY `tab` (`id_tab`)
) ENGINE=MyISAM  DEFAULT CHARSET=utf8 AUTO_INCREMENT=2 ;
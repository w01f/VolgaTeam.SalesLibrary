DROP TABLE IF EXISTS `tbl_banner`;
CREATE TABLE IF NOT EXISTS `tbl_banner` (
  `id` varchar(36) NOT NULL,
  `id_library` varchar(36) NOT NULL,
  `enabled` tinyint(1) NOT NULL,
  `image` mediumblob,
  `show_text` tinyint(1) NOT NULL,
  `image_alignment` varchar(16) NOT NULL,
  `text` varchar(256) DEFAULT NULL,
  `fore_color` varchar(8) NOT NULL,
  `font_name` varchar(64) NOT NULL,
  `font_size` int(11) NOT NULL,
  `font_bold` tinyint(1) NOT NULL,
  `font_italic` tinyint(1) NOT NULL,
  `date_modify` datetime NULL,
  PRIMARY KEY (`id`),
  KEY `id_library` (`id_library`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;
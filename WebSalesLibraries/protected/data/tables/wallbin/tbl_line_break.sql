DROP TABLE IF EXISTS `tbl_line_break`;
CREATE TABLE IF NOT EXISTS `tbl_line_break` (
  `id` varchar(36) NOT NULL,
  `id_library` varchar(36) NOT NULL,
  `note` varchar(256),
  `fore_color` varchar(8) NOT NULL,
  `font_name` varchar(64) NOT NULL,
  `font_size` int(11) NOT NULL,
  `font_bold` tinyint(1) NOT NULL,
  `font_italic` tinyint(1) NOT NULL,
  `font_underline` tinyint(1) NULL,
  `date_modify` datetime NULL,
  PRIMARY KEY (`id`),
  KEY `id_library` (`id_library`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;
DROP TABLE IF EXISTS `tbl_column`;
CREATE TABLE IF NOT EXISTS `tbl_column` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `id_page` varchar(36) NOT NULL,
  `id_library` varchar(36) NOT NULL,
  `name` varchar(256) NOT NULL,
  `order` int(11) NOT NULL,
  `back_color` varchar(8) NOT NULL,
  `fore_color` varchar(8) NOT NULL,
  `font_name` varchar(64) NOT NULL,
  `font_size` int(11) NOT NULL,
  `font_bold` tinyint(1) NOT NULL,
  `font_italic` tinyint(1) NOT NULL,
  `show_text` tinyint(1) NOT NULL,
  `alignment` varchar(64) NOT NULL,
  `enable_widget` tinyint(1) NOT NULL,
  `widget` mediumblob,
  `id_banner` varchar(36) NOT NULL,
  `settings` longtext,
  `date_modify` datetime NULL,
  PRIMARY KEY (`id`),
  KEY `id_page` (`id_page`),
  KEY `id_library` (`id_library`)
) ENGINE=MyISAM  DEFAULT CHARSET=latin1 AUTO_INCREMENT=55 ;
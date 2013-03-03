DROP TABLE IF EXISTS `tbl_autowidget`;
CREATE TABLE IF NOT EXISTS `tbl_autowidget` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `id_library` varchar(36) NOT NULL,
  `extension` varchar(8) NOT NULL,
  `widget` mediumblob,
  PRIMARY KEY (`id`),
  KEY `id_library` (`id_library`)
) ENGINE=MyISAM  DEFAULT CHARSET=latin1 AUTO_INCREMENT=5 ;
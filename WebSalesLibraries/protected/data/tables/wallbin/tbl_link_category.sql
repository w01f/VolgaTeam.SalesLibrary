DROP TABLE IF EXISTS `tbl_link_category`;
CREATE TABLE IF NOT EXISTS `tbl_link_category` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `id_link` varchar(36) NOT NULL,
  `id_library` varchar(36) NOT NULL,
  `category` varchar(256) NOT NULL,
  `tag` varchar(256) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `id_link` (`id_link`),
  KEY `id_library` (`id_library`),
  KEY `category` (`category`,`tag`)
) ENGINE=MyISAM  DEFAULT CHARSET=latin1 AUTO_INCREMENT=165 ;

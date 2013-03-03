DROP TABLE IF EXISTS `tbl_attachment`;
CREATE TABLE IF NOT EXISTS `tbl_attachment` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `id_link` varchar(36) NOT NULL,
  `id_library` varchar(36) NOT NULL,
  `name` varchar(256) NOT NULL,
  `path` varchar(256) NOT NULL,
  `format` varchar(256) NOT NULL,
  `id_preview` varchar(36),
  PRIMARY KEY (`id`),
  KEY `id_link` (`id_link`),
  KEY `id_library` (`id_library`)
) ENGINE=MyISAM  DEFAULT CHARSET=latin1;

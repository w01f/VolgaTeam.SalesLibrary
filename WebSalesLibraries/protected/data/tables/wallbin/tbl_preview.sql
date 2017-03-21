DROP TABLE IF EXISTS `tbl_preview`;
CREATE TABLE IF NOT EXISTS `tbl_preview` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `id_container` varchar(36) NOT NULL,
  `id_library` varchar(36) NOT NULL,
  `type` varchar(16) NOT NULL,
  `relative_path` varchar(256) NOT NULL,
  `thumb_width` int(11),
  `thumb_height` int(11),
  PRIMARY KEY (`id`),
  KEY `id_container` (`id_container`),
  KEY `id_library` (`id_library`),
  KEY `type_container` (`id_container`,`type`),
  KEY `type` (`type`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;
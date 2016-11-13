DROP TABLE IF EXISTS `tbl_library`;
CREATE TABLE IF NOT EXISTS `tbl_library` (
  `id` varchar(36) NOT NULL,
  `id_group` varchar(36) DEFAULT NULL,
  `order` int(11) DEFAULT NULL,
  `name` varchar(256) NOT NULL,
  `path` varchar(256),
  `settings` longtext,
  `last_update` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `group` (`id_group`),
  KEY `order` (`order`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS `tbl_library_config`;
CREATE TABLE IF NOT EXISTS `tbl_library_config` (
  `id` varchar(36) NOT NULL,
  `id_library` varchar(36) NOT NULL,
  `dead_link_sender` varchar(256) NOT NULL,
  `dead_link_recipients` varchar(1024) NOT NULL,
  `dead_link_subject` varchar(1024) NOT NULL,
  `dead_link_body` varchar(1024) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `id_library` (`id_library`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;
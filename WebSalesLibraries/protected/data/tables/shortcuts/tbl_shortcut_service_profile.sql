DROP TABLE IF EXISTS `tbl_shortcut_service_profile`;
CREATE TABLE IF NOT EXISTS `tbl_shortcut_service_profile` (
  `id` varchar(36) NOT NULL,
  `name` varchar(512) NOT NULL,
  `service_type` varchar(128) NOT NULL,
  `config` longtext,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM  DEFAULT CHARSET=utf8;
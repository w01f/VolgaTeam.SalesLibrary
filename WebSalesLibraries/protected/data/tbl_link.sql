DROP TABLE IF EXISTS `tbl_link`;
CREATE TABLE IF NOT EXISTS `tbl_link` (
  `id` varchar(36) NOT NULL,
  `id_folder` varchar(36) NOT NULL,
  `id_library` varchar(36) NOT NULL,
  `name` varchar(256) NOT NULL,
  `file_relative_path` varchar(256) NOT NULL,
  `file_name` varchar(256) NOT NULL,
  `file_extension` varchar(8) NOT NULL,
  `note` varchar(256) NOT NULL,
  `is_bold` tinyint(1) NOT NULL,
  `order` int(11) NOT NULL,
  `type` int(11) NOT NULL,
  `enable_widget` tinyint(1) NOT NULL,
  `widget` mediumblob,
  `id_banner` varchar(36) NOT NULL,
  `id_line_break` varchar(36),
  `content` longtext,
  PRIMARY KEY (`id`),
  KEY `id_folder` (`id_folder`),
  KEY `id_library` (`id_library`),
  FULLTEXT KEY `content` (`name`,`file_name`,`content`)
) ENGINE=MyISAM  DEFAULT CHARSET=latin1;

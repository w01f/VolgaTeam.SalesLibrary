DROP TABLE IF EXISTS `tbl_link`;
CREATE TABLE IF NOT EXISTS `tbl_link` (
  `id` varchar(36) NOT NULL,
  `id_parent_link` varchar(36) NULL,
  `id_folder` varchar(36) NOT NULL,
  `id_library` varchar(36) NOT NULL,
  `name` varchar(4096) NOT NULL,
  `file_relative_path` varchar(256) NOT NULL,
  `file_name` varchar(256) NOT NULL,
  `file_extension` varchar(8) NOT NULL,
  `file_date` datetime NULL,
  `file_size` int(11) NULL,
  `format` varchar(256) NOT NULL,
  `order` int(11) NOT NULL,
  `type` int(11) NOT NULL,
  `widget_type` tinyint(1) NOT NULL,
  `widget` mediumblob,
  `id_banner` varchar(36),
  `id_line_break` varchar(36),
  `id_preview` varchar(36),
  `tags` varchar(512) NULL,
  `content` longtext,
  `settings` longtext,
  `is_dead` tinyint(1) NOT NULL DEFAULT 0,
  `is_preview_not_ready` tinyint(1) NOT NULL DEFAULT 0,
  `is_restricted` tinyint(1) NOT NULL DEFAULT 0,
  `no_share` tinyint(1) NOT NULL DEFAULT 0,
  `date_add` datetime NULL,
  `date_modify` datetime NULL,
  PRIMARY KEY (`id`),
  KEY `id_parent_link` (`id_parent_link`),
  KEY `id_folder` (`id_folder`),
  KEY `id_library` (`id_library`),
  KEY `is_restricted` (`is_restricted`),
  KEY `format` (`format`),
  FULLTEXT KEY `content` (`name`,`file_name`,`tags`,`content`)
) ENGINE=MyISAM  DEFAULT CHARSET=latin1;

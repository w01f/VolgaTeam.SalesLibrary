DROP TABLE IF EXISTS `tbl_qpage`;
CREATE TABLE IF NOT EXISTS `tbl_qpage` (
	`id` varchar(36) NOT NULL,
	`id_owner` int(11) NOT NULL,
	`list_order` int(11) NOT NULL DEFAULT '0',
	`title` varchar(2048) NOT NULL,
	`subtitle` longblob NULL,
	`create_date` datetime NULL,
	`expiration_date` datetime NULL,
	`logo` mediumblob,
	`header` longblob NULL,
	`footer` longblob NULL,
	`is_email` tinyint(1) NOT NULL,
	`restricted` tinyint(1) NOT NULL DEFAULT 0,
  `disable_banners` tinyint(1) DEFAULT '0',
  `disable_widgets` tinyint(1) DEFAULT '0',
  `show_links_as_url` tinyint(1) DEFAULT '0',
  `record_activity` tinyint(1) DEFAULT '0',
  `pin_code` varchar(4) NULL DEFAULT NULL,
  `activity_email_copy` varchar(512) NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `id_owner` (`id_owner`),
  KEY `list_order` (`list_order`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
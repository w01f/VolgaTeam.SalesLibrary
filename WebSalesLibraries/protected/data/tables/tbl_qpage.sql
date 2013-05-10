DROP TABLE IF EXISTS `tbl_qpage`;
CREATE TABLE IF NOT EXISTS `tbl_qpage` (
	`id` varchar(36) NOT NULL,
	`id_owner` int(11) NOT NULL,
	`title` varchar(2048) NOT NULL,
	`subtitle` varchar(2048) NULL,
	`create_date` datetime NULL,
	`expiration_date` datetime NULL,
	`logo` mediumblob,
	`header` varchar(2048) NULL,
	`footer` varchar(2048) NULL,
	`is_email` tinyint(1) NOT NULL,
	`restricted` tinyint(1) NOT NULL DEFAULT 1,
	`show_ticker` tinyint(1) NOT NULL DEFAULT 0,
	`show_site_link` tinyint(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`id`),
  KEY `id_owner` (`id_owner`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

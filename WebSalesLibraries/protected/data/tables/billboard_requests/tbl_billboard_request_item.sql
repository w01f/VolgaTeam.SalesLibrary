DROP TABLE IF EXISTS `tbl_billboard_request_item`;
CREATE TABLE IF NOT EXISTS `tbl_billboard_request_item` (
	`id` varchar(36) NOT NULL,
	`id_owner` int(11) NOT NULL,
	`title` varchar(512) NOT NULL,
	`status` varchar(512) NULL,
	`assigned_to` varchar(512) NULL,
	`create_date` datetime NULL,
	`date_submit` datetime NULL,
	`date_needed` datetime NULL,
	`date_completed` datetime NULL,
	`content` longtext,
  PRIMARY KEY (`id`),
  KEY `id_owner` (`id_owner`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

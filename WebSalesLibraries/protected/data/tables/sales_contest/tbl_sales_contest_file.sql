DROP TABLE IF EXISTS `tbl_sales_contest_file`;
CREATE TABLE IF NOT EXISTS `tbl_sales_contest_file` (
	`id` varchar(36) NOT NULL,
	`id_item` varchar(36) NOT NULL,
	`list_order` int(11) NOT NULL DEFAULT '0',
	`file_name` varchar(512) NOT NULL,
	`file_type` varchar(512) NOT NULL,
	`file_format` varchar(512) NOT NULL,
	`upload_date` datetime NOT NULL,
	`content` longblob NULL,
  PRIMARY KEY (`id`),
  KEY `id_item` (`id_item`),
  KEY `list_order` (`list_order`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

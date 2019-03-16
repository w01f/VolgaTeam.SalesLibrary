DROP TABLE IF EXISTS `tbl_sales_idea_item`;
CREATE TABLE IF NOT EXISTS `tbl_sales_idea_item` (
	`id` varchar(36) NOT NULL,
	`id_owner` int(11) NOT NULL,
	`title` varchar(512) NOT NULL,
	`advertiser` varchar(512) NULL,
	`revenue` decimal(37,2) NULL,
	`storage_path` varchar(512) NULL,
	`create_date` datetime NULL,
	`date_submit` datetime NULL,
	`content` longtext,
  PRIMARY KEY (`id`),
  KEY `id_owner` (`id_owner`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

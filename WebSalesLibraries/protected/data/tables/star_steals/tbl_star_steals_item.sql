DROP TABLE IF EXISTS `tbl_star_steals_item`;
CREATE TABLE IF NOT EXISTS `tbl_star_steals_item` (
	`id` varchar(36) NOT NULL,
	`id_owner` int(11) NOT NULL,
	`list_order` int(11) NOT NULL DEFAULT '0',
	`title` varchar(2048) NOT NULL,
	`create_date` datetime NULL,
	`content` longtext,
  PRIMARY KEY (`id`),
  KEY `id_owner` (`id_owner`),
  KEY `list_order` (`list_order`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

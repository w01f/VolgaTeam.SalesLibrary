DROP TABLE IF EXISTS `tbl_link_bundle`;
CREATE TABLE IF NOT EXISTS `tbl_link_bundle` (
	`id` varchar(36) NOT NULL,
	`id_bundle` varchar(36) NOT NULL,
	`id_link` varchar(36) NOT NULL,
  `order` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `id_bundle` (`id_bundle`),
  KEY `id_link` (`id_link`),
  KEY `id_link_order` (`id_link`,`order`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

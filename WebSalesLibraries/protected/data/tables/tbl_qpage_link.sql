DROP TABLE IF EXISTS `tbl_qpage_link`;
CREATE TABLE IF NOT EXISTS `tbl_qpage_link` (
	`id` varchar(36) NOT NULL,
	`id_page` varchar(36) NOT NULL,
	`id_link` varchar(36) NOT NULL,
  `list_order` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  KEY `id_page` (`id_page`),
  KEY `id_link` (`id_link`),
  KEY `list_order` (`list_order`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

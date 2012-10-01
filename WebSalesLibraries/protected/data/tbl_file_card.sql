DROP TABLE IF EXISTS `tbl_file_card`;
CREATE TABLE IF NOT EXISTS `tbl_file_card` (
  `id` varchar(36) NOT NULL,
  `id_library` varchar(36) NOT NULL,
  `title` varchar(256),
  `advertiser` varchar(256),
  `date_sold` datetime DEFAULT NULL,
  `broadcast_closed` decimal(17,2) DEFAULT NULL,
  `digital_closed` decimal(17,2) DEFAULT NULL,
  `publishing_closed` decimal(17,2) DEFAULT NULL,
  `sales_name` varchar(256),
  `sales_email` varchar(256),
  `sales_phone` varchar(256),
  `sales_station` varchar(256),
  `notes` varchar(4096),
  PRIMARY KEY (`id`),
  KEY `id_library` (`id_library`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;
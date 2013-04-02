DROP TABLE IF EXISTS `tbl_ticker_link_detail`;
CREATE TABLE IF NOT EXISTS `tbl_ticker_link_detail` (
  `id` varchar(36) NOT NULL,
  `id_ticker` varchar(36) NOT NULL,
  `tag` varchar(64) NOT NULL,
  `data` varchar(2048) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `tag` (`tag`),
  KEY `id_ticker` (`id_ticker`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
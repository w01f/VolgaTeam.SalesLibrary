DROP TABLE IF EXISTS `tbl_ticker_link`;
CREATE TABLE IF NOT EXISTS `tbl_ticker_link` (
  `id` varchar(36) NOT NULL,
  `type` varchar(64) NOT NULL,
  `text` varchar(2048) DEFAULT NULL,
  `link_order` int(11) NOT NULL DEFAULT 999,
  PRIMARY KEY (`id`),
  KEY `type` (`type`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
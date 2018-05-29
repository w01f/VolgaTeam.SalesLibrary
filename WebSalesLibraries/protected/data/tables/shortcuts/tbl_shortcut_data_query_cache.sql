DROP TABLE IF EXISTS `tbl_shortcut_data_query_cache`;
CREATE TABLE IF NOT EXISTS `tbl_shortcut_data_query_cache` (
  `id` char(128) NOT NULL,
  `id_block` char(128) NOT NULL,
  `expire` datetime NULL,
  `value` longtext,
  PRIMARY KEY (`id`),
  KEY `block_expire` (`id_block`,`expire`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

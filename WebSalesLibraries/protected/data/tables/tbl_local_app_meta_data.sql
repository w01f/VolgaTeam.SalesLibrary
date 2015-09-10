DROP TABLE IF EXISTS `tbl_local_app_meta_data`;
CREATE TABLE IF NOT EXISTS `tbl_local_app_meta_data` (
  `id` varchar(36) NOT NULL,
  `data_tag` varchar(128) NOT NULL,
  `data_content` longblob,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

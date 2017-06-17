DROP TABLE IF EXISTS `tbl_link_internal_link`;
CREATE TABLE IF NOT EXISTS `tbl_link_internal_link` (
  `id` varchar(36) NOT NULL,
  `id_internal` varchar(36) NOT NULL,
  `id_original` varchar(36) NOT NULL,
  `id_library` varchar(36) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `id_internal` (`id_internal`),
  KEY `id_original` (`id_original`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

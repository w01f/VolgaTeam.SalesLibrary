DROP TABLE IF EXISTS `tbl_user_link_cart`;
CREATE TABLE IF NOT EXISTS `tbl_user_link_cart` (
  `id` varchar(36) NOT NULL,
  `id_link` varchar(36) NOT NULL,
  `id_user` varchar(36) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `id_link` (`id_link`),
  KEY `id_user` (`id_user`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

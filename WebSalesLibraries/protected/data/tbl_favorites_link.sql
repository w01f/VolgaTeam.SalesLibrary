DROP TABLE IF EXISTS `tbl_favorites_link`;
CREATE TABLE IF NOT EXISTS `tbl_favorites_link` (
  `id` varchar(36) NOT NULL,
  `id_link` varchar(36) NOT NULL,
  `id_library` varchar(36) NOT NULL,
  `id_folder` varchar(36) NULL,
  `id_user` varchar(36) NOT NULL,
  `name` varchar(512) NULL,
  `order` int(11) NOT NULL DEFAULT 0,
  PRIMARY KEY (`id`),
  KEY `id_link` (`id_link`),
  KEY `id_folder` (`id_folder`),
  KEY `id_user` (`id_user`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

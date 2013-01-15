DROP TABLE IF EXISTS `tbl_favorites_folder`;
CREATE TABLE IF NOT EXISTS `tbl_favorites_folder` (
  `id` varchar(36) NOT NULL,
  `id_parent_folder` varchar(36) NULL,
  `id_user` varchar(36) NOT NULL,
  `name` varchar(512) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `id_parent_folder` (`id_parent_folder`),
  KEY `id_user` (`id_user`),
  KEY `name` (`name`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

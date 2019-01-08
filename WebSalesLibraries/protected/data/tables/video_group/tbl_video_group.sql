DROP TABLE IF EXISTS `tbl_video_group`;
CREATE TABLE IF NOT EXISTS `tbl_video_group` (
	`id` varchar(256) NOT NULL,
  `id_group` varchar(256) NOT NULL,
	`id_shortcut` varchar(256) NOT NULL,
	`id_user` int(11) NOT NULL,
  `state` longtext,
  PRIMARY KEY (`id`),
  KEY `id_user` (`id_user`),
  KEY `id_group` (`id_group`),
  KEY `id_shortcut` (`id_shortcut`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

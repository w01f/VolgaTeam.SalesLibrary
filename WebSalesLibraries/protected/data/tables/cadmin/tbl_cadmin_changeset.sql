DROP TABLE IF EXISTS `tbl_cadmin_changeset`;
CREATE TABLE IF NOT EXISTS `tbl_cadmin_changeset` (
  `id` varchar(36) NOT NULL,
  `id_library` varchar(36) NOT NULL,
  `user` varchar(256) NOT NULL,
  `change_date` datetime NOT NULL,
  `change_type` int(11) NOT NULL,
  `object_type` int(11) NOT NULL,
  `object_data` longtext,
  PRIMARY KEY (`id`),
  KEY `library_date` (`id_library`,`change_date`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

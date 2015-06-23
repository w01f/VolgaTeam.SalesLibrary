DROP TABLE IF EXISTS `tbl_user_tab`;
CREATE TABLE IF NOT EXISTS `tbl_user_tab` (
  `id` varchar(36) NOT NULL,
  `id_user` varchar(36) NOT NULL,
  `id_object` varchar(36) NOT NULL,
  `object_type` int(11) NOT NULL,
  `order` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  INDEX objects_by_user (id_user,object_type)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

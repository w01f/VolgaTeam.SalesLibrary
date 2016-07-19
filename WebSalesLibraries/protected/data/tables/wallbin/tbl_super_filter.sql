DROP TABLE IF EXISTS `tbl_super_filter`;
CREATE TABLE IF NOT EXISTS `tbl_super_filter` (
  `id` int(11) NOT NULL,
  `value` varchar(256) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM  DEFAULT CHARSET=latin1;

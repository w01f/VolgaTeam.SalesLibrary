DROP TABLE IF EXISTS `tbl_category`;
CREATE TABLE IF NOT EXISTS `tbl_category` (
  `id` int(11) NOT NULL,
  `group` varchar(256) NULL,
  `group_icon` varchar(256) NULL,
  `category` varchar(256) NOT NULL,
  `description` varchar(256) NULL,
  `tag` varchar(256) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM  DEFAULT CHARSET=latin1;

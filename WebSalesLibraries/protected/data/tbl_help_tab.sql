DROP TABLE IF EXISTS `tbl_help_tab`;
CREATE TABLE IF NOT EXISTS `tbl_help_tab` (
  `id` varchar(36) NOT NULL,
  `name` varchar(256) NOT NULL,
  `order` int(11) NOT NULL,
  `enabled` tinyint(1) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM  DEFAULT CHARSET=utf8 AUTO_INCREMENT=2 ;
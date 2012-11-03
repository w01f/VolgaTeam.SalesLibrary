DROP TABLE IF EXISTS `tbl_emailed_link`;
CREATE TABLE IF NOT EXISTS `tbl_emailed_link` (
  `id` varchar(32) NOT NULL,
  `name` varchar(256) NOT NULL,
  `path` varchar(512) NOT NULL,
  `initial_date` datetime NOT NULL,
  `expires_in` int(11) NULL,
  `sender_login` varchar(256) NOT NULL,
  `sender_email` varchar(256) NOT NULL,
  `recipients` varchar(256) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

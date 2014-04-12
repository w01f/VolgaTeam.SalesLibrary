DROP TABLE IF EXISTS `tbl_quiz`;
CREATE TABLE IF NOT EXISTS `tbl_quiz` (
  `id` varchar(36) NOT NULL,
  `id_group` varchar(36) NOT NULL,
  `unique_id` varchar(256) NOT NULL,
  `order` int(11) NOT NULL,
  `name` varchar(256) NOT NULL,
  `source_path` varchar(512) NOT NULL,
  `pass_score` int(11) NOT NULL,
  `config` longblob,
  PRIMARY KEY (`id`),
  KEY `group` (`id_group`),
  KEY `unique_id` (`unique_id`)
) ENGINE=MyISAM  DEFAULT CHARSET=utf8 AUTO_INCREMENT=2 ;
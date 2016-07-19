DROP TABLE IF EXISTS `tbl_quiz_result`;
CREATE TABLE IF NOT EXISTS `tbl_quiz_result` (
  `id` varchar(36) NOT NULL,
  `id_user` int(11) NOT NULL,
  `id_quiz` varchar(256) NOT NULL,
  `quiz_set` varchar(36) NOT NULL,
  `id_question` int(11) NOT NULL,
  `question_result` int(11) NOT NULL,
  `successful` tinyint(1) NOT NULL,
  `date` datetime NOT NULL,
  PRIMARY KEY (`id`),
  KEY `user` (`id_user`),
  KEY `quiz` (`id_quiz`),
  KEY `quiz_set` (`quiz_set`),
  KEY `date` (`date`)
) ENGINE=MyISAM  DEFAULT CHARSET=utf8 AUTO_INCREMENT=2 ;
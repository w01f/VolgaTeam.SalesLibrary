DROP TABLE IF EXISTS `tbl_shortcut_bundle_modal_favorite_item`;
CREATE TABLE IF NOT EXISTS `tbl_shortcut_bundle_modal_favorite_item` (
  `id` varchar(256) NOT NULL,
  `id_item` varchar(256),
  `id_owner` int(11) NOT NULL,
  `type` varchar(256) NOT NULL,
  `config` longblob,
  PRIMARY KEY (`id`),
  KEY `item` (`id_item`)
) ENGINE=MyISAM  DEFAULT CHARSET=utf8 AUTO_INCREMENT=2 ;
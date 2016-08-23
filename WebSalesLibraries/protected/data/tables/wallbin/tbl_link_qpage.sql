DROP TABLE IF EXISTS `tbl_link_qpage`;
CREATE TABLE IF NOT EXISTS `tbl_link_qpage` (
  `id` bigint NOT NULL AUTO_INCREMENT,
  `id_link` varchar(36) NOT NULL,
  `id_qpage` varchar(36) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `id_link` (`id_link`),
  KEY `id_qpage` (`id_qpage`),
  KEY `link_qpage` (`id_link`, `id_qpage`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;
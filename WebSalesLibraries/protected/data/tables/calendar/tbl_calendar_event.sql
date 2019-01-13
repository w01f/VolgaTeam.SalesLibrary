DROP TABLE IF EXISTS `tbl_calendar_event`;
CREATE TABLE IF NOT EXISTS `tbl_calendar_event` (
  `id` varchar(256) NOT NULL,
  `id_calendar` varchar(256) NOT NULL,
  `id_shortcut` varchar(256) NOT NULL,
  `event_data` longtext,
  PRIMARY KEY (`id`),
  KEY `id_calendar` (`id_calendar`),
  KEY `id_shortcut` (`id_shortcut`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

<?php
	class LibraryConfigStorage extends CActiveRecord
	{
		public static function model($className = __CLASS__)
		{
			return parent::model($className);
		}

		public function tableName()
		{
			return '{{library_config}}';
		}

		public static function updateData($libraryConfig)
		{
			$libraryConfigRecord = new LibraryConfigStorage();
			$libraryConfigRecord->id = uniqid();
			$libraryConfigRecord->id_library = $libraryConfig['libraryId'];
			$libraryConfigRecord->dead_link_sender = $libraryConfig['deadLinkSender'];
			$libraryConfigRecord->dead_link_recipients = $libraryConfig['deadLinkRecipients'];
			$libraryConfigRecord->dead_link_subject = $libraryConfig['deadLinkSubject'];
			$libraryConfigRecord->dead_link_body = $libraryConfig['deadLinkBody'];
			$libraryConfigRecord->save();
		}

		public static function clearData($libraryId)
		{
			self::model()->deleteAll('id_library=?', array($libraryId));
		}
	}
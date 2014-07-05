<?php

	/**
	 * Class LibraryConfigRecord
	 * @property mixed id
	 * @property mixed id_library
	 * @property mixed dead_link_sender
	 * @property mixed dead_link_recipients
	 * @property mixed dead_link_subject
	 * @property mixed dead_link_body
	 */
	class LibraryConfigRecord extends CActiveRecord
	{
		/**
		 * @param string $className
		 * @return CActiveRecord
		 */
		public static function model($className = __CLASS__)
		{
			return parent::model($className);
		}

		/**
		 * @return string
		 */
		public function tableName()
		{
			return '{{library_config}}';
		}

		/**
		 * @param $libraryConfig
		 */
		public static function updateData($libraryConfig)
		{
			$libraryConfigRecord = new LibraryConfigRecord();
			$libraryConfigRecord->id = uniqid();
			$libraryConfigRecord->id_library = $libraryConfig['libraryId'];
			$libraryConfigRecord->dead_link_sender = $libraryConfig['deadLinkSender'];
			$libraryConfigRecord->dead_link_recipients = $libraryConfig['deadLinkRecipients'];
			$libraryConfigRecord->dead_link_subject = $libraryConfig['deadLinkSubject'];
			$libraryConfigRecord->dead_link_body = $libraryConfig['deadLinkBody'];
			$libraryConfigRecord->save();
		}

		/**
		 * @param $libraryId
		 */
		public static function clearData($libraryId)
		{
			self::model()->deleteAll('id_library=?', array($libraryId));
		}
	}
<?php

	/**
	 * Class AttachmentRecord
	 * @property mixed id_link
	 * @property mixed id_library
	 * @property mixed name
	 * @property mixed path
	 * @property mixed format
	 * @property mixed is_dead
	 * @property mixed is_preview_not_ready
	 * @property mixed id_preview
	 */
	class AttachmentRecord extends CActiveRecord
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
			return '{{attachment}}';
		}

		/**
		 * @param $attachment
		 */
		public static function updateData($attachment)
		{
			$attachmentRecord = new AttachmentRecord();
			$attachmentRecord->id_link = $attachment['linkId'];
			$attachmentRecord->id_library = $attachment['libraryId'];
			$attachmentRecord->name = $attachment['name'];
			$attachmentRecord->path = $attachment['path'];
			$attachmentRecord->format = $attachment['originalFormat'];
			if (array_key_exists('isDead', $attachment))
				$attachmentRecord->is_dead = $attachment['isDead'];
			if (array_key_exists('isPreviewNotReady', $attachment))
				$attachmentRecord->is_preview_not_ready = $attachment['isPreviewNotReady'];

			if (array_key_exists('previewId', $attachment) && isset($attachment['previewId']))
				$attachmentRecord->id_preview = $attachment['previewId'];

			$attachmentRecord->save();
		}

		/**
		 * @param $libraryId
		 */
		public static function clearData($libraryId)
		{
			self::model()->deleteAll('id_library=?', array($libraryId));
		}

		/**
		 * @param $attachmentId
		 * @return AttachmentRecord
		 */
		public static function getAttachmentById($attachmentId)
		{
			$attachmentRecord = self::model()->findByPk($attachmentId);
			if ($attachmentRecord !== false)
				return $attachmentRecord;
			return null;
		}
	}

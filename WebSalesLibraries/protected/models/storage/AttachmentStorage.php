<?php
	class AttachmentStorage extends CActiveRecord
	{
		public static function model($className = __CLASS__)
		{
			return parent::model($className);
		}

		public function tableName()
		{
			return '{{attachment}}';
		}

		public static function updateData($attachment)
		{
			$attachmentRecord = new AttachmentStorage();
			$attachmentRecord->id_link = $attachment['linkId'];
			$attachmentRecord->id_library = $attachment['libraryId'];
			$attachmentRecord->name = $attachment['name'];
			$attachmentRecord->path = $attachment['path'];
			$attachmentRecord->format = $attachment['originalFormat'];
			$attachmentRecord->is_dead = $attachment['isDead'];
			$attachmentRecord->is_preview_not_ready = $attachment['isPreviewNotReady'];

			if (array_key_exists('previewId', $attachment) && isset($attachment['previewId']))
				$attachmentRecord->id_preview = $attachment['previewId'];

			$attachmentRecord->save();
		}

		public static function clearData($libraryId)
		{
			self::model()->deleteAll('id_library=?', array($libraryId));
		}

		public static function getAttachmentById($attachmentId)
		{
			$attachmentRecord = self::model()->findByPk($attachmentId);
			if ($attachmentRecord !== false)
				return $attachmentRecord;
			return null;
		}

	}

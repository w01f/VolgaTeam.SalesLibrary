<?php

	/**
	 * Class UserRecipientRecord
	 * @property mixed id
	 * @property mixed id_user
	 * @property mixed recipient
	 */
	class UserRecipientRecord extends CActiveRecord
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
			return '{{user_recipient}}';
		}

		/**
		 * @param $userId
		 * @param $recipients
		 */
		public static function setRecipientsForUser($userId, $recipients)
		{
			$existedRecipients = self::getRecipientsByUser($userId);
			if (isset($recipients))
			{
				foreach (array_unique($recipients) as $recipient)
				{
					if (!isset($existedRecipients) || (isset($existedRecipients) && !in_array(trim($recipient), $existedRecipients)))
					{
						$userRecipientsRecord = new UserRecipientRecord();
						$userRecipientsRecord->id = uniqid();
						$userRecipientsRecord->id_user = $userId;
						$userRecipientsRecord->recipient = trim($recipient);
						$userRecipientsRecord->save();
					}
				}
			}
		}

		/**
		 * @param $userId
		 * @return array|null
		 */
		public static function getRecipientsByUser($userId)
		{
			$userRecipientsRecords = self::model()->findAll('id_user =?', array($userId));
			if (isset($userRecipientsRecords))
				foreach ($userRecipientsRecords as $userRecipientsRecord)
					$existedRecipients[] = trim($userRecipientsRecord->recipient);
			if (isset($existedRecipients))
				return array_unique($existedRecipients);
			return null;
		}

		/**
		 * @param $userId
		 */
		public static function clearRecipientsByUser($userId)
		{
			self::model()->deleteAll('id_user =?', array($userId));
		}

	}

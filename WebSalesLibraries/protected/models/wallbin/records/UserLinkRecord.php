<?php

	/**
	 * Class UserLinkRecord
	 * @property mixed id
	 * @property mixed id_user
	 * @property mixed id_link
	 * @property mixed id_library
	 * @property int list_order
	 */
	class UserLinkRecord extends CActiveRecord
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
			return '{{user_link}}';
		}

		/**
		 * @param $linkId
		 * @param $libraryId
		 * @param $assignedUsers
		 */
		public static function updateData($linkId, $libraryId, $assignedUsers)
		{
			$assignedUsers = explode(',', $assignedUsers);
			foreach ($assignedUsers as $user)
			{
				/** @var $userRecord UserRecord */
				$userRecord = UserRecord::model()->find('LOWER(login)=?', array(strtolower(trim($user))));
				if (isset($userRecord))
				{
					$userLinkRecord = new UserLinkRecord();
					$userLinkRecord->id = uniqid();
					$userLinkRecord->id_user = $userRecord->id;
					$userLinkRecord->id_link = $linkId;
					$userLinkRecord->id_library = $libraryId;
					$userLinkRecord->save();
				}
			}
		}

		/**
		 * @param $userId
		 * @return array|null
		 */
		public static function getAvailableLinks($userId)
		{
			foreach (self::model()->findAll('id_user=?', array($userId)) as $userLink)
				$linkIds[] = $userLink->id_link;
			return isset($linkIds) ? $linkIds : null;
		}

		/**
		 * @param $libraryId
		 * @return array|null
		 */
		public static function getRestrictedUsersIds($libraryId)
		{
			foreach (self::model()->findAll('id_library=?', array($libraryId)) as $userLink)
				$userIds[] = $userLink->id_user;
			return isset($userIds) ? array_unique($userIds) : null;
		}

		/**
		 * @param $libraryId
		 */
		public static function clearData($libraryId)
		{
			self::model()->deleteAll('id_library=?', array($libraryId));
		}
	}

<?php

	/**
	 * Class LinkBlackListRecord
	 * @property mixed id
	 * @property mixed id_link
	 * @property mixed id_library
	 * @property mixed id_user
	 */
	class LinkBlackListRecord extends CActiveRecord
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
			return '{{link_black_list}}';
		}

		/**
		 * @param $linkId
		 * @param $libraryId
		 * @param $users
		 */
		public static function updateData($linkId, $libraryId, $users)
		{
			$users = explode(',', $users);
			foreach ($users as $user)
			{
				/** @var $userRecord UserRecord */
				$userRecord = UserRecord::model()->find('LOWER(login)=?', array(strtolower(trim($user))));
				if (isset($userRecord))
				{
					$userLinkRecord = new LinkBlackListRecord();
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
		 * @return array
		 */
		public static function getDeniedLinks($userId)
		{
			$linkIds = array();
			foreach (self::model()->findAll('id_user=?', array($userId)) as $userLink)
			{
				/** @var LinkBlackListRecord $userLink*/
				$linkIds[] = $userLink->id_link;
			}
			return $linkIds;
		}

		/**
		 * @param $linkId
		 * @return array
		 */
		public static function getUserIds($linkId)
		{
			$userIds = array();
			foreach (self::model()->findAll('id_link=?', array($linkId)) as $userLink)
			{
				/** @var LinkBlackListRecord $userLink*/
				$userIds[] = $userLink->id_user;
			}
			return array_unique($userIds);
		}

		/**
		 * @param $libraryId
		 */
		public static function clearData($libraryId)
		{
			self::model()->deleteAll('id_library=?', array($libraryId));
		}
	}

<?php

	/**
	 * @property mixed id
	 * @property mixed id_user
	 * @property mixed id_group
	 */
	class UserGroupRecord extends CActiveRecord
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
			return '{{user_group}}';
		}

		/**
		 * @param $groupId
		 * @param $users UserViewModel[]
		 */
		public static function assignUsersForGroup($groupId, $users)
		{
			self::clearObjectsByGroup($groupId);
			foreach ($users as $user)
			{
				$userGroupRecord = new UserGroupRecord();
				$userGroupRecord->id = uniqid();
				$userGroupRecord->id_user = $user->id;
				$userGroupRecord->id_group = $groupId;
				$userGroupRecord->save();
			}
		}

		/**
		 * @param $login
		 * @param $groups GroupViewModel[]
		 */
		public static function assignGroupsForUser($login, $groups)
		{
			/** @var $userRecord UserRecord */
			$userRecord = UserRecord::model()->find('LOWER(login)=?', array(strtolower($login)));
			if (isset($userRecord))
			{
				self::clearObjectsByUser($userRecord->id);
				foreach ($groups as $group)
				{
					$userGroupRecord = new UserGroupRecord();
					$userGroupRecord->id = uniqid();
					$userGroupRecord->id_user = $userRecord->id;
					$userGroupRecord->id_group = $group->id;
					$userGroupRecord->save();
				}
			}
		}

		/**
		 * @param $userId
		 * @return array
		 */
		public static function getGroupIdsByUser($userId)
		{
			$userGroupRecords = self::model()->findAll('id_user=?', array($userId));
			if (isset($userGroupRecords))
				foreach ($userGroupRecords as $userGroupRecord)
					$groupIds[] = $userGroupRecord->id_group;
			if (isset($groupIds))
				return array_unique($groupIds);
			return array();
		}

		/**
		 * @param $userId
		 * @return array
		 */
		public static function getGroupNamesByUser($userId)
		{
			$result = array();
			$groupIds = array();
			$userGroupRecords = self::model()->findAll('id_user=?', array($userId));
			foreach ($userGroupRecords as $userGroupRecord)
				$groupIds[] = $userGroupRecord->id_group;
			foreach ($groupIds as $groupId)
			{
				/** @var $groupRecord GroupRecord */
				$groupRecord = GroupRecord::model()->findByPk($groupId);
				if (isset($groupRecord))
					$result[] = $groupRecord->name;
			}
			return array_unique($result);
		}

		/**
		 * @param $groupId
		 * @return array
		 */
		public static function getUserIdsByGroup($groupId)
		{
			$userGroupRecords = self::model()->findAll('id_group=?', array($groupId));
			if (isset($userGroupRecords))
				foreach ($userGroupRecords as $userGroupRecord)
					$userIds[] = $userGroupRecord->id_user;
			if (isset($userIds))
				return array_unique($userIds);
			return array();
		}

		/**
		 * @param $userId
		 */
		public static function clearObjectsByUser($userId)
		{
			self::model()->deleteAll('id_user=?', array($userId));
		}

		/**
		 * @param $groupId
		 */
		public static function clearObjectsByGroup($groupId)
		{
			self::model()->deleteAll('id_group=?', array($groupId));
		}

	}

<?php

	class UserGroupStorage extends CActiveRecord
	{
		public static function model($className = __CLASS__)
		{
			return parent::model($className);
		}

		public function tableName()
		{
			return '{{user_group}}';
		}

		public static function assignUsersForGroup($groupId, $users)
		{
			self::clearObjectsByGroup($groupId);
			foreach ($users as $user)
			{
				$userGroupRecord = new UserGroupStorage();
				$userGroupRecord->id = uniqid();
				$userGroupRecord->id_user = $user->id;
				$userGroupRecord->id_group = $groupId;
				$userGroupRecord->save();
			}
		}

		public static function assignGroupsForUser($login, $groups)
		{
			$userRecord = UserStorage::model()->find('LOWER(login)=?', array(strtolower($login)));
			if (isset($userRecord))
			{
				self::clearObjectsByUser($userRecord->id);
				foreach ($groups as $group)
				{
					$userGroupRecord = new UserGroupStorage();
					$userGroupRecord->id = uniqid();
					$userGroupRecord->id_user = $userRecord->id;
					$userGroupRecord->id_group = $group->id;
					$userGroupRecord->save();
				}
			}
		}

		public static function getGroupIdsByUser($userId)
		{
			$userGroupRecords = self::model()->findAll('id_user=?', array($userId));
			if (isset($userGroupRecords))
				foreach ($userGroupRecords as $userGroupRecord)
					$groupIds[] = $userGroupRecord->id_group;
			if (isset($groupIds))
				return array_unique($groupIds);
			return null;
		}

		public static function getGroupNamesByUser($userId)
		{
			$result = array();
			$groupIds = array();
			$userGroupRecords = self::model()->findAll('id_user=?', array($userId));
			foreach ($userGroupRecords as $userGroupRecord)
				$groupIds[] = $userGroupRecord->id_group;
			foreach ($groupIds as $groupId)
			{
				$groupRecord = GroupStorage::model()->findByPk($groupId);
				if (isset($groupRecord))
					$result[] = $groupRecord->name;
			}
			return array_unique($result);
		}

		public static function getUserIdsByGroup($groupId)
		{
			$userGroupRecords = self::model()->findAll('id_group=?', array($groupId));
			if (isset($userGroupRecords))
				foreach ($userGroupRecords as $userGroupRecord)
					$userIds[] = $userGroupRecord->id_user;
			if (isset($userIds))
				return array_unique($userIds);
			return null;
		}

		public static function clearObjectsByUser($userId)
		{
			self::model()->deleteAll('id_user=?', array($userId));
		}

		public static function clearObjectsByGroup($groupId)
		{
			self::model()->deleteAll('id_group=?', array($groupId));
		}

	}

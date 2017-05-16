<?php

	/**
	 * Class UserLibraryRecord
	 * @property mixed id
	 * @property mixed id_user
	 * @property mixed id_library
	 * @property mixed id_page
	 */
	class UserLibraryRecord extends CActiveRecord
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
			return '{{user_library}}';
		}

		/**
		 * @param $login
		 * @param $assignedPages
		 */
		public static function assignPagesForUser($login, $assignedPages)
		{
			/** @var $userRecord userRecord */
			$userRecord = UserRecord::model()->find('LOWER(login)=?', array(strtolower($login)));
			if (isset($userRecord))
			{
				self::clearObjectsByUser($userRecord->id);
				foreach ($assignedPages as $page)
				{
					$userLibraryRecord = new UserLibraryRecord();
					$userLibraryRecord->id = uniqid();
					$userLibraryRecord->id_user = $userRecord->id;
					$userLibraryRecord->id_library = $page->libraryId;
					$userLibraryRecord->id_page = $page->id;
					$userLibraryRecord->save();
				}
			}
		}

		/**
		 * @param $page
		 * @param $users
		 */
		public static function assignUsersForPage($page, $users)
		{
			self::clearObjectsByPage($page->id);
			foreach ($users as $user)
			{
				$userLibraryRecord = new UserLibraryRecord();
				$userLibraryRecord->id = uniqid();
				$userLibraryRecord->id_user = $user->id;
				$userLibraryRecord->id_library = $page->libraryId;
				$userLibraryRecord->id_page = $page->id;
				$userLibraryRecord->save();
			}
		}

		/**
		 * @param $userId
		 * @return array
		 */
		public static function getLibraryIdsByUser($userId)
		{
			/** @var UserLibraryRecord[] $userLibraryRecords */
			$userLibraryRecords = self::model()->findAll('id_user=?', array($userId));
			if (isset($userLibraryRecords))
				foreach ($userLibraryRecords as $userLibraryRecord)
					$libraryIds[] = $userLibraryRecord->id_library;
			if (isset($libraryIds))
				return array_unique($libraryIds);
			return array();
		}

		/**
		 * @param $userId
		 * @return array
		 */
		public static function getLibraryIdsByUserAngHisGroups($userId)
		{
			$libraryIds = array();
			/** @var UserLibraryRecord[] $userLibraryRecords */
			$userLibraryRecords = self::model()->findAll('id_user=?', array($userId));
			if (isset($userLibraryRecords))
				foreach ($userLibraryRecords as $userLibraryRecord)
					$libraryIds[] = $userLibraryRecord->id_library;

			/** @var UserGroupRecord[] $userGroupRecords */
			$userGroupRecords = UserGroupRecord::model()->findAll('id_user=?', array($userId));
			if (isset($userGroupRecords))
				foreach ($userGroupRecords as $userGroupRecord)
				{
					$libraryIdsByGroup = GroupLibraryRecord::getLibraryIdsByGroup($userGroupRecord->id_group);
					if (isset($libraryIdsByGroup))
					{
						if (isset($libraryIds))
							$libraryIds = array_merge($libraryIds, $libraryIdsByGroup);
						else
							$libraryIds = $libraryIdsByGroup;
					}
				}

			return array_unique($libraryIds);
		}

		/**
		 * @param $userId
		 * @return array
		 */
		public static function getPageIdsByUser($userId)
		{
			/** @var UserLibraryRecord[] $userLibraryRecords */
			$userLibraryRecords = self::model()->findAll('id_user=?', array($userId));
			if (isset($userLibraryRecords))
				foreach ($userLibraryRecords as $userLibraryRecord)
					$pageIds[] = $userLibraryRecord->id_page;
			if (isset($pageIds))
				return array_unique($pageIds);
			return array();
		}

		/**
		 * @param $userId
		 * @return array
		 */
		public static function getPageIdsByUserAngHisGroups($userId)
		{
			/** @var UserLibraryRecord[] $userLibraryRecords */
			$userLibraryRecords = self::model()->findAll('id_user=?', array($userId));
			if (isset($userLibraryRecords))
				foreach ($userLibraryRecords as $userLibraryRecord)
					$pageIds[] = $userLibraryRecord->id_page;

			/** @var UserGroupRecord[] $userGroupRecords */
			$userGroupRecords = UserGroupRecord::model()->findAll('id_user=?', array($userId));
			if (isset($userGroupRecords))
				foreach ($userGroupRecords as $userGroupRecord)
				{
					$pageIdsByGroup = GroupLibraryRecord::getPageIdsByGroup($userGroupRecord->id_group);
					if (isset($pageIdsByGroup))
					{
						if (isset($pageIds))
							$pageIds = array_merge($pageIds, $pageIdsByGroup);
						else
							$pageIds = $pageIdsByGroup;
					}
				}

			if (isset($pageIds))
				return array_unique($pageIds);
			return array();
		}

		/**
		 * @param $pageId
		 * @return array
		 */
		public static function getUserIdsByPage($pageId)
		{
			/** @var UserLibraryRecord[] $userLibraryRecords */
			$userLibraryRecords = self::model()->findAll('id_page=?', array($pageId));
			if (isset($userLibraryRecords))
				foreach ($userLibraryRecords as $userLibraryRecord)
					$userIds[] = $userLibraryRecord->id_user;
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
		 * @param $pageId
		 */
		public static function clearObjectsByPage($pageId)
		{
			self::model()->deleteAll('id_page=?', array($pageId));
		}

	}

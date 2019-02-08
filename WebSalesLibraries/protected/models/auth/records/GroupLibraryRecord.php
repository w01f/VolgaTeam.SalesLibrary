<?php

	/**
	 * Class GroupLibraryRecord
	 * @property mixed id
	 * @property mixed id_group
	 * @property mixed id_library
	 * @property mixed id_page
	 */
	class GroupLibraryRecord extends CActiveRecord
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
			return '{{group_library}}';
		}

		/**
		 * @param $groupId
		 * @param $assignedPages LibraryPageViewModel[]
		 */
		public static function assignPagesForGroup($groupId, $assignedPages)
		{
			self::clearObjectsByGroup($groupId);
			foreach ($assignedPages as $page)
			{
				$groupLibraryRecord = new GroupLibraryRecord();
				$groupLibraryRecord->id = uniqid();
				$groupLibraryRecord->id_group = $groupId;
				$groupLibraryRecord->id_library = $page->libraryId;
				$groupLibraryRecord->id_page = $page->id;
				$groupLibraryRecord->save();
			}
		}

		/**
		 * @param $page
		 * @param $groups GroupViewModel[]
		 */
		public static function assignGroupsForPage($page, $groups)
		{
			self::clearObjectsByPage($page->id);
			foreach ($groups as $group)
			{
				$groupLibraryRecord = new GroupLibraryRecord();
				$groupLibraryRecord->id = uniqid();
				$groupLibraryRecord->id_group = $group->id;
				$groupLibraryRecord->id_library = $page->libraryId;
				$groupLibraryRecord->id_page = $page->id;
				$groupLibraryRecord->save();
			}
		}

		/**
		 * @param $pageId
		 * @return array
		 */
		public static function getGroupIdsByPage($pageId)
		{
			$groupLibraryRecords = self::model()->findAll('id_page=?', array($pageId));
			if (isset($groupLibraryRecords))
				foreach ($groupLibraryRecords as $groupLibraryRecord)
					$groupIds[] = $groupLibraryRecord->id_group;
			if (isset($groupIds))
				return array_unique($groupIds);
			return array();
		}

		/**
		 * @param $groupId
		 * @return array
		 */
		public static function getLibraryIdsByGroup($groupId)
		{
			$groupLibraryRecords = self::model()->findAll('id_group=?', array($groupId));
			if (isset($groupLibraryRecords))
				foreach ($groupLibraryRecords as $groupLibraryRecord)
					$libraryIds[] = $groupLibraryRecord->id_library;
			if (isset($libraryIds))
				return array_unique($libraryIds);
			return array();
		}

		/**
		 * @param $groupId
		 * @return array
		 */
		public static function getPageIdsByGroup($groupId)
		{
			$groupLibraryRecords = self::model()->findAll('id_group=?', array($groupId));
			if (isset($groupLibraryRecords))
				foreach ($groupLibraryRecords as $groupLibraryRecord)
					$pageIds[] = $groupLibraryRecord->id_page;
			if (isset($pageIds))
				return array_unique($pageIds);
			return array();
		}

		/**
		 * @param $groupId
		 */
		public static function clearObjectsByGroup($groupId)
		{
			self::model()->deleteAll('id_group=?', array($groupId));
		}

		/**
		 * @param $pageId
		 */
		public static function clearObjectsByPage($pageId)
		{
			self::model()->deleteAll('id_page=?', array($pageId));
		}

	}

<?php

	/**
	 * Class UserPageCacheRecord
	 * @property mixed id
	 * @property mixed id_user
	 * @property mixed id_page
	 * @property mixed id_library
	 */
	class UserPageCacheRecord extends CActiveRecord
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
			return '{{user_page_cache}}';
		}

		/**
		 * @param $userId
		 * @param $pageId
		 * @param $libraryId
		 */
		public static function updateData($userId, $pageId, $libraryId)
		{
			$userLinkRecord = new UserPageCacheRecord();
			$userLinkRecord->id = uniqid();
			$userLinkRecord->id_user = $userId;
			$userLinkRecord->id_page = $pageId;
			$userLinkRecord->id_library = $libraryId;
			$userLinkRecord->save();
		}

		/**
		 * @param $userId
		 * @param $pageId
		 * @return CActiveRecord|null
		 */
		public static function getPageCache($userId, $pageId)
		{
			$pageCache = self::model()->find('id_user=? and id_page=?', array($userId, $pageId));
			return isset($pageCache) ? $pageCache : null;
		}

		/**
		 * @param $libraryId
		 */
		public static function clearData($libraryId)
		{
			self::model()->deleteAll('id_library=?', array($libraryId));
		}
	}

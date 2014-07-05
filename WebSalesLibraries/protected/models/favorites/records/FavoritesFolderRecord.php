<?php

	/**
	 * Class FavoritesFolderRecord
	 * @property mixed id
	 * @property mixed id_parent_folder
	 * @property mixed id_user
	 * @property mixed name
	 */
	class FavoritesFolderRecord extends CActiveRecord
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
			return '{{favorites_folder}}';
		}

		/**
		 * @param $userId
		 * @param $folderName
		 * @return CActiveRecord|FavoritesFolderRecord
		 */
		public static function getFolderByName($userId, $folderName)
		{
			$existedFolder = self::model()->find('id_user=? and LOWER(name)=?', array($userId, strtolower($folderName)));
			if (!isset($existedFolder))
				$existedFolder = self::addFolder($userId, $folderName, null);
			return $existedFolder;
		}

		/**
		 * @param $folderId
		 * @return FavoritesFolder|null
		 */
		public static function getFolderById($folderId)
		{
			if (isset($folderId))
			{
				/** @var $folderRecord FavoritesFolderRecord */
				$folderRecord = self::model()->findByPk($folderId);
				if (isset($folderRecord))
				{
					$folder = new FavoritesFolder();
					$folder->id = $folderRecord->id;
					$folder->name = $folderRecord->name;
					return $folder;
				}
			}
			return null;
		}

		/**
		 * @param $userId
		 * @param $folderName
		 * @param $parentFolderId
		 * @return FavoritesFolderRecord
		 */
		public static function addFolder($userId, $folderName, $parentFolderId)
		{
			$folder = new FavoritesFolderRecord();
			$folder->id = uniqid();
			if (isset($parentFolderId))
				$folder->id_parent_folder = $parentFolderId;
			$folder->id_user = $userId;
			$folder->name = $folderName;
			$folder->save();
			return $folder;
		}

		/**
		 * @param $userId
		 * @return array
		 */
		public static function getAllFolderNames($userId)
		{
			$names = array();
			$folderRecords = self::model()->findAll('id_user=?', array($userId));
			if (isset($folderRecords))
				foreach ($folderRecords as $folderRecord)
					$names[] = $folderRecord->name;
			return array_unique($names);
		}

		/**
		 * @param $userId
		 * @return FavoritesFolder
		 */
		public static function getRootFolder($userId)
		{
			$rootFolder = new FavoritesFolder();
			$rootFolder->name = 'Favorites';
			$rootFolder->childFolders = self::getChildFolders($userId, null);
			return $rootFolder;
		}

		/**
		 * @param $userId
		 * @param $parentFolderId
		 * @return FavoritesFolder[]
		 */
		public static function getChildFolders($userId, $parentFolderId)
		{
			$folders = array();
			if (isset($parentFolderId))
				$folderRecords = self::model()->findAll('id_user=? and id_parent_folder=?', array($userId, $parentFolderId));
			else
				$folderRecords = self::model()->findAll('id_user=? and id_parent_folder is null', array($userId));
			if (isset($folderRecords) && count($folderRecords) > 0)
			{
				foreach ($folderRecords as $folderRecord)
				{
					$folder = new FavoritesFolder();
					$folder->id = $folderRecord->id;
					$folder->name = $folderRecord->name;
					$childFolders = self::getChildFolders($userId, $folderRecord->id);
					if (isset($childFolders) && count($childFolders) > 0)
						$folder->childFolders = $childFolders;
					$folders[] = $folder;
				}
			}
			return $folders;
		}

		/**
		 * @param $folderId
		 * @param $parentId
		 */
		public static function putFolderToFolder($folderId, $parentId)
		{
			/** @var $folderRecord FavoritesFolderRecord */
			$folderRecord = self::model()->findByPk($folderId);
			if (isset($folderId))
			{
				$folderRecord->id_parent_folder = $parentId;
				$folderRecord->save();
				if (!self::validateFoldersChain($folderId, $parentId))
				{
					/** @var $parentRecord FavoritesFolderRecord */
					$parentRecord = self::model()->findByPk($parentId);
					$parentRecord->id_parent_folder = null;
					$parentRecord->save();
				}
			}
		}

		/**
		 * @param $folderId
		 * @param $parentId
		 * @return bool
		 */
		public static function validateFoldersChain($folderId, $parentId)
		{
			/** @var $parentRecord FavoritesFolderRecord */
			$parentRecord = self::model()->findByPk($parentId);
			if (isset($parentRecord))
			{
				if ($parentRecord->id_parent_folder == $folderId)
					return false;
				else
					return self::validateFoldersChain($folderId, $parentRecord->id_parent_folder);
			}
			return true;
		}

		/**
		 * @param $folderId
		 */
		public static function deleteFolder($folderId)
		{
			foreach (self::model()->findAll('id_parent_folder=?', array($folderId)) as $childFolderRecord)
				self::deleteFolder($childFolderRecord->id);
			FavoritesLinkRecord::clearByFolder($folderId);
			self::model()->deleteByPk($folderId);
		}

		public static function clearAll()
		{
			self::model()->deleteAll();
		}

		/**
		 * @param $userId
		 */
		public static function clearByUser($userId)
		{
			self::model()->deleteAll('id_user=?', array($userId));
		}
	}

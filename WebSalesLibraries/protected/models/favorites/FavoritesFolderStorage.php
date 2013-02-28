<?php
	class FavoritesFolderStorage extends CActiveRecord
	{
		public static function model($className = __CLASS__)
		{
			return parent::model($className);
		}

		public function tableName()
		{
			return '{{favorites_folder}}';
		}

		public static function getFolder($userId, $folderName)
		{
			$existedFolder = self::model()->find('id_user=? and LOWER(name)=?', array($userId, strtolower($folderName)));
			if (!isset($existedFolder))
				$existedFolder = self::addFolder($userId, $folderName, null);
			return $existedFolder;
		}

		public static function addFolder($userId, $folderName, $parentFolderId)
		{
			$folder = new FavoritesFolderStorage();
			$folder->id = uniqid();
			if (isset($parentFolderId))
				$folder->id_parent_folder = $parentFolderId;
			$folder->id_user = $userId;
			$folder->name = $folderName;
			$folder->save();
			return $folder;
		}

		public static function getAllFolderNames($userId)
		{
			$folderRecords = self::model()->findAll('id_user=?', array($userId));
			if (isset($folderRecords))
				foreach ($folderRecords as $folderRecord)
					$names[] = $folderRecord->name;
			if (isset($names))
				return array_unique($names);
			else
				return null;
		}

		public static function getRootFolder($userId)
		{
			$rootFolder = new FavoritesFolder();
			$rootFolder->name = 'Favorites';
			$rootFolder->childFolders = self::getChildFolders($userId, null);
			return $rootFolder;
		}

		public static function getChildFolders($userId, $parentFolderId)
		{
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
				return $folders;
			}
			return null;
		}

		public static function putFolderToFolder($folderId, $parentId)
		{
			$folderRecord = self::model()->findByPk($folderId);
			if (isset($folderId))
			{
				$folderRecord->id_parent_folder = $parentId;
				$folderRecord->save();
				if (!self::validateFoldersChain($folderId, $parentId))
				{
					$parentRecord = self::model()->findByPk($parentId);
					$parentRecord->id_parent_folder = null;
					$parentRecord->save();
				}
			}
		}

		public static function validateFoldersChain($folderId, $parentId)
		{
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

		public static function deleteFolder($folderId)
		{
			foreach (self::model()->findAll('id_parent_folder=?', array($folderId)) as $childFolderRecord)
				self::deleteFolder($childFolderRecord->id);
			FavoritesLinkStorage::clearByFolder($folderId);
			self::model()->deleteByPk($folderId);
		}

		public static function clearAll()
		{
			self::model()->deleteAll();
		}

		public static function clearByUser($userId)
		{
			self::model()->deleteAll('id_user=?', array($userId));
		}
	}

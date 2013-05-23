<?php
	class FavoritesController extends IsdController
	{
		public function getViewPath()
		{
			return YiiBase::getPathOfAlias($this->pathPrefix . 'favorites');
		}

		public function actionGetFavoritesView()
		{
			$userId = Yii::app()->user->getId();
			if (isset($userId))
			{
				$rootFolder = FavoritesFolderStorage::getRootFolder($userId);
				$this->renderPartial('favoritesView', array('rootFolder' => $rootFolder), false, true);
			}
		}

		public function actionAddLinkDialog()
		{
			$linkId = Yii::app()->request->getPost('linkId');
			$userId = Yii::app()->user->getId();
			if (isset($userId) && isset($linkId))
			{
				$linkRecord = LinkStorage::getLinkById($linkId);
				$userFolderRecords = FavoritesFolderStorage::getAllFolderNames($userId);
				$this->renderPartial('addLinkDialog', array('link' => $linkRecord, 'folders' => $userFolderRecords), false, true);
				StatisticActivityStorage::WriteActivity('Link', 'Favorites', array('Name' => $linkRecord->name, 'File' => $linkRecord->file_name, 'Original Format' => $linkRecord->format));
			}
		}

		public function actionAddLink()
		{
			$linkId = Yii::app()->request->getPost('linkId');
			$linkName = Yii::app()->request->getPost('linkName');
			$userId = Yii::app()->user->getId();
			$folderName = Yii::app()->request->getPost('folderName');
			if (isset($folderName) && $folderName == '')
				$folderName = null;
			if (isset($userId) && isset($linkId) && isset($linkName))
			{
				$linkRecord = LinkStorage::getLinkById($linkId);
				StatisticActivityStorage::WriteActivity('Link', 'Add to Favorites', array('Name' => $linkRecord->name, 'File' => $linkRecord->file_name, 'Original Format' => $linkRecord->format, 'Favorites Folder' => $folderName, 'Favorites Name' => $linkName));
				FavoritesLinkStorage::addLink($userId, $linkId, $linkName, $folderName, $linkRecord->id_library);
				$this->renderPartial('successAddDialog', array('header' => 'SUCCESS!', 'content' => $linkRecord->file_name . ' was  saved to your favorites...'), false, true);
			}
		}

		public function actionGetFoldersAndLinks()
		{
			$folderId = Yii::app()->request->getPost('folderId');
			if (!isset($folderId) || (isset($folderId) && ($folderId == "" || $folderId == "null")))
				$folderId = null;
			$userId = Yii::app()->user->getId();
			if (isset($userId))
			{
				$parentFolder = FavoritesFolderStorage::getFolderById($folderId);
				$folders = FavoritesFolderStorage::getChildFolders($userId, $folderId);
				$links = FavoritesLinkStorage::getLinksByFolder($userId, $folderId, false);
				$this->renderPartial('favoritesLinksAndFolders', array('parentFolder' => $parentFolder, 'folders' => $folders, 'links' => $links), false, true);
			}
		}

		public function actionGetFoldersList()
		{
			$userId = Yii::app()->user->getId();
			if (isset($userId))
			{
				$userFolderRecords = FavoritesFolderStorage::getAllFolderNames($userId);
				$this->renderPartial('foldersList', array('folders' => $userFolderRecords), false, true);
			}
		}

		public function actionGetLinks()
		{
			$folderId = Yii::app()->request->getPost('folderId');
			if (!isset($folderId) || (isset($folderId) && ($folderId == "" || $folderId == "null")))
				$folderId = null;
			$isSort = intval(Yii::app()->request->getPost('isSort'));
			$userId = Yii::app()->user->getId();
			if (isset($userId) && isset($isSort))
			{
				$links = FavoritesLinkStorage::getLinksByFolder($userId, $folderId, $isSort);
				$this->renderPartial('favoritesLinks', array('links' => $links), false, true);
			}
		}

		public function actionPutFolderToFolder()
		{
			$folderId = Yii::app()->request->getPost('folderId');
			$parentId = Yii::app()->request->getPost('parentId');
			if (!isset($parentId) || (isset($parentId) && ($parentId == "" || $parentId == "null")))
				$parentId = null;
			$userId = Yii::app()->user->getId();
			if (isset($folderId) && isset($userId))
			{
				FavoritesFolderStorage::putFolderToFolder($folderId, $parentId);
				$rootFolder = FavoritesFolderStorage::getRootFolder($userId);
				$this->renderPartial('favoritesView', array('rootFolder' => $rootFolder, 'selectedFolderId' => $folderId), false, true);
			}
		}

		public function actionPutLinkToFolder()
		{
			$linkId = Yii::app()->request->getPost('linkId');
			$parentId = Yii::app()->request->getPost('newfolderId');
			if (!isset($parentId) || (isset($parentId) && ($parentId == "" || $parentId == "null")))
				$parentId = null;
			$oldParentId = Yii::app()->request->getPost('oldfolderId');
			if (!isset($oldParentId) || (isset($oldParentId) && ($oldParentId == "" || $oldParentId == "null")))
				$oldParentId = null;
			if (isset($linkId))
				FavoritesLinkStorage::putLinkToFolder($linkId, $parentId, $oldParentId);
		}

		public function actionDeleteLink()
		{
			$linkId = Yii::app()->request->getPost('linkId');
			$folderId = Yii::app()->request->getPost('folderId');
			if (!isset($folderId) || (isset($folderId) && ($folderId == "" || $folderId == "null")))
				$folderId = null;
			if (isset($linkId))
				FavoritesLinkStorage::deleteLink($linkId, $folderId);
			Yii::app()->end();
		}

		public function actionDeleteFolder()
		{
			$folderId = Yii::app()->request->getPost('folderId');
			if (isset($folderId))
				FavoritesFolderStorage::deleteFolder($folderId);
			Yii::app()->end();
		}
	}

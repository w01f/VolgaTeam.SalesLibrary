<?php

	/**
	 * Class FavoritesController
	 */
	class FavoritesController extends IsdController
	{
		public function getViewPath()
		{
			return YiiBase::getPathOfAlias($this->pathPrefix . 'favorites');
		}

		//------Common Site API-------------------------------------------
		public function actionAddLink()
		{
			$linkId = Yii::app()->request->getPost('linkId');
			$linkName = Yii::app()->request->getPost('linkName');
			$userId = UserIdentity::getCurrentUserId();
			$folderName = Yii::app()->request->getPost('folderName');
			if (isset($folderName) && $folderName == '')
				$folderName = null;

			$selectedFolderTagName = 'favorites-selected-folder-name';
			$cookie = new CHttpCookie($selectedFolderTagName, $folderName);
			$cookie->expire = time() + (60 * 60 * 24 * 7);
			Yii::app()->request->cookies[$selectedFolderTagName] = $cookie;

			if (isset($userId) && isset($linkId) && isset($linkName))
			{
				$linkRecord = LinkRecord::getLinkById($linkId);
				FavoritesLinkRecord::addLink($userId, $linkId, $linkName, $folderName, $linkRecord->id_library);
			}
			Yii::app()->end();
		}

		public function actionDeleteLink()
		{
			$linkId = Yii::app()->request->getPost('linkId');
			$folderId = Yii::app()->request->getPost('folderId');
			if (!isset($folderId) || (isset($folderId) && ($folderId == "" || $folderId == "null")))
				$folderId = null;
			if (isset($linkId))
				FavoritesLinkRecord::deleteLink($linkId, $folderId);
			Yii::app()->end();
		}

		public function actionDeleteFolder()
		{
			$folderId = Yii::app()->request->getPost('folderId');
			if (isset($folderId))
				FavoritesFolderRecord::deleteFolder($folderId);
			Yii::app()->end();
		}
		//------Common Site API-------------------------------------------

		//------Regular Site API-------------------------------------------
		public function actionAddLinkDialog()
		{
			$linkId = Yii::app()->request->getPost('linkId');
			$userId = UserIdentity::getCurrentUserId();
			if (isset($linkId))
			{
				$linkRecord = LinkRecord::getLinkById($linkId);
				$userFolderRecords = FavoritesFolderRecord::getAllFolderNames($userId);
				$this->renderPartial('addLinkDialog', array('link' => $linkRecord, 'folders' => $userFolderRecords), false, true);
			}
		}

		public function actionGetLinks()
		{
			$folderId = Yii::app()->request->getPost('folderId');
			if (!isset($folderId) || (isset($folderId) && ($folderId == "" || $folderId == "null")))
				$folderId = null;
			$userId = UserIdentity::getCurrentUserId();
			$columnSettings = TableColumnSettings::createEmpty();
			$links = FavoritesLinkRecord::getLinksByFolder($userId, $folderId, $columnSettings);
			echo CJSON::encode(array(
				'links' => $links,
				'viewOptions' => array(
					'columnSettings' => $columnSettings,
					'showDeleteButton' => true
				)
			));
		}

		public function actionPutFolderToFolder()
		{
			$folderId = Yii::app()->request->getPost('folderId');
			$parentId = Yii::app()->request->getPost('parentId');
			if (!isset($parentId) || (isset($parentId) && ($parentId == "" || $parentId == "null")))
				$parentId = null;
			$userId = UserIdentity::getCurrentUserId();
			if (isset($folderId))
			{
				FavoritesFolderRecord::putFolderToFolder($folderId, $parentId);
				$rootFolder = FavoritesFolderRecord::getRootFolder($userId);
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
				FavoritesLinkRecord::putLinkToFolder($linkId, $parentId, $oldParentId);
		}
		//------Regular Site API-------------------------------------------

		//------Mobile Site API-----------------------------------------------
		public function actionGetFoldersAndLinks()
		{
			$folderId = Yii::app()->request->getPost('folderId');
			if (!isset($folderId) || (isset($folderId) && ($folderId == "" || $folderId == "null")))
				$folderId = null;
			$userId = UserIdentity::getCurrentUserId();
			$folders = FavoritesFolderRecord::getChildFolders($userId, $folderId);
			$columnSettings = TableColumnSettings::createEmpty();
			$links = FavoritesLinkRecord::getLinksByFolder($userId, $folderId, $columnSettings);
			$this->renderPartial('foldersAndLinks', array('folders' => $folders, 'links' => $links, 'topLevel' => false), false, true);
		}
		//------Mobile Site API-----------------------------------------------
	}

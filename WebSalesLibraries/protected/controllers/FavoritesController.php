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
				FavoritesLinkStorage::addLink($userId, $linkId, $linkName, $folderName, $linkRecord->id_library);
				$this->renderPartial('application.views.regular.site.successDialog', array('header' => 'Add to Favorites', 'content' => 'Link was successfully added to Favorites list'), false, true);
			}
		}

		public function actionGetLinks()
		{
			$folderId = Yii::app()->request->getPost('folderId');
			if (!isset($folderId) || (isset($folderId) && ($folderId=="" || $folderId=="null")))
				$folderId = null;
			$isSort = intval(Yii::app()->request->getPost('isSort'));
			$userId = Yii::app()->user->getId();
			if (isset($userId) && isset($isSort))
			{
				$links = FavoritesLinkStorage::getLinksByFolder($userId, $folderId, $isSort);
				$this->renderPartial('favoritesLinks', array('links' => $links), false, true);
			}
		}
	}

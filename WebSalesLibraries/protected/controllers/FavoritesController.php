<?php
	class FavoritesController extends IsdController
	{
		public function getViewPath()
		{
			return YiiBase::getPathOfAlias($this->pathPrefix . 'favorites');
		}

		public function actionGetFavoritesView()
		{
			$this->renderPartial('favoritesView', array(), false, true);
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
			$folderName = Yii::app()->request->getPost('folderName');
			$userId = Yii::app()->user->getId();
			if (isset($userId) && isset($linkId) && isset($linkName))
			{
				$linkRecord = LinkStorage::getLinkById($linkId);
				FavoritesLinkStorage::addLink($userId, $linkId, $linkName, $folderName, $linkRecord->id_library);
				$this->renderPartial('application.views.regular.site.successDialog', array('header' => 'Add to Favorites', 'content' => 'Link was successfully added to Favorites list'), false, true);
			}
		}
	}

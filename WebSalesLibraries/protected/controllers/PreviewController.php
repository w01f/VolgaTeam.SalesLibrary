<?php

	/**
	 * Class PreviewController
	 */
	class PreviewController extends IsdController
	{
		public function getViewPath()
		{
			return YiiBase::getPathOfAlias($this->pathPrefix . 'preview');
		}

		public function actionGetViewDialog()
		{
			$linkId = Yii::app()->request->getPost('linkId');
			if (strtolower(trim(Yii::app()->request->getPost('isQuickSite'))) == 'true')
				$isQuickSite = true;
			else
				$isQuickSite = false;
			$dialogData = array();
			if (isset($linkId))
			{
				$linkRecord = LinkRecord::getLinkById($linkId);
				if (isset($linkRecord))
				{
					$libraryManager = new LibraryManager();
					$library = $libraryManager->getLibraryById($linkRecord->id_library);
					$link = new LibraryLink(new LibraryFolder(new LibraryPage($library)));
					$link->load($linkRecord);

					$previewData = $link->getPreviewData($isQuickSite);

					if ($previewData->userAuthorized)
						StatisticActivityRecord::WriteActivity('Link', 'Preview Options', array('Name' => $link->fileName, 'File' => $link->fileName));

					$dialogData = array(
						'format' => $previewData->viewerFormat,
						'data' => CJSON::encode($previewData),
						'content' => $this->renderPartial($previewData->contentView, array('data' => $previewData,), true),
					);
				}
			}
			echo CJSON::encode($dialogData);
			Yii::app()->end();
		}

		public function actionGetSpecialDialog()
		{
			$folderId = Yii::app()->request->getPost('folderId');
			if (isset($folderId))
			{
				$folderRecord = FolderRecord::model()->findByPk($folderId);
				if (isset($folderRecord))
				{
					$this->renderPartial('specialDialog', array('object' => $folderRecord), false, true);
				}
			}
		}

		public function actionGetLinkPreviewList()
		{
			$linkId = Yii::app()->request->getPost('linkId');
			if (strtolower(trim(Yii::app()->request->getPost('isQuickSite'))) == 'true')
				$isQuickSite = true;
			else
				$isQuickSite = false;
			if (isset($linkId))
			{
				$linkRecord = LinkRecord::getLinkById($linkId);
				if (isset($linkRecord))
				{
					$authorized = false;
					if (isset(Yii::app()->user))
					{
						$userId = Yii::app()->user->getId();
						$authorized = isset($userId);
					}
					$libraryManager = new LibraryManager();
					$library = $libraryManager->getLibraryById($linkRecord->id_library);
					$link = new LibraryLink(new LibraryFolder(new LibraryPage($library)));
					$link->load($linkRecord);
					$this->renderPartial('linkPreview', array('link' => $link, 'authorized' => $authorized, 'isQuickSite' => $isQuickSite), false, true);
				}
			}
		}

		public function actionRunFullScreenGallery()
		{
			$linkId = Yii::app()->request->getQuery('linkId');
			$format = Yii::app()->request->getQuery('format');
			if (isset($linkId) && isset($format))
			{
				$linkRecord = LinkRecord::getLinkById($linkId);
				if (isset($linkRecord))
				{
					$libraryManager = new LibraryManager();
					$libraryManager->getLibraries();
					$library = $libraryManager->getLibraryById($linkRecord->id_library);
					if (isset($library))
					{
						$link = new LibraryLink(new LibraryFolder(new LibraryPage($library)));
						$link->load($linkRecord);

						/** @var  $previewData GalleryPreviewData */
						$previewData = $link->getPreviewData(false);
						$this->pageTitle = Yii::app()->name . ' - Fullscreen Gallery';
						$this->render('fullScreenGallery', array('previewData' => $previewData, 'format' => $format));
					}
				}
			}
			Yii::app()->end();
		}

		public function actionGetBar()
		{
			$this->renderPartial('bar', array(), false, true);
		}

		public function actionDownloadFile()
		{
			$data = CJSON::decode(Yii::app()->request->getQuery('data'), false);
			Yii::app()->getRequest()->sendFile($data->name, @file_get_contents($data->path));
			Yii::app()->end();
		}
	}

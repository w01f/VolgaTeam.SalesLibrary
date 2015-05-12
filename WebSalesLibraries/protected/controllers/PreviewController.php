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
			$rendered = false;
			$userId = Yii::app()->user->getId();
			if (isset($userId))
			{
				$linkIds = Yii::app()->request->getPost('linkIds');
				$folderId = Yii::app()->request->getPost('folderId');
				$isSearchResults = Yii::app()->request->getPost('isSearchResults');
				if (isset($folderId))
				{
					$folderRecord = FolderRecord::model()->findByPk($folderId);
					if (isset($folderRecord))
					{
						$this->renderPartial('specialDialog', array('object' => $folderRecord, 'isLink' => false, 'isLineBreak' => false), false, true);
						$rendered = true;
					}
				}
				else if (isset($isSearchResults) && isset($linkIds))
				{
					$searchResults = new stdClass();
					$searchResults->id = implode(',', $linkIds);
					$searchResults->name = 'Search Results: ' . count($linkIds);
					$this->renderPartial('specialDialog', array('object' => $searchResults, 'isLink' => false, 'isLineBreak' => false, 'isSearchResult' => true), false, true);
					$rendered = true;
				}
				if (!$rendered)
					$this->renderPartial('empty', array(), false, true);
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
			$format = Yii::app()->request->getPost('format');
			switch ($format)
			{
				case 'ppt':
				case 'doc':
				case 'pdf':
					$this->renderPartial('documentBar', array('format' => $format), false, true);
					break;
				case 'mp4':
				case 'wmv':
				case 'video':
					$this->renderPartial('videoBar', array('format' => $format), false, true);
					break;
				default:
					Yii::app()->end();
					break;
			}
		}

		public function actionDownloadFile()
		{
			$data = CJSON::decode(Yii::app()->request->getQuery('data'), false);
			Yii::app()->getRequest()->sendFile($data->name, @file_get_contents($data->path));
			Yii::app()->end();
		}
	}

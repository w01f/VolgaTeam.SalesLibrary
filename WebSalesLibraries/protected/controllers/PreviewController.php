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
			$authorized = false;
			$dialogData = array();
			$userId = -1;
			if (isset(Yii::app()->user))
			{
				$userId = Yii::app()->user->getId();
				$authorized = isset($userId);
			}
			if (isset($linkId))
			{
				if (Yii::app()->browser->isMobile())
					$browser = 'mobile';
				else
				{
					$browser = Yii::app()->browser->getBrowser();
					switch ($browser)
					{
						case 'Internet Explorer':
							$browser = 'ie';
							break;
						case 'Chrome':
						case 'Safari':
							$browser = 'webkit';
							break;
						case 'Firefox':
							$browser = 'firefox';
							break;
						case 'Opera':
							$browser = 'opera';
							break;
						default:
							$browser = 'webkit';
							break;
					}
				}
				$linkRecord = LinkRecord::getLinkById($linkId);
				if (isset($linkRecord))
				{
					$libraryManager = new LibraryManager();
					$library = $libraryManager->getLibraryById($linkRecord->id_library);
					$link = new LibraryLink(new LibraryFolder(new LibraryPage($library)));
					$link->browser = $browser;
					$link->load($linkRecord);
					if ($authorized)
						StatisticActivityRecord::WriteActivity('Link', 'Preview Options', array('Name' => $link->fileName, 'File' => $link->fileName));
					$dialogData = array(
						'html' => $this->renderPartial('viewDialog', array('link' => $link, 'authorized' => $authorized, 'isQuickSite' => $isQuickSite), true),
						'rateData' => LinkRateRecord::getRateData($linkId, $userId)
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
					$link->browser = 'phone';
					$link->load($linkRecord);
					$this->renderPartial('linkPreview', array('link' => $link, 'authorized' => $authorized, 'isQuickSite' => $isQuickSite), false, true);
				}
			}
		}

		public function actionRunFullscreenGallery()
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
						$link->browser = 'default';
						$link->load($linkRecord);
						$selectedLinks = $link->getViewSource($format);
						$selectedThumbs = $link->getViewSource('thumbs');
					}
				}
			}
			if (isset($selectedLinks) && isset($selectedThumbs))
			{
				for ($i = 0; $i < count($selectedLinks); $i++)
				{
					$galleryLink = array('image' => $selectedLinks[$i]['href'],
						'title' => $selectedLinks[$i]['title'],
						'thumb' => $selectedThumbs[$i]['href']);
					$galleryLinks[] = $galleryLink;
				}
				if (isset($galleryLinks))
				{
					$this->pageTitle = Yii::app()->name . ' - Fullscreen Gallery';
					$this->render('fullscreenGallery', array('selectedLinks' => $galleryLinks));
				}
			}
		}

		public function actionGetViewDialogBar()
		{
			$format = Yii::app()->request->getPost('format');
			$this->renderPartial('viewDialogBar', array('format' => $format), false, true);
		}
	}

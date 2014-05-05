<?php
	class PreviewController extends IsdController
	{
		public function getViewPath()
		{
			return YiiBase::getPathOfAlias($this->pathPrefix . 'preview');
		}

		public function actionGetViewDialog()
		{
			$rendered = false;
			$linkId = Yii::app()->request->getPost('linkId');
			$isAttachment = Yii::app()->request->getPost('isAttachment');
			$authorized = false;
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

				if (isset($isAttachment) && $isAttachment == 'true')
				{
					$attachmentRecord = AttachmentStorage::getAttachmentById($linkId);
					if (isset($attachmentRecord))
					{
						$linkRecord = LinkStorage::getLinkById($attachmentRecord->id_link);
						$libraryManager = new LibraryManager();
						$libraryManager->getLibraries();
						$library = $libraryManager->getLibraryById($attachmentRecord->id_library);
						if (isset($library) && isset($linkRecord))
						{
							$link = new LibraryLink(new LibraryFolder(new LibraryPage($library)));
							$link->browser = $browser;
							$link->load($linkRecord);
							$attachment = new Attachment($link);
							$attachment->browser = $browser;
							$attachment->load($attachmentRecord);
							if ($authorized)
								StatisticActivityStorage::WriteActivity('Link', 'Preview Options', array('Name' => $attachment->name, 'File' => basename($attachment->path)));
							$this->renderPartial('viewDialog', array('link' => $attachment, 'authorized' => $authorized), false, true);
							$rendered = true;
						}
					}
				}
				else
				{
					$linkRecord = LinkStorage::getLinkById($linkId);
					if (isset($linkRecord))
					{
						$libraryManager = new LibraryManager();
						$library = $libraryManager->getLibraryById($linkRecord->id_library);
						$link = new LibraryLink(new LibraryFolder(new LibraryPage($library)));
						$link->browser = $browser;
						$link->load($linkRecord);
						if ($authorized)
							StatisticActivityStorage::WriteActivity('Link', 'Preview Options', array('Name' => $link->name, 'File' => $link->fileName));
						$this->renderPartial('viewDialog', array('link' => $link, 'authorized' => $authorized), false, true);
						$rendered = true;
					}
				}
			}
			if (!$rendered)
				$this->renderPartial('empty', array(), false, true);
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
				if (isset($linkIds) && !isset($isSearchResults))
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
					$linkRecord = count($linkIds) > 0 ? LinkStorage::getLinkById($linkIds[0]) : null;
					if (isset($linkRecord))
					{
						$libraryManager = new LibraryManager();
						$library = $libraryManager->getLibraryById($linkRecord->id_library);
						$link = new LibraryLink(new LibraryFolder(new LibraryPage($library)));
						$link->browser = $browser;
						$link->load($linkRecord);
						StatisticActivityStorage::WriteActivity('Link', 'Special Options', array('Name' => $link->name, 'File' => $link->fileName));
						$this->renderPartial('specialDialog', array('object' => $link, 'isLink' => true, 'isLineBreak' => $link->getIsLineBreak()), false, true);
						$rendered = true;
					}
				}
				else if (isset($folderId))
				{
					$folderRecord = FolderStorage::model()->findByPk($folderId);
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

		public function actionGetLinkDetails()
		{
			$linkId = Yii::app()->request->getPost('linkId');
			if (isset($linkId))
			{
				$linkRecord = LinkStorage::getLinkById($linkId);
				if (isset($linkRecord))
				{
					$libraryManager = new LibraryManager();
					$library = $libraryManager->getLibraryById($linkRecord->id_library);
					$link = new LibraryLink(new LibraryFolder(new LibraryPage($library)));
					switch ($this->browser)
					{
						case Browser::BROWSER_IPHONE:
						case Browser::BROWSER_ANDROID_MOBILE:
							$link->browser = 'phone';
							break;
						default :
							if ($this->isTabletMobileView)
								$link->browser = 'phone';
							else if (Yii::app()->browser->isMobile())
							{
								$link->browser = 'mobile';
							}
							else
							{
								$browser = Yii::app()->browser->getBrowser();
								switch ($browser)
								{
									case 'Internet Explorer':
										$link->browser = 'ie';
										break;
									case 'Chrome':
									case 'Safari':
										$link->browser = 'webkit';
										break;
									case 'Firefox':
										$link->browser = 'firefox';
										break;
									case 'Opera':
										$link->browser = 'opera';
										break;
									default:
										$link->browser = 'webkit';
										break;
								}
							}
							break;
					}
					$link->load($linkRecord);
					$this->renderPartial('linkDetails', array('link' => $link), false, true);
				}
			}
		}

		public function actionGetLinkPreviewList()
		{
			$linkId = Yii::app()->request->getPost('linkId');
			if (isset($linkId))
			{
				$linkRecord = LinkStorage::getLinkById($linkId);
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
					$this->renderPartial('linkPreview', array('link' => $link, 'authorized' => $authorized), false, true);
				}
			}
		}

		public function actionGetAttachmentPreviewList()
		{
			$attachmnetId = Yii::app()->request->getPost('linkId');
			if (isset($attachmnetId))
			{
				$attachmentRecord = AttachmentStorage::getAttachmentById($attachmnetId);
				if (isset($attachmentRecord))
				{
					$linkRecord = LinkStorage::getLinkById($attachmentRecord->id_link);
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

						$attachment = new Attachment($link);
						$attachment->browser = $link->browser;
						$attachment->load($attachmentRecord);

						$this->renderPartial('linkPreview', array('link' => $attachment, 'authorized' => $authorized), false, true);
					}
				}
			}
		}

		public function actionGetFileCard()
		{
			$linkId = Yii::app()->request->getPost('linkId');
			if (isset($linkId))
			{
				$linkRecord = LinkStorage::getLinkById($linkId);
				if (isset($linkRecord))
				{
					$libraryManager = new LibraryManager();
					$library = $libraryManager->getLibraryById($linkRecord->id_library);
					$link = new LibraryLink(new LibraryFolder(new LibraryPage($library)));
					$link->browser = 'phone';
					$link->load($linkRecord);
					StatisticActivityStorage::WriteActivity('Link', 'File Card', array('Name' => $link->name, 'File' => $link->fileName));
					$this->renderPartial('fileCard', array('link' => $link), false, true);
				}
			}
		}

		public function actionRunFullscreenGallery()
		{
			$linkId = Yii::app()->request->getQuery('linkId');
			$format = Yii::app()->request->getQuery('format');
			if (isset($linkId) && isset($format))
			{
				$linkRecord = LinkStorage::getLinkById($linkId);
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
				else
				{
					$attachmentRecord = AttachmentStorage::getAttachmentById($linkId);
					if (isset($attachmentRecord))
					{
						$linkRecord = LinkStorage::getLinkById($attachmentRecord->id_link);
						$libraryManager = new LibraryManager();
						$libraryManager->getLibraries();
						$library = $libraryManager->getLibraryById($attachmentRecord->id_library);
						if (isset($library) && isset($linkRecord))
						{
							$link = new LibraryLink(new LibraryFolder(new LibraryPage($library)));
							$link->browser = 'default';
							$link->load($linkRecord);

							$attachment = new Attachment($link);
							$attachment->browser = 'default';
							$attachment->load($attachmentRecord);
							$selectedLinks = $attachment->getViewSource($format);
							$selectedThumbs = $attachment->getViewSource('thumbs');
						}
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

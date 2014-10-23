<?php

	class SiteController extends IsdController
	{
		public $defaultAction = 'index';

		public function getViewPath()
		{
			return YiiBase::getPathOfAlias($this->pathPrefix . 'site');
		}

		public function actionIndex()
		{
			$this->pageTitle = Yii::app()->name;
			$tickerRecords = TickerLinkRecord::getLinks();
			$this->render('index', array('tickerRecords' => $tickerRecords));
		}

		public function actionBadBrowser()
		{
			$this->render('badBrowser');
		}

		public function actionLogin()
		{
			$this->redirect($this->createUrl('auth/login'));
		}

		public function actionError()
		{
			if ($error = Yii::app()->errorHandler->error)
			{
				$this->pageTitle = Yii::app()->name . ' - Error';
				if (Yii::app()->request->isAjaxRequest)
					echo $error['message'];
				else
					$this->render('errorMessage', $error);
			}
		}

		public function actionDownloadDialog()
		{
			$linkId = Yii::app()->request->getPost('linkId');
			if (isset($linkId))
			{
				$linkRecord = LinkRecord::getLinkById($linkId);
				if (isset($linkRecord))
				{
					if ($linkRecord->format == 'video' || $linkRecord->format == 'wmv' || $linkRecord->format == 'mp4')
						$this->renderPartial('downloadVideoDialog', array('format' => $linkRecord->format), false, true);
				}
			}
		}

		public function actionDownloadFile()
		{
			$linkId = Yii::app()->request->getQuery('linkId');
			$partId = Yii::app()->request->getQuery('partId');
			$partFormat = Yii::app()->request->getQuery('partFormat');
			$format = Yii::app()->request->getQuery('format');
			if (isset($linkId))
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
						$name = $link->name;
						$fileName = $link->fileName;
						$originalFormat = $linkRecord->format;
						if ($linkRecord->format == 'video' || $linkRecord->format == 'mp4' || $linkRecord->format == 'wmv')
						{
							if ($format == 'wmv')
							{
								if ($linkRecord->format == 'wmv')
									$path = $link->filePath;
								else
								{
									/** @var $previewRecord PreviewRecord */
									$previewRecord = PreviewRecord::model()->find('id_container =? and type=?', array($linkRecord->id_preview, 'wmv'));
									if (isset($previewRecord))
										$path = $library->storagePath . DIRECTORY_SEPARATOR . str_replace('\\', '/', $previewRecord->relative_path);
									else
										$path = $link->filePath;
								}
							}
							else if ($format == 'mp4')
							{
								if ($linkRecord->format == 'mp4')
									$path = $link->filePath;
								else
								{
									/** @var $previewRecord PreviewRecord */
									$previewRecord = PreviewRecord::model()->find('id_container =? and type=?', array($linkRecord->id_preview, 'mp4'));
									if (isset($previewRecord))
										$path = $library->storagePath . DIRECTORY_SEPARATOR . str_replace('\\', '/', $previewRecord->relative_path);
								}
							}
							if (isset($path))
								$fileName = basename($path);
						}
						else if (isset($partId) && $partId != "null" && isset($partFormat) && $partFormat != "null")
						{
							$index = intval($partId);
							$previewRecords = PreviewRecord::model()->findAll('id_container =? and type=? order by length(relative_path), relative_path', array($linkRecord->id_preview, $partFormat));
							if (count($previewRecords) > $index)
							{
								$path = $library->storagePath . DIRECTORY_SEPARATOR . str_replace('\\', '/', $previewRecords[$index]->relative_path);
								$fileName = str_replace('.' . $link->fileExtension, '', $link->fileName) . "-" . basename($path);
							}
						}
						else if ($linkRecord->format != 'pdf' && $format == 'pdf')
						{
							$previewRecord = PreviewRecord::model()->find('id_container =? and type=?', array($linkRecord->id_preview, 'pdf'));
							if (isset($previewRecord))
							{
								$path = $library->storagePath . DIRECTORY_SEPARATOR . str_replace('\\', '/', $previewRecord->relative_path);
								$fileName = basename($path);
							}
						}
						else
							$path = $link->filePath;
					}
				}
			}
			if (isset($path))
			{
				/** @var $name string */
				/** @var $fileName string */
				/** @var $originalFormat string */
				StatisticActivityRecord::WriteActivity('Link', 'Download', array('Name' => $name, 'File' => basename($path), 'Original Format' => $originalFormat, 'Format' => $format));
				return Yii::app()->getRequest()->sendFile($fileName, @file_get_contents($path));
			}
			Yii::app()->end();
			return null;
		}

		public function actionSwitchVersion()
		{
			$version = Yii::app()->request->getPost('siteVersion');
			if (isset($version))
			{
				Yii::app()->cacheDB->set('siteVersion', $version, (60 * 60 * 24 * 7));
				Yii::app()->end();
			}
		}
	}

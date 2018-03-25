<?
	use application\models\wallbin\models\web\LibraryManager as LibraryManager;
	use application\models\wallbin\models\web\LibraryPage as LibraryPage;
	use application\models\wallbin\models\web\LibraryFolder as LibraryFolder;
	use application\models\wallbin\models\web\LibraryLink as LibraryLink;

	Yii::import('application.extensions.phpQuery.phpQuery.phpQuery');

	/**
	 * Class PreviewController
	 */
	class PreviewController extends IsdController
	{
		/** return boolean */
		protected function getIsPublicController()
		{
			return true;
		}

		public function getViewPath()
		{
			return YiiBase::getPathOfAlias($this->pathPrefix . 'preview');
		}

		public function actionGetViewDialog()
		{
			$linkId = Yii::app()->request->getPost('linkId');
			$parentBundleId = Yii::app()->request->getPost('parentBundleId');
			if (strtolower(trim(Yii::app()->request->getPost('isQuickSite'))) == 'true')
				$isQuickSite = true;
			else
				$isQuickSite = false;
			$screenSettings = Yii::app()->request->getPost('screenSettings');
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
					$previewData = $link->getPreviewData($parentBundleId, $isQuickSite, $this->isPhone);
					$content = '';
					if ($previewData->contentView != '')
						$content = $this->renderPartial(
							$previewData->contentView,
							array(
								'data' => $previewData,
								'screenSettings' => $screenSettings
							),
							true);
					$dialogData = array(
						'format' => $previewData->viewerFormat,
						'data' => $previewData,
						'content' => $content,
					);
				}
			}
			echo CJSON::encode($dialogData);
			Yii::app()->end();
		}

		public function actionGetRateDialog()
		{
			$linkId = Yii::app()->request->getPost('linkId');
			$parentBundleId = Yii::app()->request->getPost('parentBundleId');
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

					$previewData = $link->getPreviewData($parentBundleId, false, $this->isPhone);

					$dialogData = array(
						'format' => $previewData->viewerFormat,
						'data' => $previewData,
						'content' => $this->renderPartial('rateDialog', array('previewData' => $previewData), true),
					);
				}
			}
			echo CJSON::encode($dialogData);
			Yii::app()->end();
		}

		public function actionGetLinkContextMenu()
		{
			$linkId = Yii::app()->request->getPost('linkId');
			$parentBundleId = Yii::app()->request->getPost('parentBundleId');
			if (strtolower(trim(Yii::app()->request->getPost('isQuickSite'))) == 'true')
				$isQuickSite = true;
			else
				$isQuickSite = false;
			$menuData = array();
			if (isset($linkId))
			{
				$linkRecord = LinkRecord::getLinkById($linkId);
				if (isset($linkRecord))
				{
					$libraryManager = new LibraryManager();
					$library = $libraryManager->getLibraryById($linkRecord->id_library);
					$link = new LibraryLink(new LibraryFolder(new LibraryPage($library)));
					$link->load($linkRecord);

					$previewData = $link->getPreviewData($parentBundleId, $isQuickSite, $this->isPhone);

					$menuData = array(
						'format' => $previewData->viewerFormat,
						'data' => $previewData,
						'content' => count($previewData->contextActions) > 0 ?
							$this->renderPartial('linkContextMenu', array('data' => $previewData), true) :
							'',
					);
				}
			}
			echo CJSON::encode($menuData);
			Yii::app()->end();
		}

		public function actionGetWindowContextMenu()
		{
			$folderId = Yii::app()->request->getPost('folderId');
			if (isset($folderId))
			{
				$folderRecord = FolderRecord::model()->findByPk($folderId);
				if (isset($folderRecord))
				{
					$this->renderPartial('folderContextMenu');
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
					$authorized = UserIdentity::isUserAuthorized();
					$libraryManager = new LibraryManager();
					$library = $libraryManager->getLibraryById($linkRecord->id_library);
					$link = new LibraryLink(new LibraryFolder(new LibraryPage($library)));
					$link->load($linkRecord);
					$this->renderPartial('linkPreview', array('link' => $link, 'authorized' => $authorized, 'isQuickSite' => $isQuickSite), false, true);
				}
			}
		}

		public function actionGetBar()
		{
			$this->renderPartial('bar', array(), false, true);
		}

		public function actionGetEmailDialog()
		{
			$linkId = Yii::app()->request->getPost('linkId');
			$parentBundleId = Yii::app()->request->getPost('parentBundleId');
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

					$previewData = $link->getPreviewData($parentBundleId, false, $this->isPhone);

					$dialogData = array(
						'format' => $previewData->viewerFormat,
						'data' => $previewData,
						'content' => $this->renderPartial('emailDialog', array('data' => $previewData), true),
					);
				}
			}
			echo CJSON::encode($dialogData);
			Yii::app()->end();
		}

		public function actionDownloadFile()
		{
			$data = CJSON::decode(Yii::app()->request->getPost('fileData'), false);
			Yii::app()->getRequest()->sendFile($data->name, @file_get_contents($data->path));
			Yii::app()->end();
		}

		public function actionPrepareDownloadFolderLink()
		{
			$linkId = Yii::app()->request->getPost('linkId');
			if (isset($linkId))
			{
				$linkRecord = LinkRecord::getLinkById($linkId);
				if (isset($linkRecord))
				{
					$libraryManager = new LibraryManager();
					$library = $libraryManager->getLibraryById($linkRecord->id_library);
					$link = new LibraryLink(new LibraryFolder(new LibraryPage($library)));
					$link->load($linkRecord);
					if ($link->isFolder)
					{
						$downloadInfoList = FileDownloadInfo::getFolderDownloadInfo($link);
						$this->renderPartial(
							'zipFiles',
							array(
								'zipName' => $link->fileName,
								'downloadInfoList' => $downloadInfoList
							),
							false, true);
					}
				}
			}
		}

		public function actionPrepareDownloadLinkBundle()
		{
			$linkId = Yii::app()->request->getPost('linkId');
			if (isset($linkId))
			{
				$linkRecord = LinkRecord::getLinkById($linkId);
				if (isset($linkRecord))
				{
					$libraryManager = new LibraryManager();
					$library = $libraryManager->getLibraryById($linkRecord->id_library);
					$link = new LibraryLink(new LibraryFolder(new LibraryPage($library)));
					$link->load($linkRecord);
					if ($link->isLinkBundle)
					{
						$downloadInfoList = FileDownloadInfo::getLinkBundleDownloadInfo($link);
						$this->renderPartial(
							'zipFiles',
							array(
								'zipName' => sprintf("%s.zip", $link->name),
								'downloadInfoList' => $downloadInfoList
							),
							false, true);
					}
				}
			}
		}

		public function actionPrepareDownloadLibraryFolder()
		{
			$folderId = Yii::app()->request->getPost('folderId');
			if (isset($folderId))
			{
				$downloadInfoList = array();
				/** @var FolderRecord $folderRecord */
				$folderRecord = FolderRecord::model()->findByPk($folderId);
				$libraryManager = new LibraryManager();
				$linkRecords = LinkRecord::getLinksByFolder($folderId);
				foreach ($linkRecords as $linkRecord)
				{
					if (!isset($library))
						$library = $libraryManager->getLibraryById($linkRecord->id_library);
					$link = new LibraryLink(new LibraryFolder(new LibraryPage($library)));
					$link->load($linkRecord);
					if ($link->isFolder)
					{
						$downloadInfoList = array_merge($downloadInfoList, FileDownloadInfo::getFolderDownloadInfo($link));
					}
					else if ($link->isLinkBundle)
					{
						$downloadInfoList = array_merge($downloadInfoList, FileDownloadInfo::getLinkBundleDownloadInfo($link));
					}
					else
					{
						$fileInfo = FileInfo::fromLinkRecord($linkRecord, $library);
						if ($fileInfo->isFile)
							$downloadInfoList[] = FileDownloadInfo::getFileDownloadInfo($fileInfo);
					}
				}
				$this->renderPartial(
					'zipFiles',
					array(
						'zipName' => $folderRecord->name,
						'downloadInfoList' => $downloadInfoList
					),
					false, true);
			}
		}

		public function actionZipAndDownloadFiles()
		{
			$zipName = Yii::app()->request->getPost('zipName');

			/** @var FileDownloadInfo $files */
			$files = CJSON::decode(Yii::app()->request->getPost('files'), false);

			$zipFile = $zipName . '.zip';
			$zipPath = LibraryManager::getLibrariesRootPath() . DIRECTORY_SEPARATOR . 'downloads' . DIRECTORY_SEPARATOR . $zipFile;
			$zip = new ZipArchive();
			$zip->open($zipPath, ZipArchive::CREATE | ZipArchive::OVERWRITE);

			foreach ($files as $file)
			{
				$filePath = $file->fullPath;
				$relativePath = $file->relativePath;
				$zip->addFile($filePath, $relativePath);
			}
			$zip->close();
			Yii::app()->getRequest()->sendFile($zipFile, @file_get_contents($zipPath));
			Yii::app()->end();
		}

		public function actionZipAndDownloadLinkBundle()
		{
			$linkId = Yii::app()->request->getQuery('linkId');
			if (isset($linkId))
			{
				$linkRecord = LinkRecord::getLinkById($linkId);
				if (isset($linkRecord))
				{
					$libraryManager = new LibraryManager();
					$library = $libraryManager->getLibraryById($linkRecord->id_library);
					$link = new LibraryLink(new LibraryFolder(new LibraryPage($library)));
					$link->load($linkRecord);
					if ($link->isLinkBundle)
					{
						$files = FileDownloadInfo::getLinkBundleDownloadInfo($link);
						$zipFile = $link->name . '.zip';
						$zipPath = LibraryManager::getLibrariesRootPath() . DIRECTORY_SEPARATOR . 'downloads' . DIRECTORY_SEPARATOR . $zipFile;
						$zip = new ZipArchive();
						$zip->open($zipPath, ZipArchive::CREATE | ZipArchive::OVERWRITE);
						foreach ($files as $file)
						{
							$filePath = $file->fullPath;
							$relativePath = $file->relativePath;
							$zip->addFile($filePath, $relativePath);
						}
						$zip->close();
						Yii::app()->getRequest()->sendFile($zipFile, @file_get_contents($zipPath));
						Yii::app()->end();
					}
				}
			}
		}

		public function actionGetSingleInternalLink()
		{
			$linkId = Yii::app()->request->getQuery('linkId');
			$linkRecord = LinkRecord::getLinkById($linkId);
			$this->pageTitle = sprintf('%s', $linkRecord->name);
			$menuGroups = ShortcutsManager::getAvailableGroups($this->isPhone);
			$this->render('internalLinkSinglePage', array('menuGroups' => $menuGroups, 'linkName' => $linkRecord->name, 'linkId' => $linkId));
		}
	}

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
					$content = '';
					if ($previewData->contentView != '')
						$content = $this->renderPartial($previewData->contentView, array('data' => $previewData), true);
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

					$previewData = $link->getPreviewData(false);

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

					$previewData = $link->getPreviewData($isQuickSite);

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

					$previewData = $link->getPreviewData(false);

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

		public function actionPrepareDownloadFolder()
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
						$rootPath = realpath($link->filePath);

						/** @var SplFileInfo[] $folderFies */
						$folderFies = new RecursiveIteratorIterator(
							new RecursiveDirectoryIterator($rootPath),
							RecursiveIteratorIterator::LEAVES_ONLY
						);

						$folderFileInfoList = array();
						foreach ($folderFies as $name => $file)
						{
							if (!$file->isDir())
							{
								$fileInfo = new FileDownloadInfo();
								$fileInfo->name = $file->getBasename();
								$fileInfo->fullPath = $file->getRealPath();
								$fileInfo->relativePath = substr($fileInfo->fullPath, strlen($rootPath) + 1);
								$fileInfo->size = $file->getSize();
								$folderFileInfoList[] = $fileInfo;
							}
						}
						$this->renderPartial(
							'zipFolder',
							array(
								'folderName' => $link->fileName,
								'folderFileInfoList' => $folderFileInfoList
							),
							false, true);
					}
				}
			}
		}

		public function actionDownloadFolder()
		{
			$folderName = Yii::app()->request->getPost('folderName');

			/** @var FileDownloadInfo $folderFiles */
			$folderFiles = CJSON::decode(Yii::app()->request->getPost('folderFiles'), false);

			$zipFile = $folderName . '.zip';
			$zipPath = LibraryManager::getLibrariesRootPath() . DIRECTORY_SEPARATOR . 'downloads' . DIRECTORY_SEPARATOR . $zipFile;
			$zip = new ZipArchive();
			$zip->open($zipPath, ZipArchive::CREATE | ZipArchive::OVERWRITE);

			foreach ($folderFiles as $file)
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

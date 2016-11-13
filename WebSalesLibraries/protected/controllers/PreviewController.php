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

					$dialogData = array(
						'format' => $previewData->viewerFormat,
						'data' => $previewData,
						'content' => $this->renderPartial($previewData->contentView, array('data' => $previewData), true),
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
			$data = CJSON::decode(base64_decode(Yii::app()->request->getQuery('data')), false);
			Yii::app()->getRequest()->sendFile($data->name, @file_get_contents($data->path));
			Yii::app()->end();
		}

		public function actionZipAndDownloadLink()
		{
			$data = CJSON::decode(base64_decode(Yii::app()->request->getQuery('data')), false);
			$linkId = $data->linkId;
			if (isset($linkId))
			{
				$linkRecord = LinkRecord::getLinkById($linkId);
				if (isset($linkRecord))
				{
					$libraryManager = new LibraryManager();
					$library = $libraryManager->getLibraryById($linkRecord->id_library);
					$link = new LibraryLink(new LibraryFolder(new LibraryPage($library)));
					$link->load($linkRecord);

					$zipFile = $link->fileName . '.zip';
					$zipPath = LibraryManager::getLibrariesRootPath() . DIRECTORY_SEPARATOR . 'downloads' . DIRECTORY_SEPARATOR . $zipFile;
					$zip = new ZipArchive();
					$zip->open($zipPath, ZipArchive::CREATE | ZipArchive::OVERWRITE);

					if ($link->isFolder)
					{
						$rootPath = realpath($link->filePath);

						/** @var SplFileInfo[] $files */
						$files = new RecursiveIteratorIterator(
							new RecursiveDirectoryIterator($rootPath),
							RecursiveIteratorIterator::LEAVES_ONLY
						);

						foreach ($files as $name => $file)
						{
							if (!$file->isDir())
							{
								$filePath = $file->getRealPath();
								$relativePath = substr($filePath, strlen($rootPath) + 1);
								$zip->addFile($filePath, $relativePath);
							}
						}
					}
					else
					{
						$filePath = realpath($link->filePath);
						$zip->addFile($filePath, $link->fileName);
					}

					$zip->close();
					Yii::app()->getRequest()->sendFile($zipFile, @file_get_contents($zipPath));
				}
			}
			Yii::app()->end();
		}
	}

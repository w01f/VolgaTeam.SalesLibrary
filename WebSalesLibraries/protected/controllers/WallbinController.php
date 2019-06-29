<?

	use application\models\wallbin\models\web\LibraryManager as LibraryManager;
	use application\models\wallbin\models\web\LibraryPage as LibraryPage;
	use application\models\wallbin\models\web\LibraryFolder as LibraryFolder;
	use application\models\wallbin\models\web\LibraryLink as LibraryLink;

	Yii::import('application.extensions.phpQuery.phpQuery.phpQuery');

	/**
	 * Class WallbinController
	 */
	class WallbinController extends IsdController
	{
		public function getViewPath()
		{
			return YiiBase::getPathOfAlias($this->pathPrefix . 'wallbin');
		}

		//------Common Site API-------------------------------------------
		public function actionGetFolderLinksList()
		{
			$folderId = Yii::app()->request->getPost('folderId');
			if (isset($folderId))
			{
				/** @var $folderRecord FolderRecord */
				$folderRecord = FolderRecord::model()->findByPk($folderId);
				$libraryManager = new LibraryManager();
				$library = $libraryManager->getLibraryById($folderRecord->id_library, false);
				/** @var LibraryPageRecord $pageRecord */
				$pageRecord = LibraryPageRecord::model()->findByPk($folderRecord->id_page);
				$page = new LibraryPage($library);
				$page->load($pageRecord);
				$folder = new LibraryFolder($page);
				$folder->load($folderRecord);
				$folder->loadFiles(true);
				$this->renderPartial('folderLinks', array('folder' => $folder), false, true);
			}
		}

		public function actionGetLinkFolderContent()
		{
			$linkId = Yii::app()->request->getPost('linkId');
			if (isset($linkId))
			{
				$linkRecord = LinkRecord::getLinkById($linkId);
				if (isset($linkRecord))
				{
					$libraryManager = new LibraryManager();
					$library = $libraryManager->getLibraryById($linkRecord->id_library, true);

					$folderRecord = FolderRecord::model()->findByPk($linkRecord->id_folder);
					$folder = new LibraryFolder(new LibraryPage($library));
					$folder->load($folderRecord);
					$link = new LibraryLink($folder);
					$link->load($linkRecord);
					$link->loadFolderContent();
					$this->renderPartial('linkFolderContent', array('link' => $link), false, true);
				}
			}
		}
		//------Common Site API-------------------------------------------

		//------Regular Site API-------------------------------------------
		public function actionGetColumnsView()
		{
			$styleContainerType = Yii::app()->request->getPost('styleContainerType');
			$styleContainerId = Yii::app()->request->getPost('styleContainerId');
			$libraryId = Yii::app()->request->getPost('libraryId');
			$pageId = Yii::app()->request->getPost('pageId');
			$contentContainerId = Yii::app()->request->getPost('contentContainerId');
			$screenSettings = Yii::app()->request->getPost('screenSettings');

			$libraryManager = new LibraryManager();

			/** @var \application\models\wallbin\models\web\style\WallbinStyle $style */
			$style = null;
			if (isset($styleContainerType) && isset($styleContainerId))
			{
				switch ($styleContainerType)
				{
					case 'shortcut':
						/** @var  $shortcutRecord ShortcutLinkRecord */
						$shortcutRecord = ShortcutLinkRecord::model()->findByPk($styleContainerId);
						/** @var  $shortcut WallbinShortcut */
						$shortcut = $shortcutRecord->getRegularModel($this->isPhone);
						$shortcut->loadPageConfig();
						$style = $shortcut->style;
						break;
					case 'internal link':
						$linkRecord = LinkRecord::getLinkById($styleContainerId);
						$library = $libraryManager->getLibraryById($linkRecord->id_library, false);
						$link = new LibraryLink(new LibraryFolder(new LibraryPage($library)));
						$link->load($linkRecord);
						/** @var InternalLinkPreviewData $previewData */
						$previewData = $link->getPreviewData(null, false, $this->isPhone);
						$style = $previewData->previewInfo->getStyle();
						break;
				}
			}

			$library = $libraryManager->getLibraryById($libraryId, true);
			$selectedPage = $library->getPageById($pageId);
			if (isset($style) && $style->page->enabled)
			{
				$selectedPage->loadData();
				$selectedPage->loadFolders(true);
				$this->renderPartial('../wallbin/columnsView',
					array(
						'libraryPage' => $selectedPage,
						'containerId' => $contentContainerId,
						'style' => $style->page,
						'screenSettings' => $screenSettings
					));
			}
			else if ($this->isIOSDevice || $screenSettings['screenSizeType'] !== 'large')
			{
				$selectedPage->loadData();
				$selectedPage->loadFolders(true);
				$this->renderPartial('../wallbin/columnsView',
					array(
						'libraryPage' => $selectedPage,
						'containerId' => $contentContainerId,
						'style' => \application\models\wallbin\models\web\style\WallbinStyle::createDefault()->page,
						'screenSettings' => $screenSettings
					));
			}
			else
				echo $selectedPage->getCache();
		}

		public function actionGetAccordionView()
		{
			$styleContainerType = Yii::app()->request->getPost('styleContainerType');
			$styleContainerId = Yii::app()->request->getPost('styleContainerId');
			$libraryId = Yii::app()->request->getPost('libraryId');
			$pageId = Yii::app()->request->getPost('pageId');
			$contentContainerId = Yii::app()->request->getPost('contentContainerId');
			$screenSettings = Yii::app()->request->getPost('screenSettings');

			$libraryManager = new LibraryManager();

			/** @var \application\models\wallbin\models\web\style\WallbinStyle $style */
			$style = null;
			if (isset($styleContainerType) && isset($styleContainerId))
			{
				switch ($styleContainerType)
				{
					case 'shortcut':
						/** @var  $shortcutRecord ShortcutLinkRecord */
						$shortcutRecord = ShortcutLinkRecord::model()->findByPk($styleContainerId);
						/** @var  $shortcut WallbinShortcut */
						$shortcut = $shortcutRecord->getRegularModel($this->isPhone);
						$shortcut->loadPageConfig();
						$style = $shortcut->style;
						break;
					case 'internal link':
						$linkRecord = LinkRecord::getLinkById($styleContainerId);
						$library = $libraryManager->getLibraryById($linkRecord->id_library, false);
						$link = new LibraryLink(new LibraryFolder(new LibraryPage($library)));
						$link->load($linkRecord);
						/** @var InternalLinkPreviewData $previewData */
						$previewData = $link->getPreviewData(null, false, $this->isPhone);
						$style = $previewData->previewInfo->getStyle();
						break;
				}
			}
			if (!isset($style))
				$style = \application\models\wallbin\models\web\style\WallbinStyle::createDefault();

			$library = $libraryManager->getLibraryById($libraryId, true);
			$selectedPage = $library->getPageById($pageId);
			$selectedPage->loadData();
			$this->renderPartial('accordionView',
				array(
					'libraryPage' => $selectedPage,
					'containerId' => $contentContainerId,
					'style' => $style->page,
					'screenSettings' => $screenSettings
				),
				false, true);
		}
		//------Regular Site API-------------------------------------------

		//------Mobile Site API-----------------------------------------------

		public function actionGetLibraryPage()
		{
			$libraryId = Yii::app()->request->getQuery('libraryId');
			$pageId = Yii::app()->request->getQuery('pageId');
			$libraryManager = new LibraryManager();
			if (isset($libraryId))
				$library = $libraryManager->getLibraryById($libraryId, true);
			else
			{
				$availableLibraries = $libraryManager->getAvailableLibraries();
				if (count($availableLibraries) > 0)
					$library = $availableLibraries[0];
			}
			if (isset($library))
			{
				$library = $libraryManager->getLibraryById($library->id, true);
				$defaultPage = null;
				if (isset($pageId))
					$defaultPage = $library->getPageById($pageId);
				if (!isset($defaultPage))
					$defaultPage = $library->pages[0];
				$defaultPage->loadData();

				$this->render('libraryWrapper', array(
					'library' => $library,
					'defaultPage' => $defaultPage
				));
			}
			else
				Yii::app()->end();
		}

		public function actionGetPageContent()
		{
			$pageId = Yii::app()->request->getPost('pageId');
			$libraryManager = new LibraryManager();
			/** @var  $pageRecord LibraryPageRecord */
			$pageRecord = LibraryPageRecord::model()->findByPk($pageId);
			$library = $libraryManager->getLibraryById($pageRecord->id_library, false);
			$pageModel = new LibraryPage($library);
			$pageModel->load($pageRecord);
			$pageModel->loadData();
			$this->renderPartial('pageContent', array('page' => $pageModel));
		}
		//------Mobile Site API-----------------------------------------------
	}

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
				$library = $libraryManager->getLibraryById($folderRecord->id_library);
				$pageRecord = LibraryPageRecord::model()->findByPk($folderRecord->id_page);
				$page = new LibraryPage($library);
				$page->load($pageRecord);
				$folder = new LibraryFolder($page);
				$folder->load($folderRecord);
				$folder->displayLinkWidgets = true;
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
					$library = $libraryManager->getLibraryById($linkRecord->id_library);

					$folderRecord = FolderRecord::model()->findByPk($linkRecord->id_folder);
					$folder = new LibraryFolder(new LibraryPage($library));
					$folder->load($folderRecord);
					$folder->displayLinkWidgets = true;
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
			$shortcutId = Yii::app()->request->getPost('shortcutId');
			$libraryId = Yii::app()->request->getPost('libraryId');
			$pageId = Yii::app()->request->getPost('pageId');

			/** @var \application\models\wallbin\models\web\style\WallbinStyle $style */
			$style = null;
			if (isset($shortcutId))
			{
				/** @var  $shortcutRecord ShortcutLinkRecord */
				$shortcutRecord = ShortcutLinkRecord::model()->findByPk($shortcutId);
				/** @var  $shortcut WallbinShortcut */
				$shortcut = $shortcutRecord->getModel($this->isPhone);
				$style = $shortcut->style;
			}

			$libraryManager = new LibraryManager();
			$library = $libraryManager->getLibraryById($libraryId);
			$selectedPage = $library->getPageById($pageId);
			if (isset($style) && $style->page->enabled)
			{
				$selectedPage->loadData();
				$selectedPage->loadFolders(true);
				$this->renderPartial('../wallbin/columnsView',
					array(
						'libraryPage' => $selectedPage,
						'style' => $style->page
					));
			}
			else
				echo $selectedPage->getCache();
		}

		public function actionGetAccordionView()
		{
			$libraryId = Yii::app()->request->getPost('libraryId');
			$pageId = Yii::app()->request->getPost('pageId');
			$libraryManager = new LibraryManager();
			$library = $libraryManager->getLibraryById($libraryId);
			$selectedPage = $library->getPageById($pageId);
			$selectedPage->loadData();
			$this->renderPartial('accordionView', array('libraryPage' => $selectedPage), false, true);
		}
		//------Regular Site API-------------------------------------------

		//------Mobile Site API-----------------------------------------------

		public function actionGetLibraryPage()
		{
			$libraryId = Yii::app()->request->getQuery('libraryId');
			$pageId = Yii::app()->request->getQuery('pageId');
			$libraryManager = new LibraryManager();
			if (isset($libraryId))
				$library = $libraryManager->getLibraryById($libraryId);
			else
			{
				$availableLibraries = $libraryManager->getLibraries();
				if (count($availableLibraries) > 0)
					$library = $availableLibraries[0];
			}
			if (isset($library))
			{
				$defaultPage = null;
				if (isset($pageId))
					$defaultPage = $library->getPageById($pageId);
				if (!isset($defaultPage))
					$defaultPage = $library->pages[0];
				$defaultPage->loadData();

				$userId = UserIdentity::getCurrentUserId();
				$tabPageExisted = UserTabRecord::isUserTabExists($userId, $library->id);

				$this->render('libraryWrapper', array(
					'library' => $library,
					'defaultPage' => $defaultPage,
					'tabPageExisted' => $tabPageExisted
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
			$library = $libraryManager->getLibraryById($pageRecord->id_library);
			$pageModel = new LibraryPage($library);
			$pageModel->load($pageRecord);
			$pageModel->loadData();
			$this->renderPartial('pageContent', array('page' => $pageModel));
		}
		//------Mobile Site API-----------------------------------------------
	}

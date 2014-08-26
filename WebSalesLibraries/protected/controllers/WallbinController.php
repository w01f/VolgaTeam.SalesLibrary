<?php
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

		public function actionGetColumnsView()
		{
			$libraryManager = new LibraryManager();
			$selectedPage = $libraryManager->getSelectedPage();
			$this->renderPartial('columnsView', array('selectedPage' => $selectedPage), false, true);
		}

		public function actionGetAccordionView()
		{
			$libraryManager = new LibraryManager();
			$selectedPage = $libraryManager->getSelectedPage();
			$selectedPage->loadData();
			$this->renderPartial('accordionView', array('libraryPage' => $selectedPage), false, true);
		}

		public function actionGetTabsView()
		{
			$wallbinView = Yii::app()->request->getPost('wallbinView');
			if (!isset($wallbinView) || $wallbinView == 'null')
				$wallbinView = 'columns';

			$libraryManager = new LibraryManager();
			$selectedLibrary = $libraryManager->getSelectedLibrary();
			$selectedPage = $libraryManager->getSelectedPage();
			$selectedPage->loadData();
			$this->renderPartial('tabsView', array('library' => $selectedLibrary, 'selectedPage' => $selectedPage, 'wallbinView' => $wallbinView), false, true);
		}

		public function actionGetLibraryDropDownList()
		{
			$libraryManager = new LibraryManager();
			$this->renderPartial('libraryDropDownList', array('libraryManager' => $libraryManager), false, true);
		}

		public function actionGetPageDropDownList()
		{
			$libraryManager = new LibraryManager();
			$this->renderPartial('pageDropDownList', array('selectedLibrary' => $libraryManager->getSelectedLibrary(),
				'selectedPage' => $libraryManager->getSelectedPage()), false, true);
		}

		public function actionGetFoldersList()
		{
			$libraryManager = new LibraryManager();
			$selectedPage = $libraryManager->getSelectedPage();
			$selectedPage->loadData();
			$this->renderPartial('folders', array('page' => $selectedPage), false, true);
		}

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
	}

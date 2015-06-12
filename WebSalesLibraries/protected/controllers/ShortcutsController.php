<?php

	/**
	 * Class ShortcutsController
	 */
	class ShortcutsController extends IsdController
	{
		public function getViewPath()
		{
			return YiiBase::getPathOfAlias($this->pathPrefix . 'shortcuts');
		}

		//------Common Site API-------------------------------------------
		public function actionGetPage()
		{
			$pageId = Yii::app()->request->getPost('pageId');
			$predefinedPageType = Yii::app()->request->getPost('predefinedPageType');
			if (isset($pageId))
			{
				/** @var $pageRecord ShortcutsPageRecord */
				$pageRecord = ShortcutsPageRecord::model()->findByPk($pageId);
				if (isset($pageRecord))
				{
					/** @var $tabRecord ShortcutsTabRecord */
					$tabRecord = ShortcutsTabRecord::model()->findByPk($pageRecord->id_tab);

					$savePageTypeTagName = sprintf('%s-%s', $tabRecord->name, $pageRecord->name);
					if (!(isset($predefinedPageType) && $predefinedPageType != ''))
					{
						if (isset(Yii::app()->request->cookies[$savePageTypeTagName]))
							$predefinedPageType = Yii::app()->request->cookies[$savePageTypeTagName]->value;
					}
					else
					{
						$cookie = new CHttpCookie($savePageTypeTagName, $predefinedPageType);
						$cookie->expire = time() + (60 * 60 * 24 * 7);
						Yii::app()->request->cookies[$savePageTypeTagName] = $cookie;
					}

					StatisticActivityRecord::WriteActivity('Shortcuts', 'Page Changed', array('Tab' => $tabRecord->name, 'Button' => $pageRecord->name));
					$pageModel = $pageRecord->getModel($this->isPhone ? 'grid' : $predefinedPageType);
					echo CJSON::encode(array(
						'type' => $pageModel->type,
						'logo' => $pageModel->ribbonLogoPath,
						'content' => $this->renderPartial('page', array('page' => $pageModel), true),
						'displayParameters' => $pageModel->getDisplayParameters(),
						'pageType' => $predefinedPageType
					));
				}
			}
			Yii::app()->end();
		}

		public function actionGetWindowShortcut()
		{
			$this->pageTitle = 'Shortcuts - Window';
			$linkId = Yii::app()->request->getQuery('linkId');
			$samePage = (bool)Yii::app()->request->getQuery('samePage');
			if (isset($linkId))
			{
				$linkRecord = ShortcutsLinkRecord::model()->findByPk($linkId);
				$windowShortcut = new WindowShortcut($linkRecord);
				$folder = $windowShortcut->getWindow();

				if ($this->isPhone)
				{
					$content = $this->renderPartial('../wallbin/folderLinks', array('folder' => $folder), true);
				}
				else
				{
					$content = $this->renderPartial('folderContainerHeader', array('windowShortcut' => $windowShortcut), true);
					$content .= $this->renderFile(Yii::getPathOfAlias($this->pathPrefix . 'wallbin') . '/folderContainer.php', array('folder' => $folder), true);
				}

				if ($samePage || $this->isPhone)
					echo $content;
				else
					$this->render('linkWrapper', array('objectId' => $linkId, 'objectName' => 'Window', 'objectLogo' => $windowShortcut->ribbonLogoPath, 'content' => $content));
			}
		}

		public function actionGetQuickList()
		{
			$this->pageTitle = 'Shortcuts - Quick List';
			$linkId = Yii::app()->request->getQuery('linkId');
			$samePage = (bool)Yii::app()->request->getQuery('samePage');
			if (isset($linkId))
			{
				$linkRecord = ShortcutsLinkRecord::model()->findByPk($linkId);
				$quickListShortcut = new QuickListShortcut($linkRecord);
				$quickListShortcut->loadQuickLinks();
				$content = $this->renderPartial('quickList', array('quickListShortcut' => $quickListShortcut), true);
				if ($samePage || $this->isPhone)
					echo $content;
				else
					$this->render('linkWrapper', array('objectId' => $linkId, 'objectName' => 'Quick List', 'objectLogo' => $quickListShortcut->ribbonLogoPath, 'content' => $content));
			}
		}

		public function actionGetSearchShortcut()
		{
			$this->pageTitle = 'Shortcuts - Search';
			$linkId = Yii::app()->request->getQuery('linkId');
			$samePage = (bool)Yii::app()->request->getQuery('samePage');
			if (isset($linkId))
			{
				/** @var $linkRecord ShortcutsLinkRecord */
				$linkRecord = ShortcutsLinkRecord::model()->findByPk($linkId);
				$searchShortcut = new SearchShortcut($linkRecord);
				$this->pageTitle = $searchShortcut->tooltip;
				$content = $this->renderPartial('searchConditions', array('searchContainer' => $searchShortcut), true);
				if ($samePage || $this->isPhone)
					echo $content;
				else
					$this->render('linkWrapper', array('objectId' => $linkId, 'objectName' => $searchShortcut->tooltip, 'objectLogo' => $searchShortcut->ribbonLogoPath, 'content' => $content));
			}
		}

		public function actionDownload()
		{
			$linkId = Yii::app()->request->getQuery('linkId');
			if (isset($linkId))
			{
				/**@var $linkRecord ShortcutsLinkRecord */
				$linkRecord = ShortcutsLinkRecord::model()->findByPk($linkId);
				/**@var $link DownloadShortcut */
				$link = $linkRecord->getModel();
				$fileName = $link->fileName;
				$path = $link->sourcePath;
				Yii::app()->getRequest()->sendFile($fileName, @file_get_contents($path));
			}
			Yii::app()->end();
		}
		//------Common Site API-------------------------------------------

		//------Regular Site API-------------------------------------------
		public function actionGetQuickSearchResult()
		{
			$pageId = Yii::app()->request->getQuery('pageId');
			$text = Yii::app()->request->getQuery('text');
			$onlyFiles = filter_var(trim(Yii::app()->request->getQuery('onlyFiles')), FILTER_VALIDATE_BOOLEAN);
			$onlyNewFiles = filter_var(trim(Yii::app()->request->getQuery('onlyNewFiles')), FILTER_VALIDATE_BOOLEAN);
			$fileTypes = Yii::app()->request->getQuery('fileTypes');
			if (isset($fileTypes))
				$fileTypes = CJSON::decode($fileTypes);
			else
				$fileTypes = array();

			$superFilters = Yii::app()->request->getQuery('superFilters');
			if (isset($superFilters))
				$superFilters = CJSON::decode($superFilters);
			else
				$superFilters = array();

			$categories = Yii::app()->request->getQuery('categories');
			if (isset($categories))
				$categories = CJSON::decode($categories);
			else
				$categories = array();

			if (isset($pageId))
			{
				/** @var $pageRecord ShortcutsPageRecord */
				$pageRecord = ShortcutsPageRecord::model()->findByPk($pageId);
				$pageModel = $pageRecord->getModel();
				$searchBar = $pageModel->searchBar;
				$this->pageTitle = $searchBar->title;
				$searchBar->conditions->text = $text;
				$searchBar->conditions->searchByContent = $onlyFiles;

				if ($onlyNewFiles)
				{
					$searchBar->conditions->startDate = date('Y-m-d', strtotime('-1 year'));
					$searchBar->conditions->endDate = date("Y-m-d");
				}

				$searchBar->conditions->fileTypes = $fileTypes;
				$searchBar->conditions->superFilters = $superFilters;

				unset($searchBar->conditions->categories);
				foreach ($categories as $arrayItem)
				{
					$category = (object)$arrayItem;
					$searchBar->conditions->categories[] = $category;
				}
				$content = $this->renderPartial('searchResult', array('searchContainer' => $searchBar), true);
				$this->render('linkWrapper', array('objectId' => $pageId, 'objectName' => $searchBar->title, 'content' => $content));
			}
		}

		public function actionGetDownloadDialog()
		{
			$linkId = Yii::app()->request->getPost('linkId');
			if (isset($linkId))
			{
				/** @var $linkRecord ShortcutsLinkRecord */
				$linkRecord = ShortcutsLinkRecord::model()->findByPk($linkId);
				/**@var $link DownloadShortcut */
				$link = $linkRecord->getModel();
				$this->renderPartial('downloadDialog', array('link' => $link));
			}
		}

		public function actionGetSubSearchBar()
		{
			$pageId = Yii::app()->request->getPost('pageId');
			$linkId = Yii::app()->request->getPost('linkId');
			/** @var $optionsContainer SearchShortcut|SearchBar */
			$optionsContainer = null;
			if (isset($pageId))
			{
				/** @var $pageRecord ShortcutsPageRecord */
				$pageRecord = ShortcutsPageRecord::model()->findByPk($pageId);
				$pageModel = $pageRecord->getModel();
				$optionsContainer = $pageModel->searchBar;
			}
			else if (isset($linkId))
			{
				/** @var $linkRecord ShortcutsLinkRecord */
				$linkRecord = ShortcutsLinkRecord::model()->findByPk($linkId);
				$optionsContainer = new SearchShortcut($linkRecord);
			}
			$this->renderPartial('subSearchBar/bar', array('optionsContainer' => $optionsContainer));
		}

		public function actionGetSubSearchCustomPanel()
		{
			$pageId = Yii::app()->request->getPost('pageId');
			$linkId = Yii::app()->request->getPost('linkId');
			/** @var $pageRecord ShortcutsPageRecord */
			if (!isset($pageId) && isset($linkId))
			{
				/** @var $linkRecord ShortcutsLinkRecord */
				$linkRecord = ShortcutsLinkRecord::model()->findByPk($linkId);
				$pageRecord = $linkRecord->getParentPage();
			}
			else
				$pageRecord = ShortcutsPageRecord::model()->findByPk($pageId);
			$pageModel = $pageRecord->getModel();
			$searchBar = $pageModel->searchBar;
			$this->renderPartial('subSearchBar/customSearchPanel', array('searchBar' => $searchBar));
		}

		public function actionGetSubSearchTemplatesPanel()
		{
			$pageId = Yii::app()->request->getPost('pageId');
			$linkId = Yii::app()->request->getPost('linkId');
			$templates = array();
			$id = '';
			if (isset($pageId))
			{
				/** @var $pageRecord ShortcutsPageRecord */
				$pageRecord = ShortcutsPageRecord::model()->findByPk($pageId);
				$pageModel = $pageRecord->getModel();
				$searchBar = $pageModel->searchBar;
				$templates = $searchBar->subConditions;
				$id = $pageId;
			}
			else if (isset($linkId))
			{
				/** @var $linkRecord ShortcutsLinkRecord */
				$linkRecord = ShortcutsLinkRecord::model()->findByPk($linkId);
				$link = new SearchShortcut($linkRecord);
				$templates = $link->subConditions;
				$id = $linkId;
			}
			$this->renderPartial('subSearchBar/searchTemplatesPanel', array('templates' => $templates, 'id' => $id));
		}

		public function actionEditSearchBarSettings()
		{
			$this->renderPartial('searchBar/settings');
		}
		//------Regular Site API-------------------------------------------

		//------Mobile Site API-----------------------------------------------
		public function actionGetTab()
		{
			$tabId = Yii::app()->request->getQuery('tabId');
			$tabRecord = ShortcutsTabRecord::model()->findByPk($tabId);
			if (isset($tabRecord))
			{
				$tabPages = TabPages::getList();
				$this->render('tabPage', array(
					'tabPages' => $tabPages,
					'tabRecord' => $tabRecord,
				));
			}
			else
				$this->redirect(Yii::app()->createAbsoluteUrl('site/index'));
		}

		public function actionGetLinkContentWrapper()
		{
			$linkId = Yii::app()->request->getPost('linkId');
			/** @var $linkRecord ShortcutsLinkRecord */
			$linkRecord = ShortcutsLinkRecord::model()->findByPk($linkId);
			if (isset($linkRecord))
			{
				$tabPages = TabPages::getList();
				switch ($linkRecord->type)
				{
					case 'window':
					case 'page':
					case 'quicklist':
						$link = $linkRecord->getModel();
						$this->renderPartial('linkWrapper', array(
							'link' => $link,
							'tabPages' => $tabPages,
						));
						break;
					case 'search':
						$this->renderPartial('../search/searchResultPage', array(
							'tabPages' => $tabPages,
							'parentId' => 'shortcuts'
						));
						break;
					default:
						Yii::app()->end();
						break;
				}
			}
		}

		public function actionGetLibraryPageShortcut()
		{
			$linkId = Yii::app()->request->getPost('linkId');
			if (isset($linkId))
			{
				$linkRecord = ShortcutsLinkRecord::model()->findByPk($linkId);
				$libraryPageShortcut = new PageShortcut($linkRecord);
				$libraryPage = $libraryPageShortcut->getLibraryPage();
				$content = $this->renderPartial('../wallbin/pageContent', array('page' => $libraryPage), true);
				echo $content;
			}
		}
		//------Mobile Site API-----------------------------------------------
	}


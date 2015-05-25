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

		public function actionGetTabs()
		{
			$tabShortcuts = ShortcutsTabRecord::model()->findAll(array('order' => '`order', 'condition' => 'enabled=:enabled', 'params' => array(':enabled' => true)));
			$this->renderPartial('tabs', array('tabShortcuts' => $tabShortcuts));
		}

		public function actionGetPages()
		{
			$tabId = Yii::app()->request->getPost('tabId');
			if (isset($tabId))
			{
				/** @var $tabRecord ShortcutsTabRecord */
				$tabRecord = ShortcutsTabRecord::model()->findByPk($tabId);
				$pageShortcuts = ShortcutsPageRecord::model()->findAll(array('order' => '`order`', 'condition' => 'id_tab=:id_tab', 'params' => array(':id_tab' => $tabId)));
				StatisticActivityRecord::WriteActivity('Shortcuts', 'Tab Changed', array('Tab' => $tabRecord->name));
				$this->renderPartial('pages', array('pageShortcuts' => $pageShortcuts));
			}
		}

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
					if (!isset($predefinedPageType))
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
					$pageModel = $pageRecord->getModel($predefinedPageType);
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
				$content = $this->renderPartial('folderContainerHeader', array('windowShortcut' => $windowShortcut), true);;
				$content .= $this->renderFile(Yii::getPathOfAlias($this->pathPrefix . 'wallbin') . '/folderContainer.php', array('folder' => $folder), true);
				if ($samePage)
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
				if ($samePage)
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
				$content = $this->renderPartial('searchResult', array('searchContainer' => $searchShortcut), true);
				if ($samePage)
					echo $content;
				else
					$this->render('linkWrapper', array('objectId' => $linkId, 'objectName' => $searchShortcut->tooltip, 'objectLogo' => $searchShortcut->ribbonLogoPath, 'content' => $content));
			}
		}

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
				return Yii::app()->getRequest()->sendFile($fileName, @file_get_contents($path));
			}
			Yii::app()->end();
			return null;
		}
	}


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
			if (isset($pageId))
			{
				$linkRecords = ShortcutsLinkRecord::model()->findAll(array('order' => '`order`', 'condition' => 'id_page=:id_page', 'params' => array(':id_page' => $pageId)));
				/** @var $pageRecord ShortcutsPageRecord */
				$pageRecord = ShortcutsPageRecord::model()->findByPk($pageId);
				if (isset($linkRecords) && isset($pageRecord))
				{
					/** @var $tabRecord ShortcutsTabRecord */
					$tabRecord = ShortcutsTabRecord::model()->findByPk($pageRecord->id_tab);
					StatisticActivityRecord::WriteActivity('Shortcuts', 'Page Changed', array('Tab' => $tabRecord->name, 'Button' => $pageRecord->name));
					$this->renderPartial('page', array('pageRecord' => $pageRecord, 'linkRecords' => $linkRecords,));
				}
			}
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
					$this->render('linkWrapper', array('objectName' => 'Window', 'objectLogo' => $windowShortcut->ribbonLogoPath, 'content' => $content));
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
					$this->render('linkWrapper', array('objectName' => 'Quick List', 'objectLogo' => $quickListShortcut->ribbonLogoPath, 'content' => $content));
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
				/** @var $pageRecord ShortcutsPageRecord */
				$pageRecord = ShortcutsPageRecord::model()->findByPk($linkRecord->id_page);
				$searchShortcut = new SearchShortcut($linkRecord);
				$searchShortcut->loadSearchConditions();
				$this->pageTitle = $searchShortcut->tooltip;
				$content = $this->renderPartial('searchResult', array('searchContainer' => $searchShortcut), true);
				if ($samePage)
					echo $content;
				else
				{
					if (!$searchShortcut->showResultsBar)
					{
						$homeBar = $pageRecord->getHomeBar();
						$content = $this->renderPartial('homeBar', array('homeBar' => $homeBar, 'enableSearchBar' => false), true) . $content;
					}
					$this->render('linkWrapper', array('objectName' => $searchShortcut->tooltip, 'objectLogo' => $searchShortcut->ribbonLogoPath, 'content' => $content));
				}
			}
		}

		public function actionGetQuickSearchResult()
		{
			$pageId = Yii::app()->request->getQuery('pageId');
			$text = Yii::app()->request->getQuery('text');
			$onlyFiles = intval(Yii::app()->request->getQuery('onlyFiles'));
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
				$searchBar = $pageRecord->getSearchBar();
				$this->pageTitle = $searchBar->title;
				$searchBar->conditions->text = $text;
				$searchBar->conditions->searchByContent = $onlyFiles == 0;
				$searchBar->conditions->fileTypes = $fileTypes;
				$searchBar->conditions->superFilters = $superFilters;

				unset($searchBar->conditions->categories);
				foreach ($categories as $arrayItem)
				{
					$category = (object)$arrayItem;
					$searchBar->conditions->categories[] = $category;
				}
				$content = $this->renderPartial('searchResult', array('searchContainer' => $searchBar), true);
				$this->render('linkWrapper', array('objectName' => $searchBar->title, 'content' => $content));
			}
		}
	}

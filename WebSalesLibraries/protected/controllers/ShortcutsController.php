<?php
	class ShortcutsController extends IsdController
	{
		public function getViewPath()
		{
			return YiiBase::getPathOfAlias($this->pathPrefix . 'shortcuts');
		}

		public function actionGetPage()
		{
			$pageId = Yii::app()->request->getPost('pageId');
			if (isset($pageId))
			{
				$linkRecords = ShortcutsLinkStorage::model()->findAll(array('order' => '`order`', 'condition' => 'id_page=:id_page', 'params' => array(':id_page' => $pageId)));
				$pageRecord = ShortcutsPageStorage::model()->findByPk($pageId);
				if (isset($linkRecords) && isset($pageRecord))
				{
					$tabRecord = ShortcutsTabStorage::model()->findByPk($pageRecord->id_tab);
					StatisticActivityStorage::WriteActivity('Shortcuts', 'Page Changed', array('Tab' => $tabRecord->name, 'Button' => $pageRecord->name));
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
				$linkRecord = ShortcutsLinkStorage::model()->findByPk($linkId);
				$windowShortcut = new WindowShortcut($linkRecord);
				$folder = $windowShortcut->getWindow();
				$content = $this->renderFile(Yii::getPathOfAlias('application.views.regular.shortcuts') . '/folderContainerHeader.php', array('windowShortcut' => $windowShortcut), true);;
				$content .= $this->renderFile(Yii::getPathOfAlias('application.views.regular.wallbin') . '/folderContainer.php', array('folder' => $folder), true);
				if ($samePage)
					echo $content;
				else
					$this->render('linkWrapper', array('objectName' => 'Window', 'content' => $content));
			}
		}

		public function actionGetQuickList()
		{
			$this->pageTitle = 'Shortcuts - Quick List';
			$linkId = Yii::app()->request->getQuery('linkId');
			$samePage = (bool)Yii::app()->request->getQuery('samePage');
			if (isset($linkId))
			{
				$linkRecord = ShortcutsLinkStorage::model()->findByPk($linkId);
				$quickListShortcut = new QuickListShortcut($linkRecord);
				$quickListShortcut->loadQuickLinks();
				$content = $this->renderFile(Yii::getPathOfAlias('application.views.regular.shortcuts') . '/quickList.php', array('quickListShortcut' => $quickListShortcut), true);
				if ($samePage)
					echo $content;
				else
					$this->render('linkWrapper', array('objectName' => 'Quick List', 'content' => $content));
			}
		}

		public function actionGetSearchShortcut()
		{
			$this->pageTitle = 'Shortcuts - Search';
			$linkId = Yii::app()->request->getQuery('linkId');
			$samePage = (bool)Yii::app()->request->getQuery('samePage');
			if (isset($linkId))
			{
				$linkRecord = ShortcutsLinkStorage::model()->findByPk($linkId);
				$searchShortcut = new SearchShortcut($linkRecord);
				$searchShortcut->loadSearchConditions();
				$content = $this->renderFile(Yii::getPathOfAlias('application.views.regular.shortcuts') . '/searchResult.php', array('searchContainer' => $searchShortcut), true, true);
				if ($samePage)
					echo $content;
				else
					$this->render('linkWrapper', array('objectName' => 'Search', 'content' => $content));
			}
		}

		public function actionGetQuickSearchResult()
		{
			$pageId = Yii::app()->request->getQuery('pageId');
			$text = Yii::app()->request->getQuery('text');
			$fileTypes = Yii::app()->request->getQuery('fileTypes');
			if (isset($fileTypes))
				$fileTypes = CJSON::decode($fileTypes);
			else
				$fileTypes = array();
			if (isset($pageId))
			{
				$pageRecord = ShortcutsPageStorage::model()->findByPk($pageId);
				$searchBar = $pageRecord->getSearchBar();
				$this->pageTitle = $searchBar->title;
				$searchBar->conditions->text = $text;
				$searchBar->conditions->fileTypes = $fileTypes;
				$content = $this->renderFile(Yii::getPathOfAlias('application.views.regular.shortcuts') . '/searchResult.php', array('searchContainer' => $searchBar), true, true);
				$this->render('linkWrapper', array('objectName' => $searchBar->title, 'content' => $content));
			}
		}
	}

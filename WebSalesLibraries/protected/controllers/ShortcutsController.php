<?php
	Yii::import('application.extensions.phpQuery.phpQuery.phpQuery');

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
		public function actionGetSinglePage()
		{
			$linkId = Yii::app()->request->getQuery('linkId');
			/** @var  $shortcutRecord ShortcutLinkRecord */
			$shortcutRecord = ShortcutLinkRecord::model()->findByPk($linkId);
			/** @var  $shortcut PageContentShortcut */
			$shortcut = $shortcutRecord->getModel($this->isPhone);

			$this->pageTitle = sprintf('%s - %s', $shortcut->title, $shortcut->description);

			$menuGroups = ShortcutsManager::getAvailableGroups($this->isPhone);

			$this->render('pages/singlePage', array('menuGroups' => $menuGroups, 'shortcut' => $shortcut));
		}

		public function actionGetSamePage()
		{
			$linkId = Yii::app()->request->getPost('linkId');
			$linkParameters = Yii::app()->request->getPost('parameters');

			echo CJSON::encode($this->buildShortcutPage($linkId, $linkParameters));
		}

		public function actionGetShortcutData()
		{
			$shortcutType = Yii::app()->request->getPost('shortcutType');
			/** @var  $shortcutRecord ShortcutLinkRecord */
			$shortcutRecord = ShortcutLinkRecord::model()->find('type=?', array($shortcutType));
			/** @var  $shortcut BaseShortcut */
			$shortcut = $shortcutRecord->getModel($this->isPhone);
			echo $shortcut->getMenuItemData();
		}

		public function actionDownload()
		{
			$linkId = Yii::app()->request->getQuery('linkId');
			if (isset($linkId))
			{
				/**@var $linkRecord ShortcutLinkRecord */
				$linkRecord = ShortcutLinkRecord::model()->findByPk($linkId);
				/**@var $link DownloadShortcut */
				$link = $linkRecord->getModel($this->isPhone);
				$fileName = $link->fileName;
				$path = $link->sourcePath;
				Yii::app()->getRequest()->sendFile($fileName, @file_get_contents($path));
			}
			Yii::app()->end();
		}

		/**
		 * @param $linkId string
		 * @param $parameters array
		 * @return array
		 */
		private function buildShortcutPage($linkId, $parameters)
		{
			$pageContentBundle = array();

			/** @var  $shortcutRecord ShortcutLinkRecord */
			$shortcutRecord = ShortcutLinkRecord::model()->findByPk($linkId);
			/** @var  $shortcut PageContentShortcut */
			$shortcut = $shortcutRecord->getModel($this->isPhone, $parameters);


			switch ($shortcut->type)
			{
				case 'gridbundle':
				case 'carouselbundle':
				case 'library':
				case 'page':
				case 'window':
				case 'search':
				case 'searchapp':
				case 'qbuilder':
				case 'favorites':
				case 'quizzes':
					if ($shortcut->samePage)
					{
						$defaultShortcutTagName = 'default-shortcut';
						Yii::app()->session[$defaultShortcutTagName] = sprintf('%s', $shortcutRecord->getUniqueId());
					}
					break;
			}

			$viewName = '';
			$useMobileWrapper = true;
			switch ($shortcut->type)
			{
				case 'gridbundle':
				case 'carouselbundle':
					/** @var $shortcut  BundleShortcut */
					$shortcut->getLinks();
					$viewName = $shortcut->viewName;
					break;
				case 'video':
					$viewName = 'video';
					break;
				case 'search':
					$useMobileWrapper = false;
					$viewName = 'searchLink';
					break;
				case 'quicklist':
					$viewName = 'quickList';
					break;
				case 'window':
					$viewName = 'libraryWindow';
					break;
				case 'page':
					$viewName = 'libraryPage';
					break;
				case 'qpage':
					$useMobileWrapper = false;
					$viewName = 'qpage';
					break;
				case 'download':
					$viewName = 'downloadDialog';
					break;
				case 'library':
					$useMobileWrapper = false;
					$viewName = 'library';
					break;
				case 'searchapp':
					$useMobileWrapper = false;
					$viewName = 'searchApp';
					break;
				case 'qbuilder':
					$useMobileWrapper = false;
					$viewName = 'qbuilder';
					break;
				case 'quizzes':
					$useMobileWrapper = false;
					$viewName = 'quizzes';
					break;
				case 'favorites':
					$useMobileWrapper = false;
					$viewName = 'favorites';
					break;
			}

			if ($viewName != '')
			{
				if ($this->isPhone && $useMobileWrapper)
					$content = $this->renderPartial(
						'pages/pageWrapper',
						array_merge(
							$shortcut->getViewParameters(),
							array('shortcutContent' => $this->renderPartial('pages/' . $viewName, $shortcut->getViewParameters(), true))
						),
						true);
				else
					$content = $this->renderPartial('pages/' . $viewName, $shortcut->getViewParameters(), true);

				$actions = !$this->isPhone ?
					$this->renderPartial('../menu/actionItems', array('actionContainer' => $shortcut), true) :
					'';

				$navigationPanel = '';
				if ($shortcut instanceof PageContentShortcut)
				{
					/** @var PageContentShortcut $shortcut */
					$navigationPanel = $shortcut->getNavigationPanel();
					if (isset($navigationPanel))
						$navigationPanel = $this->renderPartial('navigationPanel/itemsList', array('navigationPanel' => $navigationPanel), true);
				}

				$pageContentBundle = array(
					'content' => $content,
					'actions' => $actions,
					'navigationPanel' => $navigationPanel,
					'options' => $shortcut->getPageData()
				);
			}
			return $pageContentBundle;
		}
		//------Common Site API-------------------------------------------

		//------Regular Site API-------------------------------------------
		public function actionCheckShortcutsUpdated()
		{
			$menuDate = strtotime(Yii::app()->request->getPost('menuDate'));
			$actualDate = ShortcutsManager::getLastUpdate();
			echo CJSON::encode(array(
				'needUpdate' => $menuDate < $actualDate,
				'lastUpdate' => date(Yii::app()->params['sourceDateFormat'])
			));
		}

		public function actionGetDownloadDialog()
		{
			$linkId = Yii::app()->request->getPost('linkId');
			if (isset($linkId))
			{
				/** @var $linkRecord ShortcutLinkRecord */
				$linkRecord = ShortcutLinkRecord::model()->findByPk($linkId);
				/**@var $link DownloadShortcut */
				$link = $linkRecord->getModel($this->isPhone);
				$this->renderPartial('downloadDialog', array('link' => $link));
			}
		}

		public function actionGetQuickSearchResult()
		{
			$bundleId = Yii::app()->request->getQuery('bundleId');
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

			if (isset($bundleId))
			{
				/** @var $linkRecord ShortcutLinkRecord */
				$linkRecord = ShortcutLinkRecord::model()->findByPk($bundleId);
				/** @var $bundle BundleShortcut */
				$bundle = $linkRecord->getModel($this->isPhone);
				$searchBar = $bundle->searchBar;
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
				$menuGroups = ShortcutsManager::getAvailableGroups($this->isPhone);
				$this->render('searchBar/searchBarResultsPage', array('menuGroups' => $menuGroups, 'searchBar' => $searchBar, 'bundleId' => $bundleId));
			}
		}

		public function actionGetSubSearchCustomPanel()
		{
			$bundleId = Yii::app()->request->getPost('bundleId');
			$linkId = Yii::app()->request->getPost('linkId');

			if (isset($bundleId) && !isset($linkId))
			{
				/** @var $linkRecord ShortcutLinkRecord */
				$linkRecord = ShortcutLinkRecord::model()->findByPk($bundleId);
				/** @var $bundle BundleShortcut */
				$bundle = $linkRecord->getModel($this->isPhone);
				$searchBar = $bundle->searchBar;
			}
			else
			{
				/** @var $linkRecord ShortcutLinkRecord */
				$linkRecord = ShortcutLinkRecord::model()->findByPk($linkId);
				/** @var $searchLink SearchLinkShortcut */
				$searchLink = $linkRecord->getModel($this->isPhone);
				$searchBar = $searchLink->subSearchBar;
			}
			$this->renderPartial('subSearchBar/customSearchPanel', array('searchBar' => $searchBar));
		}

		public function actionGetSubSearchTemplatesPanel()
		{
			$bundleId = Yii::app()->request->getPost('bundleId');
			$linkId = Yii::app()->request->getPost('linkId');

			if (isset($bundleId) && !isset($linkId))
			{
				/** @var $linkRecord ShortcutLinkRecord */
				$linkRecord = ShortcutLinkRecord::model()->findByPk($bundleId);
				/** @var $bundle BundleShortcut */
				$bundle = $linkRecord->getModel($this->isPhone);
				$templates = $bundle->searchBar->subConditions;
				$id = $bundleId;
			}
			else
			{
				/** @var $linkRecord ShortcutLinkRecord */
				$linkRecord = ShortcutLinkRecord::model()->findByPk($linkId);
				$link = new SearchLinkShortcut($linkRecord, $this->isPhone);
				$templates = $link->subConditions;
				$id = $linkId;
			}
			$this->renderPartial('subSearchBar/searchTemplatesPanel', array('templates' => $templates, 'id' => $id));
		}

		public function actionEditSearchBarSettings()
		{
			$this->renderPartial('searchBar/settings');
		}

		public function actionConfirmSearchBarSearch()
		{
			$this->renderPartial('searchBar/confirmation');
		}

		public function actionSetSuperGroup()
		{
			$superGroupTag = Yii::app()->request->getPost('superGroupTag');
			ShortcutsManager::setSelectedSuperGroup($superGroupTag);
		}
		//------Regular Site API-------------------------------------------

		//------Mobile Site API-----------------------------------------------
		public function actionGetGroupContent()
		{
			$groupId = Yii::app()->request->getQuery('groupId');
			/** @var  $groupRecord ShortcutGroupRecord */
			$groupRecord = ShortcutGroupRecord::model()->findByPk($groupId);
			if (isset($groupRecord))
			{
				$groupModel = new ShortcutGroup($groupRecord, null, $this->isPhone);
				$this->render('groups/groupContent', array('group' => $groupModel));
			}
			else
				$this->redirect(Yii::app()->createAbsoluteUrl('site/index'));
		}
		//------Mobile Site API-----------------------------------------------
	}


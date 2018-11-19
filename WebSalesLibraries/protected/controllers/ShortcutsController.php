<?
	try
	{
		Yii::import('application.extensions.phpQuery.phpQuery.phpQuery');
	}
	catch (CException $e)
	{
	}

	/**
	 * Class ShortcutsController
	 */
	class ShortcutsController extends IsdController
	{
		/** return array */
		protected function getPublicActionIds()
		{
			return array(
				'getSinglePage',
			);
		}

		public function getViewPath()
		{
			return YiiBase::getPathOfAlias($this->pathPrefix . 'shortcuts');
		}

		//------Common Site API-------------------------------------------
		public function actionGetSinglePage()
		{
			$shortcutId = Yii::app()->request->getQuery('linkId');
			$useForThumbnail = Yii::app()->request->getQuery('useForThumbnail') !== null;
			/** @var  $shortcutRecord ShortcutLinkRecord */
			$shortcutRecord = ShortcutLinkRecord::model()->findByPk($shortcutId);
			if (isset($shortcutRecord))
			{
				/** @var  $shortcut PageContentShortcut */
				$shortcut = $shortcutRecord->getRegularModel($this->isPhone, $_GET);
				$shortcut->loadPageConfig();

				if ($useForThumbnail || UserIdentity::isUserAuthorized() || ($shortcut->allowPublicAccess && !isset($shortcut->publicPassword)))
					$this->renderSinglePage($shortcut);
				else if ($shortcut->allowPublicAccess && isset($shortcut->publicPassword))
				{
					$passwordSessionKey = sprintf('shortcutPassword%s', $shortcutId);
					$savedPassword = Yii::app()->session[$passwordSessionKey];
					if ($shortcut->publicPassword == $savedPassword)
						$this->renderSinglePage($shortcut);
					else
					{
						$passwordModel = new PublicPageContentShortcutPasswordForm();
						$passwordModel->shortcutId = $shortcutId;
						$passwordModel->isPhone = $shortcutId;

						$attributes = Yii::app()->request->getPost('PublicPageContentShortcutPasswordForm');
						if (isset($attributes))
						{
							$passwordModel->attributes = $attributes;
							$passwordModel->password = $attributes['password'];
							if ($passwordModel->validate())
							{
								Yii::app()->session[$passwordSessionKey] = $passwordModel->password;
								$this->renderSinglePage($shortcut);
							}
							else
								$this->render('publicLogin', array('formData' => $passwordModel, 'shortcut' => $shortcut));
						}
						else
							$this->render('publicLogin', array('formData' => $passwordModel, 'shortcut' => $shortcut));
					}
				}
				else
					Yii::app()->user->loginRequired();
			}
		}

		/**
		 * @param $shortcut BaseShortcut
		 */
		protected function renderSinglePage($shortcut)
		{
			$this->pageTitle = sprintf('%s - %s', $shortcut->title, $shortcut->description);
			if (UserIdentity::isUserAuthorized())
				$menuGroups = ShortcutsManager::getAvailableGroups($this->isPhone);
			else
				$menuGroups = array();
			$this->render('pages/singlePage', array('menuGroups' => $menuGroups, 'shortcut' => $shortcut));
		}

		public function actionGetSamePage()
		{
			$linkId = Yii::app()->request->getPost('linkId');
			$linkParameters = Yii::app()->request->getPost('parameters');
			$screenSettings = Yii::app()->request->getPost('screenSettings');

			echo CJSON::encode($this->buildShortcutPage($linkId, $linkParameters, $screenSettings));
		}

		public function actionGetShortcutDataById()
		{
			$shortcutId = Yii::app()->request->getPost('linkId');
			/** @var  $shortcutRecord ShortcutLinkRecord */
			$shortcutRecord = ShortcutLinkRecord::model()->findByPk($shortcutId);
			/** @var  $shortcut BaseShortcut */
			$shortcut = $shortcutRecord->getRegularModel($this->isPhone);
			echo $shortcut->getMenuItemData();
		}

		public function actionGetShortcutDataByType()
		{
			$shortcutType = Yii::app()->request->getPost('shortcutType');
			/** @var  $shortcutRecord ShortcutLinkRecord */
			$shortcutRecord = ShortcutLinkRecord::model()->find('type=?', array($shortcutType));
			/** @var  $shortcut BaseShortcut */
			$shortcut = $shortcutRecord->getRegularModel($this->isPhone);
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
				$link = $linkRecord->getRegularModel($this->isPhone);
				$fileName = $link->fileName;
				$path = $link->sourcePath;
				try
				{
					Yii::app()->getRequest()->sendFile($fileName, @file_get_contents($path));
				}
				catch (CHttpException $e)
				{
				}
			}
			Yii::app()->end();
		}

		/**
		 * @param $linkId string
		 * @param $parameters array
		 * @param $screenSettings array
		 * @return array
		 */
		private function buildShortcutPage($linkId, $parameters, $screenSettings)
		{
			$pageContentBundle = array();

			/** @var  $shortcutRecord ShortcutLinkRecord */
			$shortcutRecord = ShortcutLinkRecord::model()->findByPk($linkId);
			/** @var  $shortcut CustomHandledShortcut */
			$shortcut = $shortcutRecord->getRegularModel($this->isPhone, $parameters);
			if ($shortcut instanceof PageContentShortcut)
				$shortcut->loadPageConfig();

			switch ($shortcut->type)
			{
				case 'gridbundle':
				case 'carouselbundle':
				case 'library':
				case 'page':
				case 'pagebundle':
				case 'window':
				case 'search':
				case 'searchapp':
				case 'qbuilder':
				case 'starssteals':
				case 'rrq1':
				case 'favorites':
				case 'quizzes':
				case 'landing':
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
				case 'youtube':
					$viewName = 'youtube';
					break;
				case 'vimeo':
					$viewName = 'vimeo';
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
				case 'pagebundle':
					$useMobileWrapper = false;
					$viewName = 'libraryPageBundle';
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
				case 'starssteals':
					$useMobileWrapper = false;
					$viewName = 'starsteals';
					break;
				case 'rrq1':
					$useMobileWrapper = false;
					$viewName = 'salesRequests';
					break;
				case 'quizzes':
					$useMobileWrapper = false;
					$viewName = 'quizzes';
					break;
				case 'favorites':
					$useMobileWrapper = false;
					$viewName = 'favorites';
					break;
				case 'landing':
					$viewName = 'landingPage';
					break;
			}

			if ($viewName != '')
			{
				$viewParameters = $shortcut->getViewParameters();
				$viewParameters = array_merge($viewParameters, array('screenSettings' => $screenSettings));
				if ($this->isPhone && $useMobileWrapper)
					$content = $this->renderPartial(
						'pages/pageWrapper',
						array_merge(
							$viewParameters,
							array('shortcutContent' => $this->renderPartial('pages/' . $viewName, $viewParameters, true))
						),
						true);
				else
					$content = $this->renderPartial('pages/' . $viewName, $viewParameters, true);

				$actions = null;
				$navigationPanel = null;
				$fixedPanels = null;

				if ($shortcut instanceof PageContentShortcut)
				{
					/** @var PageContentShortcut $shortcut */
					$actions = !$this->isPhone ?
						$this->renderPartial('../menu/actionItems', array('actionContainer' => $shortcut), true) :
						'';

					$navigationPanelData = $shortcut->getNavigationPanel();
					if (isset($navigationPanelData))
					{
						$navigationPanel = array(
							'content' => $this->renderPartial('navigationPanel/itemsList', array('navigationPanel' => $navigationPanelData), true),
							'options' => array(
								'id' => $navigationPanelData->id,
								'expanded' => $navigationPanelData->isExpanded,
								'hideCondition' => array(
									'extraSmall' => $navigationPanelData->hideCondition->extraSmall,
									'small' => $navigationPanelData->hideCondition->small,
									'medium' => $navigationPanelData->hideCondition->medium,
									'large' => $navigationPanelData->hideCondition->large,
								)
							)
						);
					}

					$fixedPanelSettings = $shortcut->fixedPanelSettings;
					if (isset($fixedPanelSettings))
					{
						$fixedPanels = array();
						if (isset($fixedPanelSettings->topPanel))
						{
							$fixedPanels['top'] = array(
								'content' => $this->renderPartial('landingPageMarkup/common/fixedPanel',
									array(
										'markup' => $fixedPanelSettings->topPanel,
										'height' => $fixedPanelSettings->topHeight,
										'isTop' => true,
										'screenSettings' => $screenSettings,
									), true),
								'height' => $fixedPanelSettings->topHeight
							);
						}
						if (isset($fixedPanelSettings->bottomPanel))
						{
							$fixedPanels['bottom'] = array(
								'content' => $this->renderPartial('landingPageMarkup/common/fixedPanel',
									array(
										'markup' => $fixedPanelSettings->bottomPanel,
										'height' => $fixedPanelSettings->bottomHeight,
										'isTop' => false,
										'screenSettings' => $screenSettings,
									), true),
								'height' => $fixedPanelSettings->bottomHeight
							);
						}
					}
				}

				$pageContentBundle = array(
					'content' => $content,
					'actions' => $actions,
					'navigationPanel' => $navigationPanel,
					'fixedPanels' => $fixedPanels,
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
				$link = $linkRecord->getRegularModel($this->isPhone);
				try
				{
					$this->renderPartial('downloadDialog', array('link' => $link));
				}
				catch (CException $e)
				{
				}
			}
		}

		public function actionGetQuickSearchResult()
		{
			$linkId = Yii::app()->request->getQuery('linkId');
			$text = Yii::app()->request->getQuery('text');
			$onlyFiles = filter_var(trim(Yii::app()->request->getQuery('onlyFiles')), FILTER_VALIDATE_BOOLEAN);
			$onlyNewFiles = filter_var(trim(Yii::app()->request->getQuery('onlyNewFiles')), FILTER_VALIDATE_BOOLEAN);
			$fileTypes = Yii::app()->request->getQuery('fileTypesInclude');
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

			if (isset($linkId))
			{
				/** @var $linkRecord ShortcutLinkRecord */
				$linkRecord = ShortcutLinkRecord::model()->findByPk($linkId);
				/** @var $searchBarContainer ContainerShortcut */
				$searchBarContainer = $linkRecord->getRegularModel($this->isPhone);
				$searchBarContainer->loadPageConfig();
				$searchBar = $searchBarContainer->getSearchBar();
				$this->pageTitle = $searchBar->title;
				$searchBar->conditions->text = $text;
				$searchBar->conditions->searchByContent = $onlyFiles;

				if ($onlyNewFiles)
				{
					$searchBar->conditions->startDate = date('Y-m-d', strtotime('-1 year'));
					$searchBar->conditions->endDate = date("Y-m-d");
				}

				$searchBar->conditions->fileTypesInclude = $fileTypes;
				$searchBar->conditions->superFilters = $superFilters;

				unset($searchBar->conditions->categories);
				foreach ($categories as $arrayItem)
				{
					$category = (object)$arrayItem;
					$searchBar->conditions->categories[] = $category;
				}
				$menuGroups = ShortcutsManager::getAvailableGroups($this->isPhone);
				$this->render('searchBar/searchBarResultsPage', array('menuGroups' => $menuGroups, 'searchBar' => $searchBar, 'linkId' => $linkId));
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
				/** @var $searchBarContainer ContainerShortcut */
				$searchBarContainer = $linkRecord->getRegularModel($this->isPhone);
				$searchBarContainer->loadPageConfig();
				$searchBar = $searchBarContainer->searchBar;
			}
			else
			{
				/** @var $linkRecord ShortcutLinkRecord */
				$linkRecord = ShortcutLinkRecord::model()->findByPk($linkId);
				/** @var $searchLink SearchLinkShortcut */
				$searchLink = $linkRecord->getRegularModel($this->isPhone);
				$searchLink->loadPageConfig();
				$searchBar = $searchLink->subSearchBar;
			}
			try
			{
				$this->renderPartial('subSearchBar/customSearchPanel', array('searchBar' => $searchBar));
			}
			catch (CException $e)
			{
			}
		}

		public function actionGetSubSearchTemplatesPanel()
		{
			$bundleId = Yii::app()->request->getPost('bundleId');
			$linkId = Yii::app()->request->getPost('linkId');
			if (isset($bundleId) && !isset($linkId))
			{
				/** @var $linkRecord ShortcutLinkRecord */
				$linkRecord = ShortcutLinkRecord::model()->findByPk($bundleId);
				/** @var $searchBarContainer ContainerShortcut */
				$searchBarContainer = $linkRecord->getRegularModel($this->isPhone);
				$searchBarContainer->loadPageConfig();
				$templates = $searchBarContainer->searchBar->subConditions;
				$id = $bundleId;
			}
			else
			{
				/** @var $linkRecord ShortcutLinkRecord */
				$linkRecord = ShortcutLinkRecord::model()->findByPk($linkId);
				$link = new SearchLinkShortcut($linkRecord, $this->isPhone);
				$link->loadPageConfig();
				$templates = $link->subConditions;
				$id = $linkId;
			}
			try
			{
				$this->renderPartial('subSearchBar/searchTemplatesPanel', array('templates' => $templates, 'id' => $id));
			}
			catch (CException $e)
			{
			}
		}

		public function actionEditSearchBarSettings()
		{
			try
			{
				$this->renderPartial('searchBar/settings');
			}
			catch (CException $e)
			{
			}
		}

		public function actionConfirmSearchBarSearch()
		{
			try
			{
				$this->renderPartial('searchBar/confirmation');
			}
			catch (CException $e)
			{
			}
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


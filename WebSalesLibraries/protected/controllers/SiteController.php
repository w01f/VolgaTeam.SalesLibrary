<?php

	class SiteController extends IsdController
	{
		public $defaultAction = 'index';

		public function getViewPath()
		{
			return YiiBase::getPathOfAlias($this->pathPrefix . 'site');
		}

		public function actionIndex()
		{
			$menuGroups = ShortcutsManager::getAvailableGroups($this->isPhone);
			$this->pageTitle = Yii::app()->name;

			if ($this->isPhone)
			{
				$defaultGroup = $menuGroups[0];
				$this->render('../shortcuts/groups/groupContent', array('group' => $defaultGroup));
			}
			else
			{
				$defaultShortcutId = isset(Yii::app()->session['default-shortcut']) ? Yii::app()->session['default-shortcut'] : null;
				$defaultShortcut = isset($defaultShortcutId) ? ShortcutLinkRecord::getModelByUniqueId($defaultShortcutId, $this->isPhone) : null;
				if (!isset($defaultShortcut))
					foreach ($menuGroups as $menuGroup)
					{
						if ($menuGroup->enabled == true)
							foreach ($menuGroup->menuItems as $menuItem)
							{
								/** @var  $shortcut PageContentShortcut */
								$shortcut = $menuItem->shortcut;
								if ($shortcut->enabled == true)
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
											$defaultShortcut = $shortcut;
											break;
										default:
											continue;
									}
								if (isset($defaultShortcut))
									break;
							}
						if (isset($defaultShortcut))
							break;
					}
				$this->render('index', array('menuGroups' => $menuGroups, 'defaultShortcut' => $defaultShortcut));
			}
		}

		public function actionGetMenu()
		{
			$menuGroups = ShortcutsManager::getAvailableGroups($this->isPhone);
			$this->renderPartial('../menu/menuPopupContent', array('menuGroups' => $menuGroups));
		}

		public function actionBadBrowser()
		{
			$this->render('badBrowser');
		}

		public function actionLogin()
		{
			$this->redirect($this->createUrl('auth/login'));
		}

		public function actionError()
		{
			if ($error = Yii::app()->errorHandler->error)
			{
				$this->pageTitle = Yii::app()->name . ' - Error';
				if (Yii::app()->request->isAjaxRequest)
					echo $error['message'];
				else
					$this->render('errorMessage', $error);
			}
		}

		public function actionSwitchVersion()
		{
			$version = Yii::app()->request->getPost('siteVersion');
			if (isset($version))
			{
				Yii::app()->cacheDB->set('siteVersion', $version, (60 * 60 * 24 * 7));
				Yii::app()->end();
			}
		}
	}

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
			$tabPages = TabPages::getList();

			if ($this->isPhone)
			{
				if (Yii::app()->params['jqm_home_page_enabled'] == true)
				{
					if (isset(Yii::app()->user))
					{
						$userId = Yii::app()->user->getId();
						$userLibraryTabs = UserTabRecord::getLibraryTabs($userId);
					}
					else
						$userLibraryTabs = array();

					$this->render('index', array(
						'tabPages' => $tabPages,
						'userLibraryPages' => $userLibraryTabs
					));
				}
				else
				{
					foreach ($tabPages as $tabName => $tabIndex)
					{
						$this->redirect(TabPages::getTabUrl($tabName));
						break;
					}
				}
			}
			else
			{
				$this->pageTitle = Yii::app()->name;
				$tickerRecords = TickerLinkRecord::getLinks();
				$this->render('index', array('tabPages' => $tabPages, 'tickerRecords' => $tickerRecords));
			}
		}

		public function actionEmpty()
		{
			$tabPages = TabPages::getList();
			$this->render('index', array('tabPages' => $tabPages));
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
	}

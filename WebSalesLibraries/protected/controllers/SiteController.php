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
			$this->pageTitle = Yii::app()->name;
			$tickerRecords = TickerLinkRecord::getLinks();
			$this->render('index', array('tickerRecords' => $tickerRecords));
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

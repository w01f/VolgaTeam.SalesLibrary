<?php

	/**
	 * Class BehaviorManager
	 */
	class BehaviorManager extends CBehavior
	{
		/**
		 * @param CComponent $owner
		 */
		public function attach($owner)
		{
			$owner->attachEventHandler('onBeginRequest', array($this, 'checkBrowser'));
			$owner->attachEventHandler('onBeginRequest', array($this, 'checkLoginRequired'));
		}

		public function checkBrowser()
		{
			if (!strstr(Yii::app()->request->getUrl(), 'site/badBrowser')
				&& !strstr(Yii::app()->request->getUrl(), '/quote')
			)
			{
				try
				{
					$browser = Yii::app()->browser->getBrowser();
					$version = intval(Yii::app()->browser->getVersion());
					if ($browser == Browser::BROWSER_IE && $version <= 8 && $version != 7)
						Yii::app()->request->redirect("site/badBrowser");
				} catch (Exception $e)
				{
				}
			}
		}

		public function checkLoginRequired()
		{
			$url = strtolower(Yii::app()->request->getUrl());
			if (Yii::app()->user->isGuest &&
				$url != '' &&
				!strstr($url, 'auth/') &&
				!strstr($url, 'site/badBrowser') &&
				!strstr($url, 'site/login') &&
				!strstr($url, 'site/switchVersion') &&
				!strstr($url, 'qpage/show') &&
				!strstr($url, 'qpage/recordActivity') &&
				!strstr($url, 'qpage/getPublic') &&
				!strstr($url, 'qpage/preview') &&
				!strstr($url, 'qpage/site') &&
				!strstr($url, 'qpage/wallbin') &&
				!strstr($url, 'admin/') &&
				!strstr($url, 'utility/') &&
				!strstr($url, 'statistic/quote') &&
				!strstr($url, 'qbuilder/quote') &&
				!strstr($url, 'inactiveusers/') &&
				!strstr($url, 'filemanagerdata/') &&
				!strstr($url, 'adsalesdata/') &&
				!strstr($url, 'content/')
			)
				Yii::app()->user->loginRequired();
		}
	}
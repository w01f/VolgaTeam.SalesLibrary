<?php

	/**
	 * Class RequireLogin
	 */
	class RequireLogin extends CBehavior
	{
		/**
		 * @param CComponent $owner
		 */
		public function attach($owner)
		{
			$owner->attachEventHandler('onBeginRequest', array($this, 'handleBeginRequest'));
		}

		public function handleBeginRequest()
		{
			if (Yii::app()->user->isGuest &&
				!strstr(Yii::app()->request->getUrl(), 'auth/') &&
				!strstr(Yii::app()->request->getUrl(), 'site/login') &&
				!strstr(Yii::app()->request->getUrl(), 'site/emailLinkGet') &&
				!strstr(Yii::app()->request->getUrl(), 'site/switchVersion') &&
				!strstr(Yii::app()->request->getUrl(), 'qbuilder/quote') &&
				!strstr(Yii::app()->request->getUrl(), 'qpage/show') &&
				!strstr(Yii::app()->request->getUrl(), 'qpage/recordActivity') &&
				!strstr(Yii::app()->request->getUrl(), 'qpage/getPublic') &&
				!strstr(Yii::app()->request->getUrl(), 'qpage/preview') &&
				!strstr(Yii::app()->request->getUrl(), 'qpage/site') &&
				!strstr(Yii::app()->request->getUrl(), 'qpage/wallbin') &&
				!strstr(Yii::app()->request->getUrl(), 'admin/') &&
				!strstr(Yii::app()->request->getUrl(), 'statistic/quote') &&
				!strstr(Yii::app()->request->getUrl(), 'ticker/') &&
				!strstr(Yii::app()->request->getUrl(), 'utility/') &&
				!strstr(Yii::app()->request->getUrl(), 'inactiveusers/') &&
				!strstr(Yii::app()->request->getUrl(), 'content/')
			)
			{
				Yii::app()->user->loginRequired();
			}
		}
	}
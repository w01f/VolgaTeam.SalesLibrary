<?php

	/**
	 * Class IsdController
	 */
	class IsdController extends CController
	{
		public $browser;
		public $pathPrefix;
		public $isPhone;

		public function init()
		{
			//Yii::app()->browser->setUserAgent('Mozilla/5.0 (iPhone; CPU iPhone OS 5_0 like Mac OS X) AppleWebKit/534.46 (KHTML, like Gecko) Version/5.1 Mobile/9A334 Safari/7534.48.3');
			//Yii::app()->browser->setUserAgent('Mozilla/5.0 (Linux; U; Android 4.2.2; es-us; GT-P5210 Build/JDQ39) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 Safari/534.30/1.05v.3406.d7');
			$this->browser = Yii::app()->browser->getBrowser();
			switch ($this->browser)
			{
				case Browser::BROWSER_IPHONE:
				case Browser::BROWSER_ANDROID_MOBILE:
					$this->layout = '/phone/layouts/main';
					$this->pathPrefix = 'application.views.phone.';
					$this->isPhone = true;
					break;
				default :
					$version = Yii::app()->cacheDB->get('siteVersion');
					if (Yii::app()->browser->isMobile() && isset($version) && $version == 'mobile')
					{
						$this->layout = '/phone/layouts/main';
						$this->pathPrefix = 'application.views.phone.';
						$this->isPhone = true;
					}
					else
					{
						$this->layout = '/regular/layouts/main';
						$this->pathPrefix = 'application.views.regular.';
						$this->isPhone = false;
					}
					break;
			}
		}
	}

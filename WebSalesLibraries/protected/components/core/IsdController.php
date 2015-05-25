<?php

	/**
	 * Class IsdController
	 */
	class IsdController extends CController
	{
		public $browser;
		public $pathPrefix;

		public function init()
		{
			$this->browser = Yii::app()->browser->getBrowser();
			//$this->browser = Browser::BROWSER_IPHONE;
			switch ($this->browser)
			{
				case Browser::BROWSER_IPHONE:
				case Browser::BROWSER_ANDROID_MOBILE:
					$this->layout = '/phone/layouts/main';
					$this->pathPrefix = 'application.views.phone.';
					break;
				default :
					$this->layout = '/regular/layouts/main';
					$this->pathPrefix = 'application.views.regular.';
					break;
			}
		}

	}

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
						Yii::app()->request->redirect(Yii::app()->createAbsoluteUrl("site/badBrowser"));
				}
				catch (Exception $e)
				{
				}
			}
		}
	}
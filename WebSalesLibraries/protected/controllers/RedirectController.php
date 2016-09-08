<?php

	/**
	 * Class RedirectController
	 */
	class RedirectController extends IsdController
	{
		public $defaultAction = 'index';

		public function actionIndex()
		{
			$uri = Yii::app()->request->getQuery('secured_uri');
			$uri = Yii::app()->getBaseUrl(true) . '/' . $uri;
			StatisticActivityRecord::writeCommonActivity('Secure Links', 'Open', array('url' => $uri));
			$uri = str_replace('index.html', 'protected.html', $uri);
			$this->redirect($uri);
		}
	}
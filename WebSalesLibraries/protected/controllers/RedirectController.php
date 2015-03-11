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
			$this->redirect(Yii::app()->getBaseUrl(true) . '/' . str_replace('index.html', 'protected.html', $uri));
		}
	}
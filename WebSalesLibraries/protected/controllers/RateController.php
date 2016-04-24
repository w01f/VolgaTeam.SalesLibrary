<?php

	/**
	 * Class RateController
	 */
	class RateController extends IsdController
	{
		public function getViewPath()
		{
			return YiiBase::getPathOfAlias($this->pathPrefix . 'rate');
		}

		public function actionSetRate()
		{
			$linkId = Yii::app()->request->getPost('linkId');
			$value = floatval(Yii::app()->request->getPost('value'));
			$userId = UserIdentity::getCurrentUserId();
			if (isset($linkId))
			{
				LinkRateRecord::setRate($linkId, $userId, $value);
				echo CJSON::encode(LinkRateRecord::getRateData($linkId, $userId));
			}
			Yii::app()->end();
		}

		public function actionGetRate()
		{
			$linkId = Yii::app()->request->getPost('linkId');
			$userId = UserIdentity::getCurrentUserId();
			if (isset($linkId))
			{
				echo CJSON::encode(LinkRateRecord::getRateData($linkId, $userId));
			}
			Yii::app()->end();
		}
	}

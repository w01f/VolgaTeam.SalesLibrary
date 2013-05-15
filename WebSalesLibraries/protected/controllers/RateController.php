<?php
	class RateController extends IsdController
	{
		public function getViewPath()
		{
			return YiiBase::getPathOfAlias($this->pathPrefix . 'rate');
		}

		public function actionGetRate()
		{
			$linkId = Yii::app()->request->getPost('linkId');
			$userId = Yii::app()->user->getId();
			if (isset($linkId) && isset($userId))
			{
				$rate = LinkRateStorage::model()->getLinkRate($linkId);
				$isRated = LinkRateStorage::model()->isRated($linkId, $userId);
				$this->renderPartial('linkRate', array('rate' => $rate, 'isRated' => $isRated), false, true);
			}
		}

		public function actionAddRate()
		{
			$linkId = Yii::app()->request->getPost('linkId');
			$userId = Yii::app()->user->getId();
			if (isset($linkId) && isset($userId))
				LinkRateStorage::addRate($linkId, $userId);
			Yii::app()->end();
		}
	}

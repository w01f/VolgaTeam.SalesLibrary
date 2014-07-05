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

		public function actionGetRate()
		{
			$linkId = Yii::app()->request->getPost('linkId');
			$userId = Yii::app()->user->getId();
			if (isset($linkId) && isset($userId))
			{
				$rate = LinkRateRecord::model()->getLinkRate($linkId);
				$isRated = LinkRateRecord::model()->isRated($linkId, $userId);
				$this->renderPartial('linkRate', array('rate' => $rate, 'isRated' => $isRated), false, true);
			}
		}

		public function actionAddRate()
		{
			$linkId = Yii::app()->request->getPost('linkId');
			$userId = Yii::app()->user->getId();
			if (isset($linkId) && isset($userId))
			{
				LinkRateRecord::addRate($linkId, $userId);
				$linkRecord = LinkRecord::getLinkById($linkId);
				if (isset($linkRecord))
					StatisticActivityRecord::WriteActivity('Link', 'Like', array('Name' => $linkRecord->name, 'File' => $linkRecord->file_name, 'Original Format' => $linkRecord->format));
			}
			Yii::app()->end();
		}

		public function actionDeleteRate()
		{
			$linkId = Yii::app()->request->getPost('linkId');
			$userId = Yii::app()->user->getId();
			if (isset($linkId) && isset($userId))
			{
				LinkRateRecord::deleteRate($linkId, $userId);
				$linkRecord = LinkRecord::getLinkById($linkId);
				if (isset($linkRecord))
					StatisticActivityRecord::WriteActivity('Link', 'Unlike', array('Name' => $linkRecord->name, 'File' => $linkRecord->file_name, 'Original Format' => $linkRecord->format));
			}
			Yii::app()->end();
		}
	}

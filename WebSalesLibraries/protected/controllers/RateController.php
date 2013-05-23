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
			{
				LinkRateStorage::addRate($linkId, $userId);
				$linkRecord = LinkStorage::getLinkById($linkId);
				if (isset($linkRecord))
					StatisticActivityStorage::WriteActivity('Link', 'Like', array('Name' => $linkRecord->name, 'File' => $linkRecord->file_name, 'Original Format' => $linkRecord->format));
			}
			Yii::app()->end();
		}

		public function actionDeleteRate()
		{
			$linkId = Yii::app()->request->getPost('linkId');
			$userId = Yii::app()->user->getId();
			if (isset($linkId) && isset($userId))
			{
				LinkRateStorage::deleteRate($linkId, $userId);
				$linkRecord = LinkStorage::getLinkById($linkId);
				if (isset($linkRecord))
					StatisticActivityStorage::WriteActivity('Link', 'Unlike', array('Name' => $linkRecord->name, 'File' => $linkRecord->file_name, 'Original Format' => $linkRecord->format));
			}
			Yii::app()->end();
		}
	}

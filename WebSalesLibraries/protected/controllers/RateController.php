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
			$userId = Yii::app()->user->getId();
			if (isset($linkId) && isset($userId))
			{
				LinkRateRecord::setRate($linkId, $userId, $value);
				$linkRecord = LinkRecord::getLinkById($linkId);
				if (isset($linkRecord))
					StatisticActivityRecord::WriteActivity('Link', 'Like', array('Name' => $linkRecord->name, 'File' => $linkRecord->file_name, 'Original Format' => $linkRecord->format, 'Rate' => $value));
				echo json_encode(LinkRateRecord::getRateData($linkId, $userId));
			}
			Yii::app()->end();
		}
	}

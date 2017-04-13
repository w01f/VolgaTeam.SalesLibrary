<?

	/**
	 * Class LinkFeedController
	 */
	class LinkFeedController extends IsdController
	{
		public function getViewPath()
		{
			return YiiBase::getPathOfAlias($this->pathPrefix . 'link_feed');
		}

		public function actionGetItems()
		{
			$feedId = Yii::app()->request->getPost('feedId');
			$feedType = Yii::app()->request->getPost('feedType');
			$feedSettingsEncoded = Yii::app()->request->getPost('settings');

			if (isset($feedType) && isset($feedSettingsEncoded))
			{
				$feedSettings = \application\models\link_feed\LinkFeedSettings::fromJson($feedType, CJSON::encode($feedSettingsEncoded));
				$feedItems = \application\models\link_feed\LinkFeedManager::queryFeedItems($feedSettings);
			}

			if (isset($feedSettings) && isset($feedItems))
				$this->renderPartial('feedItems', array('feedId' => $feedId, 'feedSettings' => $feedSettings, 'feedItems' => $feedItems));
			else
				Yii::app()->end();
		}
	}
<?
	use application\models\data_query\link_feed\LinkFeedQueryHelper;
	use application\models\data_query\link_feed\LinkFeedQuerySettings;
	use application\models\feeds\horizontal\FeedSettings;

	/**
	 * Class LinkFeedController
	 */
	class LandingPageController extends IsdController
	{
		public function getViewPath()
		{
			return YiiBase::getPathOfAlias($this->pathPrefix . 'shortcuts.landingPageMarkup');
		}

		public function actionGetLinkFeedItems()
		{
			$feedId = Yii::app()->request->getPost('feedId');
			$feedType = Yii::app()->request->getPost('feedType');
			$querySettingsEncoded = Yii::app()->request->getPost('querySettings');
			$viewSettingsEncoded = Yii::app()->request->getPost('viewSettings');

			if (isset($feedType) && isset($querySettingsEncoded))
			{
				$querySettings = LinkFeedQuerySettings::fromJson($feedType, CJSON::encode($querySettingsEncoded));
				$feedItems = LinkFeedQueryHelper::queryFeedItems($querySettings);
			}

			if (isset($viewSettingsEncoded))
				$viewSettings = FeedSettings::fromJson($feedType, CJSON::encode($viewSettingsEncoded));

			if (isset($viewSettings) && isset($feedItems))
				$this->renderPartial('horizontal_feed/feedItems', array('feedId' => $feedId, 'viewSettings' => $viewSettings, 'feedItems' => $feedItems));
			else
				Yii::app()->end();
		}
	}
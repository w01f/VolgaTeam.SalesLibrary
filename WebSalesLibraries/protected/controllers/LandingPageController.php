<?
	use application\models\data_query\link_feed\LinkFeedQueryHelper;
	use application\models\data_query\link_feed\LinkFeedQuerySettings;
	use application\models\feeds\horizontal\FeedSettings as HorizontalFeedSettings;
	use application\models\feeds\vertical\FeedSettings as VerticalFeedSettings;
	use application\models\shortcuts\models\landing_page\regular_markup\masonry\MasonrySettings;

	/**
	 * Class LinkFeedController
	 */
	class LandingPageController extends IsdController
	{
		public function getViewPath()
		{
			return YiiBase::getPathOfAlias($this->pathPrefix . 'shortcuts.landingPageMarkup');
		}

		public function actionGetHorizontalLinkFeedItems()
		{
			$feedId = Yii::app()->request->getPost('feedId');
			$feedType = Yii::app()->request->getPost('feedType');
			$querySettingsEncoded = Yii::app()->request->getPost('querySettings');
			$viewSettingsEncoded = Yii::app()->request->getPost('viewSettings');

			if (!empty($feedType) && isset($querySettingsEncoded))
			{
				$querySettings = LinkFeedQuerySettings::fromJson($feedType, CJSON::encode($querySettingsEncoded));
				$feedItems = LinkFeedQueryHelper::queryFeedItems($querySettings);
			}

			if (!empty($viewSettingsEncoded))
				$viewSettings = HorizontalFeedSettings::fromJson($feedType, CJSON::encode($viewSettingsEncoded));

			if (isset($viewSettings) && isset($feedItems))
				$this->renderPartial('horizontal_feed/feedItems', array('feedId' => $feedId, 'viewSettings' => $viewSettings, 'feedItems' => $feedItems));
			else
				Yii::app()->end();
		}

		public function actionGetVerticalLinkFeedItems()
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
				$viewSettings = VerticalFeedSettings::fromJson($feedType, CJSON::encode($viewSettingsEncoded));

			if (isset($viewSettings) && isset($feedItems))
				$this->renderPartial('vertical_feed/feedItems', array('feedId' => $feedId, 'viewSettings' => $viewSettings, 'feedItems' => $feedItems));
			else
				Yii::app()->end();
		}

		public function actionGetMasonryLinkFeedItems()
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
				$viewSettings = MasonrySettings::fromJson($feedType, CJSON::encode($viewSettingsEncoded));

			if (isset($viewSettings) && isset($feedItems))
				$this->renderPartial('masonry/feedItems', array('feedId' => $feedId, 'viewSettings' => $viewSettings, 'feedItems' => $feedItems));
			else
				Yii::app()->end();
		}
	}
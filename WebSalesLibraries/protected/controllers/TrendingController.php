<?

	/**
	 * Class TrendingController
	 */
	class TrendingController extends IsdController
	{
		public function getViewPath()
		{
			return YiiBase::getPathOfAlias($this->pathPrefix . 'trending');
		}

		public function actionGetItems()
		{
			$barId = Yii::app()->request->getPost('barId');
			$trendingSettings = Yii::app()->request->getPost('settings');
			if (isset($trendingSettings))
				$trendingSettings = \application\models\trending\TrendingSettings::fromJson(CJSON::encode($trendingSettings));
			else
				$trendingSettings = new \application\models\trending\TrendingSettings();

			$trendingLinks = \application\models\trending\TrendingBarManager::queryTrendingLinks($trendingSettings);

			$this->renderPartial('linksList', array('barId' => $barId, 'trendingSettings' => $trendingSettings, 'links' => $trendingLinks));
		}
	}
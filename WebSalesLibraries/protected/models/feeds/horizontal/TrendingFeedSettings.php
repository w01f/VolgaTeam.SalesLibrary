<?

	namespace application\models\feeds\horizontal;

	use application\models\feeds\common\FeedControlFactory;

	/**
	 * Class TrendingFeedSettings
	 */
	class TrendingFeedSettings extends LinkFeedSettings
	{
		public function __construct()
		{
			$this->feedType = self::FeedTypeTrending;

			parent::__construct();
		}

		protected function initDefaultControlSettings()
		{
			$this->controlSettings = new \stdClass();
			foreach (FeedControlFactory::getAvailableControlTags(FeedControlFactory::FeedTypeTrending) as $tag)
				$this->controlSettings->{$tag} = FeedControlFactory::getControl(FeedControlFactory::FeedTypeTrending, $tag);
		}
	}
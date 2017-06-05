<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\masonry;

	use application\models\feeds\common\FeedControlFactory;

	/**
	 * Class TrendingFeedSettings
	 */
	class TrendingFeedSettings extends MasonryFeedSettings
	{
		public function __construct()
		{
			$this->feedType = self::MasonryTypeTrending;
			parent::__construct();
		}

		protected function initDefaultControlSettings()
		{
			$this->controlSettings = new \stdClass();
			foreach (FeedControlFactory::getAvailableControlTags(FeedControlFactory::FeedTypeTrending) as $tag)
				$this->controlSettings->{$tag} = FeedControlFactory::getControl(FeedControlFactory::FeedTypeTrending, $tag);
		}
	}
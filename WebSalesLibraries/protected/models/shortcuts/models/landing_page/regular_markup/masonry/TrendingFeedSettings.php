<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\masonry;

	use application\models\feeds\common\TrendingFeedControlSettings;

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
			foreach (TrendingFeedControlSettings::$tags as $tag)
				$this->controlSettings->{$tag} = TrendingFeedControlSettings::createDefault($tag);
		}
	}
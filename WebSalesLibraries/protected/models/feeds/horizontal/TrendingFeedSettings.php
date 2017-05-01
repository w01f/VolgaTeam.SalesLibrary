<?

	namespace application\models\feeds\horizontal;

	use application\models\feeds\common\TrendingFeedControlSettings;

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
			foreach (TrendingFeedControlSettings::$tags as $tag)
				$this->controlSettings->{$tag} = TrendingFeedControlSettings::createDefault($tag);
		}
	}
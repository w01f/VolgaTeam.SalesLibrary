<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\masonry;

	use application\models\feeds\common\SearchFeedControlSettings;

	/**
	 * Class SearchFeedSettings
	 */
	class SearchFeedSettings extends MasonryFeedSettings
	{
		public function __construct()
		{
			$this->feedType = self::MasonryTypeSearch;
			parent::__construct();
		}

		protected function initDefaultControlSettings()
		{
			$this->controlSettings = new \stdClass();
			foreach (SearchFeedControlSettings::$tags as $tag)
				$this->controlSettings->{$tag} = SearchFeedControlSettings::createDefault($tag);
		}
	}
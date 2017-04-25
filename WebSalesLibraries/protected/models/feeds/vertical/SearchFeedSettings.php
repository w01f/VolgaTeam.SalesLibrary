<?

	namespace application\models\feeds\vertical;

	use application\models\feeds\common\SearchFeedControlSettings;

	/**
	 * Class SearchFeedSettings
	 */
	class SearchFeedSettings extends LinkFeedSettings
	{
		public function __construct()
		{
			$this->feedType = self::FeedTypeSearch;
			parent::__construct();
		}

		protected function initDefaultControlSettings()
		{
			$this->controlSettings = new \stdClass();
			foreach (SearchFeedControlSettings::$tags as $tag)
				$this->controlSettings->{$tag} = SearchFeedControlSettings::createDefault($tag);
		}
	}
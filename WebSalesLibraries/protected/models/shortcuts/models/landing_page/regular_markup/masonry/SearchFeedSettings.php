<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\masonry;

	use application\models\feeds\common\FeedControlFactory;

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
			foreach (FeedControlFactory::getAvailableControlTags(FeedControlFactory::FeedTypeSearch) as $tag)
				$this->controlSettings->{$tag} = FeedControlFactory::getControl(FeedControlFactory::FeedTypeSearch, $tag);
		}
	}
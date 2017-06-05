<?

	namespace application\models\feeds\horizontal;

	use application\models\feeds\common\FeedControlFactory;

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
			foreach (FeedControlFactory::getAvailableControlTags(FeedControlFactory::FeedTypeSearch) as $tag)
				$this->controlSettings->{$tag} = FeedControlFactory::getControl(FeedControlFactory::FeedTypeSearch, $tag);
		}
	}
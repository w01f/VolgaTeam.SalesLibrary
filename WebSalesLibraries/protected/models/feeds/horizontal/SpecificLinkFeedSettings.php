<?

	namespace application\models\feeds\horizontal;

	use application\models\feeds\common\FeedControlFactory;

	/**
	 * Class SpecificLinkFeedSettings
	 */
	class SpecificLinkFeedSettings extends LinkFeedSettings
	{
		public function __construct()
		{
			$this->feedType = self::FeedTypeSpecificLinks;

			parent::__construct();
		}

		protected function initDefaultControlSettings()
		{
			$this->controlSettings = new \stdClass();
			foreach (FeedControlFactory::getAvailableControlTags(FeedControlFactory::FeedTypeSpecificLinks) as $tag)
				$this->controlSettings->{$tag} = FeedControlFactory::getControl(FeedControlFactory::FeedTypeSpecificLinks, $tag);
		}
	}
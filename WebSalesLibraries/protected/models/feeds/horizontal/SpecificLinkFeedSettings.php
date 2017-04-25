<?

	namespace application\models\feeds\horizontal;

	use application\models\feeds\common\SpecificLinkFeedControlSettings;

	/**
	 * Class SpecificLinkFeedSettings
	 */
	class SpecificLinkFeedSettings extends FeedSettings
	{
		public function __construct()
		{
			$this->feedType = self::FeedTypeSpecificLinks;

			parent::__construct();
		}

		protected function initDefaultControlSettings()
		{
			$this->controlSettings = new \stdClass();
			foreach (SpecificLinkFeedControlSettings::$tags as $tag)
				$this->controlSettings->{$tag} = SpecificLinkFeedControlSettings::createDefault($tag);
		}
	}
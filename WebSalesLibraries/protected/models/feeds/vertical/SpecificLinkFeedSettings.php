<?

	namespace application\models\feeds\vertical;

	use application\models\feeds\common\SpecificLinkFeedControlSettings;

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
			foreach (SpecificLinkFeedControlSettings::$tags as $tag)
				$this->controlSettings->{$tag} = SpecificLinkFeedControlSettings::createDefault($tag);
		}
	}
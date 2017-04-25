<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\masonry;

	use application\models\feeds\common\SpecificLinkFeedControlSettings;

	/**
	 * Class SpecificLinkFeedSettings
	 */
	class SpecificLinkFeedSettings extends MasonryFeedSettings
	{
		public function __construct()
		{
			$this->feedType = self::MasonryTypeSpecificLinks;
			parent::__construct();
		}

		protected function initDefaultControlSettings()
		{
			$this->controlSettings = new \stdClass();
			foreach (SpecificLinkFeedControlSettings::$tags as $tag)
				$this->controlSettings->{$tag} = SpecificLinkFeedControlSettings::createDefault($tag);
		}
	}
<?

	namespace application\models\link_feed;

	/**
	 * Class SpecificLinkFeedControlSettings
	 */
	class SpecificLinkFeedControlSettings extends FeedControlSettings
	{
		public static $tags = array(
			self::ControlTagLinkFormatPowerPoint,
			self::ControlTagLinkFormatDocuments,
			self::ControlTagLinkFormatVideo
		);

		/**
		 * @param $tag
		 * @return SpecificLinkFeedControlSettings
		 */
		public static function createDefault($tag)
		{
			$instance = new self();
			$instance->initDefaults($tag);
			return $instance;
		}
	}
<?

	namespace application\models\feeds\horizontal;

	/**
	 * Class SpecificLinkFeedControlSettings
	 */
	class SpecificLinkFeedControlSettings extends FeedControlSettings
	{
		public static $tags = array(
			self::ControlTagScrollButton,
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
<?

	namespace application\models\link_feed;

	/**
	 * Class SearchFeedControlSettings
	 */
	class SearchFeedControlSettings extends FeedControlSettings
	{
		public static $tags = array(
			self::ControlTagScrollButton,
			self::ControlTagLinkFormatPowerPoint,
			self::ControlTagLinkFormatDocuments,
			self::ControlTagLinkFormatVideo
		);

		/**
		 * @param $tag
		 * @return SearchFeedControlSettings
		 */
		public static function createDefault($tag)
		{
			$instance = new self();
			$instance->initDefaults($tag);
			return $instance;
		}
	}
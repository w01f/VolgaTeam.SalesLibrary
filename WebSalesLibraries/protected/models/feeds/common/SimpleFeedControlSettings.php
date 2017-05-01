<?

	namespace application\models\feeds\common;

	/**
	 * Class SpecificLinkFeedControlSettings
	 */
	class SimpleFeedControlSettings extends FeedControlSettings
	{
		public static $tags = array(
			self::ControlTagScrollButton
		);

		/**
		 * @param $tag
		 * @return FeedControlSettings
		 */
		public static function createDefault($tag)
		{
			$instance = new self();
			$instance->initDefaults($tag);
			return $instance;
		}
	}
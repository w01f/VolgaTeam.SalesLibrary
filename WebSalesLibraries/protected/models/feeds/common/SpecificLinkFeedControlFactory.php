<?

	namespace application\models\feeds\common;

	/**
	 * Class SpecificLinkFeedControlSettings
	 */
	class SpecificLinkFeedControlFactory extends FeedControlFactory
	{
		public static $tags = array(
			FeedControlTag::ControlTagScrollButton,
			FeedControlTag::ControlTagLinkFormatPowerPoint,
			FeedControlTag::ControlTagLinkFormatDocuments,
			FeedControlTag::ControlTagLinkFormatVideo,
			FeedControlTag::ControlTagDetailsButton
		);
	}
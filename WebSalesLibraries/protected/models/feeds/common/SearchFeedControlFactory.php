<?

	namespace application\models\feeds\common;

	/**
	 * Class SearchFeedControlSettings
	 */
	class SearchFeedControlFactory extends FeedControlFactory
	{
		public static $tags = array(
			FeedControlTag::ControlTagScrollButton,
			FeedControlTag::ControlTagLinkFormatPowerPoint,
			FeedControlTag::ControlTagLinkFormatDocuments,
			FeedControlTag::ControlTagLinkFormatVideo,
			FeedControlTag::ControlTagLinkFormatHyperlinks,
			FeedControlTag::ControlTagDetailsButton
		);
	}
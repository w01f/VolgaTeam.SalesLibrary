<?

	namespace application\models\feeds\common;

	/**
	 * Class TrendingControlSettings
	 */
	class TrendingFeedControlFactory extends FeedControlFactory
	{
		public static $tags = array(
			FeedControlTag::ControlTagScrollButton,
			FeedControlTag::ControlTagDateToday,
			FeedControlTag::ControlTagDateWeek,
			FeedControlTag::ControlTagDateMonth,
			FeedControlTag::ControlTagLinkFormatPowerPoint,
			FeedControlTag::ControlTagLinkFormatDocuments,
			FeedControlTag::ControlTagLinkFormatVideo,
			FeedControlTag::ControlTagDetailsButton
		);

		/**
		 * @param $controlTag string
		 * @return FeedControlSettings
		 * @throws \Exception
		 */
		public function createControl($controlTag)
		{
			switch ($controlTag)
			{
				case FeedControlTag::ControlTagDateToday:
					$control = new FeedControlSettings();
					$control->enabled = true;
					$control->title = 'today';
					return $control;
				case FeedControlTag::ControlTagDateWeek:
					$control = new FeedControlSettings();
					$control->enabled = true;
					$control->title = 'this week';
					return $control;
				case FeedControlTag::ControlTagDateMonth:
					$control = new FeedControlSettings();
					$control->enabled = true;
					$control->title = 'this month';
					return $control;
				default:
					return parent::createControl($controlTag);
			}
		}
	}
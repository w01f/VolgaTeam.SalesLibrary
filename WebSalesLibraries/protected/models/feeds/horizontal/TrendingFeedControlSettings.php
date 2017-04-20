<?

	namespace application\models\feeds\horizontal;

	/**
	 * Class TrendingControlSettings
	 */
	class TrendingFeedControlSettings extends FeedControlSettings
	{
		const ControlTagDateToday = 'today';
		const ControlTagDateWeek = 'week';
		const ControlTagDateMonth = 'month';

		public static $tags = array(
			self::ControlTagScrollButton,
			self::ControlTagDateToday,
			self::ControlTagDateWeek,
			self::ControlTagDateMonth,
			self::ControlTagLinkFormatPowerPoint,
			self::ControlTagLinkFormatDocuments,
			self::ControlTagLinkFormatVideo
		);

		/**
		 * @param $tag
		 * @return TrendingFeedControlSettings
		 */
		public static function createDefault($tag)
		{
			$instance = new self();
			$instance->initDefaults($tag);
			return $instance;
		}

		/**
		 * @param $tag string
		 */
		protected function initDefaults($tag)
		{
			switch ($tag)
			{
				case self::ControlTagDateToday:
					$this->enabled = true;
					$this->title = 'today';
					break;
				case self::ControlTagDateWeek:
					$this->enabled = true;
					$this->title = 'this week';
					break;
				case self::ControlTagDateMonth:
					$this->enabled = true;
					$this->title = 'this month';
					break;
				default:
					parent::initDefaults($tag);
					break;
			}
		}
	}
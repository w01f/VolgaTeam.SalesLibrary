<?
	namespace application\models\trending;

	/**
	 * Class TrendingControlSettings
	 */
	class TrendingControlSettings
	{
		const ControlTagDateToday = 'today';
		const ControlTagDateWeek = 'week';
		const ControlTagDateMonth = 'month';

		const ControlTagLinkFormatPowerPoint = 'ppt';
		const ControlTagLinkFormatVideo = 'video';
		const ControlTagLinkFormatDocuments = 'document';

		public static $tags = array(
			self::ControlTagDateToday,
			self::ControlTagDateWeek,
			self::ControlTagDateMonth,
			self::ControlTagLinkFormatPowerPoint,
			self::ControlTagLinkFormatDocuments,
			self::ControlTagLinkFormatVideo
		);

		public $enabled;
		public $title;

		/**
		 * @param $tag
		 * @return TrendingControlSettings
		 */
		public static function createDefault($tag)
		{
			$instance = new self();

			switch ($tag)
			{
				case self::ControlTagDateToday:
					$instance->enabled = true;
					$instance->title = 'today';
					break;
				case self::ControlTagDateWeek:
					$instance->enabled = true;
					$instance->title = 'this week';
					break;
				case self::ControlTagDateMonth:
					$instance->enabled = true;
					$instance->title = 'this month';
					break;

				case self::ControlTagLinkFormatPowerPoint:
					$instance->enabled = true;
					$instance->title = 'presentations';
					break;
				case self::ControlTagLinkFormatDocuments:
					$instance->enabled = true;
					$instance->title = 'documents';
					break;
				case self::ControlTagLinkFormatVideo:
					$instance->enabled = true;
					$instance->title = 'video';
					break;
			}

			return $instance;
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			$queryResult = $xpath->query('./Enable', $contextNode);
			$this->enabled = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $this->enabled;

			$queryResult = $xpath->query('./Title', $contextNode);
			$this->title = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $this->title;
		}
	}
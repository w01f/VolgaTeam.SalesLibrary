<?

	/**
	 * Class FeedSearchConditions
	 */
	class FeedSearchConditions extends BaseSearchConditions
	{
		public function __construct()
		{
			parent::__construct();
			$this->columnSettings = DataColumnSettings::createLinkFeedColumns();
		}

		/**
		 * @param $xpath DOMXPath
		 * @param $contextNode DOMNode
		 * @return FeedSearchConditions
		 */
		public static function fromXml($xpath, $contextNode)
		{
			$instance = new self();
			$instance->configureFromXml($xpath, $contextNode);
			return $instance;
		}
	}
<?
	namespace application\models\data_query\conditions;

	use application\models\data_query\data_table\DataTableColumnSettings;

	/**
	 * Class FeedSearchConditions
	 */
	class FeedQueryConditions extends BaseQueryConditions
	{
		public function __construct()
		{
			parent::__construct();
			$this->columnSettings = DataTableColumnSettings::createLinkFeedColumns();
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return FeedQueryConditions
		 */
		public static function fromXml($xpath, $contextNode)
		{
			$instance = new self();
			$instance->configureFromXml($xpath, $contextNode);
			return $instance;
		}
	}
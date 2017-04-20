<?
	namespace application\models\data_query\link_feed;

	/**
	 * Class SearchFeedQuerySettings
	 */
	class SearchFeedQuerySettings extends LinkFeedQuerySettings
	{
		/** @var  \FeedSearchConditions */
		public $conditions;

		public function __construct()
		{
			$this->feedType = self::FeedTypeSearch;

			parent::__construct();

			$this->conditions = new \FeedSearchConditions();
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			parent::configureFromXml($xpath, $contextNode);

			$queryResult = $xpath->query('./SearchCondition', $contextNode);
			if ($queryResult->length > 0)
				$this->conditions = \FeedSearchConditions::fromXml($xpath, $queryResult->item(0));
		}
	}
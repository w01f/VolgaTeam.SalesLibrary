<?
	namespace application\models\data_query\link_feed;

	use application\models\data_query\conditions\FeedQueryConditions;

	/**
	 * Class SearchFeedQuerySettings
	 */
	class SearchFeedQuerySettings extends LinkFeedQuerySettings
	{
		/** @var  FeedQueryConditions */
		public $conditions;

		public function __construct()
		{
			$this->feedType = self::FeedTypeSearch;

			parent::__construct();

			$this->conditions = new FeedQueryConditions();
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
				$this->conditions = FeedQueryConditions::fromXml($xpath, $queryResult->item(0));
		}
	}
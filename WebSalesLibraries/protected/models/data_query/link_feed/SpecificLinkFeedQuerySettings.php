<?
	namespace application\models\data_query\link_feed;

	use application\models\data_query\common\QuerySortSettings;

	/**
	 * Class SpecificLinkFeedQuerySettings
	 */
	class SpecificLinkFeedQuerySettings extends LinkFeedQuerySettings
	{
		/** @var  SpecificLinkCondition[] */
		public $linkConditions;

		/** @var  QuerySortSettings */
		public $sortSettings;

		public function __construct()
		{
			$this->feedType = self::FeedTypeSpecificLinks;

			parent::__construct();

			$this->linkConditions = array();
			$this->sortSettings = new QuerySortSettings();
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			parent::configureFromXml($xpath, $contextNode);

			$queryResult = $xpath->query('./SpecificLinks/Item', $contextNode);
			foreach ($queryResult as $node)
				$this->linkConditions[] = SpecificLinkCondition::fromXml($xpath, $node);

			$queryResult = $xpath->query('./SortSettings', $contextNode);
			if ($queryResult->length > 0)
				$this->sortSettings->configureFromXml($xpath, $queryResult->item(0));
		}
	}
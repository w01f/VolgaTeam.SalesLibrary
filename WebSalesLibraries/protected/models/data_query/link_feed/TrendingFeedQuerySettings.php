<?
	namespace application\models\data_query\link_feed;

	/**
	 * Class TrendingFeedQuerySettings
	 */
	class TrendingFeedQuerySettings extends LinkFeedQuerySettings
	{
		const DataRangeTypeToday = 'today';
		const DataRangeTypeWeek = 'week';
		const DataRangeTypeMonth = 'month';

		public $libraries;
		public $dateRangeType;

		public function __construct()
		{
			$this->feedType = self::FeedTypeTrending;

			parent::__construct();

			$this->libraries = array();
			$this->dateRangeType = self::DataRangeTypeWeek;
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			parent::configureFromXml($xpath, $contextNode);

			$queryResult = $xpath->query('./Libraries/Item', $contextNode);
			if ($queryResult->length > 0)
			{
				$this->libraries = array();
				foreach ($queryResult as $node)
				{
					$libraryName = trim($node->nodeValue);
					if (!empty($libraryName))
						$this->libraries[] = $libraryName;
				}
			}

			$queryResult = $xpath->query('./DateRange', $contextNode);
			$dateRangeType = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : $this->dateRangeType;
			if (in_array($dateRangeType, array(self::DataRangeTypeToday, self::DataRangeTypeWeek, self::DataRangeTypeMonth)))
				$this->dateRangeType = $dateRangeType;
		}
	}
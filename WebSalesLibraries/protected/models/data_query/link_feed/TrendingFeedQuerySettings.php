<?

	namespace application\models\data_query\link_feed;

	use application\models\data_query\conditions\CategoryConditionItem;
	use application\models\data_query\conditions\ExcludeQueryConditions;

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

		public $text;
		public $textExactMatch;

		/** @var CategoryConditionItem[] */
		public $categories;

		/** @var  ExcludeQueryConditions */
		public $excludeQueryConditions;

		public function __construct()
		{
			$this->feedType = self::FeedTypeTrending;

			parent::__construct();

			$this->libraries = array();
			$this->dateRangeType = self::DataRangeTypeWeek;
			$this->categories = array();
			$this->excludeQueryConditions = new ExcludeQueryConditions();
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

			$queryResult = $xpath->query('./Text', $contextNode);
			$this->text = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('./TextExactMatch', $contextNode);
			$this->textExactMatch = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;

			$queryResult = $xpath->query('./Categories/Category', $contextNode);
			/** @var $node \DOMElement */
			foreach ($queryResult as $node)
			{
				$groupName = $node->getAttribute('Group');
				$category = new CategoryConditionItem();
				$category->name = $groupName;
				$tagNodes = $node->getElementsByTagName('Tag');
				foreach ($tagNodes as $tagNode)
					$category->items[] = trim($tagNode->nodeValue);
				$this->categories[] = $category;
			}

			$queryResult = $xpath->query('./ExcludeConditions', $contextNode);
			if ($queryResult->length > 0)
				$this->excludeQueryConditions->configurefromXml($xpath, $queryResult->item(0));
		}
	}
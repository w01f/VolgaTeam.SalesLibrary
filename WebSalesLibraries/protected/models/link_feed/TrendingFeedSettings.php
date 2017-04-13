<?

	namespace application\models\link_feed;

	/**
	 * Class TrendingFeedSettings
	 */
	class TrendingFeedSettings extends LinkFeedSettings
	{
		const DataRangeTypeToday = 'today';
		const DataRangeTypeWeek = 'week';
		const DataRangeTypeMonth = 'month';

		public $libraries;
		public $dateRangeType;

		/** @var  TrendingFeedControlSettings[] */
		public $controlSettings;

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

			$queryResult = $xpath->query('./ControlSettings/Item', $contextNode);
			/** @var $node \DOMElement */
			foreach ($queryResult as $node)
			{
				$tag = $node->getAttribute('tag');
				if (!empty($tag) && property_exists($this->controlSettings, $tag))
				{
					/** @var TrendingFeedControlSettings $controlSettings */
					$controlSettings = $this->controlSettings->{$tag};
					$controlSettings->configureFromXml($xpath, $node);
				}
			}
		}

		protected function initDefaultControlSettings()
		{
			$this->controlSettings = new \stdClass();
			foreach (TrendingFeedControlSettings::$tags as $tag)
				$this->controlSettings->{$tag} = TrendingFeedControlSettings::createDefault($tag);
		}
	}
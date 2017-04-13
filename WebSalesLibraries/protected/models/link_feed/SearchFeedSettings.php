<?

	namespace application\models\link_feed;

	/**
	 * Class SearchFeedSettings
	 */
	class SearchFeedSettings extends LinkFeedSettings
	{

		/** @var  SearchFeedControlSettings[] */
		public $controlSettings;

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

			$queryResult = $xpath->query('./ControlSettings/Item', $contextNode);
			/** @var $node \DOMElement */
			foreach ($queryResult as $node)
			{
				$tag = $node->getAttribute('tag');
				if (!empty($tag) && property_exists($this->controlSettings, $tag))
				{
					/** @var SearchFeedControlSettings $controlSettings */
					$controlSettings = $this->controlSettings->{$tag};
					$controlSettings->configureFromXml($xpath, $node);
				}
			}

			$queryResult = $xpath->query('./SearchCondition', $contextNode);
			if($queryResult->length>0)
				$this->conditions = \FeedSearchConditions::fromXml($xpath, $queryResult->item(0));
		}

		protected function initDefaultControlSettings()
		{
			$this->controlSettings = new \stdClass();
			foreach (SearchFeedControlSettings::$tags as $tag)
				$this->controlSettings->{$tag} = SearchFeedControlSettings::createDefault($tag);
		}
	}
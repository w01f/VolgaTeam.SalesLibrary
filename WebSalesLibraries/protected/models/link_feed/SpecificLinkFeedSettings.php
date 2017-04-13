<?

	namespace application\models\link_feed;
	/**
	 * Class SpecificLinkFeedSettings
	 */
	class SpecificLinkFeedSettings extends LinkFeedSettings
	{

		/** @var  SpecificLinkFeedControlSettings[] */
		public $controlSettings;

		/** @var  SpecificLinkCondition[] */
		public $linkConditions;

		/** @var  \QuerySortSettings */
		public $sortSettings;

		public function __construct()
		{
			$this->feedType = self::FeedTypeSpecificLinks;

			parent::__construct();

			$this->linkConditions = array();
			$this->sortSettings = new \QuerySortSettings();
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
					/** @var SpecificLinkFeedControlSettings $controlSettings */
					$controlSettings = $this->controlSettings->{$tag};
					$controlSettings->configureFromXml($xpath, $node);
				}
			}

			$queryResult = $xpath->query('./SpecificLinks/Item', $contextNode);
			foreach ($queryResult as $node)
				$this->linkConditions[] = SpecificLinkCondition::fromXml($xpath, $node);

			$queryResult = $xpath->query('./SortSettings', $contextNode);
			if ($queryResult->length > 0)
				$this->sortSettings->configureFromXml($xpath, $queryResult->item(0));
		}

		protected function initDefaultControlSettings()
		{
			$this->controlSettings = new \stdClass();
			foreach (SpecificLinkFeedControlSettings::$tags as $tag)
				$this->controlSettings->{$tag} = SpecificLinkFeedControlSettings::createDefault($tag);
		}
	}
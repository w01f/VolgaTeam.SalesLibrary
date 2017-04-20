<?

	namespace application\models\feeds\horizontal;
	/**
	 * Class SpecificLinkFeedSettings
	 */
	class SpecificLinkFeedSettings extends FeedSettings
	{
		/** @var  SpecificLinkFeedControlSettings[] */
		public $controlSettings;

		public function __construct()
		{
			$this->feedType = self::FeedTypeSpecificLinks;

			parent::__construct();
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
		}

		protected function initDefaultControlSettings()
		{
			$this->controlSettings = new \stdClass();
			foreach (SpecificLinkFeedControlSettings::$tags as $tag)
				$this->controlSettings->{$tag} = SpecificLinkFeedControlSettings::createDefault($tag);
		}
	}
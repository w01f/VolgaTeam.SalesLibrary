<?

	namespace application\models\feeds\horizontal;

	/**
	 * Class TrendingFeedSettings
	 */
	class TrendingFeedSettings extends FeedSettings
	{
		/** @var  TrendingFeedControlSettings[] */
		public $controlSettings;

		public function __construct()
		{
			$this->feedType = self::FeedTypeTrending;

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
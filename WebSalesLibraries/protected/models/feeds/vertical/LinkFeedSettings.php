<?

	namespace application\models\feeds\vertical;

	use application\models\feeds\common\FeedControlSettings;
	use application\models\feeds\common\FeedItemSettings;

	/**
	 * Class LinkFeedSettings
	 */
	abstract class LinkFeedSettings extends FeedSettings
	{
		public $controlActiveColor;

		/** @var  FeedItemSettings[] */
		public $dataItemSettings;

		/** @var  FeedControlSettings[] */
		public $controlSettings;

		public function __construct()
		{
			parent::__construct();
			$this->style = new LinkFeedStyle();
			$this->initDefaultDataItemSettings();
			$this->initDefaultControlSettings();
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			parent::configureFromXml($xpath, $contextNode);

			$queryResult = $xpath->query('./ControlActiveColor', $contextNode);
			$this->controlActiveColor = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : null;

			$queryResult = $xpath->query('./DataSettings/Item', $contextNode);
			/** @var $node \DOMElement */
			foreach ($queryResult as $node)
			{
				$tag = $node->getAttribute('tag');
				if (!empty($tag) && property_exists($this->dataItemSettings, $tag))
				{
					/** @var FeedItemSettings $dataItem */
					$dataItem = $this->dataItemSettings->{$tag};
					$dataItem->configureFromXml($xpath, $node);
				}
			}

			$queryResult = $xpath->query('./ControlSettings/Item', $contextNode);
			/** @var $node \DOMElement */
			foreach ($queryResult as $node)
			{
				$tag = $node->getAttribute('tag');
				if (!empty($tag) && property_exists($this->controlSettings, $tag))
				{
					/** @var FeedControlSettings $controlSettings */
					$controlSettings = $this->controlSettings->{$tag};
					$controlSettings->configureFromXml($xpath, $node);
				}
			}
		}

		private function initDefaultDataItemSettings()
		{
			$this->dataItemSettings = new \stdClass();
			foreach (FeedItemSettings::$tags as $tag)
				$this->dataItemSettings->{$tag} = new FeedItemSettings($tag);
		}

		protected abstract function initDefaultControlSettings();
	}
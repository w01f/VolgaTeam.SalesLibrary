<?

	namespace application\models\feeds\horizontal;

	use application\models\feeds\common\ControlsStyle;
	use application\models\feeds\common\FeedControlSettings;
	use application\models\feeds\common\FeedItemSettings;

	/**
	 * Class LinkFeedSettings
	 */
	abstract class LinkFeedSettings extends FeedSettings
	{
		/** @var  FeedItemSettings[] */
		public $dataItemSettings;

		/** @var  ControlsStyle */
		public $controlsStyle;

		public function __construct()
		{
			parent::__construct();
			$this->controlsStyle = new ControlsStyle();
			$this->initDefaultDataItemSettings();
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			parent::configureFromXml($xpath, $contextNode);

			$queryResult = $xpath->query('./MaxThumbnailHeight', $contextNode);
			$this->maxImageHeight = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : $this->maxImageHeight;

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

			$queryResult = $xpath->query('./ControlsStyle', $contextNode);
			if ($queryResult->length > 0)
				$this->controlsStyle->configureFromXml($xpath, $queryResult->item(0));
		}

		protected function initDefaultDataItemSettings()
		{
			$this->dataItemSettings = new \stdClass();
			foreach (FeedItemSettings::$tags as $tag)
				$this->dataItemSettings->{$tag} = new FeedItemSettings($tag);
		}
	}
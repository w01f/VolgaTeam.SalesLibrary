<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\masonry;

	use application\models\feeds\common\ControlsStyle;
	use application\models\feeds\common\FeedControlSettings;
	use application\models\feeds\common\FeedItemSettings;

	abstract class MasonryFeedSettings extends MasonrySettings
	{
		public $imageWidth;
		public $imageHeight;

		/** @var  MasonryFeedItemSettings[] */
		public $dataItemSettings;

		/** @var  FeedControlSettings[] */
		public $controlSettings;

		/** @var  ControlsStyle */
		public $controlsStyle;

		public function __construct()
		{
			parent::__construct();
			$this->imageWidth = 0;
			$this->imageHeight = 0;
			$this->controlsStyle = new ControlsStyle();
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

			$queryResult = $xpath->query('./ImageWidth', $contextNode);
			$this->imageWidth = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $this->imageWidth;

			$queryResult = $xpath->query('./ImageHeight', $contextNode);
			$this->imageHeight = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $this->imageHeight;

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

			$queryResult = $xpath->query('./ControlsStyle', $contextNode);
			if ($queryResult->length > 0)
				$this->controlsStyle->configureFromXml($xpath, $queryResult->item(0));
		}

		private function initDefaultDataItemSettings()
		{
			$this->dataItemSettings = new \stdClass();
			foreach (FeedItemSettings::$tags as $tag)
				$this->dataItemSettings->{$tag} = new MasonryFeedItemSettings($tag);
		}

		protected abstract function initDefaultControlSettings();
	}
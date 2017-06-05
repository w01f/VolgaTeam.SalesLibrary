<?

	namespace application\models\feeds\horizontal;

	use application\models\feeds\common\FeedControlFactory;

	/**
	 * Class SimpleFeedSettings
	 */
	class SimpleFeedSettings extends FeedSettings
	{
		/** @var  SimpleFeedStyle */
		public $style;

		public function __construct()
		{
			$this->feedType = self::FeedTypeSimpleSlider;

			parent::__construct();

			$this->style = new SimpleFeedStyle();
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			parent::configureFromXml($xpath, $contextNode);

			$queryResult = $xpath->query('./MaxImageHeight', $contextNode);
			$this->maxImageHeight = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : $this->maxImageHeight;

			$queryResult = $xpath->query('./Style', $contextNode);
			if ($queryResult->length > 0)
				$this->style->configureFromXml($xpath, $queryResult->item(0));
		}

		protected function initDefaultControlSettings()
		{
			$this->controlSettings = new \stdClass();
			foreach (FeedControlFactory::getAvailableControlTags(FeedControlFactory::FeedTypeSimple) as $tag)
				$this->controlSettings->{$tag} = FeedControlFactory::getControl(FeedControlFactory::FeedTypeSimple, $tag);
		}
	}
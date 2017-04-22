<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\masonry;


	class MasonryFeedSettings extends MasonrySettings
	{
		public $imageWidth;
		public $imageHeight;

		public function __construct()
		{
			parent::__construct();
			$this->imageWidth = 0;
			$this->imageHeight = 0;
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
		}
	}
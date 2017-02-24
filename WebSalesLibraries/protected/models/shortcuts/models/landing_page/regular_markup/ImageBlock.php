<?
	namespace application\models\shortcuts\models\landing_page\regular_markup;

	/**
	 * Class ImageBlock
	 */
	class ImageBlock extends ContentBlock
	{
		public $source;
		public $floatSide;
		public $animation;

		/**
		 * @param $parentShortcut \LandingPageShortcut
		 * @param $parentBlock BlockContainer
		 */
		public function __construct($parentShortcut, $parentBlock)
		{
			parent::__construct($parentShortcut, $parentBlock);
			$this->type = 'image';
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		protected function configureFromXml($xpath, $contextNode)
		{
			parent::configureFromXml($xpath, $contextNode);

			$queryResult = $xpath->query('./File', $contextNode);
			$fileName = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;
			$this->source = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . $this->parentShortcut->relativeLink . '/images/' . $fileName);

			$queryResult = $xpath->query('./Float', $contextNode);
			$this->floatSide = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('./Animation', $contextNode);
			$this->animation = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;
		}
	}
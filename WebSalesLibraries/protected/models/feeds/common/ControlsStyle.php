<?

	namespace application\models\feeds\common;

	/**
	 * Class ControlsStyle
	 */
	class ControlsStyle
	{
		public $regularTextColor;
		public $regularBackColor;
		public $activeTextColor;
		public $activeBackColor;
		public $borderColor;

		public $lineHeight;

		public function __construct()
		{
			$this->lineHeight = 32;
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			$queryResult = $xpath->query('./RegularTextColor', $contextNode);
			$this->regularTextColor = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : null;

			$queryResult = $xpath->query('./RegularBackColor', $contextNode);
			$this->regularBackColor = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : null;

			$queryResult = $xpath->query('./ActiveTextColor', $contextNode);
			$this->activeTextColor = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : null;

			$queryResult = $xpath->query('./ActiveBackColor', $contextNode);
			$this->activeBackColor = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : null;

			$queryResult = $xpath->query('./BorderColor', $contextNode);
			$this->borderColor = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : null;

			$queryResult = $xpath->query('./LineHeight', $contextNode);
			$this->lineHeight = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : $this->lineHeight;
		}
	}
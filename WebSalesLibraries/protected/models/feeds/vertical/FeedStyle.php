<?

	namespace application\models\feeds\vertical;

	/**
	 * Class FeedStyle
	 */
	class FeedStyle
	{
		public $headerColor;
		public $footerColor;
		public $headerTextColor;
		public $headerIconColor;
		public $outsideBorderColor;
		public $dividerColor;
		public $buttonBackColor;
		public $buttonBorderColor;
		public $buttonIconColor;

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			$queryResult = $xpath->query('./HeaderColor', $contextNode);
			$this->headerColor = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : null;

			$queryResult = $xpath->query('./FooterColor', $contextNode);
			$this->footerColor = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : null;

			$queryResult = $xpath->query('./HeaderTextColor', $contextNode);
			$this->headerTextColor = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : null;

			$queryResult = $xpath->query('./HeaderIconColor', $contextNode);
			$this->headerIconColor = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : null;

			$queryResult = $xpath->query('./OutsideBorderColor', $contextNode);
			$this->outsideBorderColor = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : null;

			$queryResult = $xpath->query('./DividerColor', $contextNode);
			$this->dividerColor = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : null;

			$queryResult = $xpath->query('./ButtonBackColor', $contextNode);
			$this->buttonBackColor = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : null;

			$queryResult = $xpath->query('./ButtonBorderColor', $contextNode);
			$this->buttonBorderColor = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : null;

			$queryResult = $xpath->query('./ButtonIconColor', $contextNode);
			$this->buttonIconColor = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : null;
		}
	}
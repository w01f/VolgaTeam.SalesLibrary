<?
	/**
	 * Class SearchBarStyle
	 */
	class SearchBarStyle
	{
		public $buttonTextColor;
		public $buttonBackColor;
		public $searchTextColor;
		public $searchBackColor;
		public $labelTextColor;
		public $labelBackColor;
		public $placeholderTextColor;
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
			$queryResult = $xpath->query('./ButtonTextColor', $contextNode);
			$this->buttonTextColor = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : null;

			$queryResult = $xpath->query('./ButtonBackColor', $contextNode);
			$this->buttonBackColor = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : null;

			$queryResult = $xpath->query('./SearchTextColor', $contextNode);
			$this->searchTextColor = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : null;

			$queryResult = $xpath->query('./SearchBackColor', $contextNode);
			$this->searchBackColor = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : null;

			$queryResult = $xpath->query('./LabelTextColor', $contextNode);
			$this->labelTextColor = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : null;

			$queryResult = $xpath->query('./LabelBackColor', $contextNode);
			$this->labelBackColor = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : null;

			$queryResult = $xpath->query('./PlaceholderTextColor', $contextNode);
			$this->placeholderTextColor = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : null;

			$queryResult = $xpath->query('./BorderColor', $contextNode);
			$this->borderColor = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : null;

			$queryResult = $xpath->query('./LineHeight', $contextNode);
			$this->lineHeight = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : $this->lineHeight;
		}
	}
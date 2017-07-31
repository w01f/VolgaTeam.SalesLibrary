<?
	namespace application\models\wallbin\models\web\style;

	/**
	 * TabPageSelectorStyle
	 */
	class TabPageSelectorStyle
	{
		public $regularTextColor;
		public $regularBackColor;
		public $hoverTextColor;
		public $hoverBackColor;
		public $selectedTextColor;
		public $selectedBackColor;
		public $arrowColor;
		public $borderColor;

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return TabPageSelectorStyle
		 */
		public static function fromXml($xpath, $contextNode)
		{
			$tabSelectorStyle = self::createDefault();

			$queryResult = $xpath->query('.//TextColor', $contextNode);
			$tabSelectorStyle->regularTextColor = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $tabSelectorStyle->regularTextColor;

			$queryResult = $xpath->query('.//FillColor', $contextNode);
			$tabSelectorStyle->regularBackColor = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $tabSelectorStyle->regularBackColor;

			$queryResult = $xpath->query('.//TextColorHover', $contextNode);
			$tabSelectorStyle->hoverTextColor = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $tabSelectorStyle->hoverTextColor;

			$queryResult = $xpath->query('.//FillColorHover', $contextNode);
			$tabSelectorStyle->hoverBackColor = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $tabSelectorStyle->hoverBackColor;

			$queryResult = $xpath->query('.//TextColorSelected', $contextNode);
			$tabSelectorStyle->selectedTextColor = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $tabSelectorStyle->selectedTextColor;

			$queryResult = $xpath->query('.//FillColorSelected', $contextNode);
			$tabSelectorStyle->selectedBackColor = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $tabSelectorStyle->selectedBackColor;

			$queryResult = $xpath->query('.//ArrowColor', $contextNode);
			$tabSelectorStyle->arrowColor = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $tabSelectorStyle->arrowColor;

			$queryResult = $xpath->query('.//BorderColor', $contextNode);
			$tabSelectorStyle->borderColor = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $tabSelectorStyle->borderColor;

			return $tabSelectorStyle;
		}

		/**
		 * @return TabPageSelectorStyle
		 */
		public static function createDefault()
		{
			$tabSelectorStyle = new self();
			$tabSelectorStyle->regularTextColor = '000000';
			$tabSelectorStyle->regularBackColor = 'ffffff';
			$tabSelectorStyle->hoverTextColor = '000000';
			$tabSelectorStyle->hoverBackColor = 'cccccc';
			$tabSelectorStyle->selectedTextColor = '000000';
			$tabSelectorStyle->selectedBackColor = 'c6e2ff';
			$tabSelectorStyle->arrowColor = '000000';
			$tabSelectorStyle->borderColor = 'cccccc';
			return $tabSelectorStyle;
		}
	}
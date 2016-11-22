<?
	namespace application\models\wallbin\models\web\style;

	/**
	 * PageColumnStyle
	 */
	class PageColumnStyle
	{
		public $enabled;

		public $padding;
		public $windowBorderColor;

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return PageColumnStyle
		 */
		public static function fromXml($xpath, $contextNode)
		{
			$columnStyle = self::createEmpty();
			$columnStyle->enabled = true;

			$queryResult = $xpath->query('Padding', $contextNode);
			$columnStyle->padding = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : 0;

			$queryResult = $xpath->query('HorizontalBorderColor', $contextNode);
			$columnStyle->windowBorderColor = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			return $columnStyle;
		}

		/**
		 * @return PageColumnStyle
		 */
		public static function createEmpty()
		{
			$columnStyle = new PageColumnStyle();
			$columnStyle->enabled = false;
			$columnStyle->padding = 0;
			$columnStyle->windowBorderColor = null;
			return $columnStyle;
		}
	}
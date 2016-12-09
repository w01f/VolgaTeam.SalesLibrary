<?
	namespace application\models\wallbin\models\web\style;

	/**
	 * PageColumnStyle
	 */
	class PageColumnStyle
	{
		public $enabled;

		public $frozen;
		public $padding;
		public $windowBorderColor;

		/** @var  FolderStyle */
		public $windowStyle;

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return PageColumnStyle
		 */
		public static function fromXml($xpath, $contextNode)
		{
			$columnStyle = self::createDefault();
			$columnStyle->enabled = true;

			$queryResult = $xpath->query('.//IsFrozen', $contextNode);
			$columnStyle->frozen = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;

			$queryResult = $xpath->query('.//Padding', $contextNode);
			$columnStyle->padding = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : 0;

			$queryResult = $xpath->query('.//HorizontalBorderColor', $contextNode);
			$columnStyle->windowBorderColor = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('.//Windows', $contextNode);
			if ($queryResult->length > 0)
				$columnStyle->windowStyle = FolderStyle::fromXml($xpath, $queryResult->item(0));

			return $columnStyle;
		}

		/**
		 * @return PageColumnStyle
		 */
		public static function createDefault()
		{
			$columnStyle = new PageColumnStyle();
			$columnStyle->enabled = false;
			$columnStyle->frozen = false;
			$columnStyle->padding = 0;
			$columnStyle->windowBorderColor = null;
			$columnStyle->windowStyle = FolderStyle::createDefault();
			return $columnStyle;
		}
	}
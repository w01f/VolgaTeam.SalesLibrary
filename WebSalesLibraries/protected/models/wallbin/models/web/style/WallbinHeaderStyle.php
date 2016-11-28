<?
	namespace application\models\wallbin\models\web\style;

	/**
	 * WallbinHeaderStyle
	 */
	class WallbinHeaderStyle
	{
		public $showLogo;
		public $showText;
		public $textColor;
		public $backColor;
		public $headerBorderColor;
		public $paddingLeft;

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return WallbinHeaderStyle
		 */
		public static function fromXml($xpath, $contextNode)
		{
			$headerStyle = self::createDefault();

			$queryResult = $xpath->query('.//ShowLogo', $contextNode);
			$headerStyle->showLogo = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : true;

			$queryResult = $xpath->query('.//ShowPageName', $contextNode);
			$headerStyle->showText = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : true;

			$queryResult = $xpath->query('.//PageNameColor', $contextNode);
			$headerStyle->textColor = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : 'inherite';

			$queryResult = $xpath->query('.//PageNameBackground', $contextNode);
			$headerStyle->backColor = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : 'inherite';

			$queryResult = $xpath->query('.//TopBorderColor', $contextNode);
			$headerStyle->headerBorderColor = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : '999';

			$queryResult = $xpath->query('.//PaddingLeft', $contextNode);
			$headerStyle->paddingLeft = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : 0;

			return $headerStyle;
		}

		/**
		 * @return WallbinHeaderStyle
		 */
		public static function createDefault()
		{
			$headerStyle = new WallbinHeaderStyle();
			$headerStyle->showLogo = true;
			$headerStyle->showText = true;
			$headerStyle->textColor = 'inherite';
			$headerStyle->backColor = 'inherite';
			$headerStyle->headerBorderColor = '999';
			return $headerStyle;
		}
	}
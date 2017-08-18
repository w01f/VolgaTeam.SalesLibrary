<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\masonry;

	class MasonryFilterButtonStyle
	{
		public $hasBorder;
		public $borderColorRegular;
		public $borderColorSelected;
		public $backColorRegular;
		public $backColorSelected;


		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return MasonryFilterButtonStyle
		 */
		public static function fromXml($xpath, $contextNode)
		{
			$style = self::createDefault();

			$queryResult = $xpath->query('.//Border', $contextNode);
			$style->hasBorder = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $style->hasBorder;

			$queryResult = $xpath->query('.//BorderColor', $contextNode);
			$style->borderColorRegular = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $style->borderColorRegular;

			$queryResult = $xpath->query('.//BorderColorSelected', $contextNode);
			$style->borderColorSelected = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $style->borderColorSelected;

			$queryResult = $xpath->query('.//BackgroundColor', $contextNode);
			$style->backColorRegular = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $style->backColorRegular;

			$queryResult = $xpath->query('.//BackgroundColorSelected', $contextNode);
			$style->backColorSelected = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $style->backColorSelected;

			return $style;
		}

		/**
		 * @return MasonryFilterButtonStyle
		 */
		public static function createDefault()
		{
			$style = new self();
			$style->hasBorder = true;
			$style->borderColorRegular = 'ECECEC';
			$style->borderColorSelected = '8CD2E5';
			$style->backColorRegular = 'ffffff';
			$style->backColorSelected = 'ffffff';
			return $style;
		}
	}
<?
	namespace application\models\wallbin\models\web\style;

	/**
	 * WallbinPageStyle
	 */
	class WallbinPageStyle
	{
		public $enabled;
		public $columnStyleEnabled;

		public $showWindowHeaders;

		public $verticalBorder1Color;
		public $verticalBorder2Color;
		public $verticalBorderStretch;

		/** @var  PageColumnStyle */
		public $column1Style;
		/** @var  PageColumnStyle */
		public $column2Style;
		/** @var  PageColumnStyle */
		public $column3Style;

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return WallbinPageStyle
		 */
		public static function fromXml($xpath, $contextNode)
		{
			$pageStyle = self::createEmpty();
			$pageStyle->enabled = true;

			$queryResult = $xpath->query('WindowTitleBars', $contextNode);
			$pageStyle->showWindowHeaders = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : true;

			$queryResult = $xpath->query('VerticalBorder1Color', $contextNode);
			$pageStyle->verticalBorder1Color = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('VerticalBorder2Color', $contextNode);
			$pageStyle->verticalBorder2Color = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('VerticalBorderStretch', $contextNode);
			$pageStyle->verticalBorderStretch = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : true;

			$queryResult = $xpath->query('Column1', $contextNode);
			if ($queryResult->length > 0)
				$pageStyle->column1Style = PageColumnStyle::fromXml($xpath, $queryResult->item(0));

			$queryResult = $xpath->query('Column2', $contextNode);
			if ($queryResult->length > 0)
				$pageStyle->column2Style = PageColumnStyle::fromXml($xpath, $queryResult->item(0));

			$queryResult = $xpath->query('Column3', $contextNode);
			if ($queryResult->length > 0)
				$pageStyle->column3Style = PageColumnStyle::fromXml($xpath, $queryResult->item(0));

			$pageStyle->columnStyleEnabled = $pageStyle->column1Style->enabled || $pageStyle->column2Style->enabled || $pageStyle->column3Style->enabled;
			$pageStyle->showWindowHeaders = $pageStyle->showWindowHeaders && !$pageStyle->columnStyleEnabled;

			return $pageStyle;
		}

		/**
		 * @return WallbinPageStyle
		 */
		public static function createEmpty()
		{
			$pageStyle = new WallbinPageStyle();
			$pageStyle->enabled = false;
			$pageStyle->columnStyleEnabled = false;
			$pageStyle->showWindowHeaders = true;
			$pageStyle->verticalBorder1Color = null;
			$pageStyle->verticalBorder2Color = null;
			$pageStyle->column1Style = PageColumnStyle::createEmpty();
			$pageStyle->column2Style = PageColumnStyle::createEmpty();
			$pageStyle->column3Style = PageColumnStyle::createEmpty();
			return $pageStyle;
		}
	}
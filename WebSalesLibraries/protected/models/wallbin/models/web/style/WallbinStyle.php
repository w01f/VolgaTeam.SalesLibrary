<?
	namespace application\models\wallbin\models\web\style;

	/**
	 * WallbinStyle
	 */
	class WallbinStyle
	{
		/** @var  WallbinHeaderStyle */
		public $header;
		/** @var  WallbinPageStyle */
		public $page;

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return WallbinStyle
		 */
		public static function fromXml($xpath, $contextNode)
		{
			$style = self::createDefault();

			$queryResult = $xpath->query('Header', $contextNode);
			if ($queryResult->length > 0)
				$style->header = WallbinHeaderStyle::fromXml($xpath, $queryResult->item(0));

			$queryResult = $xpath->query('.//Page', $contextNode);
			if ($queryResult->length > 0)
				$style->page = WallbinPageStyle::fromXml($xpath, $queryResult->item(0));

			return $style;
		}

		/**
		 * @return WallbinStyle
		 */
		public static function createDefault()
		{
			$style = new WallbinStyle();
			$style->header = WallbinHeaderStyle::createDefault();
			$style->page = WallbinPageStyle::createDefault();
			return $style;
		}
	}
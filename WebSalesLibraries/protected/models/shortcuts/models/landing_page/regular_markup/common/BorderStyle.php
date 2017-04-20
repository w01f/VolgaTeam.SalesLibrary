<?
	namespace application\models\shortcuts\models\landing_page\regular_markup\common;

	/**
	 * Class BorderStyle
	 */
	class BorderStyle
	{
		public $color;
		/** @var  \Padding */
		public $size;

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return BorderStyle
		 */
		public static function fromXml($xpath, $contextNode)
		{
			$borderStyle = new BorderStyle();

			$queryResult = $xpath->query('./Color', $contextNode);
			$borderStyle->color = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : null;

			$queryResult = $xpath->query('./Size', $contextNode);
			if ($queryResult->length > 0)
				$borderStyle->size = \Padding::fromXml($xpath, $queryResult->item(0));
			else
				$borderStyle->size = new \Padding(0);

			return $borderStyle;
		}
	}
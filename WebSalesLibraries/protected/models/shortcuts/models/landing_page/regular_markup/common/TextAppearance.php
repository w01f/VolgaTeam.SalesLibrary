<?
	namespace application\models\shortcuts\models\landing_page\regular_markup\common;

	/**
	 * Class TextAppearance
	 */
	class TextAppearance
	{
		public $color;
		public $hoverColor;
		public $alignment;
		public $lineHeight;

		/** @var  \Font */
		public $font;

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return TextAppearance
		 */
		public static function fromXml($xpath, $contextNode)
		{
			$textAppearance = new TextAppearance();

			$queryResult = $xpath->query('./Color', $contextNode);
			$textAppearance->color = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : null;

			$queryResult = $xpath->query('./HoverColor', $contextNode);
			$textAppearance->hoverColor = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : null;

			$queryResult = $xpath->query('./TextAlign', $contextNode);
			$textAppearance->alignment = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : null;

			$queryResult = $xpath->query('./LineHeight', $contextNode);
			$textAppearance->lineHeight = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : 0;

			$queryResult = $xpath->query('./Font', $contextNode);
			if($queryResult->length > 0)
				$textAppearance->font = \Font::fromXml($xpath, $queryResult->item(0));

			return $textAppearance;
		}
	}
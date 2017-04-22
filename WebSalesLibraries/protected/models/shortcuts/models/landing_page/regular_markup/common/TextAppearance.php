<?
	namespace application\models\shortcuts\models\landing_page\regular_markup\common;

	/**
	 * Class TextAppearance
	 */
	class TextAppearance
	{
		const TextAlignLeft = 'left';
		const TextAlignCenter = 'center';
		const TextAlignRight = 'right';

		public $color;
		public $hoverColor;
		public $alignment;
		public $lineHeight;
		public $wrapText;
		public $textAlign;

		/** @var  \Font */
		public $font;

		public function __construct()
		{
			$this->wrapText = true;
		}

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
			$textAlign = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : $textAppearance->alignment;
			if (in_array($textAlign, array(self::TextAlignLeft, self::TextAlignCenter, self::TextAlignRight)))
				$textAppearance->alignment = $textAlign;

			$queryResult = $xpath->query('./LineHeight', $contextNode);
			$textAppearance->lineHeight = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : 0;

			$queryResult = $xpath->query('./Font', $contextNode);
			if($queryResult->length > 0)
				$textAppearance->font = \Font::fromXml($xpath, $queryResult->item(0));

			$queryResult = $xpath->query('./TextWrap', $contextNode);
			$textAppearance->wrapText = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $textAppearance->wrapText;



			return $textAppearance;
		}
	}
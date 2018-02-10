<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\style;

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
		public $selectedColor;
		public $alignment;
		public $lineHeight;
		public $wrapText;

		/** @var  \Font */
		public $font;

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return TextAppearance
		 */
		public static function fromXml($xpath, $contextNode)
		{
			$textAppearance = self::createEmpty();

			$queryResult = $xpath->query('./Color', $contextNode);
			$textAppearance->color = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : $textAppearance->color;

			$queryResult = $xpath->query('./HoverColor', $contextNode);
			$textAppearance->hoverColor = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : $textAppearance->hoverColor;

			$queryResult = $xpath->query('./SelectedColor', $contextNode);
			$textAppearance->selectedColor = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : $textAppearance->selectedColor;

			$queryResult = $xpath->query('./TextAlign', $contextNode);
			$textAlign = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : $textAppearance->alignment;
			if (in_array($textAlign, array(self::TextAlignLeft, self::TextAlignCenter, self::TextAlignRight)))
				$textAppearance->alignment = $textAlign;

			$queryResult = $xpath->query('./LineHeight', $contextNode);
			$textAppearance->lineHeight = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : 0;

			$queryResult = $xpath->query('./Font', $contextNode);
			if ($queryResult->length > 0)
				$textAppearance->font = \Font::fromXml($xpath, $queryResult->item(0));

			$queryResult = $xpath->query('./TextWrap', $contextNode);
			$textAppearance->wrapText = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $textAppearance->wrapText;

			return $textAppearance;
		}

		/**
		 * @return TextAppearance
		 */
		public static function createEmpty()
		{
			$textAppearance = new TextAppearance();
			$textAppearance->color = null;
			$textAppearance->hoverColor = null;
			$textAppearance->selectedColor = null;
			$textAppearance->alignment = null;
			$textAppearance->lineHeight = 0;
			$textAppearance->wrapText = true;
			$textAppearance->font = null;
			return $textAppearance;
		}
	}
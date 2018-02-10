<?php

	/**
	 * Class Font
	 */
	class Font
	{
		/**
		 * @var string
		 * @soap
		 */
		public $name;
		/**
		 * @var \Size
		 */
		public $size;
		/**
		 * @var boolean
		 * @soap
		 */
		public $isBold;
		/**
		 * @var boolean
		 * @soap
		 */
		public $isItalic;
		/**
		 * @var boolean
		 * @soap
		 */
		public $isUnderlined;

		/**
		 * @return Font
		 */
		public static function createDefault()
		{
			$font = new Font();
			$font->name = 'Arial';
			$font->size = new \Size(12);
			$font->isBold = false;
			$font->isItalic = false;
			$font->isUnderlined = false;
			return $font;
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return Font
		 */
		public static function fromXml($xpath, $contextNode)
		{
			$font = self::createDefault();

			$queryResult = $xpath->query('./Name', $contextNode);
			$font->name = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $font->name;

			$queryResult = $xpath->query('./Size', $contextNode);
			if ($queryResult->length > 0)
				$font->size = \Size::fromXml($xpath, $queryResult->item(0));

			$queryResult = $xpath->query('./IsBold', $contextNode);
			$font->isBold = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $font->isBold;

			$queryResult = $xpath->query('./IsItalic', $contextNode);
			$font->isItalic = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $font->isItalic;

			$queryResult = $xpath->query('./IsUnderlined', $contextNode);
			$font->isUnderlined = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $font->isUnderlined;

			return $font;
		}
	}


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
		 * @var int
		 * @soap
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
		public static function getDefault()
		{
			$font = new Font();
			$font->name = 'Arial';
			$font->size = 12;
			$font->isBold = false;
			$font->isItalic = false;
			$font->isUnderlined = false;
			return $font;
		}
	}


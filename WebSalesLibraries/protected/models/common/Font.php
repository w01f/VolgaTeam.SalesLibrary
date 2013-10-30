<?php
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

		public static function getDefault()
		{
			$font = new Font();
			$font->name = 'Arial';
			$font->size = 12;
			$font->isBold = false;
			$font->isItalic = false;
			return $font;
		}
	}


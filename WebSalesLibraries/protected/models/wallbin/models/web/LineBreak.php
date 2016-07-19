<?php

	/**
	 * Class LineBreak
	 */
	class LineBreak
	{
		/**
		 * @var string
		 * @soap
		 */
		public $id;
		/**
		 * @var string
		 * @soap
		 */
		public $libraryId;
		/**
		 * @var Font
		 * @soap
		 */
		public $font;
		/**
		 * @var string
		 * @soap
		 */
		public $foreColor;
		/**
		 * @var string
		 * @soap
		 */
		public $note;
		/**
		 * @var string
		 * @soap
		 */
		public $dateModify;

		/**
		 * @param $lineBreakRecord
		 */
		public function load($lineBreakRecord)
		{
			$this->id = $lineBreakRecord->id;
			$this->libraryId = $lineBreakRecord->id_library;
			$this->note = $lineBreakRecord->note;
			$this->foreColor = $lineBreakRecord->fore_color;
			$this->font = new Font();
			$this->font->name = $lineBreakRecord->font_name;
			$this->font->size = $lineBreakRecord->font_size;
			$this->font->isBold = $lineBreakRecord->font_bold;
			$this->font->isItalic = $lineBreakRecord->font_italic;
		}
	}

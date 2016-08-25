<?
	namespace application\models\wallbin\models\web;

	/**
	 * Class Banner
	 */
	class Banner
	{
		public $id;
		public $libraryId;
		public $isEnabled;
		public $image;
		public $showText;
		public $imageAlignment;
		public $text;
		/**
		 * @var \Font
		 */
		public $font;
		public $foreColor;
		public $dateModify;

		/**
		 * @param $bannerRecord
		 */
		public function load($bannerRecord)
		{
			$this->id = $bannerRecord->id;
			$this->libraryId = $bannerRecord->id_library;
			$this->isEnabled = $bannerRecord->enabled;
			$this->image = $bannerRecord->image;
			$this->showText = $bannerRecord->show_text;
			$this->imageAlignment = $bannerRecord->image_alignment;
			$this->text = $bannerRecord->text;
			$this->foreColor = $bannerRecord->fore_color;
			$this->font = new \Font();
			$this->font->name = $bannerRecord->font_name;
			$this->font->size = $bannerRecord->font_size;
			$this->font->isBold = $bannerRecord->font_bold;
			$this->font->isItalic = $bannerRecord->font_italic;
			$this->font->isUnderlined = $bannerRecord->font_underline;
		}
	}

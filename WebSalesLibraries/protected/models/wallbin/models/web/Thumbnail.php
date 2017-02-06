<?
	namespace application\models\wallbin\models\web;

	/**
	 * Class Thumbnail
	 */
	class Thumbnail
	{
		public $isEnabled;
		public $image;
		public $imagePadding;
		public $imageAlignment;
		public $showText;
		public $text;
		/**
		 * @var \Font
		 */
		public $font;
		public $foreColor;
		public $textPosition;
		public $textAlignment;

		/**
		 * @param $content string
		 * @return Thumbnail
		 */
		public static function createByContent($content)
		{
			return \CJSON::decode($content, false);
		}
	}

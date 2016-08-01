<?
	use application\models\wallbin\models\web\LibraryLink as LibraryLink;

	/**
	 * Class PowerPointPreviewData
	 */
	class PowerPointPreviewData extends DocumentPreviewData
	{
		public $slideWidth;
		public $slideHeight;

		/**
		 * @param $link LibraryLink
		 */
		public function __construct($link)
		{
			parent::__construct($link);
			$this->linkTitle = 'PowerPoint';

			/** @var  $linkSettings PowerPointLinkSettings*/
			$linkSettings = $link->extendedProperties;

			$this->slideWidth = $linkSettings->slideWidth;
			$this->slideHeight = $linkSettings->slideHeight;
		}
	}
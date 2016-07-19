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
		 * @param $isQuickSite boolean
		 */
		public function __construct($link, $isQuickSite)
		{
			parent::__construct($link, $isQuickSite);
			$this->linkTitle = 'PowerPoint';

			/** @var  $linkSettings PowerPointLinkSettings*/
			$linkSettings = $link->extendedProperties;

			$this->slideWidth = $linkSettings->slideWidth;
			$this->slideHeight = $linkSettings->slideHeight;
		}
	}
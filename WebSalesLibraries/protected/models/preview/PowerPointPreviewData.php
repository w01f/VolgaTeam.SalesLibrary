<?

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

			$this->slideWidth = $link->extendedProperties->slideWidth;
			$this->slideHeight = $link->extendedProperties->slideHeight;
		}
	}
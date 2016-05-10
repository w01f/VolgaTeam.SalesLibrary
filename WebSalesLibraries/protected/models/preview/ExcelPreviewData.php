<?

	/**
	 * Class ExcelPreviewData
	 */
	class ExcelPreviewData extends FilePreviewData
	{
		public $isEOBrowser;

		public $forceDownload;
		public $forceOpen;

		/**
		 * @param $link LibraryLink
		 * @param $isQuickSite boolean
		 */
		public function __construct($link, $isQuickSite)
		{
			parent::__construct($link, $isQuickSite);
			$this->viewerFormat = 'xls';
			$this->linkTitle = 'Excel File';

			$this->isEOBrowser = Yii::app()->browser->getBrowser() == Browser::BROWSER_EO;

			/** @var  $linkSettings ExcelLinkSettings*/
			$linkSettings = $link->extendedProperties;

			$this->forceDownload = $linkSettings->forceDownload;
			$this->forceOpen = $linkSettings->forceOpen;
		}
	}
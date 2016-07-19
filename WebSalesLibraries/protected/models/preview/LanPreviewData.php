<?
	use application\models\wallbin\models\web\LibraryLink as LibraryLink;

	/**
	 * Class LanPreviewData
	 */
	class LanPreviewData extends PreviewData
	{
		public $fileName;
		public $isEOBrowser;

		/**
		 * @param $link LibraryLink
		 * @param $isQuickSite boolean
		 */
		public function __construct($link, $isQuickSite)
		{
			parent::__construct($link, $isQuickSite);

			$this->viewerFormat = 'lan';
			$this->contentView = 'lanViewer';
			$this->fileName = $link->fileName;
			$this->isEOBrowser = Yii::app()->browser->getBrowser() == Browser::BROWSER_EO;
		}

		protected function initActions()
		{
			$this->actions = array();
		}
	}
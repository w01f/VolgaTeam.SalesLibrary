<?
	use application\models\wallbin\models\web\LibraryLink as LibraryLink;

	/**
	 * Class LanPreviewData
	 */
	class LanPreviewData extends PreviewData
	{
		public $isEOBrowser;

		/**
		 * @param $link LibraryLink
		 */
		public function __construct($link)
		{
			parent::__construct($link);

			$this->viewerFormat = 'lan';
			$this->contentView = 'lanViewer';
			$this->fileName = $link->fileName;
			$this->isEOBrowser = Yii::app()->browser->getBrowser() == Browser::BROWSER_EO;
		}

		public function initDialogActions()
		{
			$this->dialogActions = array();
		}

		public function initContextActions()
		{
			$this->contextActions = array();
		}
	}
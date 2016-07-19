<?
	use application\models\wallbin\models\web\LibraryLink as LibraryLink;

	/**
	 * Class AppLinkPreviewData
	 */
	class AppLinkPreviewData extends PreviewData
	{
		public $fileName;
		public $secondPath;

		/**
		 * @param $link LibraryLink
		 * @param $isQuickSite boolean
		 */
		public function __construct($link, $isQuickSite)
		{
			parent::__construct($link, $isQuickSite);

			$this->viewerFormat = 'app';
			$this->contentView = 'appLinkViewer';
			$this->fileName = $link->fileName;

			/** @var  $linkSettings AppLinkSettings*/
			$linkSettings = $link->extendedProperties;
			$this->secondPath = $linkSettings->secondPath;
		}

		protected function initActions()
		{
			$this->actions = array();
		}
	}
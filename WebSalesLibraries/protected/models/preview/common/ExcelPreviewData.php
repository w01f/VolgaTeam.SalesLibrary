<?
	use application\models\wallbin\models\web\LibraryLink as LibraryLink;

	/**
	 * Class ExcelPreviewData
	 */
	class ExcelPreviewData extends FilePreviewData
	{
		/**
		 * @param $link LibraryLink
		 */
		public function __construct($link)
		{
			parent::__construct($link);
			$this->viewerFormat = 'xls';
			$this->linkTitle = 'Excel File';
		}

		/**
		 * @param $isQuickSite boolean
		 */
		public function applyLinkSettings($isQuickSite)
		{
			parent::applyLinkSettings($isQuickSite);

			/** @var  $previewConfig FilePreviewConfig */
			$previewConfig = $this->config;

			/** @var  $linkSettings ExcelLinkSettings */
			$linkSettings = $this->link->extendedProperties;

			$previewConfig->forceOpen |= $linkSettings->forceOpen;
			$previewConfig->forceDownload |= $linkSettings->forceDownload;
		}
	}
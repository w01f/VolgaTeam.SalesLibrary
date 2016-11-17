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

			$previewConfig->forceEOOpen |= $linkSettings->forceOpen;
			$previewConfig->forceDownload |= $linkSettings->forceDownload;
		}

		public function initDialogActions()
		{
			parent::initDialogActions();

			if ((($this->link->fileSize * .0009765625) * .0009765625) < 10)
			{
				$imageUrlPrefix = Yii::app()->getBaseUrl(true);
				$action = new PreviewAction();
				$action->tag = 'view';
				$action->text = 'View this file';
				$action->logo = sprintf('%s/images/preview/actions/view-excel.png?%s', $imageUrlPrefix, Yii::app()->params['version']);
				$this->dialogActions = CMap::mergeArray(array($action), $this->dialogActions);
			}
		}
	}
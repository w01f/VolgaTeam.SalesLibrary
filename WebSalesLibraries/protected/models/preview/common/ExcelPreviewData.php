<?

	use application\models\wallbin\models\web\LibraryLink as LibraryLink;

	/**
	 * Class ExcelPreviewData
	 */
	class ExcelPreviewData extends FilePreviewData
	{
		public $isPhone;
		public $thumbImageSrc;

		/**
		 * @param $link LibraryLink
		 * @param $isPhone boolean
		 */
		public function __construct($link, $isPhone)
		{
			$this->isPhone = $isPhone;

			parent::__construct($link);
			$this->viewerFormat = 'xls';
			$this->linkTitle = 'Excel File';

			if ($this->isPhone)
				$this->contentView = 'excelViewer';

			$this->thumbImageSrc = $link->universalPreview->thumbItems[0]->link;
		}

		/**
		 * @param $isQuickSite boolean
		 * @param $openFromBundle boolean
		 */
		public function applyLinkSettings($isQuickSite, $openFromBundle)
		{
			parent::applyLinkSettings($isQuickSite, $openFromBundle);

			/** @var  $previewConfig FilePreviewConfig */
			$previewConfig = $this->config;
			$previewConfig->forceOpenGallery &= (($this->link->fileSize * .0009765625) * .0009765625) < 10;

			if (!$openFromBundle)
			{
				/** @var  $linkSettings ExcelLinkSettings */
				$linkSettings = $this->link->extendedProperties;

				$previewConfig->forceEOOpen |= $linkSettings->forceOpen;
				$previewConfig->forceDownload |= $linkSettings->forceDownload;
			}
		}

		public function initDialogActions()
		{
			parent::initDialogActions();
			if (!$this->isPhone && (($this->link->fileSize * .0009765625) * .0009765625) < 10)
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
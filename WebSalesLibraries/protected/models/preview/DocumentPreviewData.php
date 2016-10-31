<?
	use application\models\wallbin\models\web\LibraryLink as LibraryLink;

	/**
	 * Class DocumentPreviewData
	 */
	class DocumentPreviewData extends GalleryPreviewData
	{
		public $documentInPdf;
		public $pages;

		/**
		 * @param $link LibraryLink
		 */
		public function __construct($link)
		{
			parent::__construct($link);

			if ($this->galleryEnabled)
			{
				$this->viewerFormat = 'document';
				$this->contentView = 'documentViewer';
			}
			else
			{
				$this->viewerFormat = 'file';
				$this->contentView = 'fileViewer';
			}

			$this->documentInPdf = $link->universalPreview->pdf;

			$this->pages = array();
			$i = 0;
			foreach ($link->universalPreview->officeItems as $previewFile)
			{
				$pageItem = new PagePreviewItem();
				$pageItem->fileName = sprintf('%s-%s%s.%s', str_replace('.' . $link->fileExtension, '', $link->fileName), $this->pageItemName, ($i + 1), pathinfo($previewFile->path, PATHINFO_EXTENSION));
				$pageItem->href = $previewFile->link;
				$pageItem->path = $previewFile->path;
				$pageItem->size = FilePreviewData::formatFileSize($previewFile->size);
				$this->pages[] = $pageItem;
				$i++;
			}
		}

		/**
		 * @return PreviewAction[]
		 */
		protected function getDownloadActions()
		{
			$actions = array();

			$imageUrlPrefix = Yii::app()->getBaseUrl(true);

			if ($this->config->allowDownload)
			{
				$action = new PreviewAction();
				$action->tag = 'download';
				$action->text = 'DOWNLOAD this file';
				$action->logo = sprintf('%s/images/preview/actions/download.png?%s', $imageUrlPrefix, Yii::app()->params['version']);
				$actions[] = $action;
			}

			if ($this->config->allowPdf)
			{
				$action = new PreviewAction();
				$action->tag = 'download-pdf';
				$action->text = 'Open PDF file';
				$action->logo = sprintf('%s/images/preview/actions/open-pdf.png?%s', $imageUrlPrefix, Yii::app()->params['version']);
				$actions[] = $action;
			}

			return $actions;
		}
	}
<?

	/**
	 * Class DocumentPreviewData
	 */
	class DocumentPreviewData extends GalleryPreviewData
	{
		public $documentInPdf;
		public $pages;

		/**
		 * @param $link LibraryLink
		 * @param $isQuickSite boolean
		 */
		public function __construct($link, $isQuickSite)
		{
			parent::__construct($link, $isQuickSite);

			switch ($this->format)
			{
				case 'ppt':
					$this->linkTitle = 'PowerPoint';
					break;
				case 'doc':
					$this->linkTitle = 'Word';
					break;
			}

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
				$pageItem->fileName = sprintf('%s-%s%s.%s', str_replace('.' . $link->fileExtension, '', $link->fileName), $this->pageItemName, ($i + 1), $link->fileExtension);
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
				$action->text = 'DOWNLOAD this file to your Desktop or Mobile Device...';
				$action->shortText = 'DOWNLOAD this file';
				$action->logo = sprintf('%s/images/preview/actions/download-%s.png?%s', $imageUrlPrefix, $this->format, Yii::app()->params['version']);
				$actions[] = $action;
			}

			if ($this->config->allowPdf)
			{
				$action = new PreviewAction();
				$action->tag = 'download-pdf';
				$action->text = 'Open PDF version of this file to your Desktop or Mobile Device...';
				$action->shortText = 'Open PDF file';
				$action->logo = sprintf('%s/images/preview/actions/download-pdf.png?%s', $imageUrlPrefix, $this->format, Yii::app()->params['version']);
				$actions[] = $action;
			}

			return $actions;
		}
	}
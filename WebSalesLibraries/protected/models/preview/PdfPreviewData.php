<?

	/**
	 * Class PdfPreviewData
	 */
	class PdfPreviewData extends GalleryPreviewData
	{
		public $documentInPdf;

		/**
		 * @param $link LibraryLink
		 * @param $isQuickSite boolean
		 */
		public function __construct($link, $isQuickSite)
		{
			parent::__construct($link, $isQuickSite);
			$this->linkTitle ='PDF file';
			if ($this->galleryEnabled)
			{
				$this->viewerFormat = 'document';
				$this->contentView = 'pdfViewer';
			}
			else
			{
				$this->viewerFormat = 'file';
				$this->contentView = 'fileViewer';
			}

			$this->documentInPdf = new PreviewFile();
			$this->documentInPdf->name = $this->fileName;
			$this->documentInPdf->path = $this->filePath;
			$this->documentInPdf->link = $this->url;
		}
	}
<?

	/**
	 * Class ImagePreviewData
	 */
	class ImagePreviewData extends FilePreviewData
	{
		public function __construct($link)
		{
			parent::__construct($link);
			$this->viewerFormat = 'image';
			$this->contentView = 'imageViewer';
		}
	}
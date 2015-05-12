<?

	/**
	 * Class ImagePreviewData
	 */
	class ImagePreviewData extends FilePreviewData
	{
		/**
		 * @param $link LibraryLink
		 * @param $isQuickSite boolean
		 */
		public function __construct($link, $isQuickSite)
		{
			parent::__construct($link, $isQuickSite);
			$this->viewerFormat = 'image';
			$this->contentView = 'imageViewer';
		}
	}
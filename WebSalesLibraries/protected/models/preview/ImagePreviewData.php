<?
	use application\models\wallbin\models\web\LibraryLink as LibraryLink;

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

			switch ($this->format)
			{
				case 'png':
					$this->linkTitle ='PNG File';
					break;
				case 'jpeg':
					$this->linkTitle ='JPEG File';
					break;
			}
		}
	}
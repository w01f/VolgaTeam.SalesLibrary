<?
	use application\models\wallbin\models\web\LibraryLink as LibraryLink;

	/**
	 * Class ImagePreviewData
	 */
	class ImagePreviewData extends FilePreviewData
	{
		/**
		 * @param $link LibraryLink
		 */
		public function __construct($link)
		{
			parent::__construct($link);
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
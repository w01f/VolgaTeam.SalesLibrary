<?

	/**
	 * Class VideoPreviewData
	 */
	class VideoPreviewData extends FilePreviewData
	{
		public $forcePreview;

		public $thumbImageSrc;

		public $playerSrc;

		public $mp4Src;

		/**
		 * @param $link LibraryLink
		 * @param $isQuickSite boolean
		 */
		public function __construct($link, $isQuickSite)
		{
			parent::__construct($link, $isQuickSite);
			$this->viewerFormat = 'video';
			$this->contentView = 'videoViewer';
			$this->linkTitle ='Video';

			$this->thumbImageSrc = $link->universalPreview->mp4Thumb->link;
			$this->forcePreview = $link->extendedProperties->forcePreview;
			$this->playerSrc = Yii::app()->getBaseUrl(true) . '/vendor/video-js/video-js.swf';

			$this->mp4Src = new VideoPreviewItem();
			$this->mp4Src->type = 'video/mp4';
			if (isset($link->universalPreview->mp4))
			{
				$this->mp4Src->title = $link->universalPreview->mp4->name;
				$this->mp4Src->href = $link->universalPreview->mp4->link;
				$this->mp4Src->path = $link->universalPreview->mp4->path;
			}
			else
			{
				$this->mp4Src->title = $this->fileName;
				$this->mp4Src->href = $this->url;
				$this->mp4Src->path = $this->filePath;
			}
		}
	}
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
		public $wmvSrc;
		public $ogvSrc;

		/**
		 * @param $link LibraryLink
		 */
		public function __construct($link)
		{
			parent::__construct($link);
			$this->viewerFormat = 'video';
			$this->contentView = 'videoViewer';

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

			if (isset($link->universalPreview->wmv))
			{
				$this->wmvSrc = new VideoPreviewItem();
				$this->wmvSrc->title = $link->universalPreview->wmv->name;
				$this->wmvSrc->type = 'video/wmv';
				$this->wmvSrc->href = $link->universalPreview->wmv->link;
				$this->wmvSrc->path = $link->universalPreview->wmv->path;
			}

			if (isset($link->universalPreview->ogv))
			{
				$this->ogvSrc = new VideoPreviewItem();
				$this->ogvSrc->title = $link->universalPreview->ogv->name;
				$this->ogvSrc->type = 'video/ogg';
				$this->ogvSrc->href = $link->universalPreview->ogv->link;
				$this->ogvSrc->path = $link->universalPreview->ogv->path;
			}
		}

		/**
		 * @return PreviewAction[]
		 */
		protected function getDownloadActions()
		{
			$imageUrlPrefix = Yii::app()->getBaseUrl(true);

			$actions = array();

			$action = new PreviewAction();
			$action->tag = 'download-mp4';
			$action->text = 'DOWNLOAD MP4 version of this file to your Desktop or Mobile Device...';
			$action->logo = sprintf('%s/images/preview/actions/download-mp4.png?%s', $imageUrlPrefix, Yii::app()->params['version']);
			$actions[] = $action;

			if (isset($this->link->universalPreview->wmv))
			{
				$action = new PreviewAction();
				$action->tag = 'download-wmv';
				$action->text = 'DOWNLOAD WMV version of this file to your Desktop or Mobile Device...';
				$action->logo = sprintf('%s/images/preview/actions/download-wmv.png?%s', $imageUrlPrefix, Yii::app()->params['version']);
				$actions[] = $action;
			}

			return $actions;
		}
	}
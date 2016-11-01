<?
	use application\models\wallbin\models\web\LibraryLink as LibraryLink;

	/**
	 * Class VideoPreviewData
	 */
	class VideoPreviewData extends FilePreviewData
	{
		public $forcePreview;

		public $thumbImageSrc;

		public $mp4Src;

		/**
		 * @param $link LibraryLink
		 */
		public function __construct($link)
		{
			parent::__construct($link);
			$this->viewerFormat = 'video';
			$this->contentView = 'videoViewer';
			$this->linkTitle ='Video';

			$this->thumbImageSrc = $link->universalPreview->mp4Thumb->link;

			/** @var  $linkSettings VideoLinkSettings */
			$linkSettings = $this->link->extendedProperties;
			$this->forcePreview = $linkSettings->forcePreview;

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
<?

	/**
	 * Class Mp3PreviewData
	 */
	class Mp3PreviewData extends FilePreviewData
	{
		/**
		 * @param $link LibraryLink
		 * @param $isQuickSite boolean
		 */
		public function __construct($link, $isQuickSite)
		{
			parent::__construct($link, $isQuickSite);
			$this->viewerFormat = 'file';
			$this->contentView = 'fileViewer';
			$this->linkTitle ='MP3 File';

			$imageUrlPrefix = Yii::app()->getBaseUrl(true);
			$action = new PreviewAction();
			$action->tag = 'play';
			$action->text = 'Play this file...';
			$action->shortText = 'Play this file';
			$action->logo = sprintf('%s/images/preview/actions/download-mp3.png?%s', $imageUrlPrefix, $this->format, Yii::app()->params['version']);

			$this->actions = CMap::mergeArray(array($action), $this->actions);
		}
	}
<?

	/**
	 * Class Mp3PreviewData
	 */
	class Mp3PreviewData extends FilePreviewData
	{
		public function __construct($link)
		{
			parent::__construct($link);
			$this->viewerFormat = 'file';
			$this->contentView = 'fileViewer';

			$imageUrlPrefix = Yii::app()->getBaseUrl(true);
			$action = new PreviewAction();
			$action->tag = 'play';
			$action->text = 'Play this file...';
			$action->logo = sprintf('%s/images/preview/actions/download-mp3.png?%s', $imageUrlPrefix, $this->format, Yii::app()->params['version']);

			$this->actions = CMap::mergeArray(array($action), $this->actions);
		}
	}
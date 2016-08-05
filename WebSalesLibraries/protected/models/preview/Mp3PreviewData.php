<?
	use application\models\wallbin\models\web\LibraryLink as LibraryLink;

	/**
	 * Class Mp3PreviewData
	 */
	class Mp3PreviewData extends FilePreviewData
	{
		/**
		 * @param $link LibraryLink
		 */
		public function __construct($link)
		{
			parent::__construct($link);
			$this->viewerFormat = 'file';
			$this->contentView = 'fileViewer';
			$this->linkTitle = 'MP3 File';
		}

		public function initDialogActions()
		{
			parent::initDialogActions();

			$imageUrlPrefix = Yii::app()->getBaseUrl(true);
			$action = new PreviewAction();
			$action->tag = 'play';
			$action->text = 'Play this file...';
			$action->shortText = 'Play this file';
			$action->logo = sprintf('%s/images/preview/actions/download-mp3.png?%s', $imageUrlPrefix, $this->format, Yii::app()->params['version']);

			$this->dialogActions = CMap::mergeArray(array($action), $this->dialogActions);
		}
	}
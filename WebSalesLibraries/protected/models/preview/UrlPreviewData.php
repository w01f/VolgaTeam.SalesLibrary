<?

	/**
	 * Class UrlPreviewData
	 */
	class UrlPreviewData extends PreviewData
	{
		public $isOffice365;

		public function __construct($link)
		{
			parent::__construct($link);
			$this->viewerFormat = 'url';
			$this->contentView = 'urlViewer';

			$this->isOffice365 = $this->format == 'url365';

			$imageUrlPrefix = Yii::app()->getBaseUrl(true);
			$linkTitle = $this->isOffice365 ? 'Office 365 Link' : 'URL';

			$this->actions = array();

			$action = new PreviewAction();
			$action->tag = 'open';
			$action->text = sprintf('OPEN this %s in a new browser window...',$linkTitle);
			$action->logo = sprintf('%s/images/preview/actions/open-%s.png?%s', $imageUrlPrefix, $this->format, Yii::app()->params['version']);
			$this->actions[] = $action;


			$action = new PreviewAction();
			$action->tag = 'quicksite';
			$action->text = sprintf('Save this %s to a Quick Site...',$linkTitle);
			$action->logo = sprintf('%s/images/preview/actions/quicksite.png?%s', $imageUrlPrefix, Yii::app()->params['version']);
			$this->actions[] = $action;

			$action = new PreviewAction();
			$action->tag = 'favorites';
			$action->text = sprintf('Add this %s to my Favorites...',$linkTitle);
			$action->logo = sprintf('%s/images/preview/actions/favorites.png?%s', $imageUrlPrefix, Yii::app()->params['version']);
			$this->actions[] = $action;
		}
	}
<?

	/**
	 * Class LanPreviewData
	 */
	class LanPreviewData extends PreviewData
	{
		public $fileName;
		public $isEOBrowser;

		/**
		 * @param $link LibraryLink
		 * @param $isQuickSite boolean
		 */
		public function __construct($link, $isQuickSite)
		{
			parent::__construct($link, $isQuickSite);

			$this->viewerFormat = 'lan';
			$this->contentView = 'lanViewer';

			$this->fileName = $link->fileName;
			$this->linkTitle = 'LAN link';

			$this->isEOBrowser = Yii::app()->browser->getBrowser() == Browser::BROWSER_EO;
		}

		protected function initActions()
		{
			$this->actions = array();
			$imageUrlPrefix = Yii::app()->getBaseUrl(true);
			$action = new PreviewAction();
			$action->tag = 'open';
			$action->text = sprintf('OPEN this %s in a new browser window...', $this->linkTitle);
			$action->shortText = sprintf('OPEN this %s<br>%s', $this->linkTitle, $this->url);
			$action->logo = sprintf('%s/images/preview/actions/open-%s.png?%s', $imageUrlPrefix, 'url', Yii::app()->params['version']);
			$this->actions[] = $action;

			if ($this->config->allowAddToQuickSite)
			{
				$action = new PreviewAction();
				$action->tag = 'quicksite';
				$action->text = sprintf('Save this %s to a Quick Site...', $this->linkTitle);
				$action->shortText = sprintf('Email a Link to this %s', $this->linkTitle);
				$action->logo = sprintf('%s/images/preview/actions/quicksite.png?%s', $imageUrlPrefix, Yii::app()->params['version']);
				$this->actions[] = $action;
			}

			if ($this->config->allowAddToFavorites)
			{
				$action = new PreviewAction();
				$action->tag = 'favorites';
				$action->text = sprintf('Add this %s to my Favorites...', $this->linkTitle);
				$action->shortText = sprintf('Add to Favorites', $this->linkTitle);
				$action->logo = sprintf('%s/images/preview/actions/favorites.png?%s', $imageUrlPrefix, Yii::app()->params['version']);
				$this->actions[] = $action;
			}
		}
	}
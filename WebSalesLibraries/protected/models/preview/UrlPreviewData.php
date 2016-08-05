<?
	use application\models\wallbin\models\web\LibraryLink as LibraryLink;

	/**
	 * Class UrlPreviewData
	 */
	class UrlPreviewData extends PreviewData
	{
		public $fileName;

		/**
		 * @param $link LibraryLink
		 */
		public function __construct($link)
		{
			parent::__construct($link);

			$this->viewerFormat = 'url';
			$this->contentView = 'urlViewer';

			$this->fileName = $link->fileName;

			switch ($this->format)
			{
				case 'quicksite':
					$this->linkTitle = 'QuickSite';
					break;
				default:
					$this->linkTitle = 'URL';
					break;
			}
		}

		public function initDialogActions()
		{
			switch ($this->format)
			{
				case 'quicksite':
					$linkTitle = 'QuickSite';
					break;
				default:
					$linkTitle = 'URL';
					break;
			}

			$this->dialogActions = array();
			$imageUrlPrefix = Yii::app()->getBaseUrl(true);
			$action = new PreviewAction();
			$action->tag = 'open';
			$action->text = sprintf('OPEN this %s in a new browser window...', $linkTitle);
			$action->shortText = sprintf('OPEN this %s<br>%s', $linkTitle, $this->url);
			$action->logo = sprintf('%s/images/preview/actions/open-url.png?%s', $imageUrlPrefix, Yii::app()->params['version']);
			$this->dialogActions[] = $action;

			if ($this->config->allowAddToQuickSite)
			{
				$action = new PreviewAction();
				$action->tag = 'quicksite';
				$action->text = sprintf('Save this %s to a Quick Site...', $linkTitle);
				$action->shortText = sprintf('Email a Link to this %s', $linkTitle);
				$action->logo = sprintf('%s/images/preview/actions/quicksite.png?%s', $imageUrlPrefix, Yii::app()->params['version']);
				$this->dialogActions[] = $action;
			}

			if ($this->config->allowAddToFavorites)
			{
				$action = new PreviewAction();
				$action->tag = 'favorites';
				$action->text = sprintf('Add this %s to my Favorites...', $this->linkTitle);
				$action->shortText = sprintf('Add to Favorites', $this->linkTitle);
				$action->logo = sprintf('%s/images/preview/actions/favorites.png?%s', $imageUrlPrefix, Yii::app()->params['version']);
				$this->dialogActions[] = $action;
			}
		}

		public function initContextActions()
		{
			$this->contextActions = array();

			if ($this->link->isDirectUrl && $this->config->isEOBrowser)
				return;

			$action = new PreviewAction();
			$action->tag = 'open';
			$action->text = 'Open this URL Link';
			$this->contextActions[] = $action;

			if ($this->config->allowAddToQuickSite)
			{
				$action = new PreviewAction();
				$action->tag = 'linkcart';
				$action->text = 'Add this URL to my QuickSites Cart';
				$this->contextActions[] = $action;

				$action = new PreviewAction();
				$action->tag = 'quicksite';
				$action->text = 'Email this URL';
				$this->contextActions[] = $action;
			}
			if ($this->config->allowAddToFavorites)
			{
				$action = new PreviewAction();
				$action->tag = 'favorites';
				$action->text = 'Save to Favorites';
				$this->contextActions[] = $action;
			}
			if ($this->config->enableRating)
			{
				$action = new PreviewAction();
				$action->tag = 'rate';
				$action->text = 'Rate this URL Link';
				$this->contextActions[] = $action;
			}
		}
	}
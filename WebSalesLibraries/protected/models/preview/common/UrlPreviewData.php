<?
	use application\models\wallbin\models\web\LibraryLink as LibraryLink;

	/**
	 * Class UrlPreviewData
	 */
	class UrlPreviewData extends PreviewData
	{
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
				case 'html5':
					$this->linkTitle = 'HTML5 URL';
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
				case 'html5':
					$linkTitle = 'HTML5 URL';
					break;
				default:
					$linkTitle = 'URL';
					break;
			}

			$this->dialogActions = array();
			$imageUrlPrefix = Yii::app()->getBaseUrl(true);
			$action = new PreviewAction();
			$action->tag = 'open';
			$action->text = sprintf('OPEN this %s<br>%s', $linkTitle, $this->url);
			$action->logo = sprintf('%s/images/preview/actions/download.png?%s', $imageUrlPrefix, Yii::app()->params['version']);
			$this->dialogActions[] = $action;

			if ($this->config->allowAddToQuickSite)
			{
				$action = new PreviewAction();
				$action->tag = 'quicksite';
				$action->text = sprintf('Email a Link to this %s', $linkTitle);
				$action->logo = sprintf('%s/images/preview/actions/quicksite.png?%s', $imageUrlPrefix, Yii::app()->params['version']);
				$this->dialogActions[] = $action;
			}

			if ($this->config->allowAddToFavorites)
			{
				$action = new PreviewAction();
				$action->tag = 'favorites';
				$action->text = sprintf('Add to Favorites', $this->linkTitle);
				$action->logo = sprintf('%s/images/preview/actions/favorites.png?%s', $imageUrlPrefix, Yii::app()->params['version']);
				$this->dialogActions[] = $action;
			}
		}

		public function initContextActions()
		{
			$this->contextActions = array();

			if ($this->link->isDirectUrl && $this->config->isEOBrowser)
				return;

			$action = new ContextMenuAction();
			$action->tag = 'open';
			$action->url = $this->link->isDirectUrl ? $this->url : $action->url;
			$action->text = 'Open this URL Link';
			$this->contextActions[] = $action;

			if ($this->config->allowAddToQuickSite)
			{
				$action = new ContextMenuAction();
				$action->tag = 'linkcart';
				$action->text = 'Add this URL to my QuickSites Cart';
				$action->beginGroup = true;
				$this->contextActions[] = $action;

				$action = new ContextMenuAction();
				$action->tag = 'quicksite';
				$action->text = 'Email this URL';
				$action->beginGroup = true;
				$this->contextActions[] = $action;
			}
			if ($this->config->allowAddToFavorites)
			{
				$action = new ContextMenuAction();
				$action->tag = 'favorites';
				$action->text = 'Save to Favorites';
				$this->contextActions[] = $action;
			}
			if ($this->config->allowDownload)
			{
				$action = new ContextMenuAction();
				$action->tag = 'zip-library-folder';
				$action->text = 'Download ALL in this window';
				$action->beginGroup = true;
				$action->onlyWallbinAction = true;
				$this->contextActions[] = $action;
			}
			if ($this->config->enableRating)
			{
				$action = new ContextMenuAction();
				$action->tag = 'rate';
				$action->text = 'Rate this URL Link';
				$action->beginGroup = $this->config->allowDownload || $this->config->allowAddToFavorites || $this->config->allowAddToQuickSite;
				$this->contextActions[] = $action;
			}
		}
	}
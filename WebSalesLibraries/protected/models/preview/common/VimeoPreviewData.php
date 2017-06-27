<?
	use application\models\wallbin\models\web\LibraryLink as LibraryLink;

	/**
	 * Class VimeoPreviewData
	 */
	class VimeoPreviewData extends PreviewData
	{
		public $playerUrl;
		public $forcePreview;

		/**
		 * @param $link LibraryLink
		 */
		public function __construct($link)
		{
			parent::__construct($link);

			$this->linkTitle = 'Vimeo link';
			$this->viewerFormat = 'vimeo';
			$this->contentView = 'vimeoViewer';

			$this->fileName = $link->fileName;
			if (preg_match('/https?:\/\/(?:www\.|player\.)?vimeo.com\/(?:channels\/(?:\w+\/)?|groups\/([^\/]*)\/videos\/|album\/(\d+)\/video\/|video\/|(\w*\/)*review\/|)(\d+)(?:$|\/|\?)/', $this->url, $match))
				$this->playerUrl = 'https://player.vimeo.com/video/'.$match[4];
			else
				$this->playerUrl = $this->url;

				$this->forcePreview = $link->extendedProperties->forcePreview;
		}

		public function initDialogActions()
		{
			$this->dialogActions = array();
			$imageUrlPrefix = Yii::app()->getBaseUrl(true);
			$action = new PreviewAction();
			$action->tag = 'open';
			$action->text = sprintf('OPEN this %s<br>%s', $this->linkTitle, $this->url);
			$action->logo = sprintf('%s/images/preview/actions/download.png?%s', $imageUrlPrefix, Yii::app()->params['version']);
			$this->dialogActions[] = $action;

			if ($this->config->allowAddToQuickSite)
			{
				$action = new PreviewAction();
				$action->tag = 'quicksite';
				$action->text = sprintf('Email a Link to this %s', $this->linkTitle);
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

			$action = new PreviewAction();
			$action->tag = 'open';
			$action->url = $this->link->isDirectUrl ? $this->url : $action->url;
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
			if ($this->config->allowDownload)
			{
				$action = new PreviewAction();
				$action->tag = 'zip-library-folder';
				$action->text = 'Download ALL in this window';
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
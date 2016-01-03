<?

	/**
	 * Class PreviewData
	 */
	abstract class PreviewData
	{
		public $linkId;
		public $name;
		public $format;
		public $tags;
		public $url;
		public $linkTitle;

		/**
		 * @var PreviewConfig config
		 */
		public $config;

		public $rateData;

		public $viewerFormat;
		public $contentView;

		public $actions;

		protected $link;

		/**
		 * @param $link LibraryLink
		 * @param $isQuickSite boolean
		 */
		public function __construct($link, $isQuickSite)
		{
			$this->link = $link;
			$this->linkId = $link->id;
			$this->name = $link->name;
			$this->format = $link->originalFormat;
			$this->tags = $link->getTagsString();
			$this->url = $link->fileLink;

			$this->config = new PreviewConfig();
			$this->config->init($link, $isQuickSite);

			$userId = UserIdentity::getCurrentUserId();
			$this->rateData = LinkRateRecord::getRateData($link->id, $userId);

			$this->initActions();
		}

		protected function initActions()
		{
			$this->actions = array();
			$imageUrlPrefix = Yii::app()->getBaseUrl(true);
			if ($this->config->allowAddToQuickSite)
			{
				$action = new PreviewAction();
				$action->tag = 'quicksite';
				$action->text = 'Add this file to a QUICKSITE...';
				$action->shortText = 'Add to a QUICKSITE';
				$action->logo = sprintf('%s/images/preview/actions/quicksite.png?%s', $imageUrlPrefix, Yii::app()->params['version']);
				$this->actions[] = $action;
			}

			if ($this->config->allowAddToFavorites)
			{
				$action = new PreviewAction();
				$action->tag = 'favorites';
				$action->text = 'Save a Copy of this file to your FAVORITES page...';
				$action->shortText = 'Add to Favorites';
				$action->logo = sprintf('%s/images/preview/actions/favorites.png?%s', $imageUrlPrefix, Yii::app()->params['version']);
				$this->actions[] = $action;
			}
		}

		/**
		 * @param $link LibraryLink
		 * @param $isQuickSite boolean
		 * @return PreviewData
		 */
		public static function getInstance($link, $isQuickSite)
		{
			switch ($link->originalFormat)
			{
				case 'ppt':
				case 'doc':
					return new DocumentPreviewData($link, $isQuickSite);
				case 'pdf':
					return new PdfPreviewData($link, $isQuickSite);
				case 'video':
				case 'wmv':
				case 'mp4':
					return new VideoPreviewData($link, $isQuickSite);
				case 'png':
				case 'jpeg':
					return new ImagePreviewData($link, $isQuickSite);
				case 'url':
				case 'url365':
					return new UrlPreviewData($link, $isQuickSite);
				case 'mp3':
					return new Mp3PreviewData($link, $isQuickSite);
				case 'xls':
					return new ExcelPreviewData($link, $isQuickSite);
				default:
					return new FilePreviewData($link, $isQuickSite);
			}
		}
	}
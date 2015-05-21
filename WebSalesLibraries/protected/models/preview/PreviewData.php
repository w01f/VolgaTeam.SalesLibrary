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

		public $rateData;

		public $userAuthorized;

		public $allowAddToFavorites;
		public $allowAddToQuickSite;

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

			$this->userAuthorized = false;
			$userId = -1;
			if (isset(Yii::app()->user))
			{
				$userId = Yii::app()->user->getId();
				$this->userAuthorized = isset($userId);
			}
			$this->allowAddToFavorites = $this->userAuthorized;
			$this->allowAddToQuickSite = !$isQuickSite;

			$this->rateData = LinkRateRecord::getRateData($link->id, $userId);

			$imageUrlPrefix = Yii::app()->getBaseUrl(true);

			$this->actions = array();
			if ($this->allowAddToQuickSite)
			{
				$action = new PreviewAction();
				$action->tag = 'quicksite';
				$action->text = 'Add this file to a QUICKSITE...';
				$action->logo = sprintf('%s/images/preview/actions/quicksite.png?%s', $imageUrlPrefix, Yii::app()->params['version']);
				$this->actions[] = $action;
			}

			if ($this->allowAddToFavorites)
			{
				$action = new PreviewAction();
				$action->tag = 'favorites';
				$action->text = 'Save a Copy of this file to your FAVORITES page...';
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
				default:
					return new FilePreviewData($link, $isQuickSite);
			}
		}
	}
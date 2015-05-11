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
		 */
		public function __construct($link)
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

			$this->rateData = LinkRateRecord::getRateData($link->id, $userId);

			$imageUrlPrefix = Yii::app()->getBaseUrl(true);
			$action = new PreviewAction();
			$action->tag = 'quicksite';
			$action->text = 'Add this file to a QUICKSITE...';
			$action->logo = sprintf('%s/images/preview/actions/quicksite.png?%s', $imageUrlPrefix, Yii::app()->params['version']);
			$this->actions[] = $action;

			$action = new PreviewAction();
			$action->tag = 'favorites';
			$action->text = 'Save a Copy of this file to your FAVORITES page...';
			$action->logo = sprintf('%s/images/preview/actions/favorites.png?%s', $imageUrlPrefix, Yii::app()->params['version']);
			$this->actions[] = $action;
		}

		/**
		 * @param $link LibraryLink
		 * @return PreviewData
		 */
		public static function getInstance($link)
		{
			switch ($link->originalFormat)
			{
				case 'ppt':
				case 'doc':
					return new DocumentPreviewData($link);
				case 'pdf':
					return new PdfPreviewData($link);
				case 'video':
				case 'wmv':
				case 'mp4':
					return new VideoPreviewData($link);
				case 'png':
				case 'jpeg':
					return new ImagePreviewData($link);
				case 'url':
				case 'url365':
					return new UrlPreviewData($link);
				case 'mp3':
					return new Mp3PreviewData($link);
				default:
					return new FilePreviewData($link);
			}
		}
	}
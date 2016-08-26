<?
	use application\models\wallbin\models\web\LibraryLink as LibraryLink;

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
		 * @var BasePreviewConfig config
		 */
		public $config;

		public $rateData;

		public $totalViews;

		public $viewerFormat;
		public $contentView;

		public $dialogActions;
		public $contextActions;

		protected $link;

		/**
		 * @param $link LibraryLink
		 */
		public function __construct($link)
		{
			$this->link = $link;
			$this->linkId = $link->id;
			$this->name = $link->extendedProperties->isTextWordWrap ?
				$link->fileName :
				$link->name;
			$this->format = $link->originalFormat;
			$this->tags = $link->getTagsString();
			$this->url = str_replace('SalesLibraries/SalesLibraries', 'SalesLibraries', $link->fileLink);

			$userId = UserIdentity::getCurrentUserId();
			$this->rateData = LinkRateRecord::getRateData($link->id, $userId);

			$this->totalViews = $link->getTotalViews();
		}

		/**
		 * @param $isQuickSite boolean
		 */
		public function applyLinkSettings($isQuickSite)
		{
			$this->config = new BasePreviewConfig();
			$this->config->init($this->link, $isQuickSite);
		}

		public function initDialogActions()
		{
			$this->dialogActions = array();
			$imageUrlPrefix = Yii::app()->getBaseUrl(true);
			if ($this->config->allowAddToQuickSite)
			{
				$action = new PreviewAction();
				$action->tag = 'quicksite';
				$action->text = 'Add this file to a QUICKSITE...';
				$action->shortText = 'Add to a QUICKSITE';
				$action->logo = sprintf('%s/images/preview/actions/quicksite.png?%s', $imageUrlPrefix, Yii::app()->params['version']);
				$this->dialogActions[] = $action;
			}

			if ($this->config->allowAddToFavorites)
			{
				$action = new PreviewAction();
				$action->tag = 'favorites';
				$action->text = 'Save a Copy of this file to your FAVORITES page...';
				$action->shortText = 'Add to Favorites';
				$action->logo = sprintf('%s/images/preview/actions/favorites.png?%s', $imageUrlPrefix, Yii::app()->params['version']);
				$this->dialogActions[] = $action;
			}
		}

		public abstract function initContextActions();

		/**
		 * @param $link LibraryLink
		 * @param $isQuickSite boolean
		 * @return PreviewData
		 */
		public static function getInstance($link, $isQuickSite)
		{
			$previewData = null;
			switch ($link->originalFormat)
			{
				case 'ppt':
					$previewData = new PowerPointPreviewData($link);
					break;
				case 'doc':
					$previewData = new WordPreviewData($link);
					break;
				case 'pdf':
					$previewData = new PdfPreviewData($link);
					break;
				case 'video':
				case 'wmv':
				case 'mp4':
					$previewData = new VideoPreviewData($link);
					break;
				case 'png':
				case 'jpeg':
					$previewData = new ImagePreviewData($link);
					break;
				case 'url':
				case 'quicksite':
					$previewData = new UrlPreviewData($link);
					break;
				case 'youtube':
					$previewData = new YouTubePreviewData($link);
					break;
				case 'lan':
					$previewData = new LanPreviewData($link);
					break;
				case 'mp3':
					$previewData = new Mp3PreviewData($link);
					break;
				case 'xls':
					$previewData = new ExcelPreviewData($link);
					break;
				case 'app':
					$previewData = new AppLinkPreviewData($link);
					break;
				case 'internal':
					$previewData = new InternalLinkPreviewData($link);
					break;
				case 'folder':
					$previewData = new FolderPreviewData($link);
					break;
				case 'line break':
					$previewData = new LineBreakPreviewData($link);
					break;
				default:
					if ($link->isFolder)
						$previewData = new FolderPreviewData($link);
					else if ($link->isLineBreak)
						$previewData = new LineBreakPreviewData($link);
					else
						$previewData = new FilePreviewData($link);
					break;
			}
			$previewData->applyLinkSettings($isQuickSite);
			$previewData->initDialogActions();
			$previewData->initContextActions();
			return $previewData;
		}
	}
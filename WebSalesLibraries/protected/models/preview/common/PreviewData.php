<?

	use application\models\wallbin\models\web\LibraryLink as LibraryLink;

	/**
	 * Class PreviewData
	 */
	abstract class PreviewData
	{
		public $linkId;
		public $folderId;
		public $linkBundleId;
		public $name;
		public $format;
		public $tags;
		public $url;
		public $linkTitle;

		public $fileName;
		public $fileLogo;

		public $quickLinkUrl;
		public $quickLinkLogo;
		public $quickLinkTitle;

		public $showBundleInfo;

		/**
		 * @var BasePreviewConfig config
		 */
		public $config;

		public $rateData;

		public $totalViews;

		public $viewerFormat;
		public $contentView;

		/** @var  PreviewAction[] */
		public $dialogActions;

		/** @var  PreviewAction[] */
		public $contextActions;

		protected $link;

		/**
		 * @param $link LibraryLink
		 */
		public function __construct($link)
		{
			$this->link = $link;
			$this->linkId = $link->id;
			$this->folderId = $link->folderId;
			$this->name = $link->extendedProperties->isTextWordWrap ?
				$link->fileName :
				$link->name;
			$this->format = $link->originalFormat;
			$this->tags = $link->getTagsString();
			$this->url = str_replace('SalesLibraries/SalesLibraries', 'SalesLibraries', $link->fileLink);

			$this->fileLogo = sprintf('%s/images/preview/actions/file-logo-unknown.png?%s', Yii::app()->getBaseUrl(true), Yii::app()->params['version']);

			$this->quickLinkUrl = isset($link->extendedProperties->quickLinkUrl) ? $link->extendedProperties->quickLinkUrl : null;
			$this->quickLinkTitle = 'view ' . strtolower($link->extendedProperties->quickLinkTitle);
			switch ($link->extendedProperties->quickLinkTitle)
			{
				case "Info":
					$this->quickLinkLogo = sprintf('%s/images/preview/gallery/quick-link-info.png?%s', Yii::app()->getBaseUrl(true), Yii::app()->params['version']);
					break;
				case "HTML5":
					$this->quickLinkLogo = sprintf('%s/images/preview/gallery/quick-link-html5.png?%s', Yii::app()->getBaseUrl(true), Yii::app()->params['version']);
					break;
				case "Link":
					$this->quickLinkLogo = sprintf('%s/images/preview/gallery/quick-link-link.png?%s', Yii::app()->getBaseUrl(true), Yii::app()->params['version']);
					break;
				case "Resources":
					$this->quickLinkLogo = sprintf('%s/images/preview/gallery/quick-link-resources.png?%s', Yii::app()->getBaseUrl(true), Yii::app()->params['version']);
					break;
			}

			$userId = UserIdentity::getCurrentUserId();
			$this->rateData = LinkRateRecord::getRateData($link->id, $userId);

			$this->totalViews = $link->getTotalViews();
		}

		/**
		 * @param $isQuickSite boolean
		 * @param $openFromBundle boolean
		 */
		public function applyLinkSettings($isQuickSite, $openFromBundle)
		{
			$this->config = new BasePreviewConfig();
			$this->config->init($this->link, $isQuickSite, $openFromBundle);
		}

		public function initDialogActions()
		{
			$this->dialogActions = array();
			$imageUrlPrefix = Yii::app()->getBaseUrl(true);
			if ($this->config->allowAddToQuickSite)
			{
				$action = new PreviewAction();
				$action->tag = 'quicksite';
				$action->text = 'Add to a QUICKSITE';
				$action->logo = sprintf('%s/images/preview/actions/quicksite.png?%s', $imageUrlPrefix, Yii::app()->params['version']);
				$this->dialogActions[] = $action;
			}

			if ($this->config->allowAddToFavorites)
			{
				$action = new PreviewAction();
				$action->tag = 'favorites';
				$action->text = 'Add to Favorites';
				$action->logo = sprintf('%s/images/preview/actions/favorites.png?%s', $imageUrlPrefix, Yii::app()->params['version']);
				$this->dialogActions[] = $action;
			}
		}

		public abstract function initContextActions();

		/**
		 * @return ParentLinkBundleInfo
		 */
		public function getLinkBundleInfo()
		{
			if (isset($this->linkBundleId))
				return ParentLinkBundleInfo::fromLinkId($this->linkBundleId);
			return null;
		}

		/**
		 * @param $link LibraryLink
		 * @param $parentBundleId boolean
		 * @param $isQuickSite boolean
		 * @param $isPhone boolean
		 * @return PreviewData
		 */
		public static function getInstance($link, $parentBundleId, $isQuickSite, $isPhone)
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
				case 'gif':
					$previewData = new ImagePreviewData($link);
					break;
				case 'url':
				case 'quicksite':
				case 'html5':
					$previewData = new UrlPreviewData($link);
					break;
				case 'youtube':
					$previewData = new YouTubePreviewData($link);
					break;
				case 'vimeo':
					$previewData = new VimeoPreviewData($link);
					break;
				case 'lan':
					$previewData = new LanPreviewData($link);
					break;
				case 'mp3':
					$previewData = new Mp3PreviewData($link);
					break;
				case 'xls':
					$previewData = new ExcelPreviewData($link, $isPhone);
					break;
				case 'app':
					$previewData = new AppLinkPreviewData($link);
					break;
				case 'internal library':
				case 'internal page':
				case 'internal window':
				case 'internal link':
				case 'internal shortcut':
					$previewData = new InternalLinkPreviewData($link, $isPhone);
					break;
				case 'folder':
					$previewData = new FolderPreviewData($link);
					break;
				case 'line break':
					$previewData = new LineBreakPreviewData($link);
					break;
				case 'link bundle':
					$previewData = new LinkBundlePreviewData($link);
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
			$previewData->linkBundleId = $parentBundleId;
			$previewData->applyLinkSettings($isQuickSite, isset($parentBundleId));
			$previewData->initDialogActions();
			$previewData->initContextActions();
			return $previewData;
		}
	}
<?
	use application\models\wallbin\models\web\LibraryLink as LibraryLink;

	/**
	 * Class FilePreviewData
	 */
	class FilePreviewData extends PreviewData
	{
		public $filePath;
		public $fileSize;
		public $fileExtension;

		/**
		 * @param $link LibraryLink
		 */
		public function __construct($link)
		{
			parent::__construct($link);

			$this->viewerFormat = 'file';
			$this->contentView = 'fileViewer';
			$this->linkTitle = 'File';

			$this->fileName = $link->fileName;
			$this->filePath = $link->filePath;
			$this->fileSize = FileInfo::formatFileSize($link->fileSize);
			$this->fileExtension = $link->fileExtension;

			switch ($this->format)
			{
				case 'ppt':
					$fileLogoSuffix = 'ppt';
					break;
				case 'xls':
					$fileLogoSuffix = 'excel';
					break;
				case 'doc':
					$fileLogoSuffix = 'doc';
					break;
				case 'pdf':
					$fileLogoSuffix = 'pdf';
					break;
				case 'image':
					$fileLogoSuffix = 'image';
					break;
				case 'video':
					$fileLogoSuffix = 'video';
					break;
				default:
					switch ($link->fileExtension)
					{
						case 'zip':
						case 'rar':
						case '7z':
							$fileLogoSuffix = 'archive';
							break;
						case 'ai':
						case 'psd':
							$fileLogoSuffix = 'image';
							break;
						default:
							$fileLogoSuffix = 'unknown';
							break;
					}
			}
			$this->fileLogo = sprintf('%s/images/preview/actions/file-logo-%s.png?%s', Yii::app()->getBaseUrl(true), $fileLogoSuffix, Yii::app()->params['version']);
		}

		/**
		 * @param $isQuickSite boolean
		 */
		public function applyLinkSettings($isQuickSite)
		{
			$this->config = new FilePreviewConfig();
			$this->config->init($this->link, $isQuickSite);
		}

		public function initDialogActions()
		{
			parent::initDialogActions();
			$this->dialogActions = CMap::mergeArray($this->getDownloadActions(), $this->dialogActions);
		}

		public function initContextActions()
		{
			$this->contextActions = array();

			if ($this->link->isDirectUrl && $this->config->isEOBrowser)
				return;

			$action = new PreviewAction();
			$action->tag = 'open';
			$action->url = $this->link->isDirectUrl ? $this->url : $action->url;
			$action->text = 'Open this Link';
			$this->contextActions[] = $action;

			if ($this->config->allowDownload)
			{
				$action = new PreviewAction();
				$action->tag = 'download';
				$action->text = 'Download this file';
				$this->contextActions[] = $action;
			}
			if ($this->config->allowAddToQuickSite)
			{
				$action = new PreviewAction();
				$action->tag = 'linkcart';
				$action->text = 'Add this file to my QuickSites Cart';
				$this->contextActions[] = $action;

				$action = new PreviewAction();
				$action->tag = 'linkcart-all-window';
				$action->text = 'Add all links in this window…';
				$this->contextActions[] = $action;

				$action = new PreviewAction();
				$action->tag = 'quicksite';
				$action->text = 'Email this Link';
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
				$action->text = 'Rate this Link';
				$this->contextActions[] = $action;
			}
		}

		/**
		 * @return PreviewAction[]
		 */
		protected function getDownloadActions()
		{
			$actions = array();

			if ($this->config->allowDownload)
			{
				$imageUrlPrefix = Yii::app()->getBaseUrl(true);

				$action = new PreviewAction();
				$action->tag = 'download';
				$action->text = 'DOWNLOAD this file...';
				$action->logo = sprintf('%s/images/preview/actions/download.png?%s', $imageUrlPrefix, Yii::app()->params['version']);
				$actions[] = $action;
			}

			return $actions;
		}
	}
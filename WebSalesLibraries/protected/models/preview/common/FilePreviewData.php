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

		public $dragUrl;
		public $oneDriveUrl;

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

			$fileInfo = FileInfo::fromLinkData($link->id, $link->type, $link->name, $link->fileRelativePath, $link->extendedProperties, $link->parent->parent->parent);
			if ($fileInfo->isFile)
				$this->dragUrl = \FileInfo::getFileMIME($link->originalFormat) . ':' .
					$fileInfo->dragDownloadName . ':' . $fileInfo->link;

			$this->oneDriveUrl = $link->oneDrive->url;
		}

		/**
		 * @param $isQuickSite boolean
		 * @param $openFromBundle boolean
		 */
		public function applyLinkSettings($isQuickSite, $openFromBundle)
		{
			$this->config = new FilePreviewConfig();
			$this->config->init($this->link, $isQuickSite, $openFromBundle);
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

			$action = new ContextMenuAction();
			$action->tag = 'open';
			$action->url = $this->link->isDirectUrl ? $this->url : $action->url;
			$action->text = 'Open this Link';
			$this->contextActions[] = $action;

			if (Yii::app()->params['one_drive_links']['enabled'] && !empty($this->oneDriveUrl))
			{
				$action = new ContextMenuAction();
				$action->tag = 'one-drive-open';
				$action->text = 'Open OneDrive Link';
				$this->contextActions[] = $action;
			}
			if ($this->config->allowDownload)
			{
				$action = new ContextMenuAction();
				$action->tag = 'download';
				$action->text = 'Download this file';
				$this->contextActions[] = $action;
			}
			if ($this->config->allowAddToQuickSite)
			{
				$action = new ContextMenuAction();
				$action->tag = 'linkcart';
				$action->text = 'Add this file to my QuickSites Cart';
				$action->beginGroup = true;
				$this->contextActions[] = $action;

				$action = new ContextMenuAction();
				$action->tag = 'linkcart-all-window';
				$action->text = 'Add all links in this window…';
				$action->onlyWallbinAction = true;
				$this->contextActions[] = $action;

				$action = new ContextMenuAction();
				$action->tag = 'quicksite';
				$action->text = 'Email this Link';
				$action->beginGroup = true;
				$this->contextActions[] = $action;
			}
			if (Yii::app()->params['one_drive_links']['enabled'] && !empty($this->oneDriveUrl))
			{
				$action = new ContextMenuAction();
				$action->tag = 'one-drive-email';
				$action->text = 'Email OneDrive Link';
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
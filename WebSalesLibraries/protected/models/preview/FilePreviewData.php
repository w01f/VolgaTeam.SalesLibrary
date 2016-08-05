<?
	use application\models\wallbin\models\web\LibraryLink as LibraryLink;

	/**
	 * Class FilePreviewData
	 */
	class FilePreviewData extends PreviewData
	{
		public $fileName;
		public $filePath;
		public $fileSize;

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
			$this->fileSize = self::formatFileSize($link->fileSize);
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
				switch ($this->format)
				{
					case 'ppt':
					case 'doc':
					case 'pdf':
					case 'xls':
					case 'mp3':
					case 'jpeg':
					case 'png':
					case 'key':
						$downloadSuffix = $this->format;
						break;
					case 'wmv':
					case 'mp4':
					case 'video':
						$downloadSuffix = 'mp4';
						break;
					default:
						$downloadSuffix = 'default';
						break;
				}

				$action = new PreviewAction();
				$action->tag = 'download';
				$action->text = 'DOWNLOAD this file to your Desktop or Mobile Device...';
				$action->shortText = 'DOWNLOAD this file...';
				$action->logo = sprintf('%s/images/preview/actions/download-%s.png?%s', $imageUrlPrefix, $downloadSuffix, Yii::app()->params['version']);
				$actions[] = $action;
			}

			return $actions;
		}

		/**
		 * @param $fileSize
		 * @return string
		 */
		protected static function formatFileSize($fileSize)
		{
			$type = '';
			if (isset($fileSize))
			{
				if ($fileSize < 524288000)
				{
					if ($fileSize < 512000)
						$type = 'kb';
					else
						$type = 'mb';
				}
				else
					$type = 'gb';
				switch ($type)
				{
					case "kb":
						$fileSize = $fileSize * .0009765625; // bytes to KB
						break;
					case "mb":
						$fileSize = ($fileSize * .0009765625) * .0009765625; // bytes to MB
						break;
					case "gb":
						$fileSize = (($fileSize * .0009765625) * .0009765625) * .0009765625; // bytes to GB
						break;
				}
			}
			else
				$fileSize = -1;
			if ($fileSize <= 0)
				return '';
			else
				return round($fileSize, 0) . $type;
		}
	}
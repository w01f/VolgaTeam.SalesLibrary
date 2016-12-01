<?
	use application\models\wallbin\models\web\LibraryManager as LibraryManager;
	use application\models\wallbin\models\web\LibraryLink as LibraryLink;

	/**
	 * Class InternalLinkPreviewData
	 */
	class InternalLinkPreviewData extends PreviewData
	{
		/** @var InternalWallbinPreviewInfo|InternalLibraryPagePreviewInfo|InternalLibraryFolderPreviewInfo|InternalLibraryObjectPreviewInfo */
		public $previewInfo;

		/**
		 * @param $link LibraryLink
		 */
		public function __construct($link)
		{
			parent::__construct($link);

			$this->viewerFormat = 'internal';
			$this->fileName = $link->fileName;

			/** @var  $linkSettings InternalWallbinLinkSettings|InternalLibraryPageLinkSettings|InternalLibraryFolderLinkSettings|InternalLibraryObjectLinkSettings|InternalShortcutLinkSettings */
			$linkSettings = $link->extendedProperties;
			switch ($linkSettings->internalLinkType)
			{
				case 1:
					/** @var  $linkSettings InternalWallbinLinkSettings */
					$this->contentView = 'internalWallbinViewer';
					$this->previewInfo = new InternalWallbinPreviewInfo($linkSettings);
					break;
				case 2:
					/** @var  $linkSettings InternalLibraryPageLinkSettings */
					$this->contentView = 'internalLibraryPageViewer';
					$this->previewInfo = new InternalLibraryPagePreviewInfo($linkSettings);
					break;
				case 3:
					/** @var  $linkSettings InternalLibraryFolderLinkSettings */
					$this->contentView = 'internalLibraryFolderViewer';
					$this->previewInfo = new InternalLibraryFolderPreviewInfo($linkSettings);
					break;
				case 4:
					/** @var  $linkSettings InternalLibraryObjectLinkSettings */
					$this->contentView = 'internalLibraryObjectViewer';
					$this->previewInfo = new InternalLibraryObjectPreviewInfo($linkSettings);
					break;
				case 5:
					/** @var  $linkSettings InternalShortcutLinkSettings */
					$this->contentView = 'internalShortcutViewer';
					$this->previewInfo = new InternalShortcutPreviewInfo($linkSettings);
					break;
			}
		}

		public function initDialogActions()
		{
			$this->dialogActions = array();
		}

		public function initContextActions()
		{
			$this->contextActions = array();

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
			if ($this->config->enableRating)
			{
				$action = new PreviewAction();
				$action->tag = 'rate';
				$action->text = 'Rate this URL Link';
				$this->contextActions[] = $action;
			}
		}
	}
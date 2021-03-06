<?
	use application\models\wallbin\models\web\LibraryLink as LibraryLink;

	/**
	 * Class AppLinkPreviewData
	 */
	class AppLinkPreviewData extends PreviewData
	{
		public $secondPath;

		/**
		 * @param $link LibraryLink
		 */
		public function __construct($link)
		{
			parent::__construct($link);

			$this->viewerFormat = 'app';
			$this->contentView = 'appLinkViewer';
			$this->fileName = $link->fileName;

			/** @var  $linkSettings AppLinkSettings*/
			$linkSettings = $link->extendedProperties;
			$this->secondPath = $linkSettings->secondPath;
		}

		public function initDialogActions()
		{
			$this->dialogActions = array();
		}

		public function initContextActions()
		{
			$this->contextActions = array();
			if ($this->config->allowDownload)
			{
				$action = new ContextMenuAction();
				$action->tag = 'zip-library-folder';
				$action->text = 'Download ALL in this window';
				$action->onlyWallbinAction = true;
				$this->contextActions[] = $action;
			}
		}
	}
<?
	use application\models\wallbin\models\web\LibraryLink as LibraryLink;

	/**
	 * Class FolderPreviewData
	 */
	class FolderPreviewData extends PreviewData
	{
		/**
		 * @param $link LibraryLink
		 */
		public function __construct($link)
		{
			parent::__construct($link);
			$this->fileName = $link->fileName;
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
				$action->tag = 'zip-folder';
				$action->text = 'Zip & Download';
				$this->contextActions[] = $action;

				$action = new ContextMenuAction();
				$action->tag = 'zip-library-folder';
				$action->text = 'Download ALL in this window';
				$this->contextActions[] = $action;
			}
			if ($this->config->allowAddToQuickSite)
			{
				$action = new ContextMenuAction();
				$action->tag = 'linkcart';
				$action->text = 'Add this Folder Link to my QuickSites Cart';
				$action->beginGroup = $this->config->allowDownload;
				$this->contextActions[] = $action;

				$action = new ContextMenuAction();
				$action->tag = 'linkcart-all-window';
				$action->text = 'Add all links in this window…';
				$this->contextActions[] = $action;

				$action = new ContextMenuAction();
				$action->tag = 'quicksite';
				$action->text = 'Email this Folder Link';
				$action->beginGroup = true;
				$this->contextActions[] = $action;
			}
		}
	}
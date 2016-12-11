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
				$action = new PreviewAction();
				$action->tag = 'zip-folder';
				$action->text = 'Zip & Download';
				$this->contextActions[] = $action;
			}
			if ($this->config->allowAddToQuickSite)
			{
				$action = new PreviewAction();
				$action->tag = 'linkcart';
				$action->text = 'Add this Folder Link to my QuickSites Cart';
				$this->contextActions[] = $action;

				$action = new PreviewAction();
				$action->tag = 'quicksite';
				$action->text = 'Email this Folder Link';
				$this->contextActions[] = $action;
			}
		}
	}
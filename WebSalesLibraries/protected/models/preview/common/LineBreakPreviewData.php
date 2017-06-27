<?
	use application\models\wallbin\models\web\LibraryLink as LibraryLink;

	/**
	 * Class LineBreakPreviewData
	 */
	class LineBreakPreviewData extends PreviewData
	{
		/**
		 * @param $link LibraryLink
		 */
		public function __construct($link)
		{
			parent::__construct($link);
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
				$action->text = 'Add this Line Break to my QuickSites Cart';
				$this->contextActions[] = $action;

				$action = new PreviewAction();
				$action->tag = 'linkcart-all-window';
				$action->text = 'Add all links in this window…';
				$this->contextActions[] = $action;
			}
			if ($this->config->allowDownload)
			{
				$action = new PreviewAction();
				$action->tag = 'zip-library-folder';
				$action->text = 'Download ALL in this window';
				$this->contextActions[] = $action;
			}
		}
	}
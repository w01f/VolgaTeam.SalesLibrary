<?
	use application\models\wallbin\models\web\LibraryLink as LibraryLink;

	/**
	 * Class LinkBundlePreviewData
	 */
	class LinkBundlePreviewData extends PreviewData
	{
		/** @var LinkBundlePreviewBaseItem[] */
		public $bundleItems;

		public $defaultItemId;

		/**
		 * @param $link LibraryLink
		 */
		public function __construct($link)
		{
			parent::__construct($link);

			$this->linkTitle = 'Link Bundle';
			$this->viewerFormat = 'link bundle';
			$this->contentView = 'linkBundleViewer';

			/** @var  $linkSettings LinkBundleLinkSettings */
			$linkSettings = $link->extendedProperties;

			$this->bundleItems = array();
			$hasDefault = false;
			foreach ($linkSettings->bundleItems as $bundleItem)
			{
				$previewItem = LinkBundlePreviewBaseItem::Create($bundleItem);
				if (!$hasDefault && $previewItem->itemType != 'url')
				{
					$this->defaultItemId = $previewItem->id;
					$hasDefault = true;
				}
				$this->bundleItems[] = $previewItem;
			}
		}

		public function initDialogActions()
		{
			$this->dialogActions = array();
		}

		public function initContextActions()
		{
			$this->contextActions = array();

			$action = new PreviewAction();
			$action->tag = 'open';
			$action->text = 'Open this Link Bundle';
			$this->contextActions[] = $action;

			if ($this->config->allowAddToQuickSite)
			{
				$action = new PreviewAction();
				$action->tag = 'linkcart';
				$action->text = 'Add this Link Bundle to my QuickSites Cart';
				$this->contextActions[] = $action;

				$action = new PreviewAction();
				$action->tag = 'quicksite';
				$action->text = 'Email this Link Bundle';
				$this->contextActions[] = $action;
			}
		}
	}
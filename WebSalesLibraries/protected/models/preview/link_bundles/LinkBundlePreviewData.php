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
		 * @param $isPhone boolean
		 */
		public function __construct($link, $isPhone)
		{
			parent::__construct($link);

			if ($isPhone)
				$this->linkTitle = $link->name;
			else
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

			if ($this->config->allowDownload)
			{
				$action = new PreviewAction();
				$action->tag = 'zip-link-bundle';
				$action->text = 'Zip & Download all files from Link Bundle';
				$this->contextActions[] = $action;

				$action = new PreviewAction();
				$action->tag = 'zip-library-folder';
				$action->text = 'Download ALL in this window';
				$this->contextActions[] = $action;
			}
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

		/**
		 * @return ParentLinkBundleInfo
		 */
		public function getLinkBundleInfo()
		{
			return ParentLinkBundleInfo::fromLinkId($this->linkId);
		}
	}
<?

	/**
	 * Class LinkBundlePreviewUrlItem
	 */
	class LinkBundlePreviewUrlItem extends LinkBundlePreviewBaseItem
	{
		public $url;

		/**
		 * @param $bundleItem BaseLinkBundleItem
		 */
		public function __construct($bundleItem)
		{
			parent::__construct($bundleItem);
			$this->itemType = 'url';
			$this->hasContent = false;
			$this->contentView = 'linkBundleUrlContent';
		}

		/**
		 * @param $bundleItem LibraryLinkBundleItem
		 * @param $url string
		 * @return LinkBundlePreviewUrlItem
		 */
		public static function fromLibraryLinkItem($bundleItem, $url)
		{
			$bundlePreviewItem = new LinkBundlePreviewUrlItem($bundleItem);
			$bundlePreviewItem->url = $url;
			return $bundlePreviewItem;
		}

		/**
		 * @param $bundleItem UrlLinkBundleItem
		 * @return LinkBundlePreviewUrlItem
		 */
		public static function fromUrlLinkBundleItem($bundleItem)
		{
			$bundlePreviewItem = new LinkBundlePreviewUrlItem($bundleItem);
			$bundlePreviewItem->url = $bundleItem->url;
			return $bundlePreviewItem;
		}
	}
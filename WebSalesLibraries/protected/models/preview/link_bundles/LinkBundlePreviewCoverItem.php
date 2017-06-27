<?
	/**
	 * Class LinkBundlePreviewCoverItem
	 */
	class LinkBundlePreviewCoverItem extends LinkBundlePreviewContentItem
	{
		public $logo;

		/**
		 * @param $bundleItem LinkBundleCoverItem
		 */
		public function __construct($bundleItem)
		{
			parent::__construct($bundleItem);
			$this->itemType = 'cover';
			$this->contentView = 'linkBundleCoverContent';
			$this->logo = $bundleItem->logo;
		}
	}
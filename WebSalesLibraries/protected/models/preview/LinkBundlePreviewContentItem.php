<?
	/**
	 * Class LinkBundlePreviewContentItem
	 */
	abstract class LinkBundlePreviewContentItem extends LinkBundlePreviewBaseItem
	{
		/**
		 * @param $bundleItem LinkBundleInfoItem |LinkBundleStrategyItem | LinkBundleRevenueItem
		 */
		public function __construct($bundleItem)
		{
			parent::__construct($bundleItem);
			$this->itemType = 'content';
			$this->hasContent = true;
		}
	}
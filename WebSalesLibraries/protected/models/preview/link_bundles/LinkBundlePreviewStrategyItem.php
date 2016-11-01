<?
	/**
	 * Class LinkBundlePreviewStrategyItem
	 */
	class LinkBundlePreviewStrategyItem extends LinkBundlePreviewContentItem
	{
		public $header;
		public $body;

		/**
		 * @param $bundleItem LinkBundleStrategyItem
		 */
		public function __construct($bundleItem)
		{
			parent::__construct($bundleItem);
			$this->contentView = 'linkBundleStrategyContent';
			$this->header = $bundleItem->header;
			$this->body = $bundleItem->body;
		}
	}
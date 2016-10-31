<?
	/**
	 * Class LinkBundlePreviewInfoItem
	 */
	class LinkBundlePreviewInfoItem extends LinkBundlePreviewContentItem
	{
		public $header;
		public $body;

		/**
		 * @param $bundleItem LinkBundleInfoItem
		 */
		public function __construct($bundleItem)
		{
			parent::__construct($bundleItem);
			$this->contentView = 'linkBundleInfoContent';
			$this->header = $bundleItem->header;
			$this->body = $bundleItem->body;
		}
	}
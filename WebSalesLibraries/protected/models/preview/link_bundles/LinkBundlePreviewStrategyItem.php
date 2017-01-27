<?
	/**
	 * Class LinkBundlePreviewStrategyItem
	 */
	class LinkBundlePreviewStrategyItem extends LinkBundlePreviewContentItem
	{
		public $header;
		public $body;
		public $foreColor;
		public $backColor;
		/**
		 * @var \Font
		 */
		public $font;

		/**
		 * @param $bundleItem LinkBundleStrategyItem
		 */
		public function __construct($bundleItem)
		{
			parent::__construct($bundleItem);
			$this->contentView = 'linkBundleStrategyContent';
			$this->header = $bundleItem->header;
			$this->body = $bundleItem->body;
			$this->foreColor = $bundleItem->foreColor;
			$this->backColor = $bundleItem->backColor;
			$this->font = $bundleItem->font;
		}
	}
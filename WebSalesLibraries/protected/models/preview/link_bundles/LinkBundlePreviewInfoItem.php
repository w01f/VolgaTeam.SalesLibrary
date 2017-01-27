<?
	/**
	 * Class LinkBundlePreviewInfoItem
	 */
	class LinkBundlePreviewInfoItem extends LinkBundlePreviewContentItem
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
		 * @param $bundleItem LinkBundleInfoItem
		 */
		public function __construct($bundleItem)
		{
			parent::__construct($bundleItem);
			$this->contentView = 'linkBundleInfoContent';
			$this->header = $bundleItem->header;
			$this->body = $bundleItem->body;

			$this->foreColor = $bundleItem->foreColor;
			$this->backColor = $bundleItem->backColor;
			$this->font = $bundleItem->font;
		}
	}
<?
	/**
	 * Class LinkBundlePreviewLaunchScreenItem
	 */
	class LinkBundlePreviewLaunchScreenItem extends LinkBundlePreviewContentItem
	{
		public $header;
		public $footer;
		public $logo;
		public $banner;
		public $headerForeColor;
		public $headerBackColor;
		/**
		 * @var \Font
		 */
		public $headerFont;
		public $footerForeColor;
		public $footerBackColor;
		/**
		 * @var \Font
		 */
		public $footerFont;

		/**
		 * @param $bundleItem LinkBundleLaunchScreenItem
		 */
		public function __construct($bundleItem)
		{
			parent::__construct($bundleItem);

			$this->contentView = 'linkBundleLaunchScreenContent';
			$this->header = $bundleItem->header;
			$this->footer = $bundleItem->footer;

			$this->logo = $bundleItem->logo;
			$this->banner = $bundleItem->banner;

			$this->headerForeColor = $bundleItem->headerForeColor;
			$this->headerBackColor = $bundleItem->headerBackColor;
			$this->headerFont = $bundleItem->headerFont;

			$this->footerForeColor = $bundleItem->footerForeColor;
			$this->footerBackColor = $bundleItem->footerBackColor;
			$this->footerFont = $bundleItem->footerFont;
		}
	}
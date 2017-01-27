<?
	/**
	 * Class LinkBundleRevenueItem
	 */
	class LinkBundleRevenueItem extends BaseLinkBundleItem
	{
		/**
		 * @var int
		 * @soap
		 */
		public $revenueType;
		/**
		 * @var string
		 * @soap
		 */
		public $additionalInfo;
		/**
		 * @var LinkBundleRevenueInfoItem[]
		 * @soap
		 */
		public $infoItems;
		/**
		 * @var string
		 * @soap
		 */
		public $foreColor;
		/**
		 * @var string
		 * @soap
		 */
		public $backColor;
		/**
		 * @var Font
		 * @soap
		 */
		public $font;
	}
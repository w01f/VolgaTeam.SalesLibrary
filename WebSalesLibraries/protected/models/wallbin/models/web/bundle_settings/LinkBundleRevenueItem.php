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
	}
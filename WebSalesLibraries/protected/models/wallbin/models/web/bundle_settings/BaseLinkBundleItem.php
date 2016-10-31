<?
	/**
	 * Class BaseLinkBundleItem
	 */
	abstract class BaseLinkBundleItem
	{
		/**
		 * @var string
		 * @soap
		 */
		public $id;
		/**
		 * @var int
		 * @soap
		 */
		public $itemType;
		/**
		 * @var int
		 * @soap
		 */
		public $collectionOrder;
		/**
		 * @var string
		 * @soap
		 */
		public $image;
		/**
		 * @var string
		 * @soap
		 */
		public $title;
		/**
		 * @var string
		 * @soap
		 */
		public $hoverTip;
	}
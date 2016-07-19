<?
	/**
	 * Class SoapLibraryFolder
	 */
	class SoapLibraryFolder
	{
		/**
		 * @var string
		 * @soap
		 */
		public $id;
		/**
		 * @var string
		 * @soap
		 */
		public $pageId;
		/**
		 * @var string
		 * @soap
		 */
		public $libraryId;
		/**
		 * @var string
		 * @soap
		 */
		public $name;
		/**
		 * @var int
		 * @soap
		 */
		public $rowOrder;
		/**
		 * @var int
		 * @soap
		 */
		public $columnOrder;
		/**
		 * @var string
		 * @soap
		 */
		public $borderColor;
		/**
		 * @var string
		 * @soap
		 */
		public $windowBackColor;
		/**
		 * @var string
		 * @soap
		 */
		public $windowForeColor;
		/**
		 * @var string
		 * @soap
		 */
		public $headerBackColor;
		/**
		 * @var string
		 * @soap
		 */
		public $headerForeColor;
		/**
		 * @var Font
		 * @soap
		 */
		public $windowFont;
		/**
		 * @var Font
		 * @soap
		 */
		public $headerFont;
		/**
		 * @var string
		 * @soap
		 */
		public $headerAlignment;
		/**
		 * @var boolean
		 * @soap
		 */
		public $enableWidget;
		/**
		 * @var string
		 * @soap
		 */
		public $widget;
		/**
		 * @var SoapBanner
		 * @soap
		 */
		public $banner;
		/**
		 * @var SoapLibraryLink[]
		 * @soap
		 */
		public $files;
		/**
		 * @var string
		 * @soap
		 */
		public $dateAdd;
		/**
		 * @var string
		 * @soap
		 */
		public $dateModify;
	}

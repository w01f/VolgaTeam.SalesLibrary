<?
	/**
	 * Class SoapLibraryPage
	 */
	class SoapLibraryPage
	{
		/**
		 * @var string name
		 * @soap
		 */
		public $id;
		/**
		 * @var string name
		 * @soap
		 */
		public $libraryId;
		/**
		 * @var string name
		 * @soap
		 */
		public $name;
		/**
		 * @var string libraryName
		 * @soap
		 */
		public $libraryName;
		/**
		 * @var int order
		 * @soap
		 */
		public $order;
		/**
		 * @var SoapLibraryPageSettings settings
		 * @soap
		 */
		public $settings;
		/**
		 * @var SoapLibraryFolder[]
		 * @soap
		 */
		public $folders;
		/**
		 * @var boolean enableColumns
		 * @soap
		 */
		public $enableColumns;
		/**
		 * @var SoapColumn[]
		 * @soap
		 */
		public $columns;
		/**
		 * @var string
		 * @soap
		 */
		public $dateModify;
		/**
		 * @var GroupViewModel[]
		 * @soap
		 */
		public $groups;
		/**
		 * @var UserViewModel[]
		 * @soap
		 */
		public $users;
	}

<?
	/**
	 * Class SoapLibrary
	 */
	class SoapLibrary
	{
		/**
		 * @var string id
		 * @soap
		 */
		public $id;
		/**
		 * @var string name
		 * @soap
		 */
		public $name;
		/**
		 * @var SoapLibraryPage[]
		 * @soap
		 */
		public $pages;
		/**
		 * @var SoapAutoWidget[]
		 * @soap
		 */
		public $autoWidgets;
		/**
		 * @var SoapUniversalPreviewContainer[]
		 * @soap
		 */
		public $previewContainers;
		/**
		 * @var LibraryConfig
		 * @soap
		 */
		public $config;
	}

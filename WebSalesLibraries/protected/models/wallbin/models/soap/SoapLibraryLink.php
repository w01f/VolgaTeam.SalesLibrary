<?

	/**
	 * Class SoapLibraryLink
	 */
	class SoapLibraryLink
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
		public $parentLinkId;
		/**
		 * @var string
		 * @soap
		 */
		public $folderId;
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
		 * @var string
		 * @soap
		 */
		public $fileRelativePath;
		/**
		 * @var string
		 * @soap
		 */
		public $fileName;
		/**
		 * @var string
		 * @soap
		 */
		public $fileExtension;
		/**
		 * @var string
		 * @soap
		 */
		public $fileDate;
		/**
		 * @var int
		 * @soap
		 */
		public $fileSize;
		/**
		 * @var string
		 * @soap
		 */
		public $originalFormat;
		/**
		 * @var string
		 * @soap
		 */
		public $searchFormat;
		/**
		 * @var int
		 * @soap
		 */
		public $order;
		/**
		 * @var int
		 * @soap
		 */
		public $type;
		/**
		 * @var BaseLinkSettings|VideoLinkSettings|HyperLinkSettings|PowerPointLinkSettings|AppLinkSettings|InternalLinkPreviewData|QPageLinkSettings
		 * @soap
		 */
		public $extendedProperties;
		/**
		 * @var LineBreak
		 * @soap
		 */
		public $lineBreakProperties;
		/**
		 * @var int
		 * @soap
		 */
		public $widgetType;
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
		 * @var SoapThumbnail
		 * @soap
		 */
		public $thumbnail;
		/**
		 * @var string
		 * @soap
		 */
		public $previewId;
		/**
		 * @var LinkSuperFilter[]
		 * @soap
		 */
		public $superFilters;
		/**
		 * @var LinkCategory[]
		 * @soap
		 */
		public $categories;
		/**
		 * @var string
		 * @soap
		 */
		public $tags;
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
		/**
		 * @var string
		 * @soap
		 */
		public $contentPath;
		public $fileLink;
		public $filePath;
		public $universalPreview;
		public $browser;
		public $isFavorite;
		public $folderContent;
		public $tooltip;
		public $isFolder;
		public $isLineBreak;
		public $isDirectUrl;
		public $isAppLink;
	}
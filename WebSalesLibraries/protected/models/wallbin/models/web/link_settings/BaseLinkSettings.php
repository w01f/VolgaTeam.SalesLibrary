<?

	/**
	 * Class BaseLinkSettings
	 */
	class BaseLinkSettings
	{
		/**
		 * @var string
		 * @soap
		 */
		public $note;
		/**
		 * @var string
		 * @soap
		 */
		public $hoverNote;
		/**
		 * @var boolean
		 * @soap
		 */
		public $showOnlyCustomHoverNote;
		/**
		 * @var boolean
		 * @soap
		 */
		public $isBold;
		/**
		 * @var boolean
		 * @soap
		 */
		public $isItalic;
		/**
		 * @var boolean
		 * @soap
		 */
		public $isUnderline;
		/**
		 * @var boolean
		 * @soap
		 */
		public $isSpecialFormat;
		/**
		 * @var Font
		 * @soap
		 */
		public $font;
		/**
		 * @var string
		 * @soap
		 */
		public $foreColor;
		/**
		 * @var boolean
		 * @soap
		 */
		public $isTextWordWrap;
		/**
		 * @var boolean
		 * @soap
		 */
		public $isRestricted;
		/**
		 * @var boolean
		 * @soap
		 */
		public $noShare;
		/**
		 * @var string
		 * @soap
		 */
		public $assignedUsers;
		/**
		 * @var string
		 * @soap
		 */
		public $deniedUsers;
		/**
		 * @var string
		 * @soap
		 */
		public $quickLinkUrl;
		/**
		 * @var string
		 * @soap
		 */
		public $quickLinkTitle;

		/**
		 * @param $linkRecord LinkRecord
		 * @return BaseLinkSettings
		 */
		public static function createByLink($linkRecord)
		{
			return self::createByContent($linkRecord->settings);
		}

		/**
		 * @param $content string
		 * @return BaseLinkSettings
		 */
		public static function createByContent($content)
		{
			return CJSON::decode($content, false);
		}
	}
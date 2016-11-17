<?
	/**
	 * Class GroupBookmarkShortcut
	 */
	class GroupBookmarkShortcut extends CustomHandledShortcut
	{
		public $bookmarkId;

		/**
		 * @param $linkRecord
		 * @param $isPhone boolean
		 */
		public function __construct($linkRecord, $isPhone)
		{
			parent::__construct($linkRecord, $isPhone);

			$this->enabled = !$this->isPhone;

			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($linkRecord->config);

			$this->bookmarkId = trim($linkConfig->getElementsByTagName("BookmarkGroupID")->item(0)->nodeValue);
		}

		/**
		 * @return string
		 */
		public function getTypeForActivityTracker()
		{
			return 'Group Bookmark';
		}

		/**
		 * @return string
		 */
		public function getMenuItemData()
		{
			$result = parent::getMenuItemData();
			$result .= '<div class="bookmark-id">' . $this->bookmarkId . '</div>';
			return $result;
		}

		/**
		 * @return string
		 */
		public function getSourceLink()
		{
			return '#';
		}
	}
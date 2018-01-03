<?
	/**
	 * Class LibraryLinkShortcut
	 */
	class LibraryLinkShortcut extends CustomHandledShortcut
	{
		public $linkId;

		/**
		 * @param $linkRecord
		 * @param $isPhone boolean
		 */
		public function __construct($linkRecord, $isPhone)
		{
			parent::__construct($linkRecord, $isPhone);
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($linkRecord->config);

			$libraryName = str_replace("'", "''", trim($linkConfig->getElementsByTagName("Library")->item(0)->nodeValue));
			$pageName = str_replace("'", "''", trim($linkConfig->getElementsByTagName("Page")->item(0)->nodeValue));
			$windowName = str_replace("'", "''", trim($linkConfig->getElementsByTagName("Window")->item(0)->nodeValue));
			$fileName = str_replace("'", "''", trim($linkConfig->getElementsByTagName("File")->item(0)->nodeValue));
			if (isset($libraryName) && isset($pageName) && isset($windowName) && isset($fileName))
			{
				$linkRecord = LinkRecord::getLinkByName($libraryName, $pageName, $windowName, $fileName);
				if (isset($linkRecord))
					$this->linkId = $linkRecord->id;
			}
		}

		/**
		 * @return string
		 */
		public function getMenuItemData()
		{
			$result = parent::getMenuItemData();
			$result .= '<div class="library-link-id">' . $this->linkId . '</div>';
			$result .= '<div class="link-header">' . ($this->headerTitle != '' ? $this->headerTitle : $this->title) . '</div>';
			return $result;
		}

		/**
		 * @return string
		 */
		public function getTypeForActivityTracker()
		{
			return 'Libraryfile';
		}

		/**
		 * @return string
		 */
		public function getSourceLink()
		{
			return '#';
		}
	}
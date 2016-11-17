<?

	/**
	 * Class SuperGroupShortcut
	 */
	class SuperGroupShortcut extends CustomHandledShortcut
	{
		public $superGroupTag;

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

			$superGroupTags = $linkConfig->getElementsByTagName("SuperGroupTag");
			$this->superGroupTag = $superGroupTags->length > 0 ? trim($superGroupTags->item(0)->nodeValue) : null;
		}

		/**
		 * @return string
		 */
		public function getTypeForActivityTracker()
		{
			return 'SuperGroup Toggle';
		}

		/**
		 * @return string
		 */
		public function getMenuItemData()
		{
			$result = parent::getMenuItemData();
			if (isset($this->superGroupTag))
				$result .= '<div class="super-group-tag">' . $this->superGroupTag . '</div>';
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
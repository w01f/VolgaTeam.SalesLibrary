<?

	/**
	 * Class YouTubeShortcut
	 */
	class YouTubeShortcut extends CustomHandledShortcut
	{
		public $sourceLink;
		public $youTubeId;

		/**
		 * @param $linkRecord
		 * @param $isPhone boolean
		 */
		public function __construct($linkRecord, $isPhone)
		{
			parent::__construct($linkRecord, $isPhone);
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($linkRecord->config);

			$this->sourceLink = trim($linkConfig->getElementsByTagName("Source")->item(0)->nodeValue);
			if (preg_match('%(?:youtube(?:-nocookie)?\.com/(?:[^/]+/.+/|(?:v|e(?:mbed)?)/|.*[?&]v=)|youtu\.be/)([^"&?/ ]{11})%i', $this->sourceLink, $match))
			{
				$this->youTubeId = $match[1];
			}
		}

		/**
		 * @return string
		 */
		public function getMenuItemData()
		{
			$result = parent::getMenuItemData();
			$result .= '<div class="youtube-id">' . $this->youTubeId . '</div>';
			$result .= '<div class="youtube-title">' . $this->title . '</div>';
			return $result;
		}

		/**
		 * @return array
		 */
		public function getActivityData()
		{
			$activityData = parent::getActivityData();
			$activityData['details']['URL'] = $this->sourceLink;
			return $activityData;
		}

		/**
		 * @return string
		 */
		public function getTypeForActivityTracker()
		{
			return 'YouTube';
		}

		public function getSourceLink()
		{
			return $this->sourceLink;
		}
	}
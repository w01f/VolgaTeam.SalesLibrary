<?

	/**
	 * Class UrlShortcut
	 */
	class UrlShortcut extends BaseShortcut
	{
		public $sourceLink;

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
			return 'URL';
		}

		public function getSourceLink()
		{
			return $this->sourceLink;
		}
	}
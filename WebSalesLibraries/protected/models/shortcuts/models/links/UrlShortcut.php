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
		 * @return string
		 */
		public function getTypeForActivityTracker()
		{
			return 'Shortcut Web Link';
		}

		public function getSourceLink()
		{
			return $this->sourceLink;
		}
	}
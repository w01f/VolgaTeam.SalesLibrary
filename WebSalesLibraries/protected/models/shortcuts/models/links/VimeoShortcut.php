<?

	/**
	 * Class VimeoShortcut
	 */
	class VimeoShortcut extends CustomHandledShortcut
	{
		public $sourceLink;
		public $playerUrl;

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
			if (preg_match('/https?:\/\/(?:www\.|player\.)?vimeo.com\/(?:channels\/(?:\w+\/)?|groups\/([^\/]*)\/videos\/|album\/(\d+)\/video\/|video\/|(\w*\/)*review\/|)(\d+)(?:$|\/|\?)/', $this->sourceLink, $match))
				$this->playerUrl = 'https://player.vimeo.com/video/'.$match[4];
			else
				$this->playerUrl = $this->sourceLink;
		}

		/**
		 * @return string
		 */
		public function getMenuItemData()
		{
			$result = parent::getMenuItemData();
			$result .= '<div class="player-url">' . $this->playerUrl . '</div>';
			$result .= '<div class="player-title">' . $this->title . '</div>';
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
			return 'Vimeo';
		}

		public function getSourceLink()
		{
			return $this->isPhone ?
				Yii::app()->createAbsoluteUrl('shortcuts/getSamePage') :
				$this->sourceLink;
		}
	}
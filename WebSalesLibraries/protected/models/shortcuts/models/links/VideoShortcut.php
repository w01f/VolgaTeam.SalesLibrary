<?php

	/**
	 * Class VideoShortcut
	 */
	class VideoShortcut extends PageContentShortcut
	{
		public $sourceLink;
		public $playerLink;

		/**
		 * @param $linkRecord
		 * @param $isPhone boolean
		 */
		public function __construct($linkRecord, $isPhone)
		{
			parent::__construct($linkRecord, $isPhone);
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($linkRecord->config);

			$baseUrl = Yii::app()->getBaseUrl(true);
			$this->sourceLink = str_replace('&', '%26', str_replace(' ', '%20', $baseUrl . $this->relativeLink . '/' . trim($linkConfig->getElementsByTagName("Source")->item(0)->nodeValue)));
			$this->playerLink = $baseUrl . '/vendor/video-js/video-js.swf';
		}

		/**
		 * @return string
		 */
		public function getMenuItemData()
		{
			$result = parent::getMenuItemData();
			$result .= '<div class="file-type">mp4</div>';
			$result .= '<div class="view-type">mp4</div>';
			$result .= '<div class="links">'
				. CJSON::encode(array(array(
					'src' => $this->getSourceLink(),
					'href' => $this->getSourceLink(),
					'title' => $this->title,
					'type' => 'video/mp4',
					'swf' => $this->playerLink)))
				. '</div>';
			return $result;
		}

		/**
		 * @return string
		 */
		public function getTypeForActivityTracker()
		{
			return 'Video';
		}

		/**
		 * @return string
		 */
		public function getSourceLink()
		{
			return $this->isPhone ? parent::getSourceLink() : $this->sourceLink;
		}
	}
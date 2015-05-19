<?php

	/**
	 * Class VideoShortcut
	 */
	class VideoShortcut extends BaseShortcut
	{
		public $playerLink;

		/**
		 * @param $linkRecord
		 */
		public function __construct($linkRecord)
		{
			parent::__construct($linkRecord);
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($linkRecord->config);

			$this->viewPath = Yii::app()->browser->isMobile() ? 'directLink' : 'videoLink';

			$baseUrl = Yii::app()->getBaseUrl(true);
			$this->sourceLink = str_replace('&', '%26', str_replace(' ', '%20', $baseUrl . $linkRecord->source_path . '/' . trim($linkConfig->getElementsByTagName("Source")->item(0)->nodeValue)));
			$this->playerLink = $baseUrl . '/vendor/video-js/video-js.swf';
		}

		/**
		 * @return string
		 */
		public function getServiceData()
		{
			$result = '';
			$result .= '<div class="file-type">mp4</div>';
			$result .= '<div class="view-type">mp4</div>';
			$result .= '<div class="links">'
				. CJSON::encode(array(array(
					'src' => $this->sourceLink,
					'href' => $this->sourceLink,
					'title' => $this->name,
					'type' => 'video/mp4',
					'swf' => $this->playerLink)))
				. '</div>';
			$result .= '<div class="activity-data">' . CJSON::encode(array(
					'action' => 'Shortcut Video Link',
					'title' => sprintf('%s - %s', $this->name, $this->tooltip)
				)) . '</div>';
			return $result;
		}
	}
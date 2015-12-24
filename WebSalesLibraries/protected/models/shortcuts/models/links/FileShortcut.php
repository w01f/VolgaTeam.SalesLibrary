<?php

	/**
	 * Class FileShortcut
	 */
	class FileShortcut extends BaseShortcut
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

			$baseUrl = Yii::app()->getBaseUrl(true);
			$this->sourceLink = str_replace('&', '%26', str_replace(' ', '%20', $baseUrl . $this->relativeLink . '/' . trim($linkConfig->getElementsByTagName("Source")->item(0)->nodeValue)));
		}

		/**
		 * @return string
		 */
		public function getSourceLink()
		{
			return $this->sourceLink;
		}

		/**
		 * @return string
		 */
		public function getTypeForActivityTracker()
		{
			return 'File';
		}
	}
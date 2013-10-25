<?php
	class FileShortcut
	{
		public $type;
		public $imagePath;
		public $sourceLink;

		public function __construct($linkRecord)
		{
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($linkRecord->config);
			$this->type = trim($linkConfig->getElementsByTagName("Type")->item(0)->nodeValue);
			$baseUrl = Yii::app()->getBaseUrl(true);
			$this->imagePath = $baseUrl . $linkRecord->image_path . '?' . $linkRecord->id;
			$this->sourceLink = str_replace('&', '%26', str_replace(' ', '%20', $baseUrl . $linkRecord->source_path . '/' . trim($linkConfig->getElementsByTagName("Source")->item(0)->nodeValue)));
		}
	}
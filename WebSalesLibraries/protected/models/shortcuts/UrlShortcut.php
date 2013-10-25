<?php
	class UrlShortcut
	{
		public $type;
		public $imagePath;
		public $sourceLink;

		public function __construct($linkRecord)
		{
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($linkRecord->config);
			$this->type = trim($linkConfig->getElementsByTagName("Type")->item(0)->nodeValue);
			$this->imagePath = Yii::app()->getBaseUrl(true) . $linkRecord->image_path . '?' . $linkRecord->id;
			$this->sourceLink = trim($linkConfig->getElementsByTagName("Source")->item(0)->nodeValue);
		}
	}
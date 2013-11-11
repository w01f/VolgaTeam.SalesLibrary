<?php
	class VideoShortcut
	{
		public $type;
		public $tooltip;
		public $name;
		public $imagePath;
		public $sourceLink;
		public $playerLink;

		public function __construct($linkRecord)
		{
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($linkRecord->config);
			$this->type = trim($linkConfig->getElementsByTagName("Type")->item(0)->nodeValue);
			$this->name = trim($linkConfig->getElementsByTagName("Source")->item(0)->nodeValue);
			$tooltipTags = $linkConfig->getElementsByTagName("ToolTip");
			$this->tooltip = $tooltipTags->length > 0 ? trim($tooltipTags->item(0)->nodeValue) : '';
			$baseUrl = Yii::app()->getBaseUrl(true);
			$this->imagePath = $baseUrl . $linkRecord->image_path . '?' . $linkRecord->id;
			$this->sourceLink = str_replace('&', '%26', str_replace(' ', '%20', $baseUrl . $linkRecord->source_path . '/' . $this->name));
			$this->playerLink = $baseUrl . '/vendor/video-js/video-js.swf';
		}
	}
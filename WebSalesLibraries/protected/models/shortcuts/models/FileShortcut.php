<?php

	/**
	 * Class FileShortcut
	 */
	class FileShortcut
	{
		public $id;
		public $type;
		public $name;
		public $tooltip;
		public $imagePath;
		public $sourceLink;

		/**
		 * @param $linkRecord
		 */
		public function __construct($linkRecord)
		{
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($linkRecord->config);
			$this->id = $linkRecord->id;
			$this->type = trim($linkConfig->getElementsByTagName("Type")->item(0)->nodeValue);
			$nameTags = $linkConfig->getElementsByTagName("line1");
			$this->name = $nameTags->length > 0 ? trim($nameTags->item(0)->nodeValue) : '';
			$tooltipTags = $linkConfig->getElementsByTagName("line2");
			$this->tooltip = $tooltipTags->length > 0 ? trim($tooltipTags->item(0)->nodeValue) : '';
			$baseUrl = Yii::app()->getBaseUrl(true);
			$this->imagePath = $baseUrl . $linkRecord->image_path . '?' . $linkRecord->id;
			$this->sourceLink = str_replace('&', '%26', str_replace(' ', '%20', $baseUrl . $linkRecord->source_path . '/' . trim($linkConfig->getElementsByTagName("Source")->item(0)->nodeValue)));
		}
	}
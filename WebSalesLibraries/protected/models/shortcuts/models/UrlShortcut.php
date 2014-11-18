<?php

	/**
	 * Class UrlShortcut
	 */
	class UrlShortcut
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
			$this->id = $linkRecord->id;
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($linkRecord->config);
			$this->type = trim($linkConfig->getElementsByTagName("Type")->item(0)->nodeValue);
			$nameTags = $linkConfig->getElementsByTagName("line1");
			$this->name = $nameTags->length > 0 ? trim($nameTags->item(0)->nodeValue) : '';
			$tooltipTags = $linkConfig->getElementsByTagName("line2");
			$this->tooltip = $tooltipTags->length > 0 ? trim($tooltipTags->item(0)->nodeValue) : '';
			$this->imagePath = Yii::app()->getBaseUrl(true) . $linkRecord->image_path . '?' . $linkRecord->id_page . $linkRecord->id;
			$this->sourceLink = trim($linkConfig->getElementsByTagName("Source")->item(0)->nodeValue);
		}
	}
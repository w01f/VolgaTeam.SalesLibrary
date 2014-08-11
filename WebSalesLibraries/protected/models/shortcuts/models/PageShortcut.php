<?php

	/**
	 * Class PageShortcut
	 */
	class PageShortcut
	{
		public $id;
		public $type;
		public $name;
		public $tooltip;
		public $imagePath;
		public $libraryName;
		public $pageName;

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
			$baseUrl = Yii::app()->getBaseUrl(true);
			$this->imagePath = $baseUrl . $linkRecord->image_path . '?' . $linkRecord->id;
			$this->libraryName = trim($linkConfig->getElementsByTagName("Library")->item(0)->nodeValue);
			$this->pageName = trim($linkConfig->getElementsByTagName("Page")->item(0)->nodeValue);
		}
	}
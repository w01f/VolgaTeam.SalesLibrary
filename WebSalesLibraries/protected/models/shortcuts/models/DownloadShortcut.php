<?php

	/**
	 * Class DownloadShortcut
	 */
	class DownloadShortcut
	{
		public $id;
		public $type;
		public $name;
		public $fileName;
		public $note;
		public $tooltip;
		public $imagePath;
		public $sourcePath;
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
			$nameTags = $linkConfig->getElementsByTagName("Source");
			$this->fileName = $nameTags->length > 0 ? trim($nameTags->item(0)->nodeValue) : '';
			$tooltipTags = $linkConfig->getElementsByTagName("line2");
			$this->tooltip = $tooltipTags->length > 0 ? trim($tooltipTags->item(0)->nodeValue) : '';
			$tooltipTags = $linkConfig->getElementsByTagName("filenotes");
			$this->note = $tooltipTags->length > 0 ? trim($tooltipTags->item(0)->nodeValue) : '';
			$baseUrl = Yii::app()->getBaseUrl(true);
			$this->imagePath = $baseUrl . $linkRecord->image_path . '?' . $linkRecord->id_page . $linkRecord->id;
			$this->sourceLink = Yii::app()->createAbsoluteUrl('shortcuts/download', array('linkId' => $this->id));
			$this->sourcePath = $linkRecord->source_path . DIRECTORY_SEPARATOR . trim($this->fileName);
		}
	}
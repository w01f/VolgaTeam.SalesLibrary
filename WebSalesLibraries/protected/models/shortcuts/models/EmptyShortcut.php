<?php

	/**
	 * Class EmptyShortcut
	 */
	class EmptyShortcut
	{
		public $id;
		public $type;
		public $name;
		public $tooltip;
		public $imagePath;

		/**
		 * @param $linkRecord
		 */
		public function __construct($linkRecord)
		{
			$this->id = $linkRecord->id;
			$this->type = 'none';
			$this->imagePath = Yii::app()->getBaseUrl(true) . $linkRecord->image_path . '?' . $linkRecord->id_page . $linkRecord->id;
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($linkRecord->config);
			$nameTags = $linkConfig->getElementsByTagName("line1");
			$this->name = $nameTags->length > 0 ? trim($nameTags->item(0)->nodeValue) : '';
			$tooltipTags = $linkConfig->getElementsByTagName("line2");
			$this->tooltip = $tooltipTags->length > 0 ? trim($tooltipTags->item(0)->nodeValue) : '';
		}
	}
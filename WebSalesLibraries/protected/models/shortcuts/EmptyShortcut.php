<?php
	class EmptyShortcut
	{
		public $type;
		public $tooltip;
		public $imagePath;

		public function __construct($linkRecord)
		{
			$this->type = 'none';
			$this->imagePath = Yii::app()->getBaseUrl(true) . $linkRecord->image_path . '?' . $linkRecord->id;
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($linkRecord->config);
			$tooltipTags = $linkConfig->getElementsByTagName("ToolTip");
			$this->tooltip = $tooltipTags->length > 0 ? trim($tooltipTags->item(0)->nodeValue) : '';
		}
	}
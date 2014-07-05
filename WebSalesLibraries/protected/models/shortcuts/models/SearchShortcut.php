<?php

	/**
	 * Class SearchShortcut
	 */
	class SearchShortcut
	{
		public $type;
		public $name;
		public $title;
		public $tooltip;
		public $imagePath;
		public $ribbonLogoPath;
		public $sourceLink;
		public $samePage;
		public $showResultsBar;

		public $conditions;

		private $linkRecord;

		/**
		 * @param $linkRecord
		 */
		public function __construct($linkRecord)
		{
			$this->linkRecord = $linkRecord;
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($linkRecord->config);
			$this->type = trim($linkConfig->getElementsByTagName("Type")->item(0)->nodeValue);
			$samePageTags = $linkConfig->getElementsByTagName("OpenOnSamePage");
			$this->samePage = $samePageTags->length > 0 ? filter_var(trim($samePageTags->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;
			$titleTags = $linkConfig->getElementsByTagName("Title");
			$this->title = $titleTags->length > 0 ? trim($titleTags->item(0)->nodeValue) : '';
			$nameTags = $linkConfig->getElementsByTagName("line1");
			$this->name = $nameTags->length > 0 ? trim($nameTags->item(0)->nodeValue) : '';
			$tooltipTags = $linkConfig->getElementsByTagName("line2");
			$this->tooltip = $tooltipTags->length > 0 ? trim($tooltipTags->item(0)->nodeValue) : '';
			$baseUrl = Yii::app()->getBaseUrl(true);
			$this->imagePath = $baseUrl . $linkRecord->image_path . '?' . $linkRecord->id;
			$this->ribbonLogoPath = $baseUrl . $linkRecord->source_path . '/link_logo.png' . '?' . $linkRecord->id;
			$this->sourceLink = Yii::app()->createAbsoluteUrl('shortcuts/getSearchShortcut', array('linkId' => $linkRecord->id, 'samePage' => Yii::app()->browser->isMobile() ? true : $this->samePage));
			$showResultsBarTags = $linkConfig->getElementsByTagName("ShowResultsBar");
			$this->showResultsBar = $showResultsBarTags->length > 0 ? filter_var(trim($showResultsBarTags->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : true;
		}

		public function loadSearchConditions()
		{
			$this->conditions = new SearchConditions($this->linkRecord->config);
		}
	}
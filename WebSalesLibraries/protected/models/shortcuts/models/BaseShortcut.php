<?php

	/**
	 * Class BaseShortcut
	 */
	abstract class BaseShortcut
	{
		public $id;
		public $pageId;
		public $type;
		public $name;
		public $tooltip;
		public $title;
		public $imagePath;
		public $sourceLink;
		public $samePage;
		public $viewPath;

		public $mobileHeader;

		/** @var $linkRecord ShortcutsLinkRecord */
		protected $linkRecord;

		/**
		 * @param $linkRecord
		 */
		public function __construct($linkRecord)
		{
			$this->linkRecord = $linkRecord;
			$this->id = $linkRecord->id;
			$this->pageId = $linkRecord->id_page;
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($linkRecord->config);
			$this->type = trim($linkConfig->getElementsByTagName("Type")->item(0)->nodeValue);
			$nameTags = $linkConfig->getElementsByTagName("line1");
			$this->name = $nameTags->length > 0 ? trim($nameTags->item(0)->nodeValue) : '';
			$tooltipTags = $linkConfig->getElementsByTagName("line2");
			$this->tooltip = $tooltipTags->length > 0 ? trim($tooltipTags->item(0)->nodeValue) : '';
			$titleTags = $linkConfig->getElementsByTagName("Title");
			$this->title = $titleTags->length > 0 ? trim($titleTags->item(0)->nodeValue) : '';
			$this->imagePath = Yii::app()->getBaseUrl(true) . $linkRecord->image_path . '?' . $linkRecord->id_page . $linkRecord->id;
			$this->samePage = false;

			$mobileHeaderTags = $linkConfig->getElementsByTagName("JQMtitle");
			$this->mobileHeader = $mobileHeaderTags->length > 0 ? trim($mobileHeaderTags->item(0)->nodeValue) : '';
		}

		/**
		 * @return string
		 */
		public abstract function getServiceData();
	}
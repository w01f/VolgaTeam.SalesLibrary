<?php

	/**
	 * Class QuickListShortcut
	 */
	class QuickListShortcut
	{
		public $id;
		public $type;
		public $name;
		public $title;
		public $tooltip;
		public $imagePath;
		public $ribbonLogoPath;
		public $sourceLink;
		public $samePage;

		public $linkRecord;

		public $fileLinks;

		/**
		 * @param $linkRecord
		 */
		public function __construct($linkRecord)
		{
			$this->id = $linkRecord->id;
			$this->linkRecord = $linkRecord;
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($linkRecord->config);
			$this->type = trim($linkConfig->getElementsByTagName("Type")->item(0)->nodeValue);
			$nameTags = $linkConfig->getElementsByTagName("line1");
			$this->name = $nameTags->length > 0 ? trim($nameTags->item(0)->nodeValue) : '';
			$tooltipTags = $linkConfig->getElementsByTagName("line2");
			$this->tooltip = $tooltipTags->length > 0 ? trim($tooltipTags->item(0)->nodeValue) : '';
			$samePageTags = $linkConfig->getElementsByTagName("OpenOnSamePage");
			$this->samePage = $samePageTags->length > 0 ? filter_var(trim($samePageTags->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;
			$titleTags = $linkConfig->getElementsByTagName("Title");
			$this->title = $titleTags->length > 0 ? trim($titleTags->item(0)->nodeValue) : '';
			$baseUrl = Yii::app()->getBaseUrl(true);
			$this->imagePath = $baseUrl . $linkRecord->image_path . '?' . $linkRecord->id_page . $linkRecord->id;
			$this->ribbonLogoPath = $baseUrl . $linkRecord->source_path . '/link_logo.png' . '?' . $linkRecord->id_page . $linkRecord->id;
			$this->sourceLink = Yii::app()->createAbsoluteUrl('shortcuts/getQuickList', array('linkId' => $linkRecord->id, 'samePage' => $this->samePage));
		}

		/**
		 *
		 */
		public function loadQuickLinks()
		{
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($this->linkRecord->config);
			$baseUrl = Yii::app()->getBaseUrl(true);
			$quickListPath = $baseUrl . $this->linkRecord->source_path . '/quicklist/';
			$fileNodes = $linkConfig->getElementsByTagName("File");
			foreach ($fileNodes as $fileNode)
			{
				$fileName = trim($fileNode->nodeValue);
				$fileLink = new QuickListFile();
				$fileLink->name = $fileName;
				$fileLink->link = str_replace('&', '%26', str_replace(' ', '%20', $quickListPath . $fileName));
				$this->fileLinks[] = $fileLink;
			}
		}
	}
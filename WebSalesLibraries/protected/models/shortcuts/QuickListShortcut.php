<?php
	class QuickListShortcut
	{
		public $type;
		public $title;
		public $imagePath;
		public $sourceLink;
		public $samePage;

		public $linkRecord;

		public $fileLinks;

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
			$this->imagePath = Yii::app()->getBaseUrl(true) . $linkRecord->image_path . '?' . $linkRecord->id;
			$this->sourceLink = Yii::app()->createAbsoluteUrl('shortcuts/getQuickList', array('linkId' => $linkRecord->id, 'samePage' => $this->samePage));
		}

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
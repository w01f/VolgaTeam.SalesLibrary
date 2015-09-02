<?php

	/**
	 * Class DownloadShortcut
	 */
	class DownloadShortcut extends PageContentShortcut
	{
		public $fileName;
		public $sourcePath;
		public $downloadLink;

		/**
		 * @param $linkRecord
		 * @param $isPhone boolean
		 */
		public function __construct($linkRecord, $isPhone)
		{
			parent::__construct($linkRecord, $isPhone);
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($linkRecord->config);

			$nameTags = $linkConfig->getElementsByTagName("Source");
			$this->fileName = $nameTags->length > 0 ? trim($nameTags->item(0)->nodeValue) : '';

			$this->sourcePath = $linkRecord->source_path . DIRECTORY_SEPARATOR . trim($this->fileName);
			$this->downloadLink = Yii::app()->createAbsoluteUrl('shortcuts/download', array('linkId' => $this->id));
		}

		/**
		 * @return array
		 */
		public function getPageData()
		{
			$data = parent::getPageData();
			$data['url'] = $this->downloadLink;
			return $data;
		}

		/**
		 * @return string
		 */
		public function getTypeForActivityTracker()
		{
			return 'Shortcut Download Link';
		}
	}
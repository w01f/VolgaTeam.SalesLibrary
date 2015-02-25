<?php

	/**
	 * Class DownloadShortcut
	 */
	class DownloadShortcut extends BaseShortcut
	{
		public $fileName;
		public $note;
		public $sourcePath;

		/**
		 * @param $linkRecord
		 */
		public function __construct($linkRecord)
		{
			parent::__construct($linkRecord);
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($linkRecord->config);

			$this->viewPath = Yii::app()->browser->isMobile() ? 'directLink' : 'downloadLink';

			$nameTags = $linkConfig->getElementsByTagName("Source");
			$this->fileName = $nameTags->length > 0 ? trim($nameTags->item(0)->nodeValue) : '';
			$noteTags = $linkConfig->getElementsByTagName("filenotes");
			$this->note = $noteTags->length > 0 ? trim($noteTags->item(0)->nodeValue) : '';

			$this->sourceLink = Yii::app()->createAbsoluteUrl('shortcuts/download', array('linkId' => $this->id));
			$this->sourcePath = $linkRecord->source_path . DIRECTORY_SEPARATOR . trim($this->fileName);
		}

		/**
		 * @return string
		 */
		public function getServiceData()
		{
			$result = '';
			$result .= '<div class="link-id">' . $this->id . '</div>';
			$result .= '<div class="url">' . $this->sourceLink . '</div>';
			return $result;
		}
	}
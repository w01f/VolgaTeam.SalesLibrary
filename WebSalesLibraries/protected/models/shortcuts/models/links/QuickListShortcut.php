<?php

	/**
	 * Class QuickListShortcut
	 */
	class QuickListShortcut extends PageContentShortcut
	{
		public $fileLinks;

		/**
		 * @param $linkRecord
		 * @param $isPhone boolean
		 */
		public function __construct($linkRecord, $isPhone)
		{
			parent::__construct($linkRecord, $isPhone);
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($linkRecord->config);

			$baseUrl = Yii::app()->getBaseUrl(true);
			$quickListPath = $baseUrl . str_replace('\\', '/', $this->relativePath . DIRECTORY_SEPARATOR . 'quicklist' . DIRECTORY_SEPARATOR);
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

		/**
		 * @return string
		 */
		public function getTypeForActivityTracker()
		{
			return 'Quicklist';
		}
	}
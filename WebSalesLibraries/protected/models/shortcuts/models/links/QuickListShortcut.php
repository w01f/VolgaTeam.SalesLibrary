<?php

	/**
	 * Class QuickListShortcut
	 */
	class QuickListShortcut extends PageContentShortcut
	{
		public $fileLinks;

		public function loadPageConfig()
		{
			parent::loadPageConfig();

			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($this->linkRecord->config);

			$baseUrl = Yii::app()->getBaseUrl(true);
			$quickListPath = $baseUrl . str_replace('\\', '/', $this->relativePath . DIRECTORY_SEPARATOR . 'quicklist' . DIRECTORY_SEPARATOR);
			$fileNodes = $linkConfig->getElementsByTagName("File");
			foreach ($fileNodes as $fileNode)
			{
				$fileName = trim($fileNode->nodeValue);
				$fileLink = new QuickListFile();
				$fileLink->name = $fileName;
				$fileLink->link = Utils::formatUrl($quickListPath . $fileName);
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
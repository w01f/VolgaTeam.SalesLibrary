<?php

	/**
	 * Class PageShortcut
	 */
	class PageShortcut extends BaseShortcut
	{
		public $libraryName;
		public $pageName;

		/**
		 * @param $linkRecord
		 */
		public function __construct($linkRecord)
		{
			parent::__construct($linkRecord);
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($linkRecord->config);

			$this->viewPath = 'pageLink';

			$this->libraryName = trim($linkConfig->getElementsByTagName("Library")->item(0)->nodeValue);
			$this->pageName = trim($linkConfig->getElementsByTagName("Page")->item(0)->nodeValue);
		}

		/**
		 * @return string
		 */
		public function getServiceData()
		{
			$result = '';
			$result .= '<div class="library-name">' . $this->libraryName . '</div>';
			$result .= '<div class="page-name">' . $this->pageName . '</div>';
			return $result;
		}
	}
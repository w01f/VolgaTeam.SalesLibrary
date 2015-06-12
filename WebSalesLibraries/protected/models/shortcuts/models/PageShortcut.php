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
			$result .= '<div class="link-id">' . $this->id . '</div>';
			$result .= '<div class="library-name">' . $this->libraryName . '</div>';
			$result .= '<div class="page-name">' . $this->pageName . '</div>';
			$result .= '<div class="activity-data">' . CJSON::encode(array(
					'action' => 'Shortcut Page Link',
					'title' => sprintf('%s - %s', $this->name, $this->tooltip)
				)) . '</div>';
			return $result;
		}

		/**
		 * @return LibraryPage
		 */
		public function getLibraryPage()
		{
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($this->linkRecord->config);
			$libraryName = trim($linkConfig->getElementsByTagName("Library")->item(0)->nodeValue);
			$pageName = trim($linkConfig->getElementsByTagName("Page")->item(0)->nodeValue);
			if (!(isset($libraryName) && isset($pageName))) return null;
			$libraryPageRecord = Yii::app()->db->createCommand()
				->select("p.*")
				->from('tbl_page p')
				->join('tbl_library l', 'l.id = p.id_library')
				->where("p.name='" . $pageName . "' and l.name='" . $libraryName . "'")
				->queryRow();
			if (!is_array($libraryPageRecord)) return null;
			$libraryPageRecord = (object)$libraryPageRecord;
			$libraryManager = new LibraryManager();
			$library = $libraryManager->getLibraryById($libraryPageRecord->id_library);
			$libraryPage = new LibraryPage($library);
			$libraryPage->load($libraryPageRecord);
			return $libraryPage;
		}
	}
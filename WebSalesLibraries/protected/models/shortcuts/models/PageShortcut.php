<?

	/**
	 * Class PageShortcut
	 */
	abstract class PageShortcut extends BaseShortcut
	{
		public $libraryName;
		public $pageName;
		public $ribbonLogoPath;

		/**
		 * @param $linkRecord
		 */
		public function __construct($linkRecord)
		{
			parent::__construct($linkRecord);
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($linkRecord->config);
			$this->libraryName = trim($linkConfig->getElementsByTagName("Library")->item(0)->nodeValue);
			$this->pageName = trim($linkConfig->getElementsByTagName("Page")->item(0)->nodeValue);
			$baseUrl = Yii::app()->getBaseUrl(true);
			$this->ribbonLogoPath = $baseUrl . $linkRecord->source_path . '/rbnlogo.png' . '?' . $linkRecord->id_page . $linkRecord->id;
		}

		/**
		 * @return string
		 */
		public function getServiceData()
		{
			$result = '';
			if (isset($this->ribbonLogoPath) && @getimagesize($this->ribbonLogoPath))
				$result .= '<div class="ribbon-logo-path">' . $this->ribbonLogoPath . '</div>';
			$result .= '<div class="link-id">' . $this->id . '</div>';
			$result .= '<div class="link-type">' . $this->type . '</div>';
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
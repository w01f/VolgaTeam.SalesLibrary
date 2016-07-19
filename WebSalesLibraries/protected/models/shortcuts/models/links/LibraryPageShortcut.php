<?
	use application\models\wallbin\models\web\LibraryManager as LibraryManager;
	use application\models\wallbin\models\web\LibraryPage as LibraryPage;

	/**
	 * Class LibraryPageShortcut
	 */
	class LibraryPageShortcut extends PageContentShortcut
	{
		public $libraryName;
		public $pageName;
		public $pageViewType;

		/**
		 * @param $linkRecord
		 * @param $isPhone boolean
		 */
		public function __construct($linkRecord, $isPhone)
		{
			parent::__construct($linkRecord, $isPhone);
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($linkRecord->config);

			$this->libraryName = trim($linkConfig->getElementsByTagName("Library")->item(0)->nodeValue);
			$this->pageName = trim($linkConfig->getElementsByTagName("Page")->item(0)->nodeValue);

			$pageTypeTags = $linkConfig->getElementsByTagName("PageViewType");
			$this->pageViewType = $pageTypeTags->length > 0 ? trim($pageTypeTags->item(0)->nodeValue) : 'columns';
		}

		/**
		 * @return string
		 */
		public function getMenuItemData()
		{
			$result = parent::getMenuItemData();
			$result .= '<div class="library-name">' . $this->libraryName . '</div>';
			$result .= '<div class="page-name">' . $this->pageName . '</div>';
			return $result;
		}

		/**
		 * @return array
		 */
		public function getPageData()
		{
			$data = parent::getPageData();
			$data['pageViewType'] = $this->pageViewType;
			return $data;
		}

		/**
		 * @return string
		 */
		public function getTypeForActivityTracker()
		{
			return 'LibraryPage';
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
			if ($this->pageViewType == 'accordion')
				$libraryPage->loadData();
			return $libraryPage;
		}
	}

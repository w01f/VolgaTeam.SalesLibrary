<?

	use application\models\wallbin\models\web\LibraryManager as LibraryManager;
	use application\models\wallbin\models\web\LibraryPage as LibraryPage;
	use application\models\wallbin\models\web\LibraryFolder as LibraryFolder;
	use application\models\wallbin\models\web\style\WallbinHeaderStyle;

	/**
	 * Class WindowShortcut
	 */
	class WindowShortcut extends PageContentShortcut
	{
		public $column;
		public $windowViewType;
		public $linksOnly;

		/** @var SearchBar */
		public $searchBar;

		/** @var  WallbinHeaderStyle */
		public $header;

		/** @var  \Padding */
		public $contentPadding;

		/**
		 * @param $linkRecord
		 * @param $isPhone boolean
		 */
		public function __construct($linkRecord, $isPhone)
		{
			parent::__construct($linkRecord, $isPhone);

			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($this->linkRecord->config);
			$xpath = new DomXPath($linkConfig);

			$queryResult = $xpath->query('//Config/Library');
			$libraryName = $queryResult->length > 0 ? str_replace("'", "''", trim($queryResult->item(0)->nodeValue)) : '';

			$queryResult = $xpath->query('//Config/Page');
			$pageName = $queryResult->length > 0 ? str_replace("'", "''", trim($queryResult->item(0)->nodeValue)) : '';

			$queryResult = $xpath->query('//Config/Window');
			$windowName = $queryResult->length > 0 ? str_replace("'", "''", trim($queryResult->item(0)->nodeValue)) : '';

			$userId = \UserIdentity::getCurrentUserId();
			$isAdmin = \UserIdentity::isUserAdmin();
			$assignedPageIds = \UserLibraryRecord::getPageIdsByUserAngHisGroups($userId);

			$windowRecord = Yii::app()->db->createCommand()
				->select("f.*")
				->from('tbl_folder f')
				->join('tbl_page p', 'p.id = f.id_page')
				->join('tbl_library l', 'l.id = p.id_library')
				->where("f.name='" . $windowName . "' and p.name='" . $pageName . "' and l.name='" . $libraryName . "'")
				->queryRow();

			$this->isAccessGranted &= isset($windowRecord) && ($isAdmin || in_array($windowRecord['id_page'], $assignedPageIds));
		}

		public function loadPageConfig()
		{
			if ($this->isPhone != true)
				$this->searchBar = SearchBar::fromShortcut($this);
			else
				$this->searchBar = SearchBar::createEmpty();

			parent::loadPageConfig();

			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($this->linkRecord->config);
			$xpath = new DomXPath($linkConfig);

			$queryResult = $xpath->query('//Config/Column');
			$this->column = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) - 1 : -1;

			$queryResult = $xpath->query('//Config/WindowViewType');
			$this->windowViewType = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : 'columns';

			$queryResult = $xpath->query('//Config/LinksOnly');
			$this->linksOnly = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;

			$queryResult = $xpath->query('//Config/Style/Header');
			if ($queryResult->length == 0)
				$queryResult = $xpath->query('//Config/Header');
			if ($queryResult->length > 0)
				$this->header = WallbinHeaderStyle::fromXml($xpath, $queryResult->item(0));
			else
				$this->header = WallbinHeaderStyle::createDefault();

			$queryResult = $xpath->query('//Config/Style/Content/Padding');
			if ($queryResult->length > 0)
				$this->contentPadding = \Padding::fromXml($xpath, $queryResult->item(0));
			else
				$this->contentPadding = new \Padding(30);
		}

		/**
		 * @return array
		 */
		public function getPageData()
		{
			$data = parent::getPageData();
			$data['windowViewType'] = $this->windowViewType;
			$data['processResponsiveColumns'] = false;
			return $data;
		}

		/**
		 * @return LibraryFolder
		 */
		public function getWindow()
		{
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($this->linkRecord->config);
			$libraryName = trim($linkConfig->getElementsByTagName("Library")->item(0)->nodeValue);
			$pageName = trim($linkConfig->getElementsByTagName("Page")->item(0)->nodeValue);
			$windowName = trim($linkConfig->getElementsByTagName("Window")->item(0)->nodeValue);
			if (!(isset($libraryName) && isset($pageName) && isset($windowName))) return null;
			$windowRecord = Yii::app()->db->createCommand()
				->select("f.*")
				->from('tbl_folder f')
				->join('tbl_page p', 'p.id = f.id_page')
				->join('tbl_library l', 'l.id = p.id_library')
				->where("f.name='" . $windowName . "' and p.name='" . $pageName . "' and l.name='" . $libraryName . "'")
				->queryRow();
			if (!is_array($windowRecord)) return null;
			$windowRecord = (object)$windowRecord;
			$libraryManager = new LibraryManager();
			$library = $libraryManager->getLibraryById($windowRecord->id_library, false);
			$folder = new LibraryFolder(new LibraryPage($library));
			$folder->load($windowRecord);
			$folder->loadFiles(true);
			return $folder;
		}

		/**
		 * @param $actionsByKey array
		 * @param $xpath DOMXPath
		 * @param $actionConfigNodes DOMNodeList
		 */
		protected function customizeActions($actionsByKey, $xpath, $actionConfigNodes)
		{
			parent::customizeActions($actionsByKey, $xpath, $actionConfigNodes);
			if (array_key_exists('show-search', $actionsByKey))
				$actionsByKey['show-search']->enabled = $actionsByKey['show-search']->enabled && $this->searchBar->configured;
			if (array_key_exists('hide-search', $actionsByKey))
				$actionsByKey['hide-search']->enabled = $actionsByKey['hide-search']->enabled && $this->searchBar->configured;
		}

		/**
		 * @return string
		 */
		public function getTypeForActivityTracker()
		{
			return 'Window';
		}

		/** @return SearchBar */
		public function getSearchBar()
		{
			return $this->searchBar;
		}
	}
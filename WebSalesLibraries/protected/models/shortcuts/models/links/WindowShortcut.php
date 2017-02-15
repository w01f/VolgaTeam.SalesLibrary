<?
	use application\models\wallbin\models\web\LibraryManager as LibraryManager;
	use application\models\wallbin\models\web\LibraryPage as LibraryPage;
	use application\models\wallbin\models\web\LibraryFolder as LibraryFolder;

	/**
	 * Class WindowShortcut
	 */
	class WindowShortcut extends PageContentShortcut implements ISearchBarContainer
	{
		public $column;
		public $windowViewType;
		public $linksOnly;

		/** @var SearchBar */
		public $searchBar;

		/**
		 * @param $linkRecord
		 * @param $isPhone boolean
		 */
		public function __construct($linkRecord, $isPhone)
		{
			$this->linkRecord = $linkRecord;
			if ($isPhone != true)
				$this->searchBar = SearchBar::fromShortcut($this);
			else
				$this->searchBar = SearchBar::createEmpty();

			parent::__construct($linkRecord, $isPhone);

			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($linkRecord->config);
			$xpath = new DomXPath($linkConfig);

			$queryResult = $xpath->query('//Config/Column');
			$this->column = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) - 1 : -1;

			$queryResult = $xpath->query('//Config/WindowViewType');
			$this->windowViewType = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : 'columns';

			$queryResult = $xpath->query('//Config/LinksOnly');
			$this->linksOnly = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;
		}

		/**
		 * @return array
		 */
		public function getPageData()
		{
			$data = parent::getPageData();
			$data['windowViewType'] = $this->windowViewType;
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
			$library = $libraryManager->getLibraryById($windowRecord->id_library);
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
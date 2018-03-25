<?

	use application\models\wallbin\models\web\LibraryManager as LibraryManager;
	use application\models\wallbin\models\web\LibraryPage as LibraryPage;
	use application\models\wallbin\models\web\Library as Library;
	use application\models\wallbin\models\web\style\IWallbinStyleContainer;
	use application\models\wallbin\models\web\style\WallbinStyle as WallbinStyle;

	/**
	 * Class LibraryPageShortcut
	 */
	class LibraryPageShortcut extends PageContentShortcut implements IWallbinStyleContainer
	{
		public $libraryName;
		public $pageName;
		public $pageViewType;

		/** @var SearchBar */
		public $searchBar;

		/** @var  WallbinStyle */
		public $style;

		/** @var $library Library */
		public $library;

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
			$this->libraryName = $queryResult->length > 0 ? str_replace("'", "''", trim($queryResult->item(0)->nodeValue)) : null;
			$queryResult = $xpath->query('//Config/Page');
			$this->pageName = $queryResult->length > 0 ? str_replace("'", "''", trim($queryResult->item(0)->nodeValue)) : null;
			$queryResult = $xpath->query('//Config/PageViewType');
			$this->pageViewType = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : 'columns';

			$userId = \UserIdentity::getCurrentUserId();
			$isAdmin = \UserIdentity::isUserAdmin();
			$assignedPageIds = \UserLibraryRecord::getPageIdsByUserAngHisGroups($userId);
			$libraryPageRecord = Yii::app()->db->createCommand()
				->select("p.*")
				->from('tbl_page p')
				->join('tbl_library l', 'l.id = p.id_library')
				->where("p.name='" . $this->pageName . "' and l.name='" . $this->libraryName . "'")
				->queryRow();
			$this->isAccessGranted &= isset($libraryPageRecord) && ($isAdmin || in_array($libraryPageRecord['id'], $assignedPageIds));

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
			$queryResult = $xpath->query('//Config/WallbinStyle');
			if ($queryResult->length > 0)
				$this->style = WallbinStyle::fromXml($xpath, $queryResult->item(0));
			else
				$this->style = WallbinStyle::createDefault();

			$libraryManager = new LibraryManager();
			$this->library = $libraryManager->getLibraryByName($this->libraryName);
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
			$data['serviceData'] = $this->getMenuItemData();
			$data['pageViewType'] = $this->pageViewType;
			$data['processResponsiveColumns'] = $this->style->page->showResponsiveColumns;
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
			/** @var LibraryPageRecord $libraryPageRecord */
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
			$libraryPage->loadData();
			if ($this->pageViewType == 'columns')
				$libraryPage->loadFolders(true);
			return $libraryPage;
		}

		/**
		 * @param $actionsByKey array
		 * @param $xpath DOMXPath
		 * @param $actionConfigNodes DOMNodeList
		 */
		protected function customizeActions($actionsByKey, $xpath, $actionConfigNodes)
		{
			parent::customizeActions($actionsByKey, $xpath, $actionConfigNodes);
			if (array_key_exists('page-view-columns', $actionsByKey))
				$actionsByKey['page-view-columns']->enabled = $actionsByKey['page-view-columns']->enabled && $this->pageViewType != 'columns';
			if (array_key_exists('page-view-accordion', $actionsByKey))
				$actionsByKey['page-view-accordion']->enabled = $actionsByKey['page-view-accordion']->enabled && $this->pageViewType != 'accordion';
			if (array_key_exists('page-zoom-in', $actionsByKey))
				$actionsByKey['page-zoom-in']->enabled = $actionsByKey['page-zoom-in']->enabled && $this->pageViewType == 'columns';
			if (array_key_exists('page-zoom-out', $actionsByKey))
				$actionsByKey['page-zoom-out']->enabled = $actionsByKey['page-zoom-out']->enabled && $this->pageViewType == 'columns';
			if (array_key_exists('show-search', $actionsByKey))
				$actionsByKey['show-search']->enabled = $actionsByKey['show-search']->enabled && isset($this->searchBar) && $this->searchBar->configured;
			if (array_key_exists('hide-search', $actionsByKey))
				$actionsByKey['hide-search']->enabled = $actionsByKey['hide-search']->enabled && isset($this->searchBar) && $this->searchBar->configured;
		}

		public function updateAction()
		{
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($this->linkRecord->config);
			$xpath = new DomXPath($linkConfig);
			$queryResult = $xpath->query('//Config/Actions/Action');
			$this->initActions($xpath, $queryResult);
		}

		/** @return SearchBar */
		public function getSearchBar()
		{
			return $this->searchBar;
		}

		/** @return string */
		public function getStyleContainerId()
		{
			return $this->id;
		}

		/** @return WallbinStyle */
		public function getStyle()
		{
			return $this->style;
		}

		/** @return string */
		public function getStyleContainerType()
		{
			return 'shortcut';
		}
	}

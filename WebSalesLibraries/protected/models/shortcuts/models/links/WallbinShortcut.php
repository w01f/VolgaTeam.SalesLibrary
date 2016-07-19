<?
	use application\models\wallbin\models\web\LibraryManager as LibraryManager;
	use application\models\wallbin\models\web\Library as Library;

	/**
	 * Class WallbinShortcut
	 */
	class WallbinShortcut extends PageContentShortcut
	{
		public $libraryName;
		public $pageSelectorMode;
		public $pageViewType;

		/** @var $library Library */
		public $library;

		/**
		 * @param $linkRecord
		 * @param $isPhone boolean
		 */
		public function __construct($linkRecord, $isPhone)
		{
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($linkRecord->config);

			$pageTypeTags = $linkConfig->getElementsByTagName("PageViewType");
			$this->pageViewType = $pageTypeTags->length > 0 ? trim($pageTypeTags->item(0)->nodeValue) : 'columns';
			$pageSelectorModeTags = $linkConfig->getElementsByTagName("PageSelectorMode");
			$this->pageSelectorMode = $pageSelectorModeTags->length > 0 ? trim($pageSelectorModeTags->item(0)->nodeValue) : 'tabs';

			parent::__construct($linkRecord, $isPhone);

			$libraryManager = new LibraryManager();
			$this->libraryName = trim($linkConfig->getElementsByTagName("Library")->item(0)->nodeValue);
			$this->library = $libraryManager->getLibraryByName($this->libraryName);
		}

		/**
		 * @return array
		 */
		public function getPageData()
		{
			$data = parent::getPageData();
			$data['serviceData'] = $this->getMenuItemData();
			$data['libraryId'] = $this->library->id;
			$data['pageViewType'] = $this->pageViewType;
			$data['pageSelectorMode'] = $this->pageSelectorMode;
			return $data;
		}

		/**
		 * @return string
		 */
		public function getTypeForActivityTracker()
		{
			return 'Library';
		}

		/**
		 * @param $actionsByKey array
		 * @param $xpath DOMXPath
		 * @param $actionConfigNodes DOMNodeList
		 */
		protected function customizeActions($actionsByKey, $xpath, $actionConfigNodes)
		{
			parent::customizeActions($actionsByKey, $xpath, $actionConfigNodes);
			if (array_key_exists('page-select-tabs', $actionsByKey))
				$actionsByKey['page-select-tabs']->enabled = $actionsByKey['page-select-tabs']->enabled && $this->pageSelectorMode != 'tabs';
			if (array_key_exists('page-select-combo', $actionsByKey))
				$actionsByKey['page-select-combo']->enabled = $actionsByKey['page-select-combo']->enabled && $this->pageSelectorMode != 'combo';
			if (array_key_exists('page-view-columns', $actionsByKey))
				$actionsByKey['page-view-columns']->enabled = $actionsByKey['page-view-columns']->enabled && $this->pageViewType != 'columns';
			if (array_key_exists('page-view-accordion', $actionsByKey))
				$actionsByKey['page-view-accordion']->enabled = $actionsByKey['page-view-accordion']->enabled && $this->pageViewType != 'accordion';
			if (array_key_exists('page-zoom-in', $actionsByKey))
				$actionsByKey['page-zoom-in']->enabled = $actionsByKey['page-zoom-in']->enabled && $this->pageViewType == 'columns';
			if (array_key_exists('page-zoom-out', $actionsByKey))
				$actionsByKey['page-zoom-out']->enabled = $actionsByKey['page-zoom-out']->enabled && $this->pageViewType == 'columns';
		}

		public function updateAction()
		{
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($this->linkRecord->config);
			$xpath = new DomXPath($linkConfig);
			$queryResult = $xpath->query('//Config/Actions/Action');
			$this->initActions($xpath, $queryResult);
		}
	}
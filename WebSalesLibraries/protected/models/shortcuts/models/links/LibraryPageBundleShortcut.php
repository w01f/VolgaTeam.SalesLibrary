<?

	use application\models\wallbin\models\web\style\IWallbinStyleContainer;
	use application\models\wallbin\models\web\style\WallbinStyle as WallbinStyle;

	/**
	 * Class LibraryPageBundleShortcut
	 */
	class LibraryPageBundleShortcut extends PageContentShortcut implements IWallbinStyleContainer
	{
		public $pageSelectorMode;
		public $pageViewType;

		/** @var SearchBar */
		public $searchBar;

		/** @var  WallbinStyle */
		public $style;

		/** @var LibraryPageBundleItem[] */
		public $items;

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

			$queryResult = $xpath->query('//Config/PageViewType');
			$this->pageViewType = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : 'columns';
			$queryResult = $xpath->query('//Config/PageSelectorMode');
			$this->pageSelectorMode = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : 'columns';

			$this->items = array();
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

			$queryResult = $xpath->query('//Config/Bundle/Item');
			foreach ($queryResult as $itemNode)
				$this->items[] = LibraryPageBundleItem::fromXml($xpath, $itemNode);
		}

		/**
		 * @return array
		 */
		public function getPageData()
		{
			$data = parent::getPageData();
			$data['serviceData'] = $this->getMenuItemData();
			$data['bundleId'] = $this->id;
			$data['pageViewType'] = $this->pageViewType;
			$data['pageSelectorMode'] = $this->pageSelectorMode;
			$data['processResponsiveColumns'] = $this->style->page->responsiveColumnsStyle->enabled;
			return $data;
		}

		/**
		 * @return string
		 */
		public function getTypeForActivityTracker()
		{
			return 'LibraryPageBundle';
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
			if (array_key_exists('show-search', $actionsByKey))
				$actionsByKey['show-search']->enabled = $actionsByKey['show-search']->enabled && $this->searchBar->configured;
			if (array_key_exists('hide-search', $actionsByKey))
				$actionsByKey['hide-search']->enabled = $actionsByKey['hide-search']->enabled && $this->searchBar->configured;
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
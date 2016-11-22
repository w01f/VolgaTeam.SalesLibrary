<?
	use application\models\wallbin\models\web\LibraryManager as LibraryManager;
	use application\models\wallbin\models\web\Library as Library;
	use application\models\wallbin\models\web\style\WallbinStyle as WallbinStyle;

	/**
	 * Class WallbinShortcut
	 */
	class WallbinShortcut extends PageContentShortcut
	{
		public $libraryName;
		public $pageSelectorMode;
		public $pageViewType;

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
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($linkRecord->config);
			$xpath = new DomXPath($linkConfig);

			$queryResult = $xpath->query('//Config/Library');
			$this->libraryName = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;
			$queryResult = $xpath->query('//Config/PageViewType');
			$this->pageViewType = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : 'columns';
			$queryResult = $xpath->query('//Config/PageSelectorMode');
			$this->pageSelectorMode = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : 'columns';

			parent::__construct($linkRecord, $isPhone);

			$queryResult = $xpath->query('//Config/WallbinStyle');
			if ($queryResult->length > 0)
				$this->style = WallbinStyle::fromXml($xpath, $queryResult->item(0));
			else
				$this->style = WallbinStyle::createEmpty();

			$libraryManager = new LibraryManager();
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
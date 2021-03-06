<?

	use application\models\wallbin\models\web\LibraryManager as LibraryManager;
	use application\models\wallbin\models\web\style\IWallbinStyleContainer;
	use application\models\wallbin\models\web\style\WallbinStyle;

	/**
	 * Class InternalWallbinPreviewInfo
	 */
	class InternalWallbinPreviewInfo extends InternalLibraryContentPreviewInfo implements IWallbinStyleContainer
	{
		public $libraryName;
		public $pageName;
		public $libraryId;
		public $pageId;
		public $pageViewType;
		public $pageSelectorMode;
		public $processResponsiveColumns;

		/** @var  WallbinStyle */
		public $style;

		/**
		 * @param $linkSettings InternalWallbinLinkSettings
		 * @param bool $isPhone
		 */
		public function __construct($linkSettings, $isPhone)
		{
			parent::__construct($linkSettings, $isPhone);

			$this->libraryName = str_replace("'", "''", $linkSettings->libraryName);
			$this->pageName = str_replace("'", "''", $linkSettings->pageName);

			$this->pageViewType = 'columns';
			$this->pageSelectorMode = 'tabs';
			$this->processResponsiveColumns = false;
			$this->style = WallbinStyle::createDefault();
			if (!empty($this->styleSettingsEncoded))
			{
				$styleConfig = new DOMDocument();
				$styleConfig->loadXML($this->styleSettingsEncoded);
				$xpath = new DomXPath($styleConfig);

				$queryResult = $xpath->query('//Config/PageViewType');
				$this->pageViewType = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $this->pageViewType;
				$queryResult = $xpath->query('//Config/PageSelectorMode');
				$this->pageSelectorMode = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $this->pageSelectorMode;

				$queryResult = $xpath->query('//Config/WallbinStyle');
				if ($queryResult->length > 0)
				{
					$this->style = WallbinStyle::fromXml($xpath, $queryResult->item(0));
					$this->processResponsiveColumns = $this->style->page->responsiveColumnsStyle->enabled;
				}

				$queryResult = $xpath->query('//Config/Actions/Action');
				$this->initActions($xpath, $queryResult);
			}

			$libraryManager = new LibraryManager();
			$library = $libraryManager->getLibraryByName($this->libraryName, false);
			$this->libraryId = $library->id;

			$libraryPageRecord = Yii::app()->db->createCommand()
				->select("p.*")
				->from('tbl_page p')
				->join('tbl_library l', 'l.id = p.id_library')
				->where("p.name='" . $this->pageName . "' and l.name='" . $this->libraryName . "'")
				->queryRow();
			if (is_array($libraryPageRecord))
			{
				$libraryPageRecord = (object)$libraryPageRecord;
				$this->pageId = $libraryPageRecord->id;
			}
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

		/** @return string */
		public function getStyleContainerId()
		{
			return $this->internalLinkId;
		}

		/** @return WallbinStyle */
		public function getStyle()
		{
			return $this->style;
		}

		/** @return string */
		public function getStyleContainerType()
		{
			return 'internal link';
		}
	}
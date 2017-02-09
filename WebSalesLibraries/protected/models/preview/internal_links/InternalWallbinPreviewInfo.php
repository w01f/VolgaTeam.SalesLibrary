<?
	use application\models\wallbin\models\web\LibraryManager as LibraryManager;
	use application\models\wallbin\models\web\style\WallbinStyle;

	/**
	 * Class InternalWallbinPreviewInfo
	 */
	class InternalWallbinPreviewInfo
	{
		public $internalLinkType;

		public $libraryName;
		public $pageName;
		public $libraryId;
		public $pageId;
		public $headerIcon;
		public $showHeaderText;
		public $pageViewType;
		public $pageSelectorMode;

		public $navigationPanel;

		/** @var SearchBar */
		public $searchBar;

		/** @var  WallbinStyle */
		public $style;

		/**
		 * @param $linkSettings InternalWallbinLinkSettings
		 */
		public function __construct($linkSettings)
		{
			$this->internalLinkType = $linkSettings->internalLinkType;

			$this->libraryName = $linkSettings->libraryName;
			$this->pageName = $linkSettings->pageName;
			$this->showHeaderText = $linkSettings->showHeaderText;

			$this->pageViewType = 'columns';
			$this->pageSelectorMode = 'tabs';
			$this->style = WallbinStyle::createDefault();
			$this->searchBar = SearchBar::createEmpty();
			if (!empty($linkSettings->styleSettingsEncoded))
			{
				$styleConfig = new DOMDocument();
				$styleConfig->loadXML(base64_decode($linkSettings->styleSettingsEncoded));
				$xpath = new DomXPath($styleConfig);

				$queryResult = $xpath->query('//Config/PageViewType');
				$this->pageViewType = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $this->pageViewType;
				$queryResult = $xpath->query('//Config/PageSelectorMode');
				$this->pageSelectorMode = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $this->pageSelectorMode;

				$queryResult = $xpath->query('//Config/WallbinStyle');
				if ($queryResult->length > 0)
					$this->style = WallbinStyle::fromXml($xpath, $queryResult->item(0));

				$queryResult = $xpath->query('//Config/HeaderIcon');
				$this->headerIcon = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $this->headerIcon;

				$queryResult = $xpath->query('//Config/ShowLeftPanel');
				$showNavigationPanel = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;
				$queryResult = $xpath->query('//Config/LeftPanelID');
				$navigationPanelId = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;
				if ($showNavigationPanel)
				{
					$navigationPanelData = ShortcutsManager::getNavigationPanel($navigationPanelId);
					$viewPath = \Yii::getPathOfAlias('application.views.regular.shortcuts.navigationPanel') . '/itemsList.php';
					$this->navigationPanel = \Yii::app()->controller->renderFile($viewPath, array('navigationPanel' => $navigationPanelData), true);
				}
			}

			$libraryManager = new LibraryManager();
			$library = $libraryManager->getLibraryByName($this->libraryName);
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
	}
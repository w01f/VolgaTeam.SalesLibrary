<?
	use application\models\wallbin\models\web\LibraryManager as LibraryManager;
	use application\models\wallbin\models\web\LibraryPage as LibraryPage;
	use application\models\wallbin\models\web\style\WallbinStyle;

	/**
	 * Class InternalLibraryPagePreviewInfo
	 */
	class InternalLibraryPagePreviewInfo
	{
		public $internalLinkType;

		public $libraryName;
		public $pageName;
		public $headerIcon;
		public $showHeaderText;
		public $pageViewType;

		public $navigationPanel;

		/** @var SearchBar */
		public $searchBar;

		/** @var  WallbinStyle */
		public $style;

		public $showLogo;
		public $showText;
		public $showWindowHeaders;
		public $textColor;
		public $backColor;

		/**
		 * @param $linkSettings InternalLibraryPageLinkSettings
		 */
		public function __construct($linkSettings)
		{
			$this->internalLinkType = $linkSettings->internalLinkType;

			$this->libraryName = $linkSettings->libraryName;
			$this->pageName = $linkSettings->pageName;
			$this->headerIcon = $linkSettings->headerIcon;
			$this->showHeaderText = $linkSettings->showHeaderText;

			$this->pageViewType = 'columns';
			$this->style = WallbinStyle::createDefault();
			$this->searchBar = SearchBar::createEmpty();
			if (!empty($linkSettings->styleSettingsEncoded))
			{
				$styleConfig =  new DOMDocument();
				$styleConfig->loadXML(base64_decode($linkSettings->styleSettingsEncoded));
				$xpath = new DomXPath($styleConfig);

				$queryResult = $xpath->query('//Config/PageViewType');
				$this->pageViewType = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $this->pageViewType;

				$queryResult = $xpath->query('//Config/WallbinStyle');
				if ($queryResult->length > 0)
					$this->style = WallbinStyle::fromXml($xpath, $queryResult->item(0));

				$queryResult = $xpath->query('//Config/ShowLeftPanel');
				$showNavigationPanel = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;
				$queryResult = $xpath->query('//Config/LeftPanelID');
				$navigationPanelId = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;
				if ($showNavigationPanel && isset($navigationPanelId))
				{
					$navigationPanelData = ShortcutsManager::getNavigationPanel($this->$navigationPanelId);
					$viewPath = \Yii::getPathOfAlias('application.views.regular.shortcuts.navigationPanel') . '/itemsList.php';
					$this->navigationPanel = \Yii::app()->controller->renderFile($viewPath, array('navigationPanel' => $navigationPanelData), true);
				}
			}
		}

		/**
		 * @return LibraryPage
		 */
		public function getLibraryPage()
		{
			if (!(isset($this->libraryName) && isset($this->pageName))) return null;
			/** @var LibraryPageRecord $libraryPageRecord */
			$libraryPageRecord = Yii::app()->db->createCommand()
				->select("p.*")
				->from('tbl_page p')
				->join('tbl_library l', 'l.id = p.id_library')
				->where("p.name='" . $this->pageName . "' and l.name='" . $this->libraryName . "'")
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
	}
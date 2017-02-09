<?
	use application\models\wallbin\models\web\LibraryManager as LibraryManager;
	use application\models\wallbin\models\web\LibraryPage as LibraryPage;
	use application\models\wallbin\models\web\LibraryFolder as LibraryFolder;

	/**
	 * Class InternalLibraryFolderPreviewInfo
	 */
	class InternalLibraryFolderPreviewInfo
	{
		public $internalLinkType;

		public $libraryName;
		public $pageName;
		public $windowName;
		public $headerIcon;
		public $showHeaderText;

		public $windowViewType;
		public $column;
		public $linksOnly;

		public $navigationPanel;

		/** @var SearchBar */
		public $searchBar;

		/**
		 * @param $linkSettings InternalLibraryFolderLinkSettings
		 */
		public function __construct($linkSettings)
		{
			$this->internalLinkType = $linkSettings->internalLinkType;

			$this->libraryName = $linkSettings->libraryName;
			$this->pageName = $linkSettings->pageName;
			$this->windowName = $linkSettings->windowName;
			$this->showHeaderText = $linkSettings->showHeaderText;

			$this->column = -1;
			$this->windowViewType = 'columns';
			$this->linksOnly = false;
			$this->searchBar = SearchBar::createEmpty();
			if (!empty($linkSettings->styleSettingsEncoded))
			{
				$styleConfig = new DOMDocument();
				$styleConfig->loadXML(base64_decode($linkSettings->styleSettingsEncoded));
				$xpath = new DomXPath($styleConfig);

				$queryResult = $xpath->query('//Config/Column');
				$this->column = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) - 1 : $this->column;
				$queryResult = $xpath->query('//Config/WindowViewType');
				$this->windowViewType = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $this->windowViewType;
				$queryResult = $xpath->query('//Config/LinksOnly');
				$this->linksOnly = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $this->linksOnly;

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
		}

		/** @return LibraryFolder */
		public function getWindow()
		{
			if (!(isset($this->libraryName) && isset($this->pageName) && isset($this->windowName))) return null;
			$windowRecord = Yii::app()->db->createCommand()
				->select("f.*")
				->from('tbl_folder f')
				->join('tbl_page p', 'p.id = f.id_page')
				->join('tbl_library l', 'l.id = p.id_library')
				->where("f.name='" . $this->windowName . "' and p.name='" . $this->pageName . "' and l.name='" . $this->libraryName . "'")
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
	}
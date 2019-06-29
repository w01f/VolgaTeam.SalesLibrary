<?
	use application\models\wallbin\models\web\LibraryManager as LibraryManager;
	use application\models\wallbin\models\web\LibraryPage as LibraryPage;
	use application\models\wallbin\models\web\LibraryFolder as LibraryFolder;
	use application\models\wallbin\models\web\style\WallbinHeaderStyle;

	/**
	 * Class InternalLibraryFolderPreviewInfo
	 */
	class InternalLibraryFolderPreviewInfo extends InternalLibraryContentPreviewInfo
	{
		public $libraryName;
		public $pageName;
		public $windowName;

		public $windowViewType;
		public $column;
		public $linksOnly;
		public $processResponsiveColumns;

		/** @var  WallbinHeaderStyle */
		public $header;

		/** @var  \Padding */
		public $contentPadding;

		/**
		 * @param $linkSettings InternalLibraryFolderLinkSettings
		 * @param bool $isPhone
		 */
		public function __construct($linkSettings, $isPhone)
		{
			parent::__construct($linkSettings, $isPhone);

			$this->libraryName = str_replace("'", "''", $linkSettings->libraryName);
			$this->pageName = str_replace("'", "''", $linkSettings->pageName);
			$this->windowName = str_replace("'", "''", $linkSettings->windowName);

			$this->column = -1;
			$this->windowViewType = 'columns';
			$this->linksOnly = false;
			$this->processResponsiveColumns = false;
			if (!empty($this->styleSettingsEncoded))
			{
				$styleConfig = new DOMDocument();
				$styleConfig->loadXML($this->styleSettingsEncoded);
				$xpath = new DomXPath($styleConfig);

				$queryResult = $xpath->query('//Config/Column');
				$this->column = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) - 1 : $this->column;
				$queryResult = $xpath->query('//Config/WindowViewType');
				$this->windowViewType = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $this->windowViewType;
				$queryResult = $xpath->query('//Config/LinksOnly');
				$this->linksOnly = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $this->linksOnly;

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

				$queryResult = $xpath->query('//Config/Actions/Action');
				$this->initActions($xpath, $queryResult);
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
	}
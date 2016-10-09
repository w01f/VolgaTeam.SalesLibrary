<?
	use application\models\wallbin\models\web\LibraryManager as LibraryManager;
	use application\models\wallbin\models\web\LibraryPage as LibraryPage;
	use application\models\wallbin\models\web\Library as Library;

	/**
	 * Class LibraryPageShortcut
	 */
	class LibraryPageShortcut extends PageContentShortcut
	{
		public $libraryName;
		public $pageName;
		public $pageViewType;
		public $showLogo;
		public $showText;
		public $showWindowHeaders;
		public $textColor;
		public $backColor;

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

			$this->libraryName = trim($linkConfig->getElementsByTagName("Library")->item(0)->nodeValue);
			$this->pageName = trim($linkConfig->getElementsByTagName("Page")->item(0)->nodeValue);

			$pageTypeTags = $linkConfig->getElementsByTagName("PageViewType");
			$this->pageViewType = $pageTypeTags->length > 0 ? trim($pageTypeTags->item(0)->nodeValue) : 'columns';

			$showLogoTags = $linkConfig->getElementsByTagName("ShowLogo");
			$this->showLogo = $showLogoTags->length > 0 ? filter_var(trim($showLogoTags->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : true;

			$showTextTags = $linkConfig->getElementsByTagName("ShowPageName");
			$this->showText = $showTextTags->length > 0 ? filter_var(trim($showTextTags->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : true;

			$showWindowHeadersTags = $linkConfig->getElementsByTagName("WindowTitleBars");
			$this->showWindowHeaders = $showWindowHeadersTags->length > 0 ? filter_var(trim($showWindowHeadersTags->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : true;

			$textColorTags = $linkConfig->getElementsByTagName("PageNameColor");
			$this->textColor = $textColorTags->length > 0 ? trim($textColorTags->item(0)->nodeValue) : 'inherite';

			$backColorTags = $linkConfig->getElementsByTagName("PageNameBackground");
			$this->backColor = $backColorTags->length > 0 ? trim($backColorTags->item(0)->nodeValue) : 'inherite';

			parent::__construct($linkRecord, $isPhone);

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
				$libraryPage->loadFolders();
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

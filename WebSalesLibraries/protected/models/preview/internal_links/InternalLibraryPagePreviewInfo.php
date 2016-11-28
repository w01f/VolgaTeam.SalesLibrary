<?
	use application\models\wallbin\models\web\LibraryManager as LibraryManager;
	use application\models\wallbin\models\web\LibraryPage as LibraryPage;
	use application\models\wallbin\models\web\Library as Library;

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
			$this->pageViewType = $linkSettings->pageViewType;
			$this->showLogo = $linkSettings->showLogo;
			$this->showText = $linkSettings->showText;
			$this->showWindowHeaders = $linkSettings->showWindowHeaders;
			$this->textColor = $linkSettings->textColor;
			$this->backColor = $linkSettings->backColor;
		}

		/**
		 * @return LibraryPage
		 */
		public function getLibraryPage()
		{
			if (!(isset($this->libraryName) && isset($this->pageName))) return null;
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
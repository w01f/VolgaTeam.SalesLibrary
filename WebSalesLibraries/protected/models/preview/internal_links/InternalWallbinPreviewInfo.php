<?
	use application\models\wallbin\models\web\LibraryManager as LibraryManager;

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
		public $pageSelectorType;
		public $showLogo;

		/**
		 * @param $linkSettings InternalWallbinLinkSettings
		 */
		public function __construct($linkSettings)
		{
			$this->internalLinkType = $linkSettings->internalLinkType;

			$this->libraryName = $linkSettings->libraryName;
			$this->pageName = $linkSettings->pageName;
			$this->headerIcon = $linkSettings->headerIcon;
			$this->showHeaderText = $linkSettings->showHeaderText;
			$this->pageViewType = $linkSettings->pageViewType;
			$this->pageSelectorType = $linkSettings->pageSelectorType;
			$this->showLogo = $linkSettings->showLogo;

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
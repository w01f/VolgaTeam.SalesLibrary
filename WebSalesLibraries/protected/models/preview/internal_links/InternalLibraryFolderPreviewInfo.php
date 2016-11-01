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

		/**
		 * @param $linkSettings InternalLibraryFolderLinkSettings
		 */
		public function __construct($linkSettings)
		{
			$this->internalLinkType = $linkSettings->internalLinkType;

			$this->libraryName = $linkSettings->libraryName;
			$this->pageName = $linkSettings->pageName;
			$this->windowName = $linkSettings->windowName;
			$this->headerIcon = $linkSettings->headerIcon;
			$this->showHeaderText = $linkSettings->showHeaderText;
			$this->windowViewType = $linkSettings->windowViewType;
			$this->column = $linkSettings->column - 1;
			$this->linksOnly = $linkSettings->linksOnly;
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
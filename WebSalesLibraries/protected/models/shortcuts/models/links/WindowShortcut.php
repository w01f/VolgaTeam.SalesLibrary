<?
	use application\models\wallbin\models\web\LibraryManager as LibraryManager;
	use application\models\wallbin\models\web\LibraryPage as LibraryPage;
	use application\models\wallbin\models\web\LibraryFolder as LibraryFolder;

	/**
	 * Class WindowShortcut
	 */
	class WindowShortcut extends PageContentShortcut
	{
		public $column;
		public $windowViewType;
		public $linksOnly;

		/**
		 * @param $linkRecord
		 * @param $isPhone boolean
		 */
		public function __construct($linkRecord, $isPhone)
		{
			parent::__construct($linkRecord, $isPhone);

			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($linkRecord->config);
			$xpath = new DomXPath($linkConfig);

			$columnPositionTags = $xpath->query('//Config/Column');
			$this->column = $columnPositionTags->length > 0 ? intval(trim($columnPositionTags->item(0)->nodeValue)) - 1 : -1;

			$windowViewTags = $linkConfig->getElementsByTagName("WindowViewType");
			$this->windowViewType = $windowViewTags->length > 0 ? trim($windowViewTags->item(0)->nodeValue) : 'columns';

			$linksOnlyTags = $linkConfig->getElementsByTagName("LinksOnly");
			$this->linksOnly = $linksOnlyTags->length > 0 ? filter_var(trim($linksOnlyTags->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;
		}

		/**
		 * @return array
		 */
		public function getPageData()
		{
			$data = parent::getPageData();
			$data['windowViewType'] = $this->windowViewType;
			return $data;
		}

		/**
		 * @return LibraryFolder
		 */
		public function getWindow()
		{
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($this->linkRecord->config);
			$libraryName = trim($linkConfig->getElementsByTagName("Library")->item(0)->nodeValue);
			$pageName = trim($linkConfig->getElementsByTagName("Page")->item(0)->nodeValue);
			$windowName = trim($linkConfig->getElementsByTagName("Window")->item(0)->nodeValue);
			if (!(isset($libraryName) && isset($pageName) && isset($windowName))) return null;
			$windowRecord = Yii::app()->db->createCommand()
				->select("f.*")
				->from('tbl_folder f')
				->join('tbl_page p', 'p.id = f.id_page')
				->join('tbl_library l', 'l.id = p.id_library')
				->where("f.name='" . $windowName . "' and p.name='" . $pageName . "' and l.name='" . $libraryName . "'")
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

		/**
		 * @return string
		 */
		public function getTypeForActivityTracker()
		{
			return 'Window';
		}
	}
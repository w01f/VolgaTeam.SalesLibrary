<?

	/**
	 * Class PageModel
	 */
	abstract class PageModel
	{
		public $id;
		public $idTab;
		public $type;
		public $homeBar;
		public $searchBar;
		public $viewPath;

		/**
		 * @var BaseShortcut[]
		 */
		public $links;

		/**
		 * @param $pageRecord ShortcutsPageRecord
		 */
		public function __construct($pageRecord)
		{
			$this->id = $pageRecord->id;
			$this->idTab = $pageRecord->id_tab;
			$this->homeBar = new HomeBar($pageRecord);
			$this->searchBar = new SearchBar($pageRecord);
			/** @var $linkRecords ShortcutsLinkRecord[] */
			$this->links = array();
			$linkRecords = $pageRecord->getLinks();
			foreach ($linkRecords as $linkRecord)
				$this->links[] = $linkRecord->getModel();
		}


		/**
		 * @return array
		 */
		public abstract function getDisplayParameters();
	}
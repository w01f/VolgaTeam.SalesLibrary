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
		public $ribbonLogoPath;

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
			$baseUrl = Yii::app()->getBaseUrl(true);
			$customRibbonPath = $baseUrl . $pageRecord->source_path . '/rbnlogo.png' . '?' . $pageRecord->id;
			$this->ribbonLogoPath = @getimagesize($customRibbonPath) ? $customRibbonPath : '';
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
<?

	/**
	 * Class BundleShortcut
	 */
	abstract class BundleShortcut extends PageContentShortcut
	{
		public $viewName;
		public $searchBar;
		public $allowSwitchView;

		/**
		 * @var BaseShortcut[]
		 */
		public $links;

		/**
		 * @param $linkRecord
		 * @param $isPhone boolean
		 */
		public function __construct($linkRecord, $isPhone)
		{
			parent::__construct($linkRecord, $isPhone);

			if ($isPhone != true)
				$this->searchBar = new SearchBar($this);
		}

		/**
		 * @return array
		 */
		public function getPageData()
		{
			$data = parent::getPageData();
			$data['displayParameters'] = $this->getDisplayParameters();
			$data['savedBundleModeTagName'] = $this->linkRecord->getUniqueId();
			$data['serviceData'] = $this->getMenuItemData();
			return $data;
		}

		public abstract function getDisplayParameters();

		public function getLinks()
		{
			$this->links = array();
			foreach ($this->linkRecord->subLinks as $linkRecord)
			{
				$shortcut = $linkRecord->getModel($this->isPhone);
				if (isset($shortcut))
					$this->links[] = $shortcut;
			}
		}
	}
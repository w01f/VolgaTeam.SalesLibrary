<?

	/**
	 * Class ContainerShortcut
	 */
	abstract class ContainerShortcut extends PageContentShortcut implements ISearchBarContainer
	{
		public $searchBar;

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
			$this->linkRecord = $linkRecord;
			if ($isPhone != true)
				$this->searchBar = SearchBar::fromShortcut($this);
			else
				$this->searchBar = SearchBar::createEmpty();

			parent::__construct($linkRecord, $isPhone);
		}

		public function getLinks()
		{
			$this->links = array();
			foreach ($this->linkRecord->subLinks as $linkRecord)
			{
				$shortcut = $linkRecord->getModel($this->isPhone);
				if (isset($shortcut) && $shortcut->isAccessGranted)
					$this->links[] = $shortcut;
			}
		}

		/** @return SearchBar */
		public function getSearchBar()
		{
			return $this->searchBar;
		}
	}
<?

	/**
	 * Class ContainerShortcut
	 */
	abstract class ContainerShortcut extends PageContentShortcut implements ISearchBarContainer
	{
		/** @var  SearchBar */
		public $searchBar;

		/**
		 * @var BaseShortcut[]
		 */
		public $links;

		public function loadPageConfig()
		{
			if ($this->isPhone != true)
				$this->searchBar = SearchBar::fromShortcut($this);
			else
				$this->searchBar = SearchBar::createEmpty();

			parent::loadPageConfig();
		}

		public function getLinks()
		{
			$this->links = array();
			foreach ($this->linkRecord->subLinks as $linkRecord)
			{
				$shortcut = $linkRecord->getRegularModel($this->isPhone);
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
<?
	namespace application\models\shortcuts\models\landing_page\regular_markup\common;

	/**
	 * Class SearchBarBlock
	 */
	class SearchBarBlock extends ContentBlock
	{
		/**
		 * @param $parentShortcut \PageContentShortcut
		 * @param $parentBlock BlockContainer
		 */
		public function __construct($parentShortcut, $parentBlock)
		{
			parent::__construct($parentShortcut, $parentBlock);
			$this->type = 'search-bar';
		}

		/** @return \SearchBar */
		public function getSearchBar()
		{
			return $this->parentShortcut->getSearchBar();
		}
	}
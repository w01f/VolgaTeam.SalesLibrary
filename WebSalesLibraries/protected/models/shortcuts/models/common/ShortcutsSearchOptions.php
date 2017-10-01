<?
	use application\models\data_query\conditions\TableQueryConditions;

	/**
	 * Class ShortcutsSearchOptions
	 */
	class ShortcutsSearchOptions
	{

		public $title;
		public $isSearchBar;
		public $openInSamePage;

		public $enableSubSearch;
		public $showSubSearchAll;
		public $showSubSearchSearch;
		public $showSubSearchTemplates;
		public $subSearchDefaultView;

		public $emptyResultLogo;

		public $defaultPageLength;

		/**
		 * @var $searchConditions TableQueryConditions
		 */
		public $conditions;
	}
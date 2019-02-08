<?
	/**
	 * Class LibraryPageViewModel
	 */
	class LibraryPageViewModel
	{
		/**
		 * @var string name
		 * @soap
		 */
		public $id;
		/**
		 * @var string name
		 * @soap
		 */
		public $libraryId;
		/**
		 * @var string name
		 * @soap
		 */
		public $name;
		/**
		 * @var string libraryName
		 * @soap
		 */
		public $libraryName;
		/**
		 * @var string[]
		 * @soap
		 */
		public $assignedGroups;
		/**
		 * @var boolean selected
		 * @soap
		 */
		public $allGroups;
		/**
		 * @var string[]
		 * @soap
		 */
		public $assignedUsers;
		/**
		 * @var boolean selected
		 * @soap
		 */
		public $allUsers;
	}

<?php
	/**
	 * Class GroupEditModel
	 */
	class GroupEditModel
	{
		/**
		 * @var string id
		 * @soap
		 */
		public $id;
		/**
		 * @var string name
		 * @soap
		 */
		public $name;
		/**
		 * @var UserViewModel[]
		 * @soap
		 */
		public $users;
		/**
		 * @var LibraryViewModel[]
		 * @soap
		 */
		public $libraries;
		/**
		 * @var string[]
		 * @soap
		 */
		public $libraryIds;
	}

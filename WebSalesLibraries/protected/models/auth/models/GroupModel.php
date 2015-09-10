<?php

	/**
	 * Class GroupModel
	 */
	class GroupModel
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
		 * @var boolean selected
		 * @soap
		 */
		public $selected;
		/**
		 * @var UserModel[]
		 * @soap
		 */
		public $users;
		/**
		 * @var boolean selected
		 * @soap
		 */
		public $allUsers;
		/**
		 * @var Library[]
		 * @soap
		 */
		public $libraries;
		/**
		 * @var string[]
		 * @soap
		 */
		public $libraryIds;
		/**
		 * @var boolean selected
		 * @soap
		 */
		public $allLibraries;
	}

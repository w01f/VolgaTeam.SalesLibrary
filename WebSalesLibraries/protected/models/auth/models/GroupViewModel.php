<?php
	/**
	 * Class GroupViewModel
	 */
	class GroupViewModel
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
		 * @var string[]
		 * @soap
		 */
		public $assignedUsers;
		/**
		 * @var boolean selected
		 * @soap
		 */
		public $allUsers;
		/**
		 * @var string[]
		 * @soap
		 */
		public $assignedLibraries;
		/**
		 * @var boolean selected
		 * @soap
		 */
		public $allLibraries;
	}

<?php
	/**
	 * Class UserViewModel
	 */
	class UserViewModel
	{
		/**
		 * @var int id
		 * @soap
		 */
		public $id;
		/**
		 * @var string login
		 * @soap
		 */
		public $login;
		/**
		 * @var string password
		 * @soap
		 */
		public $password;
		/**
		 * @var string first Name
		 * @soap
		 */
		public $firstName;
		/**
		 * @var string last Name
		 * @soap
		 */
		public $lastName;
		/**
		 * @var string email
		 * @soap
		 */
		public $email;
		/**
		 * @var string email
		 * @soap
		 */
		public $phone;
		/**
		 * @var int role
		 * @soap
		 */
		public $role;
		/**
		 * @var string
		 * @soap
		 */
		public $dateAdd;
		/**
		 * @var string
		 * @soap
		 */
		public $dateModify;
		/**
		 * @var string
		 * @soap
		 */
		public $dateLastActivity;
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
		public $assignedLibraries;
		/**
		 * @var boolean selected
		 * @soap
		 */
		public $allLibraries;
	}

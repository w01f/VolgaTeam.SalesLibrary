<?php

	/**
	 * Class UserModel
	 */
	class UserModel
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
		 * @var boolean selected
		 * @soap
		 */
		public $selected;
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
		 * @var GroupModel[]
		 * @soap
		 */
		public $groups;
		/**
		 * @var string
		 * @soap
		 */
		public $groupNames;
		/**
		 * @var boolean selected
		 * @soap
		 */
		public $allGroups;
		/**
		 * @var Library[]
		 * @soap
		 */
		public $libraries;
		/**
		 * @var boolean selected
		 * @soap
		 */
		public $allLibraries;
	}

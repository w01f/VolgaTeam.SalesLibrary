<?php

	/**
	 * Class UserActivity
	 */
	class UserActivity
	{
		/**
		 * @var string id
		 * @soap
		 */
		public $id;
		/**
		 * @var string date
		 * @soap
		 */
		public $date;
		/**
		 * @var string date
		 * @soap
		 */
		public $type;
		/**
		 * @var string date
		 * @soap
		 */
		public $subType;
		/**
		 * @var string login
		 * @soap
		 */
		public $login;
		/**
		 * @var string firstName
		 * @soap
		 */
		public $firstName;
		/**
		 * @var string lastName
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
		 * @var string groups
		 * @soap
		 */
		public $groups;
		/**
		 * @var string ip
		 * @soap
		 */
		public $ip;
		/**
		 * @var string os
		 * @soap
		 */
		public $os;
		/**
		 * @var string device
		 * @soap
		 */
		public $device;
		/**
		 * @var string date
		 * @soap
		 */
		public $browser;
		/**
		 * @var ActivityDetail[]
		 * @soap
		 */
		public $details;
	}
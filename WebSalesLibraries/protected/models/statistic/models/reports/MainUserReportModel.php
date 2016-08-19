<?php

	/**
	 * Class MainUserReportModel
	 */
	class MainUserReportModel
	{
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
		 * @var string group
		 * @soap
		 */
		public $group;
		/**
		 * @var int groupUserCount
		 * @soap
		 */
		public $groupUserCount;
		/**
		 * @var int userTotal
		 * @soap
		 */
		public $userTotal;
		/**
		 * @var int groupTotal
		 * @soap
		 */
		public $groupTotal;
		/**
		 * @var int userLogins
		 * @soap
		 */
		public $userLogins;
		/**
		 * @var int groups
		 * @soap
		 */
		public $groupLogins;
		/**
		 * @var int groups
		 * @soap
		 */
		public $userDocs;
		/**
		 * @var int groups
		 * @soap
		 */
		public $groupDocs;
		/**
		 * @var int groups
		 * @soap
		 */
		public $userVideos;
		/**
		 * @var int groups
		 * @soap
		 */
		public $groupVideos;
	}
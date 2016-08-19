<?php

	/**
	 * Class AccessReportModel
	 */
	class AccessReportModel
	{
		/**
		 * @var string name
		 * @soap
		 */
		public $name;
		/**
		 * @var int userCount
		 * @soap
		 */
		public $userCount;
		/**
		 * @var int activeCount
		 * @soap
		 */
		public $activeCount;
		/**
		 * @var string activeNames
		 * @soap
		 */
		public $activeNames;
		/**
		 * @var int inactiveCount
		 * @soap
		 */
		public $inactiveCount;
		/**
		 * @var string inactiveNames
		 * @soap
		 */
		public $inactiveNames;
	}
<?php

	class QuizPassUserReportRecord
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
		 * @var string group
		 * @soap
		 */
		public $quizName;
		/**
		 * @var string group
		 * @soap
		 */
		public $quizPassDate;
		/**
		 * @var int groupUserCount
		 * @soap
		 */
		public $quizTryCount;
	}
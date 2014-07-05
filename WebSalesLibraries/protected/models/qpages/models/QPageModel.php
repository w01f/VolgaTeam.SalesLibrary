<?php

	/**
	 * Class QPageModel
	 */
	class QPageModel
	{
		/**
		 * @var string id
		 * @soap
		 */
		public $id;
		/**
		 * @var string title
		 * @soap
		 */
		public $title;
		/**
		 * @var boolean isEmail
		 * @soap
		 */
		public $isEmail;
		/**
		 * @var string url
		 * @soap
		 */
		public $url;
		/**
		 * @var string createDate
		 * @soap
		 */
		public $createDate;
		/**
		 * @var string expirationDate
		 * @soap
		 */
		public $expirationDate;
		/**
		 * @var string subtitle
		 * @soap
		 */
		public $subtitle;
		/**
		 * @var string header
		 * @soap
		 */
		public $header;
		/**
		 * @var string footer
		 * @soap
		 */
		public $footer;
		/**
		 * @var boolean isRestricted
		 * @soap
		 */
		public $isRestricted;
		/**
		 * @var boolean disableBanners
		 * @soap
		 */
		public $disableBanners;
		/**
		 * @var boolean disableWidgets
		 * @soap
		 */
		public $disableWidgets;
		/**
		 * @var boolean showLinksAsUrl
		 * @soap
		 */
		public $showLinksAsUrl;
		/**
		 * @var boolean recordActivity
		 * @soap
		 */
		public $recordActivity;
		/**
		 * @var string pinCode
		 * @soap
		 */
		public $pinCode;
		/**
		 * @var string activityEmailCopy
		 * @soap
		 */
		public $activityEmailCopy;
		/**
		 * @var string Logo
		 * @soap
		 */
		public $logo;
		/**
		 * @var QPageLinkModel[]
		 * @soap
		 */
		public $links;

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
		 * @var string groups
		 * @soap
		 */
		public $groups;
	}
<?php

	/**
	 * Class LinkConfigProfileModel
	 */
	class LinkConfigProfileModel
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
		 * @var int order
		 * @soap
		 */
		public $order;
		/**
		 * @var LinkConfig config
		 * @soap
		 */
		public $config;
	}
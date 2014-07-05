<?php

	/**
	 * Class Category
	 */
	class Category
	{
		/**
		 * @var string
		 * @soap
		 */
		public $category;
		/**
		 * @var string
		 * @soap
		 */
		public $tag;
		public $selected;

		public function load($categoryRecord)
		{
			$this->category = $categoryRecord->category;
			$this->tag = $categoryRecord->tag;
		}
	}

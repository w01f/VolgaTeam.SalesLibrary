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
		public $description;
		/**
		 * @var string
		 * @soap
		 */
		public $tag;
		public $id;
		public $selected;

		public function load($categoryRecord)
		{
			$this->id = uniqid();
			$this->category = $categoryRecord->category;
			$this->description = $categoryRecord->description;
			$this->tag = $categoryRecord->tag;
		}
	}

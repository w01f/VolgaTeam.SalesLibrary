<?php
	namespace application\models\wallbin\models\web;

	/**
	 * Class Category
	 */
	class Category
	{
		public $category;
		public $description;
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

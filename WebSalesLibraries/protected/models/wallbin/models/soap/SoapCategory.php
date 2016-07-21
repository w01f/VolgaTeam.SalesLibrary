<?php
	use application\models\wallbin\models\web\Category as Category;

	/**
	 * Class SoapCategory
	 */
	class SoapCategory
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

		/**
		 * @param Category $category
		 * @return SoapCategory
		 */
		public static function load($category)
		{
			$soapCategory = new SoapCategory();
			$soapCategory->category = $category->category;
			$soapCategory->description = $category->description;
			$soapCategory->tag = $category->tag;
			return $soapCategory;
		}
	}

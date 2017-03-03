<?php

	/**
	 * Class CategoryRecord
	 * @property int id
	 * @property string group
	 * @property string category
	 * @property string description
	 * @property string tag
	 */
	class CategoryRecord extends CActiveRecord
	{
		/**
		 * @param string $className
		 * @return CActiveRecord
		 */
		public static function model($className = __CLASS__)
		{
			return parent::model($className);
		}

		/**
		 * @return string
		 */
		public function tableName()
		{
			return '{{category}}';
		}

		/**
		 * @param \application\models\wallbin\models\web\Category[] $categories
		 */
		public static function loadData($categories)
		{
			$i = 1;
			foreach ($categories as $category)
			{
				$categoryRecord = new CategoryRecord();
				$categoryRecord->id = $i;
				$categoryRecord->group = $category->group;
				$categoryRecord->category = $category->category;
				$categoryRecord->description = $category->description;
				$categoryRecord->tag = $category->tag;
				$categoryRecord->save();
				$i++;
			}
		}

		/**
		 *
		 */
		public static function clearData()
		{
			self::model()->deleteAll();
		}

		/**
		 * @return array
		 */
		public static function getData()
		{
			return self::model()->findAll();
		}

	}

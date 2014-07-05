<?php

	/**
	 * Class SuperFilterRecord
	 * @property int id
	 * @property mixed value
	 */
	class SuperFilterRecord extends CActiveRecord
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
			return '{{super_filter}}';
		}

		/**
		 * @param $superFilters
		 */
		public static function loadData($superFilters)
		{
			$i = 1;
			foreach ($superFilters as $superFilter)
			{
				$superFilterStorage = new SuperFilterRecord();
				$superFilterStorage->id = $i;
				$superFilterStorage->value = $superFilter;
				$superFilterStorage->save();
				$i++;
			}
		}

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

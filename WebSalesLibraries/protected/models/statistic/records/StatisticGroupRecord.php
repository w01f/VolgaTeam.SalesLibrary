<?php

	/**
	 * Class StatisticGroupRecord
	 * @property string id_activity
	 * @property mixed name
	 */
	class StatisticGroupRecord extends CActiveRecord
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
			return '{{statistic_group}}';
		}
	}

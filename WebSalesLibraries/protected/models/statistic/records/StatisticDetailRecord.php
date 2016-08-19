<?php

	/**
	 * Class StatisticDetailRecord
	 * @property string id_activity
	 * @property string tag
	 * @property mixed data
	 */
	class StatisticDetailRecord extends CActiveRecord
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
			return '{{statistic_detail}}';
		}
	}

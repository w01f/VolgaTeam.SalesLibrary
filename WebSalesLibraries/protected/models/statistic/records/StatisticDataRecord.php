<?php

	/**
	 * Class StatisticDataRecord
	 * @property string id_activity
	 * @property string data
	 */
	class StatisticDataRecord extends CActiveRecord
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
			return '{{statistic_data}}';
		}

		/**
		 * @param $activityId string
		 * @param $activityData array
		 */
		public static function writeActivityData($activityId, $activityData)
		{
			if (isset($activityData) && is_array($activityData))
			{
				$detailRecord = new StatisticDataRecord();
				$detailRecord->id_activity = $activityId;
				$detailRecord->data = CJSON::encode($activityData);
				$detailRecord->save();
			}
		}
	}

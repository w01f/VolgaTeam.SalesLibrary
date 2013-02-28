<?php
	class StatisticActivityStorage extends CActiveRecord
	{
		public static function model($className = __CLASS__)
		{
			return parent::model($className);
		}

		public function tableName()
		{
			return '{{statistic_activity}}';
		}

		public static function WriteActivity($activityType, $activitySubType, $activityData)
		{
			$id = uniqid();
			$activityRecord = new StatisticActivityStorage();
			$activityRecord->id = $id;
			$activityRecord->date_time = date(Yii::app()->params['mysqlDateFormat'], time());
			$activityRecord->type = $activityType;
			$activityRecord->sub_type = $activitySubType;
			$activityRecord->save();

			StatisticUserStorage::WriteUserDetail($id);
			StatisticDetailStorage::WriteActivityDetail($id, $activityData);
		}
	}

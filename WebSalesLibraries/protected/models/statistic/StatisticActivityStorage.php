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

		public function relations()
		{
			return array(
				'userActivity' => array(self::HAS_ONE, 'StatisticUserStorage', 'id_activity'),
				'groupActivities' => array(self::HAS_MANY, 'StatisticGroupStorage', 'id_activity'),
				'activityDetails' => array(self::HAS_MANY, 'StatisticDetailStorage', 'id_activity'),
			);
		}

		public function findByDateRange($startDate, $endDate)
		{
			$criteria = new CDbCriteria;
			$criteria->condition = 'date_time>=:start and date_time<=:end';
			$criteria->params = array(':start' => date(Yii::app()->params['mysqlDateFormat'], strtotime($startDate)), ':end' => date(Yii::app()->params['mysqlDateFormat'], strtotime($endDate)));
			return $this->with('userActivity', 'groupActivities', 'activityDetails')->findAll($criteria);
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

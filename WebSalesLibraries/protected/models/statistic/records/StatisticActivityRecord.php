<?php

	/**
	 * Class StatisticActivityRecord
	 * @property string id
	 * @property mixed date_time
	 * @property string type
	 * @property string sub_type
	 */
	class StatisticActivityRecord extends CActiveRecord
	{
		/**
		 * @param string $className
		 * @return StatisticActivityRecord
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
			return '{{statistic_activity}}';
		}

		/**
		 * @return array
		 */
		public function relations()
		{
			return array(
				'userActivity' => array(self::HAS_ONE, 'StatisticUserRecord', 'id_activity'),
				'groupActivities' => array(self::HAS_MANY, 'StatisticGroupRecord', 'id_activity'),
				'activityDetails' => array(self::HAS_MANY, 'StatisticDetailRecord', 'id_activity'),
			);
		}

		/**
		 * @param $startDate
		 * @param $endDate
		 * @return array
		 */
		public function findByDateRange($startDate, $endDate)
		{
			$criteria = new CDbCriteria;
			$criteria->condition = 'date_time>=:start and date_time<=:end';
			$criteria->params = array(':start' => date(Yii::app()->params['mysqlDateFormat'], strtotime($startDate)), ':end' => date(Yii::app()->params['mysqlDateFormat'], strtotime($endDate)));
			return $this->with('userActivity', 'groupActivities', 'activityDetails')->findAll($criteria);
		}

		/**
		 * @param $activityType string
		 * @param $activitySubType string
		 * @param $activityData array
		 */
		public static function WriteActivity($activityType, $activitySubType, $activityData)
		{
			$id = uniqid();
			$activityRecord = new StatisticActivityRecord();
			$activityRecord->id = $id;
			$activityRecord->date_time = date(Yii::app()->params['mysqlDateFormat'], time());
			$activityRecord->type = $activityType;
			$activityRecord->sub_type = $activitySubType;
			$activityRecord->save();

			StatisticUserRecord::WriteUserDetail($id);
			StatisticDetailRecord::WriteActivityDetail($id, $activityData);
		}
	}

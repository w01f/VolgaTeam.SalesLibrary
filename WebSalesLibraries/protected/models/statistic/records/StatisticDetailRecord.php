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

		/**
		 * @param $activityId string
		 * @param $activityData array
		 */
		public static function WriteActivityDetail($activityId, $activityData)
		{
			if (isset($activityData) && is_array($activityData))
			{
				foreach ($activityData as $tag => $data)
				{
					if (isset($data))
					{
						$detailRecord = new StatisticDetailRecord();
						$detailRecord->id_activity = $activityId;
						$detailRecord->tag = $tag;
						$detailRecord->data = trim(str_replace('<br>', '', str_replace('</b>', '', str_replace('<b>', '', $data))));
						$detailRecord->save();
					}
				}
			}
		}
	}

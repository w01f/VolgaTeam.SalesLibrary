<?php
	class StatisticDetailStorage extends CActiveRecord
	{
		public static function model($className = __CLASS__)
		{
			return parent::model($className);
		}

		public function tableName()
		{
			return '{{statistic_detail}}';
		}

		public static function WriteActivityDetail($activityId, $activityData)
		{
			if (isset($activityData) && is_array($activityData))
			{
				foreach ($activityData as $tag => $data)
				{
					if (isset($data))
					{
						$detailRecord = new StatisticDetailStorage();
						$detailRecord->id_activity = $activityId;
						$detailRecord->tag = $tag;
						$detailRecord->data = trim(str_replace('<br>', '', str_replace('</b>', '', str_replace('<b>', '', $data))));
						$detailRecord->save();
					}
				}
			}
		}
	}

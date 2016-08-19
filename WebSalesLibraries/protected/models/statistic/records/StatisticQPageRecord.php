<?
	/**
	 * Class StatisticQPageRecord
	 * @property string id_activity
	 * @property string id_qpage
	 */
	class StatisticQPageRecord extends CActiveRecord
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
			return '{{statistic_qpage}}';
		}

		/**
		 * @param $activityId string
		 * @param $linkId string
		 */
		public static function writeQPageActivity($activityId, $qpageId)
		{
			if (isset($qpageId))
			{
				$activityRecord = new StatisticQPageRecord();
				$activityRecord->id_activity = $activityId;
				$activityRecord->id_qpage = $qpageId;
				$activityRecord->save();
			}
		}
	}

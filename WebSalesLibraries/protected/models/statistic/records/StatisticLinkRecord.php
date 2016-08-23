<?
	/**
	 * Class StatisticLinkRecord
	 * @property string id_activity
	 * @property string id_link
	 */
	class StatisticLinkRecord extends CActiveRecord
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
			return '{{statistic_link}}';
		}

		/**
		 * @param $activityId string
		 * @param $linkId string
		 */
		public static function writeLinkActivity($activityId, $linkId)
		{
			if (isset($linkId))
			{
				$activityRecord = new StatisticLinkRecord();
				$activityRecord->id_activity = $activityId;
				$activityRecord->id_link = $linkId;
				$activityRecord->save();
			}
		}

		/**
		 * @param array $liveLinkIds
		 */
		public static function clearByLinkIds($liveLinkIds)
		{
			Yii::app()->db->createCommand()->delete('tbl_statistic_link', "id_link not in ('" . implode("','", $liveLinkIds) . "')");
		}
	}

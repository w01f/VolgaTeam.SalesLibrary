<?
	class QPageLinkStorage extends CActiveRecord
	{
		public static function model($className = __CLASS__)
		{
			return parent::model($className);
		}

		public function tableName()
		{
			return '{{qpage_link}}';
		}

		public static function deleteLink($linkInPageId)
		{
			self::model()->deleteByPk($linkInPageId);
		}

		public static function clearByLinkIds($liveLinkIds)
		{
			Yii::app()->db->createCommand()->delete('tbl_qpage_link', "id_link not in ('" . implode("','", $liveLinkIds) . "')");
		}
	}
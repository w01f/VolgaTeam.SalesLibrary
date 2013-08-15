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

		public static function getMaxLinkIndex($pageId)
		{
			return Yii::app()->db->createCommand()
				->select('max(list_order)')
				->from('tbl_qpage_link')
				->where("id_page='" . $pageId . "'")
				->queryScalar();
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
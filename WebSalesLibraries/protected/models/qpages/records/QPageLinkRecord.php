<?

	/**
	 * Class QPageLinkRecord
	 * @property mixed id
	 * @property mixed id_link
	 * @property mixed id_page
	 * @property mixed list_order
	 */
	class QPageLinkRecord extends CActiveRecord
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
			return '{{qpage_link}}';
		}

		/**
		 * @param $pageId
		 * @return mixed
		 */
		public static function getMaxLinkIndex($pageId)
		{
			return Yii::app()->db->createCommand()
				->select('max(list_order)')
				->from('tbl_qpage_link')
				->where("id_page='" . $pageId . "'")
				->queryScalar();
		}

		/**
		 * @param $linkInPageId
		 */
		public static function deleteLink($linkInPageId)
		{
			self::model()->deleteByPk($linkInPageId);
		}

		/**
		 * @param $liveLinkIds
		 */
		public static function clearByLinkIds($liveLinkIds)
		{
			Yii::app()->db->createCommand()->delete('tbl_qpage_link', "id_link not in ('" . implode("','", $liveLinkIds) . "')");
		}
	}
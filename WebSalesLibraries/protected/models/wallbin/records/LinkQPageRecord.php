<?

	/**
	 * Class LinkQPageRecord
	 * @property int id
	 * @property string id_link
	 * @property string id_qpage
	 */
	class LinkQPageRecord extends CActiveRecord
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
			return '{{link_qpage}}';
		}

		/**
		 * @param string $idLink
		 * @param string $idQPage
		 */
		public static function addLinkQPageRelation($idLink, $idQPage)
		{
			$linkQPageRecord = self::model()->find('id_link=? and id_qpage=?', array($idLink, $idQPage));
			if (!isset($linkQPageRecord))
			{
				$linkQPageRecord = new LinkQPageRecord();
				$linkQPageRecord->id_link = $idLink;
				$linkQPageRecord->id_qpage = $idQPage;
				$linkQPageRecord->save();
			}
		}

		/**
		 * @param array $liveLinkIds
		 */
		public static function clearByLinkIds($liveLinkIds)
		{
			Yii::app()->db->createCommand()->delete('tbl_link_qpage', "id_link not in ('" . implode("','", $liveLinkIds) . "')");
		}
	}
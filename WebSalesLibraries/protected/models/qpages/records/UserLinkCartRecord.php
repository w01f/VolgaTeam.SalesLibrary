<?

	/**
	 * Class UserLinkCartRecord
	 * @property mixed id
	 * @property mixed id_user
	 * @property mixed id_link
	 */
	class UserLinkCartRecord extends CActiveRecord
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
			return '{{user_link_cart}}';
		}

		/**
		 * @param $userId
		 * @return array|null
		 */
		public static function getLinksByUser($userId)
		{
			$linkRecords = Yii::app()->db->createCommand()
				->select("concat('cart',lk.id,'---link',l.id) as id, l.id_library, l.name, l.file_name, l.format, l.type")
				->from('tbl_link l')
				->join('tbl_user_link_cart lk', 'lk.id_link = l.id')
				->where("lk.id_user=" . $userId)
				->queryAll();
			return LinkRecord::getLinksGrid($linkRecords);
		}

		/**
		 * @param $userId
		 * @param $linkId
		 */
		public static function addLink($userId, $linkId)
		{
			$linkRecord = new UserLinkCartRecord();
			$linkRecord->id = uniqid();
			$linkRecord->id_user = $userId;
			$linkRecord->id_link = $linkId;
			$linkRecord->save();
		}

		/**
		 * @param $linkInCartId
		 */
		public static function deleteLink($linkInCartId)
		{
			self::model()->deleteByPk($linkInCartId);
		}

		/**
		 * @param $ownerId
		 */
		public static function deleteLinksByUser($ownerId)
		{
			self::model()->deleteAll('id_user=?', array($ownerId));
		}

		/**
		 * @param $liveLinkIds
		 */
		public static function clearByLinkIds($liveLinkIds)
		{
			Yii::app()->db->createCommand()->delete('tbl_user_link_cart', "id_link not in ('" . implode("','", $liveLinkIds) . "')");
		}
	}
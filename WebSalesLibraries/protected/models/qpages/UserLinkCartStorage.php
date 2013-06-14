<?
	class UserLinkCartStorage extends CActiveRecord
	{
		public static function model($className = __CLASS__)
		{
			return parent::model($className);
		}

		public function tableName()
		{
			return '{{user_link_cart}}';
		}

		public static function getLinksByUser($userId)
		{
			$linkRecords = Yii::app()->db->createCommand()
				->select("concat('cart',lk.id,'---link',l.id) as id, l.id_library, l.name, l.file_name, l.format, l.type")
				->from('tbl_link l')
				->join('tbl_user_link_cart lk', 'lk.id_link = l.id')
				->where("lk.id_user=" . $userId)
				->queryAll();
			return LinkStorage::getLinksGrid($linkRecords);
		}

		public static function addLink($userId, $linkId)
		{
			$linkRecord = new UserLinkCartStorage();
			$linkRecord->id = uniqid();
			$linkRecord->id_user = $userId;
			$linkRecord->id_link = $linkId;
			$linkRecord->save();
		}

		public static function deleteLink($linkInCartId)
		{
			self::model()->deleteByPk($linkInCartId);
		}

		public static function deleteLinksByUser($ownerId)
		{
			self::model()->deleteAll('id_user=?', array($ownerId));
		}

		public static function clearByLinkIds($liveLinkIds)
		{
			Yii::app()->db->createCommand()->delete('tbl_user_link_cart', "id_link not in ('" . implode("','", $liveLinkIds) . "')");
		}
	}
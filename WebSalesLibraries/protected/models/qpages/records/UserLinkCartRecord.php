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
		 * @param array $columnSettings
		 * @return array
		 */
		public static function getLinksByUser($userId, $columnSettings)
		{
			$querySettings = QuerySettings::prepareQuery(
				array(
					QuerySettings::SettingsTagFrom => 'tbl_user_link_cart lk',
					QuerySettings::SettingsTagQueryFields => array(
						'linkInCartId' => 'lk.id as linkInCartId',
					),
					QuerySettings::SettingsTagInnerJoin => array('tbl_link link' => 'lk.id_link=link.id'),
					QuerySettings::SettingsTagWhere => array(
						sprintf("lk.id_user=%s", $userId)
					),
					QuerySettings::SettingsTagGroup => array('lk.id'),
					QuerySettings::SettingsTagColumns => $columnSettings
				));
			/** @var CDbCommand $dbCommand */
			$dbCommand = DataTableHelper::buildQuery($querySettings);
			$dbCommand = $dbCommand->order('link.name');
			$linkRecords = $dbCommand->queryAll();
			$links = DataTableHelper::formatExtendedData($linkRecords, $columnSettings, array('linkInCartId'));
			return $links;
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
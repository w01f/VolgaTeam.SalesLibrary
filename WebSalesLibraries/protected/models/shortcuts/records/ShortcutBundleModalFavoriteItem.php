<?

	/**
	 * Class ShortcutBundleModalFavoriteItem
	 * @property string id
	 * @property string id_item
	 * @property mixed id_owner
	 * @property string type
	 * @property string config
	 */
	class ShortcutBundleModalFavoriteItem extends CActiveRecord
	{
		public static function model($className = __CLASS__)
		{
			return parent::model($className);
		}

		/**
		 * @return string
		 */
		public function tableName()
		{
			return '{{shortcut_bundle_modal_favorite_item}}';
		}

		/**
		 * @return \application\models\shortcuts\models\bundle_modal_dialog\BaseItem[]
		 */
		public function getModels()
		{
			$models = array();
			$criteria = new CDbCriteria();
			$criteria->addCondition("id_owner=" . $userId = UserIdentity::getCurrentUserId());
			$itemRecords = $this->findAll($criteria);
			foreach ($itemRecords as $item)
				$models[] = \application\models\shortcuts\models\bundle_modal_dialog\BaseItem::fromJson($item->type, $item->config);
			return $models;
		}

		public function addItem($itemId, $itemType, $encodedContent)
		{
			$criteria = new CDbCriteria();
			$criteria->addCondition("id_item='" . $itemId . "'");
			/** @var $existingRecords SalesRequestFileRecord[] */
			$existingRecords = self::model()->findAll($criteria);
			if (count($existingRecords) > 0)
				return;

			$itemRecord = new self();

			$itemRecord->id = uniqid();
			$itemRecord->id_item = $itemId;
			$itemRecord->id_owner = UserIdentity::getCurrentUserId();
			$itemRecord->type = $itemType;
			$itemRecord->config = $encodedContent;

			$itemRecord->save();
		}

		public function deleteItem($itemId)
		{
			$criteria = new CDbCriteria();
			$criteria->addCondition("id_item='" . $itemId . "'");
			/** @var $existingRecords SalesRequestFileRecord[] */
			self::model()->deleteAll($criteria);
		}

		public static function clearData()
		{
			self::model()->deleteAll();
		}
	}
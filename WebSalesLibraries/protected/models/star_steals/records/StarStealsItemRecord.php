<?

	/**
	 * Class StarStealsItemRecord
	 * @property mixed id
	 * @property mixed id_owner
	 * @property mixed list_order
	 * @property string title
	 * @property mixed create_date
	 * @property mixed content
	 */
	class StarStealsItemRecord extends CActiveRecord
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
			return '{{star_steals_item}}';
		}

		/**
		 * @param $ownerId
		 * @return \application\models\star_steals\models\ItemListModel[]
		 */
		public function getListItems($ownerId)
		{
			$models = array();
			$itemRecords = $this->findAll('id_owner=? order by list_order', array($ownerId));
			foreach ($itemRecords as $itemRecord)
				$models[] = \application\models\star_steals\models\ItemListModel::fromRecord($itemRecord);
			return $models;
		}

		/**
		 * @param $itemId
		 * @return \application\models\star_steals\models\ItemEditModel
		 */
		public function getEditModel($itemId)
		{
			$itemRecord = $this->findByPk($itemId);
			if (isset($itemRecord))
				return \application\models\star_steals\models\ItemEditModel::fromRecord($itemRecord);
			return null;
		}

		/**
		 * @param $ownerId int
		 * @param $title string
		 * @return string
		 */
		public static function addItem($ownerId, $title)
		{
			$itemRecord = new self();

			$itemRecord->id = uniqid();
			$itemRecord->id_owner = $ownerId;
			$itemRecord->list_order = self::getMaxItemIndex() + 1;
			$itemRecord->title = $title;
			$itemRecord->create_date = date(Yii::app()->params['mysqlDateTimeFormat']);

			$itemRecord->save();

			return $itemRecord->id;
		}

		/**
		 * @param $ownerId
		 * @param $title
		 * @param $sourceItemId
		 * @return string
		 */
		public static function cloneItem($ownerId, $title, $sourceItemId)
		{
			/** @var $sourceItemRecord StarStealsItemRecord */
			$sourceItemRecord = self::model()->findByPk($sourceItemId);
			if (isset($sourceItemRecord))
			{
				$itemRecord = new self();
				$itemRecord->id = uniqid();
				$itemRecord->id_owner = $ownerId;
				$itemRecord->list_order = self::getMaxItemIndex() + 1;
				$itemRecord->title = $title;
				$itemRecord->create_date = date(Yii::app()->params['mysqlDateTimeFormat']);
				$itemRecord->content = $sourceItemRecord->content;
				$itemRecord->save();
				return $itemRecord->id;
			}
			return null;
		}

		/**
		 * @param $itemId
		 * @param $title
		 * @param $content \application\models\star_steals\models\Content
		 */
		public static function saveItem($itemId, $title, $content)
		{
			/** @var $itemRecord StarStealsItemRecord */
			$itemRecord = self::model()->findByPk($itemId);
			if (isset($itemRecord))
			{
				$itemRecord->title = $title;
				if (isset($content))
					$itemRecord->content = CJSON::encode($content);
				$itemRecord->save();
			}
		}

		/**
		 * @param $itemId
		 */
		public static function deleteItem($itemId)
		{
			/** @var $itemRecord StarStealsItemRecord */
			$itemRecord = self::model()->findByPk($itemId);
			$ownerId = $itemRecord->id_owner;

			self::model()->deleteByPk($itemId);
			//StatisticQPageRecord::model()->deleteAll('id_qpage=?', array($itemId));

			self::rebuildItemList($ownerId, -1);
		}

		/**
		 * @param $ownerId
		 */
		public static function deleteItemsByOwner($ownerId)
		{
			/** @var StarStealsItemRecord $itemRecords */
			$itemRecords = self::model()->findAll('id_owner=?', array($ownerId));
			foreach ($itemRecords as $itemRecord)
				self::deleteItem($itemRecord->id);
			self::rebuildItemList($ownerId, -1);
		}

		/**
		 * @param $userId
		 * @param $itemId
		 * @param $order
		 */
		public static function setItemOrder($userId, $itemId, $order)
		{
			self::rebuildItemList($userId, $order);
			/** @var $itemRecord StarStealsItemRecord */
			$itemRecord = self::model()->findByPk($itemId);
			$itemRecord->list_order = $order;
			$itemRecord->save();
		}

		/**
		 * @param $userId
		 * @param $shiftIndex
		 */
		private static function rebuildItemList($userId, $shiftIndex)
		{
			$i = 0;
			foreach (self::model()->findAll('id_owner=? order by list_order', array($userId)) as $itemRecord)
			{
				if ($i == $shiftIndex)
					$i++;
				/** @var $itemRecord StarStealsItemRecord */
				$itemRecord->list_order = $i;
				$itemRecord->save();
				$i++;
			}
		}

		/**
		 * @return int
		 */
		private static function getMaxItemIndex()
		{
			$userId = UserIdentity::getCurrentUserId();
			if ($userId != -1)
			{
				return Yii::app()->db->createCommand()
					->select('max(list_order)')
					->from('tbl_qpage')
					->where("id_owner='" . $userId . "'")
					->queryScalar();
			}
			else
				return -1;
		}
	}
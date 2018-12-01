<?

	/**
	 * Class SalesRequestItemRecord
	 * @property mixed id
	 * @property mixed id_owner
	 * @property string title
	 * @property string status
	 * @property string assigned_to
	 * @property mixed create_date
	 * @property mixed date_submit
	 * @property mixed date_needed
	 * @property mixed date_completed
	 * @property mixed content
	 */
	class SalesRequestItemRecord extends CActiveRecord
	{
		public const FiledNameIdOwner = "id_owner";
		public const FiledNameStatus = "status";
		public const FiledNameDateCompleted = "date_completed";

		public const ListTypeOwn = "own-items";
		public const ListTypeAll = "all-items";
		public const ListTypeArchive = "archive-items";

		public static function model($className = __CLASS__)
		{
			return parent::model($className);
		}

		/**
		 * @return string
		 */
		public function tableName()
		{
			return '{{sales_request_item}}';
		}

		/**
		 * @param $listType
		 * @param $shortcutId
		 * @param $statusFilter
		 * @return \application\models\sales_requests\models\ItemListModel[]
		 */
		public function getListItems($listType, $shortcutId, $statusFilter = null)
		{
			$models = array();

			$criteria = new CDbCriteria();

			switch ($listType)
			{
				case self::ListTypeOwn:
					$userId = UserIdentity::getCurrentUserId();
					$criteria->addCondition(self::FiledNameIdOwner . "=" . $userId);
					break;
				case self::ListTypeAll:
					if (isset($shortcutId))
					{
						/** @var $linkRecord ShortcutLinkRecord */
						$linkRecord = ShortcutLinkRecord::model()->findByPk($shortcutId);
						/**@var $shortcut SalesRequestsShortcut */
						$shortcut = $linkRecord->getRegularModel(false);
						$shortcut->loadPageConfig();
						if ($shortcut->archiveSettings->isConfigured())
							$criteria->addCondition(self::FiledNameDateCompleted . " is null or (" . self::FiledNameDateCompleted . " is not null and date_add(date_add(t.date_completed, interval " . $shortcut->archiveSettings->archiveAfterDays . " day), interval " . $shortcut->archiveSettings->archiveAfterHours . " hour) > now())");
					}
					break;
				case self::ListTypeArchive:
					if (isset($shortcutId))
					{
						/** @var $linkRecord ShortcutLinkRecord */
						$linkRecord = ShortcutLinkRecord::model()->findByPk($shortcutId);
						/**@var $shortcut SalesRequestsShortcut */
						$shortcut = $linkRecord->getRegularModel(false);
						$shortcut->loadPageConfig();
						if ($shortcut->archiveSettings->isConfigured())
							$criteria->addCondition(self::FiledNameDateCompleted . " is not null and date_add(date_add(t.date_completed, interval " . $shortcut->archiveSettings->archiveAfterDays . " day), interval " . $shortcut->archiveSettings->archiveAfterHours . " hour) < now()");
						else
							$criteria->addCondition("1=2");
					}
					else
						$criteria->addCondition("1=2");
					break;
			}


			if (!empty($statusFilter) && $statusFilter !== \application\models\sales_requests\models\Dictionaries::StatusItemAll)
				$criteria->addCondition(self::FiledNameStatus . "='" . $statusFilter . "'");

			$itemRecords = $this->findAll($criteria);
			foreach ($itemRecords as $itemRecord)
				$models[] = \application\models\sales_requests\models\ItemListModel::fromRecord($itemRecord);

			return $models;
		}

		/**
		 * @param $itemId
		 * @return \application\models\sales_requests\models\ItemEditModel
		 */
		public function getEditModel($itemId)
		{
			$itemRecord = $this->findByPk($itemId);
			if (isset($itemRecord))
				return \application\models\sales_requests\models\ItemEditModel::fromRecord($itemRecord);
			return null;
		}

		/**
		 * @param $title
		 * @param $status
		 * @param $assignedTo
		 * @param $dateNeeded
		 * @param $content \application\models\sales_requests\models\Content
		 */
		public function saveItem($title, $status, $assignedTo, $dateNeeded, $dateCompleted, $content)
		{
			$this->title = $title;
			$this->status = $status;
			$this->assigned_to = $assignedTo;
			$this->date_needed = date(Yii::app()->params['mysqlDateTimeFormat'], strtotime($dateNeeded));
			if (!empty($dateCompleted))
				$this->date_completed = date(Yii::app()->params['mysqlDateTimeFormat'], strtotime($dateCompleted));

			if (!isset($itemRecord->date_submit) &&
				$status === \application\models\sales_requests\models\Dictionaries::StatusItemSubmitted)
			{
				$this->date_submit = date(Yii::app()->params['mysqlDateTimeFormat']);
				$content->submittedByUserId = UserIdentity::getCurrentUserId();
			}

			$this->content = CJSON::encode($content);
			$this->save();
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
			$itemRecord->title = $title;
			$itemRecord->status = \application\models\sales_requests\models\Dictionaries::StatusItemIncompleted;
			$itemRecord->date_needed = date(Yii::app()->params['mysqlDateTimeFormat']);
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
			/** @var $sourceItemRecord SalesRequestItemRecord */
			$sourceItemRecord = self::model()->findByPk($sourceItemId);
			if (isset($sourceItemRecord))
			{
				$itemRecord = new self();
				$itemRecord->id = uniqid();
				$itemRecord->id_owner = $ownerId;
				$itemRecord->title = $title;
				$itemRecord->status = $sourceItemRecord->status;
				$itemRecord->assigned_to = $sourceItemRecord->assigned_to;
				$itemRecord->create_date = date(Yii::app()->params['mysqlDateTimeFormat']);
				$itemRecord->date_submit = $sourceItemRecord->date_submit;
				$itemRecord->date_needed = $sourceItemRecord->date_needed;
				$itemRecord->date_completed = $sourceItemRecord->date_completed;
				$itemRecord->content = $sourceItemRecord->content;
				$itemRecord->save();

				SalesRequestFileRecord::cloneFiles($itemRecord->id, $sourceItemId);

				return $itemRecord->id;
			}
			return null;
		}

		/**
		 * @param $itemId
		 */
		public static function deleteItem($itemId)
		{
			self::model()->deleteByPk($itemId);
			SalesRequestFileRecord::deleteFilesByItem($itemId);
		}

		/**
		 * @param $ownerId
		 */
		public static function deleteItemsByOwner($ownerId)
		{
			/** @var SalesRequestItemRecord $itemRecords */
			$itemRecords = self::model()->findAll('id_owner=?', array($ownerId));
			foreach ($itemRecords as $itemRecord)
				self::deleteItem($itemRecord->id);
		}
	}
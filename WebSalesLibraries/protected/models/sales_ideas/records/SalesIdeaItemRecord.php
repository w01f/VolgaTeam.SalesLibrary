<?

	/**
	 * Class SalesIdeaItemRecord
	 * @property mixed id
	 * @property mixed id_owner
	 * @property string title
	 * @property string advertiser
	 * @property mixed revenue
	 * @property mixed storage_path
	 * @property mixed create_date
	 * @property mixed date_submit
	 * @property mixed content
	 */
	class SalesIdeaItemRecord extends CActiveRecord
	{
		public const FiledNameIdOwner = "id_owner";
		public const FiledNameDateSubmit = "date_submit";

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
			return '{{sales_idea_item}}';
		}

		/**
		 * @param $listType
		 * @param $shortcutId
		 * @return \application\models\sales_ideas\models\ItemListModel[]
		 */
		public function getListItems($listType, $shortcutId)
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
						/**@var $shortcut SalesIdeasShortcut */
						$shortcut = $linkRecord->getRegularModel(false);
						$shortcut->loadPageConfig();
						if ($shortcut->archiveSettings->isConfigured())
							$criteria->addCondition(self::FiledNameDateSubmit . " is null or (" . self::FiledNameDateSubmit . " is not null and date_add(date_add(t.date_submit, interval " . $shortcut->archiveSettings->archiveAfterDays . " day), interval " . $shortcut->archiveSettings->archiveAfterHours . " hour) > now())");
					}
					break;
				case self::ListTypeArchive:
					if (isset($shortcutId))
					{
						/** @var $linkRecord ShortcutLinkRecord */
						$linkRecord = ShortcutLinkRecord::model()->findByPk($shortcutId);
						/**@var $shortcut SalesIdeasShortcut */
						$shortcut = $linkRecord->getRegularModel(false);
						$shortcut->loadPageConfig();
						if ($shortcut->archiveSettings->isConfigured())
							$criteria->addCondition(self::FiledNameDateSubmit . " is not null and date_add(date_add(t.date_submit, interval " . $shortcut->archiveSettings->archiveAfterDays . " day), interval " . $shortcut->archiveSettings->archiveAfterHours . " hour) < now()");
						else
							$criteria->addCondition("1=2");
					}
					else
						$criteria->addCondition("1=2");
					break;
			}


			$itemRecords = $this->findAll($criteria);
			foreach ($itemRecords as $itemRecord)
				$models[] = \application\models\sales_ideas\models\ItemListModel::fromRecord($itemRecord);

			return $models;
		}

		/**
		 * @param $shortcutId
		 * @return \application\models\sales_ideas\models\ItemListInfo
		 */
		public function getItemListInfo($shortcutId)
		{
			$itemListInfo = new \application\models\sales_ideas\models\ItemListInfo();

			$listTypes = array(self::ListTypeOwn, self::ListTypeAll, self::ListTypeArchive);
			foreach ($listTypes as $listType)
			{
				$criteria = new CDbCriteria();
				switch ($listType)
				{
					case self::ListTypeOwn:
						$userId = UserIdentity::getCurrentUserId();
						$criteria->addCondition(self::FiledNameIdOwner . "=" . $userId);
						$itemListInfo->ownItemsCount = $this->count($criteria);
						break;
					case self::ListTypeAll:
						if (isset($shortcutId))
						{
							/** @var $linkRecord ShortcutLinkRecord */
							$linkRecord = ShortcutLinkRecord::model()->findByPk($shortcutId);
							/**@var $shortcut SalesIdeasShortcut */
							$shortcut = $linkRecord->getRegularModel(false);
							$shortcut->loadPageConfig();
							if ($shortcut->archiveSettings->isConfigured())
								$criteria->addCondition(self::FiledNameDateSubmit . " is null or (" . self::FiledNameDateSubmit . " is not null and date_add(date_add(t.date_submit, interval " . $shortcut->archiveSettings->archiveAfterDays . " day), interval " . $shortcut->archiveSettings->archiveAfterHours . " hour) > now())");
						}
						$itemListInfo->allItemsCount = $this->count($criteria);
						break;
					case self::ListTypeArchive:
						if (isset($shortcutId))
						{
							/** @var $linkRecord ShortcutLinkRecord */
							$linkRecord = ShortcutLinkRecord::model()->findByPk($shortcutId);
							/**@var $shortcut SalesIdeasShortcut */
							$shortcut = $linkRecord->getRegularModel(false);
							$shortcut->loadPageConfig();
							if ($shortcut->archiveSettings->isConfigured())
								$criteria->addCondition(self::FiledNameDateSubmit . " is not null and date_add(date_add(t.date_submit, interval " . $shortcut->archiveSettings->archiveAfterDays . " day), interval " . $shortcut->archiveSettings->archiveAfterHours . " hour) < now()");
							else
								$criteria->addCondition("1=2");
						}
						else
							$criteria->addCondition("1=2");
						$itemListInfo->archiveItemsCount = $this->count($criteria);
						break;
				}
			}

			return $itemListInfo;
		}

		/**
		 * @param $itemId
		 * @return \application\models\sales_ideas\models\ItemEditModel
		 */
		public function getEditModel($itemId)
		{
			$itemRecord = $this->findByPk($itemId);
			if (isset($itemRecord))
				return \application\models\sales_ideas\models\ItemEditModel::fromRecord($itemRecord);
			return null;
		}

		/**
		 * @param $title
		 * @param $advertiser
		 * @param $revenue
		 * @param $isSubmit boolean
		 * @param $content \application\models\sales_ideas\models\Content
		 */
		public function saveItem($title, $advertiser, $revenue, $isSubmit, $content)
		{
			$this->title = $title;
			$this->advertiser = $advertiser;
			$this->revenue = $revenue;

			if (!isset($itemRecord->date_submit) && $isSubmit == true)
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
		 * @param $shortcutId string
		 * @return string
		 */
		public static function addItem($ownerId, $title, $shortcutId)
		{
			$itemRecord = new self();

			$itemRecord->id = uniqid();
			$itemRecord->id_owner = $ownerId;
			$itemRecord->title = $title;
			$itemRecord->create_date = date(Yii::app()->params['mysqlDateTimeFormat']);

			if (!empty($shortcutId))
			{
				/** @var $linkRecord ShortcutLinkRecord */
				$linkRecord = ShortcutLinkRecord::model()->findByPk($shortcutId);
				/**@var $shortcut SalesIdeasShortcut */
				$shortcut = $linkRecord->getRegularModel(false);
				$shortcut->loadPageConfig();
				if (!empty($shortcut->fileStorageRootPath))
				{
					$storageRelativePath = $shortcut->fileStorageRootPath . '/' . $itemRecord->id;
					$itemRecord->storage_path = $storageRelativePath;
					$storageAbsolutePath = SalesIdeaFileRecord::getStoragePath($storageRelativePath);
					if (!file_exists($storageAbsolutePath))
						mkdir($storageAbsolutePath, 0777, true);
				}
			}

			$itemRecord->save();

			return $itemRecord->id;
		}

		/**
		 * @param $ownerId
		 * @param $title
		 * @param $sourceItemId
		 * @param $shortcutId string
		 * @return string
		 */
		public static function cloneItem($ownerId, $title, $sourceItemId, $shortcutId)
		{
			/** @var $sourceItemRecord SalesIdeaItemRecord */
			$sourceItemRecord = self::model()->findByPk($sourceItemId);
			if (isset($sourceItemRecord))
			{
				$itemRecord = new self();
				$itemRecord->id = uniqid();
				$itemRecord->id_owner = $ownerId;
				$itemRecord->title = $title;
				$itemRecord->advertiser = $sourceItemRecord->advertiser;
				$itemRecord->revenue = $sourceItemRecord->revenue;
				$itemRecord->create_date = date(Yii::app()->params['mysqlDateTimeFormat']);
				$itemRecord->date_submit = $sourceItemRecord->date_submit;
				$itemRecord->content = $sourceItemRecord->content;

				if (!empty($shortcutId))
				{
					/** @var $linkRecord ShortcutLinkRecord */
					$linkRecord = ShortcutLinkRecord::model()->findByPk($shortcutId);
					/**@var $shortcut SalesIdeasShortcut */
					$shortcut = $linkRecord->getRegularModel(false);
					$shortcut->loadPageConfig();
					if (!empty($shortcut->fileStorageRootPath))
					{
						$storageRelativePath = $shortcut->fileStorageRootPath . '/' . $itemRecord->id;
						$itemRecord->storage_path = $storageRelativePath;
						$storageAbsolutePath = SalesIdeaFileRecord::getStoragePath($storageRelativePath);
						if (!file_exists($storageAbsolutePath))
							mkdir($storageAbsolutePath, 0777, true);
					}
				}

				$itemRecord->save();

				SalesIdeaFileRecord::cloneFiles($itemRecord->id, $sourceItemId);

				return $itemRecord->id;
			}
			return null;
		}

		/**
		 * @param $itemId
		 */
		public static function deleteItem($itemId)
		{
			/** @var SalesIdeaItemRecord $itemRecord */
			$itemRecord = self::model()->findByPk($itemId);
			SalesIdeaFileRecord::deleteFilesByItem($itemId);
			if (!empty($itemRecord->storage_path))
			{
				$storagePath = SalesIdeaFileRecord::getStoragePath($itemRecord->storage_path);
				if (file_exists($storagePath))
					try
					{
						array_map('unlink', glob("$storagePath/*.*"));
						rmdir($storagePath);
					}
					catch (Exception $e)
					{
					}
			}
			self::model()->deleteByPk($itemId);
		}

		/**
		 * @param $ownerId
		 */
		public static function deleteItemsByOwner($ownerId)
		{
			/** @var SalesIdeaItemRecord $itemRecords */
			$itemRecords = self::model()->findAll('id_owner=?', array($ownerId));
			foreach ($itemRecords as $itemRecord)
				self::deleteItem($itemRecord->id);
		}
	}
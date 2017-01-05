<?
	use application\models\services_data\cadmin\models\versions_management\ChangeSet;

	/**
	 * Class LibraryRecord
	 * @property mixed id
	 * @property string id_group
	 * @property string name
	 * @property string path
	 * @property int order
	 * @property string settings
	 * @property string last_update
	 */
	class LibraryRecord extends CActiveRecord
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
			return '{{library}}';
		}

		/**
		 * @param $libraryName string
		 * @return LibraryRecord
		 */
		public static function createEmptyLibrary($libraryName)
		{
			$libraryId = Utils::getGUID();
			$createDate = date(Yii::app()->params['mysqlDateFormat']);

			/** @var $libraryRecord LibraryRecord */
			$libraryRecord = new LibraryRecord();
			$libraryRecord->id = $libraryId;
			$libraryRecord->name = $libraryName;
			$libraryRecord->last_update = $createDate;
			$libraryRecord->save();

			/** @var $pageRecord LibraryPageRecord */
			$pageRecord = new LibraryPageRecord();
			$pageRecord->id = Utils::getGUID();
			$pageRecord->id_library = $libraryId;
			$pageRecord->name = 'Home';
			$pageRecord->order = 0;
			$pageRecord->has_columns = false;
			$pageRecord->date_modify = $createDate;
			$pageRecord->save();

			return $libraryRecord;
		}

		/**
		 * @param array $library
		 * @param $sourceDate
		 * @param $libraryRootPath
		 * @return bool
		 */
		public static function updateDataFromSoap($library, $sourceDate, $libraryRootPath)
		{
			$needToUpdate = false;
			$needToCreate = false;
			/** @var $libraryRecord LibraryRecord */
			$libraryRecord = LibraryRecord::model()->findByPk($library['id']);
			if ($libraryRecord !== null)
			{
				if ($libraryRecord->last_update != null)
					if ($libraryRecord->last_update != date(Yii::app()->params['mysqlDateFormat'], $sourceDate))
						$needToUpdate = true;
			}
			else
			{
				$libraryRecord = new LibraryRecord();
				$needToCreate = true;
			}

			if (isset($library['id']) && ($needToCreate || $needToUpdate))
			{
				Yii::app()->cacheDB->flush();
				self::clearData($library['id']);

				$libraryRecord->id = $library['id'];
				$libraryRecord->name = $library['name'];
				$libraryRecord->path = str_replace(\application\models\wallbin\models\web\LibraryManager::getLibrariesRootPath() . DIRECTORY_SEPARATOR, '', $libraryRootPath);
				$libraryRecord->last_update = date(Yii::app()->params['mysqlDateFormat'], $sourceDate);
				$libraryRecord->save();

				foreach ($library['autoWidgets'] as $autoWidget)
					AutoWidgetRecord::updateData($autoWidget);

				foreach ($library['previewContainers'] as $previewContainer)
					PreviewRecord::updateData($previewContainer);

				$pageIds = array();
				foreach ($library['pages'] as $page)
				{
					LibraryPageRecord::updateDataFromSoap($page, $libraryRootPath);
					$pageIds[] = $page['id'];
				}
				LibraryPageRecord::clearByIds($library['id'], $pageIds);

				if (array_key_exists('config', $library) && isset($library['config']))
					LibraryConfigRecord::updateData($library['config']);

				echo 'Library ' . ($needToCreate ? 'created' : 'updated') . ': ' . $library['name'] . "\n";
				return true;
			}
			else
				return false;
		}

		/**
		 * @param application\models\wallbin\models\cadmin\entities\Library $library
		 * @param int $changeType
		 */
		public static function updateDataFromChangeSet($library, $changeType)
		{
			switch ($changeType)
			{
				case ChangeSet::ChangeTypeAdd:
				case ChangeSet::ChangeTypeUpdate:
					$libraryRecord = self::model()->findByPk($library->id);
					if (!isset($libraryRecord))
					{
						$libraryRecord = new LibraryRecord();
						$libraryRecord->id = $library->id;
					}
					$libraryRecord->name = $library->name;
					$libraryRecord->last_update = date(Yii::app()->params['mysqlDateFormat'], strtotime($library->lastModified));
					$libraryRecord->settings = CJSON::encode($library->settings);
					$libraryRecord->save();
					break;
				case ChangeSet::ChangeTypeDelete:
					self::clearData($library->id);
					self::model()->deleteByPk($library->id);
					break;
			}
		}

		/**
		 * @param $libraryId
		 */
		public static function clearData($libraryId)
		{
			AutoWidgetRecord::clearData($libraryId);
			PreviewRecord::clearData($libraryId);
			LineBreakRecord::clearData($libraryId);
			BannerRecord::clearData($libraryId);
			LinkSuperFilterRecord::clearData($libraryId);
			LinkCategoryRecord::clearData($libraryId);
			ColumnRecord::clearData($libraryId);
			LinkWhiteListRecord::clearData($libraryId);
			LinkBlackListRecord::clearData($libraryId);
			LibraryConfigRecord::clearData($libraryId);
		}
	}

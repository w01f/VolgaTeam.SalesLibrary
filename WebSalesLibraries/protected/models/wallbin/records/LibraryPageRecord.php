<?php
	use application\models\services_data\cadmin\models\versions_management\ChangeSet;

	/**
	 * Class LibraryPageRecord
	 * @property mixed id
	 * @property mixed id_library
	 * @property string name
	 * @property int order
	 * @property boolean has_columns
	 * @property string settings
	 * @property string date_modify
	 * @property string cached_col_view
	 */
	class LibraryPageRecord extends CActiveRecord
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
			return '{{page}}';
		}

		/**
		 * @param array $page
		 * @param string $libraryRootPath
		 */
		public static function updateDataFromSoap($page, $libraryRootPath)
		{
			$needToUpdate = false;
			$needToCreate = false;
			/** @var $pageRecord LibraryPageRecord */
			$pageRecord = LibraryPageRecord::model()->findByPk($page['id']);
			$pageDate = date(Yii::app()->params['mysqlDateFormat'], strtotime($page['dateModify']));
			if ($pageRecord !== null)
			{
				if ($pageRecord->date_modify != null)
					if ($pageRecord->date_modify != $pageDate)
						$needToUpdate = true;
			}
			else
			{
				$pageRecord = new LibraryPageRecord();
				$needToCreate = true;
			}
			if ($needToCreate || $needToUpdate)
			{
				$pageRecord->id = $page['id'];
				$pageRecord->id_library = $page['libraryId'];
				$pageRecord->name = $page['name'];
				$pageRecord->order = $page['order'];
				$pageRecord->has_columns = $page['enableColumns'];
				$pageRecord->settings = array_key_exists('settings', $page) && isset($page['settings']) ? CJSON::encode($page['settings']) : null;
				$pageRecord->date_modify = $pageDate;
				$pageRecord->save();

				echo 'Page ' . ($needToCreate ? 'created' : 'updated') . ': ' . $page['name'] . "\n";
			}

			$folderIds = array();
			foreach ($page['folders'] as $folder)
			{
				FolderRecord::updateDataFromSoap($folder, $libraryRootPath);
				$folderIds[] = $folder['id'];
			}
			FolderRecord::clearByIds($page['id'], $folderIds);

			foreach ($page['columns'] as $column)
				ColumnRecord::updateData($column);
		}

		/**
		 * @param application\models\wallbin\models\cadmin\entities\LibraryPage $libraryPage
		 * @param int $changeType
		 */
		public static function updateDataFromChangeSet($libraryPage, $changeType)
		{
			switch ($changeType)
			{
				case ChangeSet::ChangeTypeAdd:
				case ChangeSet::ChangeTypeUpdate:
					$libraryPageRecord = self::model()->findByPk($libraryPage->id);
					if (!isset($libraryPageRecord))
					{
						$libraryPageRecord = new LibraryPageRecord();
						$libraryPageRecord->id = $libraryPage->id;
						$libraryPageRecord->id_library = $libraryPage->libraryId;
					}
					$libraryPageRecord->name = $libraryPage->name;
					$libraryPageRecord->order = $libraryPage->order;
					$libraryPageRecord->date_modify = date(Yii::app()->params['mysqlDateFormat'], strtotime($libraryPage->lastModified));
					$libraryPageRecord->settings = CJSON::encode($libraryPage->settings);
					$libraryPageRecord->save();
					break;
				case ChangeSet::ChangeTypeDelete:
					FolderRecord::clearByIds($libraryPage->id, array());
					self::model()->deleteByPk($libraryPage->id);
					break;
			}
		}

		/**
		 * @param $page
		 */
		public static function updateCache($page)
		{
			$pageRecord = LibraryPageRecord::model()->findByPk($page->id);
			if ($pageRecord !== null)
			{
				$pageRecord->cached_col_view = $page->cachedColumnsView;
				$pageRecord->save();
			}
		}

		/**
		 * @param $libraryId
		 */
		public static function clearByLibrary($libraryId)
		{
			LibraryPageRecord::model()->deleteAll('id_library=?', array($libraryId));
		}

		/**
		 * @param string $libraryId
		 * @param array $excludePageIds
		 */
		public static function clearByIds($libraryId, $excludePageIds)
		{
			/** @var $pageRecords LibraryPageRecord[] */
			if (count($excludePageIds) > 0)
				$pageRecords = self::model()->findAll("id_library = '" . $libraryId . "' and id not in ('" . implode("','", $excludePageIds) . "')");
			else
				$pageRecords = self::model()->findAll("id_library = '" . $libraryId . "'");
			foreach ($pageRecords as $pageRecord)
			{
				FolderRecord::clearByIds($pageRecord->id, array());
				$pageRecord->delete();
			}
		}
	}
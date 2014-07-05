<?php

	/**
	 * Class LibraryPageRecord
	 * @property mixed id
	 * @property mixed id_library
	 * @property mixed name
	 * @property mixed order
	 * @property mixed has_columns
	 * @property string date_modify
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
		 * @param $page
		 * @param $libraryRootPath
		 */
		public static function updateData($page, $libraryRootPath)
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
				$pageRecord->date_modify = $pageDate;
				$pageRecord->save();

				echo 'Page ' . ($needToCreate ? 'created' : 'updated') . ': ' . $page['name'] . "\n";
			}

			$folderIds = null;
			foreach ($page['folders'] as $folder)
			{
				FolderRecord::updateData($folder, $libraryRootPath);
				$folderIds[] = $folder['id'];
			}
			FolderRecord::clearByIds($page['id'], $folderIds);

			foreach ($page['columns'] as $column)
				ColumnRecord::updateData($column);
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
		 * @param $libraryId
		 * @param $pageIds
		 */
		public static function clearByIds($libraryId, $pageIds)
		{
			/** @var $pageRecords LibraryPageRecord[] */
			if (isset($pageIds))
				$pageRecords = self::model()->findAll("id_library = '" . $libraryId . "' and id not in ('" . implode("','", $pageIds) . "')");
			else
				$pageRecords = self::model()->findAll("id_library = '" . $libraryId . "'");
			foreach ($pageRecords as $pageRecord)
			{
				FolderRecord::clearByIds($pageRecord->id, null);
				$pageRecord->delete();
			}
		}
	}
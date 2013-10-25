<?php
class LibraryPageStorage extends CActiveRecord
{
    public static function model($className = __CLASS__)
    {
        return parent::model($className);
    }

    public function tableName()
    {
        return '{{page}}';
    }

    public static function updateData($page, $libraryRootPath)
    {
        $needToUpdate = false;
        $needToCreate = false;
        $pageRecord = LibraryPageStorage::model()->findByPk($page['id']);
        $pageDate = date(Yii::app()->params['mysqlDateFormat'], strtotime($page['dateModify']));
        if ($pageRecord !== null)
        {
            if ($pageRecord->date_modify != null)
                if ($pageRecord->date_modify != $pageDate)
                    $needToUpdate = true;
        }
        else
        {
            $pageRecord = new LibraryPageStorage();
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
            FolderStorage::updateData($folder, $libraryRootPath);
            $folderIds[] = $folder['id'];
        }
        FolderStorage::clearByIds($page['id'], $folderIds);

        foreach ($page['columns'] as $column)
            ColumnStorage::updateData($column);
    }

    public static function updateCache($page)
    {
        $pageRecord = LibraryPage::model()->findByPk($page->id);
        if ($pageRecord !== null)
        {
            $pageRecord->cached_col_view = $page->cachedColumnsView;
            $pageRecord->save();
        }
    }

    public static function clearByLibrary($libraryId)
    {
        LibraryPageStorage::model()->deleteAll('id_library=?', array($libraryId));
    }

	public static function clearByIds($libraryId, $pageIds)
	{
		if (isset($pageIds))
			$pageRecords = self::model()->findAll("id_library = '" . $libraryId . "' and id not in ('" . implode("','", $pageIds) . "')");
		else
			$pageRecords = self::model()->findAll("id_library = '" . $libraryId . "'");
		foreach ($pageRecords as $pageRecord)
		{
			FolderStorage::clearByIds($pageRecord->id, null);
			$pageRecord->delete();
		}
	}

}

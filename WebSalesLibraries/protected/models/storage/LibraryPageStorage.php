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

    public static function UpdateData($page)
    {
        $pageRecord = new LibraryPageStorage();
        $pageRecord->id = $page->id;
        $pageRecord->id_library = $page->libraryId;
        $pageRecord->name = $page->name;
        $pageRecord->order = $page->order;
        $pageRecord->has_columns = $page->enableColumns;

        foreach ($page->columns as $column)
            ColumnStorage::UpdateData($column);

        foreach ($page->folders as $folder)
            FolderStorage::UpdateData($folder);

        $pageRecord->save();
    }

    public static function UpdateCache($page)
    {
        $pageRecord = LibraryPage::model()->findByPk($page->id);
        if ($pageRecord !== null)
        {
            $pageRecord->cached_col_view = $page->cachedColumnsView;
            $pageRecord->save();
        }
    }

    public static function ClearData($libraryId)
    {
        LibraryPageStorage::model()->deleteAll('id_library=?', array($libraryId));
    }

}

?>

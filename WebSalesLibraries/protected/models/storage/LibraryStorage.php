<?php
class LibraryStorage extends CActiveRecord
{
    public static function model($className = __CLASS__)
    {
        return parent::model($className);
    }

    public function tableName()
    {
        return '{{library}}';
    }

    public static function updateData($library)
    {
        $libraryRecord = new LibraryStorage();
        $libraryRecord->id = $library->id;
        $libraryRecord->name = $library->name;
        $libraryRecord->save();

        foreach ($library->pages as $page)
            LibraryPageStorage::updateData($page);

        foreach ($library->autoWidgets as $autoWidget)
            AutoWidgetStorage::updateData($autoWidget);
    }

    public static function updateCache($libraryId)
    {
        $pageRecord = LinkStorage::model()->findAll('id_library=?', array($libraryId));
        if ($pageRecord !== false)
        {
            
        }        
        $libraryRecord = new LibraryStorage();
        $libraryRecord->id = $library->id;
        $libraryRecord->name = $library->name;
        $libraryRecord->save();

        foreach ($library->pages as $page)
            LibraryPageStorage::updateData($page);

        foreach ($library->autoWidgets as $autoWidget)
            AutoWidgetStorage::updateData($autoWidget);
    }

    public static function clearData($libraryId)
    {
        PreviewStorage::clearData($libraryId);
        LineBreakStorage::clearData($libraryId);
        LinkStorage::clearData($libraryId);
        FolderStorage::clearData($libraryId);
        BannerStorage::clearData($libraryId);
        ColumnStorage::clearData($libraryId);
        LibraryPageStorage::clearData($libraryId);
        AutoWidgetStorage::clearData($libraryId);
        LibraryStorage::model()->deleteByPk($libraryId);
    }

}

?>

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

    public static function UpdateData($library)
    {
        $libraryRecord = new LibraryStorage();
        $libraryRecord->id = $library->id;
        $libraryRecord->name = $library->name;
        $libraryRecord->save();

        foreach ($library->pages as $page)
            LibraryPageStorage::UpdateData($page);

        foreach ($library->autoWidgets as $autoWidget)
            AutoWidgetStorage::UpdateData($autoWidget);
    }

    public static function ClearData($libraryId)
    {
        PreviewStorage::ClearData($libraryId);
        LineBreakStorage::ClearData($libraryId);
        LinkStorage::ClearData($libraryId);
        FolderStorage::ClearData($libraryId);
        BannerStorage::ClearData($libraryId);
        ColumnStorage::ClearData($libraryId);
        LibraryPageStorage::ClearData($libraryId);
        AutoWidgetStorage::ClearData($libraryId);
        LibraryStorage::model()->deleteByPk($libraryId);
    }

}

?>

<?php
class AutoWidgetStorage extends CActiveRecord
{
    public static function model($className = __CLASS__)
    {
        return parent::model($className);
    }

    public function tableName()
    {
        return '{{autowidget}}';
    }

    public static function updateData($autoWidget)
    {
        $autoWidgetRecord = new AutoWidgetStorage();
        $autoWidgetRecord->id_library = $autoWidget->libraryId;
        $autoWidgetRecord->extension = $autoWidget->extension;
        $autoWidgetRecord->widget = $autoWidget->widget;
        $autoWidgetRecord->save();
    }

    public static function clearData($libraryId)
    {
        AutoWidgetStorage::model()->deleteAll('id_library=?', array($libraryId));
    }

}

?>

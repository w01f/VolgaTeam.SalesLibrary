<?php
class ColumnStorage extends CActiveRecord
{
    public static function model($className = __CLASS__)
    {
        return parent::model($className);
    }

    public function tableName()
    {
        return '{{column}}';
    }

    public static function UpdateData($column)
    {
        $columnRecord = new ColumnStorage();
        $columnRecord->id_library = $column->libraryId;
        $columnRecord->name = $column->name;
        $columnRecord->order = $column->order;
        $columnRecord->back_color = $column->backColor;
        $columnRecord->fore_color = $column->foreColor;
        $columnRecord->font_name = $column->font->name;
        $columnRecord->font_size = $column->font->size;
        $columnRecord->font_bold = $column->font->isBold;
        $columnRecord->font_italic = $column->font->isItalic;
        $columnRecord->show_text = $column->showText;
        $columnRecord->alignment = $column->alignment;
        $columnRecord->enable_widget = $column->enableWidget;
        $columnRecord->widget = $column->widget;
        
        $columnRecord->id_banner = $column->banner->id;
        BannerStorage::UpdateData($column->banner);
        
        $columnRecord->save();
    }

    public static function ClearData($libraryId)
    {
        ColumnStorage::model()->deleteAll('id_library=?', array($libraryId));
    }

}

?>

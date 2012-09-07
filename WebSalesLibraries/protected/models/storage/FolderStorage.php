<?php
class FolderStorage extends CActiveRecord
{
    public static function model($className = __CLASS__)
    {
        return parent::model($className);
    }

    public function tableName()
    {
        return '{{folder}}';
    }

    public static function updateData($folder)
    {
        $folderRecord = new FolderStorage();
        $folderRecord->id = $folder->id;
        $folderRecord->id_page = $folder->pageId;
        $folderRecord->id_library = $folder->libraryId;
        $folderRecord->name = $folder->name;
        $folderRecord->column_order = $folder->columnOrder;
        $folderRecord->row_order = $folder->rowOrder;
        $folderRecord->border_color = $folder->borderColor;
        $folderRecord->window_back_color = $folder->windowBackColor;
        $folderRecord->window_fore_color = $folder->windowForeColor;
        $folderRecord->header_back_color = $folder->headerBackColor;
        $folderRecord->header_fore_color = $folder->headerForeColor;
        $folderRecord->window_font_name = $folder->windowFont->name;
        $folderRecord->window_font_size = $folder->windowFont->size;
        $folderRecord->window_font_bold = $folder->windowFont->isBold;
        $folderRecord->window_font_italic = $folder->windowFont->isItalic;
        $folderRecord->header_font_name = $folder->headerFont->name;
        $folderRecord->header_font_size = $folder->headerFont->size;
        $folderRecord->header_font_bold = $folder->headerFont->isBold;
        $folderRecord->header_font_italic = $folder->headerFont->isItalic;
        $folderRecord->header_alignment = $folder->headerAlignment;
        $folderRecord->enable_widget = $folder->enableWidget;
        $folderRecord->widget = $folder->widget;

        $folderRecord->id_banner = $folder->banner->id;
        BannerStorage::updateData($folder->banner);

        foreach ($folder->files as $link)
            LinkStorage::updateData($link);

        $folderRecord->save();
    }

    public static function clearData($libraryId)
    {
        FolderStorage::model()->deleteAll('id_library=?', array($libraryId));
    }

}

?>

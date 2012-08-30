<?php
class LinkStorage extends CActiveRecord
{
    public static function model($className = __CLASS__)
    {
        return parent::model($className);
    }

    public function tableName()
    {
        return '{{link}}';
    }

    public static function UpdateData($link)
    {
        $linkRecord = new LinkStorage();
        $linkRecord->id = $link->id;
        $linkRecord->id_folder = $link->folderId;
        $linkRecord->id_library = $link->libraryId;
        $linkRecord->name = $link->name;
        $linkRecord->file_relative_path = $link->fileRelativePath;
        $linkRecord->file_name = $link->fileName;
        $linkRecord->file_extension = $link->fileExtension;
        $linkRecord->note = $link->note;
        $linkRecord->is_bold = $link->isBold;
        $linkRecord->order = $link->order;
        $linkRecord->type = $link->type;
        $linkRecord->enable_widget = $link->enableWidget;
        $linkRecord->widget = $link->widget;

        $linkRecord->id_banner = $link->banner->id;
        BannerStorage::UpdateData($link->banner);

        if (isset($link->lineBreakProperties))
        {
            $linkRecord->id_line_break = $link->lineBreakProperties->id;
            LineBreakStorage::UpdateData($link->lineBreakProperties);
        }

        if (isset($link->universalPreview))
            PreviewStorage::UpdateData($link->universalPreview);

        $linkRecord->save();
    }

    public static function UpdateContent($linkId, $content)
    {
        $linkRecord = LinkStorage::model()->findByPk($linkId);
        if ($linkRecord !== false)
        {
            $linkRecord->content = $content;
            $linkRecord->save();
        }
    }

    public static function ClearData($libraryId)
    {
        LinkStorage::model()->deleteAll('id_library=?', array($libraryId));
    }

}

?>

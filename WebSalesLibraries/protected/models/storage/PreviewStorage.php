<?php
class PreviewStorage extends CActiveRecord
{
    public static function model($className = __CLASS__)
    {
        return parent::model($className);
    }

    public function tableName()
    {
        return '{{preview}}';
    }

    public static function UpdateData($previewContainer)
    {
        if (isset($previewContainer->pngLinks))
            foreach ($previewContainer->pngLinks as $link)
            {
                $previewRecord = new PreviewStorage();
                $previewRecord->id_link = $previewContainer->linkId;
                $previewRecord->id_library = $previewContainer->libraryId;
                $previewRecord->type = 'png';
                $previewRecord->relative_path = $link;
                $previewRecord->save();
            }

        if (isset($previewContainer->jpegLinks))
            foreach ($previewContainer->jpegLinks as $link)
            {
                $previewRecord = new PreviewStorage();
                $previewRecord->id_link = $previewContainer->linkId;
                $previewRecord->id_library = $previewContainer->libraryId;
                $previewRecord->type = 'jpeg';
                $previewRecord->relative_path = $link;
                $previewRecord->save();
            }

        if (isset($previewContainer->pdfLinks))
            foreach ($previewContainer->pdfLinks as $link)
            {
                $previewRecord = new PreviewStorage();
                $previewRecord->id_link = $previewContainer->linkId;
                $previewRecord->id_library = $previewContainer->libraryId;
                $previewRecord->type = 'pdf';
                $previewRecord->relative_path = $link;
                $previewRecord->save();
            }

        if (isset($previewContainer->mp4Links))
            foreach ($previewContainer->mp4Links as $link)
            {
                $previewRecord = new PreviewStorage();
                $previewRecord->id_link = $previewContainer->linkId;
                $previewRecord->id_library = $previewContainer->libraryId;
                $previewRecord->type = 'mp4';
                $previewRecord->relative_path = $link;
                $previewRecord->save();
            }

        if (isset($previewContainer->ogvLinks))
            foreach ($previewContainer->ogvLinks as $link)
            {
                $previewRecord = new PreviewStorage();
                $previewRecord->id_link = $previewContainer->linkId;
                $previewRecord->id_library = $previewContainer->libraryId;
                $previewRecord->type = 'ogv';
                $previewRecord->relative_path = $link;
                $previewRecord->save();
            }

        if (isset($previewContainer->oldOfficeFormatLinks))
            foreach ($previewContainer->oldOfficeFormatLinks as $link)
            {
                $previewRecord = new PreviewStorage();
                $previewRecord->id_link = $previewContainer->linkId;
                $previewRecord->id_library = $previewContainer->libraryId;
                $previewRecord->type = 'old office';
                $previewRecord->relative_path = $link;
                $previewRecord->save();
            }

        if (isset($previewContainer->newOfficeFormatLinks))
            foreach ($previewContainer->newOfficeFormatLinks as $link)
            {
                $previewRecord = new PreviewStorage();
                $previewRecord->id_link = $previewContainer->linkId;
                $previewRecord->id_library = $previewContainer->libraryId;
                $previewRecord->type = 'new office';
                $previewRecord->relative_path = $link;
                $previewRecord->save();
            }

        if (isset($previewContainer->thumbsLinks))
            foreach ($previewContainer->thumbsLinks as $link)
            {
                $previewRecord = new PreviewStorage();
                $previewRecord->id_link = $previewContainer->linkId;
                $previewRecord->id_library = $previewContainer->libraryId;
                $previewRecord->type = 'thumbs';
                $previewRecord->relative_path = $link;
                if (isset($previewContainer->thumbsWidth))
                    $previewRecord->thumb_width = $previewContainer->thumbsWidth;
                if (isset($previewContainer->thumbsHeight))
                    $previewRecord->thumb_height = $previewContainer->thumbsHeight;
                $previewRecord->save();
            }
    }

    public static function ClearData($libraryId)
    {
        PreviewStorage::model()->deleteAll('id_library=?', array($libraryId));
    }

}

?>

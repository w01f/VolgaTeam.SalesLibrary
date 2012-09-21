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

    public static function updateData($previewContainer)
    {
        if (array_key_exists('pngLinks', $previewContainer))
            if (isset($previewContainer['pngLinks']))
                foreach ($previewContainer['pngLinks'] as $link)
                {
                    $previewRecord = new PreviewStorage();
                    $previewRecord->id_link = $previewContainer['linkId'];
                    $previewRecord->id_library = $previewContainer['libraryId'];
                    $previewRecord->type = 'png';
                    $previewRecord->relative_path = $link;
                    $previewRecord->save();
                }

        if (array_key_exists('jpegLinks', $previewContainer))
            if (isset($previewContainer['jpegLinks']))
                foreach ($previewContainer['jpegLinks'] as $link)
                {
                    $previewRecord = new PreviewStorage();
                    $previewRecord->id_link = $previewContainer['linkId'];
                    $previewRecord->id_library = $previewContainer['libraryId'];
                    $previewRecord->type = 'jpeg';
                    $previewRecord->relative_path = $link;
                    $previewRecord->save();
                }

        if (array_key_exists('pdfLinks', $previewContainer))
            if (isset($previewContainer['pdfLinks']))
                foreach ($previewContainer['pdfLinks'] as $link)
                {
                    $previewRecord = new PreviewStorage();
                    $previewRecord->id_link = $previewContainer['linkId'];
                    $previewRecord->id_library = $previewContainer['libraryId'];
                    $previewRecord->type = 'pdf';
                    $previewRecord->relative_path = $link;
                    $previewRecord->save();
                }

        if (array_key_exists('mp4Links', $previewContainer))
            if (isset($previewContainer['mp4Links']))
                foreach ($previewContainer['mp4Links'] as $link)
                {
                    $previewRecord = new PreviewStorage();
                    $previewRecord->id_link = $previewContainer['linkId'];
                    $previewRecord->id_library = $previewContainer['libraryId'];
                    $previewRecord->type = 'mp4';
                    $previewRecord->relative_path = $link;
                    $previewRecord->save();
                }

        if (array_key_exists('ogvLinks', $previewContainer))
            if (isset($previewContainer['ogvLinks']))
                foreach ($previewContainer['ogvLinks'] as $link)
                {
                    $previewRecord = new PreviewStorage();
                    $previewRecord->id_link = $previewContainer['linkId'];
                    $previewRecord->id_library = $previewContainer['libraryId'];
                    $previewRecord->type = 'ogv';
                    $previewRecord->relative_path = $link;
                    $previewRecord->save();
                }

        if (array_key_exists('oldOfficeFormatLinks', $previewContainer))
            if (isset($previewContainer['oldOfficeFormatLinksLinks']))
                foreach ($previewContainer['oldOfficeFormatLinks'] as $link)
                {
                    $previewRecord = new PreviewStorage();
                    $previewRecord->id_link = $previewContainer['linkId'];
                    $previewRecord->id_library = $previewContainer['libraryId'];
                    $previewRecord->type = 'old office';
                    $previewRecord->relative_path = $link;
                    $previewRecord->save();
                }

        if (array_key_exists('newOfficeFormatLinks', $previewContainer))
            if (isset($previewContainer['newOfficeFormatLinksLinks']))
                foreach ($previewContainer['newOfficeFormatLinks'] as $link)
                {
                    $previewRecord = new PreviewStorage();
                    $previewRecord->id_link = $previewContainer['linkId'];
                    $previewRecord->id_library = $previewContainer['libraryId'];
                    $previewRecord->type = 'new office';
                    $previewRecord->relative_path = $link;
                    $previewRecord->save();
                }

        if (array_key_exists('thumbsLinks', $previewContainer))
            if (isset($previewContainer['thumbsLinks']))
                foreach ($previewContainer['thumbsLinks'] as $link)
                {
                    $previewRecord = new PreviewStorage();
                    $previewRecord->id_link = $previewContainer['linkId'];
                    $previewRecord->id_library = $previewContainer['libraryId'];
                    $previewRecord->type = 'thumbs';
                    $previewRecord->relative_path = $link;
                    if (array_key_exists('thumbsWidth', $previewContainer))
                        $previewRecord->thumb_width = $previewContainer['thumbsWidth'];
                    if (array_key_exists('thumbsHeight', $previewContainer))
                        $previewRecord->thumb_height = $previewContainer['thumbsHeight'];
                    $previewRecord->save();
                }
    }

    public static function clearByLibrary($libraryId)
    {
        PreviewStorage::model()->deleteAll('id_library=?', array($libraryId));
    }

    public static function clearByLink($linkId)
    {
        Yii::app()->db->createCommand()->delete('tbl_preview', "id_link = '" . $linkId . "'");
    }

    public static function clearByLinks($libraryId, $linkIds)
    {
        Yii::app()->db->createCommand()->delete('tbl_preview', "id_library = '" . $libraryId . "' and id_link not in ('" . implode("','", $linkIds) . "')");
    }

}

?>

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
                    $previewRecord->id_container = $previewContainer['id'];
                    $previewRecord->id_library = $previewContainer['libraryId'];
                    $previewRecord->type = 'png';
                    $previewRecord->relative_path = $link;
                    $previewRecord->save();
                }
        if (array_key_exists('pngPhoneLinks', $previewContainer))
            if (isset($previewContainer['pngPhoneLinks']))
                foreach ($previewContainer['pngPhoneLinks'] as $link)
                {
                    $previewRecord = new PreviewStorage();
                    $previewRecord->id_container = $previewContainer['id'];
                    $previewRecord->id_library = $previewContainer['libraryId'];
                    $previewRecord->type = 'png_phone';
                    $previewRecord->relative_path = $link;
                    $previewRecord->save();
                }                
        if (array_key_exists('jpegLinks', $previewContainer))
            if (isset($previewContainer['jpegLinks']))
                foreach ($previewContainer['jpegLinks'] as $link)
                {
                    $previewRecord = new PreviewStorage();
                    $previewRecord->id_container = $previewContainer['id'];
                    $previewRecord->id_library = $previewContainer['libraryId'];
                    $previewRecord->type = 'jpeg';
                    $previewRecord->relative_path = $link;
                    $previewRecord->save();
                }
        if (array_key_exists('jpegPhoneLinks', $previewContainer))
            if (isset($previewContainer['jpegPhoneLinks']))
                foreach ($previewContainer['jpegPhoneLinks'] as $link)
                {
                    $previewRecord = new PreviewStorage();
                    $previewRecord->id_container = $previewContainer['id'];
                    $previewRecord->id_library = $previewContainer['libraryId'];
                    $previewRecord->type = 'jpeg_phone';
                    $previewRecord->relative_path = $link;
                    $previewRecord->save();
                }                
        if (array_key_exists('pdfLinks', $previewContainer))
            if (isset($previewContainer['pdfLinks']))
                foreach ($previewContainer['pdfLinks'] as $link)
                {
                    $previewRecord = new PreviewStorage();
                    $previewRecord->id_container = $previewContainer['id'];
                    $previewRecord->id_library = $previewContainer['libraryId'];
                    $previewRecord->type = 'pdf';
                    $previewRecord->relative_path = $link;
                    $previewRecord->save();
                }

		if (array_key_exists('wmvLinks', $previewContainer))
			if (isset($previewContainer['wmvLinks']))
				foreach ($previewContainer['wmvLinks'] as $link)
				{
					$previewRecord = new PreviewStorage();
					$previewRecord->id_container = $previewContainer['id'];
					$previewRecord->id_library = $previewContainer['libraryId'];
					$previewRecord->type = 'wmv';
					$previewRecord->relative_path = $link;
					$previewRecord->save();
				}

        if (array_key_exists('mp4Links', $previewContainer))
            if (isset($previewContainer['mp4Links']))
                foreach ($previewContainer['mp4Links'] as $link)
                {
                    $previewRecord = new PreviewStorage();
                    $previewRecord->id_container = $previewContainer['id'];
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
                    $previewRecord->id_container = $previewContainer['id'];
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
                    $previewRecord->id_container = $previewContainer['id'];
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
                    $previewRecord->id_container = $previewContainer['id'];
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
                    $previewRecord->id_container = $previewContainer['id'];
                    $previewRecord->id_library = $previewContainer['libraryId'];
                    $previewRecord->type = 'thumbs';
                    $previewRecord->relative_path = $link;
                    if (array_key_exists('thumbsWidth', $previewContainer))
                        $previewRecord->thumb_width = $previewContainer['thumbsWidth'];
                    if (array_key_exists('thumbsHeight', $previewContainer))
                        $previewRecord->thumb_height = $previewContainer['thumbsHeight'];
                    $previewRecord->save();
                }
        if (array_key_exists('thumbsPhoneLinks', $previewContainer))
            if (isset($previewContainer['thumbsPhoneLinks']))
                foreach ($previewContainer['thumbsPhoneLinks'] as $link)
                {
                    $previewRecord = new PreviewStorage();
                    $previewRecord->id_container = $previewContainer['id'];
                    $previewRecord->id_library = $previewContainer['libraryId'];
                    $previewRecord->type = 'thumbs_phone';
                    $previewRecord->relative_path = $link;
                    $previewRecord->save();
                }                
    }

    public static function clearData($libraryId)
    {
        PreviewStorage::model()->deleteAll('id_library=?', array($libraryId));
    }
}

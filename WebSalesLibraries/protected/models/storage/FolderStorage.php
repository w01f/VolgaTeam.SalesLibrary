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

    public static function updateData($folder, $libraryRootPath)
    {
        $needToUpdate = false;
        $needToCreate = false;
        $folderRecord = FolderStorage::model()->findByPk($folder['id']);
        $folderDate = date(Yii::app()->params['mysqlDateFormat'], strtotime($folder['dateModify']));
        if ($folderRecord !== null)
        {
            if ($folderRecord->date_modify != null)
                if ($folderRecord->date_modify != $folderDate)
                    $needToUpdate = true;
        }
        else
        {
            $folderRecord = new FolderStorage();
            $needToCreate = true;
        }
        if ($needToCreate || $needToUpdate)
        {
            $folderRecord->id = $folder['id'];
            $folderRecord->id_page = $folder['pageId'];
            $folderRecord->id_library = $folder['libraryId'];
            $folderRecord->name = $folder['name'];
            $folderRecord->column_order = $folder['columnOrder'];
            $folderRecord->row_order = $folder['rowOrder'];
            $folderRecord->border_color = $folder['borderColor'];
            $folderRecord->window_back_color = $folder['windowBackColor'];
            $folderRecord->window_fore_color = $folder['windowForeColor'];
            $folderRecord->header_back_color = $folder['headerBackColor'];
            $folderRecord->header_fore_color = $folder['headerForeColor'];
            $folderRecord->window_font_name = $folder['windowFont']['name'];
            $folderRecord->window_font_size = $folder['windowFont']['size'];
            $folderRecord->window_font_bold = $folder['windowFont']['isBold'];
            $folderRecord->window_font_italic = $folder['windowFont']['isItalic'];
            $folderRecord->header_font_name = $folder['headerFont']['name'];
            $folderRecord->header_font_size = $folder['headerFont']['size'];
            $folderRecord->header_font_bold = $folder['headerFont']['isBold'];
            $folderRecord->header_font_italic = $folder['headerFont']['isItalic'];
            $folderRecord->header_alignment = $folder['headerAlignment'];
            $folderRecord->enable_widget = $folder['enableWidget'];
            $folderRecord->widget = $folder['widget'];
            $folderRecord->date_add = date(Yii::app()->params['mysqlDateFormat'], strtotime($folder['dateAdd']));
            $folderRecord->date_modify = $folderDate;

            echo 'Window ' . ($needToCreate ? 'created' : 'updated') . ': ' . $folder['name'] . "\n";
        }
        $folderRecord->id_banner = $folder['banner']['id'];
        $folderRecord->save();

        BannerStorage::updateData($folder['banner']);

		$linkIds = null;
        foreach ($folder['files'] as $link)
        {
            LinkStorage::updateData($link, $libraryRootPath);
            $linkIds[] = $link['id'];
        }
        LinkStorage::clearByIds($folder['id'], $linkIds);
    }

    public static function clearByLibrary($libraryId)
    {
        FolderStorage::model()->deleteAll('id_library=?', array($libraryId));
    }

    public static function clearByIds($pageId, $folderIds)
    {
		if(isset($folderIds))
        	Yii::app()->db->createCommand()->delete('tbl_folder', "id_page = '" . $pageId . "' and id not in ('" . implode("','", $folderIds) . "')");
		else
			Yii::app()->db->createCommand()->delete('tbl_folder', "id_page = '" . $pageId . "'");
    }

}

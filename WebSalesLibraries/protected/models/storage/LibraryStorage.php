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

    public static function updateData($library, $sourceDate, $libraryRootPath)
    {
        $needToUpdate = false;
        $needToCreate = false;
        $libraryRecord = LibraryStorage::model()->findByPk($library['id']);
        if ($libraryRecord !== null)
        {
            if ($libraryRecord->last_update != null)
                if ($libraryRecord->last_update != date(Yii::app()->params['mysqlDateFormat'], $sourceDate))
                    $needToUpdate = true;
        }
        else
        {
            $libraryRecord = new LibraryStorage();
            $needToCreate = true;
        }

        if ($needToCreate || $needToUpdate)
        {
            Yii::app()->cacheDB->flush();
            self::clearData($library['id']);

            $libraryRecord->id = $library['id'];
            $libraryRecord->name = $library['name'];
            $libraryRecord->last_update = date(Yii::app()->params['mysqlDateFormat'], $sourceDate);
            $libraryRecord->save();

            foreach ($library['autoWidgets'] as $autoWidget)
                AutoWidgetStorage::updateData($autoWidget);

            foreach ($library['previewContainers'] as $previewContainer)
                PreviewStorage::updateData($previewContainer);

            foreach ($library['pages'] as $page)
            {
                LibraryPageStorage::updateData($page, $libraryRootPath);
                $pageIds[] = $page['id'];
            }
            if (isset($pageIds))
                LibraryPageStorage::clearByIds($library['id'], $pageIds);

			if (array_key_exists('config', $library) && isset($library['config']))
				LibraryConfigStorage::updateData($library['config']);

            echo 'Library ' . ($needToCreate ? 'created' : 'updated') . ': ' . $library['name'] . "\n";
            return true;
        }
        else
        {
            return false;
        }
    }

    public static function clearData($libraryId)
    {
        AutoWidgetStorage::clearData($libraryId);
        PreviewStorage::clearData($libraryId);
        LineBreakStorage::clearData($libraryId);
        BannerStorage::clearData($libraryId);
        FileCardStorage::clearData($libraryId);
        AttachmentStorage::clearData($libraryId);
        LinkCategoryStorage::clearData($libraryId);
        ColumnStorage::clearData($libraryId);
        UserLinkStorage::clearData($libraryId);
        UserPageCacheStorage::clearData($libraryId);
		LibraryConfigStorage::clearData($libraryId);
    }

}

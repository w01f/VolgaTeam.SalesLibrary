<?php
class FavoritesLinkStorage extends CActiveRecord
{
    public static function model($className = __CLASS__)
    {
        return parent::model($className);
    }

    public function tableName()
    {
        return '{{favorites_link}}';
    }

    public static function addLink($userId, $linkId, $linkName, $folderName, $libraryId)
    {
        if (isset($folderName))
        {
            $folderRecord = FavoritesFolderStorage::getFolder($userId, $folderName);
            $linkRecord = self::model()->find('id_user=? and id_link=? and id_folder=? and LOWER(name)=?', array($userId, $linkId, $folderRecord->id, strtolower($linkName)));
        }
        else
            $linkRecord = self::model()->find('id_user=? and id_link=? and id_folder is null and LOWER(name)=?', array($userId, $linkId, strtolower($linkName)));
        if (!isset($linkRecord))
        {
            $linkRecord = new FavoritesLinkStorage();
            $linkRecord->id = uniqid();
            $linkRecord->id_link = $linkId;
            $linkRecord->id_library = $libraryId;
            $linkRecord->id_folder = isset($folderRecord) ? $folderRecord->id : null;
            $linkRecord->id_user = $userId;
        }
        $linkRecord->name = $linkName;
        $linkRecord->save();
    }

    public static function getAllLinks($userId)
    {
        return self::model()->findAll('id_user=?', array($userId));
    }

    public static function clearAll()
    {
        self::model()->deleteAll();
    }

    public static function clearByUser($userId)
    {
        self::model()->deleteAll('id_user=?', array($userId));
    }

    public static function clearByLinkIds($liveLinkIds)
    {
        Yii::app()->db->createCommand()->delete('tbl_favorites_link', "id_link not in ('" . implode("','", $liveLinkIds) . "')");
    }

    public static function clearByLibrary($libraryId)
    {
        self::model()->deleteAll('id_library=?', array($libraryId));
    }
}

<?php
class UserPageCacheStorage extends CActiveRecord
{
    public static function model($className = __CLASS__)
    {
        return parent::model($className);
    }

    public function tableName()
    {
        return '{{user_page_cache}}';
    }

    public static function updateData($userId, $pageId, $libraryId)
    {
        $userLinkRecord = new UserPageCacheStorage();
        $userLinkRecord->id = uniqid();
        $userLinkRecord->id_user = $userId;
        $userLinkRecord->id_page = $pageId;
        $userLinkRecord->id_library = $libraryId;
        $userLinkRecord->save();
    }

    public static function getPageCache($userId, $pageId)
    {
        $pageCache = self::model()->find('id_user=? and id_page=?', array($userId, $pageId));
        return isset($pageCache) ? $pageCache : null;
    }

    public static function clearData($libraryId)
    {
        self::model()->deleteAll('id_library=?', array($libraryId));
    }
}

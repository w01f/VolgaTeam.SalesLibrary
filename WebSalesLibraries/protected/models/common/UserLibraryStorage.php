<?php
class UserLibraryStorage extends CActiveRecord
{
    public static function model($className = __CLASS__)
    {
        return parent::model($className);
    }

    public function tableName()
    {
        return '{{user_library}}';
    }

    public static function assignPagesForUser($login, $assignedPages)
    {
        $userRecord = UserStorage::model()->find('LOWER(login)=?', array(strtolower($login)));
        if (isset($userRecord))
        {
            self::clearLibraryIdsByUser($userRecord->id);
            foreach ($assignedPages as $page)
            {
                $userLibraryRecord = new UserLibraryStorage();
                $userLibraryRecord->id = uniqid();
                $userLibraryRecord->id_user = $userRecord->id;
                $userLibraryRecord->id_library = $page->libraryId;
                $userLibraryRecord->id_page = $page->id;
                $userLibraryRecord->save();
            }
        }
    }

    public static function getLibraryIdsByUser($userId)
    {
        $userLibraryRecords = self::model()->findAll('id_user =?', array($userId));
        if (isset($userLibraryRecords))
            foreach ($userLibraryRecords as $userLibraryRecord)
                $libraryIds[] = $userLibraryRecord->id_library;
        if (isset($libraryIds))
            return array_unique($libraryIds);
    }

    public static function getPageIdsByUser($userId)
    {
        $userLibraryRecords = self::model()->findAll('id_user =?', array($userId));
        if (isset($userLibraryRecords))
            foreach ($userLibraryRecords as $userLibraryRecord)
                $pageIds[] = $userLibraryRecord->id_page;
        if (isset($pageIds))
            return array_unique($pageIds);
    }

    public static function clearLibraryIdsByUser($userId)
    {
        self::model()->deleteAll('id_user =?', array($userId));
    }

}

?>

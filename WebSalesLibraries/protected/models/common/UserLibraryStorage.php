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

    public static function assignLibrariesForUser($login, $libraryIds)
    {
        $userRecord = UserStorage::model()->find('LOWER(login)=?', array(strtolower($login)));
        if (isset($userRecord))
        {
            self::clearLibraryIdsByUser($userRecord->id);
            foreach ($libraryIds as $id)
            {
                $userLibraryRecord = new UserLibraryStorage();
                $userLibraryRecord->id = uniqid();
                $userLibraryRecord->id_user = $userRecord->id;
                $userLibraryRecord->id_library = $id;
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
            return $libraryIds;
    }
    
    public static function clearLibraryIdsByUser($userId)
    {
        self::model()->deleteAll('id_user =?', array($userId));
    }

}

?>

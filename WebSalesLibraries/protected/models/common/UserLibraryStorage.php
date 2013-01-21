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
            self::clearObjectsByUser($userRecord->id);
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

    public static function assignUsersForPage($page, $users)
    {
        self::clearObjectsByUser($page->id);
        foreach ($users as $user)
        {
            $userLibraryRecord = new UserLibraryStorage();
            $userLibraryRecord->id = uniqid();
            $userLibraryRecord->id_user = $user->id;
            $userLibraryRecord->id_library = $page->libraryId;
            $userLibraryRecord->id_page = $page->id;
            $userLibraryRecord->save();
        }
    }

    public static function getLibraryIdsByUser($userId)
    {
        $userLibraryRecords = self::model()->findAll('id_user=?', array($userId));
        if (isset($userLibraryRecords))
            foreach ($userLibraryRecords as $userLibraryRecord)
                $libraryIds[] = $userLibraryRecord->id_library;
        if (isset($libraryIds))
            return array_unique($libraryIds);
        return null;
    }

    public static function getLibraryIdsByUserAngHisGroups($userId)
    {
        $userLibraryRecords = self::model()->findAll('id_user=?', array($userId));
        if (isset($userLibraryRecords))
            foreach ($userLibraryRecords as $userLibraryRecord)
                $libraryIds[] = $userLibraryRecord->id_library;

        $userGroupRecords = UserGroupStorage::model()->findAll('id_user=?', array($userId));
        if (isset($userGroupRecords))
            foreach ($userGroupRecords as $userGroupRecord)
            {
                $libraryIdsByGroup = GroupLibraryStorage::getLibraryIdsByGroup($userGroupRecord->id_group);
                if (isset($libraryIdsByGroup))
                {
                    if (isset($libraryIds))
                        $libraryIds = array_merge($libraryIds, $libraryIdsByGroup);
                    else
                        $libraryIds = $libraryIdsByGroup;
                }
            }

        if (isset($libraryIds))
            return array_unique($libraryIds);
        return null;
    }

    public static function getPageIdsByUser($userId)
    {
        $userLibraryRecords = self::model()->findAll('id_user=?', array($userId));
        if (isset($userLibraryRecords))
            foreach ($userLibraryRecords as $userLibraryRecord)
                $pageIds[] = $userLibraryRecord->id_page;
        if (isset($pageIds))
            return array_unique($pageIds);
        return null;
    }

    public static function getPageIdsByUserAngHisGroups($userId)
    {
        $userLibraryRecords = self::model()->findAll('id_user=?', array($userId));
        if (isset($userLibraryRecords))
            foreach ($userLibraryRecords as $userLibraryRecord)
                $pageIds[] = $userLibraryRecord->id_page;

        $userGroupRecords = UserGroupStorage::model()->findAll('id_user=?', array($userId));
        if (isset($userGroupRecords))
            foreach ($userGroupRecords as $userGroupRecord)
            {
                $pageIdsByGroup = GroupLibraryStorage::getPageIdsByGroup($userGroupRecord->id_group);
                if (isset($pageIdsByGroup))
                {
                    if (isset($pageIds))
                        $pageIds = array_merge($pageIds, $pageIdsByGroup);
                    else
                        $pageIds = $pageIdsByGroup;
                }
            }

        if (isset($pageIds))
            return array_unique($pageIds);
        return null;
    }

    public static function getUserIdsByPage($pageId)
    {
        $userLibraryRecords = self::model()->findAll('id_page=?', array($pageId));
        if (isset($userLibraryRecords))
            foreach ($userLibraryRecords as $userLibraryRecord)
                $userIds[] = $userLibraryRecord->id_user;
        if (isset($userIds))
            return array_unique($userIds);
        return null;
    }

    public static function clearObjectsByUser($userId)
    {
        self::model()->deleteAll('id_user=?', array($userId));
    }

    public static function clearObjectsByPage($pageId)
    {
        self::model()->deleteAll('id_page=?', array($pageId));
    }

}

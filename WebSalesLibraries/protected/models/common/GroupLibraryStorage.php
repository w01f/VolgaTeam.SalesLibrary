<?php
class GroupLibraryStorage extends CActiveRecord
{
    public static function model($className = __CLASS__)
    {
        return parent::model($className);
    }

    public function tableName()
    {
        return '{{group_library}}';
    }

    public static function assignPagesForGroup($groupId, $assignedPages)
    {
        self::clearObjectsByGroup($groupId);
        foreach ($assignedPages as $page)
        {
            $groupLibraryRecord = new GroupLibraryStorage();
            $groupLibraryRecord->id = uniqid();
            $groupLibraryRecord->id_group = $groupId;
            $groupLibraryRecord->id_library = $page->libraryId;
            $groupLibraryRecord->id_page = $page->id;
            $groupLibraryRecord->save();
        }
    }

    public static function assignGroupsForPage($page, $groups)
    {
        self::clearObjectsByPage($page->id);
        foreach ($groups as $group)
        {
            $groupLibraryRecord = new GroupLibraryStorage();
            $groupLibraryRecord->id = uniqid();
            $groupLibraryRecord->id_group = $group->id;
            $groupLibraryRecord->id_library = $page->libraryId;
            $groupLibraryRecord->id_page = $page->id;
            $groupLibraryRecord->save();
        }
    }
    
    public static function getGroupIdsByPage($pageId)
    {
        $groupLibraryRecords = self::model()->findAll('id_page=?', array($pageId));
        if (isset($groupLibraryRecords))
            foreach ($groupLibraryRecords as $groupLibraryRecord)
                $groupIds[] = $groupLibraryRecord->id_group;
        if (isset($groupIds))
            return array_unique($groupIds);
    }    

    public static function getLibraryIdsByGroup($groupId)
    {
        $groupLibraryRecords = self::model()->findAll('id_group=?', array($groupId));
        if (isset($groupLibraryRecords))
            foreach ($groupLibraryRecords as $groupLibraryRecord)
                $libraryIds[] = $groupLibraryRecord->id_library;
        if (isset($libraryIds))
            return array_unique($libraryIds);
    }

    public static function getPageIdsByGroup($groupId)
    {
        $groupLibraryRecords = self::model()->findAll('id_group=?', array($groupId));
        if (isset($groupLibraryRecords))
            foreach ($groupLibraryRecords as $groupLibraryRecord)
                $pageIds[] = $groupLibraryRecord->id_page;
        if (isset($pageIds))
            return array_unique($pageIds);
    }

    public static function clearObjectsByGroup($groupId)
    {
        self::model()->deleteAll('id_group=?', array($groupId));
    }

    public static function clearObjectsByPage($pageId)
    {
        self::model()->deleteAll('id_page=?', array($pageId));
    }

}

?>

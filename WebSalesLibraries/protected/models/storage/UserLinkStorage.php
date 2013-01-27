<?php
class UserLinkStorage extends CActiveRecord
{
    public static function model($className = __CLASS__)
    {
        return parent::model($className);
    }

    public function tableName()
    {
        return '{{user_link}}';
    }

    public static function updateData($linkId, $libraryId, $assignedUsers)
    {
        $assignedUsers = explode(',', $assignedUsers);
        foreach ($assignedUsers as $user)
        {
            $userRecord = UserStorage::model()->find('LOWER(login)=?', array(strtolower(trim($user))));
            if (isset($userRecord))
            {
                $userLinkRecord = new UserLinkStorage();
                $userLinkRecord->id = uniqid();
                $userLinkRecord->id_user = $userRecord->id;
                $userLinkRecord->id_link = $linkId;
                $userLinkRecord->id_library = $libraryId;
                $userLinkRecord->save();
            }
        }
    }

    public static function getAvailableLinks($userId)
    {
        foreach (self::model()->findAll('id_user=?', array($userId)) as $userLink)
            $linkIds[] = $userLink->id_link;
        return isset($linkIds) ? $linkIds : null;
    }

    public static function getRestrictedUsersIds($libraryId)
    {
        foreach (self::model()->findAll('id_library=?', array($libraryId)) as $userLink)
            $userIds[] = $userLink->id_user;
        return isset($userIds) ? array_unique($userIds) : null;
    }

    public static function clearData($libraryId)
    {
        self::model()->deleteAll('id_library=?', array($libraryId));
    }
}

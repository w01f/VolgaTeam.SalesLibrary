<?php
class LibraryGroupStorage extends CActiveRecord
{
    public static function model($className = __CLASS__)
    {
        return parent::model($className);
    }

    public function tableName()
    {
        return '{{library_group}}';
    }

    public static function updateData($groupsContent)
    {
        $groupName = '';
        if ($groupsContent !== false)
        {
            foreach ($groupsContent as $line)
            {
                if (strpos($line, '*') !== false)
                    $groupName = trim(str_replace('*', '', $line));
                else if ($groupName != '')
                    $groups[$groupName][] = trim($line);
            }
        }
        if (isset($groups))
        {
            $groupOrder = 0;
            foreach ($groups as $key => $value)
            {
                $groupId = uniqid();

                $groupRecord = new LibraryGroupStorage();
                $groupRecord->id = $groupId;
                $groupRecord->order = $groupOrder;
                $groupRecord->name = $key;
                $groupRecord->save();

                $libraryOrder = 0;
                foreach ($value as $libraryName)
                {
                    $libraryRecord = LibraryStorage::model()->find('name=?', array($libraryName));
                    if (isset($libraryRecord))
                    {
                        $libraryRecord->id_group = $groupId;
                        $libraryRecord->order = $libraryOrder;
                        $libraryRecord->save();
                        $libraryOrder++;
                    }
                }
                $groupOrder++;
            }
        }
    }

    public static function clearData()
    {
        self::model()->deleteAll();
    }

}

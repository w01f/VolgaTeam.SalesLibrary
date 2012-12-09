<?php
class GroupTemplateStorage extends CActiveRecord
{
    public static function model($className = __CLASS__)
    {
        return parent::model($className);
    }

    public function tableName()
    {
        return '{{group_template}}';
    }

    public static function updateData($groupTemplateContent)
    {
        $groupName = '';
        if ($groupTemplateContent !== false)
        {
            foreach ($groupTemplateContent as $line)
                $groupTemplates[] = trim($line);
        }
        if (isset($groupTemplates))
            foreach ($groupTemplates as $groupTemplate)
            {
                $groupTemplateId = uniqid();
                $groupTemplateRecord = new GroupTemplateStorage();
                $groupTemplateRecord->id = $groupTemplateId;
                $groupTemplateRecord->name = $groupTemplate;
                $groupTemplateRecord->save();
            }
    }

    public static function clearData()
    {
        self::model()->deleteAll();
    }

}

?>

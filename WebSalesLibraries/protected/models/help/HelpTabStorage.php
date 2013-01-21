<?php
class HelpTabStorage extends CActiveRecord
{
    public static function model($className = __CLASS__)
    {
        return parent::model($className);
    }

    public function tableName()
    {
        return '{{help_tab}}';
    }

    public static function clearData()
    {
        HelpLinkStorage::clearData();
        HelpPageStorage::clearData();
        self::model()->deleteAll();
    }

}

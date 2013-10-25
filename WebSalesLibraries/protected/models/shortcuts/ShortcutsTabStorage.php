<?php
class ShortcutsTabStorage extends CActiveRecord
{
    public static function model($className = __CLASS__)
    {
        return parent::model($className);
    }

    public function tableName()
    {
        return '{{shortcut_tab}}';
    }

    public static function clearData()
    {
		ShortcutsLinkStorage::clearData();
		ShortcutsPageStorage::clearData();
        self::model()->deleteAll();
    }

}

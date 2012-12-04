<?php
class HelpPageStorage extends CActiveRecord
{
    public static function model($className = __CLASS__)
    {
        return parent::model($className);
    }

    public function tableName()
    {
        return '{{help_page}}';
    }

    public static function clearData()
    {
        self::model()->deleteAll();
    }

}

?>

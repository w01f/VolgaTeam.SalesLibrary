<?php
class SuperFilterStorage extends CActiveRecord
{
    public static function model($className = __CLASS__)
    {
        return parent::model($className);
    }

    public function tableName()
    {
        return '{{super_filter}}';
    }

    public static function updateData($superFilters)
    {
        self::clearData();
        $i = 1;
        foreach ($superFilters as $superFilter)
        {
            $superFilterStorage = new SuperFilterStorage();
            $superFilterStorage->id = $i;
            $superFilterStorage->value = $superFilter['value'];
            $superFilterStorage->save();
            $i++;
        }
    }

    public static function clearData()
    {
        self::model()->deleteAll();
    }

    public static function getData()
    {
        return self::model()->findAll();
    }

}
